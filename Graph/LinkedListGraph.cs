namespace Graph
{
    class GraphNode<T>(T item) where T : notnull
    {
        #region Fields
        public T Value { get; } = item;
        public LinkedList<GraphNode<T>> NextNode { get; } = new LinkedList<GraphNode<T>>();
        #endregion
    }


    class LinkedListGraph<T> where T : notnull
    {
        #region Fields 
        private readonly List<GraphNode<T>> _graph;
        #endregion

        #region constructors
        public LinkedListGraph()
        {
            _graph = new List<GraphNode<T>>();
        }

        public LinkedListGraph(LinkedListGraph<T> graph)
        {
            _graph = new List<GraphNode<T>>(graph._graph);
        }
        #endregion

        #region PublicMethods
        //그래프 정점-정점 간선 추가 메서드
        public void Insert(T from, T to)
        {
            ArgumentEqualException("Insert", from, to);

            if (Contains(from, to)) { return; }

            GraphNode<T>? fromNode = _graph.Find(value => EqualityComparer<T>.Default.Equals(value.Value, from));
            GraphNode<T>? toNode = _graph.Find(value => EqualityComparer<T>.Default.Equals(value.Value, to));

            if (fromNode == null)
            {
                fromNode = new GraphNode<T>(from);
                _graph.Add(fromNode);
            }

            if (toNode == null)
            {
                toNode = new GraphNode<T>(to);
                _graph.Add(toNode);
            }

            fromNode.NextNode.AddLast(toNode);
            toNode.NextNode.AddLast(fromNode);
        }

        //그래프 정점-정점 간선 제거 메서드
        public void Delete(T from, T to)
        {
            ArgumentEqualException("Delete", from, to);

            if (!Contains(from, to)) { return; }

            GraphNode<T> fromNode = _graph.Find(value => EqualityComparer<T>.Default.Equals(value.Value, from))!;
            GraphNode<T> toNode = _graph.Find(value => EqualityComparer<T>.Default.Equals(value.Value, to))!;

            fromNode.NextNode.Remove(toNode);
            toNode.NextNode.Remove(fromNode);

            if(fromNode.NextNode.Count == 0) {  _graph.Remove(fromNode); }
            if(toNode.NextNode.Count == 0) {  _graph.Remove(toNode); }
        }

        //그래픽 정점-정점 간선 포함 여부 메서드
        public bool Contains(T from, T to)
        {
            GraphNode<T>? fromNode = _graph.Find(value => EqualityComparer<T>.Default.Equals(value.Value, from));

            if (fromNode == null) { return false; }

            foreach (GraphNode<T> toNode in fromNode.NextNode)
            {
                if(EqualityComparer<T>.Default.Equals(toNode.Value, to)) { return true; }
            }

            return false;
        }

        //그래프 너비 우선 탐색 경로 출력 메서드
        public void BFS(T value)
        {
            GraphNode<T>? startNode = _graph.Find(v => EqualityComparer<T>.Default.Equals(v.Value, value));
            if(startNode == null) { return; }

            Queue<GraphNode<T>> queue = new Queue<GraphNode<T>>();
            List<T> visited = new List<T>();

            queue.Enqueue(startNode);

            while (queue.TryDequeue(out GraphNode<T>? visit))
            {
                visited.Add(visit.Value);
                foreach (GraphNode<T> nextNode in visit.NextNode)
                {
                    if (queue.Contains(nextNode) || visited.Contains(nextNode.Value)) { continue; }
                    queue.Enqueue(nextNode);
                }
            }

            Console.WriteLine(string.Join(", ", visited));
        }

        //그래프 깊이 우선 탐색 경로 출력 메서드
        public void DFS(T value)
        {
            GraphNode<T>? startNode = _graph.Find(v => EqualityComparer<T>.Default.Equals(v.Value, value));
            if (startNode == null) { return; }

            List<T> visited = new List<T>();
            _DFS(startNode);
            Console.WriteLine(string.Join(", ", visited));

            void _DFS(GraphNode<T> visit)
            {
                visited.Add(visit.Value);

                foreach (GraphNode<T> nextNode in visit.NextNode)
                {
                    if (visited.Contains(nextNode.Value)) { continue; }
                    _DFS(nextNode);
                }
            }
        }

        //그래프 출력 메서드
        public void Print()
        {
            foreach(GraphNode<T> node in _graph)
            {
                Console.WriteLine(node.Value);
            }
        }
        #endregion

        #region ExceptionMethods
        //자기 루프 간선 예외 처리
        public static void ArgumentEqualException(string MethodName, T first, T second)
        {
            try
            {
                if (EqualityComparer<T>.Default.Equals(first, second)) { throw new ArgumentException(string.Empty); }
            }
            catch (ArgumentException)
            {
                Console.WriteLine($"[Error]{MethodName} argument first and second cannot be equal.");
                Environment.Exit(-1);
            }
        }
        #endregion
    }
}