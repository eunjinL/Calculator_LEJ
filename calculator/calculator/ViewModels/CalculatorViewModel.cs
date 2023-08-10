using calculator.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace calculator.ViewModels
{
    public class CalculatorViewModel : INotifyPropertyChanged
    {
        #region [필드]
        private CalculatorModel calculator;
        private double intermediateResult;
        private string currentOperation;
        private string calculationProcess = "";
        private string result = "";
        #endregion

        #region [속성]
        public string Result
        {
            get
            {
                return result;
            }
            set
            {
                result = value;
                OnPropertyChanged(nameof(Result));
            }
        }
        public string CalculationProcess
        {
            get
            {
                return calculationProcess;
            }
            set
            {
                calculationProcess = value;
                OnPropertyChanged(nameof(CalculationProcess));
            }
        }
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
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|결과창과 과정창을 리셋
        */
        private void ExecuteDelete()
        {
            if (Result.Length > 0)
            {
                CalculationProcess = "";
                Result = "";
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
            currentOperation = "Add";
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
            currentOperation = "Subtract";
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
            currentOperation = "Multiply";
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
            currentOperation = "Divide";
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
                var result = calculator.Sin(number);
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
                var result = calculator.Cos(number);
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
                var result = calculator.Percent(number);
                Result = result.ToString();
            }
        }
        /**
        * @brief 중간 연산 수행
        * @note Patch-notes
        * 2023-08-09|이은진|연산자를 누를때 해당 기능 연산 함수 호출
        */
        private void PerformIntermediateCalculation()
        {
            if (result != "")
            {
                double operand = double.Parse(Result);

                switch (currentOperation)
                {
                    case "Add":
                        intermediateResult = calculator.Add(intermediateResult, operand);
                        break;
                    case "Subtract":
                        intermediateResult = calculator.Subtract(intermediateResult, operand);
                        break;
                    case "Multiply":
                        intermediateResult = calculator.Multiply(intermediateResult, operand);
                        break;
                    case "Divide":
                        intermediateResult = calculator.Divide(intermediateResult, operand);
                        break;
                    default:
                        intermediateResult = operand;
                        break;
                }
                CalculationProcess = $"{CalculationProcess}{Result}";
                Result = "";
            }
        }
        /**
        * @brief 등호 연산 수행 및 결과 표시
        * @note Patch-notes
        * 2023-08-09|이은진|등호 연산 수행 및 화면에 표시, 0으로 나눌 경우 경고메세지가 뜨도록 설정
        */
        private void ExecuteEqual()
        {
            string input = $"{CalculationProcess}{Result}";
            string withoutSpaces = input.Replace(" ", "");
            // 중위 표기법을 후위 표기법으로 변환
            string postfixExpression = calculator.ConvertToPostfix(withoutSpaces);
            // 후위 표기법을 사용하여 연산 수행
            intermediateResult = calculator.EvaluatePostfix(postfixExpression);

            currentOperation = null;
            if (intermediateResult.ToString() == "NaN")
            {
                Result = "0으로 나눌 수 없습니다.";
            }
            else
            {
                Result = intermediateResult.ToString();
            }
            intermediateResult = 0;
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
                var result = calculator.Fraction(number);
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
                Result = calculator.Square(number).ToString();
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
                var result = calculator.SquareRoot(number);
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