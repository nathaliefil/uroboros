using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Uroboros.syntax.runtime
{
    public class Logger
    {
        private static Logger INSTANCE = new Logger();
        private TextBox outputBox;
        private bool logCommands;

        private Logger()
        {
            logCommands = true;
        }

        public static Logger GetInstance()
        {
            return INSTANCE;
        }

        public void SetOutputBox(TextBox box)
        {
            outputBox = box;
        }

        public void Log(string text)
        {
            if (outputBox.Text.Length == 0)
                outputBox.AppendText(text);
            else
                outputBox.AppendText(Environment.NewLine + text);
        }

        public void LogSyntaxError(string text)
        {
            Log(text);
        }

        public void LogCommandError(string text)
        {
            Log(text);
        }

        public void LogCommand(string text)
        {
            if (logCommands)
                Log(text);
        }

        public void ClearLog()
        {
            outputBox.Text = "";
        }

        public void LogOn()
        {
            logCommands = true;
        }

        public void LogOff()
        {
            logCommands = false;
        }
    }
}
