namespace Graph
{
    class ArrayGraph
    {
        #region Fields 
        private bool[,] _graph;
        #endregion

        #region constructors
        public ArrayGraph()
        {
            _graph = new bool[0, 0];
        }

        public ArrayGraph(int vertexCount)
        {
            _graph = new bool[vertexCount, vertexCount];
        }

        public ArrayGraph(ArrayGraph graph)
        {
            _graph = (bool[,])graph._graph.Clone();
        }
        #endregion

        #region PublicMethods
        //그래프 정점-정점 간선 추가 메서드
        public void Insert(int start, int end)
        {
            ArgumentNegativeException("Insert", start, end);
            ArgumentEqualException("Insert", start, end);

            if (Contains(start, end)) { return; }

            if (IsOutOfSize(start, end)) { GraphResize(int.Max(start, end)); }

            _graph[start, end] = true;
            _graph[end, start] = true;
        }

        //그래프 정점-정점 간선 제거 메서드
        public void Delete(int start, int end)
        {
            ArgumentNegativeException("Delete", start, end);
            ArgumentEqualException("Delete", start, end);

            if (!Contains(start, end)) { return; }

            _graph[start, end] = false;
            _graph[end, start] = false;

            int deleteVertexNum = int.Max(start, end);
            int lastVertexNum = _graph.GetLength(0) - 1;
            if (deleteVertexNum == lastVertexNum)
            {
                for (int vertexNum = 0; vertexNum < _graph.GetLength(0); vertexNum++)
                {
                    if (_graph[deleteVertexNum, vertexNum] == true) { return;}
                }
                DeleteVertex(lastVertexNum);
            }
        }

        //그래픽 정점-정점 간선 포함 여부 메서드
        public bool Contains(int start, int end)
        {
            if (IsOutOfSize(start, end)) { return false; }
            return _graph[start, end];
        }

        //그래프 너비 우선 탐색 경로 출력 메서드
        public void BFS(int num)
        {
            if (IsOutOfSize(num)) return;

            Queue<int> queue = new Queue<int>();
            List<int> visited = new List<int>();
            
            queue.Enqueue(num);
            while (queue.TryDequeue(out int visit))
            {
                visited.Add(visit);
                for(int vertex = 0; vertex < _graph.GetLength(0); vertex++)
                {
                    if (_graph[visit, vertex] == true) 
                    {
                        if (queue.Contains(vertex) || visited.Contains(vertex)) { continue; }
                        queue.Enqueue(vertex);
                    }
                }
            }
            Console.WriteLine(string.Join(", ", visited));
        }

        //그래프 깊이 우선 탐색 경로 출력 메서드
        public void DFS(int num)
        {
            if (IsOutOfSize(num)) return;

            List<int> visited = new List<int>();
            _DFS(num);
            Console.WriteLine(string.Join(", ", visited));

            void _DFS(int visit)
            {
                visited.Add(visit);

                for (int vertex = 0; vertex < _graph.GetLength(0); vertex++)
                {
                    if (_graph[visit, vertex] == true)
                    {
                        if (visited.Contains(vertex)) { continue; }
                        _DFS(vertex);
                    }
                }
            }
        }

        //그래프 출력 메서드
        public void Print()
        {
            int length = _graph.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    Console.WriteLine($"[{i},{j}] : {_graph[i, j]}");
                }
            }
        }
        #endregion

        #region PrivateMethods
        //정점 제거 메서드
        private void DeleteVertex(int length)
        {
            if (length == 1)
            {
                _graph = new bool[0, 0];
                return;
            }

            bool[,] newGraph = new bool[length, length];

            for (int i = 0; i < length; i++)
            {
                for (int j = 0; j < length; j++)
                {
                    newGraph[i, j] = _graph[i, j];
                }
            }
            _graph = newGraph;
        }

        //그래프 크기 조정 메서드
        private void GraphResize(int vertex)
        {
            int length = vertex + 1;

            if (length >= Array.MaxLength)
            {
                if (_graph.GetLength(0) == Array.MaxLength) { return; }
                length = Array.MaxLength;
            }

            if (length < _graph.GetLength(0))
            {
                Console.WriteLine("The graph length cannot be less than count of vertices.");
                return;
            }

            if (length == _graph.GetLength(0))
            {
                Console.WriteLine($"The graph length is already {length}.");
                return;
            }

            bool[,] newGraph = new bool[length, length];
            CopyArray(_graph, newGraph);
            _graph = newGraph;
        }
        
        //그래프 복사 메서드
        private static void CopyArray(bool[,] sourceArray, bool[,] destinationArray)
        {
            int length = sourceArray.GetLength(0);
            for (int i = 0; i < length; i++)
            {
                for(int j = 0; j < length; j++)
                {
                    destinationArray[i,j] = sourceArray[i,j];
                }
            }
        }

        //정점 범위 초과 여부 메서드(파라미터 1개)
        private bool IsOutOfSize(int num)
        {
            return (num >= _graph.GetLength(0));
        }

        //정점 범위 초과 여부 메서드(파라미터 2개)
        private bool IsOutOfSize(int start, int end)
        {
            return (int.Max(start, end) >= _graph.GetLength(0));
        }
        #endregion

        #region ExceptionMethods
        //정점 음수 예외 처리
        public static void ArgumentNegativeException(string MethodName, params int[] arguments)
        {
            try
            {
                foreach (int arg in arguments)
                {
                    ArgumentOutOfRangeException.ThrowIfNegative(arg);
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Console.WriteLine($"[Error]{MethodName} argument cannot be Negative.");
                Environment.Exit(-1);
            }
        }

        //자기 루프 간선 예외 처리
        public static void ArgumentEqualException(string MethodName, int first, int second)
        {
            try
            {
                if (int.Equals(first, second)) { throw new ArgumentException(string.Empty); }
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