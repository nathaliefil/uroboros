﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions.list.subcommands;
using Uroboros.syntax.runtime;
using Uroboros.syntax.expressions.list.subcommands.orderby;

namespace Uroboros.syntax.commands.list
{
    class Order : ICommand
    {
        private string name;
        private OrderBy order;

        public Order(string name, OrderBy order)
        {
            this.name = name;
            this.order = order;
        }

        public void Run()
        {
            RuntimeVariables.GetInstance().Order(name, order);
        }
    }
}
