using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pets
{
    public partial class Sklad : Form
    {
        public Sklad()
        {
            InitializeComponent();
        }
        private void UpdateTovar()
        {
            DataTableSQl Zaвy = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii as 'Поставщик', Postavhik.Pochta as 'Почта',"
           + " Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa ");
            dataGridView5.DataSource = Zaвy.Table.DefaultView;
            DataTableSQl Zay = new DataTableSQl("SELECT * from Prise ");
            dataGridView1.DataSource = Zay.Table.DefaultView;
            DataTableSQl imyafam = new DataTableSQl("select  Postavhik.ID_Postavhik, Postavhik.Nazvanie_organizacii from Postavhik ");
            comboBoxpost.ValueMember = "ID_Postavhik";
            comboBoxpost.DisplayMember = "Nazvanie_organizacii";
            comboBoxpost.DataSource = imyafam.Table.DefaultView;
      
        }

        private void UpdateComboBox()
        {
            DataTableSQl kuda = new DataTableSQl("select  Mesto_hraneniya.ID_Mesto_hraneniya, CONCAT(Nazvanie,' ',Adres) as 'sotr' from Mesto_hraneniya ");
            comboBoxKuda.ValueMember = "ID_Mesto_hraneniya";
            comboBoxKuda.DisplayMember = "sotr";
            comboBoxKuda.DataSource = kuda.Table.DefaultView;
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

        private void button4_Click_1(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Перевести товары?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                switch (comboBoxKuda.Text == textBoxOtkuda.Text)

                {
                    case (false):
                        string data = dateTimePicker1.Value.ToShortDateString();
                        ConnectionClass ConCheck = new ConnectionClass();
                        ConCheck.Connection_Options();
                        SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.add_Dvijenie_na_skladee", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Data", data);
                        command.Parameters.AddWithValue("@Kuda", comboBoxKuda.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Otkuda", 1);
                        int ii = 0;
                        command.Parameters.AddWithValue("@newif", ii);
                        int iii = (int)command.ExecuteScalar();
                        int count = 0;
                        for (int j = 0; j < dataGridView2.RowCount; j++)
                        {
                            for (int i = 0; i < dataGridView2.ColumnCount; i++)
                            {
                                if (dataGridView2[i, j].Value != null)
                                {
                                    count++;
                                    string tovrid = dataGridView2[0, j].Value.ToString();
                                    string kolvo = dataGridView2[2, j].Value.ToString();
                                    string ed = dataGridView2[3, j].Value.ToString();
                                    MessageBox.Show(tovrid + kolvo + ed);
                                    SqlCommand command2 = new SqlCommand("UPDATE Tovar_v_magazine_i_na_sklade SET Kol_vo_naskl =Kol_vo_naskl -" + kolvo + " WHERE ID_Tovar_v_magazine_i_na_sklade = "+ tovrid, connection);
                                    command2.ExecuteNonQuery();
                                    UpdateTovar();
                                    
                                    SqlCommand command1 = new SqlCommand("dbo.add_Tovari_dlya_peremeheniya", connection);
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.AddWithValue("@Edinica", ed);
                                    command1.Parameters.AddWithValue("@Kol_vo", kolvo);
                                    command1.Parameters.AddWithValue("@Tovar_v_magazine_i_na_sklade_ID", tovrid);
                                    command1.Parameters.AddWithValue("@Dvijenie_na_sklade_ID", iii);

                                    command1.ExecuteNonQuery();
                                    break;
                                }
                            }
                        }
                        connection.Close();
                        break;
                    case (true):
                        MessageBox.Show("Невозможно перевести");
                        break;
                }
            }
        }
        
        private void button1_Click_1(object sender, EventArgs e)
        {
            dataGridView1.AutoResizeColumns();
            UpdateTovar();
        }
            

        private void button2_Click_1(object sender, EventArgs e)
        {
            if (textBoxskoll.Text == "") return;
            {
                kol = textBoxskoll.Text;
                dataGridView2.Rows.Add(IDTV, nametov, kol, edinica);
            }
        }

        private void dataGridView1_SelectionChanged_1(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedCells.Count ==  0 || dataGridView1.SelectedCells.Count <= 4) return;
            {
                try
                {
                    Byte[] imagef = (Byte[])dataGridView1.CurrentRow.Cells[7].Value;
                    pictureBox1.BackgroundImage = ByteArrayToImage(imagef);
                    pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;

                }
                catch
                {

                }
                nametov = dataGridView1.CurrentRow.Cells[2].Value.ToString();
                //cena = dataGridView1.CurrentRow.Cells[1].Value.ToString();
                IDTV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
                edinica = dataGridView1.CurrentRow.Cells[4].Value.ToString();
                textBoxTovar.Text = nametov;
            }
        }

        private void tabControl2_Selecting_1(object sender, TabControlCancelEventArgs e)
        {
            //tabControl2.SelectedTab.Text.ToString() ==
            string f = tabControl2.SelectedTab.Text.ToString();
            switch (f)
            {
                case "Товары на складе":
                    Dvijeniepan.Visible = true;
                    spisaniepan.Visible = false;
                    dataGridView5.Visible = false;
                    break;
                case "Регистрация накладной":
                    Dvijeniepan.Visible = false;
                    spisaniepan.Visible = true;
                    dataGridView5.Visible = true;
                    button14.Visible = false;
                    button5.Visible = false;
                    spisaniepan.Location = new Point(5, 6);
                    break;
            }
        }

      

        private void comboBoxpost_SelectedIndexChanged(object sender, EventArgs e)
        {
            //string s = comboBoxpost.Text;
            //DataTableSQl Zay = new DataTableSQl("SELECT * FROM dbo.Tovar inner join dbo.Postavhik  ON dbo.Tovar.Postavhik_ID = dbo.Postavhik.ID_Postavhik Where  Postavhik.Nazvanie_organizacii = '" + s + "'");
            //dataGridView1.DataSource = Zay.Table.DefaultView;
        }

        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
          
        }

        private void Sklad_Load(object sender, EventArgs e)
        {
            UpdateTovar();
            UpdateComboBox();
        }


        private void button9_Click(object sender, EventArgs e)
        {
                    Dvijeniepan.Visible = true;
                    spisaniepan.Visible = false;
                    Menupan.Visible = false;
            //spisaniepan.Location = new Point(5, 6);


        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (Menupan.Visible == true)
            { Menupan.Visible = false; }
            else
            { Menupan.Visible = true; }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Dvijeniepan.Visible = false;
            spisaniepan.Visible = true;
            Menupan.Visible = false;
            spisaniepan.Location = new Point(5, 6);
            spisaniepan.Size = new Size(444, 384);
            DataTableSQl Zay1 = new DataTableSQl("SELECT * FROM dbo.Zayzvka ");
            dataGridView4.DataSource = Zay1.Table.DefaultView;
        }

      

        private void button3_Click(object sender, EventArgs e)
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

        private void button17_Click(object sender, EventArgs e)
    {
        DialogResult resolt = MessageBox.Show("Очистить таблицу?", "Сообщение", MessageBoxButtons.YesNo);
        if (resolt == DialogResult.No) return;
        {
            dataGridView2.Rows.Clear();

        }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            Registraciya reg = new Registraciya();

            reg.Show();
        }

        private void spisaniepan_Paint(object sender, PaintEventArgs e)
        {

        }

        private void dataGridView4_SelectionChanged(object sender, EventArgs e)
        {
            //if (dataGridView4.SelectedCells.Count == 0) return;
            //{
            //    string IDzak = dataGridView4.CurrentRow.Cells[0].Value.ToString();

            //    DataTableSQl Tovr = new DataTableSQl("SELECT * FROM dbo.Vibor INNER JOIN dbo.Tovar_v_magazine_i_na_sklade  ON dbo.Vibor.Tovar_v_magazine_i_na_sklade_ID" +
            //    "= dbo.Tovar_v_magazine_i_na_sklade.ID_Tovar_v_magazine_i_na_sklade INNER JOIN dbo.Zayzvka  ON dbo.Vibor.Zayavka_ID = dbo.Zayzvka.ID_Zayzvka  where Vibor.Zayavka_ID ='" + IDzak + "' ");
            //    dataGridView1.DataSource = Tovr.Table.DefaultView;
        }
    

        private void dataGridView5_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView5.SelectedCells.Count == 0) return;
            {
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
        

        private void button6_Click(object sender, EventArgs e)
        {
            int count = 0;
            for (int j = 0; j < dataGridView3.RowCount; j++)
            {
                for (int i = 0; i < dataGridView3.ColumnCount; i++)
                {
                    if (dataGridView3[i, j].Value != null)
                    {
                        count++;
                        //tovrid = dataGridView3[0, j].Value.ToString();
                        Gl.post = dataGridView3[5, j].Value.ToString();
                        Gl.summ = dataGridView3[1, j].Value.ToString();
                        Gl.ID = dataGridView3[0, j].Value.ToString();
                        Tovar(dataGridView3[3, j].Value.ToString());
                        //MessageBox.Show(tovrid + namr);
                        Registraciya re = new Registraciya();
                        re.textBoxName.Text = namr;
                        re.ShowDialog();
                        break;
                    }  
                }
            }
         
        }
    }
}
