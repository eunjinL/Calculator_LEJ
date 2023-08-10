using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class CalculatorModel
    {
        /**
        * @brief 두 수를 더한다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 합 반환
        * @note Patch-notes
        * 2023-08-09|이은진|더하기 기능
        */
        public double Add(double value1, double value2)
        {
            return value1 + value2;
        }
        /**
        * @brief 두 수를 뺀다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 차 반환
        * @note Patch-notes
        * 2023-08-09|이은진|빼기 기능
        */
        public double Subtract(double value1, double value2)
        {
            return value1 - value2;
        }
        /**
        * @brief 두 수를 곱한다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 곱 반환
        * @note Patch-notes
        * 2023-08-09|이은진|곱하기 기능
        */
        public double Multiply(double value1, double value2)
        {
            return value1 * value2;
        }
        /**
        * @brief 두 수를 나눈다
        * @param value1 분자
        * @param value2 분모
        * @return 분자를 분모로 나눈 결과 반환, 분모가 0일 경우 NaN 반환
        * @note Patch-notes
        * 2023-08-09|이은진|나누기 기능
        */
        public double Divide(double value1, double value2)
        {
            if (value2 == 0)
            {
                return double.NaN;
            }
            return value1 / value2;
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

        public string ConvertToPostfix(string infix)
        {
            Stack<char> operatorStack = new Stack<char>();
            StringBuilder postfix = new StringBuilder();

            for (int i = 0; i < infix.Length; i++)
            {
                char token = infix[i];

                if (char.IsDigit(token) || token == '.')
                {
                    // 숫자를 계속 누적합니다.
                    postfix.Append(token);
                }
                else
                {
                    // 숫자가 끝나면 공백을 추가합니다.
                    postfix.Append(" ");

                    // 현재 연산자의 우선순위가 스택의 맨 위 연산자보다 낮거나 같다면, 스택에서 꺼내 후위 표기식에 추가합니다.
                    while (operatorStack.Count > 0 && Precedence(token) <= Precedence(operatorStack.Peek()))
                    {
                        postfix.Append(operatorStack.Pop() + " ");
                    }
                    operatorStack.Push(token);
                }
            }

            // 마지막 숫자에 공백을 추가합니다.
            postfix.Append(" ");

            // 남은 연산자를 꺼내 후위 표기식에 추가합니다.
            while (operatorStack.Count > 0)
            {
                char operatorSymbol = operatorStack.Pop();
                if (Precedence(operatorSymbol) == -1)
                {
                    throw new ArgumentException("Invalid operator symbol");
                }
                postfix.Append(operatorSymbol + " ");
            }

            return postfix.ToString().Trim();
        }


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
                    throw new ArgumentException("Invalid operator symbol");
            }
        }

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
                    double operand2 = operandStack.Pop(); // 두 번째 피연산자
                    double operand1 = operandStack.Pop(); // 첫 번째 피연산자
                    double result = Operate(operand1, operand2, token[0]);
                    operandStack.Push(result);
                }
            }

            return operandStack.Pop();
        }

        public double Operate(double operand1, double operand2, char operatorSymbol)
        {
            switch (operatorSymbol)
            {
                case '+':
                    return operand1 + operand2;
                case '-':
                    return operand1 - operand2;
                case 'x':
                    return operand1 * operand2;
                case '/':
                    if (operand2 != 0)
                    {
                        return operand1 / operand2;
                    }
                    else
                    {
                        throw new DivideByZeroException("Division by zero");
                    }
                default:
                    throw new ArgumentException("Invalid operator symbol");
            }
        }
    }
}