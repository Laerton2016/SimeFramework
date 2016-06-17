using SIME.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace SIME.Class.DAO
{
    public class DAOIndicacoesCliente : IDAO<NetIndicacoesCliente>
    {
        public NetIndicacoesCliente Buscar(long id)
        {
            NetIndicacoesCliente retorno = null;
            String SQl = "Select * from Clientes_indica where id =" + id + ";";
            using (OleDbConnection connect = new Conexao().getDB4net())
            {
                connect.Open();
                OleDbCommand command = new OleDbCommand(SQl, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno = MontaIndica(dr);
                }
                dr.Close();
            }
            return retorno;
            
        }

        public List<NetIndicacoesCliente> BuscaIndicacoes(Int32 id_cliente)
        {
            List<NetIndicacoesCliente> retorno = new List<NetIndicacoesCliente>();
            String SQl = "Select * from Clientes_indica where cod_cliente =" + id_cliente + ";";
            using (OleDbConnection connect = new Conexao().getDB4net())
            {
                connect.Open();
                OleDbCommand command = new OleDbCommand(SQl, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno.Add(MontaIndica(dr));
                }
                dr.Close();
            }
            return retorno;
        }

        private NetIndicacoesCliente MontaIndica(OleDbDataReader dr)
        {
            return new NetIndicacoesCliente(Int32.Parse(dr["cod_cliente"].ToString()), Int32.Parse(dr["id"].ToString()), dr["tipo"].ToString(), dr["contato"].ToString(), dr["dado"].ToString());
            
        }

        public void Excluir(NetIndicacoesCliente t)
        {
            throw new NotImplementedException();
        }

        public NetIndicacoesCliente Salvar(NetIndicacoesCliente t)
        {
            throw new NotImplementedException();
        }
    }
}