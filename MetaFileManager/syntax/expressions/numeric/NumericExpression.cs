using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.numeric
{
    class NumericExpression : DefaultNumerable
    {
        List<INumericExpressionElement> elements;

        public NumericExpression(List<INumericExpressionElement> elements)
        {
            this.elements = elements;
        }

        public override decimal ToNumber()
        {
            //Reverse Polish Notation reader

            Stack<decimal> stack = new Stack<decimal>();
            foreach (INumericExpressionElement el in elements)
            {
                if (el is INumerable)
                    stack.Push((el as INumerable).ToNumber());
                else if (el is NumericExpressionOperator)
                {
                    if ((el as NumericExpressionOperator).GetOperatorType().Equals(NumericExpressionOperatorType.UnaryMinus))
                    { /// to check if working
                        decimal a = stack.Pop();
                        stack.Push(-a);
                    }
                    else
                    {
                        NumericExpressionOperatorType type = (el as NumericExpressionOperator).GetOperatorType();

                        decimal a = stack.Pop();
                        decimal b = stack.Pop();

                        switch (type)
                        {
                            case NumericExpressionOperatorType.Plus:
                                stack.Push(a + b);
                                break;
                            case NumericExpressionOperatorType.Minus:
                                stack.Push(a - b);
                                break;
                            case NumericExpressionOperatorType.Multiply:
                                stack.Push(a * b);
                                break;
                            case NumericExpressionOperatorType.Divide:
                                if (b == 0)
                                    throw new RuntimeException("RUNTIME ERROR! Division by zero occured.");
                                stack.Push(a / b);
                                break;
                            case NumericExpressionOperatorType.Modulo:
                                stack.Push(a % b);
                                break;
                        }
                    }
                }
            }
            return stack.Pop();
        }
    }
}
