using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static AdventOfCode2024.Day02.Day02;

namespace AdventOfCode2024.Day04
{
    internal class Day04
    {
        public void Solve()
        {

            var input = ParseInput("Day04/input.txt");
            //var input = ParseInput("Day04/testinput.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.Part1());

            // Part 2
            Printify.PrintSolution(1, 2, input.Part2());

        }

        public class Input
        {
            // [line] Grid[1][2] -> X 
            // [lXne]
            public char[][] Grid {  get; set; }
            public string[] Horizontals { get; set; }
           
            private static readonly Regex xmas =
    new(pattern: "xmas",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

            private static readonly Regex samx =
    new(pattern: "samx",
        options: RegexOptions.Compiled | RegexOptions.IgnoreCase);

            public int GetXmasFromString(IEnumerable<string> input)
            {
                return input.Select(s => xmas.Count(s) + samx.Count(s)).Sum();
            }

            public int  CountHorizontalXmas()
            {
                return GetXmasFromString(Horizontals);
            }

            public int CountVerticalsXmas()
            {
                var verticals =new List<string>();
                for (int i=0; i<Grid.Length; i++)
                {
                    verticals.Add(new string(Grid.Select(carray => carray[i]).ToArray()));
                }
                return GetXmasFromString(verticals);
            }

            public int CountDiagonals()
            {
                var diagonals = new List<string>();
                var maxLowerJLeft = 0;
                var minLowerJRight = Grid[0].Length;
                // from every char in top ----> / (left) \ down (right)
                for (int upperj = 0; upperj < Grid[0].Length; upperj++)
                {
                    var jToLeft = upperj;
                    
                    var i = 0;
                    var s = "";
                    //   / 
                    while (jToLeft >= 0 && i<Grid.Length)
                    {
                        s += Grid[i][jToLeft];
                        i++;
                        jToLeft--;
                    }
                    maxLowerJLeft =  jToLeft+1 ;
                    diagonals.Add(s);
                    // \
                    var jToRight = upperj;
                    i = 0;
                    s = "";
                    while (jToRight < Grid[0].Length && i < Grid.Length)
                    {
                        s += Grid[i][jToRight];
                        i++;
                        jToRight++;
                    }
                    minLowerJRight = jToRight <= minLowerJRight? jToRight-1:minLowerJRight;
                    diagonals.Add(s);
                }
                // Fill up with missing diagonals that do not end in top line
                // /
                for(int j = maxLowerJLeft+1; j < Grid[0].Length; j++ )
                {
                    var s = "";
                    var jToLeft = j;
                    var i = Grid.Length -1;
                    //   / 
                    while (jToLeft < Grid[0].Length && i >= 0)
                    {
                        s += Grid[i][jToLeft];
                        i--;
                        jToLeft++;
                    }
                    diagonals.Add(s);
                }
                // \
                for (int j = minLowerJRight-1; j >= 0; j--)
                {
                    var s = "";
                    var jToRight = j;
                    var i = Grid.Length -1;
                    //   / 
                    while (jToRight >= 0 && i >= 0)
                    {
                        s += Grid[i][jToRight];
                        i--;
                        jToRight--;
                    }
                    diagonals.Add(s);
                }

                // from all other chars in bottom ----> / up
                return GetXmasFromString(diagonals);
            }

            public int Part1()
            {
                // find xmasses
                var h = CountHorizontalXmas();
                var v = CountVerticalsXmas();
                var d = CountDiagonals();
                return h+v+ d;
            }

            public int Part2()
            {
                var xmas = 0;
                for (int i = 1; i < Grid.Length-1; i++) { 
                    for (int j=1;  j < Grid[i].Length-1; j++)
                    {
                        // See if there is an A
                        if (Grid[i][j]=='A')
                        {
                            // check surroundings
                            char[] cs = [Grid[i - 1][j-1],  Grid[i-1][j+1], Grid[i+1][j-1], Grid[i+1][j+1]];
                            var mcount = cs.Count(c => c == 'M');
                            var scount = cs.Count(c => c == 'S');
                            if (mcount ==2 && scount ==2)
                            {
                                // ensure that m & s are not on one diagonal
                                if (Grid[i-1][j-1]!= Grid[i + 1][j+1])
                                {
                                    xmas++;
                                }
                            }
                        }
                    }
                }
                return xmas;
            }
        }

        public Input ParseInput(string filepath)
        {
            var input = new List<char[]>();
            var hs  = new List<string>();
            using (StreamReader reader = new StreamReader(filepath))
            {
                // Code to read from the file
                var line = "";
                while ((line = reader.ReadLine()) != null)
                {
                    input.Add([.. line]);
                    hs.Add(line);
                }
            }

            return new Input() { Grid = [.. input], Horizontals = [.. hs] };

        }
    }
}
