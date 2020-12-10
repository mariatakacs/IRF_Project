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

namespace IRF_Project
{
    public partial class Start_up : Form
    {
        List<string> users = new List<string>();
        List<string> pass = new List<string>();
        public static string logolt_user = "";
      
        public Start_up()
        {
            InitializeComponent();
            LoadData();
            ActiveControl = textBox1;
        }

        public void LoadData() //BETÖLTJÜK AZ ADATOKAT - 4.gyakorlat alapján
        {

            using (var sr = new StreamReader("jelszavak.csv"))
            {
                while (!sr.EndOfStream)
                {
                    var line = sr.ReadLine().Split(',');
                    users.Add(line[0]);
                    pass.Add(line[1]);
                }
            }
        }

            private void button2_Click(object sender, EventArgs e) //KILÉPÉS
            {

            }

        private void button1_Click(object sender, EventArgs e) //BEJELENTKEZÉS A FELÜLETRE
        {
            if (users.Contains(textBox1.Text) && pass.Contains(textBox2.Text) && Array.IndexOf(users.ToArray(), textBox1.Text) == Array.IndexOf(pass.ToArray(), textBox2.Text))
            {
                logolt_user = textBox1.Text;
                Form_Main f2 = new Form_Main();
                this.Hide();
                f2.ShowDialog();
                this.Show();
            }
            else
                MessageBox.Show("A megadott username és/vagy jelszó hibás!");
        }
    }
}
