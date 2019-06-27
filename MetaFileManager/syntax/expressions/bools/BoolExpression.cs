using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.bools
{
    class BoolExpression : DefaultBoolable
    {
        List<IBoolExpressionElement> elements;

        public BoolExpression(List<IBoolExpressionElement> elements)
        {
            this.elements = elements;
        }

        public override bool ToBool()
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
    }
}
