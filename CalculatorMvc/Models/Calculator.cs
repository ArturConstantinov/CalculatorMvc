using System.Globalization;

namespace CalculatorMvc.Models
{
    public class Calculator
    {
        public string _expression { get; set; }
        public string GetExpression { get; set; }

        private Dictionary<char, int> operationPriority = new() 
        {
            {'(', 0},
            {'+', 1},
            {'-', 1},
            {'*', 2},
            {'/', 2},
            {'^', 3},
            {'~', 4}
        };

        public Calculator(string expression)
        {
            _expression = expression;
            GetExpression = ToPostfix(_expression + "\r");
        }

        public string GetStringNumber(string expr, ref int pos)
        {
            string strNumber = "";

            for (; pos < expr.Length; pos++)
            {
                char num = expr[pos];

                if (Char.IsDigit(num) || num == '.')
                    strNumber += num;
                else
                {
                    pos--;
                    break;
                }
            }

            return strNumber;
        }

        public string ToPostfix(string _expression)
        {
            //try
            //{
                string GetExpression = "";
                Stack<char> stack = new();

                for (int i = 0; i < _expression.Length; i++)
                {
                    char c = _expression[i];

                    if (Char.IsDigit(c))
                    {
                        GetExpression += GetStringNumber(_expression, ref i) + " ";
                    }
                    else if (c == '(')
                    {
                        stack.Push(c);
                    }
                    else if (c == ')')
                    {
                        while (stack.Count > 0 && stack.Peek() != '(')
                            GetExpression += stack.Pop();
                        stack.Pop();
                    }
                    else if (operationPriority.ContainsKey(c))
                    {
                        char op = c;
                        if (op == '-' && (i == 0 || (i > 1 && operationPriority.ContainsKey(_expression[i - 1]))))
                            op = '~';

                        while (stack.Count > 0 && (operationPriority[stack.Peek()] >= operationPriority[op]))
                            GetExpression += stack.Pop();
                        stack.Push(op);
                    }
                }
                foreach (char op in stack)
                    GetExpression += op;

                return GetExpression;
            //}
            //catch (Exception)
            //{

            //    return "0";
            //}
        }

        public double Execute(char op, double first, double second) => op switch
        {
            '+' => first + second,                  
            '-' => first - second,
            '*' => first * second,
            '/' => first / second,
            '^' => Math.Pow(first, second),
            _ => 0
        };

        public double Calculate()
        {
            //try
            //{
                Stack<double> locals = new();
                int counter = 0;

                for (int i = 0; i < GetExpression.Length; i++)
                {
                    char c = GetExpression[i];

                    if (Char.IsDigit(c))
                    {
                        string number = GetStringNumber(GetExpression, ref i);
                    locals.Push(Convert.ToDouble(number));
                    //locals.Push(double.Parse(number, NumberStyles.Any, CultureInfo.InvariantCulture));
                }
                    else if (operationPriority.ContainsKey(c))
                    {
                        counter += 1;
                        if (c == '~')
                        {
                            double last = locals.Count > 0 ? locals.Pop() : 0;

                            locals.Push(Execute('-', 0, last));

                            continue;
                        }

                        double second = locals.Count > 0 ? locals.Pop() : 0,
                        first = locals.Count > 0 ? locals.Pop() : 0;

                        locals.Push(Execute(c, first, second));
                    }
                }

            return Math.Round(locals.Pop(), 10);
            //return locals.Pop();
            //}
            //catch (Exception)
            //{
            //return 0;
            //}
        }
    }
}
