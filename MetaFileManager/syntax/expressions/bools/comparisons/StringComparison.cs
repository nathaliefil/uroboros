using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.comparisons
{
    class StringComparison : Comparison
    {
        private IStringable leftSide;
        private IStringable rightSide;
        private ComparisonType type;

        public StringComparison(IStringable leftSide, IStringable rightSide, ComparisonType type)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.type = type;
        }

        public override bool ToBool()
        {
            string leftValue = leftSide.ToString();
            string rightValue = rightSide.ToString();

            switch (type)
            {
                case ComparisonType.Equals:
                    return leftValue.Equals(rightValue) ? true : false;
                case ComparisonType.NotEquals:
                    return leftValue.Equals(rightValue) ? false : true;
                case ComparisonType.Bigger:
                    return leftValue.CompareTo(rightValue) == 1 ? true : false;
                case ComparisonType.Smaller:
                    return leftValue.CompareTo(rightValue) == -1 ? true : false;
                case ComparisonType.BiggerOrEquals:
                    return leftValue.CompareTo(rightValue) > -1 ? true : false;
                case ComparisonType.SmallerOrEquals:
                    return leftValue.CompareTo(rightValue) < 1 ? true : false;
            }

            return false;
        }
    }
}
