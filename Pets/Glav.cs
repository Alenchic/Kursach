using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Windows;
using System.IO;
using System.Data.SqlClient;
using Microsoft.Win32;

namespace Pets
{
    public partial class Glav : Form
    {

        public ConReg Registration_Window = new ConReg();
        public TextBox Login_Text = new TextBox();
        public Label Login_Label = new Label();
        public TextBox Pass_Text = new TextBox();
        public Label Pass_Label = new Label();
        public Button Button_Enter = new Button();
        public Button Button_Register = new Button();

        public ComboBox Server_CB = new ComboBox();
        public ComboBox DataBase_CB = new ComboBox();
        public Label Base_Login_Label = new Label();
        public TextBox Base_Login_Text = new TextBox();
        public Label Base_Password_Label = new Label();
        public Label Spisok_Label = new Label();
        public Label Baza_Label = new Label();
        public TextBox Base_Password_Text = new TextBox();
        public Button Check_Connection_Button = new Button();
        public Button Connect_Data_Source_Button = new Button();

        public Label New_Login_Label = new Label();
        public Label New_Password_Label = new Label();
        public Label Conform_Password_Label = new Label();
        public Label Count_Login_Char_Label = new Label();
        public Label Count_Password_Char_Label = new Label();
        public TextBox New_Login_Text = new TextBox();
        public TextBox New_Password_Text = new TextBox();
        public TextBox Conform_Password_Text = new TextBox();
        public Button Registrate_Button = new Button();
        public Button Cancel_Button = new Button();
        public Button Vhod_Button = new Button();
        public Panel regis = new Panel();
        public Panel regis2 = new Panel();
        public Panel conpan = new Panel();
        public Panel con2pan = new Panel();
        public int Log_Count = 16;
        public int Pass_Count = 16;

        public Glav()
        {
            InitializeComponent();
            SetRoundedShape(panel1, 70);
           
            
        }


        private void Base_Password_Text_Enter(object sender, EventArgs e)
        {
            switch (Base_Password_Text.Text)
            {
                case ("Введите пароль от источника данных"):
                    Base_Password_Text.Clear();
                    Base_Password_Text.PasswordChar = '*';
                    break;
                case (""):
                    Base_Password_Text.Text = "Введите пароль от источника данных";
                    Base_Password_Text.PasswordChar = '\0';
                    break;
            }
        }



        private void Base_Login_Text_Enter(object sender, EventArgs e)
        {
            switch (Base_Login_Text.Text)
            {
                case ("Введите логин от источника данных"):
                    Base_Login_Text.Clear();
                    Base_Login_Text.PasswordChar = '*';
                    break;
                case (""):
                    Base_Login_Text.Text = "Введите логин от источника данных";
                    Base_Login_Text.PasswordChar = '\0';
                    break;
            }
        }


        private void button3_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.Gray;
            panel2.BackColor = Color.Gray;
            switch (MessageBox.Show("Вы точно хотите закрыть приложение?", "Выход", MessageBoxButtons.YesNo))
            {
                case DialogResult.Yes:
                    Application.Exit();
                    break;
                case DialogResult.No:
                    this.BackColor = Color.Indigo;
                    panel2.BackColor = Color.Indigo;
                    break;
            }

        }



        static void SetRoundedShape(Control control, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLine(radius, 0, control.Width - radius, 0);
            path.AddArc(control.Width - radius, 0, radius, radius, 270, 90);
            path.AddLine(control.Width, radius, control.Width, control.Height - radius);
            path.AddArc(control.Width - radius, control.Height - radius, radius, radius, 0, 90);
            path.AddLine(control.Width - radius, control.Height, radius, control.Height);
            path.AddArc(0, control.Height - radius, radius, radius, 90, 90);
            path.AddLine(0, control.Height - radius, 0, radius);
            path.AddArc(0, 0, radius, radius, 180, 90);
            control.Region = new Region(path);
        }
     


        public void Button_Register_Click(object sender, EventArgs e)
        {
            //    ConReg Registration_Window = new ConReg();

            Registration_Window.MouseMove += Registration_Window_MouseMove;
            Registration_Window.Show();
            this.BackColor = Color.Gray;
            button1.BackColor = Color.Gray;
            button1.ForeColor = Color.DimGray;
            button2.BackColor = Color.Gray;
            button2.ForeColor = Color.DimGray;
            button3.BackColor = Color.Gray;
            button3.ForeColor = Color.DimGray;
            //this.Enabled = false;
            //-----------------------------------------------------
            //Создание надписи "Панели"
            regis2.Width = 487; 
            regis2.Height = 298;
            SetRoundedShape(regis2, 70);
            regis2.Top = 14;
            regis2.Left = 14;
            regis2.BackColor = Color.White;
            Registration_Window.Controls.Add(regis2);
            regis.Width = regis2.Width - 55;
            regis.Height = regis2.Height - 55;
            //SetRoundedShape(regis, 70);
            regis.Top = 27;
            regis.Left = 27;
            regis.BackColor = Color.Indigo;
            regis2.Controls.Add(regis);
            //Создание надписи "label"
            New_Login_Label.Top = 5;
            New_Login_Label.Left = 100;
            New_Login_Label.Text = "Авторизация";
            New_Login_Label.TextAlign = ContentAlignment.BottomCenter;
            New_Login_Label.Height = 30;
            New_Login_Label.Width = regis.Width - 200;
            New_Login_Label.ForeColor = Color.White;
            New_Login_Label.Font = new Font("Times New Roman", 20, FontStyle.Bold); 
            regis.Controls.Add(New_Login_Label);
            //Создания надписи по количеству оставшихся символов для логина
            Count_Login_Char_Label.Top = New_Login_Label.Height + 8;
            Count_Login_Char_Label.Left = 100;
            Count_Login_Char_Label.Text = "Введите логин";
            Count_Login_Char_Label.TextAlign = ContentAlignment.BottomCenter;
            Count_Login_Char_Label.Height = 20;
            Count_Login_Char_Label.ForeColor = Color.White;
            Count_Login_Char_Label.Width = regis.Width - 200;
            Count_Login_Char_Label.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            regis.Controls.Add(Count_Login_Char_Label);
            //Создание поля ввода нового логина
            New_Login_Text.Top = Count_Login_Char_Label.Top + 20;
            New_Login_Text.Left = 100;
            New_Login_Text.Width = regis.Width - 200;
            New_Login_Text.Height = 60;
            New_Login_Text.Text = "Логин пользователя";
            New_Login_Text.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            //New_Login_Text.TextChanged += New_Login_Text_TextChanged;
            regis.Controls.Add(New_Login_Text);
           
            //-----------------------------------------------------
            //Создание надписи "Введите новый пароль"
            New_Password_Label.Top = New_Login_Text.Top + New_Login_Text.Height+5;
            New_Password_Label.Height = 20;
            New_Password_Label.Left = 100;
            New_Password_Label.Width = regis.Width - 200;
            New_Password_Label.ForeColor = Color.White;
            New_Password_Label.TextAlign = ContentAlignment.BottomCenter;
            New_Password_Label.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            New_Password_Label.Text = "Введите пароль";
            regis.Controls.Add(New_Password_Label);
            //-----------------------------------------------------
            //Создание поля ввода нового пароля
            New_Password_Text.Top = New_Password_Label.Top + New_Password_Label.Height + 5;
            New_Password_Text.Left = 100;
            New_Password_Text.Width = regis.Width - 200;
            New_Password_Text.Height = 40;
            New_Password_Text.PasswordChar = '*';
            New_Password_Text.Text = "0000000";
            New_Password_Text.Font = new Font("Times New Roman", 14, FontStyle.Regular);
            //New_Password_Text.TextChanged += New_Password_Text_TextChanged;
            //New_Password_Text.Enter += New_Password_Text_Enter;
            regis.Controls.Add(New_Password_Text);

            //Создание кнопки "Зарегестрироваться"
            Vhod_Button.Top = New_Password_Text.Top + New_Password_Text.Height  + 10;
            Vhod_Button.Left = 150;
            Vhod_Button.Width = regis.Width - 300;
            Vhod_Button.Height = 40;
            Vhod_Button.Text = "Войти";
            Vhod_Button.BackColor = Color.White;
            Vhod_Button.ForeColor = Color.Indigo;
            Vhod_Button.FlatStyle = FlatStyle.Popup;
            Vhod_Button.FlatAppearance.BorderSize = 10;
            Vhod_Button.Font = new Font("Times New Roman", 14, FontStyle.Bold);
            //Registrate_Button.MouseEnter += this.Registrate_Button_MouseEnter;
            //    Registrate_Button.MouseLeave += this.Registrate_Button_MouseLeave;
            //    Registrate_Button.MouseDown += this.Registrate_Button_MouseDown;
            //    Registrate_Button.MouseUp += this.Registrate_Button_MouseUp;
            //    Registrate_Button.Enabled = false;
            Vhod_Button.Click += Button_Click;
            regis.Controls.Add(Vhod_Button);
            // Создание кнопки "Закрыть"
            Cancel_Button.Top = Vhod_Button.Top + Vhod_Button.Height + 15;
            Cancel_Button.Left = 5;
            Cancel_Button.Width = 208;
            Cancel_Button.Height = 30;
            Cancel_Button.Text = "Выйти из авторизации";
            Cancel_Button.Click += Cancel_Button_Click;
            Cancel_Button.BackColor = Color.White;
            Cancel_Button.ForeColor = Color.Indigo;
            Cancel_Button.FlatStyle = FlatStyle.Popup;
            Cancel_Button.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            //Cancel_Button.MouseEnter += this.Cancel_Button_MouseEnter;
            //    Cancel_Button.MouseLeave += this.Cancel_Button_MouseLeave;
            //    Cancel_Button.MouseDown += this.Cancel_Button_MouseDown;
            //    Cancel_Button.MouseUp += this.Cancel_Button_MouseUp;
            regis.Controls.Add(Cancel_Button);


            Registrate_Button.Top = Vhod_Button.Top + Vhod_Button.Height + 15;
            Registrate_Button.Left = Cancel_Button.Left + Cancel_Button.Width + 5;
            Registrate_Button.Width = 208;
            Registrate_Button.Height = 30;
            Registrate_Button.Text = "Зарегестрироваться";
            Registrate_Button.BackColor = Color.White;
            Registrate_Button.ForeColor = Color.Indigo;
            Registrate_Button.FlatStyle = FlatStyle.Popup;
            Registrate_Button.FlatAppearance.BorderSize = 10;
            Registrate_Button.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            //Registrate_Button.MouseEnter += this.Registrate_Button_MouseEnter;
            //    Registrate_Button.MouseLeave += this.Registrate_Button_MouseLeave;
            //    Registrate_Button.MouseDown += this.Registrate_Button_MouseDown;
            //    Registrate_Button.MouseUp += this.Registrate_Button_MouseUp;
            //    Registrate_Button.Enabled = false;
            Registrate_Button.Click += Registrate_Button_Click;
            regis.Controls.Add(Registrate_Button);
            //-----------------------------------------------------
           
            }
        public void Button_Click(object sender, EventArgs e)
        {
            ConnectionClass ConCheck = new ConnectionClass();
            ConCheck.Connection_Options();
            SqlConnection connectionUser = new SqlConnection(ConCheck.ConnectString);
            SqlCommand Select_USID = new SqlCommand("select concat(Imya,' ',Fam,' ',Oth)  from Sotrudnik where Parol='"+New_Password_Text.Text+ "' and [Login]='"+New_Login_Text.Text+ "'", connectionUser);
            SqlCommand Select_ISA = new SqlCommand("select Otdel  from Sotrudnik where Parol='" + New_Password_Text.Text + "' and [Login]='" + New_Login_Text.Text + "'", connectionUser);

            try
            {
                connectionUser.Open();
               string USID = Select_USID.ExecuteScalar().ToString();
               string ISA = Select_ISA.ExecuteScalar().ToString();


                MessageBox.Show("Здравствуйте: " + USID);
                   switch (ISA)
                {
                    case "Отдел закупок":
                        Zakupka zakupka = new Zakupka();
                        Gl.sotr = USID;
                        zakupka.Show();
                        this.Hide();
                        Registration_Window.Close();
                        connectionUser.Close();
                        break;
                    case "Складской отдел":
                        Sklad Sklad = new Sklad();
                        Gl.sotr = USID;
                        Sklad.Show();
                        this.Hide();
                        Registration_Window.Close();
                        connectionUser.Close();
                        break;
                    case "Отдел продаж":
                        Klient Klient = new Klient();
                        Gl.sotr = USID;
                        Klient.Show();
                        this.Hide();
                        Registration_Window.Close();
                        connectionUser.Close();
                      
                        break;
                }
                            

                

            }
            catch
            {
                MessageBox.Show("Введите коректный логин и пароль");
            }

        }

        public void Registrate_Button_Click(object sender, EventArgs e)
        {
            Registraciya regis1 = new Registraciya();
            regis1.Show();
            Registration_Window.Enabled = false;
            Registration_Window.BackColor = Color.Gray;
            regis.BackColor = Color.Gray;
            Registrate_Button.BackColor = Color.Gray;
            Registrate_Button.ForeColor = Color.DimGray;
            Vhod_Button.BackColor = Color.Gray;
            Vhod_Button.ForeColor = Color.DimGray;
            Cancel_Button.BackColor = Color.Gray;
            Cancel_Button.ForeColor = Color.DimGray;

        }

        public void Cancel_Button_Click(object sender, EventArgs e)
        {
           
            this.Enabled = true;
            this.BackColor = Color.Indigo;
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Indigo;
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Indigo;
            button3.BackColor = Color.White;
            button3.ForeColor = Color.Indigo;
            Registration_Window.Controls.Remove(New_Login_Label);
            Registration_Window.Controls.Remove(New_Login_Text);
            Registration_Window.Controls.Remove(New_Password_Label);
            Registration_Window.Controls.Remove(New_Password_Text);
            Registration_Window.Controls.Remove(Conform_Password_Label);
            Registration_Window.Controls.Remove(Conform_Password_Text);
            Registration_Window.Controls.Remove(Cancel_Button);
            Registration_Window.Controls.Remove(Vhod_Button);
            Registration_Window.Controls.Remove(Registrate_Button);
            Registration_Window.Controls.Remove(Count_Login_Char_Label);
            Registration_Window.Controls.Remove(Count_Password_Char_Label);
            Registration_Window.Controls.Remove(regis);
            Registration_Window.Controls.Remove(regis2);

            Registration_Window.Controls.Remove(Server_CB);
            Registration_Window.Controls.Remove(DataBase_CB);
            Registration_Window.Controls.Remove(Base_Login_Label);
            Registration_Window.Controls.Remove(Base_Password_Label);
            Registration_Window.Controls.Remove(Spisok_Label);
            Registration_Window.Controls.Remove(Baza_Label);
            Registration_Window.Controls.Remove(Cancel_Button);
            Registration_Window.Controls.Remove(Connect_Data_Source_Button);
            Registration_Window.Controls.Remove(con2pan);
            Registration_Window.Controls.Remove(conpan);
            Registration_Window.Hide();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
            ConnectionClass ConCheck = new ConnectionClass();

            RegistryKey DataBase_Connection = Registry.CurrentConfig;
            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
            ConCheck.Connection_Options();
            SqlConnection connectionCheck = new SqlConnection(ConCheck.ConnectString);

            try
            {
                connectionCheck.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            switch (connectionCheck.State == ConnectionState.Open)
            {
                case (true):
                    label1.Text = " - Подключение к источнику данных есть.";
                    //button2.Enabled = false;
                    //button1.Enabled = true;
                    //button3.Enabled = true;
                    break;
                case (false):
                    label1.Text = " - Отсутствует подключение к источнику данных!";
                    button2.Enabled = true;
                    button1.Enabled = false;
                    button3.Enabled = false;

                    break;
            }
        }
        ConReg Con_Window = new ConReg();

        private void button2_Click(object sender, EventArgs e)
        {
            

            Server_CB.Top = 30;
            Server_CB.Left = 50;
            Server_CB.Width = 330;
            Server_CB.Height = 40;
            Server_CB.Items.Clear();
            Server_CB.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            Server_CB.Text = "Выберите доступные сервера";
            System.Data.Sql.SqlDataSourceEnumerator Server_List =
                System.Data.Sql.SqlDataSourceEnumerator.Instance;
            DataTable Server_Table = Server_List.GetDataSources();
            foreach (DataRow row in Server_Table.Rows)
            {
                Server_CB.Items.Add(row[0] + "\\" + row[1]);
            }
            con2pan.Controls.Add(Server_CB);

            Registration_Window.Show();
            this.BackColor = Color.Gray;
            button1.BackColor = Color.Gray;
            button1.ForeColor = Color.DimGray;
            button2.BackColor = Color.Gray;
            button2.ForeColor = Color.DimGray;
            button3.BackColor = Color.Gray;
            button3.ForeColor = Color.DimGray;
            this.Enabled = false;

            conpan.Width = 487;
            conpan.Height = 298;
            SetRoundedShape(conpan, 70);
            conpan.Top = 14;
            conpan.Left = 14;
            conpan.BackColor = Color.White;
            Registration_Window.Controls.Add(conpan);
            con2pan.Width = conpan.Width - 55;
            con2pan.Height = conpan.Height - 55;
            //SetRoundedShape(regis, 70);
            con2pan.Top = 27;
            con2pan.Left = 27;
            con2pan.BackColor = Color.Indigo;
            conpan.Controls.Add(con2pan);
            //-----------------------------------------------------
            //Создвание надписи "Логин от источника данных"

            Spisok_Label.Top = Server_CB.Top - 30;
            Spisok_Label.Left = Server_CB.Left;
            Spisok_Label.Height = 20;
            Spisok_Label.Width = Server_CB.Width;
            Spisok_Label.Text = "Список доступных серверов";
            Spisok_Label.TextAlign = ContentAlignment.BottomCenter;
            Spisok_Label.ForeColor = Color.White;
            Spisok_Label.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            con2pan.Controls.Add(Spisok_Label);

            Base_Login_Label.Top = Server_CB.Top + Server_CB.Height;
            Base_Login_Label.Left = Server_CB.Left;
            Base_Login_Label.Height = 20;
            Base_Login_Label.Width = Server_CB.Width;
            Base_Login_Label.Text = "Логин от источника данных";
            Base_Login_Label.TextAlign = ContentAlignment.BottomCenter;
            Base_Login_Label.ForeColor = Color.White;
            Base_Login_Label.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            con2pan.Controls.Add(Base_Login_Label);

            //-----------------------------------------------------
            //Создание поля ввода логина от базы данных
            Base_Login_Text.Top = Base_Login_Label.Top + Base_Login_Label.Height + 2;
            Base_Login_Text.Left = Server_CB.Left;
            Base_Login_Text.Width = Server_CB.Width;
            Base_Login_Text.Clear();
            Base_Login_Text.Text = "Введите логин от источника данных";
            Base_Login_Text.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            Base_Login_Text.BackColor = Color.White;
            Base_Login_Text.Leave += Base_Login_Text_Leave;
            Base_Login_Text.Enter += Base_Login_Text_Enter;
            con2pan.Controls.Add(Base_Login_Text);
            //-----------------------------------------------------
            //Создвание надписи "Пароль от источника данных"
            Base_Password_Label.Top = Base_Login_Text.Top + Base_Login_Text.Height;
            Base_Password_Label.Left = Server_CB.Left;
            Base_Password_Label.Height = 20;
            Base_Password_Label.Width = Server_CB.Width;
            Base_Password_Label.ForeColor = Color.White;
            Base_Password_Label.Text = "Пароль от источника данных";
            Base_Password_Label.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            Base_Password_Label.TextAlign = ContentAlignment.BottomCenter;
            con2pan.Controls.Add(Base_Password_Label);
            //-----------------------------------------------------
            //Создание поля ввода пароль от базы данных
            
            Base_Password_Text.Top = Base_Password_Label.Top + Base_Password_Label.Height + 5;
            Base_Password_Text.Left = Server_CB.Left;
            Base_Password_Text.Width = Server_CB.Width;
            Base_Password_Text.Clear();
            Base_Password_Text.Text = "Введите пароль от источника данных";
            Base_Password_Text.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            Base_Password_Text.BackColor = Color.White;
            Base_Password_Text.Leave += Base_Password_Text_Leave;
            Base_Password_Text.Enter += Base_Password_Text_Enter;
            con2pan.Controls.Add(Base_Password_Text);

            Baza_Label.Top = Base_Password_Text.Top + Base_Password_Text.Height;
            Baza_Label.Left = Server_CB.Left;
            Baza_Label.Height = 20;
            Baza_Label.Width = Server_CB.Width;
            Baza_Label.ForeColor = Color.White;
            Baza_Label.Text = "Список доступных баз данных";
            Baza_Label.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            Baza_Label.TextAlign = ContentAlignment.BottomCenter;
            con2pan.Controls.Add(Baza_Label);

            //-----------------------------------------------------
            //Создание выпадающего списка всех баз данных
            DataBase_CB.Top = Baza_Label.Top + Baza_Label.Height + 5;
            DataBase_CB.Left = Server_CB.Left;
            DataBase_CB.Width = Server_CB.Width;
            DataBase_CB.DataSource = null;
            DataBase_CB.DisplayMember = "";
            DataBase_CB.Items.Clear();
            DataBase_CB.Text = "Выберите нужную Базу данных";
            DataBase_CB.Font = new Font("Times New Roman", 10, FontStyle.Bold);
            DataBase_CB.Click += DataBase_CB_Click;
            con2pan.Controls.Add(DataBase_CB);
           
            //-----------------------------------------------------
            // Создание кнопки "Закрыть"
            Cancel_Button.Top = DataBase_CB.Top + DataBase_CB.Height + 15;
            Cancel_Button.Left = 5;
            Cancel_Button.Width = 208;
            Cancel_Button.Height = 30;
            Cancel_Button.Text = "Выйти из подключения";
            Cancel_Button.Click += Cancel_Button_Click;
            Cancel_Button.BackColor = Color.White;
            Cancel_Button.ForeColor = Color.Indigo;
            Cancel_Button.FlatStyle = FlatStyle.Popup;
            Cancel_Button.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            con2pan.Controls.Add(Cancel_Button);
            // Создание кнопки "Подключиться"

            Connect_Data_Source_Button.Top = DataBase_CB.Top + DataBase_CB.Height + 15;
            Connect_Data_Source_Button.Left = Cancel_Button.Left + Cancel_Button.Width +5;
            Connect_Data_Source_Button.Width = 208;
            Connect_Data_Source_Button.Height = 30;
            Connect_Data_Source_Button.Text = "Подключиться";
            //Connect_Data_Source_Button.Enabled = false;
            Connect_Data_Source_Button.BackColor = Color.White;
            Connect_Data_Source_Button.ForeColor = Color.Indigo;
            Connect_Data_Source_Button.FlatStyle = FlatStyle.Popup;
            Connect_Data_Source_Button.FlatAppearance.BorderSize = 10;
            Connect_Data_Source_Button.Font = new Font("Times New Roman", 12, FontStyle.Bold);
            //Connect_Data_Source_Button.Click += this.Connect_Data_Source_Button_Click;
            //Connect_Data_Source_Button.MouseEnter += this.Connect_Data_Source_Button_MouseEnter;
            //Connect_Data_Source_Button.MouseLeave += this.Connect_Data_Source_Button_MouseLeave;
            //Connect_Data_Source_Button.MouseDown += this.Connect_Data_Source_Button_MouseDown;
            //Connect_Data_Source_Button.MouseUp += this.Connect_Data_Source_Button_MouseUp;
            con2pan.Controls.Add(Connect_Data_Source_Button);
            //this.Height = this.Height + 200;

        }
        private void Base_Password_Text_Leave(object sender, EventArgs e)
        {
            if (Base_Password_Text.TextLength == 0 || Base_Password_Text.Text == "Введите пароль от источника данных")
            {
                Base_Password_Text.BackColor = Color.Red;
                Check_Connection_Button.Enabled = false;
            }
            else
            {
                Base_Password_Text.BackColor = Color.White;
                Check_Connection_Button.Enabled = true;
            }
        }

        public void DataBase_CB_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Try_Connect = new SqlConnection();
                Try_Connect.ConnectionString = "Data Source=" + Server_CB.Text
                    + "; Initial Catalog= master; Persist Security Info=True;User ID="
                    + Base_Login_Text.Text + ";Password=\"" + Base_Password_Text.Text + "\"";
                Try_Connect.Open();
                SqlDataAdapter Base_Adapter = new SqlDataAdapter("exec sp_helpdb", Try_Connect);
                DataSet Base_Data_Set = new DataSet();
                Base_Adapter.Fill(Base_Data_Set, "db");
                DataBase_CB.DataSource = Base_Data_Set.Tables[0];
                DataBase_CB.DisplayMember = "name";
                Check_Connection_Button.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Base_Login_Text_Leave(object sender, EventArgs e)
        {
            if (Base_Login_Text.TextLength == 0
                || Base_Login_Text.Text == "Введите логин от источника данных")
            {
                Base_Login_Text.BackColor = Color.Red;
                Check_Connection_Button.Enabled = false;
            }
            else
            {
                Base_Login_Text.BackColor = Color.White;
                Check_Connection_Button.Enabled = true;
            }
        }
      
        private void button4_Click(object sender, EventArgs e)
        {
            Zakupka ss = new Zakupka();
            ss.Show();


        }

        private void Registration_Window_MouseMove(object sender, MouseEventArgs e)
        {
            //Registraciya registr = new Registraciya();
            //if (registr.)
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Sklad ss = new Sklad();
            ss.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            Klient kl = new Klient();
            kl.Show();

        }
    }
}
