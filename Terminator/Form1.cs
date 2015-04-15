using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Terminator.Properties;

namespace Terminator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            GenerateTimerButtons();
        }

        private void GenerateTimerButtons()
        {
            //intervals in minutes
            List<double> intervals = new List<double>();
            
            //interval opetions (in minutes)
            intervals.Add(30);
            intervals.Add(60);
            intervals.Add(90);
            intervals.Add(120);
            intervals.Add(180);
            intervals.Add(240);

            //initial offset for the buttons (in relativ pixels)
            int BtnYaxis = 1;

            //generate the desired buttons
            foreach (double Ctr in intervals)
            {
                Button HibernateBtn = new Button();

                HibernateBtn.Location = new Point(45, 60 * BtnYaxis + 30);
                HibernateBtn.Size = new System.Drawing.Size(150, 45);
                HibernateBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                HibernateBtn.UseVisualStyleBackColor = true;
                HibernateBtn.Click += new EventHandler(HibernateHandler);
                HibernateBtn.Tag = (Ctr * 60 * 1000).ToString(); //convert minutes to milliseconds.
                HibernateBtn.Text = ConvertMinToHourText(Ctr);
                Controls.Add(HibernateBtn);

                Button ShutdownBtn = new Button();

                ShutdownBtn.Location = new Point(350, 60 * BtnYaxis + 30);
                ShutdownBtn.Size = new System.Drawing.Size(150, 45);
                ShutdownBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                ShutdownBtn.UseVisualStyleBackColor = true;
                ShutdownBtn.Click += new EventHandler(ShutdownHandler);
                ShutdownBtn.Tag = (Ctr * 60 * 1000).ToString(); //convert minutes to milliseconds.
                ShutdownBtn.Text = ConvertMinToHourText(Ctr);
                Controls.Add(ShutdownBtn);

                BtnYaxis++;
            }

        }

        private string ConvertMinToHourText(double Minutes)
        {
            if (Minutes < 60)
                return Minutes + " mins";
            else
            {
                return (Minutes / 60).ToString("0.##") + " hrs";
            }
        }

        private async void HibernateHandler(object sender, EventArgs e)
        {
            DisableControls();

            Button button = sender as Button;
            button.BackColor = Color.YellowGreen;
            
            int Delay = int.Parse(button.Tag.ToString());
            await Task.Delay(Delay);
            Process.Start("shutdown", "/h");
        }

        private async void ShutdownHandler(object sender, EventArgs e)
        {
            DisableControls();

            Button button = sender as Button;            
            button.BackColor = System.Drawing.Color.Crimson;
            button.ForeColor = System.Drawing.SystemColors.ButtonFace;
                       

            int Delay = int.Parse(button.Tag.ToString());
            await Task.Delay(Delay);
            Process.Start("shutdown", "/s");
        }


        private void DisableControls()
        {
            foreach (Control c in Controls)
            {
                Button b = c as Button;
                if (b != null)
                {
                    b.BackColor = System.Drawing.SystemColors.ButtonShadow;
                    b.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
                    b.Enabled = false;
                }
            }

        }

        private void EnableControls(Control con)
        {
            if (con != null)
            {
                con.Enabled = true;
                EnableControls(con.Parent);
            }
        }
          
     
    }
}
