﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace LeetCode_Problems
{
    internal class Program
    {
        // LeetCode Interview Problems:
        
        
        // 238. Product of Array Except Self
        // The idea is to an array 'left' to store the products of all the elements to the left of the current element
        // and an array 'right' to store the products of all the elements to the right of the current element.
        // then the product of all the elements except the current element will be left[i] * right[i]
        // Time Complexity: O(n) , Space Complexity: O(n)
        // Another idea is to use the output array to store the products of all the elements to the left of the current element 
        // and then use a variable to store the products of all the elements to the right of the current element
        // Time Complexity: O(n) , Space Complexity: O(1)
        public static int[] ProductExceptSelf_Var1(int[] nums)
        {
            int[] left = new int[nums.Length];
            int[] right = new int[nums.Length];
            int[] output = new int[nums.Length];
            left[0] = nums[0];
            right[nums.Length - 1] = nums[nums.Length - 1];
            for (int i = 1; i < nums.Length; i++)
            {
                left[i] = left[i - 1] * nums[i];
                right[nums.Length - i - 1] = right[nums.Length - i] * nums[nums.Length - i - 1];
            }

            output[0] = right[1];
            output[nums.Length - 1] = left[nums.Length - 2];
            for (int i = 1; i < nums.Length - 1; i++)
            {
                output[i] = left[i - 1] * right[i + 1];
            }
            return output;
        }
        public static int[] ProductExceptSelf_Var2(int[] nums)
        {
            int[] output = new int[nums.Length];
            output[0] = nums[0];
            int product = nums[nums.Length - 1];
            for(int i = 1; i < nums.Length - 1; i++)
            {
                output[i] = output[i - 1] * nums[i];
            }
            output[nums.Length - 1] = output[nums.Length - 2];
            for (int i = nums.Length - 2; i > 0; i--)
            {
                output[i] = output[i - 1] * product;
                product *= nums[i];
            }
            output[0] = product;
            return output;
        }
        
        //134. Gas Station
        // The idea is to check if there is a starting point that can travel around the circle and come back to the starting point
        // If the gas in the i-th station is not enough to travel to the (i+1)-th station, then the starting point cannot be in i-th station
        // Check the next station, if it's enough, then the starting point can be in the next station, run a 'test', if its false
        // restart finding a new starting point from the next station until the end of the array 
        // Time Complexity: O(n^2), Space Complexity: O(1)
        //Another idea is to walk through the whole array, with the 'start' variable 0 and tank 0, if at any point tank becomes negative, we update the start position to the new starting point and reset the tank
        // After the loop is over, if the tank is negative then the journey can't be completed, otherwise the starting point should be the variable 'start'
        // Time Complexity: O(n), Space Complexity: O(1)
        public static int CanCompleteCircuit_Var1(int[] gas, int[] cost)
        {
            if (gas.Length == 1 && gas[0] >= cost[0])
                return 0;
            int sumGas = 0, sumCost = 0;
            for (int i = 0; i < gas.Length; i++)
            {
                sumGas += gas[i];
                sumCost += cost[i];
            }
            if (sumGas < sumCost)
                return -1;
            for (int i = 0; i < gas.Length; i++)
            {
                if (gas[i] <= cost[i])
                    continue;
                int tank = 0;
                int j;
                for (j = i; j < gas.Length + i; j++)
                {
                    tank += gas[j % gas.Length];
                    if (tank < cost[j % gas.Length])
                        break;
                    tank -= cost[j % gas.Length];
                }
                if (j == gas.Length + i)
                    return i;
            }
            return -1;
        }
        public static int CanCompleteCircuit_Var2(int[] gas, int[] cost)
        {
            var n = gas.Length;
            var totalGas = gas.Sum();
            var totalCost = cost.Sum();
            var start = 0;
            var tank = 0;

            if (totalGas < totalCost) return -1;

            for (int i = 0; i < n; i++) {
                tank += gas[i];
                tank -= cost[i];
           
                if (tank < 0) {
                    start = i + 1;
                    tank = 0;
                }
            }
            return (tank >= 0) ? start : -1;
        }
        
        //151. Reverse Words in a String
        // An idea is to simply start from the end and for each word you find you concatenate it to the string 'result'
        // Time Complexity: O(n) , Space Complexity: O(n)
        public static string ReverseWords(string s)
        {
            bool inWord = false;
            // make a new empty string
            string result = null;
            int endWordIndex = new int();
            for (int i = s.Length - 1; i >= 0; i--)
            {
                if (s[i] == ' ' && inWord)
                {
                    result += s.Substring(i + 1, endWordIndex - (i + 1) + 1);
                    result += ' ';
                    inWord = false;
                }
                if (inWord && i == 0) 
                {
                    result += s.Substring(i, endWordIndex + 1);
                    result += ' ';
                }
                if (s[i] != ' ' && i == 0) 
                {
                    result += s.Substring(i, 1);
                    result += ' ';
                }
                if (s[i] == ' ' && !inWord)
                    continue;
                if (s[i] != ' ' && inWord)
                    continue;
                if (s[i] != ' ' && !inWord)
                {
                    endWordIndex = i;
                    inWord = true;
                }
            }
            return result.Substring(0, result.Length - 1);
        }
        
        // 12. Integer to Roman
        // The idea is to simply walk the number from left to right, if the number is of 4 digits and the second digit is 4/9 we write CD/CM, etc...
        public static string IntToRoman(int num)
        {
            string result = null;
            int counter = 0;
            if (num > 999)
            {
                counter = num / 1000;
                for (int i = 0; i < counter; i++)
                    result += 'M';
            }
            num = num % 1000;
            if (num > 99)
            {
                counter = num / 100;
                if (counter < 4)
                    for (int i = 0; i < counter; i++)
                        result += 'C';
                if (counter == 4)
                    result += "CD";
                if (counter < 9 && counter > 4)
                {
                    result += 'D';
                    for (int i = 0; i < counter - 5; i++)
                        result += 'C';
                }

                if (counter == 9)
                    result += "CM";
            }
            num = num % 100;
            if (num > 9)
            {
                counter = num / 10;
                if (counter < 4)
                    for (int i = 0; i < counter; i++)
                        result += 'X';
                if (counter == 4)
                    result += "XL";
                if (counter < 9 && counter > 4)
                {
                    result += 'L';
                    for (int i = 0; i < counter - 5; i++)
                        result += 'X';
                }

                if (counter == 9)
                    result += "XC";
            }

            num = num % 10;
            counter = num;
            if (counter < 4)
                for (int i = 0; i < counter; i++)
                    result += 'I';
            if (counter == 4)
                result += "IV";
            if (counter < 9 && counter > 4)
            {
                result += 'V';
                for (int i = 0; i < counter - 5; i++)
                    result += 'I';
            }

            if (counter == 9)
                result += "IX";
            return result;
        }
        
        // 6. ZigZag Conversion
        // The idea is to make an array of strings and go through 's' by adding a letter to each array from 0 -> (n-1) and then back from (n-1) to 0 and so on till the string 's' is done, after that we simply concatenate each array from 0 to (n-1)
        public static string Convert(string s, int numRows)
        {
            string[] array = new string[numRows];
            string result = null;
            int index = 0;
            bool isIncreasing = true;
            if (numRows == 1)
                return s;
            for (int i = 0; i < s.Length; i++)
            {
                array[index] += s[i];
                if (isIncreasing)
                    index++;
                else
                {
                    index--;
                }
                if (index == numRows - 1)
                    isIncreasing = false;
                if (index == 0)
                    isIncreasing = true;
            }

            for (int i = 0; i < numRows; i++)
            {
                result += array[i];
            }

            return result;
        }
        
        // 11. Container With Most Water
        // We simply check with 2 loops each possible solution and return the max value
        // We can first check if with the height[i] can be resulted in an area higher than the max one calculated so far
        public static int MaxArea(int[] height)
        {
            int maxArea = 0;
            for (int i = 0; i < height.Length - 1; i++)
            {
                for (int j = i + 1; j < height.Length; j++)
                {
                    if (height[i] * (height.Length - 1 - i) < maxArea)
                        break;
                    if (height[i] < height[j])
                    {
                        if (height[i] * (j - i) > maxArea)
                            maxArea = height[i] * (j - i);
                    }
                    else 
                        if (height[j] * (j - i) > maxArea)
                            maxArea = height[j] * (j - i);
                }
            }
            return maxArea;
        }
        
        // 15. 3Sum
        // First we sort the array,  then we start from left to right and for each number we use the algorithm from 2Sum II ( with 2 pointers left and right and we check for the sum to be == nums[i] )
        // [-4, -1, -1, 0, 1, 2]
        // Time Complexity: O(n^2) , Space Complexity: O(1)
        // [-3, -2, -1, 0, 1, 2, 3]
        public static IList<IList<int>> ThreeSum(int[] nums)
        {
            Array.Sort(nums);
            IList<IList<int>> result = new List<IList<int>>();
            for (var i = 0; i < nums.Length - 2; i++)
            {
                if(i != 0 && nums[i] == nums[i - 1])
                    continue;
                var left = i + 1;
                var right = nums.Length - 1;
                while (left < right)
                {
                    if (nums[left] + nums[right] == -nums[i])
                    {
                        IList<int> aux = new List<int>();
                        aux.Add(nums[i]);
                        aux.Add(nums[left]);
                        aux.Add(nums[right]);
                        result.Add(aux);
                        while (nums[right - 1] == nums[right] && right > 1)
                            right--;
                        while (nums[left + 1] == nums[left] && left < nums.Length - 2)
                            left++;
                    }
                    if(nums[left] + nums[right] < -nums[i])
                        left++;
                    else
                        right--;
                }
            }
            return result;
        }
        
        // 209. Minimum Size Subarray Sum
        //  target = 7, nums = [2,3,1,2,4,3]
        // Common Sliding Window Problem: we start with 2 pointers left and right both equal to 0, we move the right pointer until the sum of the subarray is >= target, then we move the left pointer until the sum of the subarray is < target,
        // we keep track of the min length of the subarray
        public static int MinSubArrayLen_Var1(int target, int[] nums)
        {
            int left = 0, right = 0, sum = 0, min = int.MaxValue;
            while (right < nums.Length)
            {
                sum += nums[right];
                while (sum >= target && left <= right)
                {
                    min = Math.Min(min, right - left + 1);
                    sum -= nums[left];
                    left++;
                }

                right++;
            }
            if(min > nums.Length)
                return 0;
            return min;
        }
        
        // 3. Longest Substring Without Repeating Characters
        // s = "abcabcbb"
        // We use a dictionary to keep track of the last index of each character, we start with 2 pointers left and right both equal to 0, we move the right pointer until we find a duplicate character,
        // then we move the left pointer to the last index of the duplicate character + 1
        public static int LengthOfLongestSubstring(string s)
        {
            var myDict = new Dictionary<char, int>();
            int left = 0, right = 0, max = 0;
            while (right < s.Length)
            {
                if (!myDict.ContainsKey(s[right]))
                {
                    myDict.Add(s[right], right);
                    max = Math.Max(max, right - left + 1);
                    right++;
                    continue;
                }
                while (myDict.ContainsKey(s[right]))
                {
                    myDict.Remove(s[left]);
                    left++;
                }
                myDict[s[right]] = right;
                max = Math.Max(max, right - left + 1);
                right++;
            }
            return max;
        }
        
        /* 36. Valid Sudoku
         The first idea that comes to mind is to simply check for each cell the elements from it's line and column and to then check in the 3x3 grid that it belongs to ( basically all the elements from a grid have the same modulo 3 result for line and column )
         */
        public static bool IsValidSudoku(char[][] board)
        {
            // first we iterate through the matrix checking for each cell the elements from it's line and column ( since it is a standard 9x9 matrix, even if we have O(n^3) it is not really relevant
            for (int i = 0; i < board.Length; i++)
            {
                for (int j = 0; j < board.Length; j++)
                {
                    if (board[i][j] == '.')
                        continue;
                    for (int k = j + 1; k < board.Length; k++)
                    {
                        if (board[i][j] == board[i][k])
                            return false;
                    }
                    for (int k = i + 1; k < board.Length; k++)
                    {
                        if (board[i][j] == board[k][j])
                            return false;
                    }
                    // We check for each element of it's 3x3 grid
                    int gridLine = i / 3;
                    int gridColumn = j / 3;
                    for (int line = 3 * gridLine; line < 3 * (gridLine + 1); line++)
                    {
                        for (int column = 3 * gridColumn; column < 3 * (gridColumn + 1); column++)
                        {
                            if(i != line ||  j != column)
                                if (board[i][j] == board[line][column])
                                    return false;
                        }
                    }
                }
            }
            return true;
        }
        
        /* Spiral Matrix
         The idea is to simply walk through the matrix in a spiral way, realizing the following: 
            - first step will be to go to the right for (n-1) times
            - next step to go down (m-1) times
            - then (n-1) times left
            - then (m-2) times up
            - then (n-2) times right
            
        And so on until (n-x) = 0 or (m-x) = 0, then you simply walk either (n-x) steps or (m-x) steps in the direction that is the following*/
        public static IList<int> SpiralOrder(int[][] matrix)
        {
            // Since it is given m & n >= 1, we can make the first step to the right without any loop and without any error
            IList<int> result = new List<int>();
            bool left = false;
            bool up = false;
            int line = 0, column = 0;
            int n = matrix[line].Length - 1;
            int m = matrix.Length - 1;
            int i = n;
            while(i > 0){
                result.Add(matrix[line][column]);
                column++;
                i--;
            }
            bool right = false;
            bool down = true;
            // Now begins the loop, following the steps mentioned earlier
            while (n > 0 || m > 0)
            {
                if (right)
                {
                    i = n;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        column++;
                        i--;
                    }
                    right = false;
                    down = true;
                    n--;
                    continue;
                }
                if (down)
                {
                    i = m;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        line++;
                        i--;
                    }
                    down = false;
                    left = true;
                    m--;
                    continue;
                }
                if (left)
                {
                    i = n;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        column--;
                        i--;
                    }
                    left = false;
                    up = true;
                    n--;
                    continue;
                }
                if (up)
                {
                    i = m;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        line--;
                        i--;
                    }
                    up = false;
                    right = true;
                    m--;
                }
            }
            if (n == 0)
            {
                if (up)
                {
                    i = m;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        line--;
                        i--;
                    }
                }
                else
                {
                    i = m;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        line++;
                        i--;
                    }
                }
            }
            else
            {
                if (right)
                {
                    i = n;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        column++;
                        i--;
                    }
                }
                else
                {
                    i = n;
                    while(i > 0){
                        result.Add(matrix[line][column]);
                        column--;
                        i--;
                    }
                }
            }
            if(matrix[line].Length == matrix.Length && matrix[line].Length % 2 == 1)
                result.Add(matrix[line][column]);
            return result;
        }
        public static IList<int> SpiralOrderGPT(int[][] matrix) {
            IList<int> result = new List<int>();
            if (matrix.Length == 0) return result;
            int rowBegin = 0;
            int rowEnd = matrix.Length - 1;
            int colBegin = 0;
            int colEnd = matrix[0].Length - 1;

            while (rowBegin <= rowEnd && colBegin <= colEnd) {
                for (int col = colBegin; col <= colEnd; col++) {
                    result.Add(matrix[rowBegin][col]);
                }
                rowBegin++;

                for (int row = rowBegin; row <= rowEnd; row++) {
                    result.Add(matrix[row][colEnd]);
                }
                colEnd--;

                if (rowBegin <= rowEnd) {
                    for (int col = colEnd; col >= colBegin; col--) {
                        result.Add(matrix[rowEnd][col]);
                    }
                }
                rowEnd--;

                if (colBegin <= colEnd) {
                    for (int row = rowEnd; row >= rowBegin; row--) {
                        result.Add(matrix[row][colBegin]);
                    }
                }
                colBegin++;
            }

            return result;
        }

        /*48. Rotate Image
         The first thing we notice is that first line becomes column n-1, second line becomes column n-2 and so on. The problem comes in modifying the matrix in place, we do that by rotating the edge of the matrix and then going deeper in the matrix by 1 line and 1 column*/
        public static void Rotate(int[][] matrix)
        {
            var numberOfRotations = matrix.Length / 2;
            var rotationNumber = 0;
            while (rotationNumber < numberOfRotations)
            {
                for (var i = rotationNumber; i < matrix.Length - rotationNumber - 1; i++)
                {
                    var aux1 = matrix[i][matrix.Length - rotationNumber - 1];
                    var aux2 = matrix[matrix.Length - rotationNumber - 1][matrix.Length - i - 1];
                    var aux3 = matrix[matrix.Length - i - 1][rotationNumber];
                    matrix[i][matrix.Length - rotationNumber - 1] = matrix[rotationNumber][i];
                    matrix[matrix.Length - rotationNumber - 1][matrix.Length - i - 1] = aux1;
                    matrix[matrix.Length - i - 1][rotationNumber] = aux2;
                    matrix[rotationNumber][i] = aux3;
                }
                rotationNumber++;
            }
        }
        
        /* 73. Set Matrix Zeroes
         The solution using O(m+n) uses an array of size m which holds all the lines that you need to make equal to 0 and an array
         of size n that holds all the columns that you need to make equal to 0. You populate these arrays by simply
         walking once through the matrix and then you walk again to modify it.
         */
        public static void SetZeroes(int[][] matrix)
        {
            var nrColumns = matrix[0].Length;
            var nrLines = matrix.Length;
            var firstLine = false;
            var firstColumn = false;
            for (int i = 0; i < nrColumns; i++)
            {
                if (matrix[0][i] == 0)
                    firstLine = true;
            }
            for (int i = 0; i < nrLines; i++)
            {
                if (matrix[i][0] == 0)
                    firstColumn = true;
            }
            for (var i = 1; i < nrLines; i++)
            {
                for (var j = 1; j < nrColumns; j++)
                {
                    if (matrix[i][j] == 0)
                    {
                        matrix[0][j] = 0;
                        matrix[i][0] = 0;
                    }
                }
            }
            for (var i = 1; i < nrLines; i++)
            {
                if (matrix[i][0] == 0)
                {
                    for (var j = 0; j < nrColumns; j++)
                        matrix[i][j] = 0;
                }
            }
            for (var j = 1; j < nrColumns; j++)
            {
                if (matrix[0][j] == 0)
                {
                    for (var i = 0; i < nrLines; i++)
                        matrix[i][j] = 0;
                }
            }
            if (firstLine)
            {
                for (int j = 0; j < nrColumns; j++)
                {
                    matrix[0][j] = 0;
                }
            }
            if (firstColumn)
            {
                for (int i = 0; i < nrLines; i++)
                {
                    matrix[i][0] = 0;
                }
            }
        }
        
        // 49. Group Anagrams
        // The idea is to have a dictionary with keys of arrays ( frequence array, where we count how many times each letter appears in the word ) and the values will be 
        // the index of the word in the given array. Doing so we associate the anagrams via the frequence array and we simply print the dictionary.
        // The issue with this solution is that even if the key is equal to the frequence array of the new word, when we check the dictionary it will return that 
        // it does not contain such a key because the frequence array and the key from the dictionary are 2 distinct elements in the memory, even if they are identical
        
        /*public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<List<int>, List<int>> myDict = new Dictionary<List<int>, List<int>>();
            IList<IList<string>> result = new List<IList<string>>();
            for (int i = 0; i < strs.Length; i++)
            {
                List<int> aux = new List<int>(new int[26]);
                for (int j = 0; j < strs[i].Length; j++)
                {
                    aux[strs[i][j] - 97]++;
                }

                if (myDict.ContainsKey(aux))
                {
                    myDict[aux].Add(i);
                }
                else
                {
                    List<int> auxToAdd = new List<int>(new int[] { i });
                    myDict.Add(aux, auxToAdd);
                }
            }

            foreach (KeyValuePair<List<int>, List<int>> element in myDict)
            {
                List<string> wordsToAdd = new List<string>();
                foreach (var value in element.Value)
                {
                    wordsToAdd.Add(strs[value]);
                }

                result.Add(wordsToAdd);
            }
            return result;
        }
        */

        // A second idea is to simply have an array of tuples of a bool (representing if the word has been assigned or not) and an array of frequencies to check if 
        // the current word is an anagram with the word we are checking. If yes, we make the bool true and assign it to the result list.
        public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            IList<IList<string>> result = new List<IList<string>>();
            List<List<int>> frequencies = new List<List<int>>();
            for (int i = 0; i < strs.Length; i++)
            {
                List<int> frequencyList = new List<int>(new int[26]);
                for (int j = 0; j < strs[i].Length; j++)
                {
                    frequencyList[strs[i][j] - 97]++;
                }
                frequencies.Add(frequencyList);
            }

            List<int> assignedWords = new List<int>();
            for (int i = 0; i < strs.Length; i++)
            {
                if (assignedWords.BinarySearch(i) < 0)
                {
                    List<string> auxResult = new List<string>();
                    auxResult.Add(strs[i]);
                    assignedWords.Add(i);
                    assignedWords.Sort();
                    for (int j = i + 1; j < strs.Length; j++)
                    {
                        if (assignedWords.BinarySearch(j) < 0 && frequencies[i].SequenceEqual(frequencies[j]))
                        {
                            auxResult.Add(strs[j]);
                            assignedWords.Add(j);
                            assignedWords.Sort();
                        }
                    }
                    result.Add(auxResult);
                }
            }

            return result;
        }
        // Test casses passed, but TLE (time limit exceeded)
        // Solution made by ChatGpt:
        /*public static IList<IList<string>> GroupAnagrams(string[] strs)
        {
            Dictionary<string, List<string>> myDict = new Dictionary<string, List<string>>();
            IList<IList<string>> result = new List<IList<string>>();
            for (int i = 0; i < strs.Length; i++)
            {
                char[] aux = strs[i].ToCharArray();
                Array.Sort(aux);
                string key = new string(aux);
                if (myDict.ContainsKey(key))
                {
                    myDict[key].Add(strs[i]);
                }
                else
                {
                    List<string> auxToAdd = new List<string>(new string[] { strs[i] });
                    myDict.Add(key, auxToAdd);
                }
            }

            foreach (KeyValuePair<string, List<string>> element in myDict)
            {
                result.Add(element.Value);
            }
            return result;
        }
        */
        
        
        // 128. Longest subsequence consecutive
        // The idea is to transform the input array into a set so that we can have the lookup complexity of O(1). Then, for each element we check if it is the
        // beginning of a sequence by checking if we have (n-1) in the set. If it is, then we check the length of the sequence by checking if (n+1) belongs
        // to the set, then (n+2) and so on. We do that for every number that is the beginning of a sequence and so we will have the longest subsequence.
        // The time complexity of the problem will be O(n).
        public static int LongestConsecutive(int[] nums)
        {
            int longestSubsequence = 0;
            HashSet<int> mySet = new HashSet<int>(nums);
            foreach (var element in mySet)
            {
                // Check to see if it is the beginning of the sequence
                if (!mySet.Contains(element - 1))
                {
                    int lengthCurrentSequence = 1;
                    while (mySet.Contains(element + lengthCurrentSequence))
                    {
                        lengthCurrentSequence++;
                    }

                    if (lengthCurrentSequence > longestSubsequence)
                        longestSubsequence = lengthCurrentSequence;
                }
            }
            return longestSubsequence;
        }
        
        // 56. Merge intervals 
        // The first idea that comes to mind is to check for each interval all the intervals that have the start smaller than the end of the current one. If we find one
        // we merge them, eliminate the interval from the matrix and we start looking again from the beginning.
        public static int[][] Merge(int[][] intervals)
        {
            int size = intervals.Length;
            int[][] result = new int[][] { };
            for (int i = 0; i < size; i++)
            {
                while (true)
                {
                    bool merged = false;
                    for (int j = i + 1; j < size; j++)
                        if (intervals[i][1] >= intervals[j][0] && intervals[i][0] <= intervals[j][1])
                        {
                            if (intervals[i][0] >= intervals[j][0] && intervals[i][1] <= intervals[j][1])
                            {
                                intervals[i][0] = intervals[j][0];
                                intervals[i][1] = intervals[j][1];
                            }

                            if (intervals[i][0] <= intervals[j][0] && intervals[i][1] <= intervals[j][1])
                            {
                                intervals[i][1] = intervals[j][1];
                            }

                            if (intervals[i][0] >= intervals[j][0] && intervals[i][1] >= intervals[j][1])
                            {
                                intervals[i][0] = intervals[j][0];
                            }
                            

                            for (int k = j; k < size - 1; k++)
                            {
                                intervals[k][0] = intervals[k + 1][0];
                                intervals[k][1] = intervals[k + 1][1];
                            }
                            size--;
                            merged = true;
                            break;
                        }

                    if (!merged)
                        break;
                }
            }
            result = new int[size][];
            for (int i = 0; i < size; i++)
            {
                result[i] = new int[] { intervals[i][0], intervals[i][1] };
            }
            return result;
        }
        
        // 57. Insert Interval
        // The idea is to first decide where the newInterval should be. We do that by looking at it's start and it's position will be between 2 consecutive intervals 
        // such that the start of newInterval is smaller than start of rightInterval and bigger than start of LeftInterval. After that, we simply check if there is an
        // overlap, if so, we merge the necessary intervals.

        // public static int[][] Insert(int[][] intervals, int[] newInterval)
        // {
        //     // first we find the position where we have to insert the newInterval
        //     int positionToInsert = 0;
        //     for (int i = 0; i < intervals.Length; i++)
        //     {
        //         if (intervals[i][0] > newInterval[0])
        //             positionToInsert = i;
        //     }
        //     // Special cases: If intervals is empty or if we have to insert it at the beginning of the array
        //     if (intervals.Length == 0)
        //     {
        //         int[][] auxResult = new int[][] { newInterval };
        //         return auxResult;
        //     }
        //     // We will first check to see how many intervals we have to merge in order to keep the array with non-overlapping intervals
        //     int mergeCounter = 0;
        //     if (positionToInsert != 0)
        //     {
        //         if (intervals[positionToInsert - 1][1] >= newInterval[0])
        //             mergeCounter++;
        //     }
        //
        //     for (var i = positionToInsert; i < intervals.Length; i++)
        //     {
        //         if (newInterval[1] >= intervals[i][0])
        //             mergeCounter++;
        //     }
        //
        //     int[][] result = new int[intervals.Length - mergeCounter][];
        //     // If it is possible to merge with the interval that's on the left, then we have to copy all the intervals until positionToInsert - 2, then we add
        //     // the merged interval and then the rest.
        //     if (intervals[positionToInsert - 1][1] >= newInterval[0])
        //     {
        //         for (var i = 0; i < positionToInsert - 1; i++)
        //         {
        //             result[i][0] = intervals[i][0];
        //             result[i][1] = intervals[i][1];
        //         }
        //
        //         int[] mergedInterval = new int[2];
        //         mergedInterval[0] = intervals[positionToInsert - 1][0];
        //         mergedInterval[1] = intervals[positionToInsert + mergeCounter - 1][1];
        //         result[positionToInsert][0] = mergedInterval[0];
        //         result[positionToInsert][1] = mergedInterval[1];
        //         for (var i = positionToInsert + 1; i < intervals.Length; i++)
        //         {
        //             result[i][0] = intervals[i + mergeCounter + 1][0];
        //             result[i][1] = intervals[i + mergeCounter + 1][1];
        //         }
        //     }
        //     else
        //     {
        //         for (var i = 0; i < positionToInsert; i++)
        //         {
        //             result[i][0] = intervals[i][0];
        //             result[i][1] = intervals[i][1];
        //         }
        //
        //         int[] mergedInterval = new int[2];
        //         mergedInterval[0] = newInterval[0];
        //         
        //     }
        // }
        
        // 57. Insert Interval
        public static int[][] Insert(int[][] intervals, int[] newInterval)
        {
            List<int[]> result = new List<int[]>();
            int i = 0;
            // Add all the intervals that are before the newInterval
            while (i < intervals.Length && intervals[i][1] < newInterval[0])
            {
                result.Add(intervals[i]);
                i++;
            }
            // Merge all the intervals that overlap with the newInterval
            while (i < intervals.Length && intervals[i][0] <= newInterval[1])
            {
                newInterval[0] = Math.Min(newInterval[0], intervals[i][0]);
                newInterval[1] = Math.Max(newInterval[1], intervals[i][1]);
                i++;
            }
            result.Add(newInterval);
            // Add all the intervals that are after the newInterval
            while (i < intervals.Length)
            {
                result.Add(intervals[i]);
                i++;
            }
            return result.ToArray();
        }
        
        // 172. Factorial Trailing Zeroes
        // The first idea is to simply calculate n! and then check how many trailing zeroes it has. The time complexity will be O(n)

        public static int calculateFactorial(int n)
        {
            if (n == 0)
            {
                return 1;
            }
            else
            {
                return calculateFactorial(n - 1) * n;
            }
        }
        // This is the solution for time complexity O(n)
        public static int TrailingZeroes(int n)
        {
            int factorial = calculateFactorial(n);
            int result = 0;
            if (factorial == 0)
                return 1;
            while (factorial % 10 == 0)
            {
                result++;
                factorial = factorial / 10;
            }

            return result;
        }
        
        // Solution for the time complexity O(logn) is to understand that we get a trailing zero for each number multiple of 5 smaller than n. This happens because 
        // we need a 10 to have a trailing zero, which is obtained from 5 * 2 , but the number of numbers multiple of 2 will always be greater than the number of 
        // numbers multiple of 5. 
        public static int TrailingZeroes2(int n)
        {
            int count = 0;

            while (n >= 5) {
                n /= 5;
                count += n;
            }
            return count;
        }
        
        // 50. Pow(x, n) 
        public static int MyPow(int x, int n)
        {
            if (n == 0)
                return 1;
            if (n == 1)
                return x;
            if (n == -1)
                return 1 / x;
            if (n % 2 == 0)
                return MyPow(x * x, n / 2);
            else
                return x * MyPow(x * x, n / 2);
        }
        
        
        
        public static void Main(string[] args)
        {
            // Print factorial of 5 using the functon calculateFactorial
            Console.WriteLine(TrailingZeroes2(9));
        }
    }
}