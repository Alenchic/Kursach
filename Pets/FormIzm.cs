using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pets
{
    public partial class FormIzm : Form
    {
        public TextBox IDzak = new TextBox();
        public TextBox summa = new TextBox();
        public TextBox data = new TextBox();
        public Label post = new Label();
        public FormIzm()
        {
            InitializeComponent();
        }
        void texbox(TextBox tx, string text)
        {
            FormIzm iz = new FormIzm();
            tx.Top = 5;
            tx.Left = 5;
            tx.Width = 50;
            tx.Height = 20;
            tx.Text = text;
            tx.Font = new Font("Times New Roman", 10, FontStyle.Regular);
            panel1.Controls.Add(tx);
        }
        
        private void FormIzm_Load(object sender, EventArgs e)
        {
            DataTableSQl jiv = new DataTableSQl("select  Tovar_Jivotn.ID_Jivotnogo, Tovar_Jivotn.Vid_Jivotnogo from Tovar_Jivotn ");
            comboBoxjiv.ValueMember = "ID_Jivotnogo";
            comboBoxjiv.DisplayMember = "Vid_Jivotnogo";
            comboBoxjiv.DataSource = jiv.Table.DefaultView;
            DataTableSQl tip = new DataTableSQl("select  Vid_tovara.ID_Vid, Vid_tovara.Naimenovanie from Vid_tovara ");
            comboBoxkategr.ValueMember = "ID_Vid";
            comboBoxkategr.DisplayMember = "Naimenovanie";
            comboBoxkategr.DataSource = tip.Table.DefaultView;
            MessageBox.Show(Gl.tbizm.ToString());

            switch (Gl.tbizm)
            {
                case 1:
                    texbox(IDzak, Gl.ID);

                    break;
                case 2:
                    

                    break;
                case 3:

                    dataGridViewpost.Visible = false;
                    button13.Visible = false;
                    button14.Visible = false;
                    this.Height = 240;
                    textBoxfirm.Text = Gl.status;
                    textBoxname.Text = Gl.post;
                    textcena.Text = Gl.poch;
                    button1.Text = "Сохранить изменения";
                    break;
                default:

                    break;
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (textBoxfirm.Text == ""|| textBoxname.Text == ""|| textcena.Text == "") return;
                dataGridViewpost.Rows.Add(textBoxname.Text, textBoxfirm.Text, textcena.Text, comboBoxjiv.SelectedValue.ToString(), comboBoxkategr.SelectedValue.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                int index = dataGridViewpost.CurrentRow.Index;
                dataGridViewpost.Rows.RemoveAt(index);
            }
            catch
            {

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ConnectionClass ConCheck = new ConnectionClass();
            ConCheck.Connection_Options();
            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
            connection.Open();
            switch (Gl.tbizm)
            {
                case 1:
                    texbox(IDzak, Gl.ID);

                    break;
                case 2:

                    
                    int count = 0;
                    for (int j = 0; j < dataGridViewpost.RowCount; j++)
                    {
                        for (int i = 0; i < dataGridViewpost.ColumnCount; i++)
                        {
                            if (dataGridViewpost[i, j].Value != null)
                            {
                                count++;
                                string name = dataGridViewpost[0, j].Value.ToString();
                                string firm = dataGridViewpost[1, j].Value.ToString();
                                string cena = dataGridViewpost[2, j].Value.ToString();
                                string jiv = dataGridViewpost[3, j].Value.ToString();
                                string kateg = dataGridViewpost[4, j].Value.ToString();
                                SqlCommand command1 = new SqlCommand("dbo.add_Tovar", connection);
                                command1.CommandType = CommandType.StoredProcedure;
                                command1.Parameters.AddWithValue("@Cena", cena);
                                command1.Parameters.AddWithValue("@Naimenovanie", name);
                                command1.Parameters.AddWithValue("@Vid_ID", kateg);
                                command1.Parameters.AddWithValue("@Postavhik_ID", Gl.ID);
                                command1.Parameters.AddWithValue("@Jivotnoe_ID", jiv);
                                command1.Parameters.AddWithValue("@Firma", firm);
                                command1.ExecuteNonQuery();
                                this.Close();
                                break;
                            }
                        }
                    }
                    break;
                case 3:
                            
                            SqlCommand command = new SqlCommand("dbo.upd_Tovar", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@Cena", textcena.Text);
                            command.Parameters.AddWithValue("@Naimenovanie", textBoxname.Text);
                            command.Parameters.AddWithValue("@Vid_ID", comboBoxkategr.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@Jivotnoe_ID", comboBoxjiv.SelectedValue.ToString());
                            command.Parameters.AddWithValue("@Firma", textBoxfirm.Text);
                            command.Parameters.AddWithValue("@ID_Tovara", Gl.ID);
                            command.ExecuteNonQuery();
                            connection.Close();
                            this.Close();
                    break;
                default:

                    break;
            }
            connection.Close();


        }
    }
}
