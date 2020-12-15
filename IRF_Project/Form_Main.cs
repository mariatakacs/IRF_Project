using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;
using System.Reflection;

namespace IRF_Project
{
    public partial class Form_Main : Form
    {
        Excel.Application xlApp; // A Microsoft Excel alkalmazás
        Excel.Workbook xlWB; // A létrehozott munkafüzet
        Excel.Worksheet xlSheet; // Munkalap a munkafüzeten belül

        //JÁTÉK RÉSZHEZ
        Random r = new Random();
        int[] nyeremenyek = { 0, 100, 2000, 5000, 10000, 25000 };
        bool played = false;

        public Form_Main()
        {
            InitializeComponent();
            //LINQ lekérdezés
            var user = from a in context.Jatekosok_adatai
                       where = a.USERNAME = Start_up.logolt_user
                       select new
                       
                       {
                           a.USERNAME
                           a.TELJES_NÉV
                           a.SZÜLETÉSI_DÁTUM
                           a.LAKCÍM
                           a.TELEFONSZÁM
                       };
            textBox1.Text = user.USERNAME;
            textBox2.Text = user.TELJES_NÉV;
            textBox3.Text = user.SZÜLETÉSI_DÁTUM;
            textBox4.Text = user.LAKCÍM;
            textBox5.Text = user.TELEFONSZÁM;

            tipp_1.KeyPress += ValidateKeyPress;
            tipp_2.KeyPress += ValidateKeyPress;
            tipp_3.KeyPress += ValidateKeyPress;
            tipp_4.KeyPress += ValidateKeyPress;
            tipp_5.KeyPress += ValidateKeyPress;
            




        }

        private void ValidateKeyPress(object sender, KeyPressEventArgs e) //LINQ
        {
            if (!Char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
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

        private void Excel_export_Click(object sender, EventArgs e)
        {
            if (played == true)
            {
                Excel_export();
                FormatTable();
            }
            else
            {
                MessageBox.Show("Még nem játszottál...Kérlek először játsz egyet.");
            }

        }
        private void Excel_export() //óra alapján
        {
            try
            {
                xlApp = new Excel.Application();
                xlWB = xlApp.Workbooks.Add(Missing.Value);
                xlSheet = xlWB.ActiveSheet;
                CreateTable();  //ADATOK létrehozása
                xlApp.Visible = true;
                xlApp.UserControl = true;
            }
            catch (Exception ex) // Hibakezelés a beépített hibaüzenettel
            {
                string errMsg = string.Format("Error: {0}\nLine: {1}", ex.Message, ex.Source);
                MessageBox.Show(errMsg, "Error");

                // Hiba esetén az Excel applikáció bezárása automatikusan
                xlWB.Close(false, Type.Missing, Type.Missing);
                xlApp.Quit();
                xlWB = null;
                xlApp = null;
            }

            
        }

        private void CreateTable()
        {

            string[] headers = new string[] {
             "Username",
             "Teljes Név",
             "Játék dátuma",
             "1. tipp",
             "2. tipp",
             "3. tipp",
             "4. tipp",
             "5. tipp",
             "Találatok száma",
             "Nyeremény összege"};
            string[] tartalom = { textBox1.Text, textBox2.Text, (DateTime.Today).ToString("dd/MM/yyy"), tipp_1.Text, tipp_2.Text, tipp_3.Text, tipp_4.Text, tipp_5.Text, label_talalt.Text, Convert.ToString(nyeremenyek[int.Parse(label_talalt.Text)]) };
            for (int i = 0; i < headers.Length; i++)
            {
                xlSheet.Cells[1, (i + 1)] = headers[i];
                xlSheet.Cells[2, (i + 1)] = tartalom[i];

            }
           

        }
        private string GetCell(int x, int y) // EXCEL FORMÁZÁSÁHOZ
        {
            string ExcelCoordinate = "";
            int dividend = y;
            int modulo;

            while (dividend > 0)
            {
                modulo = (dividend - 1) % 26;
                ExcelCoordinate = Convert.ToChar(65 + modulo).ToString() + ExcelCoordinate;
                dividend = (int)((dividend - modulo) / 26);
            }
            ExcelCoordinate += x.ToString();

            return ExcelCoordinate;
        }


        private void FormatTable() 
        {
            int lastRowID = xlSheet.UsedRange.Rows.Count;
            int lastColID = xlSheet.UsedRange.Columns.Count;
            Excel.Range headerRange = xlSheet.get_Range(GetCell(1, 1), GetCell(1, lastColID));
            headerRange.Font.Bold = true;
            headerRange.VerticalAlignment = Excel.XlVAlign.xlVAlignCenter;
            headerRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            headerRange.EntireColumn.AutoFit();
            headerRange.RowHeight = 40;
            headerRange.Interior.Color = Color.LightBlue;
            headerRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);
            

            Excel.Range tableRange = xlSheet.get_Range(GetCell(1, 1), GetCell(lastRowID, lastColID));
            tableRange.BorderAround2(Excel.XlLineStyle.xlContinuous, Excel.XlBorderWeight.xlThick);

            Excel.Range firstColRange = xlSheet.get_Range(GetCell(2, 1), GetCell(lastRowID, 1));
            firstColRange.Font.Bold = true;
            firstColRange.Interior.Color = Color.LightYellow;

            Excel.Range lastColRange = xlSheet.get_Range(GetCell(2, lastColID), GetCell(lastRowID, lastColID));
            lastColRange.Interior.Color = Color.LightGreen;
            
        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

