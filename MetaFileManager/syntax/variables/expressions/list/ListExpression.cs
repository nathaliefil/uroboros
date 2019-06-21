using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.variables.expressions.list.subcommands;
using Uroboros.syntax.variables.refers;
using Uroboros.syntax.variables.from_location;

namespace Uroboros.syntax.variables.expressions.list
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
                    result = Order(result, subcom as OrderBy);
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

        public List<string> Order(List<string> source, OrderBy orders)
        {
            if (orders.GetVariables().Count == 1)
            {
                switch (orders.GetVariables()[0].variable)
                {
                    case OrderByVariable.Creation:
                        source = source.OrderBy(s => FileInnerVariable.GetCreation(s)).ToList();
                        break;

                    case OrderByVariable.Extension:
                        source = source.OrderBy(s => FileInnerVariable.GetExtension(s)).ToList();
                        break;

                    case OrderByVariable.Fullname:
                        source = source.OrderBy(s => FileInnerVariable.GetFullname(s)).ToList();
                        break;

                    case OrderByVariable.Modification:
                        source = source.OrderBy(s => FileInnerVariable.GetModification(s)).ToList();
                        break;

                    case OrderByVariable.Name:
                        source = source.OrderBy(s => FileInnerVariable.GetName(s)).ToList();
                        break;

                    case OrderByVariable.Size:
                        source = source.OrderBy(s => FileInnerVariable.GetSize(s)).ToList();
                        break;
                }
                if (orders.GetVariables()[0].type.Equals(OrderByType.DESC))
                    source.Reverse();
            }
            else
            {
                foreach (OrderByStruct obs in orders.GetVariables())
                {
                    //bool ascending = obs.type.Equals(OrderByType.ASC) ? true : false;


                    ///todo
                    // order by many variables
                    // needs grouping of string
                }
            }


            return source;
        }
    }
}
