using System;
using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using MetroFramework.Forms;

namespace MetroHangman
{
    public partial class Definition : MetroForm
    {
        private readonly string _wrd;
        private string _definition = "Error 404: Definition not found. Click View Online to see the definition.";
        public Definition(string word)
        {
            InitializeComponent();
            _wrd = word;

        }
        /// <summary>
        /// Gets the Definition of a Word.
        /// </summary>
        /// <param name="word">The Word to get the Definition of.</param>
        /// <returns>The Word's Definition</returns>
        public void GetDefinition(string word)
        {
            new Thread(() =>
                {
                    string searchText = "<div class=\"def-content\">";
                    WebClient client = new WebClient();
                    string source = client.DownloadString("http://dictionary.reference.com/browse/" + word);
                    //string source = client.DownloadString("http://www.dictionaryapi.com/api/v1/references/collegiate/xml/"+Word+"?key=d9e74f34-0d38-443a-abba-4b067d15c258");
                    int start = source.IndexOf(searchText, StringComparison.Ordinal) + searchText.Length;
                    source = source.Remove(0, start);
                    int end = source.IndexOf("</div>", StringComparison.Ordinal);
                    source = source.Remove(end);

                    string definition = source;
                    Invoke(new Action(() => { _definition = definition; ShowDef(); }));
                }).Start();
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            Process.Start("http://dictionary.reference.com/browse/" + _wrd);
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
            GetDefinition(_wrd);
        }
        private void ShowDef()
        {
            try
            {
                metroLabel1.Text = _wrd;
                htmlPanel1.Text = "<center><font size=\"1\" face=\"Verdana\">" + _definition.TrimStart().TrimEnd() + "</font></center>";
                spinner.Visible = false;
            }
            catch
            {
                MessageBox.Show("An unknown error has occured.");
                Process.Start("http://dictionary.reference.com/browse/" + _wrd);
                Close();
            }
        }

    }
}
