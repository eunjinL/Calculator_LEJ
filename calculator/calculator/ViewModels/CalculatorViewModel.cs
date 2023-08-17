using calculator.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        #region [필드]
        private AdvancedCalculator adv_calc = new AdvancedCalculator();
        private CalculatorModel calculator;
        private bool isHistoryVisible;
        private HistoryItem selectedHistoryItem;
        private ObservableCollection<HistoryItem> historyItems = new ObservableCollection<HistoryItem>();
        private BaseCalculatorModel calcModel = new BaseCalculatorModel();
        #endregion

        #region [속성]
        public double IntermediateResult
        {
            get { return calcModel.IntermediateResult; }
            set
            {
                calcModel.IntermediateResult = value;
                OnPropertyChanged(nameof(IntermediateResult));
            }
        }
        public string CalculationProcess
        {
            get { return calcModel.CalculationProcess; }
            set
            {
                calcModel.CalculationProcess = value;
                OnPropertyChanged(nameof(CalculationProcess));
            }
        }
        public string Result
        {
            get { return calcModel.Result; }
            set
            {
                calcModel.Result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        public HistoryItem SelectedHistoryItem
        {
            get
            {
                return selectedHistoryItem;
            }
            set
            {
                selectedHistoryItem = value;
                if (selectedHistoryItem?.HistoryText != null)
                {
                    var historyText = selectedHistoryItem.HistoryText;
                    var indexOfEqualSign = historyText.IndexOf('=');

                    if (indexOfEqualSign != -1 && indexOfEqualSign < historyText.Length - 1)
                    {
                        var resultString = historyText.Substring(indexOfEqualSign + 1).Trim();
                        Result = resultString; 
                    }
                }
                OnPropertyChanged(nameof(SelectedHistoryItem));
            }
        }
        public bool IsHistoryVisible
        {
            get { return isHistoryVisible; }
            set
            {
                isHistoryVisible = value;
                OnPropertyChanged(nameof(IsHistoryVisible));
            }
        }
        public ObservableCollection<HistoryItem> HistoryItems
        {
            get { return historyItems; }
        }
        
        public ICommand BackCommand { get; }
        public ICommand CopyCommand { get; }
        public ICommand PasteCommand { get; }
        public ICommand HistoryCommand { get; }
        public ICommand Left_ParenthesesCommand { get; private set; }
        public ICommand Right_ParenthesesCommand { get; private set; }
        public ICommand ClearHistoryCommand { get; private set; }
        public ICommand NumberCommand 
        { 
            get; 
            private set; 
        }
        public ICommand DeleteCommand 
        { 
            get;
            private set;
        }
        public ICommand ClearEntryCommand
        {
            get;
            private set;
        }
        
        public ICommand EqualCommand
        { 
            get;
        }
        public ICommand PlusCommand 
        { 
            get;
        }
        public ICommand MinusCommand 
        {
            get;
        }
        public ICommand MultiplyCommand
        {
            get;
        }
        public ICommand DivideCommand
        {
            get;
        }
        public ICommand DotCommand
        {
            get;
            private set;
        }
        public ICommand SymbolCommand
        {
            get;
            private set;
        }
        public ICommand FractionCommand
        {
            get;
            private set;
        }
        public ICommand SquareCommand
        {
            get;
            private set;
        }
        public ICommand SquareRootCommand
        {
            get;
            private set;
        }
        public ICommand SinCommand
        {
            get;
            set;
        }
        public ICommand CosCommand
        {
            get;
            set;
        }
        public ICommand PercentCommand
        {
            get;
            set;
        }

        #endregion

        #region [생성자]
        public CalculatorViewModel()
        {
            calculator = new CalculatorModel();
            NumberCommand = new RelayCommand(ExecuteNumber);
            DeleteCommand = new RelayCommand(ExecuteDelete);
            ClearEntryCommand = new RelayCommand(ExecuteClearEntry);
            BackCommand = new RelayCommand(ExecuteBack);
            EqualCommand = new RelayCommand(ExecuteEqual);
            PlusCommand = new RelayCommand(ExecutePlus);
            MinusCommand = new RelayCommand(ExecuteMinus);
            MultiplyCommand = new RelayCommand(ExecuteMultiply);
            DivideCommand = new RelayCommand(ExecuteDivide);
            DotCommand = new RelayCommand(ExecuteDot);
            SymbolCommand = new RelayCommand(ExecuteSymbol);
            FractionCommand = new RelayCommand(ExecuteFraction);
            SquareCommand = new RelayCommand(ExecuteSquare);
            SquareRootCommand = new RelayCommand(ExecuteSquareRoot);
            SinCommand = new RelayCommand(ExecuteSin);
            CosCommand = new RelayCommand(ExecuteCos);
            PercentCommand = new RelayCommand(ExecutePercent);
            HistoryCommand = new RelayCommand(ToggleHistory);
            ClearHistoryCommand = new RelayCommand(ClearHistory);
            CopyCommand = new RelayCommand(ExecuteCopy);
            PasteCommand = new RelayCommand(ExecutePaste); 
            Left_ParenthesesCommand = new RelayCommand(LeftParenthesesExecute, CanExecute);
            Right_ParenthesesCommand = new RelayCommand(RightParenthesesExecute, CanExecute);

        }
        #endregion

        #region [public 메서드]
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region [protected, private 메서드]
        /**
        * @brief 숫자를 결과창에 추가
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|UI의 숫자를 화면에 출력하기
        */
        private void ExecuteNumber(object parameter)
        {
            string number = parameter as string;
            if (number != null)
            {
                Result += number;
            }
        }
        /**
        * @brief 결과창과 과정창을 리셋
        * @note Patch-notes
        * 2023-08-09|이은진|결과창과 과정창을 리셋
        */
        private void ExecuteDelete()
        {
            if (Result.Length > 0 || CalculationProcess.Length > 0)
            {
                CalculationProcess = "";
                Result = "";
            }
        }
        /**
        * @brief 결과창을 리셋
        * @note Patch-notes
        * 2023-08-09|이은진|결과창을 리셋
        */
        private void ExecuteClearEntry()
        {
            if (Result.Length > 0)
            {
                Result = "";
            }
        }
        /**
        * @brief 뒤로가기, 하나 지우기 기능
        * @note Patch-notes
        * 2023-08-14|이은진|결과창의 숫자 하나 지우기
        */
        private void ExecuteBack()
        {
            if (Result.Length > 0)
            {
                Result = Result.Substring(0, Result.Length - 1);
            }
        }
        /**
        * @brief 덧셈 연산 수행
        * @note Patch-notes
        * 2023-08-09|이은진|덧셈 연산 수행
        */
        private void ExecutePlus()
        {
            PerformIntermediateCalculation();
            CalculationProcess = string.Format("{0} + ", CalculationProcess);
        }
        /**
        * @brief 뺄셈 연산 수행
        * @note Patch-notes
        * 2023-08-09|이은진|뺄셈 연산 수행
        */
        private void ExecuteMinus()
        {
            PerformIntermediateCalculation();
            CalculationProcess = string.Format("{0} - ", CalculationProcess);
        }
        /**
        * @brief 곱셈 연산 수행
        * @note Patch-notes
        * 2023-08-09|이은진|곱셈 연산 수행
        */
        private void ExecuteMultiply()
        {
            PerformIntermediateCalculation();
            CalculationProcess = string.Format("{0} x ", CalculationProcess);
        }
        /**
        * @brief 나눗셈 연산 수행
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|나눗셈 연산 수행
        */
        private void ExecuteDivide()
        {
            PerformIntermediateCalculation();
            CalculationProcess = string.Format("{0} / ", CalculationProcess);
        }
        /**
        * @brief 소수점 추가
        * @note Patch-notes
        * 2023-08-09|이은진|현재 입력된 숫자의 뒤에 소수점 추가, 입력된 숫자가 없을 경우에는 0.으로 가정
        */
        private void ExecuteDot()
        {
            if (string.IsNullOrEmpty(Result) || !Result.Contains("."))
            {
                if (string.IsNullOrEmpty(Result))
                {
                    Result = $"{Result}0.";
                }
                else
                {
                    Result = $"{Result}.";
                }
            }
        }
        /**
        * @brief 부호 변경
        * @note Patch-notes
        * 2023-08-09|이은진|현재 입력된 숫자의 부호를 변경하기
        */
        private void ExecuteSymbol()
        {
            if (!string.IsNullOrEmpty(Result) && double.TryParse(Result, out var number))
            {
                double newNumber = -number;
                Result = newNumber.ToString();
            }
        }
        /**
        * @brief Sin 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Sin 연산 코드 작성
        */
        public void ExecuteSin()
        {
            if (double.TryParse(Result, out var number))
            {
                var result = adv_calc.Sin(number);
                Result = result.ToString();
            }
        }
        /**
        * @brief Cos 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Cos 연산 코드 작성
        */
        public void ExecuteCos()
        {
            if (double.TryParse(Result, out var number))
            {
                var result = adv_calc.Cos(number);
                Result = result.ToString();
            }
        }
        /**
        * @brief Percent 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Percent 연산 코드 작성
        */
        public void ExecutePercent()
        {
            if (double.TryParse(Result, out var number))
            {
                var result = adv_calc.Percent(number);
                Result = result.ToString();
            }
        }
        /**
        * @brief 중간 연산 수행
        * @note Patch-notes
        * 2023-08-09|이은진|연산자를 누를때 입력된 피연산자 숫자와 함께 계산 과정에 추가하고, 결과창 비움
        * 2023-08-14|이은진|숫자가 들어오지 않은 상태에서 연산자만 들어오면, 앞에 0을 붙여줌 
        */
        private void PerformIntermediateCalculation()
        {
            if (IsOperator(Result) && (CalculationProcess.Length > 0 && IsOperator(CalculationProcess.Last().ToString())))
            {
                CalculationProcess = $"{CalculationProcess}0{Result}";
            }
            else
            {
                CalculationProcess = $"{CalculationProcess}{Result}";
            }

            Result = "";
        }
        /**
        * @brief 연산자 여부 확인
        * @note Patch-notes
        * 2023-08-17|이은진|괄호는 연산자가 아님
        */
        private bool IsOperator(string value)
        {
            // 괄호를 연산자에서 제외
            string[] operators = { "+", "-", "*", "/" };
            return operators.Contains(value);
        }
        /**
        * @brief 등호 연산 수행, 입력된 계산 과정을 합쳐서 최종 수식을 만들고, 중위 표기법에서 후위 표기법으로 바꿈, 연산 결과 출력 후 계산 과정을 비움
        * @note Patch-notes
        * 2023-08-09|이은진|등호 연산 수행 및 화면에 표시, 0으로 나눌 경우 경고메세지가 뜨도록 설정
        * 2023-08-14|이은진|연산자가 마지막으로 들어오면 해당 연산자를 string 에서 빼버리고 시작하도록 수정
        */
        private void ExecuteEqual()
        {
            string input = $"{CalculationProcess}{Result}".TrimEnd();
            if ("+-*/(".Contains(input.Last()))
            {
                input = input.Remove(input.Length - 1);
            }
            string withoutSpaces = input.Replace(" ", "");
            string postfixExpression = calculator.ConvertToPostfix(withoutSpaces);
            IntermediateResult = calculator.EvaluatePostfix(postfixExpression);

            if (IntermediateResult.ToString() == "NaN")
            {
                Result = "0으로 나눌 수 없습니다.";
            }
            else
            {
                Result = IntermediateResult.ToString();
            }
            historyItems.Add(new HistoryItem { HistoryText = input + " = " + Result });
            IntermediateResult = 0;
            CalculationProcess = "";
        }
        /**
        * @brief 함수 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Fraction 연산 코드 작성
        */
        private void ExecuteFraction()
        {
            if (double.TryParse(Result, out var number))
            {
                var result = adv_calc.Fraction(number);
                if (double.IsNaN(result))
                {
                    Result = "0으로 나눌 수 없습니다.";
                }
                else
                {
                    Result = result.ToString();
                }
            }
        }
        /**
        * @brief 제곱 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Square 연산 코드 작성
        */
        private void ExecuteSquare()
        {
            if (double.TryParse(Result, out var number))
            {
                Result = adv_calc.Square(number).ToString();
            }
        }
        /**
        * @brief 제곱근 연산 수행
        * @return 없음
        * @note Patch-notes
        * 2023-08-10|이은진|Square 연산 코드 작성
        */
        private void ExecuteSquareRoot()
        {
            if (double.TryParse(Result, out var number))
            {
                var result = adv_calc.SquareRoot(number);
                if (double.IsNaN(result))
                {
                    Result = "음수에 대한 제곱근의 제곱은 정의되지 않습니다.";
                }
                else
                {
                    Result = result.ToString();
                }
            }
        }
        /**
        * @brief History 버튼 클릭시 리스트뷰 보이게 하기
        * @return 없음
        * @note Patch-notes
        * 2023-08-14|이은진|연산 기록 보이게
        */
        private void ToggleHistory()
        {
            IsHistoryVisible = !IsHistoryVisible;
        }
        /**
        * @brief History clear 버튼 클릭시 리스트뷰 리셋
        * @return 없음
        * @note Patch-notes
        * 2023-08-17|이은진|연산 기록 삭제
        */
        private void ClearHistory()
        {
            HistoryItems.Clear();
        }
        /**
        * @brief 연산 결과 ctrl+c기능 구현
        * @return 없음
        * @note Patch-notes
        * 2023-08-14|이은진|result값 복사
        */
        private void ExecuteCopy(object parameter)
        {
            Clipboard.SetText(Result);
        }
        /**
        * @brief History ctrl+v기능 구현
        * @return 없음
        * @note Patch-notes
        * 2023-08-14|이은진|붙여넣기 기능 구현, result에 붙여넣은 값이 들어감
        */
        private void ExecutePaste(object parameter)
        {
            string clipboardText = Clipboard.GetText();
            Result = clipboardText.ToString();
        }
        private void LeftParenthesesExecute(object parameter)
        {
            CalculationProcess += "(";
            Result = "";
        }

        private void RightParenthesesExecute(object parameter)
        {
            if (!string.IsNullOrEmpty(Result))
            {
                CalculationProcess += Result;
                Result = "";
            }
            CalculationProcess += ")";
        }

        private bool CanExecute(object parameter)
        {
            // 버튼을 활성화 또는 비활성화해야 하는 조건
            return true; 
        }

        /**
        * @brief 속성 변경 알림 이벤트 발생
        * @param parameter - 변경된 속성의 이름
        * @note Patch-notes
        * 2023-08-09|이은진|속성 변경 알림 이벤트 발생
        */
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}