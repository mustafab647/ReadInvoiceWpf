using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReadUbl.Concrete
{
    public class ReadArchive
    {
        public static Dictionary<string, string> ReadArchiveFile(string filePath)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            try
            {
                ZipArchive archive = ZipFile.OpenRead(filePath);
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    if (entry.FullName.EndsWith(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        using (StreamReader sr = new StreamReader(entry.Open()))
                        {
                            string xmlStr = sr.ReadToEnd();
                            result.Add(entry.FullName, xmlStr);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error reading file: {ex.Message}");
            }
            return result;
        }
    }
}
