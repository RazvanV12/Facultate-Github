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
        
        
        public static void Main(string[] args)
        {
            string s = "abcabcbb"; 
            LengthOfLongestSubstring(s);
        }
    }
}