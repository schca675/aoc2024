using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    public static class Printify
    {
        public static void PrintSolution(int day, int part, int solution)
        {
            if (part == 1) {
                Console.WriteLine("~~**~~~*~~*~~~*~*~~~~~*~~*~~~*");
            }
            if (part == 2)
            {
                Console.WriteLine("~**~");
            }
            Console.WriteLine($"#Solution for day {day:D2} part {part}: --->  {solution}");
        }
    }
}
