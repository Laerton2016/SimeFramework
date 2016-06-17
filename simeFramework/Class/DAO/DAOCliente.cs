using SIME.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;

namespace SIME.Class.DAO
{
    public class DAOCliente : IDAO<NetCliente>
    {
        private DAOContatosCliente _daoContatos;
        private DAOInformacaoCliente _daoInformacao;
        private DAOIndicacoesCliente _daoIndicacoes;
        public NetCliente Buscar(long id)
        {
            NetCliente retoro = new NullNetCliente();
            String SQL = "Seletc * from clientes where cod_cliente = " + id + ";";
            using (OleDbConnection connect = new Conexao().getDB4net())
            {
                connect.Open();
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retoro = MontaCliente(dr);
                }
                dr.Close();
            }
            return retoro;
        }

        private NetCliente MontaCliente(OleDbDataReader dr)
        {
            NetCliente retorno = new NetCliente();
            retorno.Bairro = dr["bairro"].ToString();
            retorno.Cep = dr["Cep"].ToString();
            retorno.Cidade = dr["cidade"].ToString();
            retorno.Classificacao = dr["clasificação"].ToString();
            retorno.Cod = Int32.Parse(dr["cod_cliente"].ToString());
            retorno.Contatos = 
            return retorno;
            throw new NotImplementedException();
        }

        public void Excluir(NetCliente t)
        {
            throw new NotImplementedException();
        }

        public NetCliente Salvar(NetCliente t)
        {
            throw new NotImplementedException();
        }
    }
}
