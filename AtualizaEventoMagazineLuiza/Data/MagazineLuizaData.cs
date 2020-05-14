using AtualizaEventoMagazineLuiza.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtualizaEventoMagazineLuiza.Data
{
    public class MagazineLuizaData : IDisposable
    {

        #region Dispose
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        #endregion

        static string conexao = "Integrated Security=SSPI;Persist Security Info=False;User ID=ksjenger;Initial Catalog=Vipp;Data Source=SVS004\\HOMOLOGACAO";

        public static Pedidos BuscaEtiqueta(int ObservacaoUm)
        {
            Pedidos oPedido = new Pedidos();
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                connection.Open();
                SqlCommand command = null;

                string sql = "SELECT c.IdConhecimento, c.ObservacaoUm, c.NumeroNotaFiscal, c.Etiqueta FROM CONHECIMENTO C iNNER JOIN Atendimento A ON A.IdAtendimento = C.IdAtendimento WHERE A.IdEmpresa = 56077 AND ObservacaoUm = @ObservacaoUm";
                command = new SqlCommand(sql, connection);

                SqlParameter sqlObservacaoUm = new SqlParameter("@ObservacaoUm", SqlDbType.Int, 0);

                sqlObservacaoUm.Value = ObservacaoUm;
                command.Parameters.Add(sqlObservacaoUm);

                command.Prepare();
                SqlDataReader reader = command.ExecuteReader();



                if (!reader.HasRows)
                    return null;

                if (reader.Read())
                {
                    oPedido.Id = int.Parse(reader[0].ToString());
                    oPedido.Pedido = int.Parse(reader[1].ToString());
                    oPedido.NotaFiscal = int.Parse(reader[2].ToString());
                    oPedido.Etiqueta = reader[3].ToString();
                }
            }
            return oPedido;
        }

        public static Pedidos BuscaObservacaoUm(string Etiqueta)
        {
            Pedidos oPedido = new Pedidos();
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                connection.Open();
                SqlCommand command = null;

                string sql = "SELECT c.IdConhecimento, c.ObservacaoUm, c.NumeroNotaFiscal, c.Etiqueta FROM CONHECIMENTO C iNNER JOIN Atendimento A ON A.IdAtendimento = C.IdAtendimento WHERE A.IdEmpresa = 56077 AND Etiqueta = @Etiqueta";
                command = new SqlCommand(sql, connection);

                SqlParameter sqlEtiqueta = new SqlParameter("@Etiqueta", SqlDbType.VarChar, 13);

                sqlEtiqueta.Value = Etiqueta;
                command.Parameters.Add(sqlEtiqueta);

                command.Prepare();
                SqlDataReader reader = command.ExecuteReader();



                if (!reader.HasRows)
                    return null;

                if (reader.Read())
                {
                    oPedido.Id = int.Parse(reader[0].ToString());
                    oPedido.Pedido = int.Parse(reader[1].ToString());
                    oPedido.NotaFiscal = int.Parse(reader[2].ToString());
                    oPedido.Etiqueta = reader[3].ToString();
                }
            }
            return oPedido;
        }

        public static void EnviaPedidos(Pedidos oPedido)
        {
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                connection.Open();
                SqlDataReader rdr = null;

                try
                {
                    SqlCommand cmd = new SqlCommand("EventoRastreioManter");
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@IdConhecimento", Convert.ToInt32(oPedido.Id));
                    cmd.Parameters.AddWithValue("@DtEvento", Convert.ToDateTime(oPedido.DataOcorrencia));
                    cmd.Parameters.AddWithValue("@IdLocalEvento", Convert.ToInt32(11));
                    cmd.Parameters.AddWithValue("@idStatusEvento", Convert.ToInt32(oPedido.StatusEvento));

                    rdr = cmd.ExecuteReader();

                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public static byte[] MontaArquivoRetorno(MontarRelatorios oMontarRelatorio)
        {

            byte[] buffer = null;
            using (SqlConnection connection = new SqlConnection(conexao))
            {
                connection.Open();
                SqlDataReader rdr = null;
                string XmlFiltroPessoa = @"<Filtro><Item><Ctr>309</Ctr><Eps>56077</Eps></Item></Filtro>";
                int StFormatoRetorno = 10034;

                try
                {
                    SqlCommand cmd = new SqlCommand("MontarArquivoRetorno");
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@XmlFiltroPessoa", Convert.ToString(XmlFiltroPessoa));
                    cmd.Parameters.AddWithValue("@StFormatoRetorno", Convert.ToInt32(StFormatoRetorno));
                    cmd.Parameters.AddWithValue("@DtInicial", Convert.ToDateTime(oMontarRelatorio.DtInicial));
                    cmd.Parameters.AddWithValue("@DtFinal", Convert.ToDateTime(oMontarRelatorio.DtFinal));
                    cmd.Parameters.AddWithValue("@IdUnidadePostadora", Convert.ToInt32(oMontarRelatorio.IdUnidadePostadora));
                    cmd.Parameters.AddWithValue("@TicketFTP", Convert.ToInt32(0));

                    rdr = cmd.ExecuteReader();
                    StringBuilder builder = new StringBuilder();
                    while (rdr.Read())
                    {
                        builder.Append(rdr[0].ToString());
                        builder.Append("\n");
                    }

                    buffer = Encoding.ASCII.GetBytes(builder.ToString());
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return buffer;
        }

    }
}
