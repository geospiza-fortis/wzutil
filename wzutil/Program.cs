using System;
using System.IO;
using MapleLib.WzLib;
using MapleLib.WzLib.Serialization;


namespace JsonExporter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Refer to MainForm::RunWzFilesExtraction
            if (args.Length < 2)
            {
                Console.WriteLine("Wrong number of arguments.");
                Console.WriteLine("Usage: <WZ File> <Output Directory>");
                System.Environment.Exit(1);
            }
            var wzPath = args[0];
            var baseDir = args[1];
            Console.WriteLine($"Exporting {wzPath} to {baseDir}");
            WzMapleVersion version = WzMapleVersion.GMS;
            IWzFileSerializer serializer = new WzClassicXmlSerializer(2, LineBreak.Windows, false);

            if (!Directory.Exists(baseDir))
            {
                Directory.CreateDirectory(baseDir);
            }
            WzFile wz = new WzFile(wzPath, version);
            wz.ParseWzFile();
            serializer.SerializeFile(wz, Path.Combine(baseDir, wz.Name));
            wz.Dispose();
        }
    }
}
