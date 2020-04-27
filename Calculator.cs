using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{

    public class Calculator
    {

        public static double Eval(string expression)
        {
            var tokens = Lexer.GetTokens(expression);
            var value = RawParser.ParseExpression<double>(tokens, eval_operator, eval_operand);
            return value;
        }

        private static double eval_operand(Token operand)
        {
            return double.Parse(operand.expr);
        }
        private static double eval_operator(Token op, Stack<double> operands)
        {
            var op2 = operands.Pop();
            var op1 = operands.Pop();
            
            switch(op.expr)
            {
                case "+": return op1+op2;
                case "-": return op1-op2;
                case "*": return op1*op2;
                case "/": return op1/op2;
                case "^": return Math.Pow(op1, op2);
                case "%": return op1%op2;
            }

            throw new InvalidOperationException();
        }

    }
}