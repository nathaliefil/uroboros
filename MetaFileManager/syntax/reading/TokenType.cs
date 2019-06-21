using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    public enum TokenType
    {
        Add,
        And,
        Ask,
        By,
        Copy,
        Create,
        Cut,
        Delete,
        Directory,
        Drop,
        Each,
        Else,
        File,
        First,
        For,
        Force,
        From,
        If,
        Last,
        Like,
        Move,
        Not,
        Open,
        Or,
        Order,
        Print,
        Remove,
        Rename,
        Reverse,
        Run,
        Select,
        Skip,
        Sleep,
        To,
        Where,
        While,
        Xor,

        Variable,
        StringConstant,
        NumericConstant,
        BoolConstant,

        Comma,
        Exclamation,
        Equals,
        Smaller,
        Bigger,
        BracketOn,
        BracketOff,
        CurlyBracketOn,
        CurlyBracketOff,
        Colon,
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
        NotEquals,
        BiggerOrEquals,
        SmallerOrEquals,
        Arrow,
        PlusEquals,
        MinusEquals,
        MultiplyEquals,
        DivideEquals,

        CreateFile,
        CreateDirectory,
        ForceTo,
        OrderBy
    }
}
