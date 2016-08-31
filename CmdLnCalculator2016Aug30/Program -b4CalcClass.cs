using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    class Program
    {
        static int Main(string[] args)
        {
            int num_args = args.Length;

            // make sure there are 3 or more arguments e.g. 1 + 2
            if ( num_args <= 3 )
            {
                Console.WriteLine("Please enter the numbers and operands for which you need the Total. \n");
                Console.WriteLine("Usage: calc < number operator number operator number... > \n");
                Console.WriteLine("Example: 3 + 4 * 6 / 2 \n");
                Console.ReadLine();
                return 1;
            }

            // Lists needed to store the arguments
            List<CalcNumbers> all_num_args = new List<CalcNumbers>();
            List<CalcMathOps> all_op_args = new List<CalcMathOps>();

            SortedList<int, string> divmul_ops = new SortedList<int, string>();
            SortedList<int, string> all_args = new SortedList<int, string>();

            int position = 1;
            foreach (string str in args)
            {
                try
                {
                    CalcNumbers calc_num;
                    CalcMathOps calc_op;

                    // numbers are in ODD locations in the args list
                    if ((position % 2) != 0)
                    {
                        calc_num = new CalcNumbers(position - 1, str);
                        //save all args to a list
                        all_num_args.Add(calc_num);
                    }
                    else // math operators are always positioned in EVEN locations in the args list
                    {
                        calc_op = new CalcMathOps(position, str);
                        //save all the operators to a list
                        all_op_args.Add(calc_op);
                        //save the * / operator and its position 
                        if (str.Equals("*") || str.Equals("/"))
                        {
                            divmul_ops.Add(position - 1, str);
                        }
                    }
                    all_args.Add(position - 1, str);
                    position += 1;
                }
                catch(FormatException FEx)
                {
                    Console.WriteLine(FEx.Message);
                    Console.ReadLine();
                    return 1;
                }
                catch (InvalidOperationException IEx)
                {
                    Console.WriteLine(IEx.Message);
                    Console.ReadLine();
                    return 1;
                }

            }

            // test
            foreach (CalcNumbers num in all_num_args)
            {
                int numPosition = num.GetPosition();
                int number = num.GetNumber();
                Console.WriteLine($"Index: {numPosition}, Number: {number}\n");
            }

            // perform math operations * / first
            foreach (KeyValuePair<int, string> element in divmul_ops)
            {
                int index = element.Key;
                string operation = element.Value;
                string operand1 = all_args[index - 1];
                string operand2 = all_args[index + 1];

                Console.WriteLine($"Index: {index}, Operation: {operation}");
                Console.WriteLine($"numbers: {operand1}, {operand2}\n");

                //do * or / operations
                //int subtotal = PerformMath(operation, operand1, operand2 );

                //Put result of * or / operations back in with ALL args list
                //bool replaced = ReplaseWithSubtotal(subtotal, index); 
            }

            //perform rest of the math operations and get total
            Console.WriteLine($"Size of AllArgs: {all_args.Count}");
            foreach (KeyValuePair<int, string> element in all_args)
            {
                Console.WriteLine($"AllArgs**: {element.Key}, {element.Value}");

            }
            Console.ReadLine();

            return 0;

        }
    }
}
