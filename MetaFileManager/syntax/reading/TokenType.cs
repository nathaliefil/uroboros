﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public enum TokenType
    {
        Add,
        And,
        Asc,
        Ask,
        By,
        Const,
        Copy,
        Create,
        Cut,
        Delete,
        Desc,
        Directory,
        Drop,
        Each,
        Else,
        Elif,
        File,
        First,
        For,
        Force,
        From,
        Here,
        If,
        Last,
        Like,
        Move,
        Not,
        Open,
        Or,
        Order,
        Print,
        Receive,
        Remove,
        Rename,
        Reverse,
        Run,
        Select,
        Set,
        Skip,
        This,
        To,
        Where,
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
