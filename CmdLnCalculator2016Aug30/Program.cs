using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCalculatorCmdLn
{
    /// <summary>
    ///  Main program validates the correct number of arguments and saves them to a sorted list of strings.
    ///  Creates a Calculator object and calls the calculateResult function for calculating the result.
    /// </summary>
    class Program
    {
        static int Main(string[] args)
        {
            int num_args = args.Length;

            if ( num_args < 3 )
            {
                Console.WriteLine("Please enter the numbers and operands -*/+ for which you need the Total.\n");
                Console.WriteLine("Usage: calc <number operator number operator number...>\n");
                Console.WriteLine("Example: 3 + 4 * 6 / 2\n");
                Console.ReadLine();
                return 1;
            }

            int position = 1;
            try
            {
                foreach (string str in args)
                {
                    //Save all the arguments
                    Calculator.calcArgsStr.Add(position - 1, str);
                    position += 1;
                }

                Calculator myCalculator = new Calculator();
                // Determine the mathematical total
                int total = myCalculator.calculateResult();
                Console.WriteLine($"Grand Total:{total}");

            }
            catch (FormatException FEx)
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
            catch (OverflowException OEx)
            {
                Console.WriteLine(OEx.Message);
                Console.ReadLine();
                return 1;
            }
            catch (Exception Ex)
            {
                Console.WriteLine(Ex.Message);
                Console.ReadLine();
                return 1;
            }

            Console.ReadLine();
            return 0;

        }
    }
}
