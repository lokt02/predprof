﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace csharp1
{
    class Program
    {

        static void Main(string[] args)
        {
            int octoInt(int octo, int m) {
                int a = 0;
                int j = Convert.ToString(octo).Length - 1;
                while (octo % 10 == 2 * m -1)
                {
                    octo /= 10;
                    a++;
                    j--;
                }
                octo++;
                for(int i = 0; i < a; i++)
                {
                    octo *= 10;
                }
                return octo;
            }

            bool check(string s)
            {
                Stack<char> stack = new Stack<char>();
                s.ToCharArray();
                foreach (char v in s)
                {
                    if (LeftBracket(v))
                    {
                        stack.Push(v);
                    }
                    else if (stack.Count != 0 && Fit(stack.Peek(), v))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        stack.Push(v);
                        break;
                    }
                }
                return stack.Count == 0 ? true : false;
            }

            bool LeftBracket(char c)
            {
                return (c == '(' || c == '{' || c == '[' || c == '<');
            }

            bool Fit(char lb, char rb)
            {
                return ('(' == lb && ')' == rb) || ('[' == lb && ']' == rb) || ('{' == lb && '}' == rb || ('<' == lb && '>' == rb));
            }

            List<string> brackets = new List<string>();
            int n, m;
            n = Convert.ToInt32(Console.ReadLine());
            m = Convert.ToInt32(Console.ReadLine());
            if (n % 2 != 0)
            {
                Console.WriteLine("NO");
                return;
            }
            int octo = 0;
            int var = 1;
            for(int i = 1; i <= n; i++)
            {
                var *= 2 * m;
            }
            for (int i = 0; i < var; i++)
            {
                bracketCheck(bracketConvert(octo, n), brackets);
                if (octo % 10 == 2 * m - 1)
                {
                    octo = octoInt(octo, m);
                }
                else
                {
                    octo++;
                }
            }
            Console.WriteLine(brackets.Count);
            IEnumerable<string> brack1 = brackets.Distinct();
            Console.WriteLine(brack1.Count());

            void bracketCheck(string s, List<string> brackets) 
            {
                if (s.StartsWith(')') || s.StartsWith(']') || s.StartsWith('}') || s.StartsWith('>'))
                {
                    return;
                }
                else
                if (check(s))
                {
                    Console.WriteLine("{0} ", s);
                    brackets.Add(s);
                }
                else
                {
                    return;
                }
            }

            string bracketConvert(int octo, int size) 
            {
                int o = octo;
                string s = "";
                for(int i = 0; i < size; i++)
                {
                    switch (o % 10)
                    {
                        case 0:
                            s += "(";
                            break;
                        case 1:
                            s += ")";
                            break;
                        case 2:
                            s += "[";
                            break;
                        case 3:
                            s += "]";
                            break;
                        case 4:
                            s += "{";
                            break;
                        case 5:
                            s += "}";
                            break;
                        case 6:
                            s += "<";
                            break;
                        case 7:
                            s += ">";
                            break;
                    }
                    o /= 10;
                }
                return s;
            }
        }
    }
}
