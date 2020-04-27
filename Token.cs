using System;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace expression_parser
{

    public class Token
    {
        public string expr => expr_builder.ToString();
        public int start {get;}
        public int end {get; internal set;}
        public TokenType type {get;}
        private StringBuilder expr_builder;

        internal void AppendChar(char ch, int pos)
        {
            this.end = pos;
            expr_builder.Append(ch);
        }

        public Token(int pos, TokenType type)
        {
            this.start = pos;
            this.end = pos;
            this.type = type;
            expr_builder = new StringBuilder();
        }
    }

    public enum TokenType
    {
        oper,
        par,
        sep,
        unk,
        operand
    }

}