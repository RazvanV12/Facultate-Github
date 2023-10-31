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
        
        
        
        public static void Main(string[] args)
        {
            /*string test = "blue is the sky";
            string result = ReverseWords(test);*/
        }
    }
}