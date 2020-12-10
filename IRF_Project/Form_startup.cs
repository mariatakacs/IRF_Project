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
    public partial class Form_startup : Form
    {
        public Form_startup()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e) //BEJENTKEZÉS A FELÜLETRE
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
