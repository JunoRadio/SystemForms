using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JunoSystem.CamadaNegocio
{
    class ServiceDriveAPI
    {

        public string msgErro { get; set; }
        public string fdNomeItem { get; set; }


        /// <summary>
        /// Solicita permissao do usuario
        /// </summary>
        /// <returns></returns>
        public Google.Apis.Auth.OAuth2.UserCredential Autenticar()
        {
            msgErro = "";
            Google.Apis.Auth.OAuth2.UserCredential credenciais;

            using (var stream = new System.IO.FileStream("client_id.json", System.IO.FileMode.Open, System.IO.FileAccess.Read))
            {
                var diretorioAtual = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
                var diretorioCredenciais = System.IO.Path.Combine(diretorioAtual, "credential");

                credenciais = Google.Apis.Auth.OAuth2.GoogleWebAuthorizationBroker.AuthorizeAsync(
                    Google.Apis.Auth.OAuth2.GoogleClientSecrets.Load(stream).Secrets,
                    new[] { Google.Apis.Drive.v3.DriveService.Scope.Drive },
                    "user",
                    System.Threading.CancellationToken.None,
                    new Google.Apis.Util.Store.FileDataStore(diretorioCredenciais, true)).Result;
            }
            return credenciais;
        }

        /// <summary>
        /// Abre serviço para comunicação com API google Drive
        /// </summary>
        /// <param name="credenciais"></param>
        /// <returns></returns>
        public Google.Apis.Drive.v3.DriveService AbrirServico(Google.Apis.Auth.OAuth2.UserCredential credenciais)
        {
            msgErro = "";
            return new Google.Apis.Drive.v3.DriveService(new Google.Apis.Services.BaseClientService.Initializer()
            {
                HttpClientInitializer = credenciais
            });
        }

        /// <summary>
        /// Procurar arquivo pelo nome e retornar as ID
        /// </summary>        
        /// <returns> Retorna as Ids</returns>
        public string[] ProcurarArquivoId(Google.Apis.Drive.v3.DriveService servico, string nomeArquivo, bool procurarNaLixeira = false)
        {
            msgErro = "";
            var retorno = new List<string>();

            var request = servico.Files.List();
            request.Q = string.Format("name = '{0}'", nomeArquivo);
            if (!procurarNaLixeira)
            {
                request.Q += " and trashed = false";
            }
            request.Fields = "files(id)";
            var resultado = request.Execute();
            var arquivos = resultado.Files;

            if (arquivos != null && arquivos.Any())
            {
                foreach (var arquivo in arquivos)
                {
                    retorno.Add(arquivo.Id);
                }
            }
            return retorno.ToArray();
        }

        /// <summary>
        /// Deletar Item
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="nome"></param>
        public void DeletarItem(Google.Apis.Drive.v3.DriveService servico, string nome)
        {
            msgErro = "";
            var ids = ProcurarArquivoId(servico, nome);
            if (ids != null && ids.Any())
            {
                foreach (var id in ids)
                {
                    try
                    {
                        var request = servico.Files.Delete(id);
                        request.Execute();
                    }
                    catch (Exception)
                    {
                        msgErro = "Erro o Excluir Arquivo (Permissão, Arquivo Inexiste!";
                    }
                }
            }
        }


        /// <summary>
        /// Download Google drive
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="nome"></param>
        /// <param name="destino"></param>
        public void Download(Google.Apis.Drive.v3.DriveService servico, string nome, string destino)
        {
            msgErro = "";
            var ids = ProcurarArquivoId(servico, nome);
            if (ids != null && ids.Any())
            {
                var request = servico.Files.Get(ids.First());
                using (var stream = new System.IO.FileStream(destino, System.IO.FileMode.Create, System.IO.FileAccess.Write))
                {
                    request.Download(stream);
                }
            }
            else
            {
                msgErro = "Nenhum Registro";
            }

        }

        /// <summary>
        /// Enviar arquivo para lixeira, não vai excluir por meio de segurança. caso o bakup que foi enviado não esteja correto, ele não
        /// </summary>
        /// <param name="servico"></param>
        /// <param name="nome"></param>
        public void MoverParaLixeira(Google.Apis.Drive.v3.DriveService servico, string nome)
        {
            msgErro = "";
            var ids = ProcurarArquivoId(servico, nome);
            if (ids != null && ids.Any())
            {
                foreach (var id in ids)
                {
                    var arquivo = new Google.Apis.Drive.v3.Data.File();
                    arquivo.Trashed = true;
                    var request = servico.Files.Update(arquivo, id);
                    request.Execute();
                }
            }
        }

    }
}
