using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2024.Day02.Day02;

namespace AdventOfCode2024.Day03
{
    internal class Day03
    {
        public void Solve()
        {

            var input = ParseInput("Day03/input.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.GetMultiplication());

            // Part 2
            Printify.PrintSolution(1, 2, input.GetMultiplicationWithDo());

        }

        public class Input
        {
            public string Line { get; set; }

            public int GetMultiplication()
            {
                var reg = new Regex(@"mul\((\d{1,3}),(\d{1,3})\)");
                var sum = 0;
                var matches = reg.Matches(Line);
                foreach (Match match in matches) { 
                    int num1 = int.Parse(match.Groups[1].Value);
                    int num2 = int.Parse(match.Groups[2].Value);
                    sum += num1 * num2;
                }
                return sum;
            }

            public int GetMultiplicationWithDo()
            {
                var reg = new Regex(@"do\(\).*?don't\(\)");
                var sum = 0;
                var matches = reg.Matches(Line);
                foreach (Match match in matches)
                {
                    var input = new Input() { Line = match.Value };
                    sum += input.GetMultiplication();
                }
                return sum;
            }
        }

        public Input ParseInput(string filepath)
        {
            var input = "";
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Code to read from the file
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    input = line;
                }
            }

            return new Input() {  Line = input };

        }
    }
}
