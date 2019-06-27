using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools.comparisons
{
    class ListComparison : DefaultBoolable
    {
        private IListable leftSide;
        private IListable rightSide;
        private ComparisonType type;

        public ListComparison(IListable leftSide, IListable rightSide, ComparisonType type)
        {
            this.leftSide = leftSide;
            this.rightSide = rightSide;
            this.type = type;
        }

        public override bool ToBool()
        {
            List<string> leftValue = leftSide.ToList();
            List<string> rightValue = rightSide.ToList();

            switch (type)
            {
                case ComparisonType.Equals:
                    return leftValue.SequenceEqual(rightValue) ? true : false;
                case ComparisonType.NotEquals:
                    return leftValue.SequenceEqual(rightValue) ? false : true;
                case ComparisonType.Bigger:
                    return leftValue.Count > rightValue.Count ? true : false;
                case ComparisonType.Smaller:
                    return leftValue.Count < rightValue.Count ? true : false;
                case ComparisonType.BiggerOrEquals:
                    return leftValue.Count >= rightValue.Count ? true : false;
                case ComparisonType.SmallerOrEquals:
                    return leftValue.Count <= rightValue.Count ? true : false;
            }

            return false;
        }
    }
}
