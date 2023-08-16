using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace calculator.Models
{
    public class ComplexNumberModel
    {
        public double Real { get; set; }
        public double Imaginary { get; set; }

        public ComplexNumberModel(double real, double imaginary)
        {
            Real = real;
            Imaginary = imaginary;
        }

        public static ComplexNumberModel operator +(ComplexNumberModel a, ComplexNumberModel b)
        {
            return new ComplexNumberModel(a.Real + b.Real, a.Imaginary + b.Imaginary);
        }

        public static ComplexNumberModel operator -(ComplexNumberModel a, ComplexNumberModel b)
        {
            return new ComplexNumberModel(a.Real - b.Real, a.Imaginary - b.Imaginary);
        }

        public override string ToString()
        {
            return $"{Real} + {Imaginary}i";
        }
    }
}
