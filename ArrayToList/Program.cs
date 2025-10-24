namespace ArrayToList
{
    public class List<T>
    {
        #region Fields
        //리스트 크기 기본값
        private const int default_Capacity = 4;
        //리스트 배열
        private T[] _array;
        //리스트 요소 수
        private int _count;
        #endregion

        #region Properties
        //리스트 크기
        public int Capacity
        {
            get { return _array.Length; }
            set {
                CheckThrowIfLessThan(value, _count, false);
                if (value != _array.Length)
                {
                    List<T>.CheckThrowIfNegative(value);
                    if (value == 0)
                        _array = Array.Empty<T>();
                    else
                    {
                        T[] newArray = new T[value];
                        if (_count > 0)
                        {
                            Array.Copy(_array, newArray, _count);
                        }
                        _array = newArray;
                    }
                }
            }
        }

        //리스트 요소 수
        public int Count
        {
            get { return _count; }
        }
        #endregion

        #region Constructors
        public List() 
        {
            _array = new T[default_Capacity];
        }

        public List(int capacity)
        {
            List<T>.CheckThrowIfNegative(capacity);
            _array = new T[capacity];
        }

        public List(List<T> list)
        {
            List<T>.CheckThrowIfNull(list);
            int capacity = list._array.Length;
            int count = list.Count;
            if(count == 0)
                _array = Array.Empty<T>();
            else
            {
                _array = new T[capacity];
                for (int i = 0; i < count; i++)
                {
                    _array[i] = list._array[i];
                }
                _count = count;
            }
        }
        #endregion

        #region Indexer
        //정수 인덱스를 사용하는 인덱서
        public T this[int index]
        {
            get
            {
                CheckOutOfRange(index, true);
                return _array[index];
            }
            set
            {
                CheckOutOfRange(index, true);
                _array[index] = value;
            }
        }
        #endregion

        #region Methods
        //마지막 위치 해당 요소 추가 메서드
        public void Add(T element)
        {
            int count = _count;

            if (count < _array.Length)
            {
                AddElement();
            }
            else
            {
                Resize(count + 1);
                AddElement();
            }

            //요소 추가
            void AddElement()
            {
                _count = count + 1;
                _array[count] = element;
            }
        }

        //해당 위치 해당 요소 추가 메서드
        public void Insert(int index, T element)
        {
            CheckOutOfRange(index, false);
            int count = _count;
            if (count < _array.Length)
            {
                InsertElement();
            }
            else
            {
                Resize(count + 1);
                InsertElement();
            }
            void InsertElement()
            {
                _count = count + 1;
                //for문과 동일한 기능 수행(더욱 최적화 된 방법)
                //이번 코드는 가독성을 위해 for문 사용
                //Array.Copy(_array, index, _array, index + 1, count - index);
                for (int i = count; i > index; i--)
                {
                    _array[i] = _array[i - 1];
                }
                _array[index] = element;
            }
        }

        //전체 역순 뒤집기 메서드
        public void Reverse() => Reverse(0, _count);

        //해당 위치 해당 길이 역순 뒤집기 메서드
        public void Reverse(int index, int length)
        {
            CheckOutOfRange(index, true);
            CheckOutOfRange(length, false);
            CheckThrowIfGreater(length, _count - index, false);
            Array.Reverse(_array, index, length);
        }

        //해당 요소 포함 여부 확인 메서드
        public bool Contains(T element)
        {
            return IndexOf(element) != -1;
        }

        //전체 요소 삭제 메서드
        public void Clear()
        {
            if (_count > 0)
            {
                _count = 0;
                Array.Clear(_array);
            }
        }
        
        //해당 요소 인덱스 찾기 메서드
        public int IndexOf(T element) => Array.IndexOf(_array, element, 0, _count);

        //마지막 위치 요소 삭제 메서드
        public void Remove()
        {
            CheckThrowIfLessThan(_count, 0, true);
            _count--;
            Array.Clear(_array, _count, 1);
        }

        //해당 위치 요소 삭제 메서드
        public void RemoveAt(int index)
        {
            CheckThrowIfLessThan(_count, 0, true);
            _count--;
            //for문과 동일한 기능 수행(더욱 최적화 된 방법)
            //이번 코드는 가독성을 위해 for문 사용
            //Array.Copy(_array, index + 1, _array, index, _count - index);
            for (int i = index; i < _count; i++)
            {
                _array[i] = _array[i + 1];
            }
            Array.Clear(_array, _count, 1);
        }

        //리스트 요소들 오름차순 정렬
        public void Sort() => Array.Sort(_array, 0, _count);

        //리스트 요소들 배열 반환
        public T[] ToArray()
        {
            T[] newArray = new T[_count];
            Array.Copy(_array, newArray, _count);
            return newArray;
        }

        //자르기
        public List<T> Slice(int index, int length)
        {
            CheckOutOfRange(index, true);
            CheckOutOfRange(length, false);
            CheckThrowIfGreater(length, _count - index, false);

            List<T> sliceList = new List<T>(length);
            Array.Copy(_array, index, sliceList._array, 0, length);
            sliceList._count = length;
            return sliceList;
        }

        //리스트 크기 조정
        private void Resize(int capacity)
        {
            int newCapacity = _array.Length == 0 ? default_Capacity : _array.Length * 2;
            if (newCapacity > Array.MaxLength) { newCapacity = Array.MaxLength; }
            while (newCapacity < capacity) { newCapacity *= 2; }
            Capacity = newCapacity;
        }
        #endregion

        #region CheckException
        private void CheckOutOfRange(int value, bool equal = false)
        {
            List<T>.CheckThrowIfNegative(value);
            if (equal) List<T>.CheckThrowIfGreater(value, _count, true);
            else List<T>.CheckThrowIfGreater(value, _count, false);
        } 
        private static void CheckThrowIfNegative(int value)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(value);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(ExceptionMessage.OutofRangeMessage);
                Environment.Exit(-1);
            }
        }
        private static void CheckThrowIfLessThan(int value, int other, bool equal = false)
        {
            try
            {
                if (equal) ArgumentOutOfRangeException.ThrowIfLessThanOrEqual(value, other);
                else ArgumentOutOfRangeException.ThrowIfLessThan(value, other);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(ExceptionMessage.OutofRangeMessage);
                Environment.Exit(-1);
            }
        }
        private static void CheckThrowIfGreater(int value, int other, bool equal = false)
        {
            try
            {
                if (equal) ArgumentOutOfRangeException.ThrowIfGreaterThanOrEqual(value, other);
                else ArgumentOutOfRangeException.ThrowIfGreaterThan(value, other);
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine(ExceptionMessage.OutofRangeMessage);
                Environment.Exit(-1);
            }
        }
        private static void CheckThrowIfNull(List<T> argument)
        {
            try
            {
                ArgumentNullException.ThrowIfNull(argument);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine(ExceptionMessage.NullMessage);
                Environment.Exit(-1);
            }
        }
        #endregion
    }

    class ArrayToList_Ex
    {
        static void Main(string[] args)
        {
            List<int> list = new List<int>();
            list.Add(0);
            Console.WriteLine(list[0]);
        }
    }
}
