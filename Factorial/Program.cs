namespace Factorial
{
    public static class Factorial
    {
        public static ulong fatorial(ulong input)
        {
            if (input <= 1) return 1;
            return input * fatorial(input - 1);
        }
    }

    class Factorial_Ex
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Write("Input unsigned number (0~20): ");
                string? s_input = Console.ReadLine();
                if (String.IsNullOrWhiteSpace(s_input))
                {
                    Console.WriteLine("You need to input unsigned number");
                    continue;
                }

                if (!ulong.TryParse(s_input, out ulong input))
                {
                    Console.WriteLine("That's not unsigned number");
                    continue;
                }

                if (input > 20)
                {
                    Console.WriteLine("Beyond my capabilities");
                    continue;
                }

                Console.WriteLine($"{input} Factorial : {Factorial.fatorial(input)}");
            }
        }
    }
}
