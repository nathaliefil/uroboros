using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    class TokenFactory
    {

        public static Token Build(char keychar)
        {
            switch (keychar)
            {
                case '!': return new Token(TokenType.Exclamation);
                case '=': return new Token(TokenType.Equals);
                case '(': return new Token(TokenType.BracketOn);
                case ')': return new Token(TokenType.BracketOff);
                case '{': return new Token(TokenType.CurlyBracketOn);
                case '}': return new Token(TokenType.CurlyBracketOff);
                case ';': return new Token(TokenType.Semicolon);
                case '-': return new Token(TokenType.Minus);
                case '+': return new Token(TokenType.Plus);
                case '*': return new Token(TokenType.Multiply);
                case '%': return new Token(TokenType.Percent);
                case '/': return new Token(TokenType.Divide);
                case '&': return new Token(TokenType.And);
                case '|': return new Token(TokenType.Or);
                case '^': return new Token(TokenType.Xor);
                case ' ': return new Token(TokenType.Empty);
            }
            return new Token(TokenType.Empty) ;
        }

        public static Token BuildQuotationToken(string code)
        {
            return new Token(TokenType.StringName, code.Trim('"'));
        }

        public static Token Build(string code)
        {
            switch (code.ToLower())
            {
                case "and": return new Token(TokenType.And);
                case "ask": return new Token(TokenType.Ask);
                case "copy": return new Token(TokenType.Copy);
                case "cut": return new Token(TokenType.Cut);
                case "delete": return new Token(TokenType.Delete);
                case "drop": return new Token(TokenType.Drop);
                case "for": return new Token(TokenType.For);
                case "from": return new Token(TokenType.From);
                case "if": return new Token(TokenType.If);
                case "move": return new Token(TokenType.Move);
                case "open": return new Token(TokenType.Open);
                case "or": return new Token(TokenType.Or);
                case "print": return new Token(TokenType.Print);
                case "receive": return new Token(TokenType.Receive);
                case "rename": return new Token(TokenType.Rename);
                case "run": return new Token(TokenType.Run);
                case "select": return new Token(TokenType.Select);
                case "set": return new Token(TokenType.Set);
                case "to": return new Token(TokenType.To);
                case "while": return new Token(TokenType.While);
                case "xor": return new Token(TokenType.Xor);
            }
            return new Token(TokenType.Name, code);
        }
    }
}
