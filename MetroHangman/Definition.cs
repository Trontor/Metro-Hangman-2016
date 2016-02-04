using MetroFramework.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;

namespace MetroHangman
{
    public partial class Definition : MetroForm
    {
        string wrd;
        string definition = "Error 404: Definition not found. Click View Online to see the definition.";
        public Definition(string Word)
        {
            InitializeComponent();
            wrd = Word;

        }
        /// <summary>
        /// Gets the Definition of a Word.
        /// </summary>
        /// <param name="Word">The Word to get the Definition of.</param>
        /// <returns>The Word's Definition</returns>
        public void GetDefinition(String Word)
        {
            new Thread(() =>
                {

                    String def = "";

                    String SearchText = "<div class=\"def-content\">";
                    System.Net.WebClient client = new System.Net.WebClient();
                    String source = client.DownloadString("http://dictionary.reference.com/browse/" + Word);
                    //string source = client.DownloadString("http://www.dictionaryapi.com/api/v1/references/collegiate/xml/"+Word+"?key=d9e74f34-0d38-443a-abba-4b067d15c258");
                    Int32 start = source.IndexOf(SearchText) + SearchText.Length;
                    source = source.Remove(0, start);
                    Int32 end = source.IndexOf("</div>");
                    source = source.Remove(end);

                    def = source;
                    Invoke(new Action(() => { definition = def; ShowDef(); }));
                }).Start();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Process.Start("http://dictionary.reference.com/browse/" + wrd);
        }

        private void Definition_FormClosing(object sender, FormClosingEventArgs e)
        {
        }

        private void Definition_FormClosed(object sender, FormClosedEventArgs e)
        {

            foreach (Control c in Application.OpenForms[2].Controls)
            {
                c.Focus();
            }
            Application.OpenForms[2].BringToFront();
        }
        private void Definition_Load(object sender, EventArgs e)
        {
            GetDefinition(wrd);
        }
        private void ShowDef()
        {
            try
            {
                metroLabel1.Text = wrd;
                htmlPanel1.Text = "<center><font size=\"1\" face=\"Verdana\">" + definition.TrimStart().TrimEnd() + "</font></center>";
                spinner.Visible = false;
            }
            catch
            {
                MessageBox.Show("An unknown error has occured.");
                Process.Start("http://dictionary.reference.com/browse/" + wrd);
                Close();
            }
        }

    }
}
