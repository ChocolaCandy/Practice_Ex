namespace MST
{
    class Node<T>(T value)
    {
        #region Fields
        //우선순위
        private int _priority = 0;
        //값
        public T Value { get; } = value;
        //간선
        public List<Edge<T>> Edges = new List<Edge<T>>();
        #endregion

        #region Properties
        public int Priority
        {
            get { return _priority; }
            set { if (_priority == 0) { _priority = value; } }
        }
        #endregion
    }

    class Edge<T>(Node<T> first, Node<T> second, int weight) : IComparable<Edge<T>>
    {
        #region Fields
        //가중치
        public int Weight { get; private set; } = weight;
        //시작 노드
        public Node<T> FirstNode { get; } = first;
        //끝 노드
        public Node<T> SecondNode { get; } = second;
        #endregion

        #region Public Methods
        public void ChangeWeight(int weight)
        {
            Weight = weight;
        }
        #endregion

        #region Operator Overload
        public static bool operator <(Edge<T> left, Edge<T> right)
        {
            return left.Weight < right.Weight;
        }

        public static bool operator >(Edge<T> left, Edge<T> right)
        {
            return left.Weight > right.Weight;
        }
        #endregion

        #region Method Override
        public int CompareTo(Edge<T>? other)
        {
            if (other == null) { return 1; }
            return Weight.CompareTo(other.Weight);
        }
        #endregion
    }

    class NoWeightEdge<T>(Node<T> first, Node<T> second)
    {
        #region Fields
        //시작 노드
        public Node<T> FirstNode { get; } = first;
        //끝 노드
        public Node<T> SecondNode { get; } = second;
        #endregion
    }
}
