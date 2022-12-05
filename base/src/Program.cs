using System;


namespace base {
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
            
            int out = 0;

            

            return out;
        }


        static int partTwo(string[] input){
            
            int out = 0;



            return out;
        }
    }
}