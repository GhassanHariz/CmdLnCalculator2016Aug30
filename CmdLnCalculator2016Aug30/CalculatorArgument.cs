using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    /// <summary>
    /// Has definition of calculator argument object: either an integer or a valid operator (/ * - +).
    /// Does validation on the arguments in the constructor.
    /// </summary>
    class CalculatorArgument
    {
        private int _position;
        private int _number;
        private string _operator;
        public enum argTypes {  mathOper, number };
        private argTypes _type;

        /// <summary>
        /// Constructor that validates the arguments as valid integers.  
        /// Validation: allows negative integers but not floats (with decimal point).
        /// Creates the CalculatorArgument objects of the validated arguments.
        /// </summary>
        /// <param name="position"></param>
        /// <param name="strArgument"></param>
        public CalculatorArgument(int position, string strArgument)
        {
            this._position = position;
            int num;

            //match any Math operator beginning with and ending with either * + - /
            Regex regexOps = new Regex(@"^[\*\+-/]$");

            //Regex regexNumbers = new Regex(@"^[0-9]+$"); -- does not catch floating pt numbers with decimal pt

            // match any digit (could be -ve digit and could be floating pt with .) *? means 0 or more times
            Regex regexNumbers = new Regex(@"^-*?\d*?\.*?\d*?$"); 
            Regex regexOtherOps = new Regex(@"^[~!#\$%\^&\(\)]*$");

            //make sure arguments are valid numbers or operators
            if (regexOps.IsMatch(strArgument))
            {
                this._operator = isValidOp(strArgument);
                this._type = argTypes.mathOper;
            }
            else if (regexNumbers.IsMatch(strArgument))
            {
                if (isNumber(strArgument, out num))
                {
                    this._number = num;
                    this._type = argTypes.number;
                }
            }
            else if (regexOtherOps.IsMatch(strArgument))
            {
                throw new InvalidOperationException("Valid operations are / * + -");
            }
            else
            {
                throw new InvalidOperationException("Numbers must be integers. Operators must be: / * + -");
            }
        }

        private static bool isNumber(string str_number, out int number)
        {
            //make sure numbers are valid
            int validatedNum;
            bool test = int.TryParse(str_number, out validatedNum);
            if (test == true)
            {
                number = validatedNum;
                return true;
            }
            else
            { 
                throw new FormatException("Numbers must be integer values e.g. 4 100\n");
            }
        }

        private static string isValidOp(string str_operator)
        {
            //make sure operators are valid
            if ((str_operator.Equals("*") || str_operator.Equals("/") || str_operator.Equals("+") || str_operator.Equals("-")))
            {
                string op = str_operator;
                return op;
            }
            else
            {
                throw new InvalidOperationException("Valid operations are / * + -");
            }
        }

        public int GetPosition()
        {
            return this._position;
        }

        public int GetNumber()
        {
            return this._number;
        }

        public string GetOperator()
        {
            return this._operator;
        }

        public argTypes GetArgType()
        {
            return this._type;
        } 

        public bool IsMultDivOp()
        {
            if (this._type == argTypes.mathOper) 
            {
                if ( this._operator.Equals("*") || this._operator.Equals("/") )
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool IsOperator()
        {
            if (this._type == argTypes.mathOper)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
        public bool IsNumber()
        {
            if (this._type == argTypes.number)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }


}
