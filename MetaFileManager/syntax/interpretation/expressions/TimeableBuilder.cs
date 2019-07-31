using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
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
        public static string[] KEYWORDS_SINGLE = new string[] { "year", "month", "week", "day", "hour", "minute", "second" };
        public static string[] KEYWORDS_MULTIPLE = new string[] { "years", "months", "weeks", "days", "hours", "minutes", "seconds" };
        public static string[] MONTHS = new string[] { "january", "february", "march", "april", "may", "june", 
            "july", "august", "september", "october", "november", "december" };

        public static ITimeable Build(List<Token> tokens)
        {
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
                ITimeable itim = InterTimeFunction.Build(tokens);
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

            // try to build Timeable from date
            if (tokens.Where(t => IsMonth(t)).Count() == 1)
            {
                ITimeable itim = BuildFromDate(tokens);
                if (!itim.IsNull())
                    return itim;
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
            bool pastTo = false;

            foreach (Token tok in tokens)
            {
                if (IsMonth(tok))
                {
                    pastTo = true;
                    month = StringToMonth(tok.GetContent());
                }
                else
                {
                    if (pastTo)
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
    }
}
