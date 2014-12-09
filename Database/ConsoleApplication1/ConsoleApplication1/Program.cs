using System;
using System.Linq;
using System.Collections.Generic;

namespace MyClass
{
    public class TestClass
    {
        public static void Main1()
        {
            // number of test cases
            int testCasesCount = Convert.ToInt32(Console.ReadLine());
            Dictionary<int, List<int>> dic = new Dictionary<int, List<int>>();

            while (testCasesCount != 0)
            {
                // Get the test case values
                // number of factories
                int factoriesCount = Convert.ToInt32(Console.ReadLine());

                string array = Console.ReadLine();

                string[] stringArray = array.Split(' ');
                List<int> list = new List<int>();
                foreach (string str in stringArray)
                {
                    list.Add(Convert.ToInt32(str));
                }

                //int[] pValue = list.ToArray();

                dic.Add(factoriesCount, list);

                //getMaximumProfitsFromFactory();
                --testCasesCount;
            }
            getMaximumProfitsFromFactory(dic);
            Console.ReadKey();
        }

        private static void getMaximumProfitsFromFactory(Dictionary<int, List<int>> myDictionary)
        {
            foreach (KeyValuePair<int, List<int>> pairs in myDictionary)
            {
                // number of factories
                int factoriesCount = pairs.Key;

                int[] pValue = pairs.Value.ToArray();

                int minValue = int.MinValue;
                // find length
                int plength = pValue.Length;
                int j = 1,
                    sum = 0;
                do
                {
                    for (int k = 0; k < plength; k++)
                    {
                        if (k + j == plength)
                        {
                            break;
                        }
                        sum = 0;
                        for (int l = k; l <= k + j; l++)   //r=k+j
                        {
                            sum += pValue[l];
                        }

                        if (sum > minValue)
                        {
                            minValue = sum;
                        }
                    }

                    ++j;
                } while (j <= plength);

                Console.WriteLine(minValue % Math.Pow(10, 7));
            }
        }
    }
}