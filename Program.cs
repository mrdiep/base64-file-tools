using System;
using System.IO;
using System.Windows;

namespace sZipTool
{
    class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            // Restore clipboard to folder: sZipTool.exe "restore-file" "folder path"
            // make base64 text from file and copy to clipboard
            try
            {
                if (args[0] == "to-base64")
                {
                    var filename = Path.GetFileName(args[1]);
                    var bytes = File.ReadAllBytes(args[1]);
                    var file = Convert.ToBase64String(bytes);
                    Clipboard.SetText(filename + Environment.NewLine + file);
                }

                if (args[0] == "restore-file")
                {
                    var tt = Clipboard.GetText();
                    var s = tt.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                    var bytes = Convert.FromBase64String(s[1]);
                    var fileRestore = Path.Combine(args[1], s[0]);
                    if (File.Exists(fileRestore)) File.Delete(fileRestore);

                    File.WriteAllBytes(fileRestore, bytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}
