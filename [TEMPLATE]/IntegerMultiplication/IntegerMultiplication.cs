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
    public static class IntegerMultiplication
    {
        #region YOUR CODE IS HERE

        //Your Code is Here:
        //==================
        /// <summary>
        /// Multiply 2 large integers of N digits in an efficient way [Karatsuba's Method]
        /// </summary>
        /// <param name="X">First large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="Y">Second large integer of N digits [0: least significant digit, N-1: most signif. dig.]</param>
        /// <param name="N">Number of digits (power of 2)</param>
        /// <returns>Resulting large integer of 2xN digits (left padded with 0's if necessarily) [0: least signif., 2xN-1: most signif.]</returns>
       
        static public byte[] IntegerMultiply(byte[] X, byte[] Y, int N)
        {
            //REMOVE THIS LINE BEFORE START CODING
            /* if (N == 1)
             {
                 byte[] R = new byte[1];
                 R[0] = (byte)(X[0] * Y[0]);
                 if (R[0] > 9)
                 {
                     byte[] newR = new byte[2];
                     newR[0] = (byte)(R[0] % 10);
                     newR[1] = (byte)(R[0] / 10);
                     return newR;
                 }
                 return R;

             }
            */
            /*    if (X.All(x => x == 0)|| (Y.All(x => x == 0)))
                {
                    byte []h = new byte[1];
                    return h;
                }
              */


            /*B=XL
             A=XR
            D=YL
            C=YR*/

            /*  byte[] XL = new byte[N / 2];
              byte[] XR = new byte[N / 2];
              Array.Copy(X, 0, XR, 0, N / 2);
              Array.Copy(X, N / 2, XL, 0, N / 2);
              byte[] YL = new byte[N / 2];
              byte[] YR = new byte[N / 2];
              Array.Copy(Y, 0, YR, 0, N / 2);
              Array.Copy(Y, N / 2, YL, 0, N / 2);
              */
            /*
         byte[] BCplusAD = add(BC, AD);
         byte[] BCplusADpowerN = shiftRight(BCplusAD, N / 2);
         byte[] BDplusBCplusADpowerN = add(BDpowerN, BCplusADpowerN);
         byte[] result = add(BDplusBCplusADpowerN, AC);
         return result;
      */

            /* 


             byte[] xlplusxr = add(XR, XL);
             byte[] ylplusyr = add(YR, YL);
             int xlplusxrL = xlplusxr.Length;
             int ylplusyrL = ylplusyr.Length;

                 if(xlplusxrL< ylplusyrL)
                 {
                     xlplusxr = shiftLeft(xlplusxr, Math.Abs(xlplusxrL - ylplusyrL));
                 }
                 else
                 {
                     ylplusyr = shiftLeft(ylplusyr, Math.Abs(xlplusxrL - ylplusyrL));
                 }




             byte[] Z = IntegerMultiply(xlplusxr, ylplusyr, ylplusyrL);
             byte[] BD = IntegerMultiply(XL, YL, N / 2);
             byte[] AC = IntegerMultiply(XR, YR, N / 2);
             byte[] BDpowerN = shiftRight(BD, N);
             byte[] ZsubtractBD = subtract(Z, BD);
             byte[] ZsubtractAC_AD = subtract(ZsubtractBD, AC);
             byte[] ZsubtractAC_ADpowerN = shiftRight(ZsubtractAC_AD, N / 2);
             byte[] BDplusZ = add(BDpowerN, ZsubtractAC_ADpowerN);
             byte[] result = add(BDplusZ, AC);
             return result;



            */


            if (N <= 256)
            {
              return multiplicationOfTwoArray(X, Y);
            }
            
            byte[] XL = secondPartOfArray(X, N);
            byte[] XR = firstPartOfArray(X, N);
            byte[] YL = secondPartOfArray(Y, N);
            byte[] YR = firstPartOfArray(Y, N);

            byte[] AplusB = addTwoArray(XL, XR);
            byte[] CplusD = addTwoArray(YL, YR);
            if (AplusB.Length< CplusD.Length)
            {
                int amountOfShift = Math.Abs(CplusD.Length - AplusB.Length);
                AplusB= shiftLeftMakeEqual(AplusB, amountOfShift);
            }
            else
            {
                int amountOfShift = Math.Abs(AplusB.Length - CplusD.Length);
                CplusD= shiftLeftMakeEqual(CplusD, amountOfShift);
            }
            AplusB = makePowerOf2(AplusB);
            CplusD = makePowerOf2(CplusD);

          //byte[] M2 = IntegerMultiply(XL, YL, N / 2);
            Task<byte[]> P1_M2 = Task.Run(() => IntegerMultiply(XL, YL, N / 2));
          //byte[] M1 = IntegerMultiply(XR, YR, N / 2);
            Task<byte[]> P2_M1 = Task.Run(() => IntegerMultiply(XR, YR, N / 2));
          //byte[] Z = IntegerMultiply(AplusB, CplusD, AplusB.Length);
            Task<byte[]> P3_Z = Task.Run(() => IntegerMultiply(AplusB, CplusD, AplusB.Length));
            Task.WaitAll(P1_M2, P2_M1, P3_Z);
            byte[] M2 = P1_M2.Result;
            byte[] M1 = P2_M1.Result;
            byte[] Z = P3_Z.Result;

            byte[] M2powerN = shiftRightPadZeros(M2, N);
            byte[] ZminusM1 = subtractTwoArray(Z, M1);
            byte[] zResult = subtractTwoArray(ZminusM1, M2);

            byte[] zResultpowerN = shiftRightPadZeros(zResult, N / 2);
            byte[] result0 = addTwoArray(M2powerN, zResultpowerN);
            byte[] result = addTwoArray(result0, M1);
            byte[] finalResult = new byte[2*N];
            Array.Copy(result, 0, finalResult, 0, 2 * N);
            return finalResult;
        }
        static public byte[] firstPartOfArray(byte[] a, int n)
        {
            /* byte[] RX = new byte[n / 2];
             for (int i = 0; i < n / 2; i++)     
                 RX[i] = a[i];
             return RX;
            */
            
                byte[] RX = new byte[n / 2];
                Array.Copy(a, RX, n / 2);
                return RX;
            
        }
        static public byte[] secondPartOfArray(byte[] a, int n)
        {
            /*
            byte[] RX = new byte[n / 2];
            for (int i = n / 2; i < n; i++)
                RX[i - (n / 2)] = a[i];
            return RX;
            */
            byte[] RX = new byte[n / 2];
            Array.Copy(a, n / 2, RX, 0, n - (n / 2));
            return RX;
        }
        static public byte[] shiftLeftMakeEqual(byte[] a, int n)
        {
            byte[] result = new byte[a.Length + n];
            Array.Copy(a, 0, result, 0, a.Length);
            /*
            for (int i = 0; i < a.Length; i++)
                result[i] = a[i];
            */
/*
            for (int i = result.Length - n; i < result.Length; i++)
            {
                result[i] = 0;
            }
*/
            return result;
        }
        static public byte[] shiftRightPadZeros(byte[] a, int n)
        {
            byte[] result = new byte[a.Length + n];
/*
            for (int i = 0; i < n; i++)
            {
                result[i] = 0;
            }
*/
           /* for (int i = 0; i < a.Length; i++)
                result[i + n] = a[i];
            return result;
           */
            Array.Copy(a, 0, result, n, a.Length);
            return result;
        }
        static public byte[] addTwoArray(byte[] a, byte[] b) { 
            
            int indexA = a.Length;
            int indexB = b.Length;
            int ammountOfShift = Math.Abs(indexA - indexB);
            if (a.Length < b.Length)
            {
                byte[] A = shiftLeftMakeEqual(a, ammountOfShift);
                int length = A.Length;
                byte[] result = new byte[length];
                byte carry = 0;
                byte sum = 0;
                for (int i =0; i <length; i++)
                {
                    sum = (byte)(A[i] + b[i] + carry);
                    if (sum / 10 != 0)
                        carry = 1;
                    else
                        carry = 0;
                    result[i] = (byte)(sum % 10);

                }

                if (carry != 0)
                {
                    /* byte[] newResult = new byte[length + 1];
                     newResult[newResult.Length-1] = carry;
                     for (int i = 0; i < result.Length; i++)
                     {
                         newResult[i] = result[i];
                     }
                     return newResult;
                    */
                    Array.Resize(ref result, length + 1);
                    result[length] = carry;
                    return result;
                }
                else
                    return result;
            }
            else
            {
                byte[] B = shiftLeftMakeEqual(b, ammountOfShift);
                int length = B.Length;
                byte[] result = new byte[length];
                byte carry = 0;
                byte sum = 0;
                for (int i = 0; i < length; i++)
                {
                    sum = (byte)(a[i] + B[i] + carry);
                    if (sum / 10 != 0)
                        carry = 1;
                    else
                        carry = 0;
                    result[i] = (byte)(sum % 10);
                }
                if (carry != 0)
                {
                   /* byte[] newResult = new byte[length + 1];
                    newResult[newResult.Length-1] = carry;
                    for (int i = 0; i < result.Length; i++)
                    {
                        newResult[i] = result[i];
                    }
                    return newResult;
                   */
                    Array.Resize(ref result, length + 1);
                    result[length ] = carry;
                    return result;
                }
                else
                    return result;
            }


        }
        static public byte[] subtractTwoArray(byte[] a, byte[] b)
        {
            int n = a.Length;
            int m = b.Length;
          /*  if (a.Length < b.Length)
            {
                a = shiftLeft(a, Math.Abs(n - m));
            }
            else
            {
                b = shiftLeft(b, Math.Abs(n - m));
            }
           */
            byte[] c = new byte[n];
            int borrow = 0;
            int i = 0; // start from the first index
            int j = 0;
            while (i < n)
            {
                int x = a[i] - borrow;
                int y = j < m ? b[j] : 0;
                if (x < y)
                {
                    x += 10;
                    borrow = 1;
                }
                else
                {
                    borrow = 0;
                }
                c[i] = (byte)(x - y);
                i++;
                j++;
            }
            // Remove leading zeros if any
            
            int k = 0;
            /*
              while (k < n - 1 && c[k] == 0)
              {
                  k++;
              }
            */
            byte[] result = new byte[n - k];
            for (int l = 0; l < result.Length; l++)
            {
                result[l] = c[k + l];
            }
            return result;
        }
        static public byte[] makePowerOf2(byte[] a)
        { 
            double result = (Math.Log(a.Length, 2));
            if (result % 1 == 0)
                return a;
            else
                result = Math.Ceiling(result);
            int amountOfShift = (int)(Math.Pow(2, result) - a.Length);
            a = shiftLeftMakeEqual(a, amountOfShift);
            return a;
        }
        public static byte[] multiplicationOfTwoArray(byte[] a, byte[] b)
        {

            int lengthOfArray = a.Length;
            byte[] R = new byte[lengthOfArray * 2];
            int count0 = 0;

            while (count0 < lengthOfArray)
            {
                int count1 = 0;
                int carry = 0;

                while (count1 < lengthOfArray)
                {
                    int product = a[count0] * b[count1] + carry + R[count0 + count1];
                    R[count0 + count1] = (byte)(product % 10);
                    carry = product / 10;
                    count1++;
                }

                R[count0 + lengthOfArray] += (byte)carry;
                count0++;
            }

            return R;
        }


        #endregion
    }
}
