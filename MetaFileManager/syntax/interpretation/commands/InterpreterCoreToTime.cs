using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.commands.core;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterCoreToTime
    {
        public static ICommand Build(List<Token> tokens)
        {
            TokenType type = tokens[0].GetTokenType();
            string name = type.Equals(TokenType.Recreate) ? "recreate" : (type.Equals(TokenType.Remodify) ? "remodify" : "reaccess");
            tokens.RemoveAt(0);

            if (tokens.Where(t => t.GetTokenType().Equals(TokenType.To)).Count() > 1)
                throw new SyntaxErrorException("ERROR! In " + name + " command keyword 'to' occurs too many times.");

            List<Token> part1 = new List<Token>();
            List<Token> part2 = new List<Token>();
            bool pastTo = false;
            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.To))
                    pastTo = true;
                else
                {
                    if (pastTo)
                        part2.Add(tok);
                    else
                        part1.Add(tok);
                }
            }

            if (part2.Count == 0)
                throw new SyntaxErrorException("ERROR! Command " + name + " is too short and do not contain all necessary information.");

            ITimeable expression2 = TimeableBuilder.Build(part2);

            if (expression2.IsNull())
                throw new SyntaxErrorException("ERROR! Second part of command " + name + " cannot be read as time.");

            if (part1.Count == 0)
                return BuildSimple(type, expression2);
            else
            {
                IListable expression1 = ListableBuilder.Build(part1);
                if (expression1.IsNull())
                    throw new SyntaxErrorException("ERROR! First part of command " + name + " cannot be read as list.");
                return BuildComplex(type, expression1, expression2);
            }
        }

        private static ICommand BuildSimple(TokenType type, ITimeable time)
        {
            switch (type)
            {
                case TokenType.Recreate:
                    return new RecreateTo(new StringVariableRefer("this"), time);
                case TokenType.Remodify:
                    return new RemodifyTo(new StringVariableRefer("this"), time);
                case TokenType.Reaccess:
                    return new ReaccessTo(new StringVariableRefer("this"), time);
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }

        private static ICommand BuildComplex(TokenType type, IListable list, ITimeable time)
        {
            switch (type)
            {
                case TokenType.Recreate:
                    return new RecreateTo(list, time);
                case TokenType.Remodify:
                    return new RemodifyTo(list, time);
                case TokenType.Reaccess:
                    return new ReaccessTo(list, time);
            }
            throw new SyntaxErrorException("ERROR! Command not indentified."); // this is never thrown
        }
    }
}