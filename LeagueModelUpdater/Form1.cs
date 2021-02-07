using LeagueToolkit.IO.SimpleSkinFile;
using LeagueToolkit.IO.SkeletonFile;
using System;
using System.IO;
using System.Windows.Forms;

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


        private void button1_Click(object sender, EventArgs e)
        {
            skn.Filter = "LeagueSkinFile| *.skn";
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

                string outputSKN = Path.ChangeExtension(sknfile, "new.skn");
                string outputSKL = Path.ChangeExtension(sklfile, "new.skl");

                skinfile.Write(outputSKN);
                skeletonfile.Write(outputSKL);
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
            }

            catch (FileNotFoundException ex)
            {

                MessageBox.Show("Skeleton Missing. Your SKN file needs a skeleton (SKL).", $"Error: Can't find .skl file!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
