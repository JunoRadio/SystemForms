using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.OleDb;
using System.Data;


namespace JunoSystem.CamadaBanco
{
    class Conexoes
    {
        public string MsgErro { get; set; }

        public OleDbConnection ConexaoOLEDB = new OleDbConnection();

        public Conexoes()
        {
            ConexaoOLEDB.ConnectionString = @"Provider=SQLNCLI11.dll" + ";Server=DESKTOP-6MBN41D" + ";Database=Juno" + ";" +
                   "Initial Catalog=Juno" + ";User ID=usuariogeral" + ";Password=usuariogeral" + ";" +
                  "Persist Security Info = true;Connect Timeout=120";
            ConexaoOLEDB.Open();

            //Seta formato de data no SQL Server
            ExecutaSELECT("SET DATEFORMAT dmy");
        }

        public OleDbDataReader ExecutaSELECT(string strSQL)
        {
            OleDbCommand cmd = new OleDbCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = strSQL;
            cmd.Connection = ConexaoOLEDB;
            try
            {
                return cmd.ExecuteReader();
            }
            catch (Exception e)
            {
                MsgErro = e.Message;
            }
            return null;
        }

        public void ExecutaComando(string strSQL)
        {
            MsgErro = "";
            try
            {
                OleDbCommand cmd = new OleDbCommand();               
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = strSQL;
                cmd.Connection = ConexaoOLEDB;
                cmd.ExecuteNonQuery();
            }
            catch (Exception err)
            {
                MsgErro = err.Message;
            }
        }
    }
}
