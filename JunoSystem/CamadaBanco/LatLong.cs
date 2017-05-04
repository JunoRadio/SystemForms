using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;

namespace JunoSystem.CamadaBanco
{
    public class LatLong
    {
        public int IdDispositivo { get; set; }
        public int CodDispositivo { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string Data { get; set; }
        public string Hora { get; set; }

        public string Email { get; set; }

        public string Retorno;


        public List<LatLong> CarregarDados(string inCodDispositivo)
        {
            string strSQL = "";
            strSQL = "SELECT IdDispositivo, CodDispositivo, Latitude, Longitude, Data, Hora ";
            strSQL = strSQL + "FROM Juno ";
            strSQL = strSQL + "WHERE CodDispositivo ='" + inCodDispositivo + "' ";


            List<LatLong> ListaDispositivos = new List<LatLong>();
            Conexoes Conexao = new Conexoes();
            OleDbDataReader Dados = Conexao.ExecutaSELECT(strSQL);
            while (Dados.Read())
            {
                LatLong objLatlong = new LatLong();
                objLatlong.IdDispositivo = Convert.ToInt32(Dados["IdDispositivo"]);
                objLatlong.CodDispositivo = Convert.ToInt32(Dados["CodDispositivo"]);
                objLatlong.Latitude = Dados["Latitude"].ToString();
                objLatlong.Longitude = Dados["Longitude"].ToString();
                objLatlong.Data = Dados["Data"].ToString();
                objLatlong.Hora = Dados["Hora"].ToString();
                ListaDispositivos.Add(objLatlong);
            }
            Dados.Close();

            return ListaDispositivos;
        }

        public void Updade(string codDispositivo, string latitude, string longitude)
        {
            string strSQL = "UPDATE Juno ";
            strSQL = strSQL + "SET Longitude = '" + latitude + "', Latitude = '" + latitude + "' ";
            strSQL = strSQL + "WHERE CodDispositivo = '" + codDispositivo + "' ";           

            Conexoes Conexao = new Conexoes();
            Conexao.ExecutaComando(strSQL);
        }

        public void Inserir(string codDispositivo, string latitude, string longitude)
        {
            string strSQL = "INSERT INTO Juno(CodDispositivo, Longitude, Latitude)";
            strSQL = strSQL + "VALUES(" + codDispositivo + ", "+ longitude + ", " + latitude + ")";

            Conexoes Conexao = new Conexoes();
            Conexao.ExecutaComando(strSQL);
        }


        public void Solicitar(string codDispositivo, string latitude, string longitude, string Email)
        {
            string strSQL = "INSERT INTO Juno(CodDispositivo, Longitude, Latitude, Email)";
            strSQL = strSQL + "VALUES(" + codDispositivo + ", " + longitude + ", " + latitude  +", " + Email + ")";

            Conexoes Conexao = new Conexoes();
            Conexao.ExecutaComando(strSQL);
        }

    }
}
