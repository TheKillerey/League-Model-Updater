using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.SkeletonFile;
using System;
using System.IO;
using System.Windows.Forms;
using SharpGLTF.Schema2;

namespace LeagueModelUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        OpenFileDialog skn = new OpenFileDialog();
        //string  skl = skn.FileName();
        OpenFileDialog glb = new OpenFileDialog();
        SaveFileDialog saveskn = new SaveFileDialog();

        private void button1_Click(object sender, EventArgs e)
        {
            skn.Filter = "LeagueSkinFile (*.skn)| *.skn";
            if (skn.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = skn.FileName;
                string SKNText = Path.GetFullPath(skn.FileName);

                string SKLText = SKNText.Replace(".skn", ".skl");
                textBox2.Text = SKLText;


            }


        }


        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string sknfile = textBox1.Text;
                string sklfile = textBox2.Text;

                
                SimpleSkin skinfile = new(sknfile);
                Skeleton skeletonfile = new(sklfile);
                var lol2gltf = SimpleSkinGltfExtensions.ToGltf(skinfile, skeletonfile);
                var gltf2lol = lol2gltf.ToLeagueModel();
                

                var sknfilenew = gltf2lol.Item1;
                var sklfilenew = gltf2lol.Item2;

                string outputSKN = Path.ChangeExtension(sknfile, "new.skn");
                string outputSKL = Path.ChangeExtension(sklfile, "new.skl");

                sknfilenew.Write(outputSKN);
                sklfilenew.Write(outputSKL);

                //Fixes the error that is caused by Crauzer :v
                byte[] find = {0x00, 0x00, 0x6F, 0x74};
                byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
                byte[] file = File.ReadAllBytes(outputSKL);
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
                  File.WriteAllBytes(outputSKL, file);
                }

            }

            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Skeleton Missing. Your SKN file needs a skeleton (SKL).", $"Error: Can't find .skl file!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                string sknfile = textBox1.Text;
                string sklfile = textBox2.Text;
                string SKNName = Path.GetFileName(sklfile);

                SimpleSkin skinfile2 = new(sknfile);
                Skeleton skeletonfile2 = new(sklfile);

                skinfile2.Write(sknfile);
                skeletonfile2.Write(sklfile);

                byte[] find = {0x00, 0x00, 0x6F, 0x74};
                byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
                byte[] file = File.ReadAllBytes(sklfile);
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
                  File.WriteAllBytes(sklfile, file);
                }
            }

            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Skeleton Missing. Your SKN file needs a skeleton (SKL).", $"Error: Can't find .skl file!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }

        

        private void button4_Click(object sender, EventArgs e)
        {

            glb.Filter = "GLTF/GLB files (*.gltf, *.glb) | *.gltf; *.glb";
            if (glb.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = glb.FileName;
                string SKNText = Path.GetFullPath(glb.FileName);

            }

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {



        }
        private void button5_Click(object sender, EventArgs e)
        {

            saveskn.Filter = "LeagueSkinFile (*.skn)| *.skn";
                if (saveskn.ShowDialog() == DialogResult.OK)
                {
                
                    string GLTFFile = textBox3.Text;
                    var model = ModelRoot.Load(GLTFFile);
                    var gltf2skn = SimpleSkinGltfExtensions.ToLeagueModel(model);
                
                    string outputskn = saveskn.FileName;
                    string SKNText = Path.GetFullPath(saveskn.FileName);
                
                    string SKLText = SKNText.Replace(".skn", ".skl");
                    string outputskl = SKLText;
                
                    
                
                    gltf2skn.Item1.Write(outputskn);
                    gltf2skn.Item2.Write(outputskl);
                    
                    byte[] find = {0x00, 0x00, 0x6F, 0x74};
                    byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
                    byte[] file = File.ReadAllBytes(outputskl);
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
                      File.WriteAllBytes(outputskl, file);
                    }
            }





           
           
        }

        
    }
}
