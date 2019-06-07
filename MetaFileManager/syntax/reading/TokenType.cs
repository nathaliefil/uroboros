using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public enum TokenType
    {
        Add,
        And,
        Ask,
        Copy,
        Create,
        Cut,
        Delete,
        Directory,
        Drop,
        Each,
        File,
        First,
        For,
        From,
        Here,
        If,
        Ignore,
        Last,
        Move,
        Open,
        Or,
        Print,
        Receive,
        Remove,
        Rename,
        Run,
        Select,
        Set,
        This,
        To,
        While,
        Xor,

        Variable,
        StringConstant,
        NumericConstant,

        Comma,
        Exclamation,
        Equals,
        Smaller,
        Bigger,

        BracketOn,
        BracketOff,
        CurlyBracketOn,
        CurlyBracketOff,
        Semicolon,

        Plus,
        Minus,
        Multiply,
        Divide,
        Percent,
        Empty,

        PlusPlus,
        MinusMinus,
        EqualsEquals,
        BiggerOrEquals,
        SmallerOfEquals,
        Arrow
    }
}
