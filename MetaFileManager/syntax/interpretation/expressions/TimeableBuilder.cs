using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.expressions.time;
using Uroboros.syntax.variables;
using Uroboros.syntax.interpretation.functions;
using Uroboros.syntax.variables.time;

namespace Uroboros.syntax.interpretation.expressions
{
    class TimeableBuilder
    {
        public static string[] KEYWORDS_SINGLE = new string[] { "century", "decade", "year", "month", "week", "day", "hour", "minute", "second" };
        public static string[] KEYWORDS_MULTIPLE = new string[] { "centuries", "decades", "years", "months", "weeks", "days", "hours", "minutes", "seconds" };
        public static string[] MONTHS = new string[] { "january", "february", "march", "april", "may", "june", 
            "july", "august", "september", "october", "november", "december" };

        public static ITimeable Build(List<Token> tokens)
        {
            // remove first and last bracket if it is there
            while (tokens[0].GetTokenType().Equals(TokenType.BracketOn) && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff) &&
                !Brackets.ContainsIndependentBracketsPairs(tokens, BracketsType.Normal))
            {
                List<Token> tokensCopy = tokens.Select(t => t.Clone()).ToList();
                tokensCopy.RemoveAt(tokens.Count - 1);
                tokensCopy.RemoveAt(0);
                tokens = tokensCopy;
            }

            // try to build simple one-token Timeable
            if (tokens.Count == 1)
            {
                if (tokens[0].GetTokenType().Equals(TokenType.Variable))
                {
                    string str = tokens[0].GetContent();
                    if (InterVariables.GetInstance().Contains(str, InterVarType.Time))
                        return new TimeVariableRefer(str);
                }
            }

            //try to build time function
            if (tokens.Count > 2 && tokens[0].GetTokenType().Equals(TokenType.Variable) && tokens[1].GetTokenType().Equals(TokenType.BracketOn)
                && tokens[tokens.Count - 1].GetTokenType().Equals(TokenType.BracketOff))
            {
                ITimeable itim = TimeFunction.Build(tokens);
                if (!itim.IsNull())
                    return itim;
            }

            // try to build time ternary
            if (TernaryBuilder.IsPossibleTernary(tokens))
            {
                ITimeable itim = TernaryBuilder.BuildTimeTernary(tokens);
                if (!itim.IsNull())
                    return itim;
            }

            // try to build relative time expression
            if (tokens.Where(t => IsTimeDirection(t)).Count() > 0)
            {
                ITimeable itim = BuildRelativeTime(tokens);
                if (!itim.IsNull())
                    return itim;
            }

            if (HasOneComma(tokens))
            {
                // try to build Timeable from date and clock
                ITimeable itim = BuildFromDateAndClock(tokens);
                if (!itim.IsNull())
                    return itim;
            }
            else
            {
                // try to build Timeable from date only
                if (ContainMonth(tokens))
                {
                    ITimeable itim = BuildFromDate(tokens);
                    if (!itim.IsNull())
                        return itim;
                }

                // try to build Timeable from clock only
                if (ContainSemicolons(tokens))
                {
                    ITimeable itim = BuildFromClock(tokens);
                    if (!itim.IsNull())
                        return itim;
                }
            }

            return null;
        }

        public static ITimeable BuildRelativeTime(List<Token> tokens)
        {
            List<RelativeTimeStruct> relativeTimes = new List<RelativeTimeStruct>();
            List<Token> currentTokens = new List<Token>();

            foreach (Token tok in tokens)
            {
                currentTokens.Add(tok);

                if (IsTimeDirection(tok))
                {
                    List<RelativeTimeStruct> rtss = BuildRelativeTimeStructs(currentTokens.Take(currentTokens.Count - 1).ToList(), currentTokens.Last());
                    if (rtss.IsNull())
                        return null;
                    else
                        relativeTimes.AddRange(rtss);
                    currentTokens.Clear();
                }
            }

            if (currentTokens.Count == 0)
                throw new SyntaxErrorException("ERROR! Relative time expression do not have definition for reference time.");

            ITimeable itim = TimeableBuilder.Build(currentTokens);
            if (itim.IsNull())
                return null;

            return new RelativeTimeExpression(relativeTimes, itim);
        }

        private static List<RelativeTimeStruct> BuildRelativeTimeStructs(List<Token> tokens, Token last)
        {
            TimeDirection direction = last.GetTokenType().Equals(TokenType.After) ? TimeDirection.After : TimeDirection.Before;

            if (tokens.Count == 0)
                throw new SyntaxErrorException("ERROR! Relative time expression do not have definition for time length.");

            List<RelativeTimeStruct> relativeTimes = new List<RelativeTimeStruct>();

            List<Token> currentTokens = new List<Token>();
            foreach (Token tok in tokens)
            {
                currentTokens.Add(tok);

                if (IsAnyKeyword(tok))
                {
                    RelativeTimeStruct rts = BuildSingleRTS(currentTokens.Take(currentTokens.Count - 1).ToList(), currentTokens.Last(), direction);
                    if (rts.IsNull())
                        return null;
                    else
                        relativeTimes.Add(rts);
                    currentTokens.Clear();
                }
            }

            if (currentTokens.Count > 0)
            {
                RelativeTimeStruct rts = BuildSingleRTS(currentTokens.Take(currentTokens.Count - 1).ToList(), currentTokens.Last(), direction);
                if (rts.IsNull())
                    return null;
                else
                    relativeTimes.Add(rts);
            }

            return relativeTimes;
        }

        private static RelativeTimeStruct BuildSingleRTS(List<Token> tokens, Token tok, TimeDirection direction)
        {
            if (IsSingleKeyword(tok))
            {
                if (tokens.Count == 1 && tokens[0].GetTokenType().Equals(TokenType.NumericConstant) && tokens[0].GetNumericContent() == 1)
                {
                    switch (tok.GetContent().ToLower())
                    {
                        case "century":
                            return new RelativeTimeStruct(RelativeTimeType.Centuries, new NumericConstant(1), direction);
                        case "decade":
                            return new RelativeTimeStruct(RelativeTimeType.Decades, new NumericConstant(1), direction);
                        case "year":
                            return new RelativeTimeStruct(RelativeTimeType.Years, new NumericConstant(1), direction);
                        case "month":
                            return new RelativeTimeStruct(RelativeTimeType.Months, new NumericConstant(1), direction);
                        case "week":
                            return new RelativeTimeStruct(RelativeTimeType.Weeks, new NumericConstant(1), direction);
                        case "day":
                            return new RelativeTimeStruct(RelativeTimeType.Days, new NumericConstant(1), direction);
                        case "hour":
                            return new RelativeTimeStruct(RelativeTimeType.Hours, new NumericConstant(1), direction);
                        case "minute":
                            return new RelativeTimeStruct(RelativeTimeType.Minutes, new NumericConstant(1), direction);
                        case "second":
                            return new RelativeTimeStruct(RelativeTimeType.Seconds, new NumericConstant(1), direction);
                    }
                }
                else
                    return null;
            }
            else
            {
                INumerable inum = NumerableBuilder.Build(tokens);
                if (inum.IsNull())
                    return null;
                else
                {
                    switch (tok.GetContent().ToLower())
                    {
                        case "centuries":
                            return new RelativeTimeStruct(RelativeTimeType.Centuries, inum, direction);
                        case "decades":
                            return new RelativeTimeStruct(RelativeTimeType.Decades, inum, direction);
                        case "years":
                            return new RelativeTimeStruct(RelativeTimeType.Years, inum, direction);
                        case "months":
                            return new RelativeTimeStruct(RelativeTimeType.Months, inum, direction);
                        case "weeks":
                            return new RelativeTimeStruct(RelativeTimeType.Weeks, inum, direction);
                        case "days":
                            return new RelativeTimeStruct(RelativeTimeType.Days, inum, direction);
                        case "hours":
                            return new RelativeTimeStruct(RelativeTimeType.Hours, inum, direction);
                        case "minutes":
                            return new RelativeTimeStruct(RelativeTimeType.Minutes, inum, direction);
                        case "seconds":
                            return new RelativeTimeStruct(RelativeTimeType.Seconds, inum, direction);
                    }
                }
            }

            return null;
        }

        private static ITimeable BuildFromDate(List<Token> tokens)
        {
            decimal month = 1;
            List<Token> leftPart = new List<Token>();
            List<Token> rightPart = new List<Token>();
            bool pastMonth = false;

            foreach (Token tok in tokens)
            {
                if (IsMonth(tok))
                {
                    pastMonth = true;
                    month = StringToMonth(tok.GetContent());
                }
                else
                {
                    if (pastMonth)
                        rightPart.Add(tok);
                    else
                        leftPart.Add(tok);
                }
            }

            if (leftPart.Count == 0)
                return null;
            
            INumerable day = NumerableBuilder.Build(leftPart);
            if (day.IsNull())
                return null;

            // try to build date without year
            if (rightPart.Count == 0)
            {
                if (day is NumericConstant)
                    return new Time(new ClockEmpty(), new DayConstant(day.ToNumber()), month, new YearNow());
                else
                    return new Time(new ClockEmpty(), new DayNumerable(day), month, new YearNow());
            }

            INumerable year = NumerableBuilder.Build(rightPart);

            if (year.IsNull())
                return null;

            if (day is NumericConstant)
            {
                if (year is NumericConstant)
                    return new Time(new ClockEmpty(), new DayConstant(day.ToNumber()), month, new YearConstant(year.ToNumber()));
                else
                    return new Time(new ClockEmpty(), new DayConstant(day.ToNumber()), month, new YearNumerable(year));
            }
            else
            {
                if (year is NumericConstant)
                    return new Time(new ClockEmpty(), new DayNumerable(day), month, new YearConstant(year.ToNumber()));
                else
                    return new Time(new ClockEmpty(), new DayNumerable(day), month, new YearNumerable(year));
            }
        }

        private static ITimeable BuildFromClock(List<Token> tokens)
        {
            IClock clock = BuildClock(tokens);
            if (clock.IsNull())
                return null;

            return new Time(clock, new DayNow(), DateTime.Now.Month, new YearNow());
        }

        private static IClock BuildClock(List<Token> tokens)
        {
            int semicolons = NumberOfColonsOutsideOfBrackets(tokens);

            if (semicolons == 1)
            {
                List<Token> leftPart = new List<Token>();
                List<Token> rightPart = new List<Token>();
                int level = 0;
                bool pastSemicolon = false;
                foreach (Token tok in tokens)
                {
                    if (tok.GetTokenType().Equals(TokenType.BracketOn))
                        level++;
                    if (tok.GetTokenType().Equals(TokenType.BracketOff))
                        level--;

                    if (tok.GetTokenType().Equals(TokenType.Colon) && level == 0)
                        pastSemicolon = true;
                    else
                    {
                        if (pastSemicolon)
                            rightPart.Add(tok);
                        else
                            leftPart.Add(tok);
                    }
                }

                INumerable inumLeft = NumerableBuilder.Build(leftPart);
                if (inumLeft.IsNull())
                    return null;
                INumerable inumRight = NumerableBuilder.Build(rightPart);
                if (inumRight.IsNull())
                    return null;

                if (inumLeft is NumericConstant && inumRight is NumericConstant)
                    return new ClockConstant(inumLeft.ToNumber(), inumRight.ToNumber(), 0);

                return new ClockWithoutSeconds(inumLeft, inumRight);
            }
            else
            {
                List<Token> leftPart = new List<Token>();
                List<Token> middlePart = new List<Token>();
                List<Token> rightPart = new List<Token>();
                int level = 0;
                int currentPart = 1;
                foreach (Token tok in tokens)
                {
                    if (tok.GetTokenType().Equals(TokenType.BracketOn))
                        level++;
                    if (tok.GetTokenType().Equals(TokenType.BracketOff))
                        level--;

                    if (tok.GetTokenType().Equals(TokenType.Colon) && level == 0)
                        currentPart++;
                    else
                    {
                        if (currentPart == 1)
                            leftPart.Add(tok);
                        else if (currentPart == 2)
                            middlePart.Add(tok);
                        else
                            rightPart.Add(tok);
                    }
                }

                INumerable inumLeft = NumerableBuilder.Build(leftPart);
                if (inumLeft.IsNull())
                    return null;
                INumerable inumMiddle = NumerableBuilder.Build(middlePart);
                if (inumMiddle.IsNull())
                    return null;
                INumerable inumRight = NumerableBuilder.Build(rightPart);
                if (inumRight.IsNull())
                    return null;

                if (inumLeft is NumericConstant && inumMiddle is NumericConstant && inumRight is NumericConstant)
                    return new ClockConstant(inumLeft.ToNumber(), inumMiddle.ToNumber(), inumRight.ToNumber());

                return new ClockNumerables(inumLeft,inumMiddle, inumRight);
            }
        }

        private static ITimeable BuildFromDateAndClock(List<Token> tokens)
        {
            List<Token> beforeComma = new List<Token>();
            List<Token> afterComma = new List<Token>();
            int level = 0;
            bool pastComma = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tok.GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tok.GetTokenType().Equals(TokenType.Comma) && level == 0)
                    pastComma = true;
                else
                {
                    if (pastComma)
                        afterComma.Add(tok);
                    else
                        beforeComma.Add(tok);
                }
            }

            ITimeable itim = TimeableBuilder.Build(beforeComma);
            if (itim.IsNull())
                return null;

            IClock clock = BuildClock(afterComma);
            if (clock.IsNull())
                return null;

            if (itim is Time)
            {
                (itim as Time).SetNewClock(clock);
                return itim;
            }
            else
                return new TimeableWithClock(itim, clock);
        }

        private static bool IsAnyKeyword(Token tok)
        {
            if (KEYWORDS_SINGLE.Contains(tok.GetContent().ToLower()))
                return true;
            if (KEYWORDS_MULTIPLE.Contains(tok.GetContent().ToLower()))
                return true;
            return false;
        }

        private static bool IsSingleKeyword(Token tok)
        {
            if (KEYWORDS_SINGLE.Contains(tok.GetContent().ToLower()))
                return true;
            return false;
        }

        private static bool IsTimeDirection(Token tok)
        {
            if (tok.GetTokenType().Equals(TokenType.After)
                || tok.GetTokenType().Equals(TokenType.Before))
                return true;
            else
                return false;
        }

        private static bool IsMonth(Token tok)
        {
            if (MONTHS.Contains(tok.GetContent().ToLower()))
                return true;
            return false;
        }

        private static int StringToMonth(string s)
        {
            switch (s.ToLower())
            {
                case "january": return 1;
                case "february": return 2;
                case "march": return 3;
                case "april": return 4;
                case "may": return 5;
                case "june": return 6;
                case "july": return 7;
                case "august": return 8;
                case "september": return 9;
                case "october": return 10;
                case "november": return 11;
                case "december": return 12;
            }
            return 0;
        }

        private static bool ContainMonth(List<Token>tokens)
        {
            return tokens.Where(t => IsMonth(t)).Count() == 1;
        }

        private static bool ContainSemicolons(List<Token> tokens)
        {
            int semicolons = NumberOfColonsOutsideOfBrackets(tokens);
            return semicolons == 1 || semicolons == 2;
        }

        private static int NumberOfColonsOutsideOfBrackets(List<Token> tokens)
        {
            int result = 0;
            int level = 0;

            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tok.GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tok.GetTokenType().Equals(TokenType.Colon) && level == 0)
                    result++;
            }
            return result;
        }

        private static bool HasOneComma(List<Token> tokens)
        {
            bool found = false;
            int level = 0;

            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.BracketOn))
                    level++;
                if (tok.GetTokenType().Equals(TokenType.BracketOff))
                    level--;

                if (tok.GetTokenType().Equals(TokenType.Comma) && level == 0)
                {
                    if (found)
                        return false;
                    found = true;
                }
            }
            return found;
        }
    }
}
