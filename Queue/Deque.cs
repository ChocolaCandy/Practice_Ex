namespace Queue
{
    class Deque<T>
    {
        private T[] _queueArray;
        private int _head;
        private int _tail;
        private int _count;
        public int Head
        {
            get { return _head; }
        }
        public int Tail
        {
            get { return _tail; }
        }

        public Deque()
        {

        }
    }
}
