namespace MST
{
    class Prim_Algorithm<T>(Graph<T> graph)
    {
        private readonly PriorityQueue<Edge<T>, int> _edges = new PriorityQueue<Edge<T>, int>();
        private readonly List<Node<T>> _visited = new List<Node<T>>();
        private readonly List<NoWeightEdge<T>> _result = new List<NoWeightEdge<T>>();

        //프림 알고리즘 메서드
        public void Run()
        {
            SetStartEdge();
            while (_edges.TryDequeue(out Edge<T>? edge1, out _))
            {
                if(edge1 ==  null) { break; }
                if (!IsVisited(edge1.SecondNode))
                {
                    foreach (Edge<T> nextEdge in edge1.SecondNode.Edges)
                    {
                        if (IsVisited(nextEdge.SecondNode)) { continue; }
                        _edges.Enqueue(nextEdge, nextEdge.Weight);
                    }
                    _result.Add(new NoWeightEdge<T>(edge1.FirstNode, edge1.SecondNode));
                    _visited.Add(edge1.SecondNode);
                }
            }

            PrintResult();
        }

        //노드 방문 여부 메서드
        private bool IsVisited(Node<T> node)
        {
            return _visited.Contains(node);
        }

        //결과 출력 메서드
        private void PrintResult()
        {
            int edgeCount = 1;
            string stringSlide = new string('-', 20);
            Console.WriteLine($"{stringSlide}Prim_Result{stringSlide}");
            foreach (var resultEdge in _result)
            {
                Console.WriteLine($"[Edge{edgeCount++}] FirstNode : {resultEdge.FirstNode.Value} SecondNode : {resultEdge.SecondNode.Value}");
            }
            Console.WriteLine(new string('-', 51));
        }

        //최소 가중치 간선 시작 설정 메서드
        private void SetStartEdge()
        {
            List<Edge<T>> sortEdge = new List<Edge<T>>();
            foreach (Node<T> node in graph.Nodes)
            {
                foreach (Edge<T> edge in node.Edges)
                {
                    sortEdge.Add(edge);
                }
            }
            sortEdge.Sort();

            foreach (Edge<T> startNodeEdge in sortEdge[0].FirstNode.Edges)
            {
                _edges.Enqueue(startNodeEdge, startNodeEdge.Weight);
            }

            _visited.Add(sortEdge[0].FirstNode);
        }
    }
}

