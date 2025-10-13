namespace Fibonacci
{
    public static class Fibonacci
    {
        public static ulong fibonacci(ulong input)
        {
            ulong Output;
            if (input < 2) { Output = input; }
            else { Output = Result(input); }
            return Output;
        }

        private static ulong Result(ulong input)
        {
            ulong first = 0, second = 1, result;
            for (ulong i = 2; ; i++)
            {
                result = first + second;
                if (i == input) return result;
                first = second;
                second = result;
            }
        }
    }

    class Fibonacci_Ex
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input unsigned number (0~93): ");
                string? input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("You need to input unsigned number");
                    continue;
                }

                if (!ulong.TryParse(input, out ulong index))
                {
                    Console.WriteLine("That's not unsigned number");
                    continue;
                }

                if (index > 93)
                {
                    Console.WriteLine("Beyond my capabilities");
                    continue;
                }

                Console.WriteLine($"The index {index} Fibonacci : {Fibonacci.fibonacci(index)}");
            }
        }
    }
}
