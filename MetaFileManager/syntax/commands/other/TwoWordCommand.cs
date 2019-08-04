using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.runtime;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Uroboros.syntax.commands.other
{
    class TwoWordCommand : ICommand
    {
        enum RecycleFlags : uint
        {
            SHERB_NOCONFIRMATION = 0x00000001,
            SHERB_NOPROGRESSUI = 0x00000002,
            SHERB_NOSOUND = 0x00000004
        }

        [DllImport("Shell32.dll", CharSet = CharSet.Unicode)]
        static extern uint SHEmptyRecycleBin(IntPtr hwnd, string pszRootPath, RecycleFlags dwFlags);

    


        private TwoWordCommandType type;

        public TwoWordCommand(TwoWordCommandType type)
        {
            this.type = type;
        }

        public void Run()
        {
            switch (type)
            {
                case TwoWordCommandType.ClearBin:
                    ClearBin();
                    break;
                case TwoWordCommandType.ClearLog:
                    Logger.GetInstance().ClearLog();
                    break;
                case TwoWordCommandType.ClearClipboard:
                    Logger.GetInstance().LogCommand("Clipboard cleared.");
                    Clipboard.Clear();
                    break;
                case TwoWordCommandType.LogOn:
                    Logger.GetInstance().LogOn();
                    break;
                case TwoWordCommandType.LogOff:
                    Logger.GetInstance().LogOff();
                    break;
                case TwoWordCommandType.UroborosStop:
                    throw new RuntimeException("Uroboros stopped.");
            }
        }

        private void ClearBin()
        {
            try
            {
                uint result = SHEmptyRecycleBin(IntPtr.Zero, null, 0);
                if (result == 0)
                    Logger.GetInstance().LogCommand("Recycle bin cleared.");
            }
            catch (Exception)
            {
                Logger.GetInstance().LogCommand("ERROR! Something went wrong during clearing recycle bin.");
            }
        }
    }
}
