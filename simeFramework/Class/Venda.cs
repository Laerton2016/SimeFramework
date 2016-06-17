using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using ADODB;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using SIME;

namespace SIME
{
    public class Venda
    {
        private DateTime Data = DateTime.Now;
        private Int32 ID = 0;
        private Int32 ID_cliente = 0;
        private Double Especie = 0;
        private Double Cheque = 0;
        private Double Vale = 0;
        private Double Cartao = 0;
        private Double Total = 0;
        private Int32 ID_vendedor = 0;
        private Int32 ID_caixa = 0;
        private Boolean vendaFechada = false;
        //private Int32 IDSmall = 0;
        private string orcamentoSmall ="0000000000" ;
        private List<Item_venda> itens = new List<Item_venda>();



        /// <summary>
        /// Classe que cria o objeto do tipo venda - finalidade é gravar como reverter os dados de uma determinada venda com seus resectivos itens
        /// </summary>
        /// <param name="ID_caixa"> Inteiro contendo ID do caixa válido</param>
        /// <param name="ID_cliente">Inteiro contendo ID do cliente válido</param>
        /// <param name="ID_vendedor">Inteiro contendo ID do vendedor válido</param>
        public Venda(Int32 ID_caixa, Int32 ID_cliente, Int32 ID_vendedor)
        {
            if (ID_caixa <= 0) { throw new ArgumentException("O ID do caixa não pode ser zero ou negatvo."); }
            if (ID_cliente < 0) { throw new ArgumentException("O ID do cliente não pode ser negativo"); }
            if (ID_vendedor <= 0) { throw new ArgumentException("O ID do Vendedor não pode ser zero ou negativo."); }

            this.ID_caixa = ID_caixa;
            this.ID_cliente = ID_cliente;
            this.ID_vendedor = ID_vendedor;
        }
        /// <summary>
        /// Classe que cria o objeto do tipo venda - finalidade é gravar como reverter os dados de uma determinada venda com seus resectivos itens
        /// </summary>
        /// <param name="ID">Inteiro com ID de identificação de uma venda já realizada.</param>
        public Venda(Int32 ID)
        {
            try
            {
                coletaDados(ID);
            }
            catch (Exception e)
            {

                throw new AggregateException(e.Message);
            }

            this.ID = ID;
        }
        /// <summary>
        /// Metodo coleta os dados da venda baseado no ID informado no argumento de entrada
        /// </summary>
        /// <param name="ID">Inteiro com n° da Venda </param>
        private void coletaDados(Int32 ID)
        {
            String SQL = "SELECT Cod_sai.* FROM Cod_sai WHERE (((Cod_sai.Cod_sai)="+ID+"));";
            Recordset rs_dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (rs_dados.EOF || rs_dados.BOF)
            {
                throw new ArgumentException("Id de venda inválida ou não registrada.");
            }
            //coletando dados da venda 
            this.ID_cliente = Convert.ToInt32(rs_dados.Fields["cod_cliente"].Value.ToString());
            this.ID_vendedor = Convert.ToInt32(rs_dados.Fields["OP"].Value.ToString());
            this.ID_caixa = Convert.ToInt32(rs_dados.Fields["CX"].Value.ToString());
            this.Especie = Convert.ToDouble(rs_dados.Fields["especie"].Value.ToString("N"));
            this.Cheque = Convert.ToDouble(rs_dados.Fields["cheque"].Value.ToString("N"));
            this.Vale = Convert.ToDouble(rs_dados.Fields["Vale"].Value.ToString("N"));
            this.Total = this.Vale + this.Cheque + this.Cartao + this.Especie;
            this.orcamentoSmall = rs_dados.Fields["dado1"].Value.ToString();
            rs_dados.Close();
            conex.Close();
            coletaItens(ID);
            this.vendaFechada = true;
        }
        /// <summary>
        /// Método que coleta no banco de dados os itens de uma determinada venda.
        /// </summary>
        /// <param name="ID_venda">Inteiro que contém ID da venda</param>
        private void coletaItens(Int32 ID_venda)
        {
            String SQL = "SELECT Saída.* FROM Saída WHERE (((Saída.cod_sai)="+ ID_venda +"));";
            Item_venda item;
            Recordset rs_dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (rs_dados.EOF || rs_dados.BOF)
            {
                throw new ArgumentException("Id de venda inválida ou não registrada.");
            }
            this.itens.Clear();
            while (!(rs_dados.BOF || rs_dados.EOF))
            {
                item = new Item_venda(Convert.ToInt32(rs_dados.Fields["cont"].Value.ToString()));
                this.itens.Add(item);
                rs_dados.MoveNext();
            }

            rs_dados.Close();
            conex.Close();

        }
        /// <summary>
        /// Método para salvar os dados ou atualiza-los na tabela do bancos de dados 
        /// Processo tem tratamento de erro caso o total ainda não esteja finalizado
        /// </summary>
        /// <returns>Retorna um boolean confirmando a gravação</returns>
        public Boolean Salvar()
        {
            if (this.Total != (this.Especie + this.Cheque + this.Cartao + this.Vale))
            { 
                throw new ArgumentException("Venda ainda não foi concluida, fechada."); 
            }

            String SQL = (this.ID == 0) ? "SELECT Cod_sai.* FROM Cod_sai;" : "SELECT Cod_sai.* FROM Cod_sai WHERE (((Cod_sai.Cod_sai)=" + this.ID + "));";
            Recordset rs_dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            try
            {
             
                rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                if (this.ID == 0) { rs_dados.AddNew(); }
                rs_dados.Fields["cx"].Value = this.ID_caixa;
                rs_dados.Fields["cod_cliente"].Value = this.ID_cliente;
                rs_dados.Fields["OP"].Value = this.ID_vendedor;
                rs_dados.Fields["especie"].Value = this.Especie;
                rs_dados.Fields["cheque"].Value = this.Cheque;
                rs_dados.Fields["vale"].Value = this.Vale;
                rs_dados.Fields["cartao"].Value = this.Cartao;
                rs_dados.Fields["dado1"].Value = this.orcamentoSmall;
                rs_dados.Update();
                this.ID = Convert.ToInt32(rs_dados.Fields["cod_sai"].Value);

            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }

            return true;
        }
        
        /// <summary>
        /// Método salva um item especifico da venda 
        /// Tratamento pelo número do Incice
        /// </summary>
        /// <param name="Index">Interiro que contém o n° do Indice</param>
        public void salvarItem(Int32 Index, Item_venda item) 
        {
            if (vendaFechada) { throw new ArgumentException("Item não pode ser modificado ou salvo com venda fechada."); }
            if (Index >= itens.Count  || Index < 0) { throw new ArgumentException("Index fora de seqüência."); }

            itens[Index] = item;
            itens[Index].Salvar();
            
        }
        /// <summary>
        /// Método retorna obejto do tipo item_venda baseado no index de argumento.
        /// </summary>
        /// <param name="Index">Inteiro dentro do intervalo</param>
        /// <returns>Retorna objeto item_venda baseado no Indice informado na entrada</returns>
        public Item_venda getItem(Int32 Index)
        {
            if (Index >= itens.Count  || Index < 0) { throw new ArgumentException("Index fora de seqüência."); }

            return itens[Index];
        }

        /// <summary>
        /// Método retorna o número de itens cadastrado na venda 
        /// </summary>
        /// <returns>Inteiro com o número de itens na venda.</returns>
        public Int32 countItens() { return itens.Count; }

        /// <summary>
        /// Método que exclui a venda como também desfaz os lançamentos de cada item repondo as quantidades em estoque.
        /// </summary>
        /// <returns></returns>
        public bool Excluir(Int32 ID_caixa)
        {

            if (this.ID_caixa != ID_caixa) { throw new ArgumentException("Não é permitido excluir uma venda de um caixa já fechado!"); }
            
            if (vendaFechada == false) { throw new ArgumentException("Não é peritido excluir venda estando aberta."); }

            //Excluindo a venda 
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getDb4();
            
            String SQL = "DELETE Cod_sai.Cod_sai FROM Cod_sai WHERE (((Cod_sai.Cod_sai)="+this.ID+"));";

            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            //Excluindo itens e gravando nos produtos
            foreach (var item in itens)
            {
                item.Excluir();
            }

            rsDados.Close();
            conex.Close();

            return true;
        }

        /// <summary>
        /// Método tem como objetivo adicionar um item a lista de itens vendidos da venda. Esse item só deve ser salvo após a venda ser salva.
        /// </summary>
        /// <param name="item">Objeto do tipo venda para ser adicionado na lista de itens vendidos</param>
        /// <returns>Retorna boolean confirmando a adição do item</returns>
        public bool add_item(Item_venda item)
        {
            if (vendaFechada) { throw new ArgumentException("Venda já fechada."); }
            this.itens.Add(item);
            return true;
        }
        /// <summary>
        /// Método tem como obetivo excluir um item da lista de itens, caso o mesmo já tenha sido gravado  do banco de dados ele também exclui da lista do banco de dados.
        /// </summary>
        /// <param name="ID_venda">Inteiro que deve conter o ID da venda a ser excluida</param>
        /// <returns>Retorna um boolean para confirmar sua remoção.</returns>
        public bool del_item(Int32 index)
        {
            if (vendaFechada) { throw new ArgumentException("Venda já fechada."); }
            itens[index].Excluir();
            itens.Remove(itens[index]);
            return true;
        }

        override public String ToString()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Processo que conclui a venda, assim a mesma não pode mais ter itens adicionados.
        /// </summary>
        /// <param name="Especie">Double com o valor em espécie desta venda</param>
        /// <param name="Cheque">Double com o valor em cheque desta venda</param>
        /// <param name="Vale">Double com o valor em vale desta venda</param>
        /// <param name="Cartao">Double com o valor em cartão desta venda</param>
        /// <param name="ID_loja">Inteiro com o valor da loja ser vendida, 0 para todas as lojas</param>
        public void fecharVenda(Double Especie, Double Cheque, Double Vale, Double Cartao, Int32 ID_loja)
        {
            if (vendaFechada) { throw new ArgumentException("Venda já fechada."); }

            if (Especie < 0)
            {
                throw new ArgumentException("Não é permitido valores negativo para espécie.");
            }

            if (Cheque < 0)
            {
                throw new ArgumentException("Não é permitido valores negativo para cheque.");
            }

            if (Vale < 0)
            {
                throw new ArgumentException("Não é permitido valores negativo para vale.");
            }

            if (Cartao < 0)
            {
                throw new ArgumentException("Não é permitido valores negativo para cartão.");
            }

            this.Especie = Especie;
            this.Cheque = Cheque;
            this.Vale = Vale;
            this.Cartao = Cartao;
            this.Total = Especie + Cheque + Vale + Cartao;
            Salvar();
            //salvarItens();
            this.vendaFechada = true;
            //processo de fechamento small.
            salvaSmall(ID_loja);
        }
        /// <summary>
        /// Metodo que verifica e retona o n° do orçamento no Small
        /// </summary>
        /// <returns>Retorna uma string de 10 dígitos com o n° do Orçamento do Small</returns>
        public String getOrcamentoSmall()
        {
            return this.orcamentoSmall;
        }

        /// <summary>
        /// Método retorna String contendo Nf de venda lançada no Small
        /// </summary>
        /// <returns>String contendo NF de venda</returns>
        public String getNFSmall()
        {
            String retorno = "";
            String SQL = "SELECT ORCAMENT.* FROM ORCAMENT WHERE (((ORCAMENT.PEDIDO)='"+this.orcamentoSmall +"'));";
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getSmall();
            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if( !(rsDados.EOF || rsDados.BOF))
            {
                retorno = rsDados.Fields["NUMERONF"].Value;
            }
            rsDados.Close();
            conex.Close();
            return retorno;
        }
        /// <summary>
        /// Metodo para adição dos itens no orçamento do small
        /// </summary>
        /// <param name="ID_loja">ID da loja a ser vendida, paramentro 0 para todas as lojas</param>
        private void salvaSmall(Int32 ID_loja)
        {
            String pedidoZero = "0000000000";
            String lancamentoZero = "0000000000";
            bool gravado = false;
            Int32 pedido, lancamento;

            ///Processo do banco de dados
            Recordset rsdados = new Recordset();
            Connection conex = new Conexao().getSmall();
            String SQL = "";
            rsdados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            rsdados.MoveLast();
           
            ///Processo de coleta de numeração de registro
            pedido = Convert.ToInt32(rsdados.Fields["PEDIDO"].Value.ToString());
            pedido++;
            lancamento = Convert.ToInt32(rsdados.Fields["REGISTRO"].Value.ToString());
            lancamento++;
            pedidoZero += pedido.ToString();
            lancamentoZero += lancamentoZero.ToString();
            pedidoZero = new SIME.Class.Uteis().direita(pedidoZero, 9);
            lancamentoZero = new SIME.Class.Uteis().direita(lancamentoZero, 9);
            
            ///Adição de registro
            foreach (var item in itens)
            {
                if (item.getID_loja() == ID_loja || item.getID_loja() == 0)
                {
                    rsdados.AddNew();
                    rsdados.Fields["CODIGO"].Value = item.getID_produto().ToString();
                    rsdados.Fields["DESCRICAO"].Value = new SIME.Class.Uteis().esquerda(item.getDescricao().ToString(), 44);
                    rsdados.Fields["QUANTIDADE"].Value = item.getQuantidade().ToString();
                    rsdados.Fields["UNITARIO"].Value = item.getUnitario().ToString("N");
                    rsdados.Fields["TOTAL"].Value = (item.getQuantidade() * item.getUnitario()).ToString("N");
                    rsdados.Fields["DATA"].Value = DateTime.Now.ToShortDateString();
                    rsdados.Fields["TIPO"].Value = "ORCAME";
                    rsdados.Fields["PEDIDO"].Value = pedidoZero;
                    rsdados.Fields["CLIFOR"].Value = "MOVIMENTAÇÃO DIÁRIA";//Modificar futuramente para procurar o cliente em small
                    rsdados.Fields["REGISTRO"].Value = lancamentoZero;
                    rsdados.Update();
                    if (gravado == false)
                    { 
                        this.orcamentoSmall = pedidoZero;
                        this.Salvar();
                        gravado = true;
                    }
                }
                rsdados.Close();
                conex.Close();
            }

        }
    }
}
