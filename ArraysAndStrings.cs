using System;
using System.Collections.Generic;

namespace TestCode
{
    // This class contains test methods covering Arrays and Strings
    class ArraysAndStrings
    {
        // 1.1 This function determines if a string has all unique characters
        public static bool CheckForUniqueCharacters(string word)
        {
            for (var i = 0; i < word.Length - 1; i++)
            {
                for (var j = i + 1; j < word.Length; j++)
                {
                    if (word[i] == word[j])
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        // 1.2 Given 2 strings, check if one is a permutation of the other
        public static bool CheckIfPermutation(string original, string perm)
        {
            if (Sort(original) == Sort(perm))
            {
                return true;
            }
            return false;
        }

        // 1.3 Given a string and the "true" length of string, replace all spaces with "%20"
        public static string URLify(string word, int len)
        {
            string url = ""; bool prevWasSpace = true; int idxInArr = 0;
            for (int i = 0; i < word.Length && idxInArr < len; i++)
            {
                if (word[i] == ' ')
                {
                    if (prevWasSpace == false)
                    {
                        url += "%20";
                        prevWasSpace = true;
                        idxInArr++;
                    }
                }
                else
                {
                    url += word[i];
                    prevWasSpace = false;
                    idxInArr++;
                }
            }
            return new string(url);
        }

        // 1.4 Given a string, check if a palindrome can be created using any permutation. Ignore spaces and case
        public static bool CheckIfPermutationIsPalindrome(string word)
        {
            // remove spaces and cases since we are ignoring anyways
            string temp = "";
            for (var i = 0; i < word.Length; i++)
            {
                if (word[i] != ' ')
                {
                    temp += word[i];
                }
            }
            temp = temp.ToLower();
            // 1. Compute all permutations
            // 2. Is it a palindrome?
            List<string> permutationList = new List<string>();
            CreatePermutations(permutationList, temp, 0, temp.Length - 1);
            for (int i = 0; i < permutationList.Count; i++)
            {
                if (CheckIfPalindrome(permutationList[i]))
                {
                    return true;
                }
            }
            return false;
        }

        // 1.4 CreatePermutations
        private static void CreatePermutations(List<string> arr, string word, int currIdx, int endIdx)
        {
            if (currIdx == endIdx)
            {
                arr.Add(word);

            }
            else
            {
                for (int i = currIdx; i <= endIdx; i++)
                {
                    CreatePermutations(arr, Swap(word, i, currIdx), currIdx + 1, endIdx);
                }
            }
        }

        // 1.4 CheckIfPalindrome
        private static bool CheckIfPalindrome(string word)
        {
            for (int i = 0; i < word.Length / 2; i++)
            {
                if (word[i] != word[word.Length - 1 - i])
                {
                    return false;
                }
            }
            return true;
        }

        // 1.5 There are 3 types of edits that can be performed on strings: insert a character, delete a character, or replace a character.
        // Given 2 strings, check if they are >= 1 edit away from each other
        public static bool CheckIfOneEditAway(string a, string b)
        {
            if (a.Length < b.Length)
            {
                string temp = a;
                a = b;
                b = temp;
            }
            char[] aArr = a.ToCharArray();
            char[] bArr = b.ToCharArray();
            int j = 0; bool oneEditUsed = false;
            for (int i = 0; i < aArr.Length; i++)
            {
                if (j >= bArr.Length)
                {
                    if (j == bArr.Length && oneEditUsed == false)
                    {
                        oneEditUsed = true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else if (aArr[i] != bArr[j])
                {
                    if (oneEditUsed)
                    {
                        return false;
                    }
                    else
                    {
                        oneEditUsed = true;
                        if (aArr.Length > bArr.Length)
                        {
                            j--;
                        }
                    }
                }
                j++;
            }
            return true;
        }

        // 1.6 Given a string (a-z), perform basic string compression using the counts of repeated characters.
        // Ex: aabcccccaaa = a2b1c5a3
        // If the original string would be shorter than the compressed string, return the original string
        public static string StringCompression(string str)
        {
            char[] strArr = str.ToCharArray();
            string newStr = ""; char currChar = ' '; int currCount = 0;
            for (int i = 0; i < strArr.Length; i++)
            {
                if (strArr[i] != currChar)
                {
                    if (currChar != ' ' && currCount > 0)
                    {
                        newStr += currChar + currCount.ToString();
                    }
                    currChar = strArr[i];
                    currCount = 1;
                }
                else
                {
                    currCount++;
                }
            }
            if (currChar != ' ' && currCount > 0)
            {
                newStr += currChar + currCount.ToString();
            }
            if (newStr.Length < str.Length)
            {
                return newStr;
            }
            return str;
        }

        // 1.7 Given an image represented by an NxN matrix, where each pixel is 4 bytes, write a method that rotates the image by 90 degrees
        public static int[][] RotateMatrix(int[][] matrix)
        {
            for (int i = 0; i < matrix.Length / 2; i++)
            {
                int first = i;
                int last = matrix.Length - 1 - i;
                for (int j = first; j < last; j++)
                {
                    int offset = j - first;
                    int top = matrix[first][j];
                    matrix[first][j] = matrix[last - offset][first]; 
                    matrix[last - offset][first] = matrix[last][last - offset];
                    matrix[last][last - offset] = matrix[j][last];
                    matrix[j][last] = top;

                }
            }
            return matrix;
        }

        // 1.8 Write an algorithm if an element in an MxN matric is 0, its entire row and column are set to 0
        public static int[][] ZeroMatrix(int[][] matrix)
        {
            bool firstRowHasZero = false;
            bool firstColHasZero = false;
            for (int i = 0; i < matrix[0].Length; i++)
            {
                if (matrix[0][i] == 0)
                {
                    firstRowHasZero = true;
                } 
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i][0] == 0)
                {
                    firstColHasZero = true;
                }
            }
            for (int i = 1; i < matrix.Length; i++)
            {
                for (int j = 1; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        matrix[0][j] = 0;
                        matrix[i][0] = 0;
                    }
                }
            }
            for (int i = 0; i < matrix[0].Length; i++)
            {
                if (matrix[0][i] == 0)
                {
                    ZeroCol(matrix, i);
                }
            }
            for (int i = 0; i < matrix.Length; i++)
            {
                if (matrix[i][0] == 0)
                {
                    ZeroRow(matrix, i);
                }
            }
            if (firstRowHasZero)
            {
                ZeroRow(matrix, 0);
            }
            if (firstColHasZero)
            {
                ZeroCol(matrix, 0);
            }
            return matrix;
        }

        // 1.8 ZeroRow
        private static void ZeroRow(int[][]matrix, int idx)
        {
            for (int i = 0; i < matrix[idx].Length; i++)
            {
                matrix[idx][i] = 0;
            }
        }

        // 1.8 ZeroCol
        private static void ZeroCol(int[][] matrix, int idx)
        {
            for (int i = 0; i < matrix.Length; i++)
            {
                matrix[i][idx] = 0;
            }
        }

        // 1.9 Given 2 strings, check if string b is a rotation of string a. Helper function isSubstring can only be called once
        // Ex: "waterbottle", "erbottlewat"
        public static bool CheckIsStringRotation(string a, string b)
        {
            b += b;
            if (b.Length == (a.Length * 2) && isSubstring(b, a))
            {
                return true;
            }
            return false;
        }

        // 1.8 isSubstring, checks if string b is a substring on string a
        private static bool isSubstring(string a, string b)
        {
            if (b.Length > a.Length)
            {
                return false;
            }
            for (int i = 0; i <= a.Length - b.Length; i++)
            {
                bool isCorrect = true;
                for (int j = 0; j < b.Length; j++)
                {
                    if (a[i + j] != b[j])
                    {
                        isCorrect = false;
                    }
                }
                if (isCorrect)
                {
                    return true;
                }
            }
            return false;
        }

        // Below is begining of basic helper functions 
        private static string Swap(string word, int idxA, int idxB)
        {
            char[] tempWord = word.ToCharArray();
            char tempChar = tempWord[idxA];
            tempWord[idxA] = tempWord[idxB];
            tempWord[idxB] = tempChar;
            return new string(tempWord);
        }
        private static string Sort(string word)
        {
            char[] arr = word.ToCharArray();
            QuickSort(arr, 0, word.Length - 1);
            return new string(arr);
        }

        private static void QuickSort(char[] word, int low, int high)
        {
            if (low < high)
            {
                int partition = Partition(word, low, high);
                QuickSort(word, low, partition - 1);
                QuickSort(word, partition + 1, high);
            }
        }

        private static int Partition(char[] word, int low, int high)
        {
            char pivot = word[high];
            int i = low - 1; char temp;
            for (int j = low; j <= high - 1; j++)
            {
                if (word[j] < pivot)
                {
                    i++;
                    temp = word[i];
                    word[i] = word[j];
                    word[j] = temp;
                }
            }
            temp = word[i + 1];
            word[i + 1] = word[high];
            word[high] = temp;
            return i + 1;
        }
    }
}
