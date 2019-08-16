﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    public enum TokenType
    {
        Add,
        After,
        And,
        Ask,
        Before,
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
        In,
        Inside,
        Is,
        Last,
        Like,
        Move,
        Open,
        Or,
        Order,
        Print,
        Reaccess,
        Recreate,
        Remodify,
        Remove,
        Rename,
        Reverse,
        Run,
        Select,
        Skip,
        Sleep,
        To,
        Unique,
        Where,
        While,
        With,
        Without,
        Xor,

        Variable,
        StringConstant,
        NumericConstant,
        BoolConstant,

        Comma,
        Exclamation,
        QuestionMark,
        Equals,
        Smaller,
        Bigger,
        BracketOn,
        BracketOff,
        CurlyBracketOn,
        CurlyBracketOff,
        SquareBracketOn,
        SquareBracketOff,
        Colon,
        Semicolon,
        Plus,
        Minus,
        Multiply,
        Divide,
        Percent,

        BigArrow,
        SmallArrow,

        PlusPlus,
        MinusMinus,
        NotEquals,
        BiggerOrEquals,
        SmallerOrEquals,
        PlusEquals,
        MinusEquals,
        MultiplyEquals,
        DivideEquals,
        PercentEquals,
        IsBefore,
        IsAfter,

        CreateFile,
        CreateDirectory,
        ForceTo,
        OrderBy,

        Null
    }
}
