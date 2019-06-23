using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.variables.from_location;

namespace Uroboros.syntax.runtime
{
    class OrderByExecutor
    {
        public static List<string> OrderBy(List<string> source, OrderBy orders)
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
