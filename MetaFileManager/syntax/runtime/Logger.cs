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

        private Logger()
        {
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
    }
}
