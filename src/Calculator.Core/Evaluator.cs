using System;
using System.Collections.Generic;

namespace Calculator
{
    public sealed class Evaluator
    {
        // Internal Instance Data
        private readonly Lexer _lexer;
        private readonly Random _random = new Random();

        // .Ctor
        // Todo: Use ILexer instead
        public Evaluator(Lexer lexer)
        {
            _lexer = lexer;
        }

        // Properties
        public EvaluatorDisplayMode DisplayMode { get; private set; } = EvaluatorDisplayMode.Decimal;

        // Methods
        // Todo: We're missing type-checking and appropriate error handling!
        // Todo: Switch to an type operator based computation!
        // Todo: Introduce Implicit Casting!
        public object Evaluate()
        {
            var stack = new SpecializedStack<Operand>();
            var variables = new Dictionary<string, Operand>();

            foreach (var token in _lexer.GetTokens())
            {
                switch (token.Kind)
                {
                    // Arithmetic Operators
                    case TokenKind.Add:
                    {
                        var right  = stack.Pop();
                        var left   = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)left.Value + (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Subtract:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)left.Value - (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Multiply:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)left.Value * (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Divide:
                    {
                        var right  = stack.Pop();
                        var left   = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)left.Value / (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.ClearStackAndVariables:
                    {
                        variables.Clear();
                        stack.Clear();

                        break;
                    }
                    case TokenKind.ClearStack:
                    {
                        stack.Clear();

                        break;
                    }
                    case TokenKind.ClearVariables:
                    {
                        variables.Clear();

                        break;
                    }
                    case TokenKind.BooleanNot:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Boolean)
                        {
                            stack.Push(Operand.FromBoolean(!(bool)operand.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Modulus:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)left.Value % (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Increment:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)operand.Value + 1));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Decrement:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)operand.Value - 1));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Bitwise Operators
                    case TokenKind.BitwiseAnd:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to a long.
                            stack.Push(Operand.FromNumber((long)(decimal)left.Value & (long)(decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BitwiseOr:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to a long.
                            stack.Push(Operand.FromNumber((long)(decimal)left.Value | (long)(decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BitwiseXor:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to a long.
                            stack.Push(Operand.FromNumber((long)(decimal)left.Value ^ (long)(decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BitwiseNot:
                    {
                        var operand  = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to a long.
                            stack.Push(Operand.FromNumber(~(long)(decimal)operand.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BitwiseShiftLeft:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to an int/long.
                            stack.Push(Operand.FromNumber((long)(decimal)left.Value << (int)(decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BitwiseShiftRight:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend we can always convert to an int/long.
                            stack.Push(Operand.FromNumber((long)(decimal)left.Value >> (int)(decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Boolean Operators
                    case TokenKind.BooleanAnd:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Boolean && right.Kind == OperandKind.Boolean)
                        {
                            stack.Push(Operand.FromBoolean((bool)left.Value && (bool)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BooleanOr:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Boolean && right.Kind == OperandKind.Boolean)
                        {
                            stack.Push(Operand.FromBoolean((bool)left.Value || (bool)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.BooleanXor:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Boolean && right.Kind == OperandKind.Boolean)
                        {
                            var leftValue  = (bool)left.Value;
                            var rightValue = (bool)right.Value;

                            stack.Push(Operand.FromBoolean((leftValue && leftValue == false) || (leftValue == false && leftValue)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Comparison Operators
                    case TokenKind.LessThan:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value < (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.LessThanOrEqualTo:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value <= (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.EqualTo:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value == (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.GreaterThan:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value > (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.GreaterThanOrEqualTo:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value >= (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.NotEqualTo:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromBoolean((decimal)left.Value != (decimal)right.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Trigonometric Functions
                    case TokenKind.Acos:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Acos((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Asin:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Asin((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Atan:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Atan((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Cos:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Cos((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Cosh:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Cosh((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Sin:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Sin((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Sinh:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Sinh((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Tanh:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Tanh((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Numeric Utilities
                    case TokenKind.Ceil:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Ceiling((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Floor:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Floor((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Round:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Round((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.IntegerPart:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Truncate((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.FloatingPart:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            var value = (decimal)operand.Value;
                            stack.Push(Operand.FromNumber(value - Math.Truncate(value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Sign:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: I'm not sure I understand the documentation on this one!
                            var value = (decimal)operand.Value;
                            stack.Push(Operand.FromNumber(value < 0 ? -1 : 0));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Absolute:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Abs((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Max:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Max((decimal)left.Value, (decimal)right.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Min:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(Math.Min((decimal)left.Value, (decimal)right.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Display Modes
                    case TokenKind.DisplayAsHex:
                    {
                        DisplayMode = EvaluatorDisplayMode.Hexadecimal;
                        break;
                    }
                    case TokenKind.DisplayAsDecimal:
                    {
                        DisplayMode = EvaluatorDisplayMode.Decimal;
                        break;
                    }
                    case TokenKind.DisplayAsBinary:
                    {
                        DisplayMode = EvaluatorDisplayMode.Binary;
                        break;
                    }
                    case TokenKind.DisplayAsOctal:
                    {
                        DisplayMode = EvaluatorDisplayMode.Octal;
                        break;
                    }
                    // Constants
                    case TokenKind.PushE:
                    {
                        stack.Push(Operand.FromNumber((decimal)Math.E));
                        break;
                    }
                    case TokenKind.PushPi:
                    {
                        stack.Push(Operand.FromNumber((decimal)Math.PI));
                        break;
                    }
                    case TokenKind.PushRandom:
                    {
                        stack.Push(Operand.FromNumber((decimal)_random.Next()));
                        break;
                    }
                    // Mathematic Functions
                    case TokenKind.Exp:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Exp((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Fact:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber(BuiltIn.Factorial((decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Sqrt:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Sqrt((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Ln:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Log((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Log:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to double!
                            stack.Push(Operand.FromNumber((decimal)Math.Log10((double)(decimal)operand.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Pow:
                    {
                        var right = stack.Pop();
                        var left  = stack.Pop();

                        if (left.Kind == OperandKind.Number && right.Kind == OperandKind.Number)
                        {
                            stack.Push(Operand.FromNumber((decimal)Math.Pow((double)(decimal)left.Value, (double)(decimal)right.Value)));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    // Networking
                    case TokenKind.HostToNetworkLong:
                    case TokenKind.HostToNetworkShort:
                    case TokenKind.NetworkToHostLong:
                    case TokenKind.NetworkToHostShort:
                    {
                        throw new NotImplementedException();
                    }
                    // Stack Manipulation
                    case TokenKind.Pick:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to int!
                            stack.Push(stack.Pick((int)operand.Value));
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Repeat:
                    {
                        // Todo: Complete!
                        throw new NotImplementedException();
                    }
                    case TokenKind.Depth:
                    {
                        stack.Push(Operand.FromNumber(stack.Count));
                        break;
                    }
                    case TokenKind.Drop:
                    {
                        stack.Pop();
                        break;
                    }
                    case TokenKind.DropN:
                    {
                        var operand = stack.Pop();

                        if (operand.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to int!
                            for (int i = 0; i < (int)operand.Value; i++)
                            {
                                stack.Pop();
                            }
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Dup:
                    {
                        stack.Push(stack.Peek());
                        break;
                    }
                    case TokenKind.DupN:
                    {
                        var op1 = stack.Pop();
                        var op2 = stack.Peek();

                        if (op1.Kind == OperandKind.Number)
                        {
                            // Todo: Let's pretend it's ok to convert to int!
                            for (int i = 0; i < (int)op1.Value; i++)
                            {
                                stack.Push(op2);
                            }
                        }
                        else
                        {
                            throw new FormatException();
                        }

                        break;
                    }
                    case TokenKind.Roll:
                    case TokenKind.RollDownwards:
                    case TokenKind.ToggleStack:
                    case TokenKind.Swap:
                    {
                        // Todo: Implement!
                        throw new NotImplementedException();
                    }
                    // Macros and Variables
                    case TokenKind.Macro:
                    //case TokenKind.Variable:
                    {
                        // Todo: Implement!
                        throw new NotImplementedException();
                    }
                    // Operand
                    case TokenKind.Operand:
                    {
                        stack.Push(Operand.FromNumber((decimal)token.Value));
                        break;
                    }
                    default:
                        break;
                }
            }

            return stack.Count > 0 ? stack.Pop().Value : null;
        }
    }
}