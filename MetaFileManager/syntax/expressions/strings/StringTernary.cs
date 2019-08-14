using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class StringTernary : DefaultStringable
    {
        private IBoolable condition;
        private IStringable leftValue;
        private IStringable rightValue;

        public StringTernary(IBoolable condition, IStringable leftValue, IStringable rightValue)
        {
            this.condition = condition;
            this.leftValue = leftValue;
            this.rightValue = rightValue;
        }

        public override string ToString()
        {
            return condition.ToBool() ? leftValue.ToString() : rightValue.ToString();
        }
    }
}
