using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class BaseCalculatorModel
    {
        public double IntermediateResult { get; set; }
        public string CalculationProcess { get; set; } = "";
        public string Result { get; set; } = "";
        /**
        * @brief 두 수를 더한다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 합 반환
        * @note Patch-notes
        * 2023-08-09|이은진|더하기 기능
        */
        public virtual double Add(BaseCalculatorModel value1, BaseCalculatorModel value2)
        {
            double parsedValue1 = double.Parse(value1.Result);
            double parsedValue2 = double.Parse(value2.Result);
            return parsedValue1 + parsedValue2;
        }
        /**
        * @brief 두 수를 뺀다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 차 반환
        * @note Patch-notes
        * 2023-08-09|이은진|빼기 기능
        */
        public virtual double Subtract(BaseCalculatorModel value1, BaseCalculatorModel value2)
        {
            double parsedValue1 = double.Parse(value1.Result);
            double parsedValue2 = double.Parse(value2.Result);
            return Math.Round(parsedValue1 - parsedValue2, 5);
        }
        /**
        * @brief 두 수를 곱한다
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 곱 반환
        * @note Patch-notes
        * 2023-08-09|이은진|곱하기 기능
        */
        public virtual double Multiply(BaseCalculatorModel value1, BaseCalculatorModel value2)
        {
            double parsedValue1 = double.Parse(value1.Result);
            double parsedValue2 = double.Parse(value2.Result);
            return parsedValue1 * parsedValue2;
        }
        /**
        * @brief 두 수를 나눈다
        * @param value1 분자
        * @param value2 분모
        * @return 분자를 분모로 나눈 결과 반환, 분모가 0일 경우 NaN 반환
        * @note Patch-notes
        * 2023-08-09|이은진|나누기 기능
        */
        public virtual double Divide(BaseCalculatorModel value1, BaseCalculatorModel value2)
        {
            double parsedValue1 = double.Parse(value1.Result);
            double parsedValue2 = double.Parse(value2.Result);
            if (parsedValue2 == 0)
            {
                return double.NaN;
            }
            return parsedValue1 / parsedValue2;
        }
    }
}
