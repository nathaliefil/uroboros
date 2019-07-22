using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.from_location;
using Uroboros.syntax.variables.from_location.date;

namespace Uroboros.syntax.expressions.list.subcommands.orderby
{
    class OrderByExecutor
    {
        public static List<string> OrderBy(List<string> source, OrderBy orders)
        {

            if (source.Count == 1)
                return source;


            List<string> orderedByFirstVar = OrderBySingleVariable(source, orders.GetVariables()[0]);
            orders.RemoveFirst();

            if (orders.GetVariables().Count == 0)
            {
                return orderedByFirstVar;
            }
            else
            {
                OrderByStruct obs = orders.GetVariables()[0];
                List<string> result = new List<string>();
                List<string> temporary = new List<string> { orderedByFirstVar[0] };

                for (int i = 1; i < orderedByFirstVar.Count; i++)
                {
                    if (OrderByComparator.Equals(orderedByFirstVar[i - 1], orderedByFirstVar[i], obs.variable))
                        temporary.Add(orderedByFirstVar[i]);
                    else
                    {
                        if (temporary.Count == 1)
                            result.Add(temporary[0]);
                        else
                            result.AddRange(OrderBy(temporary, orders));

                        temporary.Clear();
                        temporary.Add(orderedByFirstVar[i]);
                    }
                }
                if (temporary.Count == 1)
                    result.Add(temporary[0]);
                else
                    result.AddRange(OrderBy(temporary, orders));


                return result;
                /*OrderingGroup ogroup = new OrderingGroup(source, orders);
                return ogroup.GetElements();*/

            }
        }

        public static List<string> OrderBySingleVariable(List<string> source, OrderByStruct order)
        {
            switch (order.variable)
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

                case OrderByVariable.CreationYear:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationMonth:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationWeekDay:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationDay:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationHour:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationMinute:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.CreationSecond:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetCreation(s))).ToList();
                    break;

                case OrderByVariable.ModificationYear:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Year, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationMonth:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Month, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationWeekDay:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.WeekDay, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationDay:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Day, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationHour:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Hour, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationMinute:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Minute, FileInnerVariable.GetModification(s))).ToList();
                    break;

                case OrderByVariable.ModificationSecond:
                    source = source.OrderBy(s => DateExtractor.GetVariable(DateVariableType.Second, FileInnerVariable.GetModification(s))).ToList();
                    break;
            }

            if (order.type.Equals(OrderByType.DESC))
                source.Reverse();

            return source;
        }
    }
}
