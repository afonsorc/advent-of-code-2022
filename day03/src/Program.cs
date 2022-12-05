using System;
using System.Collections.Generic;

namespace day03{
    class Program{
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            //partOne(input);
            
            partTwo(input);

        }


        static void partOne(string[] input){
            
            const int prioLower = 96;
            const int prioUpper = 38;

            int priority = 0;

            foreach(string sack in input){
                IDictionary<char, int> sackItems = new Dictionary<char, int>();
                for(int i = 0; i < sack.Length / 2; i++){
                    if(!sackItems.ContainsKey(sack[i])){
                        sackItems.Add(sack[i], 1);
                    } 
                }

                for(int i = sack.Length / 2; i < sack.Length; i++){
                    if(sackItems.ContainsKey(sack[i])){
                        if(sackItems[sack[i]] == 1){
                            if(sack[i] >= 65 && sack[i] <= 90){
                                priority += sack[i] - prioUpper;
                            } else if(sack[i] >= 97 && sack[i] <= 122){
                                priority += sack[i] - prioLower;
                            }
                        }
                        break;
                    }
                }
            }

            System.Console.WriteLine(priority);
            return;
        }


        static void partTwo(string[] input){
            
            const int prioLower = 96;
            const int prioUpper = 38;

            int priority = 0;
            int row = 0;

            IDictionary<char, int> sackItems = new Dictionary<char, int>();

            foreach(string sack in input){
               
                for(int i = 0; i < sack.Length; i++){

                    // Key is in row 0, add it 
                    if(!sackItems.ContainsKey(sack[i]) && row == 0){
                        sackItems.Add(sack[i], row + 1);
                    }
                    // key exists in row non-0
                    if(sackItems.ContainsKey(sack[i]) && row != 0){
                        // key existed in every row so far
                        if(sackItems[sack[i]] == row){
                            sackItems[sack[i]]++;
                            // key exists in all 3 rows
                            if(sackItems[sack[i]] == 3){
                                if(sack[i] >= 65 && sack[i] <= 90){
                                    priority += sack[i] - prioUpper;
                                } else if(sack[i] >= 97 && sack[i] <= 122){
                                    priority += sack[i] - prioLower;
                                }
                                break;
                            }
                        }
                    }
                }
                
                if(row == 2){
                    row = 0;
                    sackItems.Clear();
                }
                else row++;
            }
            System.Console.WriteLine(priority);
            return;
        }
    }
}