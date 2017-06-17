
using Microsoft.Win32;

namespace Pets
{
    class ConnectionClass
    {
        public string ConnectString;
        string DS_NAME, INIT_CATALOG, LOD_ID, PAS_ID;
        public void Connection_Options()
        {
            RegistryKey DataBase_Connection = Registry.CurrentConfig;
            RegistryKey Connection_Base_Party_Options = DataBase_Connection.CreateSubKey("DB_PARTY_OPTIOS");
            DS_NAME = Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("DS").ToString());
            INIT_CATALOG = Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("IC").ToString());
            LOD_ID = Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("UID").ToString());
            PAS_ID = Encrypt.Decrypt(Connection_Base_Party_Options.GetValue("PDB").ToString());
            ConnectString = "Data Source="
                + DS_NAME + ";" + "Initial Catalog="
                + INIT_CATALOG + ";" + "Persist Security Info=True;User ID="
                + LOD_ID + ";Password=\"" + PAS_ID + "\"";
        }
    }
}
