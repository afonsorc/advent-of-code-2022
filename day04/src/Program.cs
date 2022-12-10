using System;


namespace day04 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static int partOne(string[] input){
            
            int contained = 0;

            foreach(string row in input){
                string[] pair = row.Split(',','-');
                int[] id = Array.ConvertAll(pair, s => int.Parse(s));

                if((id[2] <= id[0] && id[3] >= id[1]) || (id[0] <= id[2] && id[1] >= id[3])) contained++;
            }

            return contained;
        }


        static int partTwo(string[] input){
            
            int contained = 0;

            foreach(string row in input){
                string[] pair = row.Split(',','-');
                int[] id = Array.ConvertAll(pair, s => int.Parse(s));

                // cut cases where pairs don't overlap
                if(!(id[0] > id[3] || id[1] < id[2])) contained++;
            }

            return contained;
        }
    }
}