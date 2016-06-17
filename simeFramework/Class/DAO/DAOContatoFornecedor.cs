using System;
using SIME.Class.DAO;
using simeFramework.Class.primitivo;
using System.Data.OleDb;
using SIME;
using System.Collections.Generic;

namespace simeFramework.Class.DAO
{
    /// <summary>
    /// Classe cuida da persistencia de daods de um contato de um fornecedor.
    /// <autor>Learton Marques de Figueiredo</autor>
    /// <data>05/06/2016</data>
    /// </summary>
    public class DAOContatoFornecedor : IDAO<NetContatosFornecedor>
    {
        public NetContatosFornecedor Buscar(long cod)
        {
            String SQL = "Select * from dados_forncedores where cod = " + cod + ";";
            NetContatosFornecedor retorno = new NetContatosFornecedor();
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno = MontaContato(dr);
                }
                dr.Close();
            }
            return retorno;
        }

        public List<NetContatosFornecedor> BuscaContatos(Int32 cod_forncedor)
        {
            String SQL = "Select * from dados_forncedores where cod_forncedor = " + cod_forncedor + ";";
            List<NetContatosFornecedor> retorno = new List<NetContatosFornecedor>();
            using (OleDbConnection connect = new Conexao().getContasnet())
            {

                connect.Open();
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno.Add(MontaContato(dr));
                }
                dr.Close();
            }
            return retorno;
        }

        public void Excluir(NetContatosFornecedor t)
        {
            String SQL = "delete from dados_forncedores where cod = " + t.Cod + ";";
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbTransaction transacao = connect.BeginTransaction();
                OleDbCommand command = new OleDbCommand(SQL, connect, transacao);
                try
                {
                    command.ExecuteNonQuery();
                    transacao.Commit();
                }
                catch (Exception erro)
                {
                    transacao.Rollback();
                    throw new Exception("Erro DAOContatoFornecedor excluir - " + erro.Message);
                }
            }

        }

        public NetContatosFornecedor Salvar(NetContatosFornecedor t)
        {
            string SQl = "";
            if (t.Cod == 0)
            {
                SQl = "Insert into dados_forcedores (cod_fornecedor, tipo, contato, dados) values (?,?,?,?)";
            }
            else
            {
                SQl = "Update dados_fornecedores set(cod_fornecedor = ? , tipo = ? , contato = ?, dados = ?) where cod = ?;";
            }
            return Persiste(t, SQl);
        }

        private NetContatosFornecedor Persiste(NetContatosFornecedor t, string SQL)
        {
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbTransaction transacao = connect.BeginTransaction();
                OleDbCommand command = new OleDbCommand(SQL, connect, transacao);
                command.Parameters.AddWithValue("@cod_forcedor", t.Cod_fornecedor);
                command.Parameters.AddWithValue("@tipo", t.Tipo);
                command.Parameters.AddWithValue("@contato", t.Contato);
                command.Parameters.AddWithValue("@dados", t.Dado);
                if (t.Cod != 0)
                {
                    command.Parameters.AddWithValue("@cod", t.Cod);
                }
                try
                {
                    command.ExecuteNonQuery();
                    transacao.Commit();
                    if (t.Cod == 0)
                    {
                        String SQL1 = "SELECT LAST_INSERT_ID() as ID from dados_fornecedores;";
                        command.CommandText = SQL1;
                        OleDbDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            t.Cod = Int32.Parse(dr["ID"].ToString());
                        }
                        dr.Close();
                    }


                }
                catch (Exception erro)
                {
                    transacao.Rollback();
                    throw new Exception("Erro DAOContatoForncedor Periste " + erro.Message);
                }

            }
            return t;
        }

        public void Excluir(NetContatosFornecedor item, OleDbConnection connect, OleDbTransaction transa)
        {
            String SQL = "delete from dados_forncedores where cod = " + item.Cod + ";";
            OleDbCommand command = new OleDbCommand(SQL, connect, transa);
            command.ExecuteNonQuery();
        }



        /// <summary>
        /// Monta um contato a partir dos dados de um Dataread
        /// </summary>
        /// <param name="dr"></param>
        /// <returns></returns>
        private NetContatosFornecedor MontaContato(OleDbDataReader dr)
        {
            NetContatosFornecedor retorno = new NetContatosFornecedor();
            retorno.Cod = Int32.Parse(dr["cod"].ToString());
            retorno.Cod_fornecedor = Int32.Parse(dr["cod_fornecedor"].ToString());
            retorno.Contato = dr["contato"].ToString();
            retorno.Dado = dr["dados"].ToString();
            return retorno;
        }

        internal NetContatosFornecedor Salvar(NetContatosFornecedor item, OleDbConnection connect, OleDbTransaction transa)
        {
            string SQl = "";
            if (item.Cod == 0)
            {
                SQl = "Insert into dados_forcedores (cod_fornecedor, tipo, contato, dados) values (?,?,?,?)";
            }
            else
            {
                SQl = "Update dados_fornecedores set(cod_fornecedor = ? , tipo = ? , contato = ?, dados = ?) where cod = ?;";
            }
            return Persiste(item, SQl, connect, transa);
        }

        private NetContatosFornecedor Persiste(NetContatosFornecedor t, string SQL, OleDbConnection connect, OleDbTransaction transacao)
        {
            OleDbCommand command = new OleDbCommand(SQL, connect, transacao);
            command.Parameters.AddWithValue("@cod_forcedor", t.Cod_fornecedor);
            command.Parameters.AddWithValue("@tipo", t.Tipo);
            command.Parameters.AddWithValue("@contato", t.Contato);
            command.Parameters.AddWithValue("@dados", t.Dado);
            if (t.Cod != 0)
            {
                command.Parameters.AddWithValue("@cod", t.Cod);
            }
            try
            {
                command.ExecuteNonQuery();
                transacao.Commit();
                if (t.Cod == 0)
                {
                    String SQL1 = "SELECT LAST_INSERT_ID() as ID from dados_fornecedores;";
                    command.CommandText = SQL1;
                    OleDbDataReader dr = command.ExecuteReader();
                    while (dr.Read())
                    {
                        t.Cod = Int32.Parse(dr["ID"].ToString());
                    }
                    dr.Close();
                }


            }
            catch (Exception erro)
            {
                transacao.Rollback();
                throw new Exception("Erro DAOContatoForncedor Periste " + erro.Message);
            }

            return t;
        }

    }
}
