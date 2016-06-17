using System;
using System.Collections.Generic;
using System.Linq;
using ADODB;


namespace SIME.Class
{
    public class Clientes
    {

        private List<String[]> dadosCombo = new List<string[]>();
        private Int16 cod = 0;
        private String nome = "";
        /// <summary>
        /// Classe que cria o obbjeto do tipo clientes
        /// </summary>
        public Clientes()
        {
            String SQL = "SELECT Clientes.Cod_cliente, Clientes.Nome, Clientes.CNPJ FROM Clientes ORDER BY Clientes.Nome;";
            coletaDados(SQL);
        }
        //metodo que coleta os dados baseado em uma SQL recebida como argumento de entrada 
        private void coletaDados(String SQL)
        {
            Recordset dados = new Recordset();
            dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            dadosCombo.Clear();
            while (!(dados.EOF || dados.BOF))
            {
                dadosCombo.Add(new String[] { Convert.ToString(dados.Fields["cod_cliente"].Value), Convert.ToString(dados.Fields["Nome"].Value) });
                dados.MoveNext();
            }
            dados.Close();
        }
        /// <summary>
        /// Metodo retorna uma lista de arrya de string contendo todos os nomes de clientes e cod de id
        /// </summary>
        /// <returns>Lista de array de String</returns>
        public List<String[]> getListaClientes()
        {
            return dadosCombo;
        }
        /** METODO DEVE FICAR NA FACE
        /// <summary>
        /// Método para buscar clientes no banco de dados e povoar um DropDownList 
        /// </summary>
        /// <param name="cod">Cpf ou CNPJ  do cliente a buscar</param>
        /// <param name="combo">DropDonwlist para ser preenchido</param>
        /// <returns>Boolean que confirma a existencia de dados neste conntesto</returns>
        public bool preencheCombo(String cod, ref DropDownList combo)
        {
            //DropDownList combo = null;
            List<String[]> dados = this.filtraCPFCNPJ(cod);
            if (dados.Count >= 1)
            //combo = new DropDownList();
            {
                combo.Items.Clear();
                foreach (String[] item in dados)
                {
                    combo.Items.Add(new ListItem() { Value = Convert.ToString(item[0]), Text = item[1].ToUpper() });
                }
                return true;
            }
            return false;
        }
    **/
        /** METODO DEVE FICAR NO FACE
        /// <summary>
        /// Método para buscar clientes no banco de dados e povoar um DropDownList 
        /// </summary>
        /// <param name="nome">Nome ou parte do nome do cliente a ser buscado.</param>
        /// <param name="combo">DropDonwlist para ser preenchido</param>
        /// <param name="porParte">Boolean que defie se a busca é pelo nome ou parte dele</param>
        /// <returns>Boolean que confirma a existencia de dados neste conntesto</returns>
        public bool preencheCombo(String nome, ref DropDownList combo, bool porParte)
        {
            //DropDownList combo = null;
            List<String[]> dados = this.filtraNome(nome, porParte);
            if (dados.Count >= 1)
            //combo = new DropDownList();
            {
                combo.Items.Clear();
                foreach (String[] item in dados)
                {
                    combo.Items.Add(new ListItem() { Value = Convert.ToString(item[0]), Text = item[1].ToUpper() });
                }
                return true;
            }
            return false;
        }
        **/

        /// <summary>
        /// Metodo retorna uma lista de arrya de string contendo todos os nomes de clientes e cod de id filtrados por CPF ou CNPJ
        /// </summary>
        /// <param name="CPF">String contendo CPF</param>
        /// <returns>Lista de arrays de string</returns>
        public List<String[]> filtraCPFCNPJ(String CPF)
        {
            String SQL = "SELECT Clientes.Cod_cliente, Clientes.Nome, Clientes.CNPJ " +
                         "FROM Clientes WHERE (((Clientes.CNPJ)='" + CPF + "')) " +
                         "ORDER BY Clientes.Nome;";
            coletaDados(SQL);
            return dadosCombo;
        }
        /// <summary>
        /// Metodo retorna uma lista de arrya de string contendo todos os nomes de clientes e cod de id filtrado por cod ID
        /// </summary>
        /// <param name="ID">Inteiro com ID cod do cliente</param>
        /// <returns>Llista de array de string</returns>
        public List<String[]> filtraID(int ID)
        {
            String SQL = "SELECT Clientes.Cod_cliente, Clientes.Nome, Clientes.CNPJ " +
                         "FROM Clientes WHERE (((Clientes.cod_cliente)=" + ID + ")) " +
                         "ORDER BY Clientes.Nome;";
            coletaDados(SQL);
            return dadosCombo;
        }

        /// <summary>
        /// Metodo retorna uma lista de arrya de string contendo todos os nomes de clientes e cod ou id baseado no nome completo ou parte baseado em um boolean
        /// </summary>
        /// <param name="nome">String contendo as informações de filtro.</param>
        /// <param name="parte">Boolean para confirma se parcial </param>
        /// <returns></returns>
        public List<String[]> filtraNome(String nome, Boolean parte)
        {
            String SQL = "";
            if (parte)
            {
                SQL = "SELECT Clientes.Cod_cliente, Clientes.Nome, Clientes.CNPJ " +
                         "FROM Clientes WHERE (((Clientes.Nome) Like '%" + nome + "%')) " +
                         "ORDER BY Clientes.Nome;";

            }
            else
            {
                SQL = "SELECT Clientes.Cod_cliente, Clientes.Nome, Clientes.CNPJ " +
                "FROM Clientes WHERE (((Clientes.Nome)='" + nome + "')) " +
                "ORDER BY Clientes.Nome;";

            }

            coletaDados(SQL);
            return dadosCombo;
        }
        /// <summary>
        /// Método que retorna objeto do tipo cliente baseado no ID cod informado
        /// </summary>
        /// <param name="cod">Inteiro com ID ou Cod do cliente</param>
        /// <returns>Retorna o objeto do tipo Cliente</returns>
        public Cliente getCliente(Int16 cod)
        {
            return new Cliente(cod, new Conexao().getDb4());
        }
        /// <summary>
        /// Metódo que retorna String baseado nos dados dos clientes
        /// </summary>
        /// <returns>String</returns>
        public override String ToString()
        {
            String retorna = "";
            for (int i = 0; i < dadosCombo.Count; i++)
            {
                for (int j = 0; j < dadosCombo[i].Length; j++)
                {
                    retorna += dadosCombo[i][j] + " - ";
                }
                retorna += "<br>";
            }
            return retorna;
        }
        /// <summary>
        /// Método que retorna objeto do tipo cliente baseado no ID cod informado
        /// </summary>
        /// <param name="cod">Inteiro com ID ou Cod do cliente</param>
        /// <returns>Objeto do tipo cliente </returns>
        public Cliente getCliente(int cod)
        {
            return new Cliente(cod, new Conexao().getDb4());
        }
        /// <summary>
        /// Método retorna a quantiade de clientes no objeto
        /// </summary>
        /// <returns>Inteiro</returns>
        public int count()
        {
            return dadosCombo.Count;
        }
        /// <summary>
        /// Método que retona uma lista de Array de string contedo os dados das vendas de um determinado produto
        /// </summary>
        /// <param name="cliente"></param>
        /// <param name="IdProduto"></param>
        /// <returns></returns>
        public List<String[]> getProdutoporCliente(Cliente cliente, Int32 IdProduto)
        {
            List<String[]> resultado = new List<string[]>();

            if (cliente.Fidelidade) //cliente fidelizado
            {
                Int64 IdCliente = cliente.getCod();
                String data = "#" + cliente.Dt_inicializacao.ToShortDateString() + "#";
                //Coletando os dados de vendas de um determidado produto.

                String SQL = "";

                SQL = "SELECT Sum([%$##@_Alias].Quant) AS Quantidade, ([%$##@_Alias].CodCD) AS Cod " +
                       "FROM (SELECT Saída.[Cod do CD] as CodCD , Sum(Saída.Quantidade) AS Quant " +
                       "FROM Saída INNER JOIN Cod_sai ON Saída.cod_sai = Cod_sai.Cod_sai " +
                       "GROUP BY Saída.[Cod do CD], Saída.Desconto, Cod_sai.Data, Cod_sai.Cod_cliente " +
                       "HAVING (((Saída.[Cod do CD])=" + IdProduto + ") AND ((Saída.Desconto)>0) AND ((Cod_sai.Data) Between " + data
                       + " And #" + DateTime.Now.ToShortDateString() + "#) AND ((Cod_sai.Cod_cliente)=" + IdCliente + ")))  AS [%$##@_Alias] " +
                       "GROUP BY [%$##@_Alias].CodCD;";

                Recordset dados1 = new Recordset();
                Connection connex = new Conexao().getDb4();
                Produto produto = new Produto(IdProduto, connex);

                dados1.Open(SQL, connex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                while (!(dados1.EOF || dados1.BOF))
                {
                    String[] item = new string[5];
                    item[0] = produto.getDescricao();
                    item[1] = dados1.Fields["Quantidade"].Value.ToString();
                    item[2] = (Convert.ToInt64(item[1]) / 4).ToString();
                    item[3] = "0";
                    item[4] = item[2];
                    resultado.Add(item);
                    dados1.MoveNext();
                }
                dados1.Close();
                //Incrementando o caso de vendas bonificadas.
                if (resultado.Count > 0)
                {
                    SQL = "SELECT Sum([%$##@_Alias].Quant) AS Quantidade, ([%$##@_Alias].CodCD) AS Cod " +
                       "FROM (SELECT Saída.[Cod do CD] as CodCD , Sum(Saída.Quantidade) AS Quant " +
                       "FROM Saída INNER JOIN Cod_sai ON Saída.cod_sai = Cod_sai.Cod_sai " +
                       "GROUP BY Saída.[Cod do CD], Saída.Desconto, Cod_sai.Data, Cod_sai.Cod_cliente " +
                       "HAVING (((Saída.[Cod do CD])=" + IdProduto + ") AND ((Saída.Desconto)=0) AND ((Cod_sai.Data) Between " + data
                       + " And #" + DateTime.Now.ToShortDateString() + "#) AND ((Cod_sai.Cod_cliente)=" + IdCliente + ")))  AS [%$##@_Alias] " +
                       "GROUP BY [%$##@_Alias].CodCD;";

                    dados1.Open(SQL, connex);
                    while (!(dados1.BOF || dados1.EOF))
                    {
                        produto = new Produto(Int32.Parse( dados1.Fields["cod"].Value.ToString()), connex);
                        for (int i = 0; i < resultado.Count; i++)
                        {
                            if (resultado[i][0].Equals(produto.getDescricao()))
                            {
                                resultado[i][3] = dados1.Fields["Quantidade"].Value.ToString();
                                resultado[i][4] =   (Convert.ToInt64(resultado[i][2]) - (Convert.ToInt64(resultado[i][3]))).ToString();
                                break;
                            }
                        }
                        dados1.MoveNext();
                    }
                }
                connex.Close();
            }

            return resultado;
        }
    }
}