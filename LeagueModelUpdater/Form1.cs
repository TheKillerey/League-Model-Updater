using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.SkeletonFile;
using System;
using System.IO;
using System.Windows.Forms;
using SharpGLTF.Schema2;
using Newtonsoft.Json.Schema;
using System.Runtime.CompilerServices;
using LeagueToolkit.IO.MapGeometry;

namespace LeagueModelUpdater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.AllowDrop = true;
        }
        
        OpenFileDialog skn = new OpenFileDialog();
        //string  skl = skn.FileName();
        OpenFileDialog glb = new OpenFileDialog();
        SaveFileDialog saveskn = new SaveFileDialog();
        OpenFileDialog openmapgeo = new OpenFileDialog();
        SaveFileDialog savegltf = new SaveFileDialog();
        SaveFileDialog saveglb = new SaveFileDialog();

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
                Skeleton skeletonfile = new Skeleton(sklfile);
                

                string outputSKN = Path.ChangeExtension(sknfile, "new.skn");
                string outputSKL = Path.ChangeExtension(sklfile, "new.skl");

                skinfile.Write(outputSKN);
                skeletonfile.Write(outputSKL);

                //Fixes the error that is caused by Crauzer :v
                //byte[] find = {0x00, 0x00, 0x6F, 0x74};
                //byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
                //byte[] file = File.ReadAllBytes(outputSKL);
                //int i, j, iMax = file.Length - find.Length ;
                //for (i = 0; i <= iMax; i++)
                //{
                //  for (j = 0; j < find.Length; j++)
                //    if (file[i + j] != find[j]) break;
                //  if (j == find.Length) break;
                //}
                //if (i <= iMax)
                //{
                //  for (j = 0; j < find.Length; j++)
                //    file[i + j] = replace[j];
                //  //File.WriteAllBytes(outputSKL, file);
                //}
                MessageBox.Show("Your League Skin is now updated to the latest League version.", $"Files Exported!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
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

                //byte[] find = {0x00, 0x00, 0x6F, 0x74};
                //byte[] replace = {0x72, 0x6F, 0x6F, 0x74};
                //byte[] file = File.ReadAllBytes(sklfile);
                //int i, j, iMax = file.Length - find.Length ;
                //for (i = 0; i <= iMax; i++)
                //{
                //  for (j = 0; j < find.Length; j++)
                //    if (file[i + j] != find[j]) break;
                //  if (j == find.Length) break;
                //}
                //if (i <= iMax)
                //{
                //  for (j = 0; j < find.Length; j++)
                //    file[i + j] = replace[j];
                //  File.WriteAllBytes(sklfile, file);
                //}
                MessageBox.Show("Your League Skin is now updated to the latest League version.", $"Files Exported!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (Exception ex)
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
                    try
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
                    }
                    catch (Exception ag)
                    {
                    MessageBox.Show("Make sure you load a file before exporting it!", $"Error: No file is loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    

                }

            



           
           
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textbox1_dragdrop(object sender, DragEventArgs e)
        {
            var data = e.Data.GetData(DataFormats.FileDrop);
            if(data != null)
            {
                
                var fileNames = data as string[];
                string ext = Path.GetExtension(fileNames[0]);
                if(fileNames.Length > 0)
                   if(ext.Contains(".skn"))
                    {
                      textBox1.Text = Path.GetFullPath(fileNames[0]);
                      var textbox2new = textBox1.Text;
                      var skltextbox = textbox2new.Replace(".skn", ".skl");
                      textBox2.Text = skltextbox;
                    }
                   else
                    {
                        MessageBox.Show("Please import only skn (League's SimpleSkin)!", "Wrong File",MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                   
            }  
        }

        private void textbox1_dragenter(object sender, DragEventArgs e)
        {
            e.Effect=DragDropEffects.Copy;
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e) //Export Only SKN
        {
          try
            {
                
                string sknfile = textBox1.Text;
                SimpleSkin skinfile = new(sknfile);
                string outputSKN = Path.ChangeExtension(sknfile, "new.skn");
                skinfile.Write(outputSKN);
                MessageBox.Show("Your LeagueSkin is now updated to the latest League version. If it's broken please use only skl as update!", $"Files Exported!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Skeleton Missing. Your SKN file needs a skeleton (SKL).", $"Error: Can't find .skl file!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ag)
                {
                MessageBox.Show("Make sure you load a file before exporting it!", $"Error: No file is loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void button7_Click(object sender, EventArgs e) //Export Only SKL
        {
            try
            {
                
                string sklfile = textBox2.Text;
                Skeleton skeletonfile = new Skeleton(sklfile);
                string outputSKL = Path.ChangeExtension(sklfile, "new.skl");
                skeletonfile.Write(outputSKL);
                MessageBox.Show("Your Skeleton is now updated to the latest League version.", $"Files Exported!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Skeleton Missing. Your SKN file needs a skeleton (SKL).", $"Error: Can't find .skl file!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            catch (Exception ag)
                {
                MessageBox.Show("Make sure you load a file before exporting it!", $"Error: No file is loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            
        }

        private void button8_Click(object sender, EventArgs e)
        {
            openmapgeo.Filter = "LeagueMapFile (*.mapgeo)| *.mapgeo";
            if (openmapgeo.ShowDialog() == DialogResult.OK)
            {
                textBox4.Text = openmapgeo.FileName;
                string MAPGEOText = Path.GetFullPath(openmapgeo.FileName);

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            string MAPGEOText = Path.GetFullPath(openmapgeo.FileName);
            

            savegltf.Filter = "GLTF|*.gltf|GLB|*.glb";
            if (savegltf.ShowDialog() == DialogResult.OK)

            {
                string outputgltf = Path.GetFullPath(savegltf.FileName);
                try
            {

                MapGeometry mapgeo = new MapGeometry(MAPGEOText);
                var convertmap = mapgeo.ToGLTF();
                    convertmap.SaveGLB(outputgltf);
                MessageBox.Show("Exported to GLTF/GLB.", $"Files Exported!", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }

            catch (FileNotFoundException ex)
            {
                MessageBox.Show("MapFile is missing. Make sure you load the correct MAPGEO file!", $"Error: Can't find .mapgeo file!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            catch (System.ArgumentException ag)
                {
                MessageBox.Show("Make sure you load a file before exporting it!", $"Error: No file is loaded!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                
            }


            
            
        }
    }
}
