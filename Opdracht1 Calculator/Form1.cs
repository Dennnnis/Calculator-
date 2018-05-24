using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace Opdracht1_Calculator
{
    public partial class Form1 : Form
    {
        

        string backValue = string.Empty;
        string frontValue = string.Empty;
        char operatorChar = ' ';
        bool operatorPressed = false;
        string memmory = "";

        string calculatorFormat= "1234567890.E-";

        //Returns a empty string if everything went OK
        public string FormatCheck(ref string value, string legalFormat)
        {

            //Check length
            if (value.Length < 1)
            {       
                value = "0";
                return "No input.";
            }


            //Check legal format
            for (int i = 0; i < value.Length; i++)
            {
                bool ok = false;
                foreach (char c in legalFormat)
                {
                    if (c == value[i])
                    {
                        ok = true;
                    }
                }

                if (!ok)
                {
                    value = "0";
                    return "Wrong format..";
                }
            }


            //Check dots
            if (value[value.Length - 1] == '.')
            {
                value = value.Substring(0, value.Length - 1);
            }


            //Count dots
            int counter = 0;
            foreach (char c in value)
            {
                if (c == '.')
                {
                    counter++;
                }
            }
            if (counter > 1)
            {
                value = "0";
                return "Too many dots......";
            }

            return String.Empty;
        }

        public void UpdateForm()
        {
            label2.Text = backValue + " " + operatorChar;
            textBox1.Text = frontValue;
        }

        public string DoMath(double valueA, double valueB)
        {
            switch (operatorChar)
            {

                case '+':
                    listBox2.Items.Add(backValue + "+" + frontValue + "=" + Convert.ToString(valueA + valueB));
                    return Convert.ToString(valueA + valueB);    

                case '-':
                    listBox2.Items.Add(backValue + "-" + frontValue + "=" + Convert.ToString(valueA - valueB));
                    return Convert.ToString(valueA - valueB);

                case '*':
                    listBox2.Items.Add(backValue + "*" + frontValue + "=" + Convert.ToString(valueA * valueB));
                    return Convert.ToString(valueA * valueB);

                case '/':
                    listBox2.Items.Add(backValue + "/" + frontValue + "=" + Convert.ToString(valueA / valueB));
                    return Convert.ToString(valueA / valueB);

                case '^':
                    listBox2.Items.Add(backValue + "^" + frontValue + "=" + Convert.ToString(Math.Pow(valueA, valueB)));
                    return Convert.ToString(Math.Pow(valueA, valueB));


                default:
                    return Convert.ToString(valueA);
            }
        }

        public void Insert(char value)
        {
            frontValue += value;
            UpdateForm();
        }

        public void Calculate()
        {
            FormatCheck(ref frontValue,calculatorFormat);
            FormatCheck(ref backValue,calculatorFormat);


            if (frontValue.Length < 1 || backValue.Length < 1)
            {
                return;
            }
            try
            {
                frontValue = DoMath(Convert.ToDouble(backValue), Convert.ToDouble(frontValue));
            }
            catch (Exception)
            {
                frontValue = "Error";
            }

            operatorPressed = false;
            backValue = string.Empty;
            operatorChar = ' ';

            UpdateForm();
        }

        public void AddOperater(char value)
        {
            if (backValue.Length < 1 && frontValue.Length < 1)
            {
                return;
            }

            if (!operatorPressed)
            {
                operatorChar = value;
                backValue = frontValue;
                frontValue = string.Empty;
                operatorPressed = true;

                UpdateForm();
                textBox1.Text = backValue;
                return;
            }
            else
            {
                if (frontValue.Length < 1)
                {
                    //Change operator
                    operatorChar = value;
                    string tmp = textBox1.Text;
                    UpdateForm();
                    textBox1.Text = tmp;

                    return;
                }
                Calculate();
                AddOperater(value);
            }
        }

        public Form1()
        {
            UpdateCurrency("EUR");
            InitializeComponent();
        }

        private void button29_Click(object sender, EventArgs e)
        {
            label7.Text = frontValue;
            memmory = frontValue;
        }

        private void button27_Click(object sender, EventArgs e)
        {
            frontValue = memmory;
            UpdateForm();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
