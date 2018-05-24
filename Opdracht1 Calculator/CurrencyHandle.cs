using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using System.Net;

namespace Opdracht1_Calculator
{
    partial class Form1
    {
        string currencyJson = "";
        string currencyFrom = "EUR";
        dynamic currencyTable;

        string currencyFormat = "1234567890.";
        
        

        private string Get(string uri)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
                request.AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate;

                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
            catch (Exception)
            {
                System.Windows.Forms.MessageBox.Show("Could not load currency value's");
                return "";
            }
        }

        private void UpdateCurrency(string baseValue)
        {
            currencyFrom = baseValue;
            currencyJson = Get("https://api.fixer.io/latest?base=" + baseValue);

            currencyTable = JsonConvert.DeserializeObject<dynamic>(currencyJson);
        }

        private string MultiplyCurrency(string value ,string toBaseValue)
        {
            try
            {
                if (toBaseValue == currencyFrom)
                {
                    if (checkBox1.Checked)
                        return Convert.ToString(Math.Round(Convert.ToDouble(value), 2));
                    else
                        return Convert.ToString(Convert.ToDouble(value)); 
                }
                else
                {
                    double multiplier;
                    switch (toBaseValue)
                    {
                        case "USD":
                            multiplier = Convert.ToDouble(currencyTable.rates.USD); break;
                        case "EUR":
                            multiplier = Convert.ToDouble(currencyTable.rates.EUR); break;
                        case "JPY":
                            multiplier = Convert.ToDouble(currencyTable.rates.JPY); break;
                        case "GBP":
                            multiplier = Convert.ToDouble(currencyTable.rates.GBP); break;
                        case "AUD":
                            multiplier = Convert.ToDouble(currencyTable.rates.AUD); break;
                        case "CAD":
                            multiplier = Convert.ToDouble(currencyTable.rates.CAD); break;

                        default:
                            multiplier = 1; break;
                    }


                    if (checkBox1.Checked)
                        return Convert.ToString(Math.Round(Convert.ToDouble(value) * multiplier, 2));
                    else
                        return Convert.ToString(Convert.ToDouble(value) * multiplier);
                }
            }
            catch (Exception)
            {
                label4.Text = "Too big";
                label4.Visible = true;
            }
            return "0";     
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            textBox2_TextChanged(sender, e);
        }

        private void UpdateText(string value)
        {
            listBox1.Items.Clear();

            listBox1.Items.Add(MultiplyCurrency(value, "USD") + " United States dollar (USD)");
            listBox1.Items.Add(MultiplyCurrency(value, "EUR") + " Euro (EUR)");
            listBox1.Items.Add(MultiplyCurrency(value, "JPY") + " Japanese yen (JPY)");
            listBox1.Items.Add(MultiplyCurrency(value, "GBP") + " Pound sterling (GBP)");
            listBox1.Items.Add(MultiplyCurrency(value, "AUD") + " Australian dollar (AUD)");
            listBox1.Items.Add(MultiplyCurrency(value, "CAD") + " Canadian dollar (CAD)");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            label4.Visible = false;
            string temp = textBox2.Text;

            string error = FormatCheck(ref temp,currencyFormat);

            if (error != string.Empty)
            {
                label4.Text = "Error: " + error;
                label4.Visible = true;
                return;
            }


            if (temp.Length > 0)
            {
                UpdateText(temp);
            }
        }

    }

}