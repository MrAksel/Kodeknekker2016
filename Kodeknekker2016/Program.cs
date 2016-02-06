using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Kodeknekker2016
{
    class Program
    {
        static Random sr = new Random();
        static string pi1000 = "3.1415926535897932384626433832795028841971693993751058209749445923078164062862089986280348253421170679821480865132823066470938446095505822317253594081284811174502841027019385211055596446229489549303819644288109756659334461284756482337867831652712019091456485669234603486104543266482133936072602491412737245870066063155881748815209209628292540917153643678925903600113305305488204665213841469519415116094330572703657595919530921861173819326117931051185480744623799627495673518857527248912279381830119491298336733624406566430860213949463952247371907021798609437027705392171762931767523846748184676694051320005681271452635608277857713427577896091736371787214684409012249534301465495853710507922796892589235420199561121290219608640344181598136297747713099605187072113499999983729780499510597317328160963185950244594553469083026425223082533446850352619311881710100031378387528865875332083814206171776691473035982534904287554687311595628638823537875937519577818577805321712268066130019278766111959092164201989";
        static bool[][] segments = new bool[][]
        {
            new [] {true, true, true, false, true, true, true, true}, // 0
            new [] {false, false, true, false, false, true, false },  // 1
            new [] {true, false, true, true, true, false, true },     // 2
            new [] {true, false, true, true, false, true, true },
            new [] {false, true, true, true, false, true, false },
            new [] {true, true, false, true, false ,true, true },
            new [] {true, true, false, true, true, true, true},
            new [] {true, false, true, false, false, true, false },
            new [] {true, true, true, true, true, true, true },
            new [] {true, true, true, true, false, true, true }
        };

        static void Main(string[] args)
        {
            Console.BufferHeight = 1024;

            Console.WriteLine("7 & 13:"); Console.ReadLine();
            SevenAndThirteen();
            Console.WriteLine("PI:"); Console.ReadLine();
            Pi1000Count();
            Console.WriteLine("\nSegments:"); Console.ReadLine();
            CountClockSegments();
            Console.WriteLine("\nCars:"); Console.ReadLine();
            CarProbabilities();

            Console.WriteLine("\nCars stats:"); Console.ReadLine();
            int rounds = 100;
            while (true)
            {
                Console.ReadLine();
                CarStats(16, rounds);
                rounds *= 10;
            }

            Console.WriteLine("\nFinished"); Console.ReadLine();
        }

        private static void SevenAndThirteen()
        {
            for (int i = 0; i < 10000; i++)
            {
                int num7, num13;
                SplitNumber(i, out num7, out num13);
                if (num7 == -1)
                    Console.WriteLine("{0}: {1}*7 + {2}*13", i, num7, num13);
            }
        }

        private static void SplitNumber(int i, out int num7, out int num13)
        {
            int max7 = i / 7 + 1;
            int max13 = i / 13 + 1;
            for (num7 = 0; num7 < max7; num7++)
            {
                int rest = i - num7 * 7;
                int div13 = rest / 13;
                int rest13 = rest % 13;

                if (rest13 == 0)
                {
                    num13 = div13;
                    return;
                }
            }
            num7 = -1;
            num13 = -1;
        }

        static void Pi1000Count()
        {
            Dictionary<char, int> table = new Dictionary<char, int>();
            foreach (char c in pi1000)
            {
                if (!table.ContainsKey(c))
                    table.Add(c, 1);
                else
                    table[c]++;
            }

            PrintDict(table);
        }

        static void CountClockSegments()
        {
            Dictionary<int, int> segs = new Dictionary<int, int>();
            for (int i = 1; i <= 28; i++)
                segs.Add(i, 0);

            int digit;
            bool[] on;
            for (int h = 0; h < 24; h++)
            {
                for (int m = 0; m < 60; m++)
                {
                    // Hours
                    if (h / 10 > 0) // Two digits
                    {
                        digit = h / 10;
                        on = segments[digit];
                        if (on[0]) segs[1]++;
                        if (on[1]) segs[2]++;
                        if (on[2]) segs[3]++;
                        if (on[3]) segs[4]++;
                        if (on[4]) segs[5]++;
                        if (on[5]) segs[6]++;
                        if (on[6]) segs[7]++;
                    }

                    digit = h % 10;
                    on = segments[digit];
                    if (on[0]) segs[8]++;
                    if (on[1]) segs[9]++;
                    if (on[2]) segs[10]++;
                    if (on[3]) segs[11]++;
                    if (on[4]) segs[12]++;
                    if (on[5]) segs[14]++; // Oppgaven er fåkt
                    if (on[6]) segs[13]++;

                    // Minutes

                    digit = m / 10;
                    on = segments[digit];
                    if (on[0]) segs[15]++;
                    if (on[1]) segs[16]++;
                    if (on[2]) segs[17]++;
                    if (on[3]) segs[18]++;
                    if (on[4]) segs[19]++;
                    if (on[5]) segs[20]++;
                    if (on[6]) segs[21]++;

                    digit = m % 10;
                    on = segments[digit];
                    if (on[0]) segs[22]++;
                    if (on[1]) segs[23]++;
                    if (on[2]) segs[24]++;
                    if (on[3]) segs[25]++;
                    if (on[4]) segs[26]++;
                    if (on[5]) segs[27]++;
                    if (on[6]) segs[28]++;
                }
            }

            PrintDict(segs);
        }

        static void CarProbabilities()
        {
            // Key is group, value is number of times there was key groups
            Dictionary<int, int> groups = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
                groups.Add(i, 0);

            int[] initialOrder = new int[] { 1, 2, 3, 4, 5 };
            foreach (IEnumerable<int> order in Permutate(initialOrder))
            {
                int[] speeds = order.ToArray();
                int numg = NumGroups(speeds);
                groups[numg]++;
            }
            PrintDict(groups);
        }

        static void CarStats(int ntimes, long rounds)
        {
            long groupSum = 0;
            long numberRounds = (long)ntimes * (long)rounds;

            Parallel.For(0, ntimes, (int n) =>
            {
                long result = TotalGroups(rounds);
                Interlocked.Add(ref groupSum, result);
            });

            Console.WriteLine("Average is: {0:0.0000}", (double)groupSum / (double)numberRounds);
        }

        static long TotalGroups(long rounds)
        {
            Random r;
            lock (sr)
             r = new Random(sr.Next());

            long totGroups = 0;

            int[] originalSpeed = new[] { 1, 2, 3, 4, 5 };
            int[][] permutations = Permutate(originalSpeed).Select(e => e.ToArray()).ToArray();

            for (long i = 0; i < rounds; i++)
            {
                int idex = r.Next(0, permutations.Length);
                int groups = NumGroups(permutations[idex]);
                totGroups += groups;
            }

            return totGroups;
        }

        // Car at lower indexes are before the later ones
        static int NumGroups(int[] speeds)
        {
            bool log = false;
            // Loop throug every car and slow down those behind

            for (int c = 0; c < speeds.Length; c++)
            {
                int speed = speeds[c]; // Speed of this car
                for (int w = c + 1; w < speeds.Length; w++) // Loop through every car after c
                {
                    int otherSpeed = speeds[w];
                    if (speed < otherSpeed) // If the one in front drives slower we have to slow down this too
                    {
                        speeds[w] = speed;
                    }
                }
            }

            string fmt = "";
            int numg = 1;
            for (int car = 0; car < speeds.Length - 1; car++)
            {
                string carName = "Car" + (car + 1).ToString();
                string othernm = "Car" + (car + 2).ToString();

                if (speeds[car] > speeds[car + 1]) // This car is faster than the one in front, thus creating a group
                {
                    numg++;
                    fmt += carName + " <- ";
                }
                else // This is in the same group as the one behind
                {
                    fmt += carName + "----";
                }
                if (car == speeds.Length - 2)
                    fmt += othernm;
            }

            if (log)
                Console.WriteLine("{0} groups: {1}", numg, fmt);

            return numg;
        }

        static void PrintDict<T, V>(Dictionary<T, V> dict)
        {
            foreach (T c in dict.Keys)
            {
                Console.WriteLine("{0}: {1}", c, dict[c]);
            }
        }

        static void MergeInto<T>(Dictionary<T, int> into, Dictionary<T, int> from)
        {
            foreach (KeyValuePair<T, int> pair in from)
            {
                if (into.ContainsKey(pair.Key))
                {
                    into[pair.Key] += pair.Value;
                }
                else
                {
                    into.Add(pair.Key, pair.Value);
                }
            }
        }

        static IEnumerable<IEnumerable<T>> Permutate<T>(IEnumerable<T> values)
        {
            if (values.Count() == 1)
            {
                yield return values;
                yield break;
            }

            foreach (T value in values)
            {
                IEnumerable<T> element = new[] { value };
                IEnumerable<T> rest = values.Except(element);

                IEnumerable<IEnumerable<T>> permutations = Permutate(rest);
                foreach (IEnumerable<T> permuation in permutations)
                {
                    yield return element.Concat(permuation);
                }
            }
        }
    }
}
