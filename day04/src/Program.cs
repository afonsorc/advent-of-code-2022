using System;


namespace day04 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            int solutionOne = 0;
            int solutionTwo = 0;

            solutionOne = partOne(input); 
            System.Console.WriteLine("--------------\nPart1: {0}\n--------------", solutionOne);

            solutionTwo = partTwo(input);
            System.Console.WriteLine("--------------\nPart2: {0}\n--------------", solutionTwo);

            return;
        }


        static int partOne(string[] input){
            
            int contained = 0;
            foreach(string row in input){

                string[] pair = row.Split(',','-');
                int[] range = Array.ConvertAll(pair, s => int.Parse(s));

                if((range[2] <= range[0] && range[3] >= range[1]) || (range[0] <= range[2] && range[1] >= range[3])){
                    contained++;
                }
            }

            return contained;
        }


        static int partTwo(string[] input){
            
            int contained = 0;
            foreach(string row in input){

                string[] pair = row.Split(',','-');
                int[] range = Array.ConvertAll(pair, s => int.Parse(s));

                if((range[0] > range[3] || range[1] < range[2])){
                    continue;
                }else{
                    contained++;
                }
            }

            return contained;
        }
    }
}