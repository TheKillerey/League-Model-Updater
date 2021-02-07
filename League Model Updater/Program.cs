
using LeagueToolkit.Helpers;
using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.SkeletonFile;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SharpGLTF.Schema2;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;



namespace GLTF2League
{
    class Program
    {
        static void Main(string[] args)
        {
           
           
           
           var model = SharpGLTF.Schema2.ModelRoot.Load(@"K:\Riot Games\LeagueSkins\WheelChair Yasuo\RAW\assets\characters\yasuo\skins\base\yasuo.glb");
           var skn = SimpleSkinGltfExtensions.ToLeagueModel(model);
           
            skn.Item1.Write(@"K:\Riot Games\LeagueSkins\WheelChair Yasuo\RAW\assets\characters\yasuo\skins\base\yasuo.skn");
            if(skn.Item2.Joints.Count == 0)
            {
               Console.WriteLine("Root Joint doesn't have a name.");
            }

            else
            {
                skn.Item2.Write(@"K:\Riot Games\LeagueSkins\WheelChair Yasuo\RAW\assets\characters\yasuo\skins\base\yasuo.skl");
            }

            byte[] find = {0x00, 0x00, 0x6F, 0x74};
            byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
            byte[] file = File.ReadAllBytes(@"K:\Riot Games\LeagueSkins\WheelChair Yasuo\RAW\assets\characters\yasuo\skins\base\yasuo.skl");
            int i, j, iMax = file.Length - find.Length ;
            for (i = 0; i <= iMax; i++)
            {
              for (j = 0; j < find.Length; j++)
                if (file[i + j] != find[j]) break;
              if (j == find.Length) break;
            }
            if (i <= iMax)
            {
              for (j = 0; j < find.Length; j++)
                file[i + j] = replace[j];
              File.WriteAllBytes(@"K:\Riot Games\LeagueSkins\WheelChair Yasuo\RAW\assets\characters\yasuo\skins\base\yasuo.skl", file);
            }
            

        }
    }
}
