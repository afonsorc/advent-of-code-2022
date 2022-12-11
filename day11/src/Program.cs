using System;
using System.Collections.Generic;
using System.Linq;


namespace day11{
    class Monkey{

        public List<long> items;
        public char operation;
        public string operand;
        public int test;
        public int T;
        public int F;
        public long inspections;            

        public Monkey(){
            items = new List<long>();
        }
    }


    class Program{
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static long partOne(string[] input){
            return monkeyInTheMiddle(input, 20, false);
        }


        static long partTwo(string[] input){
            return monkeyInTheMiddle(input, 10000, true);
        }


        static long monkeyInTheMiddle(string[] input, int rounds, bool isVeryWorried){

            long monkeyBusiness = 0;
            int numberOfMonkeys = 0;

            // count number of monkeys
            foreach(string i in input){
                string[] line = i.Split(' ');
                if(line[0] == "Monkey") numberOfMonkeys++;
            }

            // init monkey array
            string[] charsToDelete = new string[]{":", ",", "  ", "    "};
            Monkey[] monkeys = new Monkey[numberOfMonkeys];
            for(int i = 0; i < numberOfMonkeys; i++){
                monkeys[i] = new Monkey();
            } 

            // process all inputs
            int lcm = 1;
            int curMonkey = -1;
            for(int i = 0; i < input.Length; i++){
                foreach(string c in charsToDelete){
                    input[i] = input[i].Replace(c, string.Empty);
                }
                string[] line = input[i].Split(' ');
                if(line[0] == "Monkey"){
                    curMonkey++;
                }else if(line[0] == "Starting"){
                    for(int j = 2; j < line.GetLength(0); j++){
                        monkeys[curMonkey].items.Add(Convert.ToInt64(line[j]));
                    }
                }else if(line[0] == "Operation"){
                    monkeys[curMonkey].operation = char.Parse(line[4]);
                    monkeys[curMonkey].operand = line[5];
                }else if(line[0] == "Test"){
                    monkeys[curMonkey].test = Convert.ToInt32(line[3]);
                    lcm *= monkeys[curMonkey].test;
                }else if(line[0] == "If"){
                    if(line[1] == "true") monkeys[curMonkey].T = Convert.ToInt32(line[5]);
                    else if(line[1] == "false") monkeys[curMonkey].F = Convert.ToInt32(line[5]);
                }
            }

            // play out rounds
            for(int r = 0; r < rounds; r++){
                foreach(Monkey m in monkeys){
                    foreach(long item in m.items){
                        
                        long curItem = item;
                        long operand = 0;
                        
                        if(m.operand == "old") operand = curItem;
                        else operand = Convert.ToInt64(m.operand);

                        if(m.operation == '*') curItem *= operand;
                        else if(m.operation == '+') curItem += operand;
                        
                        if(isVeryWorried) curItem %= lcm;
                        else curItem /= 3;

                        if(curItem % m.test == 0) monkeys[m.T].items.Add(curItem);
                        else monkeys[m.F].items.Add(curItem);
                    }

                    m.inspections += m.items.Count;
                    m.items.Clear();
                }
            }

            // calculate inspections
            long[] mostInspections = new long[2];
            foreach(Monkey m in monkeys){
                if(m.inspections > mostInspections.Min()) mostInspections[Array.IndexOf(mostInspections, mostInspections.Min())] = m.inspections;
            }

            monkeyBusiness = mostInspections[0] * mostInspections[1];
            return monkeyBusiness;
        }
    }
}