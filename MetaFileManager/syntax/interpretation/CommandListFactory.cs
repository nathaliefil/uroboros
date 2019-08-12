using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.reading;
using Uroboros.syntax.commands;
using Uroboros.syntax.commands.structures;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.interpretation.expressions;
using Uroboros.syntax.interpretation.vars_range;

namespace Uroboros.syntax.interpretation
{
    class CommandListFactory
    {
        public static List<ICommand> Build(List<Token> tokens)
        {
            List<Token> currentTokens = new List<Token>();
            List<ICommand> commands = new List<ICommand>();

            foreach (Token tok in tokens)
            {
                if (tok.GetTokenType().Equals(TokenType.Semicolon))
                {
                    if (currentTokens.Count > 0)
                    {
                        commands.Add(SingleCommandFactory.Build(currentTokens));
                        currentTokens.Clear();
                    }
                }
                else if (tok.GetTokenType().Equals(TokenType.CurlyBracketOff))
                {
                    if (currentTokens.Count > 0)
                    {
                        commands.Add(SingleCommandFactory.Build(currentTokens));
                        currentTokens.Clear();
                    }
                    commands.Add(new BracketOff());
                    InterVariables.GetInstance().BracketsDown();
                }
                else if (tok.GetTokenType().Equals(TokenType.CurlyBracketOn))
                {
                    if (currentTokens.Count == 0)
                    {
                        commands.Add(new EmptyOpenning());
                    }
                    else
                    {
                        TokenType first = currentTokens.First().GetTokenType();
                        if (first.Equals(TokenType.If))
                        {
                            currentTokens.RemoveAt(0);
                            if (currentTokens.Count == 0)
                                throw new SyntaxErrorException("ERROR! Expression 'if' is empty.");

                            IBoolable iboo = BoolableBuilder.Build(currentTokens);
                            if (iboo.IsNull())
                                throw new SyntaxErrorException("ERROR! There is something wrong with expression 'if'.");

                            commands.Add(new IfOpenning(iboo, commands.Count));
                            currentTokens.Clear();
                        }
                        else if (first.Equals(TokenType.Else))
                        {
                            currentTokens.RemoveAt(0);
                            if (currentTokens.Count == 0)
                                commands.Add(new ElseOpenning());
                            else
                                throw new SyntaxErrorException("ERROR! Expression 'else' contains not necessary code.");
                        }
                        else if (first.Equals(TokenType.While))
                        {
                            currentTokens.RemoveAt(0);
                            if (currentTokens.Count == 0)
                                throw new SyntaxErrorException("ERROR! Expression 'while' is empty.");

                            IBoolable iboo = BoolableBuilder.Build(currentTokens);
                            if (iboo.IsNull())
                                throw new SyntaxErrorException("ERROR! There is something wrong with expression 'while'.");

                            commands.Add(new WhileOpenning(iboo, commands.Count));
                            currentTokens.Clear();
                        }
                        else if (first.Equals(TokenType.Inside))
                        {
                            currentTokens.RemoveAt(0);
                            if (currentTokens.Count == 0)
                                throw new SyntaxErrorException("ERROR! Expression 'inside' is empty.");

                            IListable ilist = ListableBuilder.Build(currentTokens);
                            if (ilist.IsNull())
                                throw new SyntaxErrorException("ERROR! There is something wrong with expression 'inside'.");

                            commands.Add(new InsideOpenning(ilist, commands.Count));
                            currentTokens.Clear();
                        }
                        else
                        {
                            IListable ilist = ListableBuilder.Build(currentTokens);
                            if (ilist.IsNull())
                                throw new SyntaxErrorException("ERROR! There is something wrong with List Loop / Numeric Loop.");

                            if (ilist is INumerable)
                                commands.Add(new NumericLoopOpenning(ilist as INumerable, commands.Count));
                            else
                                commands.Add(new ListLoopOpenning(ilist, commands.Count));

                            currentTokens.Clear();
                        }
                    }
                    InterVariables.GetInstance().BracketsUp();
                }
                else if (tok.GetTokenType().Equals(TokenType.BigArrow))
                {

                }
                else
                {
                    currentTokens.Add(tok);
                }
            }

            if (currentTokens.Count > 0)
            {
                commands.Add(SingleCommandFactory.Build(currentTokens));
                currentTokens.Clear();
            }


            return commands;
        }
    }


}
