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
        Dpen,
        Drop,
        For,
        From,
        If,
        Move,
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

        Equals,
        Smaller,
        Bigger,

        BracketOn,
        BraketOff,
        CurlyBracketOn,
        CurlyBracketOff,

        Plus,
        Minus,
        Multiply,
        Divide,
        Percent,
        
    }
}
