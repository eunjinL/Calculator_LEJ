using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class AdvancedCalculator : BaseCalculatorModel
    {
        /**
        * @brief 더하기 연산 재정의
        * @param value1 첫 번째 숫자
        * @param value2 두 번째 숫자
        * @return 두 숫자의 합 반환
        * @note Patch-notes
        * 2023-08-16|이은진|재정의로 console 기능 추가
        */
        public override double Add(BaseCalculatorModel value1, BaseCalculatorModel value2)
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
        public override double Subtract(BaseCalculatorModel value1, BaseCalculatorModel value2)
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
        public override double Multiply(BaseCalculatorModel value1, BaseCalculatorModel value2)
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
        public override double Divide(BaseCalculatorModel value1, BaseCalculatorModel value2)
        {
            Console.WriteLine("나누기 연산 중...");
            return base.Divide(value1, value2);
        }
        public static BaseCalculatorModel operator +(AdvancedCalculator value1, AdvancedCalculator value2)
        {
            Console.WriteLine("\n+ 연산자 재정의...");
            BaseCalculatorModel result = new BaseCalculatorModel();

            double sum = value1.Add(value1, value2);

            result.Result = sum.ToString();
            return result;
        }

        public static BaseCalculatorModel operator -(AdvancedCalculator value1, AdvancedCalculator value2)
        {
            Console.WriteLine("\n- 연산자 재정의...");
            BaseCalculatorModel result = new BaseCalculatorModel();

            double sum = value1.Subtract(value1, value2);

            result.Result = sum.ToString();
            return result;
        }
        public static BaseCalculatorModel operator *(AdvancedCalculator value1, AdvancedCalculator value2)
        {
            Console.WriteLine("\nx 연산자 재정의...");
            BaseCalculatorModel result = new BaseCalculatorModel();

            double sum = value1.Multiply(value1, value2);

            result.Result = sum.ToString();
            return result;
        }
        public static BaseCalculatorModel operator /(AdvancedCalculator value1, AdvancedCalculator value2)
        {
            Console.WriteLine("\n/ 연산자 재정의...");
            BaseCalculatorModel result = new BaseCalculatorModel();

            double sum = value1.Divide(value1, value2);

            result.Result = sum.ToString();
            return result;
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

    }
}
