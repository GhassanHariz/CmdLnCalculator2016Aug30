using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    class CalcMathOps
    {
        private int _position;
        private string _operator;

        public CalcMathOps(int position, string mathOperator)
        {
            this._position = position;
            this._operator = validateOp(mathOperator);
        }

        private static string validateOp(string str_operator)
        {
            //make sure operators are valid
            if (!(str_operator.Equals("*") || str_operator.Equals("/") || str_operator.Equals("+") || str_operator.Equals("-")))
            {
                throw new InvalidOperationException("Valid operations are / * + -");
            }
            return str_operator;
        }
    }
}
