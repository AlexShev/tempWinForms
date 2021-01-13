using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace tempWinForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                checkBox2.Checked = false;
            }

        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox2.Checked == true)
            {
                checkBox1.Checked = false;
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                textBox2.Text = Cryptographer.Cipher(textBox1.Text );

                if (textBox1.Text == "")
                    MessageBox.Show("Ошибка ввода!!!\nВедите текст!");
            }
            else if(checkBox2.Checked) 
            {
                try
                {
                    textBox2.Text = Cryptographer.Decipher(textBox1.Text);
                }
                catch (Exception)
                {
                    textBox2.Text = "";
                }

                if (textBox2.Text == "") 
                    MessageBox.Show("Ошибка ввода!!!\nЭто не шифр!");
            }
            else
            {
                MessageBox.Show("Ошибка!!!\nВыберете действие!");
            }
        } 
    }
}
