using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public enum TokenType
    {
        
        And,
        Ask,
        Copy,
        Cut,
        Delete,
        Drop,
        For,
        From,
        If,
        Move,
        Open,
        Or,
        Print,
        Receive,
        Rename,
        Run,
        Select,
        Set,
        To,
        While,
        Xor,

        Name,
        StringName,

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
        
        Empty
    }
}
