using System;
using System.Windows.Forms;

namespace Pets
{
    public partial class Admin : Form
    {
        public Admin()
        {
            InitializeComponent();
        }
        private void UpdateTovar()
        {
            DataTableSQl kateg = new DataTableSQl("SELECT * from Kategoriya_postavchika ");
            dataGridViewkategpost.DataSource = kateg.Table.DefaultView;
            DataTableSQl post = new DataTableSQl("SELECT * from Postavhik ");
            dataGridViewPost.DataSource = post.Table.DefaultView;
            DataTableSQl prise = new DataTableSQl("SELECT * from Tovar ");
            dataGridViewPrisePost.DataSource = prise.Table.DefaultView;
            DataTableSQl vidtv = new DataTableSQl("SELECT * from Vid_tovara ");
            dataGridViewVidTv.DataSource = vidtv.Table.DefaultView;
            DataTableSQl vidjiv = new DataTableSQl("SELECT * from Tovar_Jivotn ");
            dataGridViewVidJiv.DataSource = vidjiv.Table.DefaultView;
            DataTableSQl zak = new DataTableSQl("SELECT * from Zakaz_tovara ");
            dataGridViewZak.DataSource = zak.Table.DefaultView;
            DataTableSQl status = new DataTableSQl("SELECT * from Status_Zakaza ");
            dataGridViewStatus.DataSource = status.Table.DefaultView;
            DataTableSQl Usl = new DataTableSQl("SELECT * from Usloviya_zakaza ");
            dataGridViewUsl.DataSource = Usl.Table.DefaultView;
            DataTableSQl tovr = new DataTableSQl("SELECT * from Tovar_v_magazine_i_na_sklade ");
            dataGridViewTovr.DataSource = tovr.Table.DefaultView;
            DataTableSQl sotr = new DataTableSQl("SELECT * from Sotrudnik ");
            dataGridViewSotr.DataSource = sotr.Table.DefaultView;
            DataTableSQl mestohr = new DataTableSQl("SELECT * from Mesto_hraneniya ");
            dataGridViewMestohr.DataSource = mestohr.Table.DefaultView;
            DataTableSQl Spisanie = new DataTableSQl("SELECT * from Spisanie_tovara ");
            dataGridViewSpisanie.DataSource = Spisanie.Table.DefaultView;
            DataTableSQl Vibr = new DataTableSQl("SELECT * from Vibor ");
            dataGridViewVibr.DataSource = Vibr.Table.DefaultView;
            DataTableSQl Zay = new DataTableSQl("SELECT * from Zayzvka ");
            dataGridViewZaya.DataSource = Zay.Table.DefaultView;
            DataTableSQl Dost = new DataTableSQl("SELECT * from Dostavka ");
            dataGridViewDost.DataSource = Dost.Table.DefaultView;
        }

        private void tabControl1_Selecting(object sender, TabControlCancelEventArgs e)
        {
            string f = tabControl1.SelectedTab.Text.ToString();
            switch (f)
            {
                case "Товары на складе":
                    dataGridViewVidJiv.Visible = false;
                    break;
                case "Регистрация накладной":
                    dataGridViewVidJiv.Visible = true;
                    break;
            }
        }
       
        private void Admin_Load(object sender, EventArgs e)
        {
            UpdateTovar();
        }
    }
}
