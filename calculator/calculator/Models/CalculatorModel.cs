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

        public double Square(double value)
        {
            return value * value;
        }
    }
}
