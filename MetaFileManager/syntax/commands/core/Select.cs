using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Uroboros.syntax.variables.abstracts;
using Uroboros.syntax.runtime;
using System.Runtime.InteropServices;
using System.IO;
using Uroboros.syntax.variables.from_location;


namespace Uroboros.syntax.commands.core
{
    class Select: CoreCommand
    {
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern int SHOpenFolderAndSelectItems(IntPtr pidlFolder, uint cidl, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] apidl, uint dwFlags);
        [DllImport("shell32.dll", SetLastError = true)]
        public static extern void SHParseDisplayName([MarshalAs(UnmanagedType.LPWStr)] string name, IntPtr bindingContext, [Out] out IntPtr pidl, uint sfgaoIn, [Out] out uint psfgaoOut);


        public Select(IListable list)
        {
            this.list = list;
        }


        public override void Run ()
        {
            string location = RuntimeVariables.GetInstance().GetValueString("location");

            IntPtr nativeFolder;
            uint psfgaoOut;
            SHParseDisplayName(location, IntPtr.Zero, out nativeFolder, 0, out psfgaoOut);

            // location not exists
            if (nativeFolder == IntPtr.Zero)
                return;

            List<string> selected = new List<string>();
            foreach (string str in list.ToList())
            {
                if (FileInnerVariable.Exist(str))
                    selected.Add(str);
            }

            // escape if there is nothing to select
            if (selected.Count == 0)
            {
                Logger.GetInstance().Log("Action ignored! There was nothing to select.");
                return;
            }

            //fill array of files and directories
            IntPtr[] fileArray = new IntPtr[selected.Count];
            for (int i = 0; i < selected.Count; i++ )
            {
                SHParseDisplayName(Path.Combine(location, selected[i]), IntPtr.Zero, out fileArray[i], 0, out psfgaoOut);
                Logger.GetInstance().Log("Select " + selected[i]);
            }

            SHOpenFolderAndSelectItems(nativeFolder, (uint)fileArray.Length, fileArray, 0);

            // free memory
            Marshal.FreeCoTaskMem(nativeFolder);
            for (int i = 0; i < selected.Count; i++)
                Marshal.FreeCoTaskMem(fileArray[i]);
        }

    }
}
