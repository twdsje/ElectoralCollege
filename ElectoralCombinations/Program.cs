using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectoralCombinations
{
    class Program
    {
        static void Main(string[] args)

        {
            int clinton = 256;
            //int clinton = 256 - 29;
            int trump = 170 - 6;
            int tossups = 538 - clinton - trump - 6;

            List<KeyValuePair<string,int>> votes = new List< KeyValuePair < string,int>>
            {
                new KeyValuePair<string, int>("Florida", 29),
                new KeyValuePair<string, int>("Ohio", 18),
                new KeyValuePair<string, int>("North Carolina", 15),
                new KeyValuePair<string, int>("New Hampshire", 4),
                new KeyValuePair<string, int>("Nevada", 6),
                new KeyValuePair<string, int>("Minnesota", 10),
                new KeyValuePair<string, int>("Indiana", 11),
                new KeyValuePair<string, int>("Iowa", 6),
                new KeyValuePair<string, int>("Maine", 2),
                new KeyValuePair<string, int>("Arizona", 11),

                //new KeyValuePair<string, int>("Georgia", 16),                               
                //new KeyValuePair<string, int>("Colorado", 9),                                               
                //new KeyValuePair<string, int>("Pennsylvania", 20),
            };

            var combinations = GetCombinations(votes);

            List<KeyValuePair<string, int>> empty = new List<KeyValuePair<string, int>>();

            foreach (var v in combinations)
            {
                v.Sort((x, y) => y.Value.CompareTo(x.Value));

                if (v.Count == 0)
                    empty = v;
            }

            combinations.Remove(empty);

            combinations.Sort((x, y) => y[0].Value.CompareTo(x[0].Value));

            foreach (var v in combinations)
            {
                string combo = "";
                int sum = 0;

                foreach (var f in v)
                {
                    combo = combo + f.Key + ", ";
                    sum = sum + f.Value;
                }

                if (sum + clinton < 270 && (tossups - sum) + trump < 270)
                {
                    Console.WriteLine(sum.ToString() + " " + combo);
                }

            }

            Console.WriteLine("Operation Complete.");
            Console.ReadLine();
        }

        static List<List<KeyValuePair<string, int>>> GetCombinations(List<KeyValuePair<string, int>> votes)
        {
            List<List<KeyValuePair<string, int>>> returnValues = new List<List<KeyValuePair<string, int>>>();

            var first = votes.First();

            //base case
            if (votes.Count == 1)
            {
                //Combination with this value.
                returnValues.Add(new List<KeyValuePair<string, int>>(votes));
                //Combinations without this value.
                returnValues.Add(new List<KeyValuePair<string, int>>());

                return returnValues;
            }

            //Get all combinations with that number.
            var myList = GetCombinations(votes.GetRange(1, votes.Count - 1));

            foreach (var v in myList)
            {

                //Combination without this value.
                var newList1 = new List<KeyValuePair<string, int>>();
                newList1.AddRange(v);
                returnValues.Add(newList1);

                //Combination with this value.
                var newList2 = new List<KeyValuePair<string, int>>();
                newList2.AddRange(v);
                newList2.Add(first);
                returnValues.Add(newList2);
            }

            //Get all combinations without that number.
            return returnValues;
        }
    }
}
