using System;
using System.Collections.Generic;


namespace day21 {
    class Program {
        static void Main(string[] args){
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static long partOne(string[] input){
            
            string charsToRemove = " ";
            Dictionary<string, string> monkeys = new Dictionary<string, string>();
            
            for(int i = 0; i < input.Length; i++){
                input[i] = input[i].Replace(charsToRemove, string.Empty);
                string[] monkey = input[i].Split(":");
                monkeys.Add(monkey[0], monkey[1]);
            }

            return monkeyJob(monkeys, "root");
        }


        static long monkeyJob(Dictionary<string, string> monkeys, string name){
            long number = 0;
            if(!long.TryParse(monkeys[name], out number)){
                long monkeyOne = monkeyJob(monkeys, monkeys[name].Substring(0, 4));
                long monkeyTwo = monkeyJob(monkeys, monkeys[name].Substring(5, 4));
                return operation(monkeyOne, monkeyTwo, monkeys[name][4]);
            }
            return number;
        }


        static long operation(long a, long b, char op){
            switch(op){
                case '+': return a + b;
                case '-': return a - b;
                case '*': return a * b;
                case '/': return a / b;
            }
            return -1;
        }

        static long operation2(long y, long b, bool isFirst, char op){
            if(isFirst){
                switch(op){
                    case '+': return y - b;
                    case '-': return y + b;
                    case '*': return y / b;
                    case '/': return y * b;
                }
            }else{
                switch(op){
                    case '+': return y - b;
                    case '-': return b - y;
                    case '*': return y / b;
                    case '/': return b / y;
                } 
            }
            return -1;
        }


        static long partTwo(string[] input){
            
            string charsToRemove = " ";
            Dictionary<string, string> monkeys = new Dictionary<string, string>();
            
            for(int i = 0; i < input.Length; i++){
                input[i] = input[i].Replace(charsToRemove, string.Empty);
                string[] monkey = input[i].Split(":");
                monkeys.Add(monkey[0], monkey[1]);
            }
            return monkeyRoot(monkeys, "root");;
        }


        static long monkeyJob2(Dictionary<string, string> monkeys, string name){
            long number = 0;
            if(name == "humn") return -1;
            if(!long.TryParse(monkeys[name], out number)){
                long monkeyOne = monkeyJob2(monkeys, monkeys[name].Substring(0, 4));
                if(monkeyOne == -1) return -1;
                long monkeyTwo = monkeyJob2(monkeys, monkeys[name].Substring(5, 4));
                if(monkeyTwo == -1) return -1;
                return operation(monkeyOne, monkeyTwo, monkeys[name][4]);
            }
            return number;
        }

        static long monkeyRoot(Dictionary<string, string> monkeys, string name){
            long number = 0;
            // if not number
            if(!long.TryParse(monkeys[name], out number)){

                // check left
                long monkeyOne = monkeyJob2(monkeys, monkeys[name].Substring(0, 4));

                // check right
                long monkeyTwo = monkeyJob2(monkeys, monkeys[name].Substring(5, 4));

                long value = 0;
                if(monkeyOne < 0){
                    value = operation2(0, monkeyTwo, true, '-');
                    return humanJob(monkeys, monkeys[name].Substring(0, 4), value, number);
                }else if(monkeyTwo < 0){
                    value = operation2(0, monkeyOne, false, '-');
                    return humanJob(monkeys, monkeys[name].Substring(5, 4), value, number);
                }
            }
            return -1;
        }

        static long humanJob(Dictionary<string, string> monkeys, string name, long match, long number){
            if(name == "humn") return match;
            if(!long.TryParse(monkeys[name], out number)){
                long monkeyOne = monkeyJob2(monkeys, monkeys[name].Substring(0, 4));
                long monkeyTwo = monkeyJob2(monkeys, monkeys[name].Substring(5, 4));
                long value = 0;
                if(monkeyOne < 0){
                    value = operation2(match, monkeyTwo, true, monkeys[name][4]);
                    return humanJob(monkeys, monkeys[name].Substring(0, 4), value, number);
                }else if(monkeyTwo < 0){
                    value = operation2(match, monkeyOne, false, monkeys[name][4]);
                    return humanJob(monkeys, monkeys[name].Substring(5, 4), value, number);
                }
            }
            return number;
        }
    }
}