using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CPE200Lab1
{
    public partial class MainForm : Form
    {
        private bool containsDot;
        private bool isAllowBack;
        private bool isAfterOperater;
        private bool isAfterEqual;
        private string firstOperand;
        private string operate;
        private string checksomething;
        private double memo = 0;
        private string mem = "";
        private bool isAfterpush;
        string xdev;
        //private string Nbtn;


        private CalculatorEngine engine;

        private void Mbtn(object sender, EventArgs e)
        {
            string MBT;
            mem = lblDisplay.Text;
                
                MBT = ((Button)sender).Text;

            if (MBT == "MS") memo = Convert.ToDouble(mem);
            else if (MBT == "MC") memo = 0;
            else if (MBT == "MR") lblDisplay.Text = memo.ToString();
            else if (MBT == "M+") memo = memo + Convert.ToDouble(mem);
            else if (MBT == "M-") memo = memo - Convert.ToDouble(mem);

            isAfterEqual = true;
        }
        private void resetAll()
        {
            lblDisplay.Text = "0";
            isAllowBack = true;
            containsDot = false;
            isAfterOperater = false;
            isAfterEqual = false;
        }

        private string calculate(string operate, string firstOperand, string secondOperand, int maxOutputSize = 8)
        {
            checksomething = secondOperand;
            switch(operate)
            {
                case "+":
                    return (Convert.ToDouble(firstOperand) + Convert.ToDouble(secondOperand)).ToString();
                case "-":
                    return (Convert.ToDouble(firstOperand) - Convert.ToDouble(secondOperand)).ToString();
                case "X":
                    return (Convert.ToDouble(firstOperand) * Convert.ToDouble(secondOperand)).ToString();
                case "÷":
                    // Not allow devide be zero
                    if(secondOperand != "0")
                    {
                        double result;
                        string[] parts;
                        int remainLength;

                        result = (Convert.ToDouble(firstOperand) / Convert.ToDouble(secondOperand));
                        // split between integer part and fractional part
                        parts = result.ToString().Split('.');
                        // if integer part length is already break max output, return error
                        if(parts[0].Length > maxOutputSize)
                        {
                            return "E";
                        }
                        // calculate remaining space for fractional part.
                        remainLength = maxOutputSize - parts[0].Length - 1;
                        // trim the fractional part gracefully. =
                        return result.ToString("N" + remainLength);
                    }
                    break;

                case "%":
                    secondOperand = ((Convert.ToDouble(firstOperand) / 100) * Convert.ToDouble(secondOperand)).ToString();
                    //your code here
                    break;
                    //return (Convert.ToDouble(firstOperand) % Convert.ToDouble(secondOperand)).ToString();

            }
            return "E";
        }
            

        public MainForm()
        {
            InitializeComponent();
            engine = new CalculatorEngine();
            resetAll();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (isAfterOperater)
            {
                lblDisplay.Text = "0";
            }
            if(lblDisplay.Text.Length is 8)
            {
                return;
            }
            isAllowBack = true;
            string digit = ((Button)sender).Text;
            if(lblDisplay.Text is "0")
            {
                lblDisplay.Text = "";
            }
            lblDisplay.Text += digit;
            
            isAfterOperater = false;
        }

        private void btnOperator_Click(object sender, EventArgs e)
        {
            string secondOperand = lblDisplay.Text;
            string op = ((Button)sender).Text;
            if (op == "%")
            {   
                secondOperand = ((Convert.ToDouble(firstOperand) / 100) * Convert.ToDouble(secondOperand)).ToString();
                lblDisplay.Text = secondOperand;
                return;
            }
            //string secondOperand = lblDisplay.Text;
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                secondOperand = checksomething;
            }
            if (isAfterpush)
            {
                if (lblDisplay.Text is "Error")
                {
                    return;
                }
                //string secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
                // secondOperand = "0";
                firstOperand = result;
                
            }
            if (isAfterOperater)
            {
               /* if (lblDisplay.Text is "Error")
                {
                    return;
                }
                //string secondOperand = lblDisplay.Text;
                string result = engine.calculate(operate, firstOperand, secondOperand);
                if (result is "E" || result.Length > 8)
                {
                    lblDisplay.Text = "Error";
                }
                else
                {
                    lblDisplay.Text = result;
                }
                isAfterEqual = true;
                // secondOperand = "0";
                firstOperand = result;*/
                return;
            }
            operate = ((Button)sender).Text;
            switch (operate)
            {
                case "+":
                case "-":
                case "X":
                case "÷":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    isAfterpush = true;
                    break;
                case "%":
                    firstOperand = lblDisplay.Text;
                    isAfterOperater = true;
                    isAfterpush = true;
                    // your code here
                    break;
            }
            isAllowBack = false;
        }

        private void btnEqual_Click(object sender, EventArgs e)
        {
            string secondOperand = lblDisplay.Text;
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                secondOperand = checksomething; 
            }
            //string secondOperand = lblDisplay.Text;
            string result = engine.calculate(operate, firstOperand, secondOperand);
            if (result is "E" || result.Length > 8)
            {
                lblDisplay.Text = "Error";
            }
            else
            {
                lblDisplay.Text = result;
            }
            isAfterEqual = true;
            firstOperand = result;



        }

        private void btnDot_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                resetAll();
            }
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if (!containsDot)
            {
                lblDisplay.Text += ".";
                containsDot = true;
            }
        }

        private void btnSign_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
          
            // already contain negative sign
            if (lblDisplay.Text.Length is 8)
            {
                return;
            }
            if(lblDisplay.Text[0] is '-')
            {
                lblDisplay.Text = lblDisplay.Text.Substring(1, lblDisplay.Text.Length - 1);
            } else
            {
                lblDisplay.Text = "-" + lblDisplay.Text;
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            resetAll();
            isAfterpush = false;
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            if (isAfterEqual)
            {
                return;
            }
            if (!isAllowBack)
            {
                return;
            }
            if(lblDisplay.Text != "0")
            {
                string current = lblDisplay.Text;
                char rightMost = current[current.Length - 1];
                if(rightMost is '.')
                {
                    containsDot = false;
                }
                lblDisplay.Text = current.Substring(0, current.Length - 1);
                if(lblDisplay.Text is "" || lblDisplay.Text is "-")
                {
                    lblDisplay.Text = "0";
                }
            }
        }

        private void lblDisplay_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string sqtop = lblDisplay.Text;
            double sqrresult = Math.Sqrt(Convert.ToDouble(sqtop));
            //string result = engine.calculate(operate, firstOperand, secondOperand);
            /* if (sqrresult is "E" || sqrresult.Length > 8)
             {
                 lblDisplay.Text = "Error";
             }
             else*/
            string sqrstr = sqrresult.ToString();


            
                lblDisplay.Text = sqrresult.ToString();
            
            isAfterEqual = true;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (lblDisplay.Text is "Error")
            {
                return;
            }
            string x1op = lblDisplay.Text;
            //double x1result = Math.Sqrt(Convert.ToDouble(x1op));
            double x1result =( 1 / (Convert.ToDouble(x1op)));
            //string result = engine.calculate(operate, firstOperand, secondOperand);
            /*if ((x1result.ToString()).Length > 8)
             {
                 lblDisplay.Text = "Error";
             }
             else*/
            string x2str = x1result.ToString();
            if (x2str.Length > 8)
            {
                //int x = x2str.Length;
                xdev = x2str.Remove(8);
            }
            //x1result = Convert.ToDouble(xdevide);
            //{
            //lblDisplay.Text = x1result.ToString();
            lblDisplay.Text = xdev;
           
            //}
            isAfterEqual = true;
        }
    }
}
