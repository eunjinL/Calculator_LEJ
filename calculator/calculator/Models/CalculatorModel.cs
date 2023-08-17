using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class CalculatorModel : AdvancedCalculator
    {
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
                else if (token == '(')
                {
                    operatorStack.Push(token);
                }
                else if (token == ')')
                {
                    while (operatorStack.Count > 0 && operatorStack.Peek() != '(')
                    {
                        postfix.Append(" " + operatorStack.Pop() + " ");
                    }
                    if (operatorStack.Count > 0 && operatorStack.Peek() == '(')
                    {
                        operatorStack.Pop();
                    }
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
                case '(':
                case ')':
                    return 0; // 괄호의 우선순위는 가장 높거나 낮음으로 설정합니다.
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
                else if (IsOperator(token))
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
            AdvancedCalculator modelValue1 = new AdvancedCalculator { Result = value1.ToString() };
            AdvancedCalculator modelValue2 = new AdvancedCalculator { Result = value2.ToString() };
            BaseCalculatorModel result;

            switch (operatorSymbol)
            {
                case '+':
                    result = modelValue1 + modelValue2;
                    break;
                case '-':
                    result = modelValue1 - modelValue2;
                    break;
                case 'x':
                    result = modelValue1 * modelValue2;
                    break;
                case '/':
                    result = modelValue1 / modelValue2;
                    break;
                default:
                    throw new ArgumentException("연산 기호 오류");
            }
            return double.Parse(result.Result);
        }


        private bool IsOperator(string token)
        {
            return token == "+" || token == "-" || token == "x" || token == "/";
        }

    }
}