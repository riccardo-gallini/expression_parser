using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{
    public class Lexer
    {
        public static IEnumerable<Token> GetTokens(string expr)
        {
            Token current = null;
            
            int pos = 0;
            char ch;

            while (pos<expr.Length)
            {
                ch = expr[pos];
                var type = verify_token(ch);
                
                if (type == TokenType.operand && ( (current != null && current.type != TokenType.operand) || current == null) )
                {
                    if (current!=null) yield return current;
                    current = new Token(pos,type);
                    current.AppendChar(ch, pos);
                }
                else if (type == TokenType.operand && current?.type == TokenType.operand)
                {
                    current.AppendChar(ch, pos);
                }
                else if (type==TokenType.sep)
                {
                    if (current!=null) yield return current;
                    current = null;
                }
                else if (type==TokenType.oper || type==TokenType.par || type==TokenType.unk)
                {
                    if (current!=null) yield return current;
                    current = new Token(pos,type);
                    current.AppendChar(ch, pos);
                }

                pos++;
            }

            if (current!=null) yield return current;
            
        }

        static private TokenType verify_token(char ch)
        {
            if (ch=='+') return TokenType.oper;
            if (ch=='-') return TokenType.oper;
            if (ch=='*') return TokenType.oper;
            if (ch=='^') return TokenType.oper;
            if (ch=='%') return TokenType.oper;
            if (ch=='/') return TokenType.oper;
            if (ch=='(') return TokenType.par;
            if (ch==')') return TokenType.par;
            if (char.IsSeparator(ch)) return TokenType.sep;
            if (char.IsLetterOrDigit(ch)) return TokenType.operand;
            if (char.IsPunctuation(ch)) return TokenType.operand;
            return TokenType.unk;
        }
    }

}