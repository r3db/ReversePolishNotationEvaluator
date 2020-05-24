using System;
using System.IO;
using System.Linq;
using System.Text;

namespace Calculator
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var ms = new MemoryStream();
            var eval = new Evaluator(new Lexer(ms));

            if (args.Length == 0)
            {
                while (true)
                {
                    var input = Console.ReadLine().Trim();

                    if (input == "exit")
                    {
                        break;
                    }
                    else if (input == "help")
                    {
                        Console.WriteLine("USAGE:");
                        Console.WriteLine("\trpn                         Launch in interactive mode");
                        Console.WriteLine("\trpn[expression]             Evaluate a one - line expression");

                        continue;
                    }

                    ms.Position = 0;
                    ms.Write(Encoding.ASCII.GetBytes(input));
                    ms.Position = 0;

                    Console.WriteLine(eval.Evaluate());
                }
            }
            else
            {
                ms.Write(Encoding.ASCII.GetBytes(string.Join(" ", args)));
                ms.Position = 0;

                Console.WriteLine(eval.Evaluate());
            }
        }
    }
}