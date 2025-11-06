namespace Queue
{
    class Program
    {
        static void Main(string[] args)
        {
            Deque<int> queue = new Deque<int>();
            for (int i = 1; i < 3; i++)
            {
                queue.EnqueueFront(i);
            }
            queue.EnqueueRear(3);
            queue.Print();
        }
    }
}
