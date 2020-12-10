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
    }
}
