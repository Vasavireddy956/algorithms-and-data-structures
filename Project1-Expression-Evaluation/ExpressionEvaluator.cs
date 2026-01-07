using System;
using System.Collections.Generic;

public class ExpressionEvaluator
{
    private static Dictionary<string, int> precedence = new Dictionary<string, int>()
    {
        { "^", 3 },
        { "*", 2 }, { "/", 2 },
        { "+", 1 }, { "-", 1 }
    };

    private static bool IsOperator(string token)
    {
        return precedence.ContainsKey(token);
    }

    public static List<string> InfixToPostfix(List<string> tokens)
    {
        Stack<string> operatorStack = new Stack<string>(tokens.Count);
        List<string> postfix = new List<string>();

        foreach (string token in tokens)
        {
            if (double.TryParse(token, out _))
            {
                postfix.Add(token);
            }
            else if (token == "(")
            {
                operatorStack.Push(token);
            }
            else if (token == ")")
            {
                while (!operatorStack.IsEmpty() && operatorStack.Peek() != "(")
                {
                    postfix.Add(operatorStack.Pop());
                }
                operatorStack.Pop(); // remove '('
            }
            else if (IsOperator(token))
            {
                while (!operatorStack.IsEmpty() && IsOperator(operatorStack.Peek()) &&
                       precedence[operatorStack.Peek()] >= precedence[token])
                {
                    postfix.Add(operatorStack.Pop());
                }
                operatorStack.Push(token);
            }
        }

        while (!operatorStack.IsEmpty())
        {
            postfix.Add(operatorStack.Pop());
        }

        return postfix;
    }

    public static double EvaluatePostfix(List<string> postfix)
    {
        Stack<double> operandStack = new Stack<double>(postfix.Count);

        foreach (string token in postfix)
        {
            if (double.TryParse(token, out double value))
            {
                operandStack.Push(value);
            }
            else
            {
                double b = operandStack.Pop();
                double a = operandStack.Pop();

                switch (token)
                {
                    case "+": operandStack.Push(a + b); break;
                    case "-": operandStack.Push(a - b); break;
                    case "*": operandStack.Push(a * b); break;
                    case "/": operandStack.Push(a / b); break;
                    case "^": operandStack.Push(Math.Pow(a, b)); break;
                }
            }
        }

        return operandStack.Pop();
    }
}

