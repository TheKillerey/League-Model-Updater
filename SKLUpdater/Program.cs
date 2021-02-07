using LeagueToolkit.IO.SkeletonFile;
using System;
using System.IO;

namespace SKLUpdater
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Converts the SKL (League Skeleton File) to the latest version.");
            
            Skeleton inputSkeleton = new(File.OpenRead(args[0]));

            string outputFile = args.Length > 1 ? args[1] : Path.ChangeExtension(args[0], "new.skl");

            inputSkeleton.Write(outputFile);
            return;
        }
    }
}
