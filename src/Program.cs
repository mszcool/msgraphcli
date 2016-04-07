using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using ConsoleAppBase;

namespace MSGraphCLI
{
    class Program
    {
        static void Main(string[] args)
        {
            var cb = new ConsoleBase("MSGraphTests.exe");
            cb.Run(args);
            Console.WriteLine("Done!");
        }
    }
}
