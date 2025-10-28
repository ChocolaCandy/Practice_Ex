namespace Stack
{
    class Stack_Ex
    {
        static void Main(string[] args)
        {
            StackByArray<int> stack = new StackByArray<int>();
            stack.Push(12);
            stack.Push(345);
            stack.Push(6789);
            stack.State();
        }
    }
}


