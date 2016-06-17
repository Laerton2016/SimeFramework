using SIME.Class.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using simeFramework.Class.primitivo;
using System.Data.OleDb;
using SIME;

namespace simeFramework.Class.DAO
{
    public class DAOFornecedor : IDAO<NetForncedor>
    {
        private DAOContatoFornecedor _daoContato;

        public DAOFornecedor()
        {
            _daoContato = FactoryDAO.CriaDAOContatoForncedor();
        }
        /// <summary>
        /// Busca pelo ID do fornecedor
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public NetForncedor Buscar(long id)
        {
            String SQL = "Select * from forncedores where cod = " + id + ";";
            return BuscaGeneric(SQL);

        }
        /// <summary>
        /// Busca pelo nome do fornecedor
        /// </summary>
        /// <param name="Fornecedor"></param>
        /// <returns></returns>
        public NetForncedor BuscaPorNome(String Fornecedor)
        {
            String SQL = "Select * from forncedores where fornecedor = " + Fornecedor + ";";
            return BuscaGeneric(SQL);

        }
        /// <summary>
        /// Busca pelo CNPJ do fornecedor
        /// </summary>
        /// <param name="CNPJ"></param>
        /// <returns></returns>
        public NetForncedor BuscaPorCNPJ(String CNPJ)
        {
            String SQL = "Select * from forncedores where CNPJ = " + CNPJ + ";";
            return BuscaGeneric(SQL);

        }
        /// <summary>
        /// Busca pelo termo SQL repasado como atributo
        /// </summary>
        /// <param name="sQL"></param>
        /// <returns></returns>
        private NetForncedor BuscaGeneric(string sQL)
        {
            NetForncedor retorno = new NetForncedorNull();
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbCommand command = new OleDbCommand(sQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    retorno = MontaFornecedor(dr);
                }
                dr.Close();
            }
            return retorno;
        }
        /// <summary>
        /// Monta um Fornecedor pelo DR passado como parametro
        /// </summary>
        /// <param name="dr">Data reader</param>
        /// <returns>Fornecedor montado</returns>
        private NetForncedor MontaFornecedor(OleDbDataReader dr)
        {
            NetForncedor retorno = new NetForncedor();
            retorno.Id = Int32.Parse(dr["cod"].ToString());
            retorno.IE = dr["estadual"].ToString();
            retorno.Nome = dr["Fornecedor"].ToString();
            retorno.Numero = Int32.Parse(dr["Numero"].ToString());
            retorno.Razao = dr["razão"].ToString();
            retorno.UF = dr["estado"].ToString();
            retorno.Bairro = dr["bairro"].ToString();
            retorno.CEP = dr["CEP"].ToString();
            retorno.Cidade = dr["cidade"].ToString();
            retorno.CNPJ = dr["CNPJ"].ToString();
            retorno.Endereco = dr["end"].ToString();
            retorno.Contatos = _daoContato.BuscaContatos(Int32.Parse(dr["cod"].ToString()));
            return retorno;

        }
        /// <summary>
        /// Método exlui um fornecedor e seus contatos;
        /// </summary>
        /// <param name="t"></param>
        public void Excluir(NetForncedor t)
        {
            String SQL = "Delete from fornecedor where cod =" + t.Id + ";";
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbTransaction transa = connect.BeginTransaction();
                OleDbCommand command = new OleDbCommand (SQL, connect, transa);
                try
                {
                    command.ExecuteNonQuery();
                    foreach (var  item in t.Contatos)
                    {
                        _daoContato.Excluir(item, connect, transa);
                    }
                    transa.Commit();
                }
                catch (Exception erro)
                {
                    transa.Rollback();
                    throw new Exception("Erro DAOFornecedor Exclui - " + erro.Message);
                }
            }
        }
        /// <summary>
        /// Metodo salva um fornecedor e seus contatos
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public NetForncedor Salvar(NetForncedor t)
        {
            String SQL = "";
            if (t.Id == 0)
            {
                SQL = "Insert into fornecedor(fornecedor, razão, end, cidade, Estado, CEP, CNPJ, Estadual, Bairro, Numero) values(?,?,?,?,?,?,?,?,?,?);";
            }
            else
            {
                SQL = "Update Fornecedor set (fornecedor = ?, razão= ?, end= ?, cidade= ?, Estado= ?, CEP= ?, CNPJ= ?, Estadual= ?, Bairro= ?, Numero= ?) where cod = " + t.Id + ";";
            }
            return Persiste(t, SQL);
        }
        /// <summary>
        /// Persiste os dados de um fornecedor no banco de dados 
        /// </summary>
        /// <param name="t">Fornecedor</param>
        /// <param name="sQL">Instrnção SQL</param>
        /// <returns>Forncedor com os dados persistidos</returns>
        private NetForncedor Persiste(NetForncedor t, string sQL)
        {
            using (OleDbConnection connect = new Conexao().getContasnet())
            {
                connect.Open();
                OleDbTransaction transa = connect.BeginTransaction();
                OleDbCommand command = new OleDbCommand(sQL, connect, transa);
                command.Parameters.AddWithValue("@fornedor", t.Nome);
                command.Parameters.AddWithValue("@razão", t.Razao);
                command.Parameters.AddWithValue("@end", t.Endereco);
                command.Parameters.AddWithValue("@cidade", t.Cidade);
                command.Parameters.AddWithValue("@estado", t.UF);
                command.Parameters.AddWithValue("@cep", t.CEP);
                command.Parameters.AddWithValue("@cnpj", t.CNPJ);
                command.Parameters.AddWithValue("@Estadual", t.IE);
                command.Parameters.AddWithValue("@Bairro", t.Bairro);
                command.Parameters.AddWithValue("@numero", t.Numero);
                if (t.Id != 0)
                {
                    command.Parameters.AddWithValue("@cod", t.Id);
                }

                try
                {
                    command.ExecuteNonQuery();
                    if (t.Id == 0)
                    {
                        String SQL1 = "SELECT LAST_INSERT_ID() as ID from fornecedores;";
                        command.CommandText = SQL1;
                        OleDbDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            t.Id = Int32.Parse(dr["ID"].ToString());
                        }
                        dr.Close();
                    }
                    PersisteConatos(t, connect, transa);
                    transa.Commit();
                }
                catch (Exception erro)
                {
                    transa.Rollback();
                    throw new Exception("Erro DAOForcedores Persiste - " + erro.Message);
                }


            }
            return t;
        }
        /// <summary>
        /// Método persiste os dados de um contato de um forncedor e exlui os contatos que foram removidos 
        /// </summary>
        /// <param name="t">Forncedor</param>
        /// <param name="connect">Conexão com o banco de dados</param>
        /// <param name="transa">Transação com o banco de dados</param>
        private void PersisteConatos(NetForncedor t, OleDbConnection connect, OleDbTransaction transa)
        {
            List<NetContatosFornecedor> contatos = _daoContato.BuscaContatos(t.Id);
            if (contatos.Count > 0)// caso de contatos excluidos
            {
                foreach (var item in t.Contatos)
                {
                    contatos.Remove(item);
                }

                if (contatos.Count > 0)//Verificando se sobrou algum contato para exluir
                {
                    foreach (var item in contatos)
                    {
                        _daoContato.Excluir(item, connect, transa);
                    }
                }
            }
            //Salva os contatos;
            foreach (NetContatosFornecedor item in t.Contatos)
            {
                if (item.Cod_fornecedor == 0) { item.Cod_fornecedor = t.Id; }
                 _daoContato.Salvar(item, connect, transa);
            }
            
        }
    }
}
