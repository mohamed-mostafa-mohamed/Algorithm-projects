﻿using System;
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
    public static class FindExtraElement
    {
        #region YOUR CODE IS HERE
        /// <summary>
        /// Find index of extra element in first array
        /// </summary>
        /// <param name="arr1">first sorted array with an extra element</param>
        /// <param name="arr2">second sorted array</param>
        /// <returns>index of the extra element in arr1</returns>
        public static int FindIndexOfExtraElement(int[] arr1, int[] arr2)
        {
            //REMOVE THIS LINE BEFORE START CODING
            if (arr1.Length <= 10)
            {
                int Index1 = arr1.GetLength(0) - 1;
                int Index2 = arr2.GetLength(0) - 1;


                if (Index1 == Index2 || Index1 == 0 || Index2 == 0 || arr1[0] != arr2[0])
                {
                    return 0;
                }
                if (arr1[Index1] != arr2[Index2])
                {
                    return Index1;
                }


                for (int i = 0; i < arr1.Length; i++)
                {


                    if (arr1[i] + arr1[i + 1] != arr2[i] + arr2[i + 1])
                    {
                        return i + 1;
                    }

                }

                return 0;
            }
            else
            {
                return BinarySearch(arr1, arr2, 0, arr1.Length - 1);
            }
        }

        private static int BinarySearch(int[] arr1, int[] arr2, int first, int last)
        {
            if (first > last)
            {
                return -1;
            }

            int middle = (first + last) / 2;

            if (middle == last || arr1[middle] != arr2[middle])
            {
                if (middle == first || arr1[middle - 1] == arr2[middle - 1])
                {
                    return middle;
                }
                else
                {
                    return BinarySearch(arr1, arr2, first, middle - 1);
                }
            }
            else
            {
                return BinarySearch(arr1, arr2, middle + 1, last);
            }





        }
        #endregion
    }
}
