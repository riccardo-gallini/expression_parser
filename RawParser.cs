using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{
    public class RawParser
    {

        //shunting-yard algorhithm
        public static T ParseExpression<T>(IEnumerable<Token> tokens, 
                                           Func<Token,Stack<T>,T> eval_operator,
                                           Func<Token,T> eval_operand)
        {
            var values_stack = new Stack<T>();
            var operators_stack = new Stack<Token>();

            foreach(var tok in tokens)
            {
                if (tok.type==TokenType.operand)
                {
                    values_stack.Push(eval_operand(tok));
                }
                else if (is_open_par(tok))
                {
                    operators_stack.Push(tok);
                }
                else if (is_closed_par(tok))
                {
                    while (operators_stack.Count > 0 && !is_open_par(operators_stack.Peek())) 
                    { 
                        var op = operators_stack.Pop();
                        var result = eval_operator(op, values_stack); 
                        values_stack.Push(result);
                    } 
                    if (operators_stack.Count > 0 && !is_open_par(operators_stack.Peek())) 
                    { 
                        throw new InvalidOperationException();
                    } 
                    else
                    { 
                        operators_stack.Pop(); 
                    } 
                }
                else if (tok.type==TokenType.oper)
                { 
                    while (operators_stack.Count > 0 && op_precedence(tok) <= op_precedence(operators_stack.Peek())) 
                    { 
                        var op = operators_stack.Pop();
                        var result = eval_operator(op, values_stack); 
                        values_stack.Push(result); 
                    } 
                    operators_stack.Push(tok); 
                } 
            }

            while (operators_stack.Count > 0) 
            { 
                var op = operators_stack.Pop();
                var result = eval_operator(op, values_stack); 
                values_stack.Push(result);
            } 

            return values_stack.Pop();
            
        }

        private static int op_precedence(Token tok)
        {
            if (tok.type==TokenType.oper)
            {
                switch(tok.expr)
                {
                    case "+":
                    case "-":
                        return 1;

                    case "*":
                    case "/":
                    case "%":
                        return 2;

                    case "^":
                        return 3;
                }
            }
            return -1;
        }

    public static bool is_open_par(Token tok) => tok.type==TokenType.par && tok.expr=="(";
    public static bool is_closed_par(Token tok) => tok.type==TokenType.par && tok.expr==")";

    }
}