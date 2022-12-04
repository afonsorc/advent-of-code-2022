using System;
using System.IO;


namespace day01{
    class Program{
        
        static void Main(string[] args){
            
            // read and process input onto array
            const string INPUT_FILE = "../input/input.txt";
            string[] input = System.IO.File.ReadAllLines(INPUT_FILE);

            int firstElf = 0;
            int secondElf = 0;
            int thirdElf = 0;
            int currentCalories = 0;
            int size = File.ReadAllLines(INPUT_FILE).Length;
            int lineNumber = 0;

            foreach(string line in input){

                if(!String.Equals(line, "")){
                    currentCalories += Convert.ToInt32(line);
                }

                if(String.Equals(line, "") || lineNumber == size){
                    if(firstElf < currentCalories){
                        thirdElf = secondElf;
                        secondElf = firstElf;
                        firstElf = currentCalories;
                    }else if(secondElf < currentCalories){
                        thirdElf = secondElf;
                        secondElf = currentCalories;
                    }else if(thirdElf < currentCalories){
                        thirdElf = currentCalories;
                    }
                    currentCalories = 0;
                }
            }

            System.Console.WriteLine(firstElf + secondElf + thirdElf);
        }
    }
}
