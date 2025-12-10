namespace MST
{
    class Program
    {
        static void Main(string[] args)
        {
            //Graph<char> graph = new Graph<char>();
            //graph.Insert('a', 'b', 29);
            //graph.Insert('a', 'f', 10);
            //graph.Insert('b', 'c', 16);
            //graph.Insert('b', 'g', 15);
            //graph.Insert('c', 'd', 12);
            //graph.Insert('d', 'e', 22);
            //graph.Insert('d', 'g', 18);
            //graph.Insert('e', 'f', 27);
            //graph.Insert('e', 'g', 25);
            //graph.PrintGraph();

            //Kruskal_Algorithm<char> kruskal = new Kruskal_Algorithm<char>(graph);
            //kruskal.Run();

            //Prim_Algorithm<char> prim = new Prim_Algorithm<char>(graph);
            //prim.Run();

            Graph<int> graph1 = new Graph<int>();
            graph1.Insert(0, 1, 10);
            graph1.Insert(0, 3, 6);
            graph1.Insert(0, 7, 1);
            graph1.Insert(1, 2, 4);
            graph1.Insert(1, 5, 2);
            graph1.Insert(2, 3, 11);
            graph1.Insert(2, 5, 7);
            graph1.Insert(3, 7, 3);
            graph1.Insert(4, 5, 5);
            graph1.Insert(4, 7, 8);
            graph1.Insert(5, 6, 9);
            graph1.Insert(6, 7, 12);

            graph1.PrintGraph();

            Kruskal_Algorithm<int> kruskal = new Kruskal_Algorithm<int>(graph1);
            kruskal.Run();

            Prim_Algorithm<int> prim = new Prim_Algorithm<int>(graph1);
            prim.Run();
        }
    }
}
