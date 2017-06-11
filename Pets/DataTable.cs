using System.Data;
using System.Data.SqlClient;
using Microsoft.Win32;


namespace Pets
{
    class DataTableSQl
    {
            private string sql;
            public SqlConnection connection = null;
            public DataTable Table = new DataTable();

            public DataTableSQl()
            {

            }

            public DataTableSQl(string sql)
            {

                this.sql = sql;


                ConnectionClass ConCheck = new ConnectionClass();
                RegistryKey DataBase_Connection = Registry.CurrentConfig;
                RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
                ConCheck.Connection_Options();
                connection = new SqlConnection(ConCheck.ConnectString);
                SqlCommand command = new SqlCommand(sql, connection);
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                connection.Open();
                adapter.Fill(Table);
                adapter.Update(Table);
                if (connection != null)
                    connection.Close();
            }

        }
    }


