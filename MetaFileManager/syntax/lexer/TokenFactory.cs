using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.lexer
{
    class TokenFactory
    {

        public static Token Build(char keychar)
        {
            switch (keychar)
            {
                case ',': return new Token(TokenType.Comma);
                case '!': return new Token(TokenType.Exclamation);
                case '=': return new Token(TokenType.Equals);
                case '(': return new Token(TokenType.BracketOn);
                case ')': return new Token(TokenType.BracketOff);
                case '{': return new Token(TokenType.CurlyBracketOn);
                case '}': return new Token(TokenType.CurlyBracketOff);
                case '[': return new Token(TokenType.SquareBracketOn);
                case ']': return new Token(TokenType.SquareBracketOff);
                case ':': return new Token(TokenType.Colon);
                case ';': return new Token(TokenType.Semicolon);
                case '-': return new Token(TokenType.Minus);
                case '+': return new Token(TokenType.Plus);
                case '*': return new Token(TokenType.Multiply);
                case '%': return new Token(TokenType.Percent);
                case '/': return new Token(TokenType.Divide);
                case '&': return new Token(TokenType.And);
                case '|': return new Token(TokenType.Or);
                case '^': return new Token(TokenType.Xor);
                case '<': return new Token(TokenType.Smaller);
                case '>': return new Token(TokenType.Bigger);
                case '?': return new Token(TokenType.QuestionMark);
            }
            return new Token(TokenType.Null) ;
        }

        public static Token Build(string code)
        {
            switch (code.ToLower())
            {
                case "add": return new Token(TokenType.Add);
                case "after": return new Token(TokenType.After);
                case "and": return new Token(TokenType.And);
                case "ask": return new Token(TokenType.Ask);
                case "before": return new Token(TokenType.Before);
                case "by": return new Token(TokenType.By);
                case "copy": return new Token(TokenType.Copy);
                case "count": return new Token(TokenType.Count);
                case "create": return new Token(TokenType.Create);
                case "cut": return new Token(TokenType.Cut);
                case "delete": return new Token(TokenType.Delete);
                case "directory": return new Token(TokenType.Directory);
                case "drop": return new Token(TokenType.Drop);
                case "each": return new Token(TokenType.Each);
                case "else": return new Token(TokenType.Else);
                case "false": return new Token(TokenType.BoolConstant);
                case "file": return new Token(TokenType.File);
                case "first": return new Token(TokenType.First);
                case "for": return new Token(TokenType.For);
                case "force": return new Token(TokenType.Force);
                case "from": return new Token(TokenType.From);
                case "if": return new Token(TokenType.If);
                case "in": return new Token(TokenType.In);
                case "inside": return new Token(TokenType.Inside);
                case "is": return new Token(TokenType.Is);
                case "last": return new Token(TokenType.Last);
                case "like": return new Token(TokenType.Like);
                case "move": return new Token(TokenType.Move);
                case "not": return new Token(TokenType.Exclamation);
                case "open": return new Token(TokenType.Open);
                case "or": return new Token(TokenType.Or);
                case "order": return new Token(TokenType.Order);
                case "print": return new Token(TokenType.Print);
                case "reaccess": return new Token(TokenType.Reaccess);
                case "recreate": return new Token(TokenType.Recreate);
                case "remodify": return new Token(TokenType.Remodify);
                case "remove": return new Token(TokenType.Remove);
                case "rename": return new Token(TokenType.Rename);
                case "reverse": return new Token(TokenType.Reverse);
                case "run": return new Token(TokenType.Run);
                case "select": return new Token(TokenType.Select);
                case "skip": return new Token(TokenType.Skip);
                case "sleep": return new Token(TokenType.Sleep);
                case "swap": return new Token(TokenType.Swap);
                case "to": return new Token(TokenType.To);
                case "true": return new Token(TokenType.BoolConstant, "true");
                case "unique": return new Token(TokenType.Unique);
                case "where": return new Token(TokenType.Where);
                case "while": return new Token(TokenType.While);
                case "with": return new Token(TokenType.With);
                case "without": return new Token(TokenType.Without);
                case "xor": return new Token(TokenType.Xor);
            }
            return new Token(TokenType.Variable, code);
        }
    }
}


