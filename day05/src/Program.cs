using System;
using System.Collections.Generic;


namespace day05 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            string solutionOne = String.Empty;
            string solutionTwo = String.Empty;

            solutionOne = partOne(input); 
            System.Console.WriteLine("--------------\nPart1: {0}\n--------------", solutionOne);

            solutionTwo = partTwo(input);
            System.Console.WriteLine("--------------\nPart2: {0}\n--------------", solutionTwo);

            return;
        }


        static string partOne(string[] input){
            
            string topCrates = String.Empty;
            int numberOfStacks = 0;
            int stacksRow = 0;

            // get number of stacks
            for(int i = 0; i < input.Length; i++){
                if(String.Equals(input[i], "")){
                    numberOfStacks = Convert.ToInt32(input[i - 1].Length / 4 + 1);
                    stacksRow = i - 1;
                }
            }

            // create stacks
            Stack<char>[] stacks = new Stack<char>[numberOfStacks];
            for(int i = 0; i < numberOfStacks; i++){
                stacks[i] = new Stack<char>();
            }

            // push all initial crates to the stacks
            for(int i = stacksRow; i >= 0; --i){
                for(int j = 0; j < input[i].Length; j++){
                    if((input[i][j] >= 65 && input[i][j] <= 90) || (input[i][j] >= 97 && input[i][j] <= 122)){
                        stacks[j / 4].Push(input[i][j]);
                    }
                }
            }

            // follow crate move instructions
            for(int i = stacksRow + 2; i < input.Length; i++){
                string[] instruction = input[i].Split(' ');
                int numberOfCrates = Convert.ToInt32(instruction[1]);
                int dstStack = Convert.ToInt32(instruction[5]);
                int srcStack = Convert.ToInt32(instruction[3]);

                for (int c = 0; c < numberOfCrates; c++){
                    stacks[dstStack - 1].Push(stacks[srcStack - 1].Pop());
                }
            }

            // get top crates
            foreach(Stack<char> s in stacks){
                topCrates += s.Pop();
            }

            return topCrates;
        }


        static string partTwo(string[] input){
            
            string topCrates = String.Empty;
            int numberOfStacks = 0;
            int stacksRow = 0;

            // get number of stacks
            for(int i = 0; i < input.Length; i++){
                if(String.Equals(input[i], "")){
                    numberOfStacks = Convert.ToInt32(input[i - 1].Length / 4 + 1);
                    stacksRow = i - 1;
                }
            }

            // create stacks
            Stack<char>[] stacks = new Stack<char>[numberOfStacks];
            for(int i = 0; i < numberOfStacks; i++){
                stacks[i] = new Stack<char>();
            }

            // push all initial crates to the stacks
            for(int i = stacksRow; i >= 0; --i){
                for(int j = 0; j < input[i].Length; j++){
                    if((input[i][j] >= 65 && input[i][j] <= 90) || (input[i][j] >= 97 && input[i][j] <= 122)){
                        stacks[j / 4].Push(input[i][j]);
                    }
                }
            }

            // follow crate move instructions
            for(int i = stacksRow + 2; i < input.Length; i++){
                string[] instruction = input[i].Split(' ');
                int numberOfCrates = Convert.ToInt32(instruction[1]);
                int dstStack = Convert.ToInt32(instruction[5]);
                int srcStack = Convert.ToInt32(instruction[3]);


                // push crates to cratemover
                Stack<char> crateMover = new Stack<char>();
                for (int c = 0; c < numberOfCrates; c++){
                    crateMover.Push(stacks[srcStack - 1].Pop());
                }

                // pop crates back on destination
                for (int c = 0; c < numberOfCrates; c++){
                    stacks[dstStack - 1].Push(crateMover.Pop());
                }
            }

            // get top crates
            foreach(Stack<char> s in stacks){
                topCrates += s.Pop();
            }

            return topCrates;
        }
    }
}