using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    /// <summary>
    /// Per
    /// </summary>
    class Calculator
    {
        public static SortedList<int, string> calcArgsStr = new SortedList<int, string>();
        // Must use a list because a sorted list does not allow insert into list
        public static List<CalculatorArgument> calcArgsList = new List<CalculatorArgument>();
        private SortedList<int, string> divMulOps = new SortedList<int, string>();

        /// <summary>
        /// Constructor that creates class objects of the type CalculatorArgument and saves 
        ///  them in a list for later access.
        /// </summary>
        public Calculator()
        {
            int position = 0;
            foreach(KeyValuePair<int, string> element in calcArgsStr)
            {
                CalculatorArgument calcArg = new CalculatorArgument(position, element.Value);
                if (calcArg.IsMultDivOp())
                {
                    divMulOps.Add(position, element.Value);
                }
                calcArgsList.Insert(position, calcArg);
                position += 1;
            }
        }

        /// <summary>
        /// Perform Divide Multiply math operations (* /) first then Add Subtract (+ -) operations next
        ///   If we already did /* operation, removed the existing arguments and inserted the result
        ///   then we have to shift the index down by 2 to get to the next arguments that need to be 
        ///   operated on
        /// </summary>
        /// <returns> Returns the Integer result of the calculation </returns>
        public int calculateResult()
        {
            printCalcArgsList("Before");
            bool wasReplaced = false;
            
            foreach (KeyValuePair<int, string> element in divMulOps)
            {
                int index = 0;

                if (wasReplaced)
                {
                    index = element.Key - 2;
                }
                else
                {
                    index = element.Key;
                }
                string multDivOp = element.Value;

                CalculatorArgument argNum1 = calcArgsList[index - 1];
                CalculatorArgument argNum2 = calcArgsList[index + 1];

                //do * or / operations
                int subtotal = PerformCalculation(multDivOp, argNum1.GetNumber(), argNum2.GetNumber() );

                //Put result of * or / operations back in with ALL args list
                wasReplaced = ReplaceWithSubtotal(subtotal, index);
            }

            printCalcArgsList("After*/");

            // What remains in the sorted list are the addition and subtraction operations
            int calcArgsListSize = calcArgsList.Count() + 1;
            for (int i=1; i <= (calcArgsListSize / 3) ; i++)
            {            
                int num1 = calcArgsList.ElementAt(0).GetNumber();
                string oper = calcArgsList.ElementAt(1).GetOperator();
                int num2 = calcArgsList.ElementAt(2).GetNumber();
                int subtotal = PerformCalculation(oper, num1, num2);
                
                wasReplaced = ReplaceWithSubtotal(subtotal, 1);
            }
            // For debug purposes only
            printCalcArgsList("After+-");
            
            int total = calcArgsList.ElementAt(0).GetNumber();
            return total;
        }

        /// <summary>
        /// Perform the math * or / or + or -
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="num1"></param>
        /// <param name="num2"></param>
        /// <returns> Integer result of calculation </returns>
        private static int PerformCalculation(string operation, int num1, int num2)
        {
            if (operation.Equals("*"))
            {
                int resultMult = doMultiplication(num1, num2);
                return resultMult;
            }
            else if (operation.Equals("/"))
            {
                int resultDivd = doDivision(num1, num2);
                    return resultDivd;
            }
            else if (operation.Equals("-"))
            {
                int resultSub = doSubtraction(num1, num2);
                return resultSub;
            }
            else if (operation.Equals("+"))
            {
                int resultAdd = doAddition(num1, num2);
                return resultAdd;
            }
            else          
            {
                throw new InvalidOperationException("Valid operations are * /");
            }
        }

        private static int doAddition(int num1, int num2)
        {
            int result = num1 + num2;
            return result;
        }

        private static int doSubtraction(int num1, int num2)
        {
            int result = num1 - num2;
            return result;
        }

        private static int doMultiplication(int op1, int op2)
        {
            int result = op1 * op2;
            return result;
        }

        private static int doDivision(int op1, int op2)
        {
            int result = op1 / op2;
            return result;
        }

        private bool ReplaceWithSubtotal(int subtotal, int index)
        {
            calcArgsList.RemoveAt(index + 1);
            calcArgsList.RemoveAt(index);
            calcArgsList.RemoveAt(index - 1);

            CalculatorArgument newArg = new CalculatorArgument(index - 1, subtotal.ToString());
            calcArgsList.Insert(index - 1, newArg);
            return true;
        }

        private static void printCalcArgsList(string Timing)
        {
            Console.WriteLine($"Size of calcArgsList {Timing}: {calcArgsList.Count}");
            foreach (CalculatorArgument element in calcArgsList)
            {
                if (element.IsNumber())
                {
                    Console.WriteLine($"calcArgsList*: {element.GetNumber()}");
                }
                else
                {
                    Console.WriteLine($"calcArgsList*: {element.GetOperator()}");
                }
            }
        }


    }
}
