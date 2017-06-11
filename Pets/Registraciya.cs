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
using System.Data.SqlClient;

namespace Pets
{
    public partial class Registraciya : Form
    {

        public Button Button = new Button();
        public Registraciya()
        {
            InitializeComponent();
            this.Height = Height - 220;
        }

        void UP()
        {
            DataTableSQl tovari = new DataTableSQl("SELECT * from Prise");
            dataGridView3.DataSource = tovari.Table.DefaultView;
        }

        void combo(ComboBox name, DataTable table, string value, string menber)
        {
            name.ValueMember = value;
            name.DisplayMember = menber;
            name.DataSource = table.DefaultView;
        }
        void rr (DataTable table)
        {
            DataRow rr = table.NewRow();
            rr[1] = "Посмотреть все";
            table.Rows.Add(rr);
        }
        
        private void Registraciya_Load(object sender, EventArgs e)
        {

            UP();
            panel5.Visible = false;
            Button.Text = "Добавить существующий товар";
            Button.Width = 290;
            Button.Height = 27;
            Button.BackColor = Color.White;
            Button.Top = 130;
            Button.Left = 7;
            panel4.Controls.Add(Button);
            button2.Top = 130;
            button2.Left = Button.Width + 95;
            Button.Click +=Sus_Button_Click;
            DataTableSQl kuda = new DataTableSQl("select  Mesto_hraneniya.ID_Mesto_hraneniya, CONCAT(Nazvanie,' ',Adres) as 'sotr' from Mesto_hraneniya ");
            combo(comboBoxhr, kuda.Table, "ID_Mesto_hraneniya", "sotr");
            DataTableSQl vid = new DataTableSQl("select  * from Vid_tovara ");
            combo(comboBoxvid, vid.Table, "ID_Vid", "Naimenovanie");
            DataTableSQl jiv = new DataTableSQl("select  * from Tovar_Jivotn ");
            combo(comboBoxjiv, jiv.Table, "ID_Jivotnogo", "Vid_Jivotnogo");
            textBoxkol.Text = Gl.summ;
            textBoxfirm.Text = Gl.post; 
        }
        public void Sus_Button_Click(object sender, EventArgs e)
        {
            if (this.Height == 505)
            {
                this.Height = Height - 220;
                panel5.Visible = false;
            }
            else
            {
                this.Height = Height + 220;
                panel5.Visible = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ConnectionClass ConCheck = new ConnectionClass();
            ConCheck.Connection_Options();
            SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
            connection.Open();
            if (dataGridView3.SelectedCells.Count == 0 || panel5.Visible == false)
            {
                DialogResult resolt = MessageBox.Show("Добавить в существующий?", "Сообщение", MessageBoxButtons.YesNo);
                if (resolt == DialogResult.No) return;
                {
                    SqlCommand command1 = new SqlCommand("dbo.add_Tovar_v_magazine_i_na_sklade", connection);
                    command1.CommandType = CommandType.StoredProcedure;
                    command1.Parameters.AddWithValue("@Edinica", textBoxed.Text);
                    command1.Parameters.AddWithValue("@Naimenovanie", textBoxName.Text);
                    command1.Parameters.AddWithValue("@Vid_ID", comboBoxvid.SelectedValue.ToString());
                    command1.Parameters.AddWithValue("@Mesto_hraneniya_ID", comboBoxhr.SelectedValue.ToString());
                    command1.Parameters.AddWithValue("@Jivotnoe_ID", comboBoxjiv.SelectedValue.ToString());
                    command1.Parameters.AddWithValue("@Kol_vo_naskl", textBoxkol.Text);
                    command1.Parameters.AddWithValue("@Firma", textBoxfirm.Text);
                    command1.Parameters.AddWithValue("@Cena", textBoxcena.Text + label1.Text);
                    command1.ExecuteNonQuery();
                    UP();
                    this.Close();
                }
            }
            else
            {
                DialogResult resolt = MessageBox.Show("Добавить в существующий?", "Сообщение", MessageBoxButtons.YesNo);
                if (resolt == DialogResult.No) return;
                {
                    string edinica = dataGridView3.CurrentRow.Cells[0].Value.ToString();
                    SqlCommand command2 = new SqlCommand("UPDATE Tovar_v_magazine_i_na_sklade SET Kol_vo_naskl =Kol_vo_naskl +" + textBoxkol.Text + " WHERE ID_Tovar_v_magazine_i_na_sklade = " + edinica, connection);
                   command2.ExecuteNonQuery();
                   UP();
                   connection.Close();
                   this.Close();
                }
                }

            connection.Close();
        }

       
    }
}
