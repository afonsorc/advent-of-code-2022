using System;
using System.Collections.Generic;


namespace day03{
    class Program{
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        const int prioLower = 96;
        const int prioUpper = 38;


        static int partOne(string[] input){
            
            int priority = 0;

            foreach(string sack in input){
                
                HashSet<char> items = new HashSet<char>();

                string first = sack.Substring(0, sack.Length / 2);
                string second = sack.Substring(sack.Length / 2);

                // add first half items to set
                foreach(char item in first){
                    items.Add(item);
                }

                // find second half item that already is in the set and sum its priority
                foreach(char item in second){
                    if(items.Contains(item)){
                        priority += item - (Convert.ToInt32(Char.IsUpper(item))) * prioUpper - (Convert.ToInt32(Char.IsLower(item))) * prioLower;
                        break;
                    }
                }
            }

            return priority;
        }


        static int partTwo(string[] input){

            int elf = 0;
            int priority = 0;

            HashSet<char> itemsOne = new HashSet<char>();
            HashSet<char> itemsTwo = new HashSet<char>();

            foreach(string sack in input){
                
                // if on the third elf, compare the item with both other sets and change the priority if the item is a badge
                foreach(char item in sack){
                    if(elf == 0) itemsOne.Add(item);
                    else if(elf == 1) itemsTwo.Add(item);
                    else if(elf == 2){
                        if(itemsOne.Contains(item) && itemsTwo.Contains(item)){
                            priority += item - (Convert.ToInt32(Char.IsUpper(item))) * prioUpper - (Convert.ToInt32(Char.IsLower(item))) * prioLower;
                            break;
                        }
                    }
                }
                
                // if on the third elf, clear sets and reset to the next elf group
                if(++elf == 3){
                    elf = 0;
                    itemsOne.Clear();
                    itemsTwo.Clear();
                }
            }

            return priority;
        }
    }
}