using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;

namespace Uroboros.syntax.expressions.list.subcommands
{
    //this one class is used for two subcommands: WITH and WITHOUT

    class With : ISubcommand
    {
        private IListable value;
        private bool negation;
        // negation = false ----> subcommand WITH
        // negation = true -----> subcommand WITHOUT

        public With(IListable value, bool negation)
        {
            this.value = value;
            this.negation = negation;
        }

        public bool IsNegated()
        {
            return negation;
        }

        public List<string> GetValue()
        {
            return value.ToList();
        }
    }
}
