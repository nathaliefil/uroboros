using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax
{
    partial class Syntax
    {
        static string[] POSSIBLE_ORDERS = { "name", "extension", "ext", "fullname",
                "name.length", "extension.length", "ext.length", "fullname.length",
                "size", "creation", "modification", 
                "modification.day", "modification.month", "modification.year",
                "modification.hour", "modification.minute", "modification.second",
                "creation.day", "creation.month", "creation.year",
                "creation.hour", "creation.minute", "creation.second"
                                      };

        static string[] POSSIBLE_SUBTYPE = { "where", "first", "last", "ignore",
                "max", "stop", "order", "each"
                                      };


        private static bool IsAllowedOrderType(string order)
        {
            order = order.ToLower();
            
            if (POSSIBLE_ORDERS.Contains(order))
                return true;
            else
                return false;
        }

        private static bool IsAllowedSubcommandType(string order)
        {
            if (POSSIBLE_SUBTYPE.Contains(order))
                return true;
            else
                return false;
        }


        private static SubcommandType GetSubcommandType(string first, string second)
        {
            if (first.Equals("where"))
            {
                if(second.Equals("not"))
                    return SubcommandType.WhereNot;
                else
                    return SubcommandType.Where;
            }
            if (first.Equals("first"))
            {
                if (second.Equals("part"))
                    return SubcommandType.FirstPart;
                else
                    return SubcommandType.First;
            }
            if (first.Equals("last"))
            {
                if (second.Equals("part"))
                    return SubcommandType.LastPart;
                else
                    return SubcommandType.Last;
            }
            if (first.Equals("order"))
            {
                if (second.Equals("by"))
                    return SubcommandType.Order;
                else
                    return SubcommandType.NULL;
            }
            if (first.Equals("stop"))
            {
                if (second.Equals("not"))
                    return SubcommandType.StopNot;
                else
                    return SubcommandType.Stop;
            }
            if (first.Equals("ignore"))
                return SubcommandType.Ignore;
            if (first.Equals("max"))
                return SubcommandType.Max;
            if (first.Equals("each"))
                return SubcommandType.Each;


            return SubcommandType.NULL;
        }
    }
}
