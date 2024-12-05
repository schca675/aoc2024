using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2024.Day02.Day02;

namespace AdventOfCode2024.Day02
{
    internal class Day02
    {
        public void Solve()
        {

            var input = ParseInput("Day02/input.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.GetCountValidRapports());

            // Part 2
            Printify.PrintSolution(1, 2, input.GetCountValidWithExc());
            Printify.PrintSolution(1, 2, input.GetCountValidWithExcSlow());

        }

        public class Rapport
        {
            public List<int> Values { get; set; }
            public bool isValid()
            {
                var diff = Values[1] - Values[0];
                if (Math.Abs(diff) > 3 || diff == 0)
                {
                    return false;
                }
                for (int i = 2; i < Values.Count; i++)
                {
                    var d2 = Values[i] - Values[i - 1];
                    if ((diff * d2 <= 0) || Math.Abs(d2) > 3)
                    {
                        return false;
                    }
                }
                return true;
            }

            public bool IsValidWithExc(int inverse = 1)
            {
                var diff = (Values[1] - Values[0]) * inverse;
                for (int i = 1; i < Values.Count; i++)
                {
                    var d2 = Values[i] - Values[i - 1];
                    if ((diff * d2 <= 0) || Math.Abs(d2) > 3)
                    {
                        var isValidWIthException = NewRapportWithoutElementAtPosition(i).isValid() || NewRapportWithoutElementAtPosition(i-1).isValid();

                        if (!isValidWIthException)
                        {
                            if (inverse == 1)
                            {
                                return IsValidWithExc(-inverse);
                            }
                            return false;
                        }
                        return true;
                    }
                }
                return true;
            }
            public bool IsValidWithExcSlow(int inverse = 1)
            {
                var diff = (Values[1] - Values[0]) * inverse;
                for (int i = 1; i < Values.Count; i++)
                {
                    var d2 = Values[i] - Values[i - 1];
                    if ((diff * d2 <= 0) || Math.Abs(d2) > 3)
                    {
                        // check if any are valid if we remove one
                        for (int j = 0; j < Values.Count; j++)
                        {
                            if (NewRapportWithoutElementAtPosition(j).isValid())
                            {
                                return true;
                            }
                            return false;
                        }
                    }
                }
                return true;
            }


            public Rapport NewRapportWithoutElementAtPosition(int index)
            {
                return new() { Values = Values.Take(index).Concat(Values.Skip(index + 1)).ToList() };
            }
        }

        public class TestInput
        {

            public List<Rapport> Rapports { get; set; }

            public int GetCountValidRapports()
                {
                    return Rapports.Count(r => r.isValid());
                }

            public int GetCountValidWithExc()
            {
                return Rapports.Count(r => r.IsValidWithExc());
            }

            public int GetCountValidWithExcSlow()
            {
                return Rapports.Count(r => r.IsValidWithExc());
            }

        }

        public TestInput ParseInput(string filepath)
        {
            List<Rapport> rapports = new List<Rapport>();
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Code to read from the file
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    var parts = line.Split(" ");

                    rapports.Add(new Rapport() { Values = parts.Select(p=> int.Parse(p)).ToList() });
                }
            }

            return new TestInput() { Rapports = rapports };

        }
    }
}
