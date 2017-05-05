using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JunoSystem
{
    public partial class RegisterEmail : Form
    {
        public RegisterEmail()
        {
            InitializeComponent();
        }

        private void RegisterEmail_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Types of supported mail server: IMAP");

            if (JunoSystem.Properties.Settings.Default.Email.ToString() != "")  txtEmail.Text = JunoSystem.Properties.Settings.Default.Email.ToString();
            if (JunoSystem.Properties.Settings.Default.Password.ToString() != "") txtPass.Text = JunoSystem.Properties.Settings.Default.Password.ToString();
            if (JunoSystem.Properties.Settings.Default.Port.ToString() != "") txtPort.Text = JunoSystem.Properties.Settings.Default.Port.ToString();
            if (JunoSystem.Properties.Settings.Default.Server.ToString() != "") txtServer.Text = JunoSystem.Properties.Settings.Default.Server.ToString();
            if(JunoSystem.Properties.Settings.Default.SSL.ToString() == "1") chkSSL.Checked = true;
            else chkSSL.Checked = false;            
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (txtEmail.Text.Contains("POP"))
            {
                MessageBox.Show("Unsupported mail server type");
                return;
            }

            JunoSystem.Properties.Settings.Default.Email = txtEmail.Text;
            JunoSystem.Properties.Settings.Default.Password = txtPass.Text;
            JunoSystem.Properties.Settings.Default.Port = txtPort.Text;
            JunoSystem.Properties.Settings.Default.Server = txtServer.Text;
            if (chkSSL.Checked) JunoSystem.Properties.Settings.Default.SSL = "1";
            else JunoSystem.Properties.Settings.Default.SSL = "0";

            JunoSystem.Properties.Settings.Default.Save();

            MessageBox.Show("Updated data");
            return;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
