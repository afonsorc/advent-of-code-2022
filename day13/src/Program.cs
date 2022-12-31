using System;
using System.Linq;
using System.Collections.Generic;

namespace day13{
    class Program {
        static void Main(string[] args){
            string input = System.IO.File.ReadAllText("../input/input.txt");
            System.Console.WriteLine("----------------\nPart1: {0}\n----------------", partOne(input));
            System.Console.WriteLine("----------------\nPart2: {0}\n----------------", partTwo(input));
            return;
        }


        static int partOne(string input){
            
        string[] pairs = input.Split("\n\n");
        int ix = 1;
        int sum = 0;
        foreach (string pair in pairs){
            string[] packetStrings = pair.Split("\n");
            List<object> p0 = Parse(packetStrings[0].Trim());
            List<object> p1 = Parse(packetStrings[1].Trim());
            if (CompareLists(p0, p1) <= 0)
            {
                sum += ix;
            }
            ix++;
        }

            return sum;
        }

        public static int CompareElements(object first, object second){
            return (first, second) switch{
                (int f, int s) => Math.Sign(f - s),
                (int f, List<object> s) => CompareLists(new List<object>() {f}, s),
                (List<object> f, int s) => CompareLists(f, new List<object>() {s}),
                (List<object> f, List<object> s) => CompareLists(f, s),
                _ => throw new Exception($"Could not compare elements {first} vs. {second}."),
            };
        }

        public static int CompareLists(List<object> first, List<object> second){
            int maxIx = Math.Min(first.Count, second.Count);
            for (int ix = 0; ix < maxIx; ix++){
                object el0 = first[ix];
                object el1 = second[ix];
                int diff = CompareElements(el0, el1);
                if (diff < 0){
                    return -1;
                }
                else if (diff > 0){
                    return 1;
                }
            }
            return Math.Sign(first.Count - second.Count);
        }

        public static Queue<char> StringToQueue(string toParse){
            Queue<char> queue = new Queue<char>();
            foreach (char ch in toParse){
                queue.Enqueue(ch);
            }
            return queue;
        }

        public static List<object> Parse(string toParse){
            Queue<char> data = StringToQueue(toParse);
            List<object> list = ParseList(data);
            return list;
        }

        public static List<object> ParseList(Queue<char> data){
            List<object> elements = new List<object>();
            data.Dequeue();
            while (data.Peek() != ']'){
                if (data.Peek() == ','){
                    data.Dequeue();
                }
                object el = ParseElement(data);
                elements.Add(el);
            }
            data.Dequeue();
            return elements;
        }

        public static object ParseElement(Queue<char> data){
            char next = data.Peek();
            if (char.IsDigit(next)){
                return ParseInt(data);
            }
            else if (next == '['){
                return ParseList(data);
            }
            else throw new Exception($"Expected an int or list but found: {string.Join("", data)}");
        }

        public static int ParseInt(Queue<char> data)
        {
            string token = string.Empty;
            while (char.IsDigit(data.Peek())){
                token += data.Dequeue();
            }
            return int.Parse(token);
        }


        static int partTwo(string input){
            
            string pairs = string.Join('\n', input.Split("\n\n"));
            string[] packets = pairs.Split("\n");
            List<string> sortedPackets = new List<string>();

            sortedPackets.Add("[[2]]");
            sortedPackets.Add("[[6]]");

            for(int i = 0; i < packets.Length; i++){
                sortedPackets.Add(packets[i]);
            }

            for(int i = 1; i < packets.Length + 2; i++){
                int j = i;
                while(j > 0){
                    List<object> p0 = Parse(sortedPackets[j-1].Trim());
                    List<object> p1 = Parse(sortedPackets[j].Trim());
                    if(CompareLists(p0, p1) < 0) break;
                    string temp = sortedPackets[j-1];
                    sortedPackets[j-1] = sortedPackets[j];
                    sortedPackets[j] = temp;
                    j--;
                }
            }

            return (sortedPackets.IndexOf("[[2]]") + 1) * (sortedPackets.IndexOf("[[6]]") + 1);
        }
    }
}