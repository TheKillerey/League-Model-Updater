using LeagueToolkit.IO.SimpleSkinFile;
using System;
using System.IO;

namespace SKNUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converts the SKN (League Skin File) to the latest version.");
            
            SimpleSkin inputSimpleSkin = new(File.OpenRead(args[0]));

            string outputFile = args.Length > 1 ? args[1] : Path.ChangeExtension(args[0], "new.skn");

            inputSimpleSkin.Write(outputFile);
            return;
        }
    }
}
