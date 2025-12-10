namespace MST
{
    class Graph<T>()
    {
        //우선순위 부여 전역변수
        private static int PRIORITY = 0;
        public List<Node<T>> Nodes = new List<Node<T>>();

        //노드 삽입 메서드(값)
        public void Insert(T first, T second, int weight)
        {
            Node<T> newNodeFirst = new Node<T>(first);
            Node<T> newNodeSecond = new Node<T>(second);
            Insert(newNodeFirst, newNodeSecond, weight);
        }

        //노드 삽입 메서드(노드)
        public void Insert(Node<T> first, Node<T> second, int weight)
        {
            Node<T>? findFirstNode = Nodes.Find(v => EqualityComparer<T>.Default.Equals(v.Value, first.Value));
            Node<T>? findSecondNode = Nodes.Find(v => EqualityComparer<T>.Default.Equals(v.Value, second.Value));

            if (findFirstNode == null)
            {
                findFirstNode = first;
                findFirstNode.Priority = PRIORITY++;
                Nodes.Add(findFirstNode);
            }
            if (findSecondNode == null)
            {
                findSecondNode = second;
                findSecondNode.Priority = PRIORITY++;
                Nodes.Add(findSecondNode);
            }

            Edge<T>? findFirstEdge = findFirstNode.Edges.Find(n => n.SecondNode == findSecondNode);
            Edge<T>? findSecondEdge = findSecondNode.Edges.Find(n => n.FirstNode == findFirstNode);
            if (findFirstEdge == null || findSecondEdge == null)
            {
                findFirstNode.Edges.Add(new Edge<T>(findFirstNode, findSecondNode, weight));
                findSecondNode.Edges.Add(new Edge<T>(findSecondNode, findFirstNode, weight));
            }
            else
            {
                findFirstEdge.ChangeWeight(weight);
                findSecondEdge.ChangeWeight(weight);
            }
        }

        //그래프 출력 메서드
        public void PrintGraph()
        {
            string stringSlide = new string('-', 23);
            Console.WriteLine($"{stringSlide}Graph{stringSlide}");
            int nodeCount = 1;
            foreach (Node<T> node in Nodes)
            {
                Console.WriteLine($"[Node{nodeCount++}] Value : {node.Value} Priority : {node.Priority}");
                int edgeCount = 1;
                foreach (Edge<T> edge in node.Edges)
                {
                    Console.WriteLine($"[Edge{edgeCount++}] FirstNode : {edge.FirstNode.Value} SecondNode : {edge.SecondNode.Value} Weight : {edge.Weight}");
                }
                Console.WriteLine();
            }
            Console.WriteLine(new string('-', 51));
        }
    }
}
