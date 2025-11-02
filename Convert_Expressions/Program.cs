namespace Convert_Expressions
{
    static class Debug
    {
        public static bool IsDebugMode = false;
    }

    enum ExpressionType
    {
        Null,
        Prefix,
        Infix,
        Postfix
    }

    class Convert_Expressions
    {
        #region Fields
        //표현식
        private string expression;
        //표현식 표기법 타입
        private ExpressionType type;
        #endregion

        #region Properties
        public string Expression
        {
            get { return expression; }
        }
        public ExpressionType Type
        {
            get { return type; }
        }
        #endregion

        #region Constructors
        public Convert_Expressions()
        {
            expression = string.Empty;
            type = ExpressionType.Null;
        }
        public Convert_Expressions(string expression) : this()
        {
            CheckExpression(expression, "Constructor");
        }
        #endregion

        #region Methods
        //표현식 입력 메서드
        public void InputExpression(string expression)
        {
            CheckExpression(expression, "InputData");
        }

        //표현식 표기법 타입 여부 메서드
        private void CheckExpression(string expression, string methodName)
        {
            if (!SelectExpression(expression, methodName))
                Console.WriteLine($"'{expression}' not an expression.");
        }

        //표현식 표기법 타입 체크 메서드
        private bool SelectExpression(string expression, string methodName)
        {
            ThrowNullorEmptyStringException(expression, methodName);
            this.expression = expression;
            if (expression.Length == 1)
                return false;
            char[] processedChar = Processing(expression);
            if (IsPrefix(processedChar)) return true;
            if (IsInfix(processedChar)) return true;
            if (IsPostfix(processedChar)) return true;
            return false;
        }
        
        //표현식 표기법 타입 비교 메서드
        private bool CheckType(ExpressionType type)
        {
            if (this.type != type)
            {
                Console.WriteLine($"The current data expression type is {this.type}.");
                Console.WriteLine($"Use this method when data expression type is {type}");
                return false;
            }
            return true;
        }


        #region IsTypeMethods
        //전위 표기법 타입 여부 메서드
        private bool IsPrefix(char[] charArray)
        {
            return IsPrePostfix(charArray, ExpressionType.Prefix);
        }

        //중위 표기법 타입 여부 메서드
        private bool IsInfix(char[] charArray)
        {
            Stack<char> stack = new Stack<char>();
            char[] newArray = new char[charArray.Length];
            Array.Copy(charArray, newArray, charArray.Length);
            bool isNextOperator = false;
            foreach (char ch in newArray)
            {
                if (char.IsLetter(ch))
                {
                    if (isNextOperator)
                    {
                        if (!stack.TryPop(out char first))
                            return false;
                        stack.Push(first);
                        isNextOperator = false;
                    }
                    else
                    {
                        stack.Push(ch);
                    }
                }
                else
                {
                    if (!IsArithmeticOperator(ch))
                        return false;
                    if (isNextOperator)
                        return false;
                    isNextOperator = true;
                }
            }
            if (stack.Count != 1 || !char.IsLetter(stack.Peek()))
                return false;
            type = ExpressionType.Infix;
            return true;
        }

        //후위 표기법 타입 여부 메서드
        private bool IsPostfix(char[] charArray)
        {
            return IsPrePostfix(charArray, ExpressionType.Postfix);
        }

        //(전/후)위 표기법 타입 여부 상세 메서드
        private bool IsPrePostfix(char[] array, ExpressionType type)
        {
            Stack<char> stack = new Stack<char>();
            char[] newArray = new char[array.Length];
            Array.Copy(array, newArray, array.Length);
            if (type == ExpressionType.Prefix)
                Array.Reverse(newArray);
            foreach (char ch in newArray)
            {
                if (char.IsLetter(ch))
                {
                    stack.Push(ch);
                }
                else
                {
                    if (!IsArithmeticOperator(ch))
                        return false;
                    if (!stack.TryPop(out _))
                        return false;
                    if (!stack.TryPop(out char first))
                        return false;
                    stack.Push(first);
                }
            }
            if (stack.Count != 1 || !char.IsLetter(stack.Peek()))
                return false;
            this.type = type;
            return true;
        }
        #endregion

        #region ConvertMethods
        //전위표현식 => 중위표현식 변환 메서드
        public string PrefixToInfix()
        {
            string result = ConvertPrefixTo(ExpressionType.Infix);
            if (result == string.Empty)
            {
                if(Debug.IsDebugMode)
                    Console.WriteLine("Prefix to Infix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Prefix to Infix Successed.");
            }
            return result;
        }

        //전위표현식 => 후위표현식 변환 메서드
        public string PrefixToPostfix()
        {
            string result = ConvertPrefixTo(ExpressionType.Postfix);
            if (result == string.Empty)
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Prefix to Postfix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Prefix to Postfix Successed.");
            }
            return result;
        }

        //중위표현식 => 전위표현식 변환 메서드
        public string InfixToPrefix()
        {
            string result = ConvertInfixTo(ExpressionType.Prefix);
            if (result == string.Empty)
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Infix to Prefix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Infix to Prefix Successed.");
            }
            return result;
        }

        //중위표현식 => 후위표현식 변환 메서드
        public string InfixToPostfix()
        {
            string result = ConvertInfixTo(ExpressionType.Postfix);
            if (result == string.Empty)
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Infix to Postfix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Infix to Postfix Successed.");
            }
            return result;
        }
  
        //후위표현식 => 전위표현식 변환 메서드
        public string PostfixToPrefix()
        {
            string result = ConvertPostfixTo(ExpressionType.Prefix);
            if (result == string.Empty)
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Postfix to Prefix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Postfix to Prefix Successed.");
            }
            return result;
        }

        //후위표현식 => 중위표현식 변환 메서드
        public string PostfixToInfix()
        {
            string result = ConvertPostfixTo(ExpressionType.Infix);
            if (result == string.Empty)
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Postfix to Infix Failed.");
            }
            else
            {
                if (Debug.IsDebugMode)
                    Console.WriteLine("Postfix to Infix Successed.");
            }
            return result;
        }

        //전위 표현식 변환 상세 메서드
        private string ConvertPrefixTo(ExpressionType targetType)
        {
            if (!CheckType(ExpressionType.Prefix) || IsTargetTypeNull(targetType))
                return string.Empty;
            string newString = new string(expression.Reverse().ToArray());
            Stack<string> stack = new Stack<string>();
            foreach (char ch in newString)
            {
                if (char.IsLetter(ch))
                {
                    stack.Push(ch.ToString());
                }
                else
                {
                    string frontOperand = stack.Pop();
                    string backOperand = stack.Pop();

                    switch (targetType)
                    {
                        case ExpressionType.Prefix:
                            stack.Push(ch.ToString() + frontOperand + backOperand);
                            break;
                        case ExpressionType.Infix:
                            stack.Push(frontOperand + ch.ToString() + backOperand);
                            break;
                        case ExpressionType.Postfix:
                            stack.Push(frontOperand + backOperand + ch.ToString());
                            break;
                    }
                }
            }
            return stack.Pop();
        }
        //중위 표현식 변환 상세 메서드
        private string ConvertInfixTo(ExpressionType targetType)
        {
            if (!CheckType(ExpressionType.Infix) || IsTargetTypeNull(targetType))
                return string.Empty;
            string newString = (targetType == ExpressionType.Prefix) ? new string(expression.Reverse().ToArray()) : new string(expression);
            Stack<char> stack = new Stack<char>();
            int index = (targetType == ExpressionType.Prefix) ? newString.Length : 0;
            char[] result = new char[newString.Length];
            foreach (char ch in newString)
            {
                if (char.IsLetter(ch))
                {
                    if (targetType == ExpressionType.Prefix)
                        result[--index] = ch;
                    else
                        result[index++] = ch;
                }
                else
                {
                    if (targetType == ExpressionType.Infix)
                    {
                        result[index++] = ch;
                    }
                    else
                    {
                        if (stack.Count == 0)
                            stack.Push(ch);
                        else
                        {
                            char firstOperator = stack.Peek();
                            if (ComparePrecedence(firstOperator, ch) > 0)
                            {
                                switch (targetType)
                                {
                                    case ExpressionType.Prefix:
                                        result[--index] = stack.Pop();
                                        break;
                                    case ExpressionType.Postfix:
                                        result[index++] = stack.Pop();
                                        break;
                                }
                            }
                            stack.Push(ch);
                        }
                    }
                }
            }
            while (stack.Count != 0)
            {
                if (targetType == ExpressionType.Prefix)
                    result[--index] = stack.Pop();
                else
                    result[index++] = stack.Pop();
            }
            return new string(result);
        }
        //후위 표현식 변환 상세 메서드
        private string ConvertPostfixTo(ExpressionType targetType)
        {
            if (!CheckType(ExpressionType.Postfix) || IsTargetTypeNull(targetType))
                return string.Empty;
            string newString = new string(expression);
            Stack<string> stack = new Stack<string>();
            foreach (char ch in newString)
            {
                if (char.IsLetter(ch))
                {
                    stack.Push(ch.ToString());
                }
                else
                {
                    string frontOperand = stack.Pop();
                    string backOperand = stack.Pop();

                    switch (targetType)
                    {
                        case ExpressionType.Prefix:
                            stack.Push(ch.ToString() + backOperand + frontOperand);
                            break;
                        case ExpressionType.Infix:
                            stack.Push(backOperand + ch.ToString() + frontOperand);
                            break;
                        case ExpressionType.Postfix:
                            stack.Push(backOperand + frontOperand + ch.ToString());
                            break;
                    }
                }
            }
            return stack.Pop();
        }
        #endregion

        #region StaticMethods
        //표현식 문자열 전처리 메서드
        public static char[] Processing(string str)
        {
            string removeWhiteSpace = string.Concat(str.Where(ch => !char.IsWhiteSpace(ch)));
            char[] result = removeWhiteSpace.ToCharArray();
            return result;
        }

        //문자 산술 연산자 비교 메서드
        public static bool IsArithmeticOperator(char s)
        {
            char[] a = { '+', '-', '*', '/' };
            if (s.ToString().IndexOfAny(a) == -1)
                return false;
            return true;
        }

        //표현식 표기법 널 비교 메서드
        private static bool IsTargetTypeNull(ExpressionType type)
        {
            if (type != ExpressionType.Null)
                return false;
            Console.WriteLine($"Target expression type cannot be Null");
            return true;
        }

        //산술 연산자 우선순위 메서드
        private static int OperatorPrecedence(char _Operator)
        {
            switch (_Operator)
            {
                case '+':
                    return 0;
                case '-':
                    return 0;
                case '*':
                    return 1;
                case '/':
                    return 1;
                default:
                    return -1;
            }
        }

        //산술 연산자 우선순위 비교 메서드
        private static int ComparePrecedence(char firstOperator, char secondOperator)
        {
            int firstOperatorPrecedence = OperatorPrecedence(firstOperator);
            int secondOperatorPrecedence = OperatorPrecedence(secondOperator);
            if (firstOperatorPrecedence == secondOperatorPrecedence) return 0;
            return firstOperatorPrecedence > secondOperatorPrecedence ? 1 : -1;
        }
        #endregion

        #region Exceptions
        private static void ThrowNullorEmptyStringException(string str, string methodName)
        {
            try
            {
                ArgumentNullException.ThrowIfNullOrEmpty(str);
            }
            catch (ArgumentNullException)
            {
                Console.WriteLine($"{methodName} string argument is NullorEmpty.");
            }
        }
        #endregion

        #endregion
    }

    class ConvertExpressions_Ex
    {
        static void Main(string[] args)
        {
            Convert_Expressions Prefix = new Convert_Expressions("+-ABC");
            Convert_Expressions Infix = new Convert_Expressions("A*B/C+D");
            Convert_Expressions Postfix = new Convert_Expressions("AB*CD*+");

            Console.WriteLine($"{Prefix.Expression} To Infix : {Prefix.PrefixToInfix()}");
            Console.WriteLine($"{Prefix.Expression} To Postfix : {Prefix.PrefixToPostfix()}");
            Console.WriteLine($"{Infix.Expression} To Prefix : {Infix.InfixToPrefix()}");
            Console.WriteLine($"{Infix.Expression} To Postfix : {Infix.InfixToPostfix()}");
            Console.WriteLine($"{Postfix.Expression} To Prefix : {Postfix.PostfixToPrefix()}");
            Console.WriteLine($"{Postfix.Expression} To Infix : {Postfix.PostfixToInfix()}");
        }
    }
}
