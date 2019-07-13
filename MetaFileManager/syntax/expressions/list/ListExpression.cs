using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.list
{
    class ListExpression: DefaultListable
    {
        private ListVariableRefer elements;
        private List<ISubcommand> subcommands;


        public ListExpression(ListVariableRefer elements)
        {
            this.elements = elements;
            subcommands = new List<ISubcommand>();
        }

        public void AddSubcommand(ISubcommand sub)
        {
            subcommands.Add(sub);
        }


        public override List<string> ToList()
        {
            List<string> result = elements.ToList();

            foreach (ISubcommand subcom in subcommands)
            {
                if (subcom is Where)
                {
                    string oldThis = RuntimeVariables.GetInstance().GetValueString("this");
                    List<string> newresult = new List<string>();
                    foreach (string s in result)
                    {
                        RuntimeVariables.GetInstance().Actualize("this", s);
                        if ((subcom as Where).GetValue())
                        {
                            newresult.Add(s);
                        }
                    }
                    RuntimeVariables.GetInstance().Actualize("this", oldThis);
                    result = newresult;
                }
                if (subcom is OrderBy)
                {
                    result = OrderByExecutor.OrderBy(result, subcom as OrderBy);
                }
                if (subcom is With)
                {
                    List<string> elementsFromSubcommands = (subcom as With).GetValue();

                    if ((subcom as With).IsNegated())
                    {
                        //WITHOUT
                        result.RemoveAll(v => (subcom as With).GetValue().Contains(v));
                    }
                    else
                    {
                        //WITH
                        result.AddRange((subcom as With).GetValue());
                    }
                }
                if (subcom is NumericSubcommand)
                {
                    int number = (subcom as NumericSubcommand).GetValue();
                    switch ((subcom as NumericSubcommand).GetNumericSubcommandType())
                    {
                        case NumericSubcommandType.First:
                        {
                            if (number <= 0)
                                return new List<string>();
                            else
                            {
                                if (number < result.Count)
                                    result.RemoveRange(number, result.Count - number);
                            }
                            break;
                        }
                        case NumericSubcommandType.Last:
                        {
                            if (number <= 0)
                                return new List<string>();
                            else
                            {
                                if (number < result.Count)
                                    result.RemoveRange(0, result.Count - number);
                            }
                            break;
                        }
                        case NumericSubcommandType.Skip:
                        {
                            if (number >= result.Count)
                                return new List<string>();
                            else
                            {
                                if (number > 0)
                                    result.RemoveRange(0, number);
                            }
                            break;
                        }
                        case NumericSubcommandType.Each:
                        {
                            if (number > 1)
                            {
                                List<string> newresult = new List<string>();
                                int c = 0;
                                do
                                {
                                    newresult.Add(result[c]);
                                    c += number;
                                } while (c < result.Count);

                                result = newresult;
                            }
                            break;
                        }
                    }
                }
            }
            return result.ToList();
        }
    }
}
