using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DivineScript.syntax.variables.abstracts;

namespace DivineScript.syntax.variables.expressions.list.subcommands
{
    class NumericSubcommand : ISubcommand
    {
        private INumerable value;
        private NumericSubcommandType type;

        public NumericSubcommand(INumerable value, NumericSubcommandType type)
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
