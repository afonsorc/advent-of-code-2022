using System;
using System.IO;



namespace day01{
    class Program{
        
        static void Main(string[] args){
            
            const string INPUT_FILE = "input.txt";

            int firstElf = 0;
            int secondElf = 0;
            int thirdElf = 0;
            int currentCalories = 0;
            int size = File.ReadAllLines(INPUT_FILE).Length;
            int lineNumber = 0;

            foreach(string line in System.IO.File.ReadLines(INPUT_FILE)){

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
