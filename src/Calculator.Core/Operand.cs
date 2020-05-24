using System;

namespace Calculator
{
    internal sealed class Operand
    {
        // .Ctor
        private Operand(OperandKind kind, object value)
        {
            Kind = kind;
            Value = value;
        }

        // Static .Ctor
        internal static Operand FromBoolean(bool value)
        {
            return new Operand(OperandKind.Boolean, value);
        }

        internal static Operand FromNumber(decimal value)
        {
            return new Operand(OperandKind.Number, value);
        }

        // Properties
        internal OperandKind Kind  { get; set; }
        internal object      Value { get; set; }
    }
}