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
        Ignore,
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
        Run,
        Select,
        Set,
        This,
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
        BiggerOrEquals,
        SmallerOrEquals,
        Arrow
    }
}
