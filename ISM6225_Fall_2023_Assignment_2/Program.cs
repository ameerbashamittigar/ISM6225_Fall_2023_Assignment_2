using System.Text;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();
            
            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1};
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();
            

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            try
            {
                // Ensuring that the constraint is met: -109 <= lower <= upper <= 109
                if (lower > upper || lower <-109 || upper >109)
                {
                    throw new ArgumentException("Invalid lower or upper bounds.");
                }

                // Ensuring that length of array is between 0 and 100 (inclusive)
                if (nums.Length > 100 || nums.Length<0)
                {
                    throw new ArgumentException("The length of the nums array should be between 0 and 100");
                }

                // Handle duplicate values
                HashSet<int> uniqueValues = new HashSet<int>();
                foreach (int num in nums)
                {
                    if (!uniqueValues.Add(num))
                    {
                        throw new ArgumentException("Duplicate values found in the nums array.");
                    }
                }

                IList<IList<int>> result = new List<IList<int>>();

                // Check if the input array 'nums' is empty or null
                if (nums == null || nums.Length == 0)
                {
                    if (lower == upper)
                    {
                        // If the lower and upper bounds are the same, add a single-element range
                        result.Add(new List<int> { lower });
                    }
                    else
                    {
                        // If the lower and upper bounds are different, add a range spanning from lower to upper
                        result.Add(new List<int> { lower, upper });
                    }
                }
                else
                {
                    // Check if there is a missing range before the first element
                    if (lower < nums[0])
                    {
                        if (lower == nums[0] - 1)
                        {
                            // If the missing range is just one element, add it to 'result'
                            result.Add(new List<int> { lower });
                        }
                        else
                        {
                            // If the missing range is more than one element, add it as a range
                            result.Add(new List<int> { lower, nums[0] - 1 });
                        }
                    }

                for (int i = 1; i < nums.Length; i++)
                {
                    long diff = (long)nums[i] - nums[i - 1];
                    if (diff > 1)
                    {
                        if (diff == 2)
                        {
                            // If there is exactly one missing element, add it to 'result'
                            result.Add(new List<int> { nums[i - 1] + 1 });
                        }
                        else
                        {
                            // If there is more than one missing element, add the range to 'result'
                            result.Add(new List<int> { nums[i - 1] + 1, nums[i] - 1 });
                        }
                    }
                }

            // Check if there is a missing range after the last element
            if (upper > nums[nums.Length - 1])
            {
                if (upper == nums[nums.Length - 1] + 1)
                {
                    // If the missing range is just one element, add it to 'result'
                    result.Add(new List<int> { upper });
                }
                else
                {
                    // If the missing range is more than one element, add it as a range
                    result.Add(new List<int> { nums[nums.Length - 1] + 1, upper });
                }
            }
        }

        return result;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public static bool IsValid(string s)
        {
            try
            {
                // Ensuring that length of input string is between 1 and 104 (inclusive)
                if (s.Length < 1 || s.Length > 104)
                    {
                        throw new ArgumentException("The length of the input string should be between 1 and 104");
                    }

                // Define a set of valid characters for the string
                HashSet<char> validChars = new HashSet<char> { '(', ')', '[', ']', '{', '}' };
                    foreach (char c in s)
                    {
                        if (!validChars.Contains(c))
                        {
                            // If the string contains other characters, it can't be valid
                            throw new ArgumentException("Invalid characters.");
                        }
                    }

                // If the string length is odd, it can't be valid
                if (s.Length % 2 != 0)
                    {
                        return false;
                    }

                // Create a stack to store opening brackets
                Stack<char> stack = new Stack<char>();

                foreach (char c in s)
                {
                    if (c == '(' || c == '[' || c == '{')
                    {
                        // Push open brackets to the stack
                        stack.Push(c);
                    }
                    else
                    {
                        if (stack.Count == 0)
                        {
                            // If the stack is empty, there's no corresponding open bracket
                            return false;
                        }

                        char openBracket = stack.Pop();

                        // Check if the current closing bracket matches the most recent open bracket
                        if ((c == ')' && openBracket != '(') ||
                            (c == ']' && openBracket != '[') ||
                            (c == '}' && openBracket != '{'))
                        {
                            return false;
                        }
                    }
                }

            // If the stack is empty at the end, all brackets were properly closed
            return stack.Count == 0;
            }

            catch (Exception)
            {
                throw;
            }
        }

        public static int MaxProfit(int[] prices)
        {
            try{
                // Ensuring that length of array is between 1 and 105 (inclusive)
                if (prices.Length < 1 || prices.Length > 105)
                    {
                        throw new ArgumentException("The length of the price array should be between 1 and 105");
                    }

                // Ensuring that price value in array is between 0 and 104 (inclusive)
                foreach (int price in prices)
                    {
                        if (price < 0 || price > 104)
                        {
                            throw new ArgumentException("Invalid input: prices[i] is out of range.");
                        }
                    }

                // Initialize the minimum price as a high value
                int minPrice = Int32.MaxValue; 
                int maxProfit = 0;

                for (int i = 0; i < prices.Length; i++)
                {
                    if (prices[i] < minPrice)
                    {
                        // Update the minimum price if we find a smaller value
                        minPrice = prices[i]; 
                    }
                    else if (prices[i] - minPrice > maxProfit)
                    {
                        // Update the maximum profit if selling on this day is profitable
                        maxProfit = prices[i] - minPrice; 
                    }
                }

                return maxProfit;
             }
            catch (Exception)
            {
                throw;
            }
        }
  
        public static bool IsStrobogrammatic(string s)
        {
            try
            {
                // Ensuring that length of array is between 0 and 50 (inclusive)
                if (s.Length<1 | s.Length>50)
                {
                    throw new ArgumentException("The length of the string should be between 0 and 50");
                }

                // Check if the input consists only digits
                for (int i = 0; i < s.Length; i++)
                {
                    if (!Char.IsDigit(s[i]))
                    {
                        throw new ArgumentException("String contains other characters");
                    }
                }

                // Check for leading zeros (except for zero itself)
                if (s.Length > 1 && s[0] == '0' && s[s.Length - 1] != '0')
                {
                    throw new ArgumentException("Leading zeros found");
                }
        
                int left = 0;
                int right = s.Length - 1;

                while (left <= right)
                {
                    char leftChar = s[left];
                    char rightChar = s[right];
                    if (leftChar == '0' && rightChar == '0' ||
                        leftChar == '1' && rightChar == '1' ||
                        leftChar == '8' && rightChar == '8' ||
                        leftChar == '6' && rightChar == '9' ||
                        leftChar == '9' && rightChar == '6')
                    {
                        left++;
                        right--;
                    }
                    else
                    {
                        return false; // Not a strobogrammatic number
                    }
                }
                return true; // It's a strobogrammatic number
            }
            catch (Exception)
            {
                throw;
            }
        }
 
        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                //Ensuring that length of array is between 1 and 100 (inclusive)
                if (nums.Length < 1 || nums.Length > 100)
                 {
                     throw new ArgumentException("The length of the nums array should be between 1 and 100");
                 }

                // Ensuring that num value in array is between 0 and 100 (inclusive)    
                foreach (int num in nums)
                {
                    if (num < 0 || num > 100)
                    {
                        throw new ArgumentException("Invalid input: num[i] is out of range.");
                    }
                }

                Dictionary<int, int> countMap = new Dictionary<int, int>();
                int goodPairs = 0;

                foreach (int i in nums)
                {
                    if (countMap.ContainsKey(i))
                    {
                        goodPairs += countMap[i]; // Increase goodPairs by the count of existing numbers.
                        countMap[i]++; // Increment the count of this number.
                    }
                    else
                    {
                        countMap[i] = 1; // Initialize the count for this number.
                    }
                }

                return goodPairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static int ThirdMax(int[] nums)
        {
            try
            {
                // Ensuring that length of array is between 1 and 104 (inclusive)
                if (nums.Length < 1 || nums.Length > 104)
                {
                    throw new ArgumentException("The length of the nums array should be between 1 and 104");
                }

                // Ensuring that num value in array is between -231 and 230 (inclusive)
                foreach (int num in nums)
                {
                    if (num < -231 || num > 230)
                    {
                        throw new ArgumentException("Invalid input: num[i] is out of range.");
                    }
                }

                // Use a set to store unique values in descending order.
                SortedSet<int> uniqueValues = new SortedSet<int>(nums, Comparer<int>.Create((a, b) => b.CompareTo(a)));

                if (uniqueValues.Count < 3)
                {
                    // If there are fewer than 3 distinct maximums, return the maximum.
                    return uniqueValues.First();
                }
                else
                {
                    // If there are at least 3 distinct maximums, return the third maximum.
                    return uniqueValues.ElementAt(2);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                //Ensuring that length of currentState string is between 1 and 500 (inclusive)
                if (currentState.Length < 1 || currentState.Length > 500)
                {
                    throw new ArgumentException("The length of the string should be between 1 and 500");
                }
                
                // Define a set of valid characters
                HashSet<char> validChars = new HashSet<char> { '+', '-' };
                    foreach (char c in currentState)
                    {
                        if (!validChars.Contains(c))
                        {
                            // If the string contains other characters, it can't be valid
                            throw new ArgumentException("Invalid characters found");
                        }
                    }

                List<string> possibleMoves = new List<string>();

                for (int i = 0; i < currentState.Length - 1; i++)
                {
                    if (currentState[i] == '+' && currentState[i + 1] == '+')
                    {
                        char[] nextState = currentState.ToCharArray();
                        nextState[i] = '-';
                        nextState[i + 1] = '-';
                        possibleMoves.Add(new string(nextState));
                    }
                }

                return possibleMoves;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public static string RemoveVowels(string s)
            {
                //Create empty string to store result
                string result = "";
                foreach (char c in s)
                {
                    if (c != 'a' && c != 'e' && c != 'i' && c != 'o' && c != 'u' &&
                        c != 'A' && c != 'E' && c != 'I' && c != 'O' && c != 'U')
                    {
                        result += c;
                    }
                }

                return result;
            }


        
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }

        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}
