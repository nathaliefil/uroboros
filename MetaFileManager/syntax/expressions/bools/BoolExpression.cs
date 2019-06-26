using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class BoolExpression : DefaultToListMethod, IBoolable
    {
        List<IBoolExpressionElement> elements;

        public BoolExpression(List<IBoolExpressionElement> elements)
        {
            this.elements = elements;
        }

        public bool ToBool()
        {
            //Reverse Polish Notation reader

            Stack<bool> stack = new Stack<bool>();
            foreach (IBoolExpressionElement el in elements)
            {
                if (el is IBoolable)
                    stack.Push((el as IBoolable).ToBool());
                else if (el is BoolExpressionOperator)
                {
                    if ((el as BoolExpressionOperator).GetOperatorType().Equals(BoolExpressionOperatorType.Not))
                    {
                        bool a = stack.Pop();
                        stack.Push(!a);
                    }
                    else
                    {
                        BoolExpressionOperatorType type = (el as BoolExpressionOperator).GetOperatorType();

                        bool a = stack.Pop();
                        bool b = stack.Pop();

                        switch (type)
                        {
                            case BoolExpressionOperatorType.And:
                                stack.Push(a & b);
                                break;
                            case BoolExpressionOperatorType.Or:
                                stack.Push(a | b);
                                break;
                            case BoolExpressionOperatorType.Xor:
                                stack.Push(a ^ b);
                                break;
                        }
                    }
                }
            }
            return stack.Pop();
        }

        public decimal ToNumber()
        {
            bool value = ToBool();
            if (value)
                return 1;
            else
                return 0;
        }

        public override string ToString()
        {
            bool value = ToBool();
            if (value)
                return "1";
            else
                return "0";
        }
    }
}
