using System.ComponentModel;
using MetroFramework.Controls;
using MetroFramework.Drawing.Html;

namespace MetroHangman
{
    partial class Definition
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.metroLabel1 = new MetroFramework.Controls.MetroLabel();
            this.def = new MetroFramework.Controls.MetroLabel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.htmlPanel1 = new MetroFramework.Drawing.Html.HtmlPanel();
            this.spinner = new MetroFramework.Controls.MetroProgressSpinner();
            this.htmlPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroLabel1
            // 
            this.metroLabel1.FontSize = MetroFramework.MetroLabelSize.Tall;
            this.metroLabel1.Location = new System.Drawing.Point(0, 28);
            this.metroLabel1.Name = "metroLabel1";
            this.metroLabel1.Size = new System.Drawing.Size(314, 39);
            this.metroLabel1.TabIndex = 0;
            this.metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // def
            // 
            this.def.Location = new System.Drawing.Point(13, 67);
            this.def.Name = "def";
            this.def.Size = new System.Drawing.Size(290, 125);
            this.def.TabIndex = 1;
            this.def.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroButton1
            // 
            this.metroButton1.Location = new System.Drawing.Point(111, 210);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(97, 23);
            this.metroButton1.TabIndex = 2;
            this.metroButton1.Text = "VIEW ONLINE";
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // htmlPanel1
            // 
            this.htmlPanel1.AutoScroll = true;
            this.htmlPanel1.AutoScrollMinSize = new System.Drawing.Size(290, 0);
            this.htmlPanel1.BackColor = System.Drawing.SystemColors.Window;
            this.htmlPanel1.Controls.Add(this.spinner);
            this.htmlPanel1.Font = new System.Drawing.Font("Segoe UI", 5.75F);
            this.htmlPanel1.Location = new System.Drawing.Point(13, 67);
            this.htmlPanel1.Name = "htmlPanel1";
            this.htmlPanel1.Size = new System.Drawing.Size(290, 125);
            this.htmlPanel1.TabIndex = 3;
            // 
            // spinner
            // 
            this.spinner.Location = new System.Drawing.Point(113, 30);
            this.spinner.Maximum = 100;
            this.spinner.Name = "spinner";
            this.spinner.Size = new System.Drawing.Size(64, 64);
            this.spinner.TabIndex = 0;
            this.spinner.Value = 75;
            // 
            // Definition
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(319, 245);
            this.Controls.Add(this.htmlPanel1);
            this.Controls.Add(this.metroButton1);
            this.Controls.Add(this.def);
            this.Controls.Add(this.metroLabel1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "Definition";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Definition_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Definition_FormClosed);
            this.Load += new System.EventHandler(this.Definition_Load);
            this.htmlPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroLabel metroLabel1;
        private MetroLabel def;
        private MetroButton metroButton1;
        private HtmlPanel htmlPanel1;
        private MetroProgressSpinner spinner;



    }
}