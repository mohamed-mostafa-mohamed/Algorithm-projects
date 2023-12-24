using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem
{
    // *****************************************
    // DON'T CHANGE CLASS OR FUNCTION NAME
    // YOU CAN ADD FUNCTIONS IF YOU NEED TO
    // *****************************************
    public static class SchoolQuizII
    {
        #region YOUR CODE IS HERE

        #region FUNCTION#1: Calculate the Value
        //Your Code is Here:
        //==================
        /// <summary>
        /// find the minimum number of integers whose sum equals to ‘N’
        /// </summary>
        /// <param name="N">number given by the teacher</param>
        /// <param name="numbers">list of possible numbers given by the teacher (starting by 1)</param>
        /// <returns>minimum number of integers whose sum equals to ‘N’</returns>
        static public int SolveValue(int N, int[] numbers)
        {
            //REMOVE THIS LINE BEFORE START CODING
            
           
            if (N == 1)
                return 1;
            int[] dpTable = new int[N + 1];
            for (int i = 0; i <= N; i++)
            {
                dpTable[i] = i;
            }

            //  dpTable[0] = 0;

            for (int j = 0; j < numbers.Length; j++)
            {
                int num = numbers[j];
                if (num == N)
                    return 1;
                if (num == 1||num>N)
                    // dpTable[j] = j;
                    continue;
                
              /*  if (j != numbers.Length - 1)
                {
                    for (int i = num; i <= N; i++)
                    {

                        dpTable[i] = i - num + 1;
                    }

                }
              */
                for (int i = num; i <= N; i++)
                {

                    if (dpTable[i] < dpTable[i - num] + 1)

                        dpTable[i] = dpTable[i];

                    else


                        dpTable[i] = dpTable[i - num] + 1;

                }
            }

            return dpTable[N];
        }
        #endregion

        #region FUNCTION#2: Construct the Solution
        //Your Code is Here:
        //==================
        /// <returns>the numbers themselves whose sum equals to ‘N’</returns>
        static public int[] ConstructSolution(int N, int[] numbers)
        {
            //REMOVE THIS LINE BEFORE START CODING
            
            int[] dpTable = new int[N + 1];

            for (int i = 0; i <= N; i++)
            {
                dpTable[i] = i;
            }

            for (int j = 0; j < numbers.Length; j++)
            {
                int num = numbers[j];

                if (num == 1||num>N)
                    continue;

               
                if (N == num||N==1)
                {
                    int[] Nresult = new int []{N};
                    return Nresult;

                }      

                for (int i = num; i <= N; i++)
                {
                    if (dpTable[i] > dpTable[i - num] + 1)
                    {
                        dpTable[i] = dpTable[i - num] + 1;
                    }
                }
            }

            int resultLength = dpTable[N];
            int[] result = new int[resultLength];
            int fianlN = N;

            for (int i = resultLength - 1; fianlN > 0 && i >= 0; i--)
            {
                for (int j = numbers.Length - 1; j >= 0; j--)
                {
                    int num = numbers[j];
                    if (fianlN >= num && dpTable[fianlN - num] == dpTable[fianlN] - 1)
                    {
                        result[i] = num;
                        fianlN -= num;
                        break;
                    }
                }
            }

            return result;

        }
        #endregion

        #endregion
    }
}
