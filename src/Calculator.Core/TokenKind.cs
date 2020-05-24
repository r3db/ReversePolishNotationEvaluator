using System;

namespace Calculator
{
    public enum TokenKind
    {
        // Arithmetic Operators
        Add,
        Subtract,
        Multiply,
        Divide,
        ClearStackAndVariables,
        ClearStack,
        ClearVariables,
        BooleanNot,
        NotEqualTo,
        Modulus,
        Increment,
        Decrement,

        // Bitwise Operators
        BitwiseAnd,
        BitwiseOr,
        BitwiseXor,
        BitwiseNot,
        BitwiseShiftLeft,
        BitwiseShiftRight,

        // Boolean Operators
        BooleanAnd,
        BooleanOr,
        BooleanXor,

        // Comparison Operators
        LessThan,
        LessThanOrEqualTo,
        EqualTo,
        GreaterThan,
        GreaterThanOrEqualTo,

        // Trigonometric Functions
        Acos,
        Asin,
        Atan,
        Cos,
        Cosh,
        Sin,
        Sinh,
        Tanh,

        // Numeric Utilities
        Ceil,
        Floor,
        Round,
        IntegerPart,
        FloatingPart,
        Sign,
        Absolute,
        Max,
        Min,

        // Display Modes
        DisplayAsHex,
        DisplayAsDecimal,
        DisplayAsBinary,
        DisplayAsOctal,

        // Constants
        PushE,
        PushPi,
        PushRandom,

        // Mathematic Functions
        Exp,
        Fact,
        Sqrt,
        Ln,
        Log,
        Pow,

        // Networking
        HostToNetworkLong,
        HostToNetworkShort,
        NetworkToHostLong,
        NetworkToHostShort,

        // Stack Manipulation
        Pick,
        Repeat,
        Depth,
        Drop,
        DropN,
        Dup,
        DupN,
        Roll,
        RollDownwards,
        ToggleStack,
        Swap,

        // Macros and Variables
        Macro,

        // Operands
        Operand,
    }
}