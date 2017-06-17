
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Windows.Forms; 
using Word = Microsoft.Office.Interop.Word;


namespace Pets
{
    public partial class Zakupka : Form
    {
        public Zakupka()
        {
            InitializeComponent();
        }

        private readonly string TemplateFileName = @"C:\Users\Лёшка\Documents\Visual Studio 2015\Projects\Pets\1.doc";
        string posft ;
        private void UpdateStatus()
        {

            DataTableSQl Tovr = new DataTableSQl("SELECT ID_Postavhik as '№', Nazvanie_organizacii as 'Поставщик', Adres as 'Адрес', Telefon as 'Телефон', Pochta "
                   + " as 'Почта', Naimenovanie as 'Категория' from dbo.Postavhik INNER JOIN dbo.Kategoriya_postavchika  ON dbo.Postavhik.Kategoriya_ID ="
                    + " dbo.Kategoriya_postavchika.ID_Kategoriya  ");
            dataGridViewpost.DataSource = Tovr.Table.DefaultView;
            DataTableSQl Zay = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii 'Поставщик', Zakaz_tovara.Summa as "
            +" 'Сумма', Postavhik.Pochta 'Почта', Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik"
            +" INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa ");
            dataGridZakk.DataSource = Zay.Table.DefaultView;
            DataTableSQl imyafam = new DataTableSQl("select  Postavhik.ID_Postavhik, Postavhik.Nazvanie_organizacii from Postavhik ");
            comboBoxpost.ValueMember = "ID_Postavhik";
            comboBoxpost.DisplayMember = "Nazvanie_organizacii";
            comboBoxpost.DataSource = imyafam.Table.DefaultView;
            DataTableSQl jiv = new DataTableSQl("select  Tovar_Jivotn.ID_Jivotnogo, Tovar_Jivotn.Vid_Jivotnogo from Tovar_Jivotn ");
            comboBoxjiv.ValueMember = "ID_Jivotnogo";
            comboBoxjiv.DisplayMember = "Vid_Jivotnogo";
            comboBoxjiv.DataSource = jiv.Table.DefaultView;
            DataTableSQl tip = new DataTableSQl("select  Vid_tovara.ID_Vid, Vid_tovara.Naimenovanie from Vid_tovara ");
            comboBoxtip.ValueMember = "ID_Vid";
            comboBoxtip.DisplayMember = "Naimenovanie";
            comboBoxtip.DataSource = tip.Table.DefaultView;
            DataTableSQl kategoriya = new DataTableSQl("select ID_Kategoriya, Naimenovanie from Kategoriya_postavchika ");
            comboBoxkategr.ValueMember = "ID_Kategoriya";
            comboBoxkategr.DisplayMember = "Naimenovanie";
            comboBoxkategr.DataSource = kategoriya.Table.DefaultView;
            DataTableSQl status = new DataTableSQl("SELECT ID_Statysa, Naimenovanie  from dbo.Status_Zakaza    ");
            DataRow rr = status.Table.NewRow();
            rr[1] = "Посмотреть все";
            status.Table.Rows.Add(rr);
            comboBoxtol.ValueMember = "ID_Statysa";
            comboBoxtol.DisplayMember = "Naimenovanie";
            comboBoxtol.DataSource = status.Table.DefaultView;
            comboBoxtol.SelectedIndex = 3;
            DataTableSQl izm = new DataTableSQl("SELECT ID_Statysa, Naimenovanie  from dbo.Status_Zakaza    ");
            comboBoxizm.ValueMember = "ID_Statysa";
            comboBoxizm.DisplayMember = "Naimenovanie";
            comboBoxizm.DataSource = izm.Table.DefaultView;
        


        }

        private void tabControl2_Selecting(object sender, TabControlCancelEventArgs e)
        {
            string f = tabControl2.SelectedTab.Text.ToString();
            switch (f)
            {
                case "Оформление заказа":          
                    panel1.Visible = true;
                    panel2.Visible = false;
                    panel3.Visible = false;
                    break;
                case "Проведение заказа":
                    panel1.Visible = false;
                    panel2.Visible = true;
                    panel3.Visible = false;
                    panel2.Location = new Point(5,6);
                    break;
                case "Регистрация поставщика":
                   
                    panel1.Visible = false;
                    panel2.Visible = false;
                    panel3.Visible = true;
                    panel3.Location = new Point(5, 6);
                    break;
            }
            }


        public Image ByteArrayToImage(byte[] inputArray)
        {
            var memoryStream = new MemoryStream(inputArray);
            return Image.FromStream(memoryStream);

           
        }

        string cena;
        string IDTV;
        string nametov;
        string summ;
        int kol;

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            DataTableSQl Tovr = new DataTableSQl("select Tovar.Pict , Tovar.ID_Tovara from Tovar");
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
                        pictureBox1.BackgroundImage = ByteArrayToImage(imagef);
                        pictureBox1.BackgroundImageLayout = ImageLayout.Zoom;
                    }
                }
            }
            catch
            {

            }
            IDTV = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            nametov = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            cena = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            try
            {
                if (cena == "" || nametov == "") return;
                {

                    int ind = cena.Length - 5;
                    cena = cena.Remove(ind);
                    textBoxTovar.Text = nametov;
                    summa();
                }
            }
            catch
            {

            }

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

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
           
            summa();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBoxkol.Text == "") return;
            if (dataGridView2.Rows[0].Cells[0].Value == null)
            {
                dataGridView2.Rows.Add(IDTV, nametov, kol, cena + " руб.", summ);
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
                dataGridView2.Rows.Add(IDTV, nametov, kol, cena + " руб.", summ);  
            }

        }

        private void button4_Click(object sender, EventArgs e)
        {

            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            string obh = label10.Text;
             string seichas =  date1.ToLongDateString();
            DialogResult resolt = MessageBox.Show("Создать заказ?","Сообщение",MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
                {
                switch (dataGridView2.Rows[0].Cells[0].Value == null)

                {
                    case (false):
                        ConnectionClass ConCheck = new ConnectionClass();
                        ConCheck.Connection_Options();
                        SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.add_Zakaz_tovara", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Data_zakaza", seichas);
                        command.Parameters.AddWithValue("@Postavhik_ID", comboBoxpost.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Statysa_ID", 2);
                        command.Parameters.AddWithValue("@summa", obh);
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
                                    string sumt = dataGridView2[4, j].Value.ToString();
                                    SqlCommand command1 = new SqlCommand("dbo.add_Usloviya_zakaza", connection);
                                    command1.CommandType = CommandType.StoredProcedure;
                                    command1.Parameters.AddWithValue("@Kolvo_tovara", kolvo);
                                    command1.Parameters.AddWithValue("@Summa", sumt);
                                    command1.Parameters.AddWithValue("@Tovara_ID", tovrid);
                                    command1.Parameters.AddWithValue("@Zakaza_ID", iii);
                                    command1.ExecuteNonQuery();
                                    break;
                                }
                            }
                        }
                        connection.Close();
                        break;
                    case (true):
                        MessageBox.Show("Заполните все пустые поля");
                        break;
                }
            }
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

        private void comboBoxpost_SelectedIndexChanged(object sender, EventArgs e)
        {
            posft = comboBoxpost.Text;
            DataTableSQl pos = new DataTableSQl("select [№], Цена,Наименование, Фирма, Животное, [Тип товара] from posttovrlist where  Поставщик = '" + posft + "'");
            dataGridView1.DataSource = pos.Table.DefaultView;
        }
        void zak()
        {
            string IDzak = dataGridZakk.CurrentRow.Cells[0].Value.ToString();
            DataTableSQl Tovr = new DataTableSQl("SELECT Usloviya_zakaza.ID_Usloviya as '№', Tovar.Naimenovanie as 'Наименование',  Tovar.Firma as 'Фирма', Tovar.Cena as 'Цена', "
                + "Usloviya_zakaza.Kolvo_tovara as 'Кол-во', Usloviya_zakaza.Summa as 'Сумма' from dbo.Usloviya_zakaza INNER JOIN dbo.Tovar  ON dbo.Usloviya_zakaza.Tovara_ID = dbo.Tovar.ID_Tovara "
                + "INNER JOIN dbo.Zakaz_tovara ON dbo.Usloviya_zakaza.Zakaza_ID = dbo.Zakaz_tovara.ID_Zakaza where Zakaz_tovara.ID_Zakaza = '" + IDzak + "' ");
            dataGridView4.DataSource = Tovr.Table.DefaultView;
        }
        private void dataGridView3_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridZakk.SelectedCells.Count == 0) return;
            {
                zak();
            }

        }


        public void poiskSQL(string text)
        {
            DataTableSQl poisk = new DataTableSQl("select [№], Цена,Наименование, Фирма, Животное, [Тип товара] From  posttovrlist where concat([№],Наименование, Цена, Фирма, Животное, [Тип товара]) like '%" + text+"%'");
            dataGridView1.DataSource = poisk.Table.DefaultView;
        }

        private void Zakupka_Load(object sender, EventArgs e)
        {
            label5.Text = Gl.sotr;
            panel7.Visible = false;
            dataGridZakk.Location = new Point(10, 32);
            dataGridZakk.Height = 315;
            panel(panel5, dataGridView1);
            UpdateStatus();
            panel1.Visible = true;
            panel2.Visible = false;
            panel3.Visible = false;
        }
        private void dataGridViewpost_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewpost.SelectedCells.Count == 0) return;
            {
                string IDzak = dataGridViewpost.CurrentRow.Cells[1].Value.ToString();
                DataTableSQl Zay = new DataTableSQl("select [№], Цена,Наименование, Фирма, Животное, [Тип товара] from posttovrlist where  Поставщик = '" + IDzak + "'");
                dataGridViewPrise.DataSource = Zay.Table.DefaultView;
                
            }

        }

        private void button11_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            poiskSQL(textBox1.Text);
        }

        private void textBoxkol_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Char.IsNumber(e.KeyChar)  | e.KeyChar == '\b') return;
            else
                e.Handled = true;
        }

        void panel(Panel pan, DataGridView data)
        {

            if (pan.Visible == false)
            {
                pan.Visible = true;
                data.Location = new Point(10, 74);
                data.Height = 273;
            }
            else
            {
                pan.Visible = false;
                data.Location = new Point(10, 32);
                data.Height = 315;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel(panel5, dataGridView1); 
        }
        private void button_Clickzakk(object sender, EventArgs e)
        {
            panel(panel7, dataGridZakk); 
        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            string jiv = comboBoxjiv.Text;
            string tip = comboBoxtip.Text;
            string post = comboBoxpost.Text;

            try
            {
                DataTableSQl Zay = new DataTableSQl("select [№], Цена,Наименование, Фирма, Животное, [Тип товара] from posttovrlist where Поставщик ='"+post+"' and Животное ='"+jiv+"' and [Тип товара] ='"+tip+"'");
                dataGridView1.DataSource = Zay.Table.DefaultView;
            }
            catch (Exception d)
            {
                MessageBox.Show(d.Message);
            }
            panel(panel5, dataGridView1);
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Очистить таблицу?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                dataGridView2.Rows.Clear();

            }
        }

        private void dataGridView2_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
            int sum = 0;
            string cenaa;
            for (int j = 0; j < dataGridView2.RowCount; j++)
            {
                if (dataGridView2[4, j].Value != null)
                {
                    cenaa = dataGridView2[4, j].Value.ToString();
                    if (cenaa == "") return;
                    {
                        int ind = cenaa.Length - 5;
                        cenaa = cenaa.Remove(ind);
                        sum = sum + Convert.ToInt32(cenaa);
                    }
                }
            }
            label10.Text = sum.ToString() +" руб.";
        }

        private void button6_Click(object sender, EventArgs e)
        {

            DateTime date1 = new DateTime();
            date1 = DateTime.Now;
            string obh = label10.Text;
            string seichas = date1.ToLongDateString();
            DialogResult resolt = MessageBox.Show("Создать заказ?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                switch (dataGridView4.Rows[0].Cells[0].Value == null)

                {
                    case (false):
                        var wordapp = new Word.Application();
                        wordapp.Visible = false;
                        var worddoc = wordapp.Documents.Open(TemplateFileName);
                        Word.Table tab = worddoc.Tables[1];
                        int count = 0;
                        int ss = 1;
                        for (int j = 0; j < dataGridView4.RowCount; j++)
                        {
                            for (int i = 0; i < dataGridView4.ColumnCount; i++)
                            {
                                if (dataGridView4[i, j].Value != null)
                                {
                                    ss++;
                                    count++;
                                    string ID = dataGridView4[0, j].Value.ToString();
                                    string nam = dataGridView4[1, j].Value.ToString();
                                    string fir = dataGridView4[2, j].Value.ToString();
                                    string cena = dataGridView4[3, j].Value.ToString();
                                    string kol = dataGridView4[4, j].Value.ToString();
                                    string sum = dataGridView4[5, j].Value.ToString();
                                    try
                                    {
                                        tab.Rows.Add();
                                        tab.Rows[ss].Cells[1].Range.Text = (ss-1).ToString();
                                        tab.Rows[ss].Cells[2].Range.Text = nam;
                                        tab.Rows[ss].Cells[3].Range.Text = ID;
                                        tab.Rows[ss].Cells[4].Range.Text = fir;
                                        tab.Rows[ss].Cells[5].Range.Text = cena;
                                        tab.Rows[ss].Cells[6].Range.Text = kol;
                                        tab.Rows[ss].Cells[7].Range.Text = sum;
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.Message); 

                                    }
                               
                                    break;
                                }
                            }
                        }
                        wordapp.Visible = true;
                        worddoc.SaveAs2(@"C:\Users\Лёшка\Documents\Visual Studio 2015\Projects\Pets\2.doc");
                        break;
                    case (true):
                        MessageBox.Show("Заполните все пустые поля");
                        break;
                }
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            posft = comboBoxpost.Text;
            DataTableSQl pos = new DataTableSQl("select [№], Цена,Наименование, Фирма, Животное, [Тип товара] from posttovrlist where  Поставщик = '" + posft + "'");
            dataGridView1.DataSource = pos.Table.DefaultView;
            panel5.Visible = false;
            dataGridView1.Location = new Point(10, 32);
            dataGridView1.Height = 315;
        }

      
        private void button11_Click_1(object sender, EventArgs e)
        {
            if (dataGridZakk.SelectedCells.Count == 0) return;
            {
                Gl.ID = dataGridZakk.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 2;
                Formdel izm = new Formdel();
                izm.ShowDialog();
                UpdateStatus() ;
            }

        }

        private void button8_Click_1(object sender, EventArgs e)
        {
            if (dataGridZakk.SelectedCells.Count == 0) return;
            {
                Gl.ID = dataGridZakk.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 1;
                FormIzm izm = new FormIzm();
                izm.ShowDialog();
                UpdateStatus();
            }
        }
        private void button5_Click(object sender, EventArgs e)
        {
            if (dataGridView4.SelectedCells.Count == 0) return;
            {
                Gl.ID2 = dataGridZakk.CurrentRow.Cells[0].Value.ToString();
                Gl.ID = dataGridView4.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 1;
                Formdel izm = new Formdel();
                izm.ShowDialog();
                zak();
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Добавить поставщика?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                switch ( textBoxadres.Text == "" || textBoxname.Text == "" || textBoxpoch.Text == "" || maskedTextBoxtel.Text == "")

                {
                    case (false):
                      
                        ConnectionClass ConCheck = new ConnectionClass();
                        ConCheck.Connection_Options();
                        SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                        connection.Open();
                        SqlCommand command = new SqlCommand("dbo.add_Postavhik", connection);
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Nazvanie_organizacii", textBoxname.Text);
                        command.Parameters.AddWithValue("@Adres", textBoxadres.Text);
                        command.Parameters.AddWithValue("@Telefon", maskedTextBoxtel.Text);
                        command.Parameters.AddWithValue("@Kategoriya_ID", comboBoxkategr.SelectedValue.ToString());
                        command.Parameters.AddWithValue("@Pochta", textBoxpoch.Text);
                        command.ExecuteNonQuery();
                        connection.Close();
                        UpdateStatus();
                        break;
                    case (true):
                        MessageBox.Show("Заполните все пустые поля");
                        break;
                }
            }
        }
        string id;
        private void button17_Click(object sender, EventArgs e)
        {
            if (dataGridViewpost.SelectedCells.Count == 0) return;
            {
                id = dataGridViewpost.CurrentRow.Cells[0].Value.ToString();
                string name = dataGridViewpost.CurrentRow.Cells[1].Value.ToString();
                string adres = dataGridViewpost.CurrentRow.Cells[2].Value.ToString();
                string telefon = dataGridViewpost.CurrentRow.Cells[3].Value.ToString();
                string pocht = dataGridViewpost.CurrentRow.Cells[4].Value.ToString();
                textBoxname.Text = name;
                textBoxpoch.Text = pocht;
                textBoxadres.Text = adres;
                maskedTextBoxtel.Text = telefon;
               
            }

        }
        private void button13_Click(object sender, EventArgs e)
        {
            if (dataGridViewpost.SelectedCells.Count == 0) return;
            {
                Gl.ID2 = dataGridViewpost.CurrentRow.Cells[1].Value.ToString();
                Gl.ID = dataGridViewpost.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 3;
                Formdel izm = new Formdel();
                izm.ShowDialog();
                UpdateStatus();
            }
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            switch ( MessageBox.Show("Сохранить изменения?", "Изменение", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    ConnectionClass ConCheck = new ConnectionClass();
                    ConCheck.Connection_Options();
                    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    connection.Open();
                    SqlCommand command = new SqlCommand("dbo.upd_Postavhik", connection);
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Nazvanie_organizacii", textBoxname.Text);
                    command.Parameters.AddWithValue("@Adres", textBoxadres.Text);
                    command.Parameters.AddWithValue("@Telefon", maskedTextBoxtel.Text);
                    command.Parameters.AddWithValue("@Kategoriya_ID", comboBoxkategr.SelectedValue.ToString());
                    command.Parameters.AddWithValue("@Pochta", textBoxpoch.Text);
                    command.Parameters.AddWithValue("@ID_Postavhik", id);
                    command.ExecuteNonQuery();
                    connection.Close();
                    UpdateStatus();
                    textBoxadres.Clear();
                    textBoxname.Clear();
                    textBoxpoch.Clear();
                    maskedTextBoxtel.Clear();
                    break;
            }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (dataGridViewpost.SelectedCells.Count == 0) return;
            {
                Gl.ID = dataGridViewpost.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 2;
                FormIzm izm = new FormIzm();
                izm.ShowDialog();
                UpdateStatus();
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (dataGridViewPrise.SelectedCells.Count == 0) return;
            {
                Gl.ID = dataGridViewPrise.CurrentRow.Cells[0].Value.ToString();
                Gl.poch = dataGridViewPrise.CurrentRow.Cells[1].Value.ToString();
                Gl.post = dataGridViewPrise.CurrentRow.Cells[2].Value.ToString();
                Gl.status = dataGridViewPrise.CurrentRow.Cells[3].Value.ToString();
                Gl.summ = dataGridViewPrise.CurrentRow.Cells[4].Value.ToString();
                Gl.ID2 = dataGridViewPrise.CurrentRow.Cells[4].Value.ToString();
                Gl.tbizm = 3;
                FormIzm izm = new FormIzm();
                izm.ShowDialog();
                UpdateStatus();
            }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (dataGridViewpost.SelectedCells.Count == 0) return;
            {
                Gl.ID2 = dataGridViewpost.CurrentRow.Cells[1].Value.ToString();
                Gl.ID = dataGridViewPrise.CurrentRow.Cells[0].Value.ToString();
                Gl.tbizm = 4;
                Formdel izm = new Formdel();
                izm.ShowDialog();
                UpdateStatus();
            }
        }

        private void button18_Click(object sender, EventArgs e)
        { 
            DialogResult resolt = MessageBox.Show("Удалить все?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                switch (dataGridView4.Rows[0].Cells[0].Value == null)
                {
                    case (false):
                        ConnectionClass ConCheck = new ConnectionClass();
                        ConCheck.Connection_Options();
                        SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                        connection.Open();
                        int count = 0;
                        for (int j = 0; j < dataGridView4.RowCount; j++)
                        {
                            for (int i = 0; i < dataGridView4.ColumnCount; i++)
                            {
                                if (dataGridView4[i, j].Value != null)
                                {
                                    count++;
                                    string tovrid = dataGridView4[0, j].Value.ToString();
                                    SqlCommand command = new SqlCommand("dbo.dell_Usloviya_zakaza", connection);
                                    command.CommandType = CommandType.StoredProcedure;
                                    command.Parameters.AddWithValue("@ID_Usloviya", tovrid);
                                    command.ExecuteNonQuery();
                                    break;
                                }
                            }
                        }
                        connection.Close();
                        zak();
                        break;
                }
            }
        }

        private void button3_Click_1(object sender, EventArgs e)
        {
            if (dataGridZakk.SelectedCells.Count == 0) return;
            {
                string id = dataGridZakk.CurrentRow.Cells[0].Value.ToString();
                ConnectionClass ConCheck = new ConnectionClass();
                ConCheck.Connection_Options();
                SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                connection.Open();
                SqlCommand command = new SqlCommand("dbo.upd_Zakaz_tovara", connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@Statysa_ID", comboBoxizm.SelectedValue.ToString());
                command.Parameters.AddWithValue("@ID_Zakaza", id);
                command.ExecuteNonQuery();
                connection.Close();
                UpdateStatus();

            }
        }

        private void comboBoxtol_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            if (comboBoxtol.Text == "Посмотреть все")
            {
                DataTableSQl Zay = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii 'Поставщик', Zakaz_tovara.Summa as "
                 + " 'Сумма', Postavhik.Pochta 'Почта', Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik"
                 + " INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa ");
                dataGridZakk.DataSource = Zay.Table.DefaultView;
            }
            else
            {
                DataTableSQl Zay = new DataTableSQl("SELECT Zakaz_tovara.ID_Zakaza as '№', Status_Zakaza.Naimenovanie as 'Статус', Postavhik.Nazvanie_organizacii 'Поставщик', Zakaz_tovara.Summa as "
               + " 'Сумма', Postavhik.Pochta 'Почта', Zakaz_tovara.Data_zakaza as 'Дата заказа' FROM dbo.Zakaz_tovara INNER JOIN dbo.Postavhik  ON dbo.Zakaz_tovara.Postavhik_ID = dbo.Postavhik.ID_Postavhik"
               + " INNER JOIN dbo.Status_Zakaza  ON dbo.Zakaz_tovara.Statysa_ID = dbo.Status_Zakaza.ID_Statysa where ID_Statysa = '" + comboBoxtol.SelectedValue.ToString() + "' ");
                dataGridZakk.DataSource = Zay.Table.DefaultView;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel5_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Zakupka_FormClosing(object sender, FormClosingEventArgs e)
        {
            Glav gll = new Glav();
            gll.Show();
        }
    }
}
