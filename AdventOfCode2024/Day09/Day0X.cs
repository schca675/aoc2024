namespace AdventOfCode2024.Day09
{
    internal class Day0X
    {
        public void Solve()
        {

            var input = ParseInput("Day0/input.txt");
            //var input = ParseInput("Day0/testinput.txt");

            // Part 1
            Printify.PrintSolution(1, 1, input.Part1());

            // Part 2
            Printify.PrintSolution(1, 2, input.Part2());

        }

        public class Input
        {
            public int Part1()
            {
                return 1;
            }

            public int Part2()
            {
                return 2;
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

            return new Input() {  };

        }
    }
}
