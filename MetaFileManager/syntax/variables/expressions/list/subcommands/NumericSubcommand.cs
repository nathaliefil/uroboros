using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DivineScript.syntax.variables.expressions.list.subcommands
{
    class NumericSubcommand : ISubcommand
    {
        private NumericExpression value;
        private NumericSubcommandType type;

        public NumericSubcommand(NumericExpression value, NumericSubcommandType type)
        {
            this.value = value;
            this.type = type;
        }

        public NumericSubcommandType GetNumericSubcommandType()
        {
            return type;
        }

        public int GetValue()
        {
            return (int)value.ToNumber();
        }
    }
}
