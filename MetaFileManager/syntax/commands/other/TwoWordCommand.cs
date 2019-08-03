using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.Windows.Forms;

namespace Uroboros.syntax.commands.other
{
    class TwoWordCommand : ICommand
    {
        private TwoWordCommandType type;

        public TwoWordCommand(TwoWordCommandType type)
        {
            this.type = type;
        }

        public void Run()
        {
            switch (type)
            {
                case TwoWordCommandType.ClearLog:
                    Logger.GetInstance().ClearLog();
                    break;
                case TwoWordCommandType.ClearClipboard:
                    Logger.GetInstance().LogCommand("Clipboard clear.");
                    Clipboard.Clear();
                    break;
                case TwoWordCommandType.LogOn:
                    Logger.GetInstance().LogOn();
                    break;
                case TwoWordCommandType.LogOff:
                    Logger.GetInstance().LogOff();
                    break;
                case TwoWordCommandType.UroborosStop:
                    throw new RuntimeException("Uroboros stop.");
            }
        }
    }
}
