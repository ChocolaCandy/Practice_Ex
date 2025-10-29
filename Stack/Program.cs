namespace Stack
{
    class Stack_Ex
    {
        static void Main(string[] args)
        {
            //StackByArray<int> arrayStack = new StackByArray<int>();
            //arrayStack.Push(12);
            //arrayStack.Push(345);
            //arrayStack.Push(6789);
            //arrayStack.State();

            StackByLinkedList<int> linkedListStack = new StackByLinkedList<int>();
            linkedListStack.Push(12);
            linkedListStack.Push(345);
            linkedListStack.Push(6789);
            linkedListStack.State();
        }
    }
}


