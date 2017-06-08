using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using JunoSystem.CamadaBanco;
using JunoSystem.CamadaNegocio;
using MimeKit;
using MailKit;
using MailKit.Search;
using MailKit.Security;
using MailKit.Net.Imap;

namespace JunoSystem
{
    public partial class frmJunoSistema : Form
    {       
        Google.Apis.Auth.OAuth2.UserCredential LCredenciais;
        Google.Apis.Drive.v3.DriveService LServico;
        string LGoogleDriveOption = "0";

        public frmJunoSistema()
        {
            InitializeComponent();            
        }

        private void frmJunoSistema_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            if (JunoSystem.Properties.Settings.Default.GoogleDriveOption != "")
                LGoogleDriveOption = JunoSystem.Properties.Settings.Default.GoogleDriveOption.ToString();

            if (LGoogleDriveOption != "0")
            {
                ServiceDriveAPI objDriveApi = new ServiceDriveAPI();

                //Seach Credenciais
                LCredenciais = objDriveApi.Autenticar();

                //OpenService
                LServico = objDriveApi.AbrirServico(LCredenciais);

                if (LCredenciais == null || LServico == null) lblStatus.Text = "Connected";
            }
            
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            

            ServiceDriveAPI objDriveApi = new ServiceDriveAPI();

            if (LGoogleDriveOption != "0")
            { 
                //Download to perform database synchronization
                objDriveApi.Download(LServico, "Juno.csv", "C:\\JunoRadio\\juno.csv");
                if (objDriveApi.msgErro == "Nenhum Registro") return;
            }

            if (JunoSystem.Properties.Settings.Default.GoogleDriveOption.ToString() != "")
            {
                bool SSL = false;

                if (JunoSystem.Properties.Settings.Default.SSL.ToString() == "1") SSL = true;

                Downloadattachment(JunoSystem.Properties.Settings.Default.Server.ToString(),
                    Convert.ToInt32(JunoSystem.Properties.Settings.Default.Port.ToString()),
                    JunoSystem.Properties.Settings.Default.Email.ToString(),
                    JunoSystem.Properties.Settings.Default.Password.ToString(),
                    SSL);
            }

            //Create List
            List<Importacao> lstImportacao = new List<Importacao>();          
            string[] PLinhas;             try
            {
                PLinhas = File.ReadAllLines("C:\\JunoRadio\\Juno.csv");
            }
            catch (Exception)
            {
                MessageBox.Show("Error downloading data from Nasa servers");
                return;
            }

            try
            {
                string[] PCamposLinha;  
                foreach (string UmaLinha in PLinhas)
                {
                    
                    if (UmaLinha == "latitude,longitude,brightness,scan,track,acq_date,acq_time,satellite,confidence,version,bright_t31,frp,day_night") continue;

                    if (UmaLinha.Replace(" ", "").Length == 0) continue;
                    PCamposLinha = UmaLinha.Split(','); 
                   
                    PCamposLinha[0] = PCamposLinha[0].Trim();

                    string Sql = "INSERT INTO Importacao (Long, Lat, Brilho) ";
                           Sql = Sql + "VALUES(" + PCamposLinha[0].ToString() + ", "+ PCamposLinha[1].ToString() + ", " + PCamposLinha[2].ToString() + ")";

                    Conexoes objConexoes = new Conexoes();
                    objConexoes.ExecutaComando(Sql);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading file.\n" + ex.Message, "Juno", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                DateTime objData = new DateTime();
                string PDataHora =  objData.Day.ToString() + "-" + objData.Month.ToString() + "-" + objData.Hour.ToString() + "-" + objData.Minute.ToString();

                if (LGoogleDriveOption != "0")
                {
                    //download and Synchronization with database
                    objDriveApi.DeletarItem(LServico, "Juno.csv");
                }
                System.IO.File.Delete("C:\\JunoRadio\\Juno.csv");
            }
        }

        private void radInserir_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Insert Device";
            txtEmail.ReadOnly = true;

        }

        private void radAlterar_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Alter Device";
            txtEmail.ReadOnly = true;
        }

        private void radSolicitar_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Request Device";
            txtEmail.ReadOnly = false;
        }

        private void btnInserir_Click(object sender, EventArgs e)
        {
            LatLong objLatLong = new LatLong();
            if (radAlterar.Checked)
            {

                if (txtDevice.Text != "" && txtDevice.Text != "" && txtLongitude.Text != "")
                {
                    objLatLong.Updade(txtDevice.Text, txtLatitude.Text, txtLongitude.Text);
                    MessageBox.Show("Updated data");
                }
                else
                {
                    MessageBox.Show("There are fields not filled");
                }
                
            }
            else if (radInserir.Checked)
            {
                if (txtDevice.Text != "" && txtDevice.Text != "" && txtLongitude.Text != "")
                {
                    objLatLong.Inserir(txtDevice.Text, txtLatitude.Text, txtLongitude.Text);
                    MessageBox.Show("Updated data");
                }
                else
                {
                    MessageBox.Show("There are fields not filled");
                }
               
            }
            else
            {
                if (txtDevice.Text != "" && txtDevice.Text != "" && txtLongitude.Text != "" && txtEmail.Text != "")
                {
                    objLatLong.Solicitar(txtDevice.Text, txtLatitude.Text, txtLongitude.Text,txtEmail.Text);
                    MessageBox.Show("Updated data");
                }
                else
                {
                    MessageBox.Show("There are fields not filled");
                }
            }
        }

        public void Downloadattachment(string Server, int Port,string Email, string Pass, bool SSL)
        {
            using (var client = new ImapClient())
            {
                //SSL Test
                if (SSL == true)
                {
                    client.Connect(Server, Port, SecureSocketOptions.SslOnConnect);
                }
                else
                {
                    client.Connect(Server, Port, SecureSocketOptions.None);
                }                

                //authenticate
                client.Authenticate(Email, Pass);

                //Open Email
                client.Inbox.Open(FolderAccess.ReadOnly);

                //Search email content FIRMS (email NASA)
                var query = SearchQuery.SubjectContains("FIRMS").Or(SearchQuery.SubjectContains("FIRMS"));
                var uids = client.Inbox.Search(query);

                
                var items = client.Inbox.Fetch(uids, MessageSummaryItems.UniqueId | MessageSummaryItems.BodyStructure);

                foreach (var item in items)
                {
                    // determine a directory to save stuff in
                    var directory = Path.Combine(@"C:\JunoRadio\");

                    // create the directory
                    Directory.CreateDirectory(directory);

                    foreach (var attachment in item.Attachments)
                    {
                        // download the attachment just like we did with the body
                        var entity = client.Inbox.GetBodyPart(item.UniqueId, attachment);


                        if (entity is MessagePart)
                        {
                            var rfc822 = (MessagePart)entity;

                            var path = Path.Combine(directory, attachment.PartSpecifier + ".eml");

                            rfc822.Message.WriteTo(path);
                        }
                        else
                        {
                            var part = (MimePart)entity;

                            // note: it's possible for this to be null, but most will specify a filename
                            var fileName = part.FileName;

                            var path = Path.Combine(directory, "Juno.csv");

                            // decode and save the content to a file
                            using (var stream = File.Create(path))
                                part.ContentObject.DecodeTo(stream);
                        }
                    }
                }

                client.Disconnect(true);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            RegisterEmail objFormEmail = new RegisterEmail();
            objFormEmail.ShowDialog();
        }

        private void chkGoogleDrive_Click(object sender, EventArgs e)
        {
            if(chkGoogleDrive.Checked) JunoSystem.Properties.Settings.Default.GoogleDriveOption = "1";
            else JunoSystem.Properties.Settings.Default.GoogleDriveOption = "0";
            JunoSystem.Properties.Settings.Default.Save();
        }
    }
}
