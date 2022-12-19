using System;


namespace day08 {
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
            int[,] trees = new int[input.Length, input[0].Length];
            int[,] visible = new int[input.Length, input[0].Length];

            // convert to 2d matrix
            for(int i = 0; i < input.Length; i++){
                for (int j = 0; j < input[i].Length; j++){
                    trees[i, j] = input[i][j] - '0';
                    if(i * j == 0 || i == input.Length - 1 || j == input[i].Length - 1) visible[i, j] = 1;
                    else visible[i, j] = 0;
                }
            }

            // horizontal search
            for (int i = 0; i < trees.GetLength(0); i++){
                int tallestTree = 0;

                // find tallest tree of the row
                for (int j = 0; j < trees.GetLength(1); j++){
                    if(trees[i, j] > trees[i, tallestTree]){
                        tallestTree = j;
                    }
                }
                visible[i, tallestTree] = 1;

                // traverse from left to right
                int tallestRelativeTree = 0;
                for (int j = 0; j < tallestTree; j++){
                    if(trees[i, j] > trees[i, tallestRelativeTree]){
                        tallestRelativeTree = j;
                        visible[i,j] = 1;
                    }
                }

                // traverse from right to left
                tallestRelativeTree = trees.GetLength(1) - 1;
                for (int j = trees.GetLength(1) - 1; j > tallestTree; --j){
                    if(trees[i, j] > trees[i, tallestRelativeTree]){
                        tallestRelativeTree = j;
                        visible[i,j] = 1;
                    }
                }
            }

            // vertical search
            for (int j = 0; j < trees.GetLength(1); j++){
                int tallestTree = 0;

                // find tallest tree of the column
                for (int i = 0; i < trees.GetLength(0); i++){
                    if(trees[i, j] > trees[tallestTree, j]){
                        tallestTree = i;
                    }
                }
                visible[tallestTree, j] = 1;

                // traverse from top to bottom
                int tallestRelativeTree = 0;
                for (int i = 0; i < tallestTree; i++){
                    if(trees[i, j] > trees[tallestRelativeTree, j]){
                        tallestRelativeTree = i;
                        visible[i,j] = 1;
                    }
                }

                // traverse from bottom to top
                tallestRelativeTree = trees.GetLength(0) - 1;
                for (int i = trees.GetLength(0) - 1; i > tallestTree; --i){
                    if(trees[i, j] > trees[tallestRelativeTree, j]){
                        tallestRelativeTree = i;
                        visible[i,j] = 1;
                    }
                }
            }

            // sum all visible trees
            for (int i = 0; i < visible.GetLength(0); i++){
                for (int j = 0; j < visible.GetLength(1); j++){
                    solution += visible[i,j];
                }
            }  

            return solution;
        }


        static int partTwo(string[] input){
            
            int solution = 0;
            int[,] trees = new int[input.Length, input[0].Length];

            // format input
            for(int i = 0; i < input.Length; i++){
                for (int j = 0; j < input[i].Length; j++){
                    trees[i, j] = input[i][j] - '0';
                }
            }

            // search inner trees score
            for (int i = 1; i < trees.GetLength(0) - 1; i++){
                for (int j = 1; j < trees.GetLength(1) - 1; j++){
                    
                    int score = 1;
                    int[] visibleTrees = new int[4];

                    // look up
                    for(int k = i - 1; k >= 0; --k){
                        visibleTrees[0]++;
                        if(trees[k,j] >= trees[i,j]) break;
                    }

                    // look down 
                    for(int k = i + 1; k < trees.GetLength(0); k++){
                        visibleTrees[1]++;
                        if(trees[k,j] >= trees[i,j]) break;
                    }

                    // look left 
                    for(int k = j - 1; k >= 0; --k){
                        visibleTrees[2]++;
                        if(trees[i,k] >= trees[i,j]) break;
                    }

                    // look right
                    for(int k = j + 1; k < trees.GetLength(1); k++){
                        visibleTrees[3]++;
                        if(trees[i,k] >= trees[i,j]) break;
                    }

                    // get scenic score
                    foreach(int dir in visibleTrees)
                        score *= dir;
                    if(solution < score) solution = score;
                }
            }

            return solution;
        }
    }
}