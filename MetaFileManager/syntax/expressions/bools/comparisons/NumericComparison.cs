using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.comparisons
{
    class NumericComparison : DefaultBoolable
    {
        private INumerable leftSide;
        private INumerable rightSide;
        private ComparisonType type;

        public NumericComparison(INumerable leftSide, INumerable rightSide, ComparisonType type)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.type = type;
        }

        public override bool ToBool()
        {
            decimal leftValue = leftSide.ToNumber();
            decimal rightValue = rightSide.ToNumber();

            switch (type)
            {
                case ComparisonType.Equals:
                    return leftValue == rightValue ? true : false;
                case ComparisonType.NotEquals:
                    return leftValue != rightValue ? true : false;
                case ComparisonType.Bigger:
                    return leftValue > rightValue ? true : false;
                case ComparisonType.Smaller:
                    return leftValue < rightValue ? true : false;
                case ComparisonType.BiggerOrEquals:
                    return leftValue >= rightValue ? true : false;
                case ComparisonType.SmallerOrEquals:
                    return leftValue <= rightValue ? true : false;
            }

            return false;
        }
    }
}
