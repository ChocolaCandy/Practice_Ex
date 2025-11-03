namespace Queue
{
    internal class Program
    {
 
        static void Main(string[] args)
        {
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Dequeue();
            queue.Enqueue(3);
            queue.Print();
        }
    }
}
