using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;
using DivineScript.syntax.variables.expressions.list.subcommands;

namespace DivineScript.syntax.variables.expressions.list
{
    class ListExpression: Variable, IListable
    {
        private IListable elements;
        private List<ISubcommand> subcommands;


        public ListExpression(IListable elements)
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
            List<string> result = elements.ToList() ;

            foreach (ISubcommand subcom in subcommands)
            {
                if (subcom is Where)
                {
                    foreach (string s in result)
                    {
                        if (!(subcom as Where).GetValue())
                        {
                            result.Remove(s);
                        }
                    }
                }
                if (subcom is OrderBy)
                {

                }
                if (subcom is NumericSubcommand)
                {
                    int number = (subcom as NumericSubcommand).GetValue();
                    if (number > 0)
                    {
                        switch ((subcom as NumericSubcommand).GetNumericSubcommandType())
                        {
                            case NumericSubcommandType.First:
                            {
                                // todo
                                break;
                            }
                            case NumericSubcommandType.Last:
                            {
                                // todo
                                break;
                            }
                            case NumericSubcommandType.Skip:
                            {
                                //todo
                                break;
                            }
                        }
                    }
                }

            }

            return elements.ToList(); ;
        }

    }
}
