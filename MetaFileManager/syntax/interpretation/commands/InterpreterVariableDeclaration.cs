using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.lexer;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.interpretation.vars_range;
using Uroboros.syntax.commands.var;
using Uroboros.syntax.runtime;
using Uroboros.syntax.variables;

namespace Uroboros.syntax.interpretation.commands
{
    class InterpreterVariableDeclaration
    {
        public static ICommand Build(List<Token> tokens)
        {
            string name = tokens[0].GetContent();
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            if(tokens.Count == 0)
                throw new SyntaxErrorException("ERROR! Variable " + name +" has no assigned value.");

            if (name.Contains('.'))
                return BuildWithPoint(tokens, name);

            if (!InterVariables.GetInstance().Contains(name))
            {
                IListable value = ListableBuilder.Build(tokens);
                if (value.IsNull())
                    throw new SyntaxErrorException("ERROR! There are is something wrong with assigning value to variable " + name + ".");

                if (value is IBoolable)
                {
                    InterVariables.GetInstance().Add(name, InterVarType.Bool);
                    return new BoolDeclaration(name, (IBoolable)value);
                }
                else
                {
                    if (value is INumerable)
                    {
                        InterVariables.GetInstance().Add(name, InterVarType.Number);
                        return new NumericDeclaration(name, (INumerable)value);
                    }
                    else
                    {
                        if (value is ITimeable)
                        {
                            InterVariables.GetInstance().Add(name, InterVarType.Time);
                            return new TimeDeclaration(name, (ITimeable)value);
                        }
                        else
                        {
                            if (value is IStringable)
                            {
                                InterVariables.GetInstance().Add(name, InterVarType.String);
                                return new StringDeclaration(name, (IStringable)value);
                            }
                            else
                            {
                                InterVariables.GetInstance().Add(name, InterVarType.List);
                                return new ListDeclaration(name, value);
                            }
                        }
                    }
                }
            }
            else
            {
                InterVar ivar = InterVariables.GetInstance().GetVar(name);

                if (ivar.IsBool())
                {
                    IBoolable value = BoolableBuilder.Build(tokens);
                    if (value.IsNull())
                        throw new SyntaxErrorException("ERROR! Value assigned to variable " + name + " must be logical.");
                    return new BoolDeclaration(name, value);
                }
                else
                {
                    if (ivar.IsNumber())
                    {
                        INumerable value = NumerableBuilder.Build(tokens);
                        if (value.IsNull())
                            throw new SyntaxErrorException("ERROR! Value assigned to variable " + name + " must be numeric.");
                        return new NumericDeclaration(name, value);
                    }
                    else
                    {
                        if (ivar.IsTime())
                        {
                            ITimeable value = TimeableBuilder.Build(tokens);
                            if (value.IsNull())
                                throw new SyntaxErrorException("ERROR! Value assigned to variable " + name + " must be time.");
                            return new TimeDeclaration(name, value);
                        }
                        else
                        {
                            if (ivar.IsString())
                            {
                                IStringable value = StringableBuilder.Build(tokens);
                                if (value.IsNull())
                                    throw new SyntaxErrorException("ERROR! Value assigned to variable " + name + " must be text.");
                                return new StringDeclaration(name, value);
                            }
                            else
                            {
                                IListable value = ListableBuilder.Build(tokens);
                                if (value.IsNull())
                                    throw new SyntaxErrorException("ERROR! Value assigned to variable " + name + " must be list.");
                                return new ListDeclaration(name, value);
                            }
                        }
                    }
                }
            }
        }

        private static ICommand BuildWithPoint(List<Token> tokens, string name)
        {
            if (name.Count(c => c == '.') > 1)
                throw new SyntaxErrorException("ERROR! Variable " + name + " contains multiple dot signs and because of that misguides compiler.");

            string leftSide = name.Substring(0, name.IndexOf('.')).ToLower();
            string rightSide = name.Substring(name.IndexOf('.') + 1).ToLower();

            if (leftSide.Length == 0 || rightSide.Length == 0)
                return null;

            if (InterVariables.GetInstance().Contains(leftSide, InterVarType.Time))
            {
                if (!InterVariables.GetInstance().ContainsChangable(leftSide, InterVarType.Time))
                    throw new SyntaxErrorException("ERROR! Variable " + leftSide + " cannot be modified.");

                INumerable inum = NumerableBuilder.Build(tokens);
                if (inum.IsNull())
                    return null;

                switch (rightSide)
                {
                    case "year":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Year, inum);
                    case "month":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Month, inum);
                    case "day":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Day, inum);
                    case "weekday":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.WeekDay, inum);
                    case "hour":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Hour, inum);
                    case "minute":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Minute, inum);
                    case "second":
                        return new TimeElementDeclaration(leftSide, TimeVariableType.Second, inum);
                    case "clock":
                        throw new SyntaxErrorException("ERROR! Clock of variable " + leftSide + " can be changed only by actualizing hours, minutes and seconds separately.");
                    case "date":
                        throw new SyntaxErrorException("ERROR! Date of variable " + leftSide + " can be changed only by actualizing year, month and day separately.");
                    default:
                        throw new SyntaxErrorException("ERROR! Property " + rightSide + " of variable " + leftSide + " do not exist.");
                }
            }
            else
                throw new SyntaxErrorException("ERROR! Variable " + leftSide + " do not exist.");
        }
    }
}
