﻿using System;
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
                case ',': return new Token(TokenType.Comma);
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
                case '<': return new Token(TokenType.Smaller);
                case '>': return new Token(TokenType.Bigger);
            }
            return new Token(TokenType.Empty) ;
        }

        public static Token BuildQuotationToken(string code)
        {
            return new Token(TokenType.StringConstant, code.Trim('"'));
        }

        public static Token Build(string code)
        {
            switch (code.ToLower())
            {
                case "add": return new Token(TokenType.Add);
                case "and": return new Token(TokenType.And);
                case "ask": return new Token(TokenType.Ask);
                case "copy": return new Token(TokenType.Copy);
                case "create": return new Token(TokenType.Create);
                case "cut": return new Token(TokenType.Cut);
                case "delete": return new Token(TokenType.Delete);
                case "directory": return new Token(TokenType.Directory);
                case "drop": return new Token(TokenType.Drop);
                case "each": return new Token(TokenType.Each);
                case "file": return new Token(TokenType.File);
                case "first": return new Token(TokenType.First);
                case "for": return new Token(TokenType.For);
                case "from": return new Token(TokenType.From);
                case "here": return new Token(TokenType.Here);
                case "if": return new Token(TokenType.If);
                case "ignore": return new Token(TokenType.Ignore);
                case "last": return new Token(TokenType.Last);
                case "move": return new Token(TokenType.Move);
                case "open": return new Token(TokenType.Open);
                case "or": return new Token(TokenType.Or);
                case "print": return new Token(TokenType.Print);
                case "receive": return new Token(TokenType.Receive);
                case "remove": return new Token(TokenType.Remove);
                case "rename": return new Token(TokenType.Rename);
                case "run": return new Token(TokenType.Run);
                case "select": return new Token(TokenType.Select);
                case "set": return new Token(TokenType.Set);
                case "this": return new Token(TokenType.This);
                case "to": return new Token(TokenType.To);
                case "while": return new Token(TokenType.While);
                case "xor": return new Token(TokenType.Xor);
            }
            return new Token(TokenType.Variable, code);
        }
    }
}


