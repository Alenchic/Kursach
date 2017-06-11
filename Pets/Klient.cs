using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pets
{
    public partial class Klient : Form
    {
        public Klient()
        {
            InitializeComponent();
        }

        private void Klient_Load(object sender, EventArgs e)
        {
           
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void UpdateTovar()
        {
            DataTableSQl Zay = new DataTableSQl("SELECT * FROM dbo.Tovar_v_magazine_i_na_sklade ");
            dataGridView1.DataSource = Zay.Table.DefaultView;
            DataTableSQl imyafam = new DataTableSQl("select  Postavhik.ID_Postavhik, Postavhik.Nazvanie_organizacii from Postavhik ");
            comboBoxpost.ValueMember = "ID_Postavhik";
            comboBoxpost.DisplayMember = "Nazvanie_organizacii";
            comboBoxpost.DataSource = imyafam.Table.DefaultView;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            panelknp.Visible = true;
            Dvijeniepan.Visible = false;
            spisaniepan.Visible = false;
            Menupan.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(5, 6);
            tabControl2.Location = new Point(277, 6);
            tabControl2.Width = 813;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxskoll.Text == "") return;
            {
                dataGridView2.Rows.Add(IDTV, nametov, kol, cena + " руб.", edinica, summ);
            }
        }
        public Image ByteArrayToImage(byte[] inputArray)
        {
            var memoryStream = new MemoryStream(inputArray);
            return Image.FromStream(memoryStream);
        }

        string IDTV;
        string nametov;
        string edinica;
        int kol;
        string cena;
        string summ;

        private void summa()
        {
            if (textBoxskoll.Text == "") return;
            {

                kol = Convert.ToInt32(textBoxskoll.Text);
                try
                {
                    summ = (Convert.ToInt32(cena) * kol).ToString() + " руб.";
                    textBoxsumm.Text = summ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count == 0) return;
            try
            {
                Byte[] imagef = (Byte[])dataGridView1.CurrentRow.Cells[9].Value;
                pictureBox1.BackgroundImage = ByteArrayToImage(imagef);
                pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;

            }
            catch
            {

            }
            nametov = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cena = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            edinica = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            IDTV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            if (cena == "" || nametov == "") return;
            {
                int ind = cena.Length - 5;
                cena = cena.Remove(ind);
                textBoxTovar.Text = nametov;
                summa();
            }
        }
       
        

        private void textBoxskoll_TextChanged(object sender, EventArgs e)
        {
            summa();
        }

        private void button16_Click(object sender, EventArgs e)
        {
          
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {

            int sum = 0;
            string cenaa;
            for (int j = 0; j < dataGridView2.RowCount; j++)
            {
                if (dataGridView2[5, j].Value != null)
                {
                    cenaa = dataGridView2[5, j].Value.ToString();
                    if (cenaa == "") return;
                    {
                        int ind = cenaa.Length - 5;
                        cenaa = cenaa.Remove(ind);
                        sum = sum + Convert.ToInt32(cenaa);
                    }
                }
            }
            label7.Text = sum.ToString();
        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button11_Click(object sender, EventArgs e)
        {
            tabControl2.Location = new Point(449, 6);
            tabControl2.Width = 643;
            panelknp.Visible = false;
            spisaniepan.Visible = false;
            Menupan.Visible = false;
            panel3.Visible = false;
            Dvijeniepan.Visible = true;
            DataTableSQl Zay = new DataTableSQl("SELECT * FROM dbo.Tovar_v_magazine_i_na_sklade where Tovar_v_magazine_i_na_sklade.Jivotnoe_ID = 3 ");
            dataGridView1.DataSource = Zay.Table.DefaultView;
           
        }

        private void button18_Click(object sender, EventArgs e)
        {
            tabControl2.Location = new Point(449, 6);
            tabControl2.Width = 643;
            panelknp.Visible = false;
            spisaniepan.Visible = false;
            Menupan.Visible = false;
            panel3.Visible = false;
            Dvijeniepan.Visible = true;
        
            DataTableSQl Zay = new DataTableSQl("SELECT * FROM dbo.Tovar_v_magazine_i_na_sklade where Tovar_v_magazine_i_na_sklade.Jivotnoe_ID = 2 ");
            dataGridView1.DataSource = Zay.Table.DefaultView;
           
        }

        private void Klient_Load_1(object sender, EventArgs e)
        {
            panelknp.Visible = true;
            Dvijeniepan.Visible = false;
            spisaniepan.Visible = false;
            Menupan.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(5, 6);
            tabControl2.Location = new Point(277, 6);
            tabControl2.Width = 813;
                
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
        void labl()
        {

        }

        private void label15_MouseEnter(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.Font = new Font("Times New Roman", 8, FontStyle.Underline);
           
        }

        private void label41_MouseLeave(object sender, EventArgs e)
        {
            Label l = sender as Label;
            l.Font = new Font("Times New Roman", 8, FontStyle.Regular);
        }
        void zagr(Label name, string tip)
        {
            tabControl2.Location = new Point(449, 6);
            tabControl2.Width = 643;
            panelknp.Visible = false;
            spisaniepan.Visible = false;
            Menupan.Visible = false;
            panel3.Visible = false;
            Dvijeniepan.Visible = true;
            string jiv = name.Text;
            DataTableSQl Zay = new DataTableSQl("select * from Tovar_v_magazine_i_na_sklade inner join Vid_tovara on Tovar_v_magazine_i_na_sklade.Vid_ID = Vid_tovara.ID_Vid inner join "
            + " Tovar_Jivotn on Tovar_v_magazine_i_na_sklade.Jivotnoe_ID = Tovar_Jivotn.ID_Jivotnogo where Vid_tovara.Naimenovanie = '" + tip + "' and Tovar_Jivotn.Vid_Jivotnogo = '" + jiv + "'");
            dataGridView1.DataSource = Zay.Table.DefaultView;
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(Sobaki, tips);

        }
        private void labelkohki(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(kohk, tips);

        }
        private void labelrib(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(ribi, tips);

        }
        private void labelgrzn(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(grznn, tips);

        }
        private void labelhr(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(horki, tips);

        }
        private void labelptic(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            string tips = lable.Text;
            zagr(ptici, tips);

        }
    }
}
