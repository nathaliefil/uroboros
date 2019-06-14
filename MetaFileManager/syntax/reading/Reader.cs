using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax;

namespace DivineScript.syntax.reading
{
    class Reader
    {
        private static char[] keySigns = new char[] 
                {',', '!', '=', '(', ')', '{', '}', ';', ':',
                 '-', '+', '*', '"', '%', '/', '&', '|', '^', ' ', '<', '>'};


        public static List<Token> CreateTokenlist(string code)
        {
            code = Comments.Remove(code);
            code = EmptySpaces.Compress(code);
            Console.WriteLine("ridin");

            List<Token> tokens = new List <Token>();
            StringBuilder stringb = new StringBuilder();
            bool quotationOn = false;

            for (int i = 0; i < code.Length; i++)
            {
                if (keySigns.Contains(code[i]))
                {
                    if (code[i].Equals('"'))
                    {
                        quotationOn = !quotationOn;
                        stringb.Append('"');
                        if (!quotationOn)
                        {
                            tokens.Add(TokenFactory.BuildQuotationToken(stringb.ToString()));
                            stringb.Clear();
                        }
                    }
                    else
                    {
                        if (!quotationOn)
                        {
                            if (stringb.ToString().Trim().Length > 0)
                            {
                                tokens.Add(TokenFactory.Build(stringb.ToString()));
                            }
                            stringb.Clear();
                            if (!code[i].Equals(' '))
                            {
                                tokens.Add(TokenFactory.Build(code[i]));
                            }
                        }
                        else
                        {
                            stringb.Append(code[i]);
                        }
                    }
                }
                else
                {
                    stringb.Append(code[i]);
                }
            }
            if (stringb.ToString().Trim().Length > 0)
            {
                tokens.Add(TokenFactory.Build(stringb.ToString()));
            }

            int bracketsOn = tokens.Where(t => t.GetTokenType()==TokenType.BracketOn).Count();
            int bracketsOff = tokens.Where(t => t.GetTokenType() == TokenType.BracketOff).Count();
            int curlyBracketsOn = tokens.Where(t => t.GetTokenType() == TokenType.CurlyBracketOn).Count();
            int curlyBracketsOff = tokens.Where(t => t.GetTokenType() == TokenType.CurlyBracketOff).Count();

            if (tokens.Count() == 0)
                throw new SyntaxErrorException("ERROR! Code is empty.");
            if (bracketsOn != bracketsOff)
                throw new SyntaxErrorException("ERROR! Check brackets ( ).");
            if (curlyBracketsOn != curlyBracketsOff)
                throw new SyntaxErrorException("ERROR! Check curly brackets { }.");

            tokens = TokenModifier.VariablesToNumeric(tokens);
            tokens = TokenModifier.MergeTokens(tokens);

            return tokens;
        }

    }
}
