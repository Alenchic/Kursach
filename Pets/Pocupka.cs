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
    public partial class Pocupka : Form
    {
        public Pocupka()
        {
            InitializeComponent();
        }

        private void Pocupka_Load(object sender, EventArgs e)
        {
            dataGridView3.DataSource = Gl.Table.DefaultView;
            panel1.Visible = false;
            this.Width = 604;
            button1.Text = "Без доставки";
            label10.Text = Gl.summ;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "С доставкой товара")
            {
                panel1.Visible = false;
                this.Width = 604;
                button1.Text = "Без доставки";
            }
            else
            {
                panel1.Visible = true;
                this.Width = 857;
                button1.Text = "С доставкой товара";
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult resolt = MessageBox.Show("Создать заказ?", "Сообщение", MessageBoxButtons.YesNo);
            if (resolt == DialogResult.No) return;
            {
                switch (dataGridView3.Rows[0].Cells[0].Value == null)

                {
                    //case (false):
                    //    ConnectionClass ConCheck = new ConnectionClass();
                    //    ConCheck.Connection_Options();
                    //    SqlConnection connection = new SqlConnection(ConCheck.ConnectString);
                    //    connection.Open();
                    //    SqlCommand command = new SqlCommand("dbo.add_Zakaz_tovara", connection);
                    //    command.CommandType = CommandType.StoredProcedure;
                    //    command.Parameters.AddWithValue("@Data_zakaza", seichas);
                    //    command.Parameters.AddWithValue("@Postavhik_ID", comboBoxpost.SelectedValue.ToString());
                    //    command.Parameters.AddWithValue("@Statysa_ID", 2);
                    //    command.Parameters.AddWithValue("@summa", obh);
                    //    int ii = 0;
                    //    command.Parameters.AddWithValue("@newif", ii);
                    //    int iii = (int)command.ExecuteScalar();
                    //    int count = 0;
                    //    for (int j = 0; j < dataGridView2.RowCount; j++)
                    //    {
                    //        for (int i = 0; i < dataGridView2.ColumnCount; i++)
                    //        {
                    //            if (dataGridView2[i, j].Value != null)
                    //            {
                    //                count++;
                    //                string tovrid = dataGridView2[0, j].Value.ToString();
                    //                string kolvo = dataGridView2[2, j].Value.ToString();
                    //                string sumt = dataGridView2[4, j].Value.ToString();
                    //                SqlCommand command1 = new SqlCommand("dbo.add_Usloviya_zakaza", connection);
                    //                command1.CommandType = CommandType.StoredProcedure;
                    //                command1.Parameters.AddWithValue("@Kolvo_tovara", kolvo);
                    //                command1.Parameters.AddWithValue("@Summa", sumt);
                    //                command1.Parameters.AddWithValue("@Tovara_ID", tovrid);
                    //                command1.Parameters.AddWithValue("@Zakaza_ID", iii);
                    //                command1.ExecuteNonQuery();
                    //                break;
                    //            }
                    //        }
                    //    }
                    //    connection.Close();
                    //    break;
                    //case (true):
                    //    MessageBox.Show("Заполните все пустые поля");
                    //    break;
                }
            }
        }
    }
}
