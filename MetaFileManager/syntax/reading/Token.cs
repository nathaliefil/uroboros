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
        private SizeSufix sizeUnit;

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
            switch (sizeUnit)
            {
                case SizeSufix.None:
                    return Convert.ToDecimal(content);
                case SizeSufix.KB:
                    return Convert.ToDecimal(content) * 1024;
                case SizeSufix.MB:
                    return Convert.ToDecimal(content) * 1048576;
                case SizeSufix.GB:
                    return Convert.ToDecimal(content) * 1073741824;
                case SizeSufix.TB:
                    return Convert.ToDecimal(content) * 1125899906842624;
                case SizeSufix.PB:
                    return Convert.ToDecimal(content) * 1152921504606846976;
                case SizeSufix.K:
                    return Convert.ToDecimal(content) * 1000;
                case SizeSufix.KK:
                    return Convert.ToDecimal(content) * 1000000;
            }
            return Convert.ToDecimal(content);
        }

        public Token Clone()
        {
            if (content == null)
                if (sizeUnit == SizeSufix.None)
                    return new Token(type);
                else
                    return new Token(type, sizeUnit);
            else
                if (sizeUnit == SizeSufix.None)
                    return new Token(type, content);
                else
                    return new Token(type, content, sizeUnit);
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
            this.sizeUnit = sizeU;
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
            this.sizeUnit = SizeSufix.None;
        }

        public Token(TokenType type, string content)
        {
            this.type = type;
            this.content = content;
            this.sizeUnit = SizeSufix.None;
        }

        public Token(TokenType type, SizeSufix sizeUnit)
        {
            this.type = type;
            this.content = "";
            this.sizeUnit = sizeUnit;
        }

        public Token(TokenType type, string content, SizeSufix sizeUnit)
        {
            this.type = type;
            this.content = content;
            this.sizeUnit = sizeUnit;
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
