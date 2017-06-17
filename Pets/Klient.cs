using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
namespace Pets
{
    public partial class Klient : Form
    {
        public Klient()
        {
            InitializeComponent();
        }

        

        private void UpdateTovar()
        {
            DataTableSQl Zay = new DataTableSQl("SELECT * FROM dbo.Tovar_v_magazine_i_na_sklade ");
            dataGridView1.DataSource = Zay.Table.DefaultView;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            panelknp.Visible = true;
            panel1.Visible = false;
            panel1.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(5, 6);
            tabControl2.Location = new Point(277, 6);
            tabControl2.Width = 813;
        }

        string IDTV;
        string nametov;
        string edinica;
        int kol;
        string cena;
        string summ;

        public Image ByteArrayToImage(byte[] inputArray)
        {
            var memoryStream = new MemoryStream(inputArray);
            return Image.FromStream(memoryStream);
        }


        private void summa()
        {
            if (textBoxkol.Text == "") return;
            {

                kol = Convert.ToInt32(textBoxkol.Text);
                try
                {
                    summ = (Convert.ToInt32(cena) * kol).ToString() + " руб.";
                    labelsumma.Text = summ;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataTableSQl Tovr = new DataTableSQl("select Pict,ID_Tovar_v_magazine_i_na_sklade from Tovar_v_magazine_i_na_sklade");
            if (dataGridView1.SelectedCells.Count == 0) return;
            try
            {
                string tt = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                for (int i = 0; i < Tovr.Table.Rows.Count; i++)
                {
                    string picth = Tovr.Table.Rows[i][1].ToString();
                    if (picth == tt)
                    {
                        Byte[] yy = (Byte[])Tovr.Table.Rows[i][0];
                        Byte[] imagef = (Byte[])yy;
                        pictureBox9.BackgroundImage = ByteArrayToImage(imagef);
                        pictureBox9.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
            catch
            {

            }
            IDTV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nametov = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            cena = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            edinica = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            try
            {
                if (cena == "" || nametov == "") return;
                {

                    int ind = cena.Length - 5;
                    cena = cena.Remove(ind);
                    textBoxname.Text = nametov;

                    summa();
                }
            }
            catch
            {

            }
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
            label10.Text = sum.ToString() + " руб.";
        }

       

        private void Klient_Load_1(object sender, EventArgs e)
        {
            panelknp.Visible = true;
            panel1.Visible = false;
            panel3.Visible = true;
            panel3.Location = new Point(5, 6);
            tabControl2.Location = new Point(277, 6);
            tabControl2.Width = 813;
            label5.Text = Gl.sotr;
                
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
        string tips;
        string jiv;
        void zagr(Label name, string tip)
        {
            tabControl2.Location = new Point(449, 6);
            tabControl2.Width = 643;
            panelknp.Visible = false;
            panel3.Visible = false;
            panel1.Visible = true;
            jiv = name.Text;
            DataTableSQl Zay = new DataTableSQl("select * from Prise where[Вид товара] = '" + tip + "' and Животное = '" + jiv + "'");
            dataGridView1.DataSource = Zay.Table.DefaultView;
        }

        private void label21_Click(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(Sobaki, tips);

        }
        private void labelkohki(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(kohk, tips);

        }
        private void labelrib(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(ribi, tips);

        }
        private void labelgrzn(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(grznn, tips);

        }
        private void labelhr(object sender, EventArgs e)
        {
            
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(horki, tips);

        }
        private void labelptic(object sender, EventArgs e)
        {
            Label lable = sender as Label;
            tips = lable.Text;
            zagr(ptici, tips);

        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (textBoxkol.Text == "") return;
            if (dataGridView2.Rows[0].Cells[0].Value == null)
            {
                dataGridView2.Rows.Add(IDTV, nametov, kol, cena + " руб.",edinica ,summ);
            }
            else
            {
                for (int i = 0; i < dataGridView2.Rows.Count - 1; i++)
                {
                    if (IDTV == dataGridView2[0, i].Value.ToString())
                    {
                        MessageBox.Show("Такой товар уже есть");
                        string dd = dataGridView2[0, i].Value.ToString();
                        return;

                    }
                }
                dataGridView2.Rows.Add(IDTV, nametov, kol, cena + " руб.", edinica, summ);
            }
        }

        private void textBoxkol_TextChanged(object sender, EventArgs e)
        {

            summa();
        }

        void poisk(string text)
        {
            DataTableSQl poisk = new DataTableSQl("select * From   prise  where [Вид товара] = '"+tips+"' and Животное = '"+jiv+"' and concat([№],Наименование, Цена , [Кол-во], [ЕД.],Фирма, [Место хранение], Адрес) like  '%" + text + "%'");
            dataGridView1.DataSource = poisk.Table.DefaultView;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            poisk(textBox1.Text);
        }

        private void button15_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridView2.CurrentRow.Index;
                dataGridView2.Rows.RemoveAt(index);
            }
            catch
            {

            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Очистить таблицу?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                dataGridView2.Rows.Clear();

            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Оформить заказ?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                Pocupka poc = new Pocupka();
                DataTable dataTable = new DataTable();
                dataTable.Columns.Add(new DataColumn("№", System.Type.GetType("System.String")));
                dataTable.Columns.Add(new DataColumn("Наименование", System.Type.GetType("System.String")));
                dataTable.Columns.Add(new DataColumn("Кол-во", System.Type.GetType("System.String")));
                dataTable.Columns.Add(new DataColumn("Цена", System.Type.GetType("System.String")));
                dataTable.Columns.Add(new DataColumn("Единица", System.Type.GetType("System.String")));
                dataTable.Columns.Add(new DataColumn("Сумма", System.Type.GetType("System.String")));
                Gl.summ = label10.Text;
                int count = 0;
                for (int j = 0; j < dataGridView2.RowCount; j++)
                {
                    for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    {
                        if (dataGridView2[i, j].Value != null)
                        {
                            count++;
                            string tovrid = dataGridView2[0, j].Value.ToString();
                            string name = dataGridView2[1, j].Value.ToString();
                            string kol = dataGridView2[2, j].Value.ToString();
                            string cena = dataGridView2[3, j].Value.ToString();
                            string ed = dataGridView2[4, j].Value.ToString();
                            string sum = dataGridView2[5, j].Value.ToString();
                            DataRow rowToAdd = dataTable.NewRow();
                            rowToAdd[0] = tovrid;
                            rowToAdd[1] = name;
                            rowToAdd[2] = kol;
                            rowToAdd[3] = cena;
                            rowToAdd[4] = ed;
                            rowToAdd[5] = sum;
                            dataTable.Rows.Add(rowToAdd);
                            break;
                        }
                    }
                }
                Gl.Table = dataTable;
                poc.ShowDialog();

            }
        }

        private void Klient_FormClosing(object sender, FormClosingEventArgs e)
        {
            Glav gll = new Glav();
            gll.Show();
        }
    }
}
