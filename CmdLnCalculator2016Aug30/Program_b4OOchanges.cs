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

            // Validation of arguments:
            int position = 1;
            foreach (string str in args)
            {
                try
                {
                    // numbers are in ODD positions in the args list
                    if ((position % 2) != 0)
                    {
                        //make sure numbers are valid
                        int num;
                        bool test = int.TryParse(str, out num);
                        if (test == false)
                        {
                            throw new FormatException ("Numbers must be integer values e.g. 4 7 100 \n");
                        }
                    }
                    // math operators are always positioned in EVEN locations in the args list
                    else
                    {   
                        //make sure operators are valid
                        if (!(str.Equals("*") || str.Equals("/") || str.Equals("+") || str.Equals("-")))
                        {
                            throw new InvalidOperationException("Valid operations are / * + -");
                        }

                    }
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

            // Lists needed to store the arguments
            List<string> all_args = new List<string>();
            SortedList<int, string> divmul_ops = new SortedList<int, string>();

            position = 1;
            foreach (string str in args)
            {
                //save all args to list array
                all_args.Add(str);

                // math operators are always positioned in EVEN positions in the arg list
                if ( (position % 2) == 0 )
                {
                    //save the operator and its position (if is a multiply or divide operator)
                    if ( str.Equals("*") || str.Equals("/") ) {
                        divmul_ops.Add(position - 1, str);
                    }   
                }
                position += 1; 
            }

            // perform math operations * / first
            foreach(KeyValuePair<int, string> element in divmul_ops)
            {
                int index = element.Key;
                string operation = element.Value;
                Console.WriteLine($"Index: {index}, Operation: {operation}");

                Console.WriteLine($"Size of AllArgs: {all_args.Count}");
                foreach (string val in all_args)
                {
                    Console.WriteLine($"AllArgs: {val}");
                    
                }
                Console.WriteLine("index: {0}", index);
                Console.WriteLine("numbers: {0}, {1}\n", all_args[index-1], all_args[index+1]);
            }

            //perform rest of the math operations and get total

            Console.ReadLine();

            return 0;

        }
    }
}
