using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.expressions;
using Uroboros.syntax.runtime;
using System.IO;
using Uroboros.syntax.variables.abstracts;
using System.Runtime.InteropServices;


namespace Uroboros.syntax.commands.core
{
    class Delete : CoreCommand
    {
        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 1)]
        public struct SHFILEOPSTRUCT
        {
            public IntPtr hwnd;
            [MarshalAs(UnmanagedType.U4)]
            public int wFunc;
            public string pFrom;
            public string pTo;
            public short fFlags;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fAnyOperationsAborted;
            public IntPtr hNameMappings;
            public string lpszProgressTitle;
        }
        [DllImport("shell32.dll", CharSet = CharSet.Auto)]
        static extern int SHFileOperation(ref SHFILEOPSTRUCT FileOp);
        const int FO_DELETE = 3;
        const int FOF_ALLOWUNDO = 0x40;
        const int FOF_NOCONFIRMATION = 0x10;



        public Delete(IListable list)
        {
            this.list = list;
        }

        protected override void DirectoryAction(string directoryName, string location)
        {
            FileAction(directoryName, location);
        }

        protected override void FileAction(string fileName, string location)
        {
            location += "\\" + fileName + "\0";
            try
            {
                SHFILEOPSTRUCT shf = new SHFILEOPSTRUCT();
                shf.wFunc = FO_DELETE;
                shf.fFlags = FOF_ALLOWUNDO | FOF_NOCONFIRMATION;
                shf.pFrom = @location;
                SHFileOperation(ref shf);

                Logger.GetInstance().LogCommand("Delete " + fileName);
            }
            catch (Exception ex)
            {
                if (ex is IOException || ex is UnauthorizedAccessException)
                    throw new CommandException("Action ignored! Access denied during deleting " + fileName + ".");
                else
                    throw new CommandException("Action ignored! Something went wrong during deleting " + fileName + ".");
            }
        }
    }
}
