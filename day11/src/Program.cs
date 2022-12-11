using System;
using System.Collections.Generic;
using System.Linq;


namespace day11{
    class Program {
        static void Main(string[] args){

            // read and process input onto array
            string[] input = System.IO.File.ReadAllLines("../input/input.txt");

            // write solutions
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }

        public struct Monkey{

            public int monkey;
            public List<ulong> items;
            public char op;
            public string opVal;
            public int test;
            public int testT;
            public int testF;
            public ulong inspections;            

            public void newMonkey(int m){
                monkey = m;
                items = new List<ulong>();
            }

        }

        static ulong partOne(string[] input){
            
            int nMonkeys = 0;
            string[] line;

            foreach(string i in input){
                line = i.Split(' ',',');
                if(line[0] == "Monkey"){
                    nMonkeys++;
                }
                else if(line[0] == "Starting"){
                    for(int j = 2; j < line.GetLength(0); j++){
                        
                    }
                }
            }
            string[] charsToDelete = new string[]{":", ",", "  ", "    "};
            Monkey[] monkeys = new Monkey[nMonkeys];
            int curMonkey = -1;
            foreach(string i in input){
                string x = i;
                foreach(string c in charsToDelete){
                    x = x.Replace(c, string.Empty);
                }

                line = x.Split(' ');
                if(line[0] == "Monkey"){
                    curMonkey++;
                    monkeys[curMonkey].newMonkey(curMonkey);
                }
                else if(line[0] == "Starting"){
                    for(int j = 2; j < line.GetLength(0); j++){
                        monkeys[curMonkey].items.Add(Convert.ToUInt64(line[j]));
                    }
                }
                else if(line[0] == "Operation"){
                    monkeys[curMonkey].op = char.Parse(line[4]);
                    monkeys[curMonkey].opVal = line[5];
                }
                else if(line[0] == "Test"){
                    monkeys[curMonkey].test = Convert.ToInt32(line[3]);
                }
                else if(line[0] == "If"){
                    if(line[1] == "true") monkeys[curMonkey].testT = Convert.ToInt32(line[5]);
                    else if(line[1] == "false") monkeys[curMonkey].testF = Convert.ToInt32(line[5]);
                }
            }


            for(int r = 0; r < 20; r++){
                for(int m = 0; m < nMonkeys; m++){
                    foreach(ulong item in monkeys[m].items){
                        ulong cur = item;
                        
                        if(monkeys[m].op == '*'){
                            if(monkeys[m].opVal == "old"){
                                cur *= cur;
                            } else{
                                cur *= Convert.ToUInt64(monkeys[m].opVal);
                            }
                        }else if(monkeys[m].op == '+'){
                            if(monkeys[m].opVal == "old"){
                                cur += cur;
                            } else{
                                cur += Convert.ToUInt64(monkeys[m].opVal);
                            }
                        }

                        cur /= 3;

                        if(cur % Convert.ToUInt64(monkeys[m].test) == 0){
                            monkeys[monkeys[m].testT].items.Add(cur);
                        }else{
                            monkeys[monkeys[m].testF].items.Add(cur);
                        }
                        monkeys[m].inspections++;
                    }
                    monkeys[m].items.Clear();
                }
            }

            ulong[] mostInsp = new ulong[2];


            foreach(Monkey m in monkeys){

                //    System.Console.WriteLine("-------------");
                //foreach(int i in m.items) System.Console.Write("{0}-",i);
                //System.Console.WriteLine();
                //System.Console.WriteLine(m.op);
                //System.Console.WriteLine(m.opVal);
                //System.Console.WriteLine(m.test);
                //System.Console.WriteLine(m.testT);
                //System.Console.WriteLine(m.testF);
                //System.Console.WriteLine(m.inspections);
                //System.Console.WriteLine("-------------");

                if(m.inspections > mostInsp.Min()) mostInsp[Array.IndexOf(mostInsp, mostInsp.Min())] = m.inspections;
            }


            System.Console.WriteLine(mostInsp[0]);
                    System.Console.WriteLine(mostInsp[1]);
            return mostInsp[0]*mostInsp[1];
        }


        static ulong partTwo(string[] input){
            
            int nMonkeys = 0;
            string[] line;

            foreach(string i in input){
                line = i.Split(' ',',');
                if(line[0] == "Monkey"){
                    nMonkeys++;
                }
                else if(line[0] == "Starting"){
                    for(int j = 2; j < line.GetLength(0); j++){
                        
                    }
                }
            }
            string[] charsToDelete = new string[]{":", ",", "  ", "    "};
            Monkey[] monkeys = new Monkey[nMonkeys];
            int curMonkey = -1;
            foreach(string i in input){
                string x = i;
                foreach(string c in charsToDelete){
                    x = x.Replace(c, string.Empty);
                }

                line = x.Split(' ');
                if(line[0] == "Monkey"){
                    curMonkey++;
                    monkeys[curMonkey].newMonkey(curMonkey);
                }
                else if(line[0] == "Starting"){
                    for(int j = 2; j < line.GetLength(0); j++){
                        monkeys[curMonkey].items.Add(Convert.ToUInt64(line[j]));
                    }
                }
                else if(line[0] == "Operation"){
                    monkeys[curMonkey].op = char.Parse(line[4]);
                    monkeys[curMonkey].opVal = line[5];
                }
                else if(line[0] == "Test"){
                    monkeys[curMonkey].test = Convert.ToInt32(line[3]);
                }
                else if(line[0] == "If"){
                    if(line[1] == "true") monkeys[curMonkey].testT = Convert.ToInt32(line[5]);
                    else if(line[1] == "false") monkeys[curMonkey].testF = Convert.ToInt32(line[5]);
                }
            }

            ulong lcm = 1;

            foreach(Monkey m in monkeys){
                lcm *= (ulong) m.test;
            }
            System.Console.WriteLine(lcm);

            for(int r = 0; r < 10000; r++){
                for(int m = 0; m < nMonkeys; m++){
                    foreach(ulong item in monkeys[m].items){
                        ulong cur = item;
                        
                        if(monkeys[m].op == '*'){
                            if(monkeys[m].opVal == "old"){
                                cur *= cur;
                            } else{
                                cur *= Convert.ToUInt64(monkeys[m].opVal);
                            }
                        }else if(monkeys[m].op == '+'){
                            if(monkeys[m].opVal == "old"){
                                cur += cur;
                            } else{
                                cur += Convert.ToUInt64(monkeys[m].opVal);
                            }
                        }
                        //System.Console.WriteLine("{0}:{1}", cur, cur%lcm);
                        cur %= lcm;


                        if(cur % Convert.ToUInt64(monkeys[m].test) == 0){
                            monkeys[monkeys[m].testT].items.Add(cur);
                        }else{
                            monkeys[monkeys[m].testF].items.Add(cur);
                        }
                        monkeys[m].inspections++;
                    }
                    monkeys[m].items.Clear();
                }
            }


            ulong[] mostInsp = new ulong[2];

            
            foreach(Monkey m in monkeys){

                //    System.Console.WriteLine("-------------");
                foreach(int i in m.items) System.Console.Write("{0} | ",i);
                //System.Console.WriteLine();
                //System.Console.WriteLine(m.op);
                //System.Console.WriteLine(m.opVal);
                //System.Console.WriteLine(m.test);
                //System.Console.WriteLine(m.testT);
                //System.Console.WriteLine(m.testF);
                System.Console.WriteLine(m.inspections);
                //System.Console.WriteLine("-------------");
            }

            foreach(Monkey m in monkeys){

                if(m.inspections > mostInsp.Min()) mostInsp[Array.IndexOf(mostInsp, mostInsp.Min())] = m.inspections;
            }

            System.Console.WriteLine(mostInsp[0]);
                System.Console.WriteLine(mostInsp[1]);
            return mostInsp[0]*mostInsp[1];
        }
    }
}