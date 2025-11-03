namespace Queue
{
    class Queue<T>
    {
        #region Fields
        //기본 큐 크기
        private readonly int defaultSize = 4;
        //큐 배열
        public T[] _array;
        //큐 헤드
        private int _head;
        //큐 테일
        private int _tail;
        //큐 요소 수
        private int _count;
        #endregion

        #region Constructors
        public Queue()
        {
            _array = new T[defaultSize];
            _head = 0;
            _tail = 0;
            _count = 0;
        }
        public Queue(int size)
        {
            _array = new T[size];
            _head = 0;
            _tail = 0;
            _count = 0;
        }
        public Queue(Queue<T> queue)
        {
            _array = new T[queue._array.Length];
            _head = queue._head;
            _tail = queue._tail;
            _count = queue._count;
        }
        #endregion

        #region Properties
        public int Count
        {
            get { return _count; }
        }
        #endregion 

        #region PublicMethods
        //큐 전체 삭제 메서드
        public void Clear()
        {
            int targetIndex = _head;
            for (int index = 0; index < _count; index++)
            {
                _array[targetIndex] = default!;
                targetIndex = (targetIndex + 1) % _array.Length;
            }
            _head = 0;
            _tail = 0;
            _count = 0;
        }

        //마지막 위치 해당 값 삽입 메서드
        public void Enqueue(T value)
        {
            if (_count == _array.Length)
            {
                Resize(_count + 1);
            }
            _array[_tail] = value;
            _tail = (_tail + 1) % _array.Length;
            _count++;
        }

        //처음 위치 값 출력 메서드
        public T Dequeue()
        {
            CheckEmptyQueue();
            T result = Dequeuing();
            return result;
        }

        //처음 위치 값 출력 가능 여부 메서드
        public bool TryDequeue(out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = Dequeuing();
            return true;
        }

        //처음 위치 값 확인 메서드
        public T Peek()
        {
            CheckEmptyQueue();
            return _array[_head];
        }

        //처음 위치 값 확인 가능 여부 메서드
        public bool TryPeek(out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = _array[_head];
            return true;
        }

        //해당 값 포함 여부 메서드
        public bool Contains(T item)
        {
            if (_count == 0)
            {
                return false;
            }
            int index = _head;
            for (int count = _count; count > 0; count--)
            {
                if (EqualityComparer<T>.Default.Equals(item, _array[index])) return true;
                index = (index + 1) % _array.Length;
            }
            return false;
        }

        //큐 배열 반환 메서드
        public T[] ToArray()
        {
            T[] newArray = new T[_count];
            int targetIndex = _head;
            for (int index = 0; index < _count; index++)
            {
                newArray[index] = _array[targetIndex];
                targetIndex = (targetIndex + 1) % _array.Length;
            }
            return newArray;
        }

        //큐 요소 프린트 메서드
        public void Print()
        {
            foreach (T item in _array)
            {
                if (EqualityComparer<T>.Default.Equals(item, default))
                {
                    Console.Write($"[null]");
                }
                else
                {
                    Console.Write($"[{item}]");
                }
            }
        }
        #endregion

        #region PrivateMethods
        //디큐 상세 메서드
        private T Dequeuing()
        {
            T value = _array[_head];
            _array[_head] = default!;
            _head++;
            _count--;
            return value;
        }

        //큐 크기 늘리기 메서드
        private void Resize(int targetCapacity)
        {
            int newCapacity = _array.Length;
            do
            {
                newCapacity *= 2;
                if (newCapacity > Array.MaxLength) newCapacity = Array.MaxLength;
            }
            while (newCapacity < targetCapacity);

            T[] newArray = new T[newCapacity];
            int index = _head;
            int newIndex = _head;
            for (int count = _array.Length; count > 0; count--)
            {
                newArray[newIndex] = _array[index];
                index = (index + 1) % _array.Length;
                newIndex++;
            }
            _tail = _head + _array.Length;
            _array = newArray;
        }

        //큐 여백 제거 메서드
        public T[] TrimToSize()
        {
            T[] newArray = new T[_count];
            int index = _head;
            int newIndex = 0;
            for (int count = _count; count > 0; count--)
            {
                newArray[newIndex] = _array[index];
                index = (index + 1) % _array.Length;
                newIndex++;
            }
            return newArray;
        }
        #endregion

        #region ExceptionMethods
        //빈큐 확인 메서드
        private void CheckEmptyQueue()
        {
            try
            {
                if (_count == 0) throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                {
                    Console.WriteLine("Queue is empty.");
                    Environment.Exit(-1);
                }
            }
        }
        #endregion
    }
}