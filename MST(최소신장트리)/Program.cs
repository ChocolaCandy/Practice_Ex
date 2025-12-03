using System.Xml.Linq;

namespace MST
{
    class Node<T>(T value)
    {
        private int _priority = 0;
        public T Value { get; } = value;
        public int Priority 
        {
            get { return _priority; }
            set
            {
                if (_priority == 0)
                {
                    _priority = value;
                }
            }
        }
        public List<Edge<T>> edges = new List<Edge<T>>();
    }

    class NoWeightEdge<T>(Node<T> first, Node<T> second)
    {
        public Node<T> FirstNode { get; } = first;
        public Node<T> SecondNode { get; } = second;
    }

    class Edge<T>(Node<T> first, Node<T> second, int weight) : IComparable<Edge<T>>
    {
        public int Weight { get; set; } = weight;
        public Node<T> FirstNode { get; } = first;
        public Node<T> SecondNode { get; } = second;

        public static bool operator <(Edge<T> left, Edge<T> right)
        {
            return left.Weight < right.Weight;
        }

        public static bool operator >(Edge<T> left, Edge<T> right)
        {
            return left.Weight > right.Weight;
        }

        public int CompareTo(Edge<T>? other)
        {
            if (other == null) { return 1; }
            return Weight.CompareTo(other.Weight);
        }
    }

    class Graph<T>()
    {
        private static int PRIORITY = 0;
        public List<Node<T>> Nodes = new List<Node<T>>();

        public void Insert(T first, T second, int weight)
        {
            Node<T> newNodeFirst = new Node<T>(first);
            Node<T> newNodeSecond = new Node<T>(second);
            Insert(newNodeFirst, newNodeSecond, weight);
        }

        public void Insert(Node<T> first, Node<T> second, int weight)
        {
            Node<T>? findFirstNode = Nodes.Find(v => EqualityComparer<T>.Default.Equals(v.Value, first.Value));
            Node<T>? findSecondNode = Nodes.Find(v => EqualityComparer<T>.Default.Equals(v.Value, second.Value));

            if(findFirstNode == null)
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

            Edge<T>? findEdge = findFirstNode.edges.Find(n => n.SecondNode == findSecondNode);
            if (findEdge == null)
            {
                findFirstNode.edges.Add(new Edge<T>(findFirstNode, findSecondNode, weight));
            }
            else
            {
                findEdge.Weight = weight;
            }
        }

        public void PrintGraph()
        {
            string stringSlide = new string('-', 23);
            Console.WriteLine($"{stringSlide}Graph{stringSlide}");
            int nodeCount = 1;
            foreach (Node<T> node in Nodes)
            {
                Console.WriteLine($"[Node{nodeCount++}] Value : {node.Value} Priority : {node.Priority}");
                int edgeCount = 1;
                foreach (Edge<T> edge in node.edges)
                { 
                    Console.WriteLine($"[Edge{edgeCount++}] FirstNode : {edge.FirstNode.Value} SecondNode : {edge.SecondNode.Value} Weight : {edge.Weight}");
                    if (edge == node.edges.Last()) { Console.WriteLine(); }
                }
            }
            Console.WriteLine(new string('-', 51));
        }
    }

    class Kruskal_Algorithm<T>(Graph<T> graph)
    {
        private readonly List<Edge<T>> _edges = new List<Edge<T>>();
        private readonly List<(Node<T>, Node<T>)> _parent = new List<(Node<T>, Node<T>)>();
        private readonly List<NoWeightEdge<T>> _result = new List<NoWeightEdge<T>>();

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

        private void OrganizeField()
        {
            foreach (Node<T> node in graph.Nodes)
            {
                foreach (Edge<T> edge in node.edges)
                {
                    _edges.Add(edge);
                }
                _parent.Add((node, node));
            }
        }

        private void SortEdge()
        {
            _edges.Sort();
        }

        private void Union(Node<T> first, Node<T> second)
        {
            Node<T> parentFirst = Find(first);
            Node<T> parentSecond = Find(second);

            if(parentFirst == parentSecond) { return; }

            _result.Add(new NoWeightEdge<T>(first, second));

            if(parentFirst.Priority < parentSecond.Priority)
            {
                var secondParent = _parent.Find(n => n.Item1 == parentSecond);
                int index = _parent.IndexOf(secondParent);
                _parent[index] = (parentSecond, parentFirst);
            }
            else
            {
                var firstParent = _parent.Find(n => n.Item1 == parentFirst);
                int index = _parent.IndexOf(firstParent);
                _parent[index] = (parentFirst, parentSecond);
            }
        }

        private Node<T> Find(Node<T> node)
        {
            Node<T> input;
            Node<T> result = node;
            do
            {
                input = result;
                result = GetParent(input);
            }while (input != result);
            return result;
        }

        private Node<T> GetParent(Node<T> node)
        {
            return _parent.Find(n => n.Item1 == node).Item2;
        }

        public void PrintResult()
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
    }

    class Program
    {
        static void Main(string[] args)
        {
            Graph<char> graph = new Graph<char>();
            graph.Insert('a', 'b', 29);
            graph.Insert('a', 'f', 10);
            graph.Insert('b', 'c', 16);
            graph.Insert('b', 'g', 15);
            graph.Insert('c', 'd', 12);
            graph.Insert('d', 'e', 22);
            graph.Insert('d', 'g', 18);
            graph.Insert('e', 'f', 27);
            graph.Insert('e', 'g', 25);
            graph.PrintGraph();

            Kruskal_Algorithm<char> alg = new Kruskal_Algorithm<char>(graph);
            alg.Run();
        }
    }
}
