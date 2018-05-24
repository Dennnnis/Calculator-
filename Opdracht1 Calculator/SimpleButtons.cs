using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading.Tasks;

namespace Opdracht1_Calculator
{
    partial class Form1
    {

        //Numbers
        private void button23_Click(object sender, EventArgs e)
        {
            Insert('0');
        }

        private void button19_Click(object sender, EventArgs e)
        {
            Insert('1');
        }

        private void button18_Click(object sender, EventArgs e)
        {
            Insert('2');
        }

        private void button17_Click(object sender, EventArgs e)
        {
            Insert('3');
        }

        private void button14_Click(object sender, EventArgs e)
        {
            Insert('4');
        }

        private void button13_Click(object sender, EventArgs e)
        {
            Insert('5');
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Insert('6');
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Insert('7');
        }

        private void button8_Click(object sender, EventArgs e)
        {
            Insert('8');
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Insert('9');
        }

        //Operators
        private void button16_Click(object sender, EventArgs e)
        {
            AddOperater('+');
        }

        private void button11_Click(object sender, EventArgs e)
        {
            AddOperater('-');
        }

        private void button6_Click(object sender, EventArgs e)
        {
            AddOperater('*');
        }

        private void button15_Click(object sender, EventArgs e)
        {
            AddOperater('/');
        }
        private void button20_Click(object sender, EventArgs e)
        {
            AddOperater('^');
        }

        //Button "="
        private void button21_Click(object sender, EventArgs e)
        {
            if (backValue.Length > 0)
                Calculate();
        }

        //Button "."
        private void button22_Click(object sender, EventArgs e)
        {
            if (frontValue.Length > 0)
            {
                if (!frontValue.Contains('.'))
                {
                    frontValue += '.';
                    UpdateForm();
                }
            }
            else
            {
                frontValue += "0.";
                UpdateForm();
            }
        }

        //Button "CE"
        private void button4_Click(object sender, EventArgs e)
        {
            frontValue = string.Empty;
            UpdateForm();
        }

        //Button "C"
        private void button3_Click(object sender, EventArgs e)
        {
            operatorPressed = false;
            operatorChar = ' ';
            frontValue = string.Empty;
            backValue = string.Empty;
            UpdateForm();
        }

        //Power 2 button
        private void button25_Click(object sender, EventArgs e)
        {
            FormatCheck(ref frontValue,calculatorFormat);
            frontValue = Convert.ToString(Math.Pow(Convert.ToDouble(frontValue), 2));
            UpdateForm();
        }

        //Move to right
        private void button2_Click(object sender, EventArgs e)
        {
            textBox2.Text = textBox1.Text;
        }



        //Currency
        private void dollarUSDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("USD");
            label3.Text = "Uinited States dollar (USD)";
            textBox2_TextChanged(sender, e);
        }

        private void euroEURToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("EUR");
            label3.Text = "Euro (EUR)";
            textBox2_TextChanged(sender, e);
        }

        private void japaneseYenJPYToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("JPY");
            label3.Text = "Japanese yen (JPY)";
            textBox2_TextChanged(sender, e);
        }

        private void poundSterlingGBPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("GBP");
            label3.Text = "Pound sterling (GBP)";
            textBox2_TextChanged(sender, e);
        }

        private void australianDollarAUDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("AUD");
            label3.Text = "Australian dollar (AUD)";
            textBox2_TextChanged(sender, e);
        }

        private void canadianDollarCADToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpdateCurrency("CAD");
            label3.Text = "Canadian dollar (CAD)";
            textBox2_TextChanged(sender, e);
        }

        //Clear notes
        private void button28_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
        }

        //View history
        private void historyToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //898, 518

            if (this.Width != 1016)
            {
                this.Width = 1016;
            }
            else
            {
                this.Width = 718;
            }
        }

        //Pressed <-
        private void button26_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            if (listBox1.SelectedIndex < 0)
            {
                label4.Visible = true;
                label4.Text = "Error: nothing selected";
                return;
            }


            string txt = listBox1.Items[listBox1.SelectedIndex].ToString();

            int spaceLocation = txt.IndexOf(" ", StringComparison.Ordinal);

            if (spaceLocation > 0)
            {
                frontValue = txt.Substring(0, spaceLocation);
                UpdateForm();
            }
        }


        //Pressed copy currency
        private void button10_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            if (listBox1.SelectedIndex < 0)
            {
                label4.Visible = true;
                label4.Text = "Error: nothing selected";
                return;
            }

            string me = listBox1.Items[listBox1.SelectedIndex].ToString();
            Clipboard.SetText(me);
        }

        //Pi button
        private void button24_Click(object sender, EventArgs e)
        {
            frontValue = "3.14159265359";
            UpdateForm();
        }

        //Clear history
        private void button30_Click(object sender, EventArgs e)
        {
            listBox2.Items.Clear();
        }

        //Procentage
        private void button1_Click(object sender, EventArgs e)
        {
            if (backValue.Length > 0 && operatorPressed)
            {
                FormatCheck(ref frontValue, calculatorFormat);
                FormatCheck(ref backValue, calculatorFormat);

                try
                {
                    frontValue = Convert.ToString((Convert.ToDouble(backValue) / 100) * Convert.ToDouble(frontValue));
                }
                catch (Exception)
                {
                    frontValue = "0";
                }
                UpdateForm();
            }
        }

        //Remove 1 
        private void button5_Click(object sender, EventArgs e)
        {
            if (frontValue.Length > 0)
            {
                frontValue = frontValue.Substring(0, frontValue.Length - 1);
            }
            UpdateForm();
        }


    }
}
