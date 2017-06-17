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
    public partial class Formdel : Form
    {
        public Formdel()
        {
            InitializeComponent();
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
                    
                    SqlCommand command = new SqlCommand("dbo.dell_Usloviya_zakaza", connection);
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@ID_Usloviya", Gl.ID);
                            command.ExecuteNonQuery();
                            this.Close();
                    break;
                case 2:
                    try
                    {
                        SqlCommand command2 = new SqlCommand("dbo.dell_Zakaz_tovara", connection);
                        command2.CommandType = CommandType.StoredProcedure;
                        command2.Parameters.AddWithValue("@ID_Zakaza", Gl.ID);
                        command2.ExecuteNonQuery();
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("Заказ не может быть удален. Удалите сначала все товары");
                        this.Close();
                    }

                    break;
                case 3:
                    try
                    {
                        SqlCommand command3 = new SqlCommand("dbo.dell_Postavhik", connection);
                        command3.CommandType = CommandType.StoredProcedure;
                        command3.Parameters.AddWithValue("@ID_Postavhik", Gl.ID);
                        command3.ExecuteNonQuery();
                        this.Close();
                    }
                    catch
                    {
                        MessageBox.Show("У этого поставщика есть товары, удалите их");
                    }
                    break;

                case 4:

                    SqlCommand command4 = new SqlCommand("dbo.dell_Tovar", connection);
                    command4.CommandType = CommandType.StoredProcedure;
                    command4.Parameters.AddWithValue("@ID_Tovara", Gl.ID);
                    command4.ExecuteNonQuery();
                    this.Close();

                    break;
                default:
                    connection.Close();
                    break;
            }
            connection.Close();
        }

        private void Formdel_Load(object sender, EventArgs e)
        {
            switch (Gl.tbizm)
            {
                case 1:
                    label1.Text = "Удалить товар из заявки №" + Gl.ID2 + " ?";

                    break;
                case 2:
                    label1.Text = "Удалить заказ №" + Gl.ID + " ?";
                    break;
                case 3:
                    label1.Text = "Удалить поставщика " + Gl.ID2 + " ?";

                    break;
                case 4:
                    label1.Text = "Удалить товар из прайс листа поставщика " + Gl.ID2 + " ?";

                    break;
                default:

                    break;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Hide();
        }
    }
}
