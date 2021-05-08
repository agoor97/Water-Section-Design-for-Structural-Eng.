using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data.Sql;
using System.Configuration;


namespace Water_SectionDrMagdy_Tayel
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(txtType.Text == "Pure Tension")
            {
                txtTw.Enabled = true;
                txtMw.Enabled = false;
                txtPw.Enabled = false;
           
                GroupRoo.Enabled = false;
                panelAS2.Visible = false;
                lblAS1.Text = "As";
            }

            if( txtType.Text == "Pure Compression")
            {
                txtPw.Enabled = true;
                txtMw.Enabled = false;
                txtTw.Enabled = false;
                GroupRoo.Enabled = false;
                panelAS2.Visible = false;
                lblAS1.Text = "As";
            }

            if(txtType.Text == "Pure Moment")
            {
                txtMw.Enabled = true;
                txtPw.Enabled = false;
                txtTw.Enabled = false;
                GroupRoo.Enabled = false;
                panelAS2.Visible = false;
            }

            if ( txtType.Text == "Moment+Tension")
            {
                txtMw.Enabled = true;
                txtPw.Enabled = false;
                txtTw.Enabled = true;
                GroupRoo.Enabled = false;
                panelAS2.Visible = false;
                lblAS1.Text = "As";
            }
            if (txtType.Text == "Moment+Comp.")
            {
                txtMw.Enabled = true;
                txtPw.Enabled = true;
                txtTw.Enabled = false;
                GroupRoo.Enabled = false;
                panelAS2.Visible = false;
                lblAS1.Text = "As";
            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtType.Text = "Pure Tension";
            txtFy.Text = "36/52";
            txtTw.Enabled = true;
            txtMw.Enabled = false;
            txtPw.Enabled = false;
        

            GroupRoo.Enabled = false;
            panelAS2.Visible = false;
            lblAS1.Text = "As";
        }

        private void groupBox3_Enter(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }
      
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {

                if (txtFcu.Text.Trim() == "" || txtFy.Text.Trim() == "" || txtType.Text.Trim() == "" || txtC.Text.Trim() == "")

                {
                    MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                double fcu = double.Parse(txtFcu.Text);
                double fy;
                double b = double.Parse(txtb.Text);

                double cover = double.Parse(txtC.Text);
                ///
                double Fctr = 0.6 * Math.Sqrt(fcu);
                /// 

                if (txtFy.Text == "36/52")
                {
                    
                    fy = 360;
                }
                else if (txtFy.Text == "40/60")
                {
                    fy = 400;
                }
                else
                {
                    fy = 360;
                }

                /// 
                ////// get (k or = epsilon)
                double eps;
                if (txtFcu.Text == "17.5")
                {
                    eps = 0.71;
                }
                else if (txtFcu.Text == "20.0")
                {
                    eps = 0.67;
                }
                else if (txtFcu.Text == "22.5")
                {
                    eps = 0.63;
                }
                else if (txtFcu.Text == "25.0")
                {
                    eps = 0.59;
                }
                else if (txtFcu.Text == "27.5")
                {
                    eps = 0.56;
                }
                else if (txtFcu.Text == "30.0")
                {
                    eps = 0.53;
                }
                else
                {
                    eps = 0.59;
                }

                /////// get (Bcr)
                double Bcr;

                if(txtFy.Text == "36/52")
                {
                    if (txtFai1.Text == "10")
                    {
                        Bcr = 0.93;
                    }
                    else if (txtFai1.Text == "12")
                    {
                        Bcr = 0.85;
                    }
                    else if (txtFai1.Text == "16")
                    {
                        Bcr = 0.75;
                    }
                    else if (txtFai1.Text == "18")
                    {
                        Bcr = 0.70;
                    }
                    else if (txtFai1.Text == "20")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "22")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "25")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "28")
                    {
                        Bcr = 0.56;
                    }
                    else
                    {
                        Bcr = 0.75;
                    }
                }
                //////

               else if(txtFy.Text == "40/60")
                {
                    if (txtFai1.Text == "10")
                    {
                        Bcr = 0.83;
                    }
                    else if (txtFai1.Text == "12")
                    {
                        Bcr = 0.75;
                    }
                    else if (txtFai1.Text == "16")
                    {
                        Bcr = 0.67;
                    }
                    else if (txtFai1.Text == "18")
                    {
                        Bcr = 0.67;
                    }
                    else if (txtFai1.Text == "20")
                    {
                        Bcr = 0.58;
                    }
                    else if (txtFai1.Text == "22")
                    {
                        Bcr = 0.58;
                    }
                    else if (txtFai1.Text == "25")
                    {
                        Bcr = 0.58;
                    }
                    else if (txtFai1.Text == "28")
                    {
                        Bcr = 0.50;
                    }
                    else
                    {
                        Bcr = 0.67;
                    }
                }

                else
                {
                    if (txtFai1.Text == "10")
                    {
                        Bcr = 0.93;
                    }
                    else if (txtFai1.Text == "12")
                    {
                        Bcr = 0.85;
                    }
                    else if (txtFai1.Text == "16")
                    {
                        Bcr = 0.75;
                    }
                    else if (txtFai1.Text == "18")
                    {
                        Bcr = 0.70;
                    }
                    else if (txtFai1.Text == "20")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "22")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "25")
                    {
                        Bcr = 0.65;
                    }
                    else if (txtFai1.Text == "28")
                    {
                        Bcr = 0.56;
                    }
                    else
                    {
                        Bcr = 0.75;
                    }
                }
             
            
                    /////// firstly :

                if (txtType.Text == "Pure Tension")
                {
                    if (txtTw.Text.Trim() == "" || txtFai1.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    panelAS2.Visible = false;
                    lblAS1.Text = "As";
                    GroupRoo.Enabled = false;

                    double Tw = double.Parse(txtTw.Text);
                    int fai1 = int.Parse(txtFai1.Text);

                    double tideal = 1000 * eps * Tw / b;
     
                    if (tideal < 250)
                    {
                        tideal = 250;
                    }
                    tideal = 50 * Math.Ceiling(tideal / 50);
                    //
                    txttideal.Text = tideal.ToString();
                    //
                    
                    double As = ((1 / Bcr) * (Tw * 1.5 * 1000)) / (0.87 * fy);
                
                    double num = Math.Ceiling(As / (3.1459 * 0.25 * fai1 * fai1));

                    //
                    txtnum1.Text = num.ToString();


                    //////// Add to the Table;
                    // table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string col = txtb.Text + " * " + txttideal.Text;
                        string Mww = "----";
                        string Tww = txtTw.Text;
                        string Pww = "----";
                        string Stage = "Safe";
                        string RFT = txtnum1.Text + " T " + txtFai1.Text;
                        object[] data = { serial, col, Mww, Tww, Pww, Stage, RFT };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }


                }
                //////////////////////////////////////////////////// finished
                /////////////////////////////////////

                //// secondly:
                if (txtType.Text == "Pure Moment")
                {
                    if (txtMw.Text.Trim() == "" || txtFai1.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    panelAS2.Visible = false;
                    lblAS1.Text = "As";
                    GroupRoo.Enabled = false;

                    double Mw = double.Parse(txtMw.Text);
                    int fai1 = int.Parse(txtFai1.Text);
                    double tideal = Math.Sqrt((Mw * 1000 * 1000) / (b * 0.30));
                    
                    if (tideal < 250)
                    {
                        tideal = 250;
                    }
                    tideal = 50 * Math.Ceiling(tideal / 50);
                    //
                    txttideal.Text = tideal.ToString();
                    //
                    /// check stresse
                    double Fct = Fctr;
                    double Ft = 6 * Mw * 1000 * 1000 / (b * tideal * tideal);

                    if(Fct < Ft)
                    {
                        MessageBox.Show("Section is UnSafe for Stage 1 .. try diffrent Criteria.", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    ///
                    double d = tideal - cover;
                    double Ku = (Mw * 1.5 * 1000 * 1000) / (b * d * d);

                    ////// حل  معادلة لاحصل على الميو

                    double A = 0.83868 * fy * fy / fcu;
                    double B = -1 * 0.87 * fy;
                    double C = Ku;

                    double root1 = (-B + Math.Sqrt(B * B - (4 * A * C))) / (2 * A);
                    double root2 = (-B - Math.Sqrt(B * B - (4 * A * C))) / (2 * A);

                    root1 = Math.Abs(root1);
                    root2 = Math.Abs(root2);

                    double mio = Math.Min(root1, root2);
                
                    if (mio < 0.0025)
                    {
                        mio = 0.0025;
                    }

                   
                    double As = (1 / Bcr) * mio * b * d;
                    double num = Math.Ceiling(As / (3.1459 * 0.25 * fai1 * fai1));

                    txtnum1.Text = num.ToString();



                    // table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string col = txtb.Text + " * " + txttideal.Text;
                        string Mww = txtMw.Text;
                        string Tww = "----";
                        string Pww = "----";
                        string Stage = "Safe";
                        string RFT = txtnum1.Text + " T " + txtFai1.Text;
                        object[] data = { serial, col, Mww, Tww, Pww, Stage, RFT };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                ///////////////////////////////////////////// finish
                ///////// thirdly Moment + Tension

                if (txtType.Text == "Moment+Tension")
                {
                    if (txtTw.Text.Trim() == "" || txtMw.Text.Trim() == "" || txtFai1.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    panelAS2.Visible = false;
                    lblAS1.Text = "As";
                    GroupRoo.Enabled = false;

                    double Mw = double.Parse(txtMw.Text);
                    double Tw = double.Parse(txtTw.Text);


                    double tideal = (Math.Sqrt((Mw * 1000 * 1000) / (b * 0.30))) + 40;
                    if (tideal < 250)
                    {
                        tideal = 250;
                    }
                    tideal = 50 * Math.Ceiling(tideal / 50);

                    /// check stresses

                    double Ft = (Tw * 1000 / (b * tideal)) + (6 * Mw * 1000 * 1000 / (b * tideal * tideal));
                    double FctN = (Tw * 1000) / (b * tideal);
                    double Fctm = (6 * Mw * 1000 * 1000) / (b * tideal * tideal);
                    double tv = tideal * (1 + (FctN / Fctm));

                    double eita;
                    if (tv <= 100)
                    {
                        eita = 1.0;
                    }
                    else if (tv > 100 && tv <= 200)
                    {
                        eita = 1.30;
                    }
                    else if (tv > 200 && tv <= 400)
                    {
                        eita = 1.60;
                    }
                    else if (tv > 400 && tv <= 600)
                    {
                        eita = 1.70;
                    }
                    else
                    {
                        eita = 1.60;
                    }
                    double Fct = Fctr / eita;

                    if (Fct < Ft)
                    {
                        MessageBox.Show("Section is UnSafe for Stage 1 ..  I will increase t by 50 mm , and reCheck.", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        for (int i = 1; i < 3; i++)
                        {
                          
                            Ft = (Tw * 1000 / (b * tideal)) + (6 * Mw * 1000 * 1000 / (b * tideal * tideal));
                            if (Fct < Ft)
                            {
                                tideal = tideal + 50;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                    //
                    txttideal.Text = tideal.ToString();
                    //
                 
                    double ecc = Mw / Tw;       //m
                    double ratio = (ecc * 1000) / tideal;
                    if (ratio < 0.5)        //// small eccen.
                    {
                        panelAS2.Visible = true;
                        lblAS1.Text = "As1";

                        if (txtFai1.Text.Trim() == "" || txtFai2.Text.Trim() == "")
                        {
                            MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        int fai1 = int.Parse(txtFai1.Text);
                        int fai2 = int.Parse(txtFai2.Text);


                        double aa = (tideal / 2000) - ecc - (0.001 * cover); ///m
                        double bb = (tideal / 2000) + ecc - (0.001 * cover);   // m

                        double T1 = (Tw * 1.5 * bb) / (aa + bb);
                        double T2 = (Tw * 1.5 * aa) / (aa + bb);
                      
                        double As1 = (1 / Bcr) * ((T1 * 1000) / (0.87 * fy));
                        double As2 = (1 / Bcr) * ((T2 * 1000) / (0.87 * fy));
                    
                        double num1 = Math.Ceiling(As1 / (3.1459 * 0.25 * fai1 * fai1));
                        double num2 = Math.Ceiling(As2 / (3.1459 * 0.25 * fai2 * fai2));

                        txtnum1.Text = num1.ToString();
                        txtnum2.Text = num2.ToString();

                        // table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string col = txtb.Text + " * " + txttideal.Text;
                            string Mww = txtMw.Text;
                            string Tww = txtTw.Text;
                            string Pww = "----";
                            string Stage = "Safe";

                            string RFT = txtnum1.Text + " T " + txtFai1.Text + "||" + txtnum2.Text + " T " + txtFai2.Text;
                            object[] data = { serial, col, Mww, Tww, Pww, Stage, RFT };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }
                    else
                    {

                        /////  ratio >= 0.5   big ecce.
                        ////using Approach method

                        panelAS2.Visible = false;
                        lblAS1.Text = "As";


                        int fai1 = int.Parse(txtFai1.Text);
                        double d = tideal - cover;

                        double es = ecc - (tideal / 2000) + (0.001 * cover);
                        double Mus = Tw * 1.5 * es;
                        double Ku = (Mus * 1000 * 1000) / (b * d * d);

                       
                        ////// حل  معادلة لاحصل على الميو

                        double A = 0.83868 * fy * fy / fcu;
                        double B = 0.87 * fy;
                        double C = Ku;

                        double root1 = (-B + Math.Sqrt(B * B - (4 * A * C))) / (2 * A);
                        double root2 = (-B - Math.Sqrt(B * B - (4 * A * C))) / (2 * A);

                        root1 = Math.Abs(root1);
                        root2 = Math.Abs(root2);

                        double mio = Math.Min(root1, root2);
                        if (mio < 0.0025)
                        {
                            mio = 0.0025;
                        }

                      
                        double As = (1 / Bcr) * (mio * b * d + (Tw * 1.5 * 1000 / (0.87 * fy)));
                        double num = Math.Ceiling(As / (3.1459 * 0.25 * fai1 * fai1));

                        txtnum1.Text = num.ToString();


                        // table
                        DialogResult dr;
                        dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                        if (dr == DialogResult.OK)
                        {
                            string serial = "";
                            string col = txtb.Text + " * " + txttideal.Text;
                            string Mww = txtMw.Text;
                            string Tww = txtTw.Text;
                            string Pww = "----";
                            string Stage = "Safe";

                            string RFT = txtnum1.Text + " T " + txtFai1.Text;
                            object[] data = { serial, col, Mww, Tww, Pww, Stage, RFT };
                            DataGridView1.Rows.Add(data);
                            return;
                        }
                        else
                        {
                            return;
                        }
                    }

                 
                }

                //////////////////////////////////////////////////////////////finish

                ////////// finally comp+Moment


                if (txtType.Text == "Moment+Comp.")
                {
                    if (txtMw.Text.Trim() == "" || txtPw.Text.Trim() == "" || txtFai1.Text.Trim() == "")
                    {
                        MessageBox.Show("Missing Data ..", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    panelAS2.Visible = false;
                    lblAS1.Text = "As";
                    GroupRoo.Enabled = false;

                    double Pw = double.Parse(txtPw.Text);
                    double Mw = double.Parse(txtMw.Text);
                    int fai1 = int.Parse(txtFai1.Text);

                    double tideal = (Math.Sqrt((Mw * 1000 * 1000) / (b * 0.30))) - 20;
                    if (tideal < 250)
                    {
                        tideal = 250;
                    }
                    tideal = 50 * Math.Ceiling(tideal / 50);
                   
                    ///
                    /// check stresses

                    double Ft = (-1 * Pw * 1000 / (b * tideal)) + (6 * Mw * 1000 * 1000 / (b * tideal * tideal));
                    double FctN = (Pw * 1000) / (b * tideal);
                    double FctM = (6 * Mw * 1000 * 1000) / (b * tideal * tideal);
                    double tv = tideal * (1 - (FctN / FctM));

                    double eita;
                    if (tv <= 100)
                    {
                        eita = 1.0;
                    }
                    else if (tv > 100 && tv <= 200)
                    {
                        eita = 1.30;
                    }
                    else if (tv > 200 && tv <= 400)
                    {
                        eita = 1.60;
                    }
                    else if (tv > 400 && tv <= 600)
                    {
                        eita = 1.70;
                    }
                    else
                    {
                        eita = 1.60;
                    }
                    double Fct = Fctr / eita;



                    if (Fct < Ft)
                    {
                        MessageBox.Show("Section is UnSafe for Stage 1 ..  I will increase t by 50 mm , and reCheck.", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        for (int i = 1; i < 5; i++)
                        {
                          
                            Ft = (Pw * 1000 / (b * tideal)) + (6 * Mw * 1000 * 1000 / (b * tideal * tideal));
                            if (Fct < Ft)
                            {
                                tideal = tideal + 50;
                            }
                            else
                            {
                                break;
                            }

                        }
                    }

                    //
                    txttideal.Text = tideal.ToString();
                    //
                    ///

                    ////////////////////////////////////////////////

                    double ecc = Mw / Pw;       //m
                    double ratio = (ecc * 1000) / tideal;
                    if (ratio < 0.5)        //// small eccen.
                    {
 
                        if (txtro.Text.Trim() == "")
                        {
                            MessageBox.Show("Enter I.D and Get ρ firstly, the reClick Design .", "Info.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            GroupRoo.Enabled = true;
                            return;
                        }
                        else
                        {
                            GroupRoo.Enabled = true;
                            double Ro = double.Parse(txtro.Text);
                            double AS = (1 / Bcr) * Ro * fcu * Math.Pow(10, -4) * b * tideal;
                            double num = Math.Ceiling(AS / (3.1459 * 0.25 * fai1 * fai1));
                            txtnum1.Text = num.ToString();

                        }

                    }
                    else
                    {

                        /////  ratio >= 0.5   big ecce.
                        ////using Approach method           
                        GroupRoo.Enabled = false;
                               
                        double d = tideal - cover;

                        double es = ecc + (tideal / 2000) - (0.001 * cover);
                        double Mus = Pw * 1.5 * es;
                        double Ku = (Mus * 1000 * 1000) / (b * d * d);


                        ////// حل  معادلة لاحصل على الميو

                        double A = 0.83868 * fy * fy / fcu;
                        double B = 0.87 * fy;
                        double C = Ku;

                        double root1 = (-B + Math.Sqrt(B * B - (4 * A * C))) / (2 * A);
                        double root2 = (-B - Math.Sqrt(B * B - (4 * A * C))) / (2 * A);

                        root1 = Math.Abs(root1);
                        root2 = Math.Abs(root2);

                        double mio = Math.Min(root1, root2);
                        if (mio < 0.0025)
                        {
                            mio = 0.0025;
                        }


                        double As = (1 / Bcr) * (mio * b * d - (Pw * 1.5 * 1000 / (0.87 * fy)));
                        double num = Math.Ceiling(As / (3.1459 * 0.25 * fai1 * fai1));

                        txtnum1.Text = num.ToString();
                    }
                    ////


                    // table
                    DialogResult dr;
                    dr = MessageBox.Show("Add to the Table ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                    if (dr == DialogResult.OK)
                    {
                        string serial = "";
                        string col = txtb.Text + " * " + txttideal.Text;
                        string Mww = txtMw.Text;
                        string Tww = "----";
                        string Pww = txtPw.Text;
                        string Stage = "Safe";
                        string RFT = txtnum1.Text + " T " + txtFai1.Text;
                        object[] data = { serial, col, Mww, Tww, Pww, Stage, RFT };
                        DataGridView1.Rows.Add(data);
                        return;
                    }
                    else
                    {
                        return;
                    }




                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Interaction inter = new Interaction(this);
            inter.ShowDialog();

            txtro.Text = inter.txtro1.Text;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            txttideal.Clear();
            txtro.Clear();
            txtnum1.Clear();
            txtnum2.Clear();
            GroupRoo.Enabled = false;
            panelAS2.Visible = false;
            lblAS1.Text = "As";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ///////////////////// هنا يتم أخد الاسكرينة 
            SaveFileDialog sf = new SaveFileDialog();
            sf.Filter = "Images|*.bmp;*.jpg;*.png";
            sf.Title = " Water Section according to Dr/Magdy ";
            Bitmap bmp = new Bitmap(this.Width, this.Height);
            Graphics g = Graphics.FromImage(bmp);
            panel1.DrawToBitmap(bmp, new Rectangle(0, 0, panel1.Width, panel1.Height));
            if (sf.ShowDialog() == DialogResult.OK)
            {
                string path = sf.FileName;
                bmp.Save(path);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Remove this Row ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);

                    if (dr == DialogResult.OK)
                    {
                        DataGridView1.Rows.Remove(DataGridView1.CurrentRow);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                if (DataGridView1.CurrentRow != null)
                {
                    DialogResult dr;
                    dr = MessageBox.Show("Do you Want to Delete all Rows ?", "Asking", MessageBoxButtons.OKCancel, MessageBoxIcon.Error);
                    if (dr == DialogResult.OK)
                    {
                        DataGridView1.Rows.Clear();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void ToExcel(DataGridView dgv, string filename)
        {
            string stoutput = "";
            string sheader = "";
            for (int j = 0; j < dgv.Columns.Count; j++)
                sheader = sheader.ToString() + Convert.ToString(dgv.Columns[j].HeaderText) + "\t";
            stoutput += sheader + "\r\n";

            for (int i = 0; i < dgv.RowCount - 1; i++)
            {
                string stLine = "";
                for (int j = 0; j < dgv.Rows[i].Cells.Count; j++)
                    stLine = stLine.ToString() + Convert.ToString(dgv.Rows[i].Cells[j].Value) + "\t";
                stoutput += stLine + "\r\n";
            }
            Encoding utf16 = Encoding.GetEncoding(1254);
            byte[] output = utf16.GetBytes(stoutput);
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);
            bw.Write(output, 0, output.Length);
            bw.Flush();
            bw.Close();
            fs.Close();



        }


        private void button7_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "ExcelDocument (*.xls)|*.xls";


            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string path = sfd.FileName;
                ToExcel(DataGridView1, path);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            MessageBox.Show("I take ϻ(min.)= 0.25% && t(min)= 250 mm && 1 ton=1000 kg=10 kN.", "Notes", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
