using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.reading
{
    public class Token
    {
        private TokenType type;
        private string content;
        private decimal numericContent;

        public TokenType GetTokenType()
        {
            return type;
        }

        public string GetContent()
        {
            return content;
        }

        public decimal GetNumericContent()
        {
            return numericContent;
        }

        public Token Clone()
        {
            if (content == null)
                return new Token(type);
            else
                return new Token(type, content);
        }

        public void SetContent(string content)
        {
            this.content = content;
        }

        public void SetToNumericConstant(decimal value)
        {
            type = TokenType.NumericConstant;
            numericContent = value;
        }

        public void PointToComma()
        {
            content = content.Replace('.', ',');
        }

        public Token(TokenType type)
        {
            this.type = type;
            this.content = "";
        }

        public Token(TokenType type, string content)
        {
            this.type = type;
            this.content = content;
        }

        // method only for testing
        public string Print()
        {
            if (content.Equals(""))
                return "Token." + type.ToString();
            else
                return "Token." + type.ToString() + ": " + content;
        }
    }
}
