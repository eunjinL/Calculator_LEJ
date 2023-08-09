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
        * @param a 첫 번째 숫자
        * @param b 두 번째 숫자
        * @return 두 숫자의 합 반환
        * @note Patch-notes
        * 2023-08-09|이은진|더하기 기능
        */
        public double Add(double a, double b)
        {
            return a + b;
        }
        /**
        * @brief 두 수를 뺀다
        * @param a 첫 번째 숫자
        * @param b 두 번째 숫자
        * @return 두 숫자의 차 반환
        * @note Patch-notes
        * 2023-08-09|이은진|빼기 기능
        */
        public double Subtract(double a, double b)
        {
            return a - b;
        }
        /**
        * @brief 두 수를 곱한다
        * @param a 첫 번째 숫자
        * @param b 두 번째 숫자
        * @return 두 숫자의 곱 반환
        * @note Patch-notes
        * 2023-08-09|이은진|곱하기 기능
        */
        public double Multiply(double a, double b)
        {
            return a * b;
        }
        /**
        * @brief 두 수를 나눈다
        * @param a 분자
        * @param b 분모
        * @return 분자를 분모로 나눈 결과 반환, 분모가 0일 경우 NaN 반환
        * @note Patch-notes
        * 2023-08-09|이은진|나누기 기능
        */
        public double Divide(double a, double b)
        {
            if (b == 0)
                return double.NaN;
            return a / b;
        }
    }
}
