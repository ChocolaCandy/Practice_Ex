namespace Graph
{
    class Program
    {
        static void Main(string[] args)
        {
            ArrayGraph arrayGraph = new ArrayGraph();
            arrayGraph.Insert(0, 1);
            arrayGraph.Insert(0, 2);
            arrayGraph.Insert(0, 4);
            arrayGraph.Insert(1, 2);
            arrayGraph.Insert(2, 3);
            arrayGraph.Insert(2, 4);
            arrayGraph.Insert(3, 4);
            arrayGraph.BFS(0); // 0, 1, 2, 4, 3
            //arrayGraph.DFS(0); //0, 1, 2, 3, 4
        }
    }
}
