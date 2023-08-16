using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class CalculatorModel : BaseCalculatorModel
    {
        /**
        * @brief 더하기 연산 재정의
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 합 반환
        * @note Patch-notes
        * 2023-08-16|이은진|재정의로 console 기능 추가
        */
        public override double Add(double value1, double value2)
        {
            Console.WriteLine("더하기 연산 중...");
            return base.Add(value1, value2);
        }
        /**
        * @brief 빼기 연산 재정의
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 차 반환
        * @note Patch-notes
        * 2023-08-16|이은진|재정의로 console 기능 추가
        */
        public override double Subtract(double value1, double value2)
        {
            Console.WriteLine("빼기 연산 중...");
            return base.Subtract(value1, value2);
        }
        /**
        * @brief 곱하기 연산 재정의
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 곱 반환
        * @note Patch-notes
        * 2023-08-16|이은진|재정의로 console 기능 추가
        */
        public override double Multiply(double value1, double value2)
        {
            Console.WriteLine("곱하기 연산 중...");
            return base.Multiply(value1, value2);
        }
        /**
        * @brief 나누기 연산 재정의
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 나눈값 반환
        * @note Patch-notes
        * 2023-08-16|이은진|재정의로 console 기능 추가
        */
        public override double Divide(double value1, double value2)
        {
            Console.WriteLine("나누기 연산 중...");
            return base.Divide(value1, value2);
        }
        /**
        * @brief 분수 연산 기능
        * @param value - 분수 계산할 값
        * @return 분수 결과 반환, 0으로 나누려는 경우 NaN 반환
        * @note Patch-notes
        * 2023-08-10|이은진|분수 연산 기능
        */
        public double Fraction(double value)
        {
            if (value == 0)
            {
                return double.NaN;
            }
            else
            {
                return 1 / value;
            }
        }
        /**
         * @brief 제곱 연산 기능
         * @param value - 제곱할 값
         * @return 제곱 결과 반환
         * @note Patch-notes
         * 2023-08-10|이은진|제곱 연산 기능
         */
        public double Square(double value)
        {
            return value * value;
        }
        /**
         * @brief 제곱근 연산 기능
         * @param value - 제곱근을 구할 값
         * @return 제곱근 결과 반환, 음수의 경우 NaN 반환
         * @note Patch-notes
         * 2023-08-10|이은진|제곱근 연산 기능
         */
        public double SquareRoot(double value)
        {
            if (value < 0)
            {
                return double.NaN;
            }
            return Math.Sqrt(value);
        }
        /**
        * @brief Sin 연산 기능
        * @param parameter - 추가할 숫자 문자열
        * @return sin 연산 결과 반환
        * @note Patch-notes
        * 2023-08-10|이은진|입력된 숫자를 각도라고 계산하기 위해 각도 -> 라디안으로 변환하는 코드 추가
        */
        public double Sin(double value)
        {
            double radians = (value * Math.PI) / 180;
            double result = Math.Sin(radians);
            if (Math.Abs(result) < 1E-15)
            {
                result = 0;
            }

            return result;
        }
        /**
         * @brief 코싸인 연산 기능
         * @param value - 입력된 각도 값 (도)
         * @return cos 연산 결과 반환 (각도 단위로 계산됨)
         * @note Patch-notes
         * 2023-08-10|이은진|입력된 숫자를 각도라고 계산하기 위해 각도 -> 라디안으로 변환하는 코드 추가
         */
        public double Cos(double value)
        {
            double radians = (value * Math.PI) / 180;
            double result = Math.Cos(radians);

            if (Math.Abs(result) < 1E-15)
            {
                result = 0;
            }

            return result;
        }
        /**
         * @brief Percent 연산 기능
         * @param value - 퍼센트로 변환할 값
         * @return 퍼센트 결과 반환 (값 / 100)
         * @note Patch-notes
         * 2023-08-10|이은진|퍼센트 연산 기능
         */
        public double Percent(double value)
        {
            return value / 100;
        }
        /**
        * @brief 중위 표기법을 후위 표기법으로 변환
        * @param infix - 중위 표기법으로 표현된 수식
        * @return 후위 표기법으로 변환된 수식 문자열 반환
        * @note Patch-notes
        * 2023-08-11|이은진|중위를 후위 표기법으로 변환
        * 2023-08-14|이은진|후위 표기법으로 변환 시 음수는 연산자가 아니라 부호로 인식되게 변경
        */
        public string ConvertToPostfix(string infix)
        {
            Stack<char> operatorStack = new Stack<char>();
            StringBuilder postfix = new StringBuilder();

            for (int i = 0; i < infix.Length; i++)
            {
                char token = infix[i];

                if (char.IsDigit(token) || token == '.')
                {
                    postfix.Append(token);
                }
                else if (token == '-' && (i == 0 || !char.IsDigit(infix[i - 1])))
                {
                    postfix.Append(token);
                }
                else
                {
                    postfix.Append(" ");

                    while (operatorStack.Count > 0 && Precedence(token) <= Precedence(operatorStack.Peek()))
                    {
                        postfix.Append(operatorStack.Pop() + " ");
                    }
                    operatorStack.Push(token);
                }
            }

            postfix.Append(" ");

            while (operatorStack.Count > 0)
            {
                postfix.Append(operatorStack.Pop() + " ");
            }

            return postfix.ToString().Trim();
        }

        /**
        * @brief 연산자의 우선순위를 반환
        * @param operatorSymbol - 우선순위를 판별할 연산자 기호
        * @return 연산자의 우선순위 반환 (덧셈과 뺄셈은 1, 곱셈과 나눗셈은 2)
        * @note Patch-notes
        * 2023-08-11|이은진|연산자 우선순위 판별
        */
        public int Precedence(char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                case '-':
                    return 1;
                case 'x':
                case '/':
                    return 2;
                default:
                    throw new ArgumentException("연산 기호 오류");
            }
        }
        /**
        * @brief 후위 표기법 수식을 계산
        * @param postfix - 후위 표기법으로 표현된 수식
        * @return 계산된 결과값 반환
        * @note Patch-notes
        * 2023-08-11|이은진|후위 표기법 수식 계산
        */
        public double EvaluatePostfix(string postfix)
        {
            Stack<double> operandStack = new Stack<double>();

            foreach (string token in postfix.Split(' '))
            {
                if (double.TryParse(token, out double number))
                {
                    operandStack.Push(number);
                }
                else
                {
                    double operand2 = operandStack.Pop();
                    double operand1 = operandStack.Pop();
                    double result = Operate(operand1, operand2, token[0]);
                    operandStack.Push(result);
                }
            }

            return operandStack.Pop();
        }
        /**
        * @brief 두 피연산자에 연산자를 적용
        * @param operand1 - 첫 번째 피연산자
        * @param operand2 - 두 번째 피연산자
        * @param operatorSymbol - 적용할 연산자 기호
        * @return 두 피연산자에 연산자를 적용한 결과 반환
        * @note Patch-notes
        * 2023-08-11|이은진|연산자 적용
        */
        public double Operate(double value1, double value2, char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return Add(value1, value2);
                case '-':
                    return Subtract(value1, value2);
                case 'x':
                    return Multiply(value1, value2);
                case '/':
                    return Divide(value1, value2);
                default:
                    throw new ArgumentException("연산 기호 오류");
            }
        }
    }
}