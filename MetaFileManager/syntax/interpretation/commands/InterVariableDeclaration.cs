﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.commands;
using Uroboros.syntax.reading;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros.syntax.interpretation.commands
{
    class InterVariableDeclaration
    {
        public static ICommand Build(List<Token> tokens)
        {
            string name = tokens[0].GetContent();
            tokens.RemoveAt(0);
            tokens.RemoveAt(0);

            if(tokens.Count == 0)
                throw new SyntaxErrorException("ERROR! Variable " + name +" has no assigned value.");

            if (!InterVariables.GetInstance().Contains(name))
            {
                IListable value = ListableBuilder.Build(tokens);
                if (value is NullVariable)
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
            else
            {
                InterVar ivar = InterVariables.GetInstance().GetVar(name);

                if (ivar.IsBool())
                {
                    IBoolable value = BoolableBuilder.Build(tokens);
                    if (value is NullVariable)
                        throw new SyntaxErrorException("ERROR! Logical value cannot be assigned to variable " + name + ".");
                    return new BoolDeclaration(name, value);
                }
                else
                {
                    if (ivar.IsNumber())
                    {
                        INumerable value = NumerableBuilder.Build(tokens);
                        if (value is NullVariable)
                            throw new SyntaxErrorException("ERROR! Numeric value cannot be assigned to variable " + name + ".");
                        return new NumericDeclaration(name, value);
                    }
                    else
                    {
                        if (ivar.IsString())
                        {
                            IStringable value = StringableBuilder.Build(tokens);
                            if (value is NullVariable)
                                throw new SyntaxErrorException("ERROR! String value cannot be assigned to variable " + name + ".");
                            return new StringDeclaration(name, value);
                        }
                        else
                        {
                            IListable value = ListableBuilder.Build(tokens);
                            if (value is NullVariable)
                                throw new SyntaxErrorException("ERROR! List value cannot be assigned to variable " + name + ".");
                            return new ListDeclaration(name, value);
                        }
                    }
                }
            }
        }
    }
}