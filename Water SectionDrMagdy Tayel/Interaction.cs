using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Water_SectionDrMagdy_Tayel
{
    public partial class Interaction : Form
    {
        Form1 form1;
        public Interaction(Form1 form11)
        {
            InitializeComponent();
            form1 = form11;
        }

        private void Interaction_Load(object sender, EventArgs e)
        {
            try
            {

                double t = double.Parse(form1.txttideal.Text);
                double b = double.Parse(form1.txtb.Text);
                double fcu = double.Parse(form1.txtFcu.Text);

                double fy;
                if (form1.txtFy.Text == "36/52")
                {
                    fy = 360;
                }
                else
                {
                    fy = 400;
                }
              
                                
                double cover = double.Parse(form1.txtC.Text);
                double M = double.Parse(form1.txtMw.Text);
                double P = double.Parse(form1.txtPw.Text);
                
                double d = t - cover;
                double Ac = b * t;

                double horiz = (M * Math.Pow(10, 6)) / (fcu * b * t * t);
                double Vert = (P * 1000) / (fcu * b * t);
                this.chart1.Series["Point"].Points.AddXY(horiz, Vert);

                ////// draw ID by 7 points

                //// firstly Assume Ro = 1.0 
                ///// curve 1 

                double As11 = 1.0 * fcu * Math.Pow(10, -4) * b * t;
                double As21 = As11;
                double Astotal1 = As11 + As21;

                // first Point 
                double M11 = 0;
                double P11 = (0.35 * fcu * (Ac - Astotal1) + 0.67 * fy * Astotal1) / 1000;
                // second point 
                double P21 = (0.35 * fcu * (Ac - Astotal1) + 0.67 * fy * Astotal1) / 1000;
                double M21 = P21 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb1 = (600 * d) / (600 + 0.87 * fy);
                double ab1 = 0.8 * Cb1;
                double P31 = (0.45 * fcu * ab1 * b + 0.87 * fy * As21 - 0.87 * fy * As11) / 1000;
                double M31 = (0.45 * fcu * ab1 * b * ((t / 2) - (ab1 / 2)) + 0.87 * fy * As21 * ((t / 2) - cover) + 0.87 * fy * As11 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P41 = 0;
                double M41 = 0.87 * fy * As11 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M51 = 0;
                //double P51 = 0.87 * fy * Astotal1/1000;

                // point 6 Lies in Compression Zone
                double ey = (0.87 * fy) / (2 * Math.Pow(10, 5));
                // Assume a > ab 
                double a61 = ab1 + (0.2*t);        // mm 
                double C61 = 1.25 * a61;
                double epsilon61 = (0.003 * (d - C61)) / C61;
                double Fs11;
                if (epsilon61 >= ey)
                {
                    Fs11 = 0.87 * fy;
                }
                else
                {
                    Fs11 = epsilon61 * 2 * Math.Pow(10, 5);
                }
                double P61 = (0.45 * fcu * a61 * b + 0.87 * fy * As21 - Fs11 * As11) / 1000;
                double M61 = (0.45 * fcu * a61 * b * ((t / 2) - (a61 / 2)) + 0.87 * fy * As21 * ((t / 2) - cover) + Fs11 * As11 * ((t / 2) - cover)) / (1000 * 1000);

                //// point 7 Lies in Tension Zone 
                double a71 = ab1 - (0.2 * t);       //mm
                double C71 = 1.25 * a71;
                double epsilon71 = (0.003 * (C71 - cover)) / C71;
                double Fs21;
                if (epsilon71 >= ey)
                {
                    Fs21 = 0.87 * fy;
                }
                else
                {
                    Fs21 = epsilon71 * 2 * Math.Pow(10, 5);
                }
                double P71 = (0.45 * fcu * a71 * b + Fs21 * As21 - 0.87 * fy * As11) / 1000;
                double M71 = (0.45 * fcu * a71 * b * ((t / 2) - (a71 / 2)) + Fs21 * As21 * ((t / 2) - cover) + 0.87 * fy * As11 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P11fianl = (P11 * 1000) / (fcu * b * t);
                double P21fianl = (P21 * 1000) / (fcu * b * t);
                double P31fianl = (P31 * 1000) / (fcu * b * t);
                double P41fianl = (P41 * 1000) / (fcu * b * t);
                //double P5fianl = (P51 * 1000) / (fcu * b * t);
                double P61fianl = (P61 * 1000) / (fcu * b * t);
                double P71fianl = (P71 * 1000) / (fcu * b * t);

                double M11fianl = (M11 * 1000 * 1000) / (fcu * b * t * t);
                double M21fianl = (M21 * 1000 * 1000) / (fcu * b * t * t);
                double M31fianl = (M31 * 1000 * 1000) / (fcu * b * t * t);
                double M41fianl = (M41 * 1000 * 1000) / (fcu * b * t * t);
                //double M5fianl = (M51* 1000 * 1000) / (fcu * b * t * t);
                double M61fianl = (M61 * 1000 * 1000) / (fcu * b * t * t);
                double M71fianl = (M71 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D1"].Points.AddXY(M11fianl, P11fianl);
                this.chart1.Series["I.D1"].Points.AddXY(M21fianl, P21fianl);
                this.chart1.Series["I.D1"].Points.AddXY(M61fianl, P61fianl);
                this.chart1.Series["I.D1"].Points.AddXY(M31fianl, P31fianl);
                this.chart1.Series["I.D1"].Points.AddXY(M71fianl, P71fianl);
                this.chart1.Series["I.D1"].Points.AddXY(M41fianl, P41fianl);
                //this.chart1.Series["I.D"].Points.AddXY(M51fianl, -1 * P51fianl);
                this.chart1.Series["I.D1"].Points.AddXY(0, 0);
                this.chart1.Series["I.D1"].Points.AddXY(M11fianl, P11fianl);









                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 2.0 
                ///// curve 1 

                double As12 = 2.0 * fcu * Math.Pow(10, -4) * b * t;
                double As22 =  As12;
                double Astotal2 = As12 + As22;

                // first Point 
                double M12 = 0;
                double P12 = (0.35 * fcu * (Ac - Astotal2) + 0.67 * fy * Astotal2) / 1000;
                // second point 
                double P22 = (0.35 * fcu * (Ac - Astotal2) + 0.67 * fy * Astotal2) / 1000;
                double M22 = P22 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb2 = (600 * d) / (600 + 0.87 * fy);
                double ab2 = 0.8 * Cb2;
                double P32 = (0.45 * fcu * ab2 * b + 0.87 * fy * As22 - 0.87 * fy * As12) / 1000;
                double M32 = (0.45 * fcu * ab2 * b * ((t / 2) - (ab2 / 2)) + 0.87 * fy * As22 * ((t / 2) - cover) + 0.87 * fy * As12 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P42 = 0;
                double M42 = 0.87 * fy * As12 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M52 = 0;
                //double P52 = 0.87 * fy * Astotal2/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a62 = ab2 + (0.2 * t);        // mm 
                double C62 = 1.25 * a62;
                double epsilon62 = (0.003 * (d - C62)) / C62;
                double Fs12;
                if (epsilon62 >= ey)
                {
                    Fs12 = 0.87 * fy;
                }
                else
                {
                    Fs12 = epsilon62 * 2 * Math.Pow(10, 5);
                }
                double P62 = (0.45 * fcu * a62 * b + 0.87 * fy * As22 - Fs12 * As12) / 1000;
                double M62 = (0.45 * fcu * a62 * b * ((t / 2) - (a62 / 2)) + 0.87 * fy * As22 * ((t / 2) - cover) + Fs12 * As12 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a72 = ab2 - (0.2 * t);       //mm
                double C72 = 1.25 * a72;
                double epsilon72 = (0.003 * (C72 - cover)) / C72;
                double Fs22;
                if (epsilon72 >= ey)
                {
                    Fs22 = 0.87 * fy;
                }
                else
                {
                    Fs22 = epsilon72 * 2 * Math.Pow(10, 5);
                }
                double P72 = (0.45 * fcu * a72 * b + Fs22 * As22 - 0.87 * fy * As12) / 1000;
                double M72 = (0.45 * fcu * a72 * b * ((t / 2) - (a72 / 2)) + Fs22 * As22 * ((t / 2) - cover) + 0.87 * fy * As12 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P12fianl = (P12 * 1000) / (fcu * b * t);
                double P22fianl = (P22 * 1000) / (fcu * b * t);
                double P32fianl = (P32 * 1000) / (fcu * b * t);
                double P42fianl = (P42 * 1000) / (fcu * b * t);
                //double P52fianl = (P52 * 1000) / (fcu * b * t);
                double P62fianl = (P62 * 1000) / (fcu * b * t);
                double P72fianl = (P72 * 1000) / (fcu * b * t);

                double M12fianl = (M12 * 1000 * 1000) / (fcu * b * t * t);
                double M22fianl = (M22 * 1000 * 1000) / (fcu * b * t * t);
                double M32fianl = (M32 * 1000 * 1000) / (fcu * b * t * t);
                double M42fianl = (M42 * 1000 * 1000) / (fcu * b * t * t);
                //double M5fianl = (M52* 1000 * 1000) / (fcu * b * t * t);
                double M62fianl = (M62 * 1000 * 1000) / (fcu * b * t * t);
                double M72fianl = (M72 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D2"].Points.AddXY(M12fianl, P12fianl);
                this.chart1.Series["I.D2"].Points.AddXY(M22fianl, P22fianl);
                this.chart1.Series["I.D2"].Points.AddXY(M62fianl, P62fianl);
                this.chart1.Series["I.D2"].Points.AddXY(M32fianl, P32fianl);
                this.chart1.Series["I.D2"].Points.AddXY(M72fianl, P72fianl);
                this.chart1.Series["I.D2"].Points.AddXY(M42fianl, P42fianl);
                //this.chart1.Series["I.D2"].Points.AddXY(M52fianl, -1 * P52fianl);
                this.chart1.Series["I.D2"].Points.AddXY(0, 0);
                this.chart1.Series["I.D2"].Points.AddXY(M12fianl, P12fianl);






                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 3.0 
                ///// curve 3 

                double As13 = 3.0 * fcu * Math.Pow(10, -4) * b * t;
                double As23 =  As13;
                double Astotal3 = As13 + As23;

                // first Point 
                double M13 = 0;
                double P13 = (0.35 * fcu * (Ac - Astotal3) + 0.67 * fy * Astotal3) / 1000;
                // second point 
                double P23 = (0.35 * fcu * (Ac - Astotal3) + 0.67 * fy * Astotal3) / 1000;
                double M23 = P23 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb3 = (600 * d) / (600 + 0.87 * fy);
                double ab3 = 0.8 * Cb3;
                double P33 = (0.45 * fcu * ab3 * b + 0.87 * fy * As23 - 0.87 * fy * As13) / 1000;
                double M33 = (0.45 * fcu * ab3 * b * ((t / 2) - (ab3 / 2)) + 0.87 * fy * As23 * ((t / 2) - cover) + 0.87 * fy * As13 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P43 = 0;
                double M43 = 0.87 * fy * As13 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M53 = 0;
                //double P53 = 0.87 * fy * Astotal3/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a63 = ab3 + (0.2 * t);        // mm 
                double C63 = 1.25 * a63;
                double epsilon63 = (0.003 * (d - C63)) / C63;
                double Fs13;
                if (epsilon63 >= ey)
                {
                    Fs13 = 0.87 * fy;
                }
                else
                {
                    Fs13 = epsilon63 * 2 * Math.Pow(10, 5);
                }
                double P63 = (0.45 * fcu * a63 * b + 0.87 * fy * As23 - Fs13 * As13) / 1000;
                double M63 = (0.45 * fcu * a63 * b * ((t / 2) - (a63 / 2)) + 0.87 * fy * As23 * ((t / 2) - cover) + Fs13 * As13 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a73 = ab3 - (0.2 * t);       //mm
                double C73 = 1.25 * a73;
                double epsilon73 = (0.003 * (C73 - cover)) / C73;
                double Fs23;
                if (epsilon73 >= ey)
                {
                    Fs23 = 0.87 * fy;
                }
                else
                {
                    Fs23 = epsilon73 * 2 * Math.Pow(10, 5);
                }
                double P73 = (0.45 * fcu * a73 * b + Fs23 * As23 - 0.87 * fy * As13) / 1000;
                double M73 = (0.45 * fcu * a73 * b * ((t / 2) - (a73 / 2)) + Fs23 * As23 * ((t / 2) - cover) + 0.87 * fy * As13 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P13fianl = (P13 * 1000) / (fcu * b * t);
                double P23fianl = (P23 * 1000) / (fcu * b * t);
                double P33fianl = (P33 * 1000) / (fcu * b * t);
                double P43fianl = (P43 * 1000) / (fcu * b * t);
                //double P53fianl = (P53 * 1000) / (fcu * b * t);
                double P63fianl = (P63 * 1000) / (fcu * b * t);
                double P73fianl = (P73 * 1000) / (fcu * b * t);

                double M13fianl = (M13 * 1000 * 1000) / (fcu * b * t * t);
                double M23fianl = (M23 * 1000 * 1000) / (fcu * b * t * t);
                double M33fianl = (M33 * 1000 * 1000) / (fcu * b * t * t);
                double M43fianl = (M43 * 1000 * 1000) / (fcu * b * t * t);
                //double M53fianl = (M53* 1000 * 1000) / (fcu * b * t * t);
                double M63fianl = (M63 * 1000 * 1000) / (fcu * b * t * t);
                double M73fianl = (M73 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D3"].Points.AddXY(M13fianl, P13fianl);
                this.chart1.Series["I.D3"].Points.AddXY(M23fianl, P23fianl);
                this.chart1.Series["I.D3"].Points.AddXY(M63fianl, P63fianl);
                this.chart1.Series["I.D3"].Points.AddXY(M33fianl, P33fianl);
                this.chart1.Series["I.D3"].Points.AddXY(M73fianl, P73fianl);
                this.chart1.Series["I.D3"].Points.AddXY(M43fianl, P43fianl);
                //this.chart1.Series["I.D3"].Points.AddXY(M53fianl, -1 * P53fianl);
                this.chart1.Series["I.D3"].Points.AddXY(0, 0);
                this.chart1.Series["I.D3"].Points.AddXY(M13fianl, P13fianl);



                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 4.0 
                ///// curve 4 

                double As14 = 4.0 * fcu * Math.Pow(10, -4) * b * t;
                double As24 = As14;
                double Astotal4 = As14 + As24;

                // first Point 
                double M14 = 0;
                double P14 = (0.35 * fcu * (Ac - Astotal4) + 0.67 * fy * Astotal4) / 1000;
                // second point 
                double P24 = (0.35 * fcu * (Ac - Astotal4) + 0.67 * fy * Astotal4) / 1000;
                double M24 = P24 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb4 = (600 * d) / (600 + 0.87 * fy);
                double ab4 = 0.8 * Cb4;
                double P34 = (0.45 * fcu * ab4 * b + 0.87 * fy * As24 - 0.87 * fy * As14) / 1000;
                double M34 = (0.45 * fcu * ab4 * b * ((t / 2) - (ab4 / 2)) + 0.87 * fy * As24 * ((t / 2) - cover) + 0.87 * fy * As14 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P44 = 0;
                double M44 = 0.87 * fy * As14 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M54 = 0;
                //double P54 = 0.87 * fy * Astotal4/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a64 = ab4 + (0.2 * t);        // mm 
                double C64 = 1.25 * a64;
                double epsilon64 = (0.003 * (d - C64)) / C64;
                double Fs14;
                if (epsilon64 >= ey)
                {
                    Fs14 = 0.87 * fy;
                }
                else
                {
                    Fs14 = epsilon64 * 2 * Math.Pow(10, 5);
                }
                double P64 = (0.45 * fcu * a64 * b + 0.87 * fy * As24 - Fs14 * As14) / 1000;
                double M64 = (0.45 * fcu * a64 * b * ((t / 2) - (a64 / 2)) + 0.87 * fy * As24 * ((t / 2) - cover) + Fs14 * As14 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a74 = ab4 - (0.2 * t);       //mm
                double C74 = 1.25 * a74;
                double epsilon74 = (0.003 * (C74 - cover)) / C74;
                double Fs24;
                if (epsilon74 >= ey)
                {
                    Fs24 = 0.87 * fy;
                }
                else
                {
                    Fs24 = epsilon74 * 2 * Math.Pow(10, 5);
                }
                double P74 = (0.45 * fcu * a74 * b + Fs24 * As24 - 0.87 * fy * As14) / 1000;
                double M74 = (0.45 * fcu * a74 * b * ((t / 2) - (a74 / 2)) + Fs24 * As24 * ((t / 2) - cover) + 0.87 * fy * As14 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P14fianl = (P14 * 1000) / (fcu * b * t);
                double P24fianl = (P24 * 1000) / (fcu * b * t);
                double P34fianl = (P34 * 1000) / (fcu * b * t);
                double P44fianl = (P44 * 1000) / (fcu * b * t);
                //double P54fianl = (P54 * 1000) / (fcu * b * t);
                double P64fianl = (P64 * 1000) / (fcu * b * t);
                double P74fianl = (P74 * 1000) / (fcu * b * t);

                double M14fianl = (M14 * 1000 * 1000) / (fcu * b * t * t);
                double M24fianl = (M24 * 1000 * 1000) / (fcu * b * t * t);
                double M34fianl = (M34 * 1000 * 1000) / (fcu * b * t * t);
                double M44fianl = (M44 * 1000 * 1000) / (fcu * b * t * t);
                //double M54fianl = (M54* 1000 * 1000) / (fcu * b * t * t);
                double M64fianl = (M64 * 1000 * 1000) / (fcu * b * t * t);
                double M74fianl = (M74 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D4"].Points.AddXY(M14fianl, P14fianl);
                this.chart1.Series["I.D4"].Points.AddXY(M24fianl, P24fianl);
                this.chart1.Series["I.D4"].Points.AddXY(M64fianl, P64fianl);
                this.chart1.Series["I.D4"].Points.AddXY(M34fianl, P34fianl);
                this.chart1.Series["I.D4"].Points.AddXY(M74fianl, P74fianl);
                this.chart1.Series["I.D4"].Points.AddXY(M44fianl, P44fianl);
                //this.chart1.Series["I.D4"].Points.AddXY(M54fianl, -1 * P54fianl);
                this.chart1.Series["I.D4"].Points.AddXY(0, 0);
                this.chart1.Series["I.D4"].Points.AddXY(M14fianl, P14fianl);




                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 5.0 
                ///// curve 5 

                double As15 = 5.0 * fcu * Math.Pow(10, -4) * b * t;
                double As25 =  As15;
                double Astotal5 = As15 + As25;

                // first Point 
                double M15 = 0;
                double P15 = (0.35 * fcu * (Ac - Astotal5) + 0.67 * fy * Astotal5) / 1000;
                // second point 
                double P25 = (0.35 * fcu * (Ac - Astotal5) + 0.67 * fy * Astotal5) / 1000;
                double M25 = P25 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb5 = (600 * d) / (600 + 0.87 * fy);
                double ab5 = 0.8 * Cb5;
                double P35 = (0.45 * fcu * ab5 * b + 0.87 * fy * As25 - 0.87 * fy * As15) / 1000;
                double M35 = (0.45 * fcu * ab5 * b * ((t / 2) - (ab5 / 2)) + 0.87 * fy * As25 * ((t / 2) - cover) + 0.87 * fy * As15 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P45 = 0;
                double M45 = 0.87 * fy * As15 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M55 = 0;
                //double P55 = 0.87 * fy * Astotal5/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a65 = ab5 + (0.2 * t);        // mm 
                double C65 = 1.25 * a65;
                double epsilon65 = (0.003 * (d - C65)) / C65;
                double Fs15;
                if (epsilon65 >= ey)
                {
                    Fs15 = 0.87 * fy;
                }
                else
                {
                    Fs15 = epsilon65 * 2 * Math.Pow(10, 5);
                }
                double P65 = (0.45 * fcu * a65 * b + 0.87 * fy * As25 - Fs15 * As15) / 1000;
                double M65 = (0.45 * fcu * a65 * b * ((t / 2) - (a65 / 2)) + 0.87 * fy * As25 * ((t / 2) - cover) + Fs15 * As15 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a75 = ab5 - (0.2 * t);       //mm
                double C75 = 1.25 * a75;
                double epsilon75 = (0.003 * (C75 - cover)) / C75;
                double Fs25;
                if (epsilon75 >= ey)
                {
                    Fs25 = 0.87 * fy;
                }
                else
                {
                    Fs25 = epsilon75 * 2 * Math.Pow(10, 5);
                }
                double P75 = (0.45 * fcu * a75 * b + Fs25 * As25 - 0.87 * fy * As15) / 1000;
                double M75 = (0.45 * fcu * a75 * b * ((t / 2) - (a75 / 2)) + Fs25 * As25 * ((t / 2) - cover) + 0.87 * fy * As15 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P15fianl = (P15 * 1000) / (fcu * b * t);
                double P25fianl = (P25 * 1000) / (fcu * b * t);
                double P35fianl = (P35 * 1000) / (fcu * b * t);
                double P45fianl = (P45 * 1000) / (fcu * b * t);
                //double P55fianl = (P55 * 1000) / (fcu * b * t);
                double P65fianl = (P65 * 1000) / (fcu * b * t);
                double P75fianl = (P75 * 1000) / (fcu * b * t);

                double M15fianl = (M15 * 1000 * 1000) / (fcu * b * t * t);
                double M25fianl = (M25 * 1000 * 1000) / (fcu * b * t * t);
                double M35fianl = (M35 * 1000 * 1000) / (fcu * b * t * t);
                double M45fianl = (M45 * 1000 * 1000) / (fcu * b * t * t);
                //double M55fianl = (M55* 1000 * 1000) / (fcu * b * t * t);
                double M65fianl = (M65 * 1000 * 1000) / (fcu * b * t * t);
                double M75fianl = (M75 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D5"].Points.AddXY(M15fianl, P15fianl);
                this.chart1.Series["I.D5"].Points.AddXY(M25fianl, P25fianl);
                this.chart1.Series["I.D5"].Points.AddXY(M65fianl, P65fianl);
                this.chart1.Series["I.D5"].Points.AddXY(M35fianl, P35fianl);
                this.chart1.Series["I.D5"].Points.AddXY(M75fianl, P75fianl);
                this.chart1.Series["I.D5"].Points.AddXY(M45fianl, P45fianl);
                //this.chart1.Series["I.D5"].Points.AddXY(M55fianl, -1 * P55fianl);
                this.chart1.Series["I.D5"].Points.AddXY(0, 0);
                this.chart1.Series["I.D5"].Points.AddXY(M15fianl, P15fianl);








                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 6.0 
                ///// curve 6 

                double As16 = 6.0 * fcu * Math.Pow(10, -4) * b * t;
                double As26 =  As16;
                double Astotal6 = As16 + As26;

                // first Point 
                double M16 = 0;
                double P16 = (0.35 * fcu * (Ac - Astotal6) + 0.67 * fy * Astotal6) / 1000;
                // second point 
                double P26 = (0.35 * fcu * (Ac - Astotal6) + 0.67 * fy * Astotal6) / 1000;
                double M26 = P26 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb6 = (600 * d) / (600 + 0.87 * fy);
                double ab6 = 0.8 * Cb6;
                double P36 = (0.45 * fcu * ab6 * b + 0.87 * fy * As26 - 0.87 * fy * As16) / 1000;
                double M36 = (0.45 * fcu * ab6 * b * ((t / 2) - (ab6 / 2)) + 0.87 * fy * As26 * ((t / 2) - cover) + 0.87 * fy * As16 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P46 = 0;
                double M46 = 0.87 * fy * As16 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M56 = 0;
                //double P56 = 0.87 * fy * Astotal6/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a66 = ab6 + (0.2 * t);        // mm 
                double C66 = 1.25 * a66;
                double epsilon66 = (0.003 * (d - C66)) / C66;
                double Fs16;
                if (epsilon66 >= ey)
                {
                    Fs16 = 0.87 * fy;
                }
                else
                {
                    Fs16 = epsilon66 * 2 * Math.Pow(10, 5);
                }
                double P66 = (0.45 * fcu * a66 * b + 0.87 * fy * As26 - Fs16 * As16) / 1000;
                double M66 = (0.45 * fcu * a66 * b * ((t / 2) - (a66 / 2)) + 0.87 * fy * As26 * ((t / 2) - cover) + Fs16 * As16 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a76 = ab6 - (0.2 * t);       //mm
                double C76 = 1.25 * a76;
                double epsilon76 = (0.003 * (C76 - cover)) / C76;
                double Fs26;
                if (epsilon76 >= ey)
                {
                    Fs26 = 0.87 * fy;
                }
                else
                {
                    Fs26 = epsilon76 * 2 * Math.Pow(10, 5);
                }
                double P76 = (0.45 * fcu * a76 * b + Fs26 * As26 - 0.87 * fy * As16) / 1000;
                double M76 = (0.45 * fcu * a76 * b * ((t / 2) - (a76 / 2)) + Fs26 * As26 * ((t / 2) - cover) + 0.87 * fy * As16 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P16fianl = (P16 * 1000) / (fcu * b * t);
                double P26fianl = (P26 * 1000) / (fcu * b * t);
                double P36fianl = (P36 * 1000) / (fcu * b * t);
                double P46fianl = (P46 * 1000) / (fcu * b * t);
                //double P56fianl = (P56 * 1000) / (fcu * b * t);
                double P66fianl = (P66 * 1000) / (fcu * b * t);
                double P76fianl = (P76 * 1000) / (fcu * b * t);

                double M16fianl = (M16 * 1000 * 1000) / (fcu * b * t * t);
                double M26fianl = (M26 * 1000 * 1000) / (fcu * b * t * t);
                double M36fianl = (M36 * 1000 * 1000) / (fcu * b * t * t);
                double M46fianl = (M46 * 1000 * 1000) / (fcu * b * t * t);
                //double M56fianl = (M56* 1000 * 1000) / (fcu * b * t * t);
                double M66fianl = (M66 * 1000 * 1000) / (fcu * b * t * t);
                double M76fianl = (M76 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D6"].Points.AddXY(M16fianl, P16fianl);
                this.chart1.Series["I.D6"].Points.AddXY(M26fianl, P26fianl);
                this.chart1.Series["I.D6"].Points.AddXY(M66fianl, P66fianl);
                this.chart1.Series["I.D6"].Points.AddXY(M36fianl, P36fianl);
                this.chart1.Series["I.D6"].Points.AddXY(M76fianl, P76fianl);
                this.chart1.Series["I.D6"].Points.AddXY(M46fianl, P46fianl);
                //this.chart1.Series["I.D6"].Points.AddXY(M56fianl, -1 * P56fianl);
                this.chart1.Series["I.D6"].Points.AddXY(0, 0);
                this.chart1.Series["I.D6"].Points.AddXY(M16fianl, P16fianl);







                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro = 7.0 
                ///// curve 7 

                double As17 = 7.0 * fcu * Math.Pow(10, -4) * b * t;
                double As27 = As17;
                double Astotal7 = As17 + As27;

                // first Point 
                double M17 = 0;
                double P17 = (0.35 * fcu * (Ac - Astotal7) + 0.67 * fy * Astotal7) / 1000;
                // second point 
                double P27 = (0.35 * fcu * (Ac - Astotal7) + 0.67 * fy * Astotal7) / 1000;
                double M27 = P27 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb7 = (600 * d) / (600 + 0.87 * fy);
                double ab7 = 0.8 * Cb7;
                double P37 = (0.45 * fcu * ab7 * b + 0.87 * fy * As27 - 0.87 * fy * As17) / 1000;
                double M37 = (0.45 * fcu * ab7 * b * ((t / 2) - (ab7 / 2)) + 0.87 * fy * As27 * ((t / 2) - cover) + 0.87 * fy * As17 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P47 = 0;
                double M47 = 0.87 * fy * As17 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M57 = 0;
                //double P57 = 0.87 * fy * Astotal7/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a67 = ab7 + (0.2 * t);        // mm 
                double C67 = 1.25 * a67;
                double epsilon67 = (0.003 * (d - C67)) / C67;
                double Fs17;
                if (epsilon67 >= ey)
                {
                    Fs17 = 0.87 * fy;
                }
                else
                {
                    Fs17 = epsilon67 * 2 * Math.Pow(10, 5);
                }
                double P67 = (0.45 * fcu * a67 * b + 0.87 * fy * As27 - Fs17 * As17) / 1000;
                double M67 = (0.45 * fcu * a67 * b * ((t / 2) - (a67 / 2)) + 0.87 * fy * As27 * ((t / 2) - cover) + Fs17 * As17 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a77 = ab7 - (0.2 * t);       //mm
                double C77 = 1.25 * a77;
                double epsilon77 = (0.003 * (C77 - cover)) / C77;
                double Fs27;
                if (epsilon77 >= ey)
                {
                    Fs27 = 0.87 * fy;
                }
                else
                {
                    Fs27 = epsilon77 * 2 * Math.Pow(10, 5);
                }
                double P77 = (0.45 * fcu * a77 * b + Fs27 * As27 - 0.87 * fy * As17) / 1000;
                double M77 = (0.45 * fcu * a77 * b * ((t / 2) - (a77 / 2)) + Fs27 * As27 * ((t / 2) - cover) + 0.87 * fy * As17 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P17fianl = (P17 * 1000) / (fcu * b * t);
                double P27fianl = (P27 * 1000) / (fcu * b * t);
                double P37fianl = (P37 * 1000) / (fcu * b * t);
                double P47fianl = (P47 * 1000) / (fcu * b * t);
                //double P57fianl = (P57 * 1000) / (fcu * b * t);
                double P67fianl = (P67 * 1000) / (fcu * b * t);
                double P77fianl = (P77 * 1000) / (fcu * b * t);

                double M17fianl = (M17 * 1000 * 1000) / (fcu * b * t * t);
                double M27fianl = (M27 * 1000 * 1000) / (fcu * b * t * t);
                double M37fianl = (M37 * 1000 * 1000) / (fcu * b * t * t);
                double M47fianl = (M47 * 1000 * 1000) / (fcu * b * t * t);
                //double M57fianl = (M57* 1000 * 1000) / (fcu * b * t * t);
                double M67fianl = (M67 * 1000 * 1000) / (fcu * b * t * t);
                double M77fianl = (M77 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D7"].Points.AddXY(M17fianl, P17fianl);
                this.chart1.Series["I.D7"].Points.AddXY(M27fianl, P27fianl);
                this.chart1.Series["I.D7"].Points.AddXY(M67fianl, P67fianl);
                this.chart1.Series["I.D7"].Points.AddXY(M37fianl, P37fianl);
                this.chart1.Series["I.D7"].Points.AddXY(M77fianl, P77fianl);
                this.chart1.Series["I.D7"].Points.AddXY(M47fianl, P47fianl);
                //this.chart1.Series["I.D7"].Points.AddXY(M57fianl, -1 * P57fianl);
                this.chart1.Series["I.D7"].Points.AddXY(0, 0);
                this.chart1.Series["I.D7"].Points.AddXY(M17fianl, P17fianl);



                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro =8.0 
                ///// curve 8 

                double As18 = 8.0 * fcu * Math.Pow(10, -4) * b * t;
                double As28 =  As18;
                double Astotal8 = As18 + As28;

                // first Point 
                double M18 = 0;
                double P18 = (0.35 * fcu * (Ac - Astotal8) + 0.67 * fy * Astotal8) / 1000;
                // second point 
                double P28 = (0.35 * fcu * (Ac - Astotal8) + 0.67 * fy * Astotal8) / 1000;
                double M28 = P28 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb8 = (600 * d) / (600 + 0.87 * fy);
                double ab8 = 0.8 * Cb8;
                double P38 = (0.45 * fcu * ab8 * b + 0.87 * fy * As28 - 0.87 * fy * As18) / 1000;
                double M38 = (0.45 * fcu * ab8 * b * ((t / 2) - (ab8 / 2)) + 0.87 * fy * As28 * ((t / 2) - cover) + 0.87 * fy * As18 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P48 = 0;
                double M48 = 0.87 * fy * As18 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M58 = 0;
                //double P58 = 0.87 * fy * Astotal8/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a68 = ab8 + (0.2 * t);        // mm 
                double C68 = 1.25 * a68;
                double epsilon68 = (0.003 * (d - C68)) / C68;
                double Fs18;
                if (epsilon68 >= ey)
                {
                    Fs18 = 0.87 * fy;
                }
                else
                {
                    Fs18 = epsilon68 * 2 * Math.Pow(10, 5);
                }
                double P68 = (0.45 * fcu * a68 * b + 0.87 * fy * As28 - Fs18 * As18) / 1000;
                double M68 = (0.45 * fcu * a68 * b * ((t / 2) - (a68 / 2)) + 0.87 * fy * As28 * ((t / 2) - cover) + Fs18 * As18 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a78 = ab8 - (0.2 * t);       //mm
                double C78 = 1.25 * a78;
                double epsilon78 = (0.003 * (C78 - cover)) / C78;
                double Fs28;
                if (epsilon78 >= ey)
                {
                    Fs28 = 0.87 * fy;
                }
                else
                {
                    Fs28 = epsilon78 * 2 * Math.Pow(10, 5);
                }
                double P78 = (0.45 * fcu * a78 * b + Fs28 * As28 - 0.87 * fy * As18) / 1000;
                double M78 = (0.45 * fcu * a78 * b * ((t / 2) - (a78 / 2)) + Fs28 * As28 * ((t / 2) - cover) + 0.87 * fy * As18 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P18fianl = (P18 * 1000) / (fcu * b * t);
                double P28fianl = (P28 * 1000) / (fcu * b * t);
                double P38fianl = (P38 * 1000) / (fcu * b * t);
                double P48fianl = (P48 * 1000) / (fcu * b * t);
                //double P58fianl = (P58 * 1000) / (fcu * b * t);
                double P68fianl = (P68 * 1000) / (fcu * b * t);
                double P78fianl = (P78 * 1000) / (fcu * b * t);

                double M18fianl = (M18 * 1000 * 1000) / (fcu * b * t * t);
                double M28fianl = (M28 * 1000 * 1000) / (fcu * b * t * t);
                double M38fianl = (M38 * 1000 * 1000) / (fcu * b * t * t);
                double M48fianl = (M48 * 1000 * 1000) / (fcu * b * t * t);
                //double M58fianl = (M58* 1000 * 1000) / (fcu * b * t * t);
                double M68fianl = (M68 * 1000 * 1000) / (fcu * b * t * t);
                double M78fianl = (M78 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D8"].Points.AddXY(M18fianl, P18fianl);
                this.chart1.Series["I.D8"].Points.AddXY(M28fianl, P28fianl);
                this.chart1.Series["I.D8"].Points.AddXY(M68fianl, P68fianl);
                this.chart1.Series["I.D8"].Points.AddXY(M38fianl, P38fianl);
                this.chart1.Series["I.D8"].Points.AddXY(M78fianl, P78fianl);
                this.chart1.Series["I.D8"].Points.AddXY(M48fianl, P48fianl);
                //this.chart1.Series["I.D8"].Points.AddXY(M58fianl, -1 * P58fianl);
                this.chart1.Series["I.D8"].Points.AddXY(0, 0);
                this.chart1.Series["I.D8"].Points.AddXY(M18fianl, P18fianl);



                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro =9.0 
                ///// curve 9 

                double As19 = 9.0 * fcu * Math.Pow(10, -4) * b * t;
                double As29 = As19;
                double Astotal9 = As19 + As29;

                // first Point 
                double M19 = 0;
                double P19 = (0.35 * fcu * (Ac - Astotal9) + 0.67 * fy * Astotal9) / 1000;
                // second point 
                double P29 = (0.35 * fcu * (Ac - Astotal9) + 0.67 * fy * Astotal9) / 1000;
                double M29 = P29 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb9 = (600 * d) / (600 + 0.87 * fy);
                double ab9 = 0.8 * Cb9;
                double P39 = (0.45 * fcu * ab9 * b + 0.87 * fy * As29 - 0.87 * fy * As19) / 1000;
                double M39 = (0.45 * fcu * ab9 * b * ((t / 2) - (ab9 / 2)) + 0.87 * fy * As29 * ((t / 2) - cover) + 0.87 * fy * As19 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P49 = 0;
                double M49 = 0.87 * fy * As19 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M59 = 0;
                //double P59 = 0.87 * fy * Astotal9/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a69 = ab9 + (0.2 * t);        // mm 
                double C69 = 1.25 * a69;
                double epsilon69 = (0.003 * (d - C69)) / C69;
                double Fs19;
                if (epsilon69 >= ey)
                {
                    Fs19 = 0.87 * fy;
                }
                else
                {
                    Fs19 = epsilon69 * 2 * Math.Pow(10, 5);
                }
                double P69 = (0.45 * fcu * a69 * b + 0.87 * fy * As29 - Fs19 * As19) / 1000;
                double M69 = (0.45 * fcu * a69 * b * ((t / 2) - (a69 / 2)) + 0.87 * fy * As29 * ((t / 2) - cover) + Fs19 * As19 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a79 = ab9 - (0.2 * t);       //mm
                double C79 = 1.25 * a79;
                double epsilon79 = (0.003 * (C79 - cover)) / C79;
                double Fs29;
                if (epsilon79 >= ey)
                {
                    Fs29 = 0.87 * fy;
                }
                else
                {
                    Fs29 = epsilon79 * 2 * Math.Pow(10, 5);
                }
                double P79 = (0.45 * fcu * a79 * b + Fs29 * As29 - 0.87 * fy * As19) / 1000;
                double M79 = (0.45 * fcu * a79 * b * ((t / 2) - (a79 / 2)) + Fs29 * As29 * ((t / 2) - cover) + 0.87 * fy * As19 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P19fianl = (P19 * 1000) / (fcu * b * t);
                double P29fianl = (P29 * 1000) / (fcu * b * t);
                double P39fianl = (P39 * 1000) / (fcu * b * t);
                double P49fianl = (P49 * 1000) / (fcu * b * t);
                //double P59fianl = (P59 * 1000) / (fcu * b * t);
                double P69fianl = (P69 * 1000) / (fcu * b * t);
                double P79fianl = (P79 * 1000) / (fcu * b * t);

                double M19fianl = (M19 * 1000 * 1000) / (fcu * b * t * t);
                double M29fianl = (M29 * 1000 * 1000) / (fcu * b * t * t);
                double M39fianl = (M39 * 1000 * 1000) / (fcu * b * t * t);
                double M49fianl = (M49 * 1000 * 1000) / (fcu * b * t * t);
                //double M58fianl = (M59* 1000 * 1000) / (fcu * b * t * t);
                double M69fianl = (M69 * 1000 * 1000) / (fcu * b * t * t);
                double M79fianl = (M79 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D9"].Points.AddXY(M19fianl, P19fianl);
                this.chart1.Series["I.D9"].Points.AddXY(M29fianl, P29fianl);
                this.chart1.Series["I.D9"].Points.AddXY(M69fianl, P69fianl);
                this.chart1.Series["I.D9"].Points.AddXY(M39fianl, P39fianl);
                this.chart1.Series["I.D9"].Points.AddXY(M79fianl, P79fianl);
                this.chart1.Series["I.D9"].Points.AddXY(M49fianl, P49fianl);
                //this.chart1.Series["I.D9"].Points.AddXY(M59fianl, -1 * P59fianl);
                this.chart1.Series["I.D9"].Points.AddXY(0, 0);
                this.chart1.Series["I.D9"].Points.AddXY(M19fianl, P19fianl);








                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro =10.0 
                ///// curve 10 

                double As110 = 10 * fcu * Math.Pow(10, -4) * b * t;
                double As210 =  As110;
                double Astotal10 = As110 + As210;

                // first Point 
                double M110 = 0;
                double P110 = (0.35 * fcu * (Ac - Astotal10) + 0.67 * fy * Astotal10) / 1000;
                // second point 
                double P210 = (0.35 * fcu * (Ac - Astotal10) + 0.67 * fy * Astotal10) / 1000;
                double M210 = P210 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb10 = (600 * d) / (600 + 0.87 * fy);
                double ab10 = 0.8 * Cb10;
                double P310 = (0.45 * fcu * ab10 * b + 0.87 * fy * As210 - 0.87 * fy * As110) / 1000;
                double M310 = (0.45 * fcu * ab10 * b * ((t / 2) - (ab10 / 2)) + 0.87 * fy * As210 * ((t / 2) - cover) + 0.87 * fy * As110 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P410 = 0;
                double M410 = 0.87 * fy * As110 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M510 = 0;
                //double P510 = 0.87 * fy * Astotal10/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a610 = ab10 + (0.2 * t);        // mm 
                double C610 = 1.25 * a610;
                double epsilon610 = (0.003 * (d - C610)) / C610;
                double Fs110;
                if (epsilon610 >= ey)
                {
                    Fs110 = 0.87 * fy;
                }
                else
                {
                    Fs110 = epsilon610 * 2 * Math.Pow(10, 5);
                }
                double P610 = (0.45 * fcu * a610 * b + 0.87 * fy * As210 - Fs110 * As110) / 1000;
                double M610 = (0.45 * fcu * a610 * b * ((t / 2) - (a610 / 2)) + 0.87 * fy * As210 * ((t / 2) - cover) + Fs110 * As110 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a710 = ab10 - (0.2 * t);       //mm
                double C710 = 1.25 * a710;
                double epsilon710 = (0.003 * (C710 - cover)) / C710;
                double Fs210;
                if (epsilon710 >= ey)
                {
                    Fs210 = 0.87 * fy;
                }
                else
                {
                    Fs210 = epsilon710 * 2 * Math.Pow(10, 5);
                }
                double P710 = (0.45 * fcu * a710 * b + Fs210 * As210 - 0.87 * fy * As110) / 1000;
                double M710 = (0.45 * fcu * a710 * b * ((t / 2) - (a710 / 2)) + Fs210 * As210 * ((t / 2) - cover) + 0.87 * fy * As110 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P110fianl = (P110 * 1000) / (fcu * b * t);
                double P210fianl = (P210 * 1000) / (fcu * b * t);
                double P310fianl = (P310 * 1000) / (fcu * b * t);
                double P410fianl = (P410 * 1000) / (fcu * b * t);
                //double P510fianl = (P510 * 1000) / (fcu * b * t);
                double P610fianl = (P610 * 1000) / (fcu * b * t);
                double P710fianl = (P710 * 1000) / (fcu * b * t);

                double M110fianl = (M110 * 1000 * 1000) / (fcu * b * t * t);
                double M210fianl = (M210 * 1000 * 1000) / (fcu * b * t * t);
                double M310fianl = (M310 * 1000 * 1000) / (fcu * b * t * t);
                double M410fianl = (M410 * 1000 * 1000) / (fcu * b * t * t);
                //double M510fianl = (M510* 1000 * 1000) / (fcu * b * t * t);
                double M610fianl = (M610 * 1000 * 1000) / (fcu * b * t * t);
                double M710fianl = (M710 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D10"].Points.AddXY(M110fianl, P110fianl);
                this.chart1.Series["I.D10"].Points.AddXY(M210fianl, P210fianl);
                this.chart1.Series["I.D10"].Points.AddXY(M610fianl, P610fianl);
                this.chart1.Series["I.D10"].Points.AddXY(M310fianl, P310fianl);
                this.chart1.Series["I.D10"].Points.AddXY(M710fianl, P710fianl);
                this.chart1.Series["I.D10"].Points.AddXY(M410fianl, P410fianl);
                //this.chart1.Series["I.D10"].Points.AddXY(M510fianl, -1 * P510fianl);
                this.chart1.Series["I.D10"].Points.AddXY(0, 0);
                this.chart1.Series["I.D10"].Points.AddXY(M110fianl, P110fianl);



                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro =11.0 
                ///// curve 11 

                double As111 = 11 * fcu * Math.Pow(10, -4) * b * t;
                double As211 =  As111;
                double Astotal11 = As111 + As211;

                // first Point 
                double M111 = 0;
                double P111 = (0.35 * fcu * (Ac - Astotal11) + 0.67 * fy * Astotal11) / 1000;
                // second point 
                double P211 = (0.35 * fcu * (Ac - Astotal11) + 0.67 * fy * Astotal11) / 1000;
                double M211 = P211 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb11 = (600 * d) / (600 + 0.87 * fy);
                double ab11 = 0.8 * Cb11;
                double P311 = (0.45 * fcu * ab11 * b + 0.87 * fy * As211 - 0.87 * fy * As111) / 1000;
                double M311 = (0.45 * fcu * ab11 * b * ((t / 2) - (ab11 / 2)) + 0.87 * fy * As211 * ((t / 2) - cover) + 0.87 * fy * As111 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P411 = 0;
                double M411 = 0.87 * fy * As111 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M511 = 0;
                //double P511 = 0.87 * fy * Astotal11/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a611 = ab11 + (0.2 * t);        // mm 
                double C611 = 1.25 * a611;
                double epsilon611 = (0.003 * (d - C611)) / C611;
                double Fs111;
                if (epsilon611 >= ey)
                {
                    Fs111 = 0.87 * fy;
                }
                else
                {
                    Fs111 = epsilon611 * 2 * Math.Pow(10, 5);
                }
                double P611 = (0.45 * fcu * a611 * b + 0.87 * fy * As211 - Fs111 * As111) / 1000;
                double M611 = (0.45 * fcu * a611 * b * ((t / 2) - (a611 / 2)) + 0.87 * fy * As211 * ((t / 2) - cover) + Fs111 * As111 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a711 = ab11 - (0.2 * t);       //mm
                double C711 = 1.25 * a711;
                double epsilon711 = (0.003 * (C711 - cover)) / C711;
                double Fs211;
                if (epsilon711 >= ey)
                {
                    Fs211 = 0.87 * fy;
                }
                else
                {
                    Fs211 = epsilon711 * 2 * Math.Pow(10, 5);
                }
                double P711 = (0.45 * fcu * a711 * b + Fs211 * As211 - 0.87 * fy * As111) / 1000;
                double M711 = (0.45 * fcu * a711 * b * ((t / 2) - (a711 / 2)) + Fs211 * As211 * ((t / 2) - cover) + 0.87 * fy * As111 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P111fianl = (P111 * 1000) / (fcu * b * t);
                double P211fianl = (P211 * 1000) / (fcu * b * t);
                double P311fianl = (P311 * 1000) / (fcu * b * t);
                double P411fianl = (P411 * 1000) / (fcu * b * t);
                //double P511fianl = (P511 * 1000) / (fcu * b * t);
                double P611fianl = (P611 * 1000) / (fcu * b * t);
                double P711fianl = (P711 * 1000) / (fcu * b * t);

                double M111fianl = (M111 * 1000 * 1000) / (fcu * b * t * t);
                double M211fianl = (M211 * 1000 * 1000) / (fcu * b * t * t);
                double M311fianl = (M311 * 1000 * 1000) / (fcu * b * t * t);
                double M411fianl = (M411 * 1000 * 1000) / (fcu * b * t * t);
                //double M511fianl = (M511* 1000 * 1000) / (fcu * b * t * t);
                double M611fianl = (M611 * 1000 * 1000) / (fcu * b * t * t);
                double M711fianl = (M711 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D11"].Points.AddXY(M111fianl, P111fianl);
                this.chart1.Series["I.D11"].Points.AddXY(M211fianl, P211fianl);
                this.chart1.Series["I.D11"].Points.AddXY(M611fianl, P611fianl);
                this.chart1.Series["I.D11"].Points.AddXY(M311fianl, P311fianl);
                this.chart1.Series["I.D11"].Points.AddXY(M711fianl, P711fianl);
                this.chart1.Series["I.D11"].Points.AddXY(M411fianl, P411fianl);
                //this.chart1.Series["I.D11"].Points.AddXY(M511fianl, -1 * P511fianl);
                this.chart1.Series["I.D11"].Points.AddXY(0, 0);
                this.chart1.Series["I.D11"].Points.AddXY(M111fianl, P111fianl);



                /////////////////////////////////////////////////////////////////////////
                //// firstly Assume Ro =12.0 
                ///// curve 12 

                double As112 = 12 * fcu * Math.Pow(10, -4) * b * t;
                double As212 = As112;
                double Astotal12 = As112 + As212;

                // first Point 
                double M112 = 0;
                double P112 = (0.35 * fcu * (Ac - Astotal12) + 0.67 * fy * Astotal12) / 1000;
                // second point 
                double P212 = (0.35 * fcu * (Ac - Astotal12) + 0.67 * fy * Astotal12) / 1000;
                double M212 = P212 * 0.05 * t * 0.001;            //kN.m

                // point 3 
                // Assume Safety Factot == 1.5 & 1.15
                double Cb12 = (600 * d) / (600 + 0.87 * fy);
                double ab12 = 0.8 * Cb12;
                double P312 = (0.45 * fcu * ab12 * b + 0.87 * fy * As212 - 0.87 * fy * As112) / 1000;
                double M312 = (0.45 * fcu * ab12 * b * ((t / 2) - (ab12 / 2)) + 0.87 * fy * As212 * ((t / 2) - cover) + 0.87 * fy * As112 * ((t / 2) - cover)) / (1000 * 1000);

                // point 4 
                double P412 = 0;
                double M412 = 0.87 * fy * As112 * (d - cover) / (1000 * 1000);

                // point 5 
                //double M512 = 0;
                //double P512 = 0.87 * fy * Astotal12/1000;

                // point 6 Lies in Compression Zone

                // Assume a > ab 
                double a612 = ab12 + (0.2 * t);        // mm 
                double C612 = 1.25 * a612;
                double epsilon612 = (0.003 * (d - C612)) / C612;
                double Fs112;
                if (epsilon612 >= ey)
                {
                    Fs112 = 0.87 * fy;
                }
                else
                {
                    Fs112 = epsilon612 * 2 * Math.Pow(10, 5);
                }
                double P612 = (0.45 * fcu * a612 * b + 0.87 * fy * As212 - Fs112 * As112) / 1000;
                double M612 = (0.45 * fcu * a612 * b * ((t / 2) - (a612 / 2)) + 0.87 * fy * As212 * ((t / 2) - cover) + Fs112 * As112 * ((t / 2) - cover)) / (1000 * 1000);

                //// poin0t 7 Lies in Tension Zone 
                double a712 = ab12 - (0.2 * t);       //mm
                double C712 = 1.25 * a712;
                double epsilon712 = (0.003 * (C712 - cover)) / C712;
                double Fs212;
                if (epsilon712 >= ey)
                {
                    Fs212 = 0.87 * fy;
                }
                else
                {
                    Fs212 = epsilon712 * 2 * Math.Pow(10, 5);
                }
                double P712 = (0.45 * fcu * a712 * b + Fs212 * As212 - 0.87 * fy * As112) / 1000;
                double M712 = (0.45 * fcu * a712 * b * ((t / 2) - (a712 / 2)) + Fs212 * As212 * ((t / 2) - cover) + 0.87 * fy * As112 * ((t / 2) - cover)) / (1000 * 1000);


                /////////// 
                /// Draw Curve 
                double P112fianl = (P112 * 1000) / (fcu * b * t);
                double P212fianl = (P212 * 1000) / (fcu * b * t);
                double P312fianl = (P312 * 1000) / (fcu * b * t);
                double P412fianl = (P412 * 1000) / (fcu * b * t);
                //double P512fianl = (P512 * 1000) / (fcu * b * t);
                double P612fianl = (P612 * 1000) / (fcu * b * t);
                double P712fianl = (P712 * 1000) / (fcu * b * t);

                double M112fianl = (M112 * 1000 * 1000) / (fcu * b * t * t);
                double M212fianl = (M212 * 1000 * 1000) / (fcu * b * t * t);
                double M312fianl = (M312 * 1000 * 1000) / (fcu * b * t * t);
                double M412fianl = (M412 * 1000 * 1000) / (fcu * b * t * t);
                //double M512fianl = (M512* 1000 * 1000) / (fcu * b * t * t);
                double M612fianl = (M612 * 1000 * 1000) / (fcu * b * t * t);
                double M712fianl = (M712 * 1000 * 1000) / (fcu * b * t * t);

                this.chart1.Series["I.D12"].Points.AddXY(M112fianl, P112fianl);
                this.chart1.Series["I.D12"].Points.AddXY(M212fianl, P212fianl);
                this.chart1.Series["I.D12"].Points.AddXY(M612fianl, P612fianl);
                this.chart1.Series["I.D12"].Points.AddXY(M312fianl, P312fianl);
                this.chart1.Series["I.D12"].Points.AddXY(M712fianl, P712fianl);
                this.chart1.Series["I.D12"].Points.AddXY(M412fianl, P412fianl);
                //this.chart1.Series["I.D12"].Points.AddXY(M512fianl, -1 * P512fianl);
                this.chart1.Series["I.D12"].Points.AddXY(0, 0);
                this.chart1.Series["I.D12"].Points.AddXY(M112fianl, P112fianl);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (txtro1.Text.Trim() == "")
            {
                Close();
            }

            else
            {
                if (double.Parse(txtro1.Text) < 1.0)
                {
                    MessageBox.Show("minimum Value of ρ = 1.0", "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    txtro1.Focus();
                    txtro1.SelectAll();
                    return;
                }
                else
                {
                    Close();
                }
            }

        }
    }
}
