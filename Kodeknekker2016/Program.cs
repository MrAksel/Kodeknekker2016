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
        {   //  Array telling which segments are on for each digit
            //  Top - top left - top right - middle - bottom left - bottom right - bottom
            //  Same order as first digit in the task
            new [] {true, true, true, false, true, true, true, true}, // 0
            new [] {false, false, true, false, false, true, false },  // 1
            new [] {true, false, true, true, true, false, true },     // 2
            new [] {true, false, true, true, false, true, true },     // 3
            new [] {false, true, true, true, false, true, false },    // 4
            new [] {true, true, false, true, false ,true, true },     // 5
            new [] {true, true, false, true, true, true, true},       // 6
            new [] {true, false, true, false, false, true, false },   // 7
            new [] {true, true, true, true, true, true, true },       // 8
            new [] {true, true, true, true, false, true, true }       // 9
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
            for (int i = 0; i < 10000; i++) // Loop through all the numbers
            {
                int num7, num13;
                SplitNumber(i, out num7, out num13); // Split the number into 7s and 13s
                if (num7 == -1) // Only print the numbers we didn't find a solution for
                    Console.WriteLine("No solution for {0}", i);
                //else
                //    Console.WriteLine("{0}: {1}*7 + {2}*13", i, num7, num13);
            }
        }

        private static void SplitNumber(int i, out int num7, out int num13)
        {
            int max7 = i / 7 + 1; //    7 * max7  > i
            int max13 = i / 13 + 1; // 13 * max13 > i
            for (num7 = 0; num7 < max7; num7++) // Start with 0*7 and go upwards
            {
                int rest = i - num7 * 7; // Remove all the 7's
                int div13 = rest / 13;
                int rest13 = rest % 13;  // Check if it is divisable by 13

                if (rest13 == 0) // We found a pair that matched 
                {
                    num13 = div13;
                    return;
                }
            }
            num7 = -1;
            num13 = -1; // No matching numbers, return -1
        }

        static void Pi1000Count()
        {
            Dictionary<char, int> table = new Dictionary<char, int>();
            foreach (char c in pi1000) // Loop through every character
            {
                if (!table.ContainsKey(c)) // Add it to the dict if it's the first the time we saw it
                    table.Add(c, 1);
                else
                    table[c]++; // Increment the count for this character
            }

            PrintDict(table);
        }

        static void CountClockSegments()
        {
            Dictionary<int, int> segs = new Dictionary<int, int>(); // Dictionary indexed by segment and storing how many seconds it is on during one day
            for (int i = 1; i <= 28; i++)
                segs.Add(i, 0); // Add initial value of 0 to every segment

            int digit;
            bool[] on;
            for (int h = 0; h < 24; h++) // Hour goes from 0 to 23
            {
                for (int m = 0; m < 60; m++) // Minute from 0 to 59
                {
                    // Hours
                    digit = h / 10; // First digit (23 -> 2,    17 -> 1,     09 -> 0)
                    if (digit > 0) // Two digits in hour. Rules said that the first digit is never 0, so only count when its > 0
                    {
                        on = segments[digit]; // The segments that are on for this digit
                        if (on[0]) segs[1]++; // Increase the value for the corresponding segment
                        if (on[1]) segs[2]++;
                        if (on[2]) segs[3]++;
                        if (on[3]) segs[4]++;
                        if (on[4]) segs[5]++;
                        if (on[5]) segs[6]++;
                        if (on[6]) segs[7]++;
                    }

                    digit = h % 10; // Get the last digit in the hour (23 -> 3, 17 -> 7, 09 -> 9)
                    on = segments[digit];
                    if (on[0]) segs[8]++;
                    if (on[1]) segs[9]++;
                    if (on[2]) segs[10]++;
                    if (on[3]) segs[11]++;
                    if (on[4]) segs[12]++;
                    if (on[5]) segs[14]++; // Oppgaven er fåkt, bytta om på rekkefølgen -.-
                    if (on[6]) segs[13]++;

                    // Minutes

                    digit = m / 10; // Gets the first digit of the minute
                    on = segments[digit];
                    if (on[0]) segs[15]++;
                    if (on[1]) segs[16]++;
                    if (on[2]) segs[17]++;
                    if (on[3]) segs[18]++;
                    if (on[4]) segs[19]++;
                    if (on[5]) segs[20]++;
                    if (on[6]) segs[21]++;

                    digit = m % 10; // Last digit of the minute
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

            PrintDict(segs); // Print the dictionary to output
        }

        static void CarProbabilities()
        {
            // Dictionary indexed by the size of the group (1-5), and returns the number of times this size occurs
            Dictionary<int, int> groups = new Dictionary<int, int>();
            for (int i = 1; i <= 5; i++)
                groups.Add(i, 0); // Adding initial values of 0

            int[] initialOrder = new int[] { 1, 2, 3, 4, 5 }; // Initial array of speeds - 1 unit/time, 2 units/time, etc.. 
                                                              // Index 0 is in the front of the queue, index 4 is the last car
            foreach (IEnumerable<int> order in Permutate(initialOrder)) // Permutate every possible combination of the initial order
            {
                int[] speeds = order.ToArray();
                int numg = NumGroups(speeds);   // Check how many groups this particular order ended up in
                groups[numg]++;                 // Increase the counter for that group size
            }
            PrintDict(groups); // Print the results
        }

        static void CarStats(int ntimes, long nrounds)
        {
            // Do a simulation

            long groupSum = 0; // Total sum of all the group sizes
            long numberRounds = (long)ntimes * (long)nrounds; // Total number of rounds

            // Do it in parallel for faster results  (outer loop runs 'ntimes' times, inner loop runs 'nrounds' times
            Parallel.For(0, ntimes, (int n) =>
            {
                long result = TotalGroups(nrounds);    // Run a simulation with 'nrounds' rounds and count the total sum of group sizes over all the rounds
                Interlocked.Add(ref groupSum, result); // Add it to the overall total (but in a thread-safe manner)
            });

            Console.WriteLine("Average is: {0:0.0000}", (double)groupSum / (double)numberRounds); // Average group size per round
        }

        static long TotalGroups(long rounds)
        {
            Random r;
            lock (sr)   // Random isn't thread-safe, so we have to get our own instance for each thread
                r = new Random(sr.Next());

            long totGroups = 0; // Sum of group sizes

            int[] originalSpeed = new[] { 1, 2, 3, 4, 5 };  // Initial ordering
            int[][] permutations = Permutate(originalSpeed).Select(e => e.ToArray()).ToArray(); // Permutations of the ordering, 
                                                                                                // but just converted to arrays instead of enumerables

            for (long i = 0; i < rounds; i++)
            {
                int idex = r.Next(0, permutations.Length);   // Pick a random index from the array
                int groups = NumGroups(permutations[idex]);  // Count the number of groups of that particular permutation
                totGroups += groups;                         // Add it to the result
            }

            return totGroups; // Return the total number of groups encountered
        }

        // Car at lower indexes are before the later ones
        static int NumGroups(int[] speeds)
        {
            bool log = false;

            // Loop throug every car and slow down those behind
            for (int c = 0; c < speeds.Length; c++)
            {
                int speed = speeds[c]; // Speed of this car (c)
                for (int w = c + 1; w < speeds.Length; w++) // Loop through every car after c
                {
                    int otherSpeed = speeds[w];
                    if (speed < otherSpeed) // If the one in front drives slower we have to slow down this too
                    {
                        speeds[w] = speed;
                    }
                }
            }

            // All cars driving at correct speed now, count the number of groups
            int numg = 1; // We always have at least one group
            string fmt = ""; // String for logging output if enabled

            for (int car = 0; car < speeds.Length - 1; car++) // Loop though all the cars again
            {
                string carName = "Car" + (car + 1).ToString(); // Logging
                string othernm = "Car" + (car + 2).ToString();

                if (speeds[car] > speeds[car + 1]) // This car is faster than the one in front, thus creating a group
                {
                    numg++; // Increase the group count
                    fmt += carName + " <- "; // Logging
                }
                else // This is in the same group as the one behind
                {
                    fmt += carName + "----"; // Logging
                }
                if (car == speeds.Length - 2) // Logging
                    fmt += othernm;
            }

            if (log)
                Console.WriteLine("{0} groups: {1}", numg, fmt);

            return numg; // Return the number of groups for this particular order
        }

        static void PrintDict<T, V>(Dictionary<T, V> dict)
        {
            foreach (T c in dict.Keys)
            {
                Console.WriteLine("{0}: {1}", c, dict[c]);
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
