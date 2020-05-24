using System;
using System.Collections.Generic;
using System.IO;
using System.Globalization;
using System.Text;

namespace Calculator
{
    public sealed class Lexer
    {
        // Internal Static Data
        private static readonly IDictionary<string, TokenKind> _commands = new Dictionary<string, TokenKind>
        {
            // Arithmetic Operators
            { "+",      TokenKind.Add                    },
            { "-",      TokenKind.Subtract               },
            { "*",      TokenKind.Multiply               },
            { "/",      TokenKind.Divide                 },
            { "cla",    TokenKind.ClearStackAndVariables },
            { "clr",    TokenKind.ClearStack             },
            { "clv",    TokenKind.ClearVariables         },
            { "!",      TokenKind.BooleanNot             },
            { "!=",     TokenKind.NotEqualTo             },
            { "%",      TokenKind.Modulus                },
            { "++",     TokenKind.Increment              },
            { "--",     TokenKind.Decrement              },
                       
            // Bitwise Operators
            { "&",      TokenKind.BitwiseAnd             },
            { "|",      TokenKind.BitwiseOr              },
            { "^",      TokenKind.BitwiseXor             },
            { "~",      TokenKind.BitwiseNot             },
            { "<<",     TokenKind.BitwiseShiftLeft       },
            { ">>",     TokenKind.BitwiseShiftRight      },
                       
            // Boolean Operators
            { "&&",     TokenKind.BooleanAnd             },
            { "||",     TokenKind.BooleanOr              },
            { "^^",     TokenKind.BooleanXor             },
                       
            // Comparison Operators
            { "<",      TokenKind.LessThan               },
            { "<=",     TokenKind.LessThanOrEqualTo      },
            { "==",     TokenKind.EqualTo                },
            { ">",      TokenKind.GreaterThan            },
            { ">=",     TokenKind.GreaterThanOrEqualTo   },
                       
                       
            // Trigonometric Functions
            { "acos",   TokenKind.Acos                   },
            { "asin",   TokenKind.Asin                   },
            { "atan",   TokenKind.Atan                   },
            { "cos",    TokenKind.Cos                    },
            { "cosh",   TokenKind.Cosh                   },
            { "sin",    TokenKind.Sin                    },
            { "sinh",   TokenKind.Sinh                   },
            { "tanh",   TokenKind.Tanh                   },

            // Numeric Utilities
            { "ceil",   TokenKind.Ceil                   },
            { "floor",  TokenKind.Floor                  },
            { "round",  TokenKind.Round                  },
            { "ip",     TokenKind.IntegerPart            },
            { "fp",     TokenKind.FloatingPart           },
            { "sign",   TokenKind.Sign                   },
            { "abs",    TokenKind.Absolute               },
            { "max",    TokenKind.Max                    },
            { "min",    TokenKind.Min                    },

            // Display Modes
            { "hex",    TokenKind.DisplayAsHex           },
            { "dec",    TokenKind.DisplayAsDecimal       },
            { "bin",    TokenKind.DisplayAsBinary        },
            { "oct",    TokenKind.DisplayAsOctal         },

            // Constants
            { "e",      TokenKind.PushE                  },
            { "π",      TokenKind.PushPi                 },
            { "pi",     TokenKind.PushPi                 },
            { "rand",   TokenKind.PushRandom             },

            // Mathematic Functions
            { "exp",    TokenKind.Exp                    },
            { "fact",   TokenKind.Fact                   },
            { "sqrt",   TokenKind.Sqrt                   },
            { "ln",     TokenKind.Ln                     },
            { "log",    TokenKind.Log                    },
            { "pow",    TokenKind.Pow                    },
            { "**",     TokenKind.Pow                    },

            // Networking
            { "hnl",    TokenKind.HostToNetworkLong      },
            { "hns",    TokenKind.HostToNetworkShort     },
            { "nhl",    TokenKind.NetworkToHostLong      },
            { "nhs",    TokenKind.NetworkToHostShort     },

            // Stack Manipulation
            { "pick",   TokenKind.Pick                   },
            { "repeat", TokenKind.Repeat                 },
            { "depth",  TokenKind.Depth                  },
            { "drop",   TokenKind.Drop                   },
            { "dropn",  TokenKind.DropN                  },
            { "dup",    TokenKind.Dup                    },
            { "dupn",   TokenKind.DupN                   },
            { "roll",   TokenKind.Roll                   },
            { "rolld",  TokenKind.RollDownwards          },
            { "stack",  TokenKind.ToggleStack            },
            { "swap",   TokenKind.Swap                   },

            // Macros and Variables
            { "macro",  TokenKind.Macro                  },
        };                              

        // Internal Instance Data
        private readonly StreamReader _reader;

        // .Ctor
        public Lexer(Stream stream)
        {
            _reader = new StreamReader(stream);
        }

        // Methods
        public IEnumerable<Token> GetTokens()
        {
            while(true)
            {
                var token = NextRawToken();

                if (token == null)
                {
                    break;
                }

                // Todo: Could be a macro!
                if (_commands.ContainsKey(token))
                {
                    yield return new Token(_commands[token], token);
                }
                else
                {
                    // Todo: Could be operand/variable!
                    yield return new Token(TokenKind.Operand, ParseOperand(token));
                }
            }
        }

        // Helpers
        // Todo: Include support for strings.
        // Todo: Memory Consumption could be optimzed using Span<T>.
        private string NextRawToken()
        {
            var result = new StringBuilder();
            var hasStarted = false;

            while (true)
            {
                var rvalue = _reader.Read();
                var cvalue = (char)rvalue;

                if (rvalue == -1)
                {
                    // Todo: This should be an exception!
                    break;
                }

                if (char.IsWhiteSpace(cvalue))
                {
                    if (hasStarted == false)
                    {
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                hasStarted = true;
                result.Append(cvalue);
            }

            if (result.Length == 0)
            {
                return null;
            }

            return result.ToString();
        }

        // Todo: Add support for octal/string?!
        private static decimal ParseOperand(string value)
        {
            if (value.StartsWith("0x") || value.StartsWith("0X"))
            {
                return Convert.ToInt64(value.Substring(2), 16);
            }
            
            if (value.StartsWith("0b") || value.StartsWith("0B"))
            {
                return Convert.ToInt64(value.Substring(2), 2);
            }
            
            if (value.StartsWith("0b") || value.StartsWith("0B"))
            {
                return Convert.ToInt64(value.Substring(2), 8);
            }

            return decimal.Parse(value);
        }
    }
}