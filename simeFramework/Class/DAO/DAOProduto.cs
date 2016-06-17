using SIME.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using SIME.Class.ProdutoClass;

namespace SIME.Class.DAO
{
    /// <summary>
    /// Classe persiste os dados no banco de dados Sime e Small
    /// </summary>
    public class DAOProduto : IDAO<NetProduto>
    {
        
        /// <summary>
        /// Método busca por um produto a partir do seu id
        /// </summary>
        /// <param name="id">Id do produto a ser pesquisado</param>
        /// <returns>Produto localizado, caso não encontre retorna null</returns>
        public NetProduto Buscar(long id)
        {
            String SQL = "Select * from produtos where cod = " + id;
            return BuscaporTermo(SQL);

        }

        /// <summary>
        /// Método efetua a busca de um Produto a partir de uma instrução SQL.
        /// </summary>
        /// <param name="SQL">Instrução de SQL</param>
        /// <returns>Produto localizado, caso não encontre retorna null</returns>
        private NetProduto BuscaporTermo(string SQL)
        {
            using (OleDbConnection connect = NetConexao.Instance().GetSimeConnect())
            {
                
                connect.Open();
                NetProduto produto = null;
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    produto = MontaProduto(dr);
                }
                return produto;
            }
        }
        /// <summary>
        /// Faz a busca de produtos a partir de um termo de sua descrição
        /// </summary>
        /// <param name="termo">Termo de busca</param>
        /// <returns>Lista de produtos localizados</returns>
        public List<NetProduto> Buscar(String termo)
        {
            return Buscar(termo, true);
        }

        /// <summary>
        /// Lista todos os produtos cadastrados no banco de dados
        /// </summary>
        /// <returns>Lista de produtos</returns>
        public List<NetProduto> AllProdutos()
        {
            String SQL = "Select * from produtos order by descrição;";
            return BuscaLista(SQL);
        }
        /// <summary>
        /// Lista todos os produtos filtrados por um tipo repassado como paramentro
        /// </summary>
        /// <param name="Tipo">Tipo de produto a ser filtrado</param>
        /// <returns>Lista de produtos localizado</returns>
        public List<NetProduto> AllProdutosTipo(Int32 Tipo)
        {
            String SQL = "Select * from produtos where tipo =" + Tipo + "order by descrição;";
            return BuscaLista(SQL);
        }

        /// <summary>
        /// Retorna uma lista de produtos descontinuados
        /// </summary>
        /// <returns>Lista de produtos descontinuados</returns>
        public List<NetProduto> ProdutosDescontinuados()
        {
            String SQL = "Select * from produtos where desc = 1 order by descrição;";
            return BuscaLista(SQL);
        }
        /// <summary>
        /// Retorna uma lista produtos cujo de estoque zero ou negativo.
        /// </summary>
        /// <returns>Lista de produtos cujo estoque é zero ou negativo</returns>
        public List<NetProduto> ProdutosZerados()
        {
            String SQL = "Select * from produtos where estoque <= 0 and desc = 0 order by descrição;";
            return BuscaLista(SQL);
        }

        /// <summary>
        /// Lista todos os produtos que apresenta um estoque maior que zero
        /// </summary>
        /// <returns>Lista produtos</returns>
        public List<NetProduto> AllProdutosEstoque()
        {
            String SQL = "Select * from produtos where estoque > 0 order by descrição;";
            return BuscaLista(SQL);
        }
        /// <summary>
        /// Lista todos os produtos filtrados por um termo que tenha estoque ou não estoque disponível.
        /// </summary>
        /// <param name="termo">Termo de filtro</param>
        /// <param name="estoque">Boolean que confirma se é com estoque ou não</param>
        /// <returns></returns>
        public List<NetProduto> Buscar(String termo, Boolean estoque)
        {
            String SQL = "";
            if (estoque)
            {
                SQL ="Select * from produtos where descrição like '%" + termo.Replace(' ', '%') + "'% order by descrição;";
            }
            else
            {
                SQL = "Select * from produtos where descrição like '%" + termo.Replace(' ', '%') + "'% and estoque > 0 order by descrição;";
            }
            return BuscaLista(SQL);
        }


        /// <summary>
        /// Busca um produto a partir de seu codigo de barras
        /// </summary>
        /// <param name="EAN">Código de barras EAN</param>
        /// <returns>Produto localizado, caso não localize retorna null</returns>
        public NetProduto BuscaEAN(String EAN)
        {
            String SQL = "Select * from produto where EAN = '" + EAN + "';";
            return BuscaporTermo(SQL);
        }

        /// <summary>
        /// Método que resulta a lista de produtos a partir de uma instrução SQL repassada como paramentro.
        /// </summary>
        /// <param name="SQL">Instrução SQL</param>
        /// <returns>Lista de produtos localizados pela lista</returns>
        private List<NetProduto> BuscaLista(string SQL)
        {
            using (OleDbConnection connect = NetConexao.Instance().GetContasConnect())
            {
                connect.Open();
                List<NetProduto> lista = new List<NetProduto>();
                
                OleDbCommand command = new OleDbCommand(SQL, connect);
                OleDbDataReader dr = command.ExecuteReader();
                while (dr.Read())
                {
                    lista.Add( MontaProduto(dr));
                }
                return lista;
            }
        }
        /// <summary>
        /// Método monta um produto a partir de um data reader repassado
        /// </summary>
        /// <param name="dr">Data reader contendo os dados</param>
        /// <returns>Produto com os dados preenchidos</returns>
        private NetProduto MontaProduto(OleDbDataReader dr)
        {
            NetProduto produto = new NetProduto();
            produto.ID = Int32.Parse(dr["cod"].ToString());
            produto.ICMSCusto = double.Parse(dr["icm de compra"].ToString());
            produto.Art33 = Boolean.Parse(dr["Art33"].ToString());
            produto.CodFabricante = dr["cod de fabricação"].ToString();
            produto.Complemento = dr["compatibilidade"].ToString();
            produto.Custo = Double.Parse(dr["custo"].ToString());
            produto.Descontinuado = Boolean.Parse(dr["desc"].ToString());
            produto.Descricao = dr["descrição"].ToString();
            produto.EAN = dr["codbarras"].ToString();
            produto.IdGrupo = Int32.Parse(dr["Tipo"].ToString());
            produto.IdMedida = Int32.Parse(dr["medida"].ToString());
            produto.IdRegra = Int32.Parse(dr["regra"].ToString());
            produto.Imagem = dr["imagem"].ToString();
            produto.NCM = dr["NCM"].ToString();
            produto.Peso = double.Parse(dr["peso"].ToString());
            produto.PoliticaVenda = dr["politica"].ToString();
            produto.QuantEstoque = Int32.Parse(dr["Estoque"].ToString());
            produto.QuantMinima = Int32.Parse(dr["mini"].ToString());
            produto.TaxaFrete = double.Parse(dr["taxa de frete"].ToString());
            produto.TaxaIPI = double.Parse(dr["IPI"].ToString());
            produto.TxComissao = double.Parse(dr["valor de venda grd"].ToString());
            produto.TxDesconto = double.Parse(dr["tx_desconto"].ToString());
            produto.TxLucroMaximo = double.Parse(dr["taxa de lucro grand"].ToString());
            produto.TxLucroMinimo = double.Parse(dr["TX_ATA_MAX"].ToString());
            produto.ValorVenda = double.Parse(dr["expr5"].ToString());
            produto.ValorVendaDesconto = double.Parse(dr["expr6"].ToString());
            return produto;
        }

        /// <summary>
        /// Exlui um produto da base de dados
        /// </summary>
        /// <param name="produto">Produto que será excluido do banco de dados</param>
        public void Excluir(NetProduto produto)
        {

            using (OleDbConnection connect = NetConexao.Instance().GetSimeConnect())
            {
                connect.Open();
                String SQL = "Delete from produtos where cod =" + produto.ID + ";";
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
                    throw new Exception(erro.Message);
                }
            }        }
        /// <summary>
        /// Salva os dados de um produto no banco de dados
        /// </summary>
        /// <param name="produto">Produto cujo dados serão salvos no banco de dados</param>
        /// <returns>Produto após os dados salvos</returns>
        public NetProduto Salvar(NetProduto produto)
        {
            String SQL = "";
            if (produto.ID == 0)
            {
                SQL = "Insert into produtos (art33, cod de fabricação, compatibilidade, custo, desc, descrição, codbarras,tipo, medida, regra, imagem, NCM, peso, politica, estoque, mini, taxa de frete, ipi, valor de venda grd, tx_desconto, taxa de lucro grand, tx_ATA_MAX, expr5, expr6, icm de compra ) values (?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?);";
                
            }
            else
            {
                SQL = "Update produtos set art33=?, cod de fabricação=?, compatibilidade=?, custo=?, desc=?, descrição=?, codbarras=? ,tipo=? , medida=? , regra=? , imagem=? , NCM=?, peso=?, politica=?, estoque=?, mini=?, taxa de frete=?, ipi=?, valor de venda grd=?, tx_desconto=?, taxa de lucro grand=?, tx_ATA_MAX=?, expr5=?, expr6=?, icm de compra = ? where cod = ?;";
            }
            return Persite(produto, SQL);
        }
        /// <summary>
        /// Persiste os dados de um produto no banco de dados 
        /// </summary>
        /// <param name="produto">Produto cujo dados serão persistidos</param>
        /// <param name="SQL">Instrunção SQL para persistencia</param>
        /// <returns>Produto após os dados persistidos.</returns>
        private NetProduto Persite(NetProduto produto, string SQL)
        {
            using (OleDbConnection connect = NetConexao.Instance().GetSimeConnect())
            {
                connect.Open();
                OleDbTransaction transacao = connect.BeginTransaction();
                OleDbCommand command = new OleDbCommand(SQL, connect, transacao);
                command.Parameters.AddWithValue("@art33", produto.Art33);
                command.Parameters.AddWithValue("@cod de fabricação", produto.CodFabricante);
                command.Parameters.AddWithValue("@compatibilidade", produto.Complemento);
                command.Parameters.AddWithValue("@custo", produto.Custo.ToString("N"));
                command.Parameters.AddWithValue("@desc", produto.Descontinuado);
                command.Parameters.AddWithValue("@descrição", produto.Descricao);
                command.Parameters.AddWithValue("@codbarras", produto.EAN);
                command.Parameters.AddWithValue("@Tipo", produto.IdGrupo);
                command.Parameters.AddWithValue("@medida", produto.IdMedida);
                command.Parameters.AddWithValue("@regra", produto.IdRegra);
                command.Parameters.AddWithValue("@imagem", produto.Imagem);
                command.Parameters.AddWithValue("@NCM", produto.NCM);
                command.Parameters.AddWithValue("@peso", produto.Peso.ToString("N"));
                command.Parameters.AddWithValue("@politica", produto.PoliticaVenda);
                command.Parameters.AddWithValue("@estoque", produto.QuantEstoque);
                command.Parameters.AddWithValue("@mini", produto.QuantMinima);
                command.Parameters.AddWithValue("@taxa de frete", produto.TaxaFrete);
                command.Parameters.AddWithValue("@IPI", produto.TaxaIPI);
                command.Parameters.AddWithValue("@valor de venda grd", produto.TxComissao);
                command.Parameters.AddWithValue("@tx_desconto", produto.TxDesconto);
                command.Parameters.AddWithValue("@taxa de lucro grand", produto.TxLucroMaximo);
                command.Parameters.AddWithValue("@TX_ATA_MAX", produto.TxLucroMinimo);
                command.Parameters.AddWithValue("@expr5", produto.ValorVenda);
                command.Parameters.AddWithValue("@expr6", produto.ValorVendaDesconto);
                command.Parameters.AddWithValue("@icm de compra", produto.ICMSCusto);
                if (produto.ID != 0)
                {
                    command.Parameters.AddWithValue("@cod", produto.ID);
                    if (produto.ID == 0)
                    {
                        SQL = "select max(cod) as Id from produtos;";
                        command.CommandText = SQL;
                        OleDbDataReader dr = command.ExecuteReader();
                        while (dr.Read())
                        {
                            produto.ID = Int32.Parse(dr["id"].ToString());
                        }
                        dr.Close();
                    }
                }
                try
                {
                    command.ExecuteNonQuery();
                    transacao.Commit();
                }
                catch (Exception erro)
                {
                    transacao.Rollback();
                    throw new Exception ("Erro DAOProduto Persiste " + erro.Message);
                }
                return produto;
            }
            
        }

        /// <summary>
        /// Método efetua a busca por uma lista e produtos que pertence ao mesmo grupo no sistema mesmo sendo descontinuado
        /// </summary>
        /// <param name="IDGrupo">Id do grupo de produto</param>
        /// <returns>Lista de produtos localizado, caso não localizado retorna uma lista em branco.</returns>
        public List<NetProduto> BuscarGrupo(long IDGrupo)
        {
            return BuscarGrupo(IDGrupo, false);
            
        }
        /// <summary>
        /// Método efetua a busca por uma lista de produtos que pertence ao mesmo grupo no sistema filtrando se o produto está ou não descontinuado
        /// </summary>
        /// <param name="IDGrupo">Id do grupo do porduto</param>
        /// <param name="Descontinuado">Booleano que confirma se o produto foi descontinuado, se true o sistema filtra removendo o descontinuado</param>
        /// <returns></returns>
        public List<NetProduto> BuscarGrupo(long IDGrupo, Boolean Descontinuado)
        {
            List<NetProduto> lista = new List<NetProduto>();
            String SQL =((Descontinuado)?"Seletc * from produto where tipo = " + IDGrupo + " + desc = false;": "Seletc * from produto where tipo = " + IDGrupo + ";");
            return BuscaLista(SQL);

        }

        
    }
}
