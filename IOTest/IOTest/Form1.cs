using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace IOTest
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        int CZK(int usd)
        {
            return (int)(usd * 22.35);
        }
        int CZK(int usd, float kurz)
        {
            return (int)(usd * kurz);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog vstup = new OpenFileDialog();
            vstup.InitialDirectory = Application.StartupPath;

            if(vstup.ShowDialog() == DialogResult.OK)
            {
                StreamReader ctec = new StreamReader(vstup.FileName);
                List<string> soubor = new List<string>();
                while(!ctec.EndOfStream)
                {
                    soubor.Add(ctec.ReadLine());
                }
                ctec.Close();

                int zen = 0, minMzda = 17300, mzda = 0, soucet = 0, pocet = 0;
                string best = "";
                bool first = true;

                listBox1.Items.Clear();
                foreach(string radek in soubor)
                {
                    if (!first)
                    {
                        string[] zaznamy = radek.Split('\t');
                        if (zaznamy[2] == "Female")
                            zen++;
                        if (CZK(int.Parse(zaznamy[4])) < minMzda)
                        {
                            listBox1.Items.Add(radek.Replace(zaznamy[4], CZK(int.Parse(zaznamy[4])).ToString()));
                        }
                        if (int.Parse(zaznamy[4]) > mzda)
                        {
                            mzda = int.Parse(zaznamy[4]);
                            best = radek.Replace(zaznamy[4], CZK(mzda).ToString());
                        }
                        soucet += int.Parse(zaznamy[3]);
                        pocet++;
                    }
                    first = false;
                }
                StreamWriter zapis = new StreamWriter("best.txt");
                zapis.WriteLine(best + "\nPrumerny vek: " + Math.Round((double)soucet / pocet, 1));
                zapis.Close();
                label2.Text = "Žen: " + zen;
            }
        }


    }
}
