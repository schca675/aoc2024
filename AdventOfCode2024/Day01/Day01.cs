using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024.Day01
{
    internal class Day01

    {
        public void Solve() {

            var input = ParseInput("Day01/input1.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.GetDistancesOfElements());

            // Part 2
            Printify.PrintSolution(1, 2, input.MultiplyByAmountInSecondList());

        }

        public class TestInput
        {
            public List<int> ListA { get; set; }
            public List<int> ListB { get; set; }

            public int GetDistancesOfElements()
            {
                ListA.Sort();
                ListB.Sort();

                var dist = 0;

                for (var i = 0; i <ListA.Count; i++)
                {
                    dist += Math.Abs(ListA[i] - ListB[i]);
                }
                return dist;
            }

            public int MultiplyByAmountInSecondList()
            {
                var sum = 0;
               foreach (var a in ListA)
                {
                    var c = ListB.Where(b => b==a).Count();
                    sum += c * a;
                }
               return sum;
            }

        }

        public TestInput ParseInput(string filepath) {
            List<int> lista = [];
            List<int> listb = [];
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Code to read from the file
                var line = "";
                while ((line = reader.ReadLine()) != null) {
                   var parts = line.Split(" ");

                    lista.Add(int.Parse(parts[0]));
                    listb.Add(int.Parse(parts[3]));
                }
            }

            return new TestInput() { ListA = lista, ListB = listb }; 
        
        }
    }
}
