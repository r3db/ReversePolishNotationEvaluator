using System;

namespace Calculator
{
    public class Token
    {
        public Token(TokenKind kind, object value)
        {
            Kind  = kind;
            Value = value;
        }

        public TokenKind Kind  { get; }
        public object    Value { get; }
    }
}