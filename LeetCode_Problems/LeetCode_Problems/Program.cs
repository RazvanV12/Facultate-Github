using System;
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
        
        public static void Main(string[] args)
        {
            // Create a int[][] array with these values : [
            /*
            [[1,2,3],[4,5,6],[7,8,9]]*/
            int[][] matrix = new int[3][];
            matrix[0] = new int[] {0, 2, 3, 0};
            matrix[1] = new int[] {4, 5, 6, 2};
            matrix[2] = new int[] {7, 8, 9, 5};
            
            SetZeroes(matrix);
        }
    }
}