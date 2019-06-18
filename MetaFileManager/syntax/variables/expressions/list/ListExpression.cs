using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables.expressions.list.subcommands;
using DivineScript.syntax.variables.refers;

namespace DivineScript.syntax.variables.expressions.list
{
    class ListExpression: IListable
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


        public List<string> ToList()
        {
            List<string> result = elements.ToList();

            foreach (ISubcommand subcom in subcommands)
            {
                if (subcom is Where)
                {
                    List<string> newresult = new List<string>();
                    foreach (string s in result)
                    {
                        if ((subcom as Where).GetValue())
                        {
                            newresult.Add(s);
                        }
                    }
                    result = newresult;
                }
                if (subcom is OrderBy)
                {

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
