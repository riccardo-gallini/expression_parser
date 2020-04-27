using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{
    class Program
    {
        static void Main(string[] args)
        {
            var expr = "1 + 1 + 1 + 1 + 1 + 1";
            // Console.WriteLine(expr);
            // Console.WriteLine();

            // var tokens = Lexer.GetTokens(expr);
            // print(tokens);
            // Console.WriteLine();

            // var postfix_tokens = Parser.ToPostfix(tokens);
            // print(postfix_tokens);
            // Console.WriteLine();

            //var res = Calculator.Eval(expr);
            var ast = AstParser.Parse(expr);

        }

        

        static void print(IEnumerable<Token> tokens)
        {
            foreach(var tok in tokens)
            {
                Console.WriteLine(tok.expr);
            }
            Console.WriteLine();
        }
    }
}
