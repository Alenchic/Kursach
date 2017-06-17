using System;
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
                   
                }
            }
        }
    }
}
