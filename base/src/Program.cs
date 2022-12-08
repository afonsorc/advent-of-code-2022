using System;


namespace base {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

            // write solution to part one
            int solutionOne = partOne(input); 
            System.Console.WriteLine("--------------\nPart1: {0}\n--------------", solutionOne);

            // write solution to part two
            int solutionTwo = partTwo(input);
            System.Console.WriteLine("--------------\nPart2: {0}\n--------------", solutionTwo);
            return;
        }


        static int partOne(string[] input){
            
            int solution = 0;

            

            return solution;
        }


        static int partTwo(string[] input){
            
            int solution = 0;



            return solution;
        }
    }
}