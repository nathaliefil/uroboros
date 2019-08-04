using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.from_file;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByExecutor
    {
        public static List<string> OrderBy(List<string> source, OrderBy orders)
        {
            if (source.Count == 1 || orders.GetVariables().Count == 0)
                return source;

            OrderByStruct first = orders.GetVariables()[0];

            List<string> orderedByFirstVar = OrderBySingleVariable(source, first);

            if (orders.GetVariables().Count == 0)
            {
                return orderedByFirstVar;
            }
            else
            {
                List<string> result = new List<string>();
                List<string> temporary = new List<string> { orderedByFirstVar[0] };

                for (int i = 1; i < orderedByFirstVar.Count; i++)
                {
                    if (OrderByComparator.Equals(orderedByFirstVar[i - 1], orderedByFirstVar[i], first))
                        temporary.Add(orderedByFirstVar[i]);
                    else
                    {
                        if (temporary.Count == 1)
                            result.Add(temporary[0]);
                        else
                            result.AddRange(OrderBy(temporary, orders.CopyWithoutFirstVariable()));

                        temporary.Clear();
                        temporary.Add(orderedByFirstVar[i]);
                    }
                }
                if (temporary.Count == 1)
                    result.Add(temporary[0]);
                else
                    result.AddRange(OrderBy(temporary, orders.CopyWithoutFirstVariable()));


                return result;
            }
        }

        public static List<string> OrderBySingleVariable(List<string> source, OrderByStruct order)
        {
            switch (order.GetVariable())
            {
                case OrderByVariable.Extension:
                    source = source.OrderBy(s => FileInnerVariable.GetExtension(s)).ToList();
                    break;

                case OrderByVariable.Fullname:
                    source = source.OrderBy(s => FileInnerVariable.GetFullname(s)).ToList();
                    break;

                case OrderByVariable.Name:
                    source = source.OrderBy(s => FileInnerVariable.GetName(s)).ToList();
                    break;

                case OrderByVariable.Size:
                    source = source.OrderBy(s => FileInnerVariable.GetSize(s)).ToList();
                    break;

                case OrderByVariable.Access:
                    {
                        if (order is OrderByStructTime)
                            source = source.OrderBy(s => DateExtractor.GetVariable(FileInnerVariable.GetAccess(s),
                                (order as OrderByStructTime).GetTimeVariable())).ToList();
                        else if (order is OrderByStructDate)
                            source = source.OrderBy(s => DateExtractor.DateToInt(FileInnerVariable.GetAccess(s))).ToList();
                        else if (order is OrderByStructClock)
                            source = source.OrderBy(s => DateExtractor.ClockToInt(FileInnerVariable.GetAccess(s))).ToList();
                        else
                            source = source.OrderBy(s => FileInnerVariable.GetAccess(s)).ToList();
                        break;
                    }

                case OrderByVariable.Creation:
                    {
                        if (order is OrderByStructTime)
                            source = source.OrderBy(s => DateExtractor.GetVariable(FileInnerVariable.GetCreation(s),
                                (order as OrderByStructTime).GetTimeVariable())).ToList();
                        else if (order is OrderByStructDate)
                            source = source.OrderBy(s => DateExtractor.DateToInt(FileInnerVariable.GetCreation(s))).ToList();
                        else if (order is OrderByStructClock)
                            source = source.OrderBy(s => DateExtractor.ClockToInt(FileInnerVariable.GetCreation(s))).ToList();
                        else
                            source = source.OrderBy(s => FileInnerVariable.GetCreation(s)).ToList();
                        break;
                    }

                case OrderByVariable.Modification:
                    {
                        if (order is OrderByStructTime)
                            source = source.OrderBy(s => DateExtractor.GetVariable(FileInnerVariable.GetModification(s),
                                (order as OrderByStructTime).GetTimeVariable())).ToList();
                        else if (order is OrderByStructDate)
                            source = source.OrderBy(s => DateExtractor.DateToInt(FileInnerVariable.GetModification(s))).ToList();
                        else if (order is OrderByStructClock)
                            source = source.OrderBy(s => DateExtractor.ClockToInt(FileInnerVariable.GetModification(s))).ToList();
                        else
                            source = source.OrderBy(s => FileInnerVariable.GetModification(s)).ToList();
                        break;
                    }
            }

            if (order.GetOrderType().Equals(OrderByType.DESC))
                source.Reverse();

            return source;
        }
    }
}
