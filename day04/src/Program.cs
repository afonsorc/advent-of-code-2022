using System;


namespace day04 {
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);
            
            //partOne(input);   
            partTwo(input);
        }


        static void partOne(string[] input){
            
            int contained = 0;
            foreach(string row in input){

                string[] pair = row.Split(',','-');
                int[] range = Array.ConvertAll(pair, s => int.Parse(s));

                if((range[2] <= range[0] && range[3] >= range[1]) || (range[0] <= range[2] && range[1] >= range[3])){
                    contained++;
                }
            }

            System.Console.WriteLine(contained);
            return;
        }


        static void partTwo(string[] input){
            
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

            System.Console.WriteLine(contained);
            return;
        }
    }
}