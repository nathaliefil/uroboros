using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public enum TokenType
    {
        Name,
        StringName,

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
        Print,
        Receive,
        Rename,
        Run,
        Select,
        Set,
        To,
        While,

        Equals,
        NotEquals,
        Smaller,
        SmallerEquals,
        Bigger,
        BiggerEquals,
        BracketOn,
        BraketOff,
        CurlyBracketOn,
        CurlyBracketOff,
        Exclamation
    }
}
