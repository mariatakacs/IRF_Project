using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IRF_Project
{
    public partial class Form_Main : Form
    {
        //JÁTÉK RÉSZHEZ
        Random r = new Random();
        int[] nyeremenyek = { 0, 100, 2000, 5000, 10000, 25000 };
        bool played = false;

        public Form_Main()
        {
            InitializeComponent();
        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e) //TAJEKOZTATÓ,szabályzat
        {
            Form_tajekoztato f3 = new Form_tajekoztato();
            this.Hide();
            f3.ShowDialog();
            this.Show();
        }

        private void button1_Click(object sender, EventArgs e) //JÁTSZOK GOMB - VÉLETLEN SZÁMOK
        {

            int talalt = 0;
            TextBox[] eredmeny = { sors_1, sors_2, sors_3, sors_4, sors_5 };
            TextBox[] tipp = { tipp_1, tipp_2, tipp_3, tipp_4, tipp_5 };
            
            foreach (var x in tipp)
            {
                if (x.Text == string.Empty || int.Parse(x.Text) < 1 || int.Parse(x.Text) > 90)
                {
                    MessageBox.Show("A " + Convert.ToString(Array.IndexOf(tipp, x) + 1) + ". tipp nem megfelelő!");
                    return;
                }
            }
            
            for (int i = 0; i < eredmeny.Length; i++)
            {
                eredmeny[i].Text = Convert.ToString(r.Next(1, 90));
                if (int.Parse(eredmeny[i].Text) == int.Parse(tipp[i].Text))
                {
                    talalt++;
                    tipp[i].BackColor = Color.LightGreen;
                }
                else
                {
                    tipp[i].BackColor = Color.Tomato;
                }

            }
            label_talalt.Text = Convert.ToString(talalt);
            label_nyeremeny.Text = Convert.ToString(nyeremenyek[talalt]);
            played = true;

            //Üzenet kivétele, mert kevés az esélye annak, hogy nyerjen:
            //MessageBox.Show("Köszi, hogy játszottál! Eltalált számok:" + Convert.ToString(talalt) + "db, a nyereményed összege így: "+ Convert.ToString(nyeremenyek[talalt]));

        }
    }
}
