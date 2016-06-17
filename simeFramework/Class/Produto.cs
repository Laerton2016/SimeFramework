using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using System.Data.OleDb;


namespace SIME.Class
{
    /// <summary>
    /// Classe que cria o objeto do tipo produto
    /// </summary>
    public class Produto : ITrataDados
    {
        private Int32 ID = 0;
        private Int32 ID_erro = 0;
        private String descricao;
        private String complemento = " ";
        private String codFabricante;
        private Double custo = 0;
        private Double ICMSCusto = 0;
        private Double taxaFrete = 0;
        private Double taxaIPI = 0;
        private Int32 idGrupo = 0;
        private Double valorVenda = 0;
        private String imagem = " ";
        private Boolean art33 = false;
        private Double peso = 0;
        private String EAN;
        private Boolean descontinuado = false;
        private Int32 idRegra = 0;
        private Double txLucroMaximo = 0;
        private Double txLucroMinimo = 0;
        private Int32 quantMinima = 0;
        private Int32 idMedida = 0;
        private Double txDesconto = 0;
        private String politicaVenda;
        private String NCM;
        private Int32 quantEstoque = 0;
        private Double valorVendaDesconto = 0;
        private Double txComissao = 0;
        private Connection conex;
        private Recordset RSdados = new Recordset();
        private Regra regra;
        private Medida medida;
        private String SQL;
        private Grupo grupo;
        private long IdProduto;
        private Connection connex;

        /// <summary>
        /// Cria o objeto tipo produto baseado os argumentos de entrada.
        /// </summary>
        /// <param name="ID">Int32 ID do produto</param>
        /// <param name="conex">ADODB tipo Connection para conectar ao banco de dados para gravação</param>
        public Produto(Int32 ID, Connection conex)
        {
            this.ID = ID;
            this.conex = conex;
            coletadados();
        }
        /// <summary>
        /// Cria o objeto tipo produto baseado nos argumentos de entrada
        /// </summary>
        /// <param name="conex">ADODB tipo Connection para conenctar ao banco de dados para gravação</param>
        public Produto(Connection conex)
        {
            this.ID = 0;
            this.conex = conex;
        }

        
        private void coletadados()
        {
            String SQL = "SELECT PRODUTOS.* FROM PRODUTOS WHERE (((PRODUTOS.Cod)=" + ID + "));";

            Recordset RSdados = new Recordset();

            RSdados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (RSdados.EOF || RSdados.BOF)
            {
                throw new ArgumentException("Dados não localizado com ID informado.");
            }
            else
            {

                this.ID = Convert.ToInt32(RSdados.Fields["COD"].Value);
                this.descricao = (RSdados.Fields["descrição"].Value.Equals(DBNull.Value)) ? "" : Convert.ToString(RSdados.Fields["descrição"].Value);
                this.complemento = (RSdados.Fields["compatibilidade"].Equals(DBNull.Value)) ? " " : Convert.ToString(RSdados.Fields["compatibilidade"].Value);
                this.codFabricante = (RSdados.Fields["cod de fabricação"].Equals(DBNull.Value)) ? " " : Convert.ToString(RSdados.Fields["cod de fabricação"].Value);
                this.custo = (RSdados.Fields["Custo"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["Custo"].Value);
                this.ICMSCusto = (RSdados.Fields["icm de Compra"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["icm de Compra"].Value);
                this.taxaFrete = (RSdados.Fields["taxa de frete"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["taxa de frete"].Value);
                this.taxaIPI = (RSdados.Fields["IPI"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["IPI"].Value);
                this.idGrupo = (RSdados.Fields["tipo"].Value.Equals(DBNull.Value)) ? 1 : Convert.ToInt32(RSdados.Fields["tipo"].Value);
                this.valorVenda = (RSdados.Fields["expr5"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["expr5"].Value);
                this.imagem = (RSdados.Fields["imagem"].Value.Equals(DBNull.Value)) ? " " : Convert.ToString(RSdados.Fields["imagem"].Value);
                this.art33 = (RSdados.Fields["art33"].Value.Equals(DBNull.Value)) ? false : Convert.ToBoolean(RSdados.Fields["art33"].Value);
                this.peso = (RSdados.Fields["peso"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["peso"].Value);
                this.EAN = (RSdados.Fields["CodBarras"].Value.Equals(DBNull.Value)) ? "0" : Convert.ToString(RSdados.Fields["CodBarras"].Value);
                this.descontinuado = (RSdados.Fields["desc"].Value.Equals(DBNull.Value)) ? false : Convert.ToBoolean(RSdados.Fields["desc"].Value);
                this.idRegra = (RSdados.Fields["regra"].Value.Equals(DBNull.Value)) ? 1 : Convert.ToInt32(RSdados.Fields["regra"].Value);
                this.txLucroMaximo = (RSdados.Fields["taxa de lucro grand"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["taxa de lucro grand"].Value);
                this.txLucroMinimo = (RSdados.Fields["TX_ATA_MAX"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["TX_ATA_MAX"].Value);
                this.quantMinima = (RSdados.Fields["MINI"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(RSdados.Fields["MINI"].Value);
                this.idMedida = (RSdados.Fields["Medida"].Value.Equals(DBNull.Value)) ? 1 : Convert.ToInt32(RSdados.Fields["Medida"].Value);
                this.txDesconto = (RSdados.Fields["tx_desconto"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["tx_desconto"].Value);
                this.politicaVenda = (RSdados.Fields["politica"].Value.Equals(DBNull.Value)) ? "" : Convert.ToString(RSdados.Fields["politica"].Value);
                this.NCM = (RSdados.Fields["NCM"].Value.Equals(DBNull.Value)) ? "0" : Convert.ToString(RSdados.Fields["NCM"].Value);
                this.quantEstoque = (RSdados.Fields["estoque"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(RSdados.Fields["estoque"].Value);
                this.valorVendaDesconto = (RSdados.Fields["Expr6"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["Expr6"].Value);
                this.txComissao = (RSdados.Fields["Valor de Venda grd"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RSdados.Fields["Valor de Venda GRD"].Value.ToString());
                RSdados.Close();
                //criando grupo e regra
                if (idGrupo > 0) { grupo = new Grupo(idGrupo); }
                if (idRegra > 0) { regra = new Regra(idRegra); }
            }


        }





        public Double getTxComissao() { return this.txComissao; }
        public Double getValorVendaDesconto() { return (valorVenda -(valorVenda * (txDesconto/100))); }
        public Int32 getID() { return this.ID; }
        public Int32 getID_erro() { return this.ID_erro; }
        public String getDescricao() { return this.descricao; }
        public String getComplemento() { return this.complemento; }
        public String getCodFabricante() { return this.codFabricante; }
        public Double getCusto() { return this.custo; }
        public Double getICMSCusto() { return this.ICMSCusto; }
        public Double gettaxaFrete() { return this.taxaFrete; }
        public Double gettaxaIPI() { return this.taxaIPI; }
        public Int32 getidGrupo() { return this.idGrupo; }
        public Double getvalorVenda() { return this.valorVenda; }
        public String getimagem() { return this.imagem; }
        public Boolean getart33() { return this.art33; }
        public Double getpeso() { return this.peso; }
        public String getEAN() { return this.EAN; }
        public Boolean getdescontinuado() { return this.descontinuado; }
        public Int32 getidRegra() { return this.idRegra; }
        public Double gettxLucroMaximo() { return this.txLucroMaximo; }
        public Double gettxLucroMinimo() { return this.txLucroMinimo; }
        public Int32 getquantMinima() { return this.quantMinima; }
        public Int32 getidMedida() { return this.idMedida; }
        public Double gettxDesconto() { return this.txDesconto; }
        public String getpoliticaVenda() { return this.politicaVenda; }
        public String getNCM() { return this.NCM; }
        public Int32 getquantEstoque()
        {
            return this.quantEstoque;
        }

        /**
        public void reloadQuantidade() 
        {
            Recordset Dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String SQL = "SELECT PRODUTOS.estoque FROM PRODUTOS WHERE (((PRODUTOS.Cod)=" + ID + "));";
            Dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            System.Threading.Thread.Sleep(500);
            this.quantEstoque = (Dados.Fields["estoque"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(Dados.Fields["estoque"].Value);
        }
         **/
        public void setTxComissao(Double txComissao)
        {
            if (txComissao < 0)
            {
                throw new ArgumentException("A Taxa de comissão não pode ter valor negativo.");
            }

            this.txComissao = txComissao;
        }

        public void setDescricao(String descricao)
        {
            if (descricao.Equals("") || descricao == null)
            {
                throw new ArgumentException("A descrição do produto não pode ser vazio ou nulo.");
            }
            else if (descricao.Length > 45)
            {
                throw new ArgumentException("A descrição do produto não pode conter mais de 45 caracteres.");
            }
            else
            {
                this.descricao = descricao;
            }

        }

        public void setValorVendaDesconto(Double valorVendaDesconto)
        {
            if (valorVendaDesconto < 0)
            {
                throw new ArgumentException("Valor de venda com desconto não pode ser negativo.");
            }
            this.valorVendaDesconto = valorVendaDesconto;
        }

        public void setComplemento(String complemento)
        {
            if (complemento.Length == 0)
            {
                this.complemento = " ";
            }
            else
            {
                this.complemento = complemento;
            }
        }

        public void setCodFabricante(String codFabricante)
        {
            if (codFabricante.Length == 0)
            {
                this.codFabricante = " ";
            }
            else
            {
                this.codFabricante = codFabricante;
            }
        }

        public void setCusto(Double custo)
        {
            if (custo < 0)
            {
                throw new ArgumentException("Custo não pode conter valores negativo.");
            }
            else
            {
                this.custo = custo;
            }
        }

        public void setICMSCusto(Double ICMSCusto)
        {
            //Temporariamante desativado enquanto o sistema sime for windows ainda estiver ativo.
            // if (ICMSCusto < 0) { throw new ArgumentException("Não é permitido valores negativos para ICMSCusto."); }
            this.ICMSCusto = ICMSCusto;
        }

        public void settaxaFrete(Double taxaFrete)
        {
            if (taxaFrete < 0)
            {
                throw new ArgumentException("A alíquota de taxa de frete não pode ter valor negativo.");
            }
            else
            {
                this.taxaFrete = taxaFrete;
            }
        }
        public void settaxaIPI(Double taxaIPI)
        {
            if (taxaIPI < 0)
            {
                throw new ArgumentNullException("A alóquota de taxa de IPI não pode conter um valor negativo.");
            }
            else
            {
                this.taxaIPI = taxaIPI;
            }
        }
        public void setidGrupo(Int32 idGrupo)
        {
            try
            {
                grupo = new Grupo(idGrupo);
                this.idGrupo = idGrupo;
            }
            catch (Exception erro)
            {

                throw new ArgumentException(erro.Message);
            }


        }
        public void setvalorVenda(Double valorVenda)
        {
            if (valorVenda < 0)
            {
                throw new ArgumentException("O valor de venda não pode conter valor negativo.");
            }
            else
            {
                this.valorVenda = valorVenda;
            }
        }
        public void setimagem(String imagem)
        {
            if (imagem.Length < 0)
            {
                this.imagem = " ";
            }
            else
            {
                this.imagem = imagem;
            }
        }
        public void setart33(Boolean art33)
        {
            this.art33 = art33;
        }

        public void setpeso(Double peso)
        {
            if (peso < 0)
            {
                throw new ArgumentNullException("Peso não pode conter valor negativo.");
            }
            else
            {
                this.peso = peso;
            }
        }
        public void setEAN(String EAN)
        {
            if (EAN.Trim().Equals(""))
            {
                throw new ArgumentException("O codigo de barras não pode ser vazio.");
            }
            else if (EAN.Length != 13)
            {
                throw new ArgumentException("O código de barras deve conter 13 dígitos.");
            }
            else if (!chekDigitoEAN(EAN))
            {
                throw new ArgumentException("O código de barras é  inválido.");
            }
            else
            {
                this.EAN = EAN;
            }

        }

        public void setdescontinuado(Boolean descontinuado)
        {
            this.descontinuado = descontinuado;
        }

        public void setidRegra(Int32 idRegra)
        {
            try
            {
                regra = new Regra(idRegra);
                this.idRegra = idRegra;
            }
            catch (Exception erro)
            {

                throw new AggregateException(erro.Message);
            }
        }
        public void settxLucroMaximo(Double txLucroMaximo) { this.txLucroMaximo = txLucroMaximo; }
        public void settxLucroMinimo(Double txLucroMinimo) { this.txLucroMinimo = txLucroMinimo; }
        public void setquantMinima(Int32 quantMinima) { this.quantMinima = quantMinima; }
        public void setidMedida(Int32 idMedida)
        {
            try
            {
                medida = new Medida(idMedida);
                this.idMedida = idMedida;
            }
            catch (Exception erro)
            {

                throw new ArgumentException(erro.Message);
            }


        }
        public void settxDesconto(Double txDesconto) { this.txDesconto = txDesconto; }
        public void setpoliticaVenda(String politicaVenda) { this.politicaVenda = politicaVenda; }
        public void setNCM(String NCM)
        {
            if (NCM.Trim().Equals(""))
            {
                this.NCM = "0";
            }
            else
            {
                this.NCM = NCM;
            }
        }
        public void setquantEstoque(Int32 quantEstoque)
        {
            if (quantEstoque < 0)
            {
                throw new ArgumentException("Não é possível valores negativos para quantidade estoque.");
            }
            else
            {
                this.quantEstoque = quantEstoque;
            }
        }

        public override string ToString()
        {
            return "ID: " + ID + Environment.NewLine +
                   "Descrição: " + descricao + Environment.NewLine +
                   "Cod de Fabrica: " + codFabricante + Environment.NewLine +
                   "EAN: " + EAN + Environment.NewLine +
                   "NCM: " + NCM + Environment.NewLine +
                   "Estoque: " + quantEstoque + Environment.NewLine +
                   "Venda: R$ " + valorVenda + Environment.NewLine +
                   "Politica: " + politicaVenda + Environment.NewLine +
                   "Desconto: " + txDesconto;
        }

        public string ToStringWeb()
        {
            return "ID: " + ID + "<br>" +
                   "Descrição: " + descricao + "<br>" +
                   "Cod de Fabrica: " + codFabricante + "<br>" +
                   "EAN: " + EAN + "<br>" +
                   "NCM: " + NCM + "<br>" +
                   "Estoque: " + quantEstoque + "<br>" +
                   "Venda: R$ " + valorVenda + "<br>" +
                   "Politica: " + politicaVenda + "<br>" +
                   "Desconto: " + txDesconto;

        }

        /// <summary>
        /// Método que esclui o produto atual
        /// </summary>
        /// <returns>Retorna um boleando confirmado a exclusão.</returns>
        public Boolean excluir()
        {
            Recordset RSdados = new Recordset();

            if (ID == 0)
            {
                throw new ArgumentException("Não é possível excluir um item que ainda não foi gravado.");
            }
            SQL = "DELETE PRODUTOS.Cod FROM PRODUTOS WHERE (((PRODUTOS.Cod)=" + ID + "));";

            try
            {

                RSdados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                ID = 0;
                return true;
            }
            catch (Exception erro)
            {
                throw new ArgumentException(erro.Message);
            }

        }
        private Boolean produtoCadastradoSmall(Int32 Id_produto)
        {
            Recordset rsDados = new Recordset();
            Boolean resposta = false;
            Connection conex = new Conexao().getSmall();
            String SQL = "SELECT ESTOQUE.CODIGO FROM ESTOQUE WHERE (((ESTOQUE.CODIGO)='" + Id_produto + "'));";
            try
            {
                rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

                resposta = !(rsDados.EOF || rsDados.BOF);
                conex.Close();
            }
            catch (Exception)
            {

                throw;
            }

            return resposta;

        }
        /// <summary>
        /// Método para Salvar os dados no programa Small em paralelo ao SIME
        /// </summary>
        private void salvarSmall()
        {
            Recordset RSdados = new Recordset();
            Regra regra = new Regra(this.idRegra);
            String SQL = "";
            Boolean cadastrado = produtoCadastradoSmall(this.ID);
            //Preparando a strunção SQL para o produto já cadastrado
            if (cadastrado)
            {
                SQL = "UPDATE ESTOQUE SET " +
                "ESTOQUE.REFERENCIA = '" + EAN + "', " +
                "ESTOQUE.DESCRICAO = '" + descricao + "', " +
                "ESTOQUE.NOME = '" + grupo.getTipo().Replace("'//", "'///") + "', " +
                "ESTOQUE.MEDIDA = '" + new Medida(idMedida).getMedida() + "', " +
                "ESTOQUE.PRECO = " + valorVenda.ToString().Replace(',', '.') + ", " +
                "ESTOQUE.CUSTOCOMPR = " + custo.ToString().Replace(',', '.') + ", " +
                "ESTOQUE.QTD_MINIM = " + quantMinima + ", " +
                "ESTOQUE.CF = '" + NCM + "', " +
                "ESTOQUE.CST = '" + regra.getCST() + "' , " +
                "ESTOQUE.COMISSAO = " + txComissao.ToString().Replace(',', '.') + ", " +
                "ESTOQUE.CSOSN = '" + regra.getCSOSN() + "', " +
                "ESTOQUE.QTD_ATUAL = " + quantEstoque.ToString() + ", " +
                "ESTOQUE.ST = '" + regra.getST() + "', " +
                "ESTOQUE.IAT = '" + regra.getIAT() + "', " +
                "ESTOQUE.IPPT = '" + regra.getIPPT() + "' " +
                "WHERE (((ESTOQUE.CODIGO)='" + ID + "'));";
            }
            else
            {

                SQL = "SELECT ESTOQUE.* FROM ESTOQUE;";
            }


            try
            {
                RSdados.Open(SQL, new Conexao().getSmall(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                //Neste caso o produto ainda não está cadasrado, logo ele deve ser incluido 
                if (!cadastrado)
                {
                    Int32 ID_novo = (this.ID == 0) ? jaCadastrado(EAN).getID() : this.ID;
                    RSdados.AddNew();

                    RSdados.Fields["REFERENCIA"].Value = EAN;
                    RSdados.Fields["DESCRICAO"].Value = descricao;
                    RSdados.Fields["NOME"].Value = grupo.getTipo();
                    RSdados.Fields["MEDIDA"].Value = new Medida(idMedida).getMedida();
                    RSdados.Fields["PRECO"].Value = valorVenda;
                    RSdados.Fields["QTD_MINIM"].Value = quantMinima;
                    RSdados.Fields["CF"].Value = NCM;
                    RSdados.Fields["CUSTOCOMPR"].Value = custo;
                    RSdados.Fields["CST"].Value = regra.getCST();
                    RSdados.Fields["COMISSAO"].Value = txComissao;
                    RSdados.Fields["CSOSN"].Value = regra.getCSOSN();
                    RSdados.Fields["QTD_ATUAL"].Value = quantEstoque.ToString();
                    RSdados.Fields["ST"].Value = regra.getST();
                    RSdados.Fields["IAT"].Value = regra.getIAT();
                    RSdados.Fields["IPPT"].Value = regra.getIPPT();
                    RSdados.Fields["REGISTRO"].Value = ID_novo;
                    RSdados.Fields["CODIGO"].Value = ID_novo;

                    RSdados.Update();
                    RSdados.Close();
                }
            }
            catch (Exception erro)
            {

                throw new ArgumentException(erro.Message);
            }



        }
        public Boolean salvar()
        {
            return salvar(true);
        }
        /// <summary>
        /// Método que salva ou atualiza o produto novo ou atual no banco de dados
        /// </summary>
        /// <returns>Retorna um tipo booleano para o resultado</returns>
        public Boolean salvar(bool salvaSmall)
        {
            String SQL = "";
                        Recordset RSdados = new Recordset();

            if (ID.Equals(0))
            {
                if (jaCadastrado(EAN) != null && !(EAN.Equals("0")))
                {
                    throw new ArgumentException("Código EAN de barras já cadastrado em outro produto:" + Environment.NewLine + jaCadastrado(EAN).ToString() +
                    Environment.NewLine + "Informações não foram salvas.");
                }



                SQL = "INSERT INTO Produtos ( Descrição, Compatibilidade, [Cod de fabricação], " +
                "Custo, [Icm de Compra], [Taxa de frete], IPI, TIPO, Expr5, imagem, art33, peso, " +
                "Codbarras, [desc], REGRA, [Taxa de lucro Grand], TX_ATA_MAX, MINI, Medida, Tx_desconto, Politica, NCM, expr6, [valor de venda grd], expr7 ) " +
                "SELECT '" + descricao.Replace(',', '.').ToString() +
                "', '" + complemento.Replace(',', '.').ToString() +
                "', '" + codFabricante.Replace(',', '.').ToString() +
                "', " + Convert.ToString(custo).Replace(",", ".") +
                ", " + Convert.ToString(ICMSCusto).Replace(",", ".") +
                ", " + Convert.ToString(taxaFrete).Replace(",", ".") +
                ", " + Convert.ToString(taxaIPI).Replace(",", ".") +
                ", " + idGrupo +
                ", " + Convert.ToString(valorVenda).Replace(",", ".") +
                ", '" + imagem +
                "', " + art33 +
                ", " + Convert.ToString(peso).Replace(",", ".") +
                ", '" + EAN +
                "', " + descontinuado +
                ", " + idRegra +
                ", " + Convert.ToString(txLucroMaximo).Replace(",", ".") +
                ", " + Convert.ToString(txLucroMinimo).Replace(",", ".") +
                ", " + quantMinima +
                ", " + idMedida +
                ", " + Convert.ToString(txDesconto).Replace(",", ".") +
                ", '" + politicaVenda.Replace(',', '.').ToString() +
                "', '" + NCM +
                "', " + Convert.ToString(valorVendaDesconto).Replace(",", ".") +
                ", " + Convert.ToString(txComissao).Replace(",", ".") +
                ", " + Convert.ToString(valorVenda).Replace(",", ".") + ";";
            }
            else
            {
                SQL = "UPDATE PRODUTOS SET PRODUTOS.Descrição = '" + descricao + "'" +
                    ", PRODUTOS.Compatibilidade = '" + ((complemento.Equals("")) ? " " : complemento) + "'" +
                    ", PRODUTOS.[Cod de fabricação] = '" + ((codFabricante.Equals("") ? "0" : codFabricante)) + "'" +
                      ", PRODUTOS.Custo = " + Convert.ToString(custo).Replace(",", ".") +
                      ", PRODUTOS.[Icm de Compra] = " + Convert.ToString(ICMSCusto).Replace(",", ".") +
                      ", PRODUTOS.[Taxa de frete] = " + Convert.ToString(taxaFrete).Replace(",", ".") +
                      ", PRODUTOS.IPI = " + Convert.ToString(taxaIPI).Replace(",", ".") +
                      ", PRODUTOS.TIPO = " + idGrupo +
                      ", PRODUTOS.Expr5 = " + Convert.ToString(valorVenda).Replace(",", ".") +
                      ", PRODUTOS.imagem = '" + imagem + "'" +
                      ", PRODUTOS.art33 = " + art33 +
                      ", PRODUTOS.peso = " + Convert.ToString(peso).Replace(",", ".") +
                      ", PRODUTOS.Codbarras = '" + EAN + "'" +
                      ", PRODUTOS.[desc] = " + descontinuado +
                      ", PRODUTOS.REGRA = " + idRegra +
                      ", PRODUTOS.[Taxa de lucro Grand] = " + Convert.ToString(txLucroMaximo).Replace(",", ".") +
                      ", PRODUTOS.TX_ATA_MAX = " + Convert.ToString(txLucroMinimo).Replace(",", ".") +
                      ", PRODUTOS.MINI = " + quantMinima +
                      ", PRODUTOS.Medida = " + idMedida +
                      ", PRODUTOS.Tx_desconto = " + Convert.ToString(txDesconto).Replace(",", ".") +
                      ", PRODUTOS.Politica = '" + politicaVenda + "'" +
                      ", PRODUTOS.NCM = '" + NCM + "'" +
                      ", PRODUTOS.Estoque = " + quantEstoque +
                      ", PRODUTOS.expr6 = " + Convert.ToString(valorVendaDesconto).Replace(",", ".") +
                      ", PRODUTOS.[valor de venda grd] = " + Convert.ToString(txComissao).Replace(",", ".") +
                      ", PRODUTOS.expr7 =" + Convert.ToString(valorVenda).Replace(",", ".") +
                      " WHERE (((PRODUTOS.Cod)=" + ID + "));";
            }

            try
            {
                RSdados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                System.Threading.Thread.Sleep(500);
                if (salvaSmall) { salvarSmall(); } //condição especial para small.
            }
            catch (Exception erro)
            {
                throw new ArgumentException(erro.Message);

            }

            return true;
        }

        /// <summary>
        /// Método que faz emparelhamento com os dados do SIME com SMALL
        /// </summary>
        public void indexar()
        {
            Recordset rsDadosSime = new Recordset();
            Recordset rsDadosSmall = new Recordset();
            String SQL1; //, SQL2;
            Connection conex1 = new Conexao().getDb4();
            //Connection conex2 = new Conexao().getSmall();
            Produto item;

            SQL1 = "SELECT PRODUTOS.* FROM PRODUTOS ORDER BY PRODUTOS.Cod;";
            //SQL2 = "SELECT ESTOQUE.* FROM ESTOQUE ORDER BY ESTOQUE.CODIGO;";

            //Procedimentos Verificar se o produto está cadastrado no Small 
            //Se estiver ver se há quantidade em estoque igual a do Sime
            //Se não estiver incluir o produto
            //Se a quantidade estiver diferente fazer ajuste do Small com o sime.

            rsDadosSime.Open(SQL1, conex1, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            //rsDadosSmall.Open(SQL2, conex2, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(rsDadosSime.EOF || rsDadosSime.BOF))
            {
                item = new Produto(Convert.ToInt32(rsDadosSime.Fields["cod"].Value), conex1);
                item.salvarSmall();
                rsDadosSime.MoveNext();
            }
            rsDadosSime.Close();
            conex1.Close();
        }

        /// <summary>
        /// Método utilizado para sobrepor um registro já existente baseado nos dados deste produto. 
        /// Só possível ultilizar caso ao Salvar a primeira vez o sistema tenha detectado um registro já existente.
        /// </summary>
        /// <returns>Retorna um booleando confirmando a gravação.</returns>
        public Boolean sobrepor()
        {
            if (ID_erro == 0)
            {
                throw new ArgumentException("Não é possível sobrepor um registro novo. Tente o método Salvar.");
            }
            this.ID = this.ID_erro;
            this.ID_erro = 0;
            return salvar();

        }
        /// <summary>
        /// Método que coleta novamente os dados do produto do banco de dados no caso de um produto já cadastrado 
        /// não afeta em caso de produto novo;
        /// </summary>
        public void reload()
        {
            if (ID != 0)
            {
                System.Threading.Thread.Sleep(500);
                coletadados();
            }
            else
            {
                this.ID = jaCadastrado(EAN).getID();
            }

        }

        public Boolean chekDigitoEAN(String EAN)
        {
            if (EAN.Length != 13)
            {
                throw new ArgumentException("O código EAN deve conter 13 digitos.");
            }

            string sTemp = EAN;
            int iSum = 0;
            int iDigit = 0;
            int fator = 1;

            for (int i = 0; i < EAN.Length - 1; i++)
            {

                iSum += ((Convert.ToInt16(EAN[i].ToString())) * fator);
                fator = (fator == 1) ? 3 : 1;
            }

            int multiplo = iSum / 10;
            multiplo = ((multiplo * 10) < iSum) ? ((multiplo + 1) * 10) : (multiplo * 10);
            iDigit = multiplo - iSum;

            return EAN[12].ToString().Equals(iDigit.ToString());

        }
        private Produto jaCadastrado(String EAN)
        {
            String SQL = "SELECT PRODUTOS.Cod, PRODUTOS.Codbarras FROM PRODUTOS WHERE (((PRODUTOS.Codbarras)='" + EAN + "'));";

            Recordset RSdados = new Recordset();

            RSdados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (!(RSdados.EOF || RSdados.BOF))
            {

                if (Convert.ToInt32(RSdados.Fields["cod"].Value) != ID)
                {
                    return new Produto(Convert.ToInt32(RSdados.Fields["cod"].Value), new Conexao().getDb4());
                }
            }
            return null;
        }

        public Grupo getGrupo()
        {
            grupo = new Grupo(idGrupo);
            return grupo;
        }

        public Regra getRegra()
        {
            regra = new Regra(idRegra);
            return regra;
        }

        public Medida getMedida()
        {
            medida = new Medida(idMedida);
            return medida;
        }

        /// <summary>
        /// Método para regeistrar a entrada da quantidade deste produtos baseado em dados de uma NF.
        /// </summary>
        /// <param name="id_fornecedor">Inteiro com ID do fornecedor da mercadoria, não são aceitos valores inferiores a 1</param>
        /// <param name="Nf">String que contém o n° da Nota fiscal de entrada.</param>
        /// <param name="quantidade">Inteiro para a quantidade a ser dado entrada. Não é permidito valores negativos.</param>
        /// <param name="sn">Boolean para identificar se o produto é sn</param>
        /// <param name="dataNF">DateTime que recebe a Data da Nota fiscal</param>
        /// <param name="Id_operador">Interiro com ID do operador do registro</param>
        /// <param name="Id_empresa">Inteiro com ID da empresa </param>

        public void setEntradas(Int32 id_fornecedor, String Nf, Int32 quantidade, Boolean sn, DateTime dataNF, Int32 Id_operador, Int32 Id_empresa)
        {
            if (this.ID == 0) { throw new ArgumentException("Não pode registrar uma entrada em um produto não gravado ou excluido."); }
            Entrada entrada = new Entrada(ID, id_fornecedor, Nf, quantidade, sn, dataNF, Id_operador, Id_empresa);

            //processo de autualização de estoque.

            this.quantEstoque += quantidade;

            Recordset Dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String SQL = "UPDATE PRODUTOS SET PRODUTOS.Estoque = " + this.quantEstoque + " WHERE (((PRODUTOS.Cod)=" + this.ID + "));";
            Dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            // Processo de gravação da tabela notas para emissão das etiquetas pelo SIME antigo
            SQL = "SELECT Notas.Loja, Notas.Fornecedor, Notas.NF, Notas.Cod FROM Notas WHERE (((Notas.Loja)=" + Id_empresa +
                ") AND ((Notas.Fornecedor)=" + id_fornecedor + ") AND ((Notas.NF)='" + Nf + "'));";

            Dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (Dados.EOF || Dados.BOF)
            {
                Dados.AddNew();
                Dados.Fields["loja"].Value = Id_empresa.ToString();
                Dados.Fields["Fornecedor"].Value = id_fornecedor.ToString();
                Dados.Fields["NF"].Value = Nf;
                Dados.Update();
                Dados.Close();
            }

            conex.Close();
            setEntradaSmall();
        }

        private void setEntradaSmall()
        {
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getSmall();
            String SQL = "UPDATE ESTOQUE SET " +
                "ESTOQUE.QTD_ATUAL = " + quantEstoque.ToString() + " " +
                "WHERE (((ESTOQUE.CODIGO)='" + ID + "'));";
            try
            {
                rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                conex.Close();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }

        /// <summary>
        /// Método que retorna uma lista de objetos entradas com todas as entradas do produto.
        /// </summary>
        /// <returns></returns>
        public List<Entrada> getListaEntradas()
        {
            List<Entrada> entradas = new List<Entrada>();
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String SQL = "SELECT Entradas.Cod, Entradas.Id FROM Entradas WHERE (((Entradas.Cod)=" + ID + ") AND ((Entradas.Quantidade)<>0)) ORDER BY Entradas.Data;";
            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(RSDados.EOF || RSDados.BOF))
            {
                entradas.Add(new Entrada(Convert.ToInt32(RSDados.Fields["id"].Value)));
                RSDados.MoveNext();
            }
            return entradas;
        }


        /// <summary>
        /// Método apresenta uma lista contendo histórico de compra de um determidado produto em um determinado perioudo
        /// </summary>
        /// <param name="idProduto">Inteiro positivo maior que zero</param>
        /// <param name="inicio">Data para inicio de pesquisa, sendo menor ou igual ao do final de pesquisa</param>
        /// <param name="fim">Data para fim de pesquisa, sendo maior ou igual ao do inicio de pesquisa</param>
        /// <returns>Lista de Array de String contendo os seguintes dados: Data da compra, Quantidade, NF e Nome do Fornecedor</returns>
        public List<string[]> historicoCompra(DateTime inicio, DateTime fim)
        {
            if (ID == 0) { throw new ArgumentException("Id produto inválido, o id deve ser um valor inteiro positivo maior que zero."); }
            if (inicio > fim) { throw new ArgumentException("Data de inicio deve ser menor que a data de fim."); }
            OleDbConnection conex = new Conexao().getDB4net();
            String SQL = "SELECT quantidade, data, fornecedor, nota " +
                         "FROM entradas AS e LEFT JOIN fornecedores AS f ON [e].cod_fornecedor = f.cod " +
                         "WHERE ((([e].data) Between #" + inicio.ToShortDateString() + "# And #" + fim.ToShortDateString() + "#) AND (([e].cod)=" + this.ID + "));";

            conex.Open();
            List<String[]> retorno = new List<string[]>();
            OleDbCommand command = new OleDbCommand();
            command.CommandText = SQL;
            command.Connection = conex;
            OleDbDataReader DR = command.ExecuteReader();

            if (DR.HasRows) { throw new Exception("Não foi localizado nenhuma entrada com esse id de produto."); }

            while (DR.Read())
            {
                string[] dado = new string[4];
                dado[0] = DR["quantidade"].ToString();
                dado[1] = DR["data"].ToString();
                dado[2] = DR["nota"].ToString();
                dado[3] = DR["Fornecedor"].ToString();
                retorno.Add(dado);
            }
            conex.Close();
            return retorno;
        }
        /// <summary>
        /// Processo que busca todas as entradas de um determinado produto no banco de dados.
        /// </summary>
        /// <param name="idProduto">Inteiro positivo maior que zero contendo o id do produto cadastrado.</param>
        /// <returns>Lista contendo arrays de string com : quantidade, data, nota e nome do fornecedor que foi efetuado a compra.</returns>
        public List<String[]> historicoCompra()
        {
            return historicoCompra(Convert.ToDateTime("01/01/1900"), DateTime.Now);
        }

    }

}