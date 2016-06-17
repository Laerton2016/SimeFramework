using System;
using System.Threading;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using ADODB;

namespace SIME
{
    public class Item_venda
    {
        private Int32 ID = 0;
        private Int32 ID_venda = 0;
        private Int32 ID_produto = 0;
        private Int32 ID_fornecedor = 0;
        private String NF = "";
        private Int32 ID_loja = 0;
        private Int32 Quantidade = 0;
        private Double Unitario = 0;
        private Double Custo = 0;
        private string descricao = "";

        /// <summary>
        /// Classe que cria um objeto do tipo Item_de venda que trata-se de um item lançado em uma determinada venda vinculada pelo seu ID de venda.
        /// </summary>
        /// <param name="ID_venda">Inteiro cujo ID da veda deve ser > 0  e válido</param>
        /// <param name="ID_produto">Inteiro cujo ID identifica um determinado produto  que deve ter valor > 0 e válido</param>
        /// <param name="ID_fornecedor">Inteiro cujo ID identifica um determinado fornecedor que deve ter valor > 0 e válido</param>
        /// <param name="ID_loja">Inteiro cujo ID identifica uma determinada empresa vendedora que deve ter valor > 0 e válido</param>
        /// <param name="Quantidade">Inteiro positivo maior que Zero, represenata a quantidade lançada</param>
        /// <param name="Unitario">Double com duas casas decimais que deve conter o valor positivo e unitário do produto.</param>
        /// <param name="Custo">Double com duas casas decimais que deve conte o valor positivo e de custo do produto. </param>
        /// <param name="NF">String que identifica a NF de entrada</param>
        /// <param name="descricao">String que repassa a descricao do produto</param>
        public Item_venda (Int32 ID_venda, Int32 ID_produto, Int32 ID_fornecedor, Int32 ID_loja, Int32 Quantidade, Double Unitario, Double Custo, String NF, string descricao)
        {
            //Tratamento
            if (ID_venda <= 0) { throw new ArgumentException("ID de venda não pode ter valor <= 0"); }
            if (ID_produto <= 0) { throw new ArgumentException("ID do produto não pode ter valor <= 0"); }
            if (ID_fornecedor <= 0) { throw new ArgumentException("ID do fornecedor não pode ter valor <= 0"); }
            if (ID_loja <= 0) { throw new ArgumentException("ID da loja não pode ter valor <= 0"); }
            if (Quantidade <= 0) { throw new ArgumentException("Quantidade não pode ter valor <= 0"); }
            if (Unitario < 0) { throw new ArgumentException("Valor unitário não pode ter valor < 0"); }
            if (Custo < 0) { throw new ArgumentException("Valor unitário não pode ter valor < 0"); }
            if (NF.Replace(" ", "").Equals("")) { throw new ArgumentException("Nf não pode ter valor nulo ou vazio."); }

            this.ID_venda = ID_venda;
            this.ID_fornecedor = ID_fornecedor;
            this.ID_loja = ID_loja;
            this.ID_produto = ID_produto;
            this.Quantidade = Quantidade;
            this.Unitario = Unitario;
            this.Custo = Custo;
            this.NF = NF;
            try
            {
                Salvar();
            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }
        }
        /// <summary>
        /// Método coleta as informações e preenche o Objeto item de venda baseado no argumento ID de entrada
        /// </summary>
        /// <param name="ID">Inteiro contendo o ID do item</param>
        public Item_venda(Int32 ID) 
        {
            if (ID <= 0) { throw new ArgumentException("ID do item inválido."); }
            try
            {
                this.ID = ID;
                coletaDados();
            }
            catch (Exception e )
            {

                throw new ArgumentException(e.Message) ;
            }
        }

        /// <summary>
        /// Método salva o item da venda.
        /// </summary>
        /// <returns></returns>
        public Boolean Salvar()
        {
            
            String SQL = (this.ID == 0 )?"":"";
            Recordset rs_dados = new Recordset();
            Connection conex = new SIME.Conexao().getDb4();
            
            try
            {
                rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                if (this.ID == 0)
                {
                    rs_dados.AddNew();
                }
                rs_dados.Fields["Fornecedor"].Value = this.ID_fornecedor;
                rs_dados.Fields["Loja"].Value = this.ID_loja;
                rs_dados.Fields["[cod do cd]"].Value = this.ID_produto;
                rs_dados.Fields["NF"].Value = this.NF;
                rs_dados.Fields["Quantidade"].Value = this.Quantidade;
                rs_dados.Fields["Desconto"].Value = this.Unitario.ToString();
                rs_dados.Fields["Custo"].Value = this.Custo.ToString();
                rs_dados.Update();
                Thread.Sleep(500);
                this.ID = Convert.ToInt32(rs_dados.Fields["cont"].Value);
                rs_dados.Close();
                //Processo de dedução do estoque 
                SQL = "";
                rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                rs_dados.Fields["Estoque"].Value = Convert.ToInt32(rs_dados.Fields["Estoque"].Value) - this.Quantidade;
                rs_dados.Close();
                conex.Close();

            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }


            return true;
        }

        public Boolean Excluir()
        {
            if (this.ID == 0) { return false; }

            String SQL = "", SQLProduto= "";
            Recordset rs_dados = new Recordset();
            Connection conex = new SIME.Conexao().getDb4();

            try
            {
                rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                rs_dados.Open(SQLProduto, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                //Processo de adiciona ao estoque a quantidade vendida estornada
                rs_dados.Fields["Estoque"].Value = Convert.ToInt32(rs_dados.Fields["Estoque"].Value) + this.Quantidade;
                rs_dados.Close();
                conex.Close();
            }
            catch (Exception)
            {
                throw new ArgumentException("Não foi possível exluir o item.");
            }
            return true;
        }

        public Int32 getID() { return this.ID; }
        public Int32 getID_venda() { return this.ID_venda; }
        public Int32 getID_produto() { return this.ID_produto; }
        public Int32 getID_fornecedor() { return this.ID_fornecedor; }
        
        public Int32 getID_loja() { return this.ID_loja; }
        public Int32 getQuantidade() { return this.Quantidade; }
        public Double getUnitario() { return this.Unitario; }
        public Double getCusto() { return this.Custo; }
        public String getNF() { return this.NF; }
        public String getDescricao() { return this.descricao; }

        private void coletaDados() 
        {
            if (this.ID == 0) { return; }

            String SQL = "SELECT Saída.* FROM Saída WHERE (((Saída.cod_sai)=" + this.ID_venda + "));";
            Recordset rs_dados = new Recordset();
            Connection conex = new SIME.Conexao().getDb4();
            try
            {
                rs_dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                
                this.ID_fornecedor = Convert.ToInt32( rs_dados.Fields["Fornecedor"].Value);
                this.ID_loja = Convert.ToInt32(rs_dados.Fields["Loja"].Value);
                this.ID_produto = Convert.ToInt32(rs_dados.Fields["[cod do cd]"].Value);
                this.NF = rs_dados.Fields["NF"].Value;
                this.Quantidade = Convert.ToInt32(rs_dados.Fields["Quantidade"].Value);
                this.Unitario = Convert.ToDouble(rs_dados.Fields["Desconto"].Value.ToString("N"));
                this.Custo = Convert.ToDouble(rs_dados.Fields["Custo"].Value.ToString("N"));
                this.descricao = new SIME.Class.Produto(ID_produto, new SIME.Conexao().getDb4() ).getDescricao();
                rs_dados.Close();
                conex.Close();

            }
            catch (Exception e)
            {

                throw new ArgumentException(e.Message);
            }

        }

        public override string ToString()
        {
            return "ID = " + this.ID + "\n" + 
                "Descrição = " + this.descricao + "\n" + 
                "Cod = " + this.ID_loja +  "-" + this.ID_produto + "-" + this.ID_fornecedor + "-" + this.NF + "\n" +
                "Quantidade = " + this.Quantidade +  "\n" + 
                "Valor unitário = " + this.Unitario.ToString("N") + "\n" + 
                "Valor custo =" + this.Custo.ToString("N") ;
            
              
        }
    }
}
