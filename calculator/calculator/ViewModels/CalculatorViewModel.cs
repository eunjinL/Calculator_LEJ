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
            get { return result; }
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
        public ICommand NumberCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand EqualCommand { get; }
        public ICommand PlusCommand { get; }
        public ICommand MinusCommand { get; }
        public ICommand MultiplyCommand { get; }
        public ICommand DivideCommand { get; }
        public ICommand DotCommand { get; private set; }
        public ICommand SymbolCommand { get; private set; }
        #endregion

        #region [생성자]
        public CalculatorViewModel()
        {
            // 생성자에서 연산자 커멘드 초기화
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
        private void ExecuteDelete(object parameter)
        {
            if (Result.Length > 0)
            {
                CalculationProcess = "";
                Result = "";
            }
        }
        /**
        * @brief 덧셈 연산 수행
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|덧셈 연산 수행
        */
        private void ExecutePlus(object parameter)
        {
            PerformIntermediateCalculation();
            currentOperation = "Add";
            CalculationProcess = string.Format("{0} + ", CalculationProcess);
        }
        /**
        * @brief 뺄셈 연산 수행
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|뺄셈 연산 수행
        */
        private void ExecuteMinus(object parameter)
        {
            PerformIntermediateCalculation();
            currentOperation = "Subtract";
            CalculationProcess = string.Format("{0} - ", CalculationProcess);
        }
        /**
        * @brief 곱셈 연산 수행
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|곱셈 연산 수행
        */
        private void ExecuteMultiply(object parameter)
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
        private void ExecuteDivide(object parameter)
        {
            PerformIntermediateCalculation();
            currentOperation = "Divide";
            CalculationProcess = string.Format("{0} / ", CalculationProcess);
        }
        /**
        * @brief 소수점 추가
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|현재 입력된 숫자의 뒤에 소수점 추가, 입력된 숫자가 없을 경우에는 0.으로 가정
        */
        private void ExecuteDot(object parameter)
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
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|현재 입력된 숫자의 부호를 변경하기
        */
        private void ExecuteSymbol(object parameter)
        {
            if (!string.IsNullOrEmpty(Result) && double.TryParse(Result, out var number))
            {
                double newNumber = -number;
                Result = newNumber.ToString();
            }
        }
        /**
        * @brief 중간 연산 수행
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|중간 연산 수행
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
        * @param parameter - 추가할 숫자 문자열
        * @note Patch-notes
        * 2023-08-09|이은진|등호 연산 수행 및 화면에 표시, 0으로 나눌 경우 경고메세지가 뜨도록 설정
        */
        private void ExecuteEqual(object parameter)
        {
            CalculationProcess = $"{CalculationProcess}{Result}";
            PerformIntermediateCalculation();
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