using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.bools.other
{
    class Like : DefaultBoolable
    {
        private IStringable value;
        private string phrase;

        public Like(IStringable value, string phrase)
        {
            this.value = value;
            this.phrase = phrase;
        }

        // function LIKE from SQL
        // special characters:
        // _    any char (is required)
        // #    any digit (is required)
        // %    any string of chars (can be empty - length 0)
        // other signs represent themselves
        public override bool ToBool()
        {
            string svalue = value.ToString();

            int iterator = 0;
            int i;
            for (i = 0; i < phrase.Length; i++)
            {
                char ch = phrase[i];

                if (ch == '_')
                {
                    if (svalue.Length == iterator)
                        return false;
                    iterator++;
                }
                else
                {
                    if (ch == '#')
                    {
                        if (svalue.Length == iterator)
                            return false;
                        if (!Char.IsDigit(svalue[iterator]))
                            return false;
                        iterator++;
                    }
                    else
                    {
                        if (ch == '%')
                        {
                            if (i == phrase.Length - 1)
                                return true;
                            else
                            {
                                i++;
                                ch = phrase[i];
                                while (iterator < svalue.Length)
                                {
                                    if (svalue[iterator].Equals(ch))
                                        break;
                                    if (ch == '#' && Char.IsDigit(svalue[iterator]))
                                        break;
                                    iterator++;
                                }
                                i--;
                                if (iterator == svalue.Length)
                                    return false;
                            }
                        }
                        else
                        {
                            if (svalue.Length == iterator)
                                return false;
                            if (!svalue[iterator].Equals(ch))
                                return false;
                            iterator++;
                        }
                    }
                }
            }

            if (iterator == svalue.Length)
                return true;
            else
                return false;
        }
    }
}
