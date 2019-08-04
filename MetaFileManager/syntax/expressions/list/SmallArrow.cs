using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;

namespace Uroboros.syntax.expressions.list
{
    class SmallArrow : DefaultListable
    {
        private IListable leftSide;
        private IStringable rightSide;
        private bool unique;

        public SmallArrow(IListable leftSide, IStringable rightSide, bool unique)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.unique = unique;
        }

        public override List<string> ToList()
        {
            List<string> result = new List<string>();
            decimal oldIndex = RuntimeVariables.GetInstance().GetValueNumber("index");
            string oldThis = RuntimeVariables.GetInstance().GetValueString("this");
            RuntimeVariables.GetInstance().Actualize("index", 0);

            foreach (string element in leftSide.ToList())
            {
                RuntimeVariables.GetInstance().Actualize("this", element);

                string value = rightSide.ToString();
                if (unique)
                {
                    if (!result.Contains(value))
                        result.Add(value);
                }
                else
                    result.Add(value);

                RuntimeVariables.GetInstance().PlusPlus("index");
            }

            RuntimeVariables.GetInstance().Actualize("index", oldIndex);
            RuntimeVariables.GetInstance().Actualize("this", oldThis);
            return result;
        }
    }
}
