namespace CheckSyntaxBracketsError
{
    public class SyntaxReader
    {
        private char[] buffer;

        public SyntaxReader() 
        {
            buffer = Array.Empty<char>();
        }

        public SyntaxReader(string stringData)
        {
            if (stringData == null)
            {
                buffer = Array.Empty<char>();
                return;
            }
            buffer = DateProcessing(stringData);
        }

        //문자열 공백 문자 처리 메서드
        private static char[] DateProcessing(string stringData)
        {
            string stringNoSpace = string.Concat(stringData.Where(c => !Char.IsWhiteSpace(c)));
            return stringNoSpace.ToCharArray();
        }

        //문자열 입력 메서드
        public void Input(string inputStringData)
        {
            buffer = DateProcessing(inputStringData);
        }

        //괄호 비교 실행 메서드
        public void Run()
        {
            if (buffer == Array.Empty<char>())
            {
                Console.WriteLine("Buffer is empty. First input string syntex.");
                return;
            }
            Console.WriteLine("Check start.");
            Stack<char> stack = new Stack<char>();
            int dotCount = 1;
            foreach (char c in buffer)
            {
                dotCount = (dotCount + 1) % 6;
                Console.Write($"char {c} check{new string('.', dotCount)}");
                char popChar;
                switch (c)
                {
                    case '[':
                        stack.Push(c);
                        break;
                    case '{':
                        stack.Push(c);
                        break;
                    case '(':
                        stack.Push(c);
                        break;
                    case ']':
                        popChar = CheckPop();
                        CheckPair(popChar, '[');
                        break;
                    case '}':
                        popChar = CheckPop();
                        CheckPair(popChar, '{');
                        break;
                    case ')':
                        popChar = CheckPop();
                        CheckPair(popChar, '(');
                        break;
                }
                Console.WriteLine(" - No Error");
            }
            IsStackEmpty();
            Console.WriteLine("Perfect Syntax");
            Console.WriteLine("Check End.");

            //스택 팝 가능 여부 메서드
            char CheckPop()
            {
                bool result = stack.TryPop(out char popChar);
                try
                {
                    if (!result) throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine(" Buffer is Empty - Syntax Error");
                    Console.WriteLine("Check End.");
                    Environment.Exit(-1);
                }
                return popChar;
            }
            //괄호 짝 확인
            bool CheckPair(char popChar, char rightChar)
            {
                try
                {
                    if (popChar != rightChar) throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine($" {rightChar} : {popChar} mismatch - Syntax Error");
                    Console.WriteLine("Check End.");
                    Environment.Exit(-1);
                }
                return true;
            }
            //스택 비었는지 확인
            void IsStackEmpty()
            {
                try
                {
                    if (stack.Count != 0) throw new Exception();
                }
                catch (Exception)
                {
                    Console.WriteLine($" [{string.Join(", ", stack.ToArray())}] remain - Sytex Error.");
                    Console.WriteLine("Check End.");
                    Environment.Exit(-1);
                }
            }
        }
    }

    class CheckSyntaxBracketsError_Ex
    {
        static void Main(string[] args)
        {
            SyntaxReader syntexReader = new SyntaxReader();
            syntexReader.Input("({(Parenthesized syntax test)})");
            syntexReader.Run();
        }
    }
}
