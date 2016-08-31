using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    class CalcNumbers
    {
        private int _position;
        private int _number;

        public CalcNumbers (int position, string str_number)
        {
            this._position = position;
            this._number = ValidateNumber(str_number);
        }

        public int GetPosition()
        {
            return this._position; 
        }
        public void SetPosition(int position)
        {
            this._position = position;
        }

        public int GetNumber()
        {
            return this._number; 
        }
        public void SetNumber(string str_number)
        {
            this._number = ValidateNumber(str_number);
        }

        private static int ValidateNumber(string str_number)
        {
            //make sure numbers are valid
            int num;
            bool test = int.TryParse(str_number, out num);
            if (test == false)
            {
                throw new FormatException("Numbers must be integer values e.g. 4 100\n");
            }
            return num;
        }
    }
}
