using System.Data.OleDb;
using SIME.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.DAO
{
    public class DAOVenda : IDAO<NetVenda>
    {
        public NetVenda Buscar(long id)
        {
            string SQL = "Select * from cod_sai where cod_sai = " + id;
            return Busca(SQL);
        }

        private NetVenda Busca(String SQL)
        {
            using (OleDbConnection connect = NetConexao.Instance().GetSimeConnect())
            {
                connect.Open();
                NetVenda venda = new NetVenda();
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    venda = MontaVenda(dr);
                }
                dr.Close();
                return venda;
            }
        }

        private List<NetVenda> BuscaLista(String SQL)
        {
            using (OleDbConnection connect = NetConexao.Instance().GetSimeConnect())
            {
                connect.Open();
                List<NetVenda> retorno = new List<NetVenda>();

                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno.Add(MontaVenda(dr));
                }
                dr.Close();
                return retorno;
            }
        }
        private NetVenda MontaVenda(OleDbDataReader dr)
        {
            NetVenda retorno = new NetVenda();
            retorno.Cartao = float.Parse(dr["cartao"].ToString());
            retorno.Cheque = float.Parse(dr["cheque"].ToString());
            retorno.Date = DateTime.Parse(dr["data"].ToString());
            retorno.Especie = float.Parse(dr["especie"].ToString());
            retorno.Id = Int64.Parse(dr["cod_sai"].ToString());
            retorno.Id_caixa = Int64.Parse(dr["cx"].ToString());
            retorno.Id_cliente = Int64.Parse(dr["cod_cliente"].ToString());
            retorno.Id_operador = Int64.Parse(dr["op"].ToString());
            retorno.Vale = float.Parse(dr["vale"].ToString());
            return retorno;
            
        }
        /// <summary>
        /// Lista todas as vendas de um caixa
        /// </summary>
        /// <param name="id_caixa">id do caixa</param>
        /// <returns>Lista de vendas de um caixa</returns>
        public List<NetVenda> VendaCaixa(Int64 id_caixa)
        {
            String SQL = "Select * from cod_sai where cx = " + id_caixa;
            return BuscaLista(SQL);
        }

        /// <summary>
        /// Lista de todas as vendas de um cliente
        /// </summary>
        /// <param name="id_cliente">Id do cliente</param>
        /// <returns></returns>
        public List<NetVenda> VendaCliente(Int64 id_cliente)
        {
            String SQL = "Select * from cod_sai where cod_cliente = " + id_cliente;
            return BuscaLista(SQL);
        }
        /// <summary>
        /// Lista de todas as vendas realizadas por um operador em um entervalo de data
        /// </summary>
        /// <param name="id_operador">Id do operador</param>
        /// <param name="inicio">Data inicial da venda</param>
        /// <param name="fim">Data final da venda</param>
        /// <returns>Lista de vendas localizadas por um determinado periodo por um determinado operador</returns>
        public List<NetVenda> VendasOperador(Int64 id_operador, DateTime inicio, DateTime fim)
        {
            String SQL = "Select * from cod_sai where op =" + id_operador + " and data Between #" + inicio.ToString("dd/MM/yyyy") + "# And #" + fim.ToString("dd/MM/yyyy") + "#;";
            return BuscaLista(SQL);
        }
        /// <summary>
        /// Método lista todas as vendas de um determinado periodo
        /// </summary>
        /// <param name="inicio">Inicio do periodo das vendas</param>
        /// <param name="fim">Fim do periodo das vendas</param>
        /// <returns>Lsita de todas as vendasde um determinado periodo</returns>
        public List<NetVenda> VendasPeriodo(DateTime inicio, DateTime fim)
        {
            String SQL = "Select * from cod_sai where data Between #" + inicio.ToString("dd/MM/yyyy") + "# And #" + fim.ToString("dd/MM/yyyy") + "#;";
            return BuscaLista(SQL);
        }

        public List<NetVenda> VendasOperadorCarteira(Int64 id_operador, DateTime inicio, DateTime fim)
        {
            String SQL = "";
            return BuscaLista(SQL);
        }

        public void Excluir(NetVenda t)
        {
            throw new NotImplementedException();
        }

        public NetVenda Salvar(NetVenda t)
        {
            throw new NotImplementedException();
        }
    }
}
