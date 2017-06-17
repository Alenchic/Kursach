
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Pets
{
    public partial class Sklad : Form
    {
        public Sklad()
        {
            InitializeComponent();
        }
        private void Upda()
        {
            DataTableSQl Zaвy = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii as 'Поставщик', Postavhik.Pochta as 'Почта',"
          + " Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa ");
            dataGridView5.DataSource = Zaвy.Table.DefaultView;
        }
        private void UpdateTovar()
        {
            DataTableSQl prise = new DataTableSQl("SELECT * from Prise ");
            dataGridView1.DataSource = prise.Table.DefaultView;
            DataTableSQl Zay = new DataTableSQl("select ID_Zayzvka as '№', Data_Zayavki as 'Дата заявки', Naimenovanie as 'Статус' from Zayzvka inner join Status_Zakaza on Zayzvka.Statys_ID = Status_Zakaza.ID_Statysa where ID_Statysa = 1");
            dataGridView4.DataSource = Zay.Table.DefaultView;
            DataTableSQl status = new DataTableSQl("SELECT ID_Statysa, Naimenovanie  from dbo.Status_Zakaza ");
            DataRow rr = status.Table.NewRow();
            rr[1] = "Посмотреть все";
            status.Table.Rows.Add(rr);
            comboBox1.ValueMember = "ID_Statysa";
            comboBox1.DisplayMember = "Naimenovanie";
            comboBox1.DataSource = status.Table.DefaultView;
            comboBox1.SelectedIndex = 3;
            DataTableSQl status2 = new DataTableSQl("SELECT ID_Statysa, Naimenovanie  from dbo.Status_Zakaza    ");
            comboBoxizm.ValueMember = "ID_Statysa";
            comboBoxizm.DisplayMember = "Naimenovanie";
            comboBoxizm.DataSource = status2.Table.DefaultView;
        }

        public Image ByteArrayToImage(byte[] inputArray)
        {
            var memoryStream = new MemoryStream(inputArray);
            return Image.FromStream(memoryStream);
        }
        
        string IDTV;
        string nametov;
        string edinica;
        string kol;
        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            UpdateTovar();
        }

       
        private void tabControl2_Selecting_1(object sender, TabControlCancelEventArgs e)
        {
            //tabControl2.SelectedTab.Text.ToString() ==
            string f = tabControl2.SelectedTab.Text.ToString();
            switch (f)
            {
                case "Товары на складе":
                    dataGridView5.Visible = false;
                    comboBox1.Visible = false;
                    dateTimePicker1.Visible = true;
                    button14.Visible = true;
                    button5.Visible = true;
                    button8.Visible = false;
                    comboBoxizm.Visible = false;
                    button1.Visible = true;
                    label5.Text = "Дата списания";
                    break;
                case "Регистрация накладной":
                    dataGridView5.Visible = true;
                    button14.Visible = false;
                    button1.Visible = false;
                    button5.Visible = false;
                    label5.Text = "Статус";
                    button8.Visible = true;
                    comboBox1.Visible = true;
                    dateTimePicker1.Visible = false;
                    spisaniepan.Location = new Point(5, 6);
                    break;
            }
        }

        private void Sklad_Load(object sender, EventArgs e)
        {
            label1.Text = Gl.sotr;
            comboBox1.Visible = false;
            dateTimePicker1.Visible = true;
            button14.Visible = true;
            dataGridView5.Visible = false;
            button5.Visible = true;
            button8.Visible = false;
            comboBoxizm.Visible = false;
            label5.Text = "Дата списания";
            UpdateTovar();
         
        }
        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedCells.Count == 0) return;
            {
                string IDzak = dataGridView4.CurrentRow.Cells[0].Value.ToString();

                DataTableSQl Tovr = new DataTableSQl(" select * from Zay where [№Заявки] = '" + IDzak + "' ");
                dataGridView1.DataSource = Tovr.Table.DefaultView;
            }
        }
    

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells.Count == 0 ) return;
            {
                string IDzakd = dataGridView5.CurrentRow.Cells[1].Value.ToString();
                if (IDzakd == "Завершен" || IDzakd == "Не заказан") 
                {
                    button6.Enabled = false;
                }
                else
                {
                    button6.Enabled = true;
                }
                string IDzak = dataGridView5.CurrentRow.Cells[0].Value.ToString();
                DataTableSQl Tovr = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№',Usloviya_zakaza.Kolvo_tovara as 'Кол-во', Usloviya_zakaza.Summa as 'Сумма', Tovar.Naimenovanie as 'Наименование', "
                   +"Tovar.Cena as 'Цена', Tovar.Firma as 'Фирма' from dbo.Usloviya_zakaza INNER JOIN dbo.Tovar  ON dbo.Usloviya_zakaza.Tovara_ID = dbo.Tovar.ID_Tovara "
                + "INNER JOIN dbo.Zakaz_tovara ON dbo.Usloviya_zakaza.Zakaza_ID = dbo.Zakaz_tovara.ID_Zakaza where Zakaz_tovara.ID_Zakaza = '" + IDzak + "' ");
                dataGridView3.DataSource = Tovr.Table.DefaultView;
            }
        }
        public string tovrid, namr;
        public void Tovar(string Name)
        {
            namr = Name;
            return;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DataTableSQl Tovr = new DataTableSQl("SELECT * from dbo.Dvijenie_na_sklade ");
            dataGridView1.DataSource = Tovr.Table.DefaultView;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Посмотреть все")
            {
                DataTableSQl Zaвy = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii as 'Поставщик', Postavhik.Pochta as 'Почта',"
        + " Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa ");
                dataGridView5.DataSource = Zaвy.Table.DefaultView;
            }
            else
            {
                DataTableSQl Zaвy = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii as 'Поставщик', Postavhik.Pochta as 'Почта',"
       + " Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik INNER JOIN "
       + " dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa where ID_Statysa = '" + comboBox1.SelectedValue.ToString() + "'   ");
                dataGridView5.DataSource = Zaвy.Table.DefaultView;
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (comboBoxizm.Visible == true)
            {
                comboBoxizm.Visible = false;
            }
            else
            {
                comboBoxizm.Visible = true;
            }
        
        }

        private void comboBoxizm_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxizm.Visible == false) return;
            {  
            DialogResult resolt = MessageBox.Show("Изменить статус?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
                {
                    string IDzak = dataGridView5.CurrentRow.Cells[0].Value.ToString();
                    ConnectionClass ConCheck = new ConnectionClass();
                    ConCheck.Connection_Options();
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    connection.Open();
                    SqlCommand command2 = new SqlCommand("UPDATE Zakaz_tovara SET Statysa_ID = '" + comboBoxizm.SelectedValue.ToString() + "' WHERE ID_Zakaza ='" + IDzak + "'", connection);
                    command2.ExecuteNonQuery();
                    connection.Close();
                    Upda();

                }
            }
        }
        void poisk(string text)
        {
            DataTableSQl poisk = new DataTableSQl("select * From   prise  where concat(Наименование, Цена , [Кол-во], [ЕД.],Фирма, [Место хранение], Адрес) like  '%" + text + "%'");
            dataGridView1.DataSource = poisk.Table.DefaultView;
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            poisk(textBox2.Text);
        }

        private void Sklad_FormClosing(object sender, FormClosingEventArgs e)
        {
            Glav gll = new Glav();
            gll.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int count = 0;
            string IDzak = dataGridView5.CurrentRow.Cells[0].Value.ToString();
            for (int j = 0; j < dataGridView3.RowCount; j++)
            {
                for (int i = 0; i < dataGridView3.ColumnCount; i++)
                {
                    if (dataGridView3[i, j].Value != null)
                    {
                        count++;
                        Gl.post = dataGridView3[5, j].Value.ToString();
                        Gl.summ = dataGridView3[1, j].Value.ToString();
                        Gl.ID = dataGridView3[0, j].Value.ToString();
                        Tovar(dataGridView3[3, j].Value.ToString());
                        Registraciya re = new Registraciya();
                        re.textBoxName.Text = namr;
                        re.ShowDialog();
                            break;
                    }  
                }
            }
            DialogResult resolt = MessageBox.Show("Изменить статус?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                ConnectionClass ConCheck = new ConnectionClass();
                ConCheck.Connection_Options();
                SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                connection.Open();
                SqlCommand command2 = new SqlCommand("UPDATE Zakaz_tovara SET Statysa_ID = '3' WHERE ID_Zakaza ='" + IDzak + "'", connection);
                command2.ExecuteNonQuery();
                connection.Close();
                Upda();
            }

        }
    }
}
