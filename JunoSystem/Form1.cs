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

        public frmJunoSistema()
        {
            InitializeComponent();
            
        }

        private void frmJunoSistema_Load(object sender, EventArgs e)
        {
            timer1.Enabled = true;

            ServiceDriveAPI objDriveApi = new ServiceDriveAPI();

            //Buscar Credenciais
            LCredenciais = objDriveApi.Autenticar();

            //AbreServiço
            LServico = objDriveApi.AbrirServico(LCredenciais);

            if (LCredenciais == null || LServico == null) lblStatus.Text = "Conectado";

            //Comentar para funcionar na apresentação
            timer1_Tick(sender, e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            ServiceDriveAPI objDriveApi = new ServiceDriveAPI();

            //Realiza o download para realizar a sincronização com banco de dados
            objDriveApi.Download(LServico, "Juno.csv", "C:\\Juno\\juno.csv");
            if (objDriveApi.msgErro == "Nenhum Registro") return;

            //CriaLista
            List<Importacao> lstImportacao = new List<Importacao>();          
            string[] PLinhas; //array que vai receber todas as linhas do arquivo
            try
            {
                PLinhas = File.ReadAllLines("C:\\Juno\\Juno.csv");
            }
            catch (Exception)
            {
                MessageBox.Show("Erro ao Baixar Dados dos servidores da Nasa");
                return;
            }

            try
            {
                string[] PCamposLinha;  
                foreach (string UmaLinha in PLinhas) //lendo desde 1a linha..não manter cabeçalho no .csv
                {
                    //Despresa cabeçalho
                    if (UmaLinha == "latitude,longitude,brightness,scan,track,acq_date,acq_time,satellite,confidence,version,bright_t31,frp,day_night") continue;

                    if (UmaLinha.Replace(" ", "").Length == 0) continue;
                    PCamposLinha = UmaLinha.Split(','); //divide a linha em um array

                    //validar Código
                    PCamposLinha[0] = PCamposLinha[0].Trim();

                    string Sql = "INSERT INTO Importacao (Long, Lat, Brilho) ";
                           Sql = Sql + "VALUES(" + PCamposLinha[0].ToString() + ", "+ PCamposLinha[1].ToString() + ", " + PCamposLinha[2].ToString() + ")";

                    Conexoes objConexoes = new Conexoes();
                    objConexoes.ExecutaComando(Sql);

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar arquivo.\n" + ex.Message, "Qualify - Comercial", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            finally
            {
                DateTime objData = new DateTime();
                string PDataHora =  objData.Day.ToString() + "-" + objData.Month.ToString() + "-" + objData.Hour.ToString() + "-" + objData.Minute.ToString();

                //Realiza o download para realizar a sincronização com banco de dados
                objDriveApi.DeletarItem(LServico, "Juno.csv");
                System.IO.File.Delete("C:\\Juno\\Juno.csv");
            }
        }

        private void radInserir_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Inserir Device";
            txtEmail.ReadOnly = true;

        }

        private void radAlterar_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Alterar Device";
            txtEmail.ReadOnly = true;
        }

        private void radSolicitar_Click(object sender, EventArgs e)
        {
            btnInserir.Text = "Solicitar Device";
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
                    MessageBox.Show("Dados cadastrais atualizado");
                }
                else
                {
                    MessageBox.Show("Existe Campos não Preenchidos");
                }
                
            }
            else if (radInserir.Checked)
            {
                if (txtDevice.Text != "" && txtDevice.Text != "" && txtLongitude.Text != "")
                {
                    objLatLong.Inserir(txtDevice.Text, txtLatitude.Text, txtLongitude.Text);
                    MessageBox.Show("Dados Gravados co suecesso");
                }
                else
                {
                    MessageBox.Show("Existe Campos não Preenchidos");
                }
               
            }
            else
            {
                if (txtDevice.Text != "" && txtDevice.Text != "" && txtLongitude.Text != "" && txtEmail.Text != "")
                {
                    objLatLong.Solicitar(txtDevice.Text, txtLatitude.Text, txtLongitude.Text,txtEmail.Text);
                    MessageBox.Show("Dados Gravados co suecesso");
                }
                else
                {
                    MessageBox.Show("Existe Campos não Preenchidos");
                }
            }
        }

        public void DownloadBodyParts()
        {
            using (var client = new ImapClient())
            {
                client.Connect("imap.gmail.com", 993, SecureSocketOptions.SslOnConnect);

                client.Authenticate("juno.spaceapp@gmail.com", "nasainjupter");

                client.Inbox.Open(FolderAccess.ReadOnly);

                // search for messages where the Subject header contains either "MimeKit" or "MailKit"
                var query = SearchQuery.SubjectContains("FIRMS").Or(SearchQuery.SubjectContains("FIRMS"));
                var uids = client.Inbox.Search(query);

                // fetch summary information for the search results (we will want the UID and the BODYSTRUCTURE
                // of each message so that we can extract the text body and the attachments)
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

                            var path = Path.Combine(directory, fileName);

                            // decode and save the content to a file
                            using (var stream = File.Create(path))
                                part.ContentObject.DecodeTo(stream);
                        }
                    }
                }

                client.Disconnect(true);
            }
        }
    }
}
