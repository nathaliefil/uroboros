using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Uroboros.syntax.reading
{
    public class Token
    {
        private TokenType type;
        private string content;
        private SizeSufix sizeSufix;

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
            return Convert.ToDecimal(content) * SizeUnit.GetMultiplier(sizeSufix);
        }

        public Token Clone()
        {
            if (content == null)
                if (sizeSufix == SizeSufix.None)
                    return new Token(type);
                else
                    return new Token(type, sizeSufix);
            else
                if (sizeSufix == SizeSufix.None)
                    return new Token(type, content);
                else
                    return new Token(type, content, sizeSufix);
        }

        public void SetContent(string content)
        {
            this.content = content;
        }

        public void SetToNumericConstant()
        {
            type = TokenType.NumericConstant;
            content = content.Replace('.', ',');
        }

        public void ChangeTokenType()
        {
            if (type.Equals(TokenType.Is))
                type = TokenType.Equals;
        }

        public void SetToNumericConstant(SizeSufix sizeU)
        {
            this.sizeSufix = sizeU;
            type = TokenType.NumericConstant;
            content = content.Replace('.', ',');
            if (sizeU.Equals(SizeSufix.K))
                content = content.Substring(0, content.Length - 1);
            else
                content = content.Substring(0, content.Length - 2);
        }

        public Token(TokenType type)
        {
            this.type = type;
            this.content = "";
            this.sizeSufix = SizeSufix.None;
        }

        public Token(TokenType type, string content)
        {
            this.type = type;
            this.content = content;
            this.sizeSufix = SizeSufix.None;
        }

        public Token(TokenType type, SizeSufix sizeUnit)
        {
            this.type = type;
            this.content = "";
            this.sizeSufix = sizeUnit;
        }

        public Token(TokenType type, string content, SizeSufix sizeUnit)
        {
            this.type = type;
            this.content = content;
            this.sizeSufix = sizeUnit;
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
