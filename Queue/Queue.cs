namespace Queue
{
    class Queue<T>
    {
        #region Fields
        //기본 큐(원형 큐) 크기
        private readonly int defaultSize = 4;
        //큐(원형 큐) 배열
        private T[] _array;
        //큐(원형 큐) 헤드
        private int _front;
        //큐(원형 큐) 테일
        private int _rear;
        //큐(원형 큐) 요소 수
        private int _count;
        #endregion

        #region Constructors
        public Queue()
        {
            _array = new T[defaultSize];
            _front = 0;
            _rear = 0;
            _count = 0;
        }
        public Queue(int size)
        {
            _array = new T[size];
            _front = 0;
            _rear = 0;
            _count = 0;
        }
        public Queue(Queue<T> queue)
        {
            _array = new T[queue._array.Length];
            _front = queue._front;
            _rear = queue._rear;
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
        //큐 요소 전체 삭제 메서드
        public void Clear()
        {
            int targetIndex = _front;
            for (int index = 0; index < _count; index++)
            {
                _array[targetIndex] = default!;
                targetIndex = (targetIndex + 1) % _array.Length;
            }
            _front = 0;
            _rear = 0;
            _count = 0;
        }

        //해당 요소 포함 여부 메서드
        public bool Contains(T item)
        {
            if (_count == 0)
            {
                return false;
            }
            int index = _front;
            for (int count = _count; count > 0; count--)
            {
                if (EqualityComparer<T>.Default.Equals(item, _array[index])) return true;
                index = (index + 1) % _array.Length;
            }
            return false;
        }

        //처음 위치 요소 출력 메서드
        public T Dequeue()
        {
            CheckEmptyQueue();
            T result = Dequeuing();
            return result;
        }

        //마지막 위치 해당 요소 삽입 메서드
        public void Enqueue(T item)
        {
            if (_count == _array.Length)
            {
                Resize(_count + 1);
            }
            _array[_rear] = item;
            _rear = (_rear + 1) % _array.Length;
            _count++;
        }

        //처음 위치 요소 확인 메서드
        public T Peek()
        {
            CheckEmptyQueue();
            return _array[_front];
        }

        //큐(원형 큐) 요소 프린트 메서드

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

        //큐(원형 큐) 배열 반환 메서드
        public T[] ToArray()
        {
            T[] newArray = new T[_count];
            int targetIndex = _front;
            for (int index = 0; index < _count; index++)
            {
                newArray[index] = _array[targetIndex];
                targetIndex = (targetIndex + 1) % _array.Length;
            }
            return newArray;
        }

        //큐(원형 큐) 여백 제거 메서드
        public T[] TrimToSize()
        {
            T[] newArray = new T[_count];
            int index = _front;
            int newIndex = 0;
            for (int count = _count; count > 0; count--)
            {
                newArray[newIndex] = _array[index];
                index = (index + 1) % _array.Length;
                newIndex++;
            }
            return newArray;
        }

        //처음 위치 요소 출력 가능 여부 메서드(참조 인자 출력)
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

        //처음 위치 요소 확인 가능 여부 메서드(참조 인자 출력)
        public bool TryPeek(out T result)
        {
            if (_count == 0)
            {
                result = default!;
                return false;
            }
            result = _array[_front];
            return true;
        }
        #endregion

        #region PrivateMethods
        //요소 출력 메서드
        private T Dequeuing()
        {
            T item = _array[_front];
            _array[_front] = default!;
            _front++;
            _count--;
            return item;
        }

        //큐(원형 큐) 배열 크기 조정 메서드
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
            int index = _front;
            int newIndex = _front;
            for (int count = _array.Length; count > 0; count--)
            {
                newArray[newIndex] = _array[index];
                index = (index + 1) % _array.Length;
                newIndex++;
            }
            _rear = _front + _array.Length;
            _array = newArray;
        }
        #endregion

        #region ExceptionMethods
        //빈 큐(원형 큐) 확인 메서드
        private void CheckEmptyQueue()
        {
            try
            {
                if (_count == 0) throw new InvalidOperationException();
            }
            catch (InvalidOperationException)
            {
                Console.WriteLine("Queue is empty.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }
}