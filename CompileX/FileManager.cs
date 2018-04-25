using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace CompileX
{
    public static class FileManager
    {
        public static void CreateFile(string path, List<string> data)
        {

            if(data!= null)
            {
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                using (FileStream fs = File.Create(path))
                {
                    using (var fw = new StreamWriter(fs))
                    {
                        foreach (string line in data)
                        {
                            fw.WriteLine(line);
                        }
                    }
                }
            }
        }
    }
}