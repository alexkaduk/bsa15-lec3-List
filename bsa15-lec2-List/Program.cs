using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bsa15_lec3_List
{
    class Program
    {
        static void Main(string[] args)
        {
            BSA bsa = new BSA();

            bsa.UsersPassTest();
            Console.WriteLine();

            bsa.UsersPassTestWithGoodTime();
            Console.WriteLine();

            bsa.UsersPassTestWithBadTime();
            Console.WriteLine();

            bsa.UsersGroupByCity();
            Console.WriteLine();

            bsa.UsersPassTestGroupByCity();
            Console.WriteLine();

            bsa.UsersResults();
            Console.WriteLine();

            Console.ReadKey();
        }

    }
}
