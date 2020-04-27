using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{
    public class AstParser
    {

        public static Node Parse(string expr)
        {
            var tokens = Lexer.GetTokens(expr);

            var root = RawParser.ParseExpression<Node>(tokens, eval_operator, eval_operand);

            return root;

        }

        private static Node eval_operand(Token operand)
        {
            var node = new OperandNode();
            node.token = operand;
            node.value = operand.expr;
            
            return node;
        }
        private static Node eval_operator(Token op, Stack<Node> operands)
        {
            var node = new OperatorNode();
            node.token = op;
            node.right = operands.Pop();
            node.left = operands.Pop();
            node.op = op.expr;            

            return node;
        }

    }


    public class Node
    {
        public Token token;
    }
    public class OperatorNode : Node
    {
        public string op;
        public Node left;
        public Node right;
    }

    public class OperandNode : Node
    {
        public string value;
    }

}
