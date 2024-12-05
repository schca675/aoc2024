using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2024.Day02.Day02;

namespace AdventOfCode2024.Day05
{
    internal class Day05
    {
        public void Solve()
        {

            var input = ParseInput("Day05/input.txt");
            //var input = ParseInput("Day05/testinput.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.Part1());

            // Part 2
            Printify.PrintSolution(1, 2, input.Part2());

        }

        public struct Page
        {
            public required int N {  get; set; }
        };


        public class PageComparer : IComparer<Page>
        {

            public Tuple<int, int>[] Rules { get; set; } // consisting 

            public int Compare(Page x, Page y)
            {
                var xLDy = new Tuple<int, int>(x.N, y.N);
                if (Rules.Contains(xLDy)) {
                    return -1;
                }
                var yLDx = new Tuple<int, int>(y.N, x.N);
                if (Rules.Contains(yLDx)) {
                    return 1;
                }
                // they are equivalent
                return 0;
            }
        }

        public class Input
        {

            public List<List<Page>> Updates { get; set; }

            public PageComparer Comparer { get; set; }
            public bool IsOrdered(List<Page> pages)
            {
               //pages.Sort(Comparer);
                for (int i = 0; i < pages.Count-1; i++) {
                    // 1 means that y < x 
                    if (Comparer.Compare(pages[i], pages[i + 1]) == 1)
                    {
                        return false;
                    };
                }
                return true;
            }

            public int SumMiddleValues(List<List<Page>> updates)
            {
                return updates.Select(u => u[u.Count / 2].N).Sum();
            }

            public int Part1()
            {
                var correctUpdates = Updates.Where(IsOrdered).ToList();
                return SumMiddleValues(correctUpdates);
            }

            public int Part2()
            {
                var incorrectUpdates = Updates.Where(u=>!IsOrdered(u)).ToList();
                incorrectUpdates.ForEach(a => a.Sort(Comparer));


                return SumMiddleValues(incorrectUpdates);
            }
        }

        public Input ParseInput(string filepath)
        {
            var input = "";
            var rules = new List<Tuple<int, int>>();
            var numbers = new List<List<Page>>();
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Code to read from the file
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Contains("|"))
                    {
                        var ns = line.Split('|');
                        var tuple = new Tuple<int,int>(int.Parse(ns[0]), int.Parse(ns[1]));
                        rules.Add(tuple);
                    } else if (line.Contains(","))
                    {
                        var ns = line.Split(",");
                        var update = ns.Select(e => new Page() { N = int.Parse(e) }).ToList();
                        numbers.Add(update);
                    }


                    input = line;
                }
            }

            return new Input() { Comparer = new PageComparer() {Rules = [.. rules] }, Updates = numbers };

        }
    }
}
