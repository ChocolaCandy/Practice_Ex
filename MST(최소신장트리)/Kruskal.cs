namespace MST
{
    class Kruskal_Algorithm<T>(Graph<T> graph)
    {
        #region Fields
        private readonly List<Edge<T>> _edges = new List<Edge<T>>();
        private readonly List<(Node<T>, Node<T>)> _parent = new List<(Node<T>, Node<T>)>();
        private readonly List<NoWeightEdge<T>> _result = new List<NoWeightEdge<T>>();
        #endregion

        #region Public Methods
        //크루스칼 알고리즘 메서드
        public void Run()
        {
            OrganizeField();
            SortEdge();

            foreach (Edge<T> edge in _edges)
            {
                Node<T> first = edge.FirstNode;
                Node<T> second = edge.SecondNode;

                Union(first, second);
            }

            PrintResult();
        }
        #endregion

        #region Private Methods
        //Find-Union 알고리즘 (Find)
        private Node<T> Find(Node<T> node)
        {
            Node<T> targetNode;
            Node<T> result = node;
            do
            {
                targetNode = result;
                result = GetParent(targetNode);
            } while (targetNode != result);
            return result;
        }

        //부모 노드 반환 메서드
        private Node<T> GetParent(Node<T> node)
        {
            return _parent.Find(n => n.Item1 == node).Item2;
        }

        //필드 리스트 초기화 메서드
        private void OrganizeField()
        {
            foreach (Node<T> node in graph.Nodes)
            {
                foreach (Edge<T> edge in node.Edges)
                {
                    if (_edges.Exists(n => edge.FirstNode == n.SecondNode && edge.SecondNode == n.FirstNode)) { continue; }
                    _edges.Add(edge);
                }
                _parent.Add((node, node));
            }
        }

        //결과 출력 메서드
        private void PrintResult()
        {
            int edgeCount = 1;
            string stringSlide = new string('-', 19);
            Console.WriteLine($"{stringSlide}KruskalResult{stringSlide}");
            foreach (var resultEdge in _result)
            {
                Console.WriteLine($"[Edge{edgeCount++}] FirstNode : {resultEdge.FirstNode.Value} SecondNode : {resultEdge.SecondNode.Value}");
            }
            Console.WriteLine(new string('-', 51));
        }

        //간선 가중치순 정렬 메서드
        private void SortEdge()
        {
            _edges.Sort();
        }

        //Find-Union 알고리즘 (Union)
        private void Union(Node<T> first, Node<T> second)
        {
            Node<T> parentFirst = Find(first);
            Node<T> parentSecond = Find(second);

            if (parentFirst == parentSecond) { return; }

            _result.Add(new NoWeightEdge<T>(first, second));

            if (parentFirst.Priority < parentSecond.Priority)
            {
                List<(Node<T>, Node<T>)> childs = _parent.FindAll(n => n.Item2 == parentSecond);
                foreach ((Node<T>, Node<T>) child in childs)
                {
                    int index = _parent.IndexOf(child);
                    _parent[index] = (child.Item1, parentFirst);
                }
            }
            else
            {
                List<(Node<T>, Node<T>)> childs = _parent.FindAll(n => n.Item2 == parentFirst);
                foreach ((Node<T>, Node<T>) child in childs)
                {
                    int index = _parent.IndexOf(child);
                    _parent[index] = (child.Item1, parentSecond);
                }
            }
        }
        #endregion
    }
}
