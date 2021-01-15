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
        private const string _enter = "Введите текст...";
        private const string _cipher = "зашифровать";
        private const string _decipher = "дешифровать";
        private const string _result = "Нажмите на кнопку \"запустить\", чтобы ";

        public Form1()
        {
            InitializeComponent();

            CorrectTextBox(textBox1, _enter);

            CorrectTextBox(textBox2, _result + (checkBox1.Checked ? _cipher : _decipher));
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            checkBox2.Checked = !checkBox1.Checked;

            CorrectTextBox(textBox2, _result + (checkBox1.Checked ? _cipher : _decipher));
        }
 
        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            checkBox1.Checked = !checkBox2.Checked;
            
            CorrectTextBox(textBox2, _result + (checkBox1.Checked ? _cipher : _decipher));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != _enter)
            { 
                if (checkBox1.Checked)
                {
                    string temp = textBox1.Text;

                    for (int i = 0; i < trackBar1.Value; i++)
                    {
                        temp = Cryptographer.Cipher(temp);
                    }

                    SetString(textBox2, temp);

                    if (textBox1.Text == "")
                        MessageBox.Show("Ошибка ввода!!!\nВедите текст!");
                }
                else if (checkBox2.Checked)
                {
                    try
                    {
                        string temp = textBox1.Text;

                        for (int i = 0; i < trackBar1.Value; i++)
                        {
                            temp = Cryptographer.Decipher(temp);
                        }
    
                        SetString(textBox2, temp);
                    }
                    catch (Exception)
                    {
                        textBox2.Text = "";
                    }

                    if (textBox2.Text == "")
                        MessageBox.Show("Ошибка ввода!!!\nЭто не шифр!");
                }
            }
            else
            {
                MessageBox.Show("Вы забыли ввести текст!!!");
            }
        }

        private void textBox1_Enter(object sender, EventArgs e) => SetTextBox(textBox1, _enter);

        private void textBox1_Leave(object sender, EventArgs e) => CorrectTextBox(textBox1, _enter);

        private void button2_Click(object sender, EventArgs e)
        {
            if (!(textBox2.Text == _result + (checkBox1.Checked ? _cipher : _decipher)))
            {
                textBox1.Text = textBox2.Text;

                textBox2.Text = "";

                checkBox1.Checked = !checkBox1.Checked;

                checkBox2.Checked = !checkBox1.Checked;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!(textBox2.Text == string.Empty || textBox2.Text == _result + (checkBox1.Checked ? _cipher : _decipher)))
            {
                Clipboard.SetText(textBox2.Text, TextDataFormat.UnicodeText);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";

            CorrectTextBox(textBox1, _enter);

            textBox2.Text = "";
            
            CorrectTextBox(textBox2, _result + (checkBox1.Checked ? _cipher : _decipher));
        }

        private void SetTextBox(TextBox textBox, string parametr)
        {
            if (textBox.Text == parametr)
            {
                textBox.Text = string.Empty;

                textBox.ForeColor = System.Drawing.Color.Black;
            }
        }

        private void SetString(TextBox textBox, string str)
        {
            textBox.Text = str;

            textBox.ForeColor = System.Drawing.Color.Black;
        }

        private void CorrectTextBox(TextBox textBox, string parametr)
        {
            if (textBox.Text == string.Empty)
            {
                textBox.Text = parametr;

                textBox.ForeColor = System.Drawing.Color.Gray;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Вашему вниманию предоставляется программа, реализующая алгоритм шифрования Урбановича - Кутукова\n\nРеализация AlexShev");
        }
    }
}
