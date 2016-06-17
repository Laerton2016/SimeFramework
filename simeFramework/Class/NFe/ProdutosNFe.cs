using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;

namespace WindowsFormsApplication2
{
    class ProdutosNFe
    {
        private List<String> dados;
        private List<List<String>> produtos;

        public ProdutosNFe(List<String> dados) {
            this.dados = dados;
            produtos = new List<List<String>>();
            analizaDados(this.dados);
            Console.WriteLine(produtos.Count);

        }
        /// <summary>
        /// Metodo que extrai da lista, sub-listas com os dados de cada produto agrupado falicitando assim o processo de analise
        /// </summary>
        /// <param name="dados">Lista de String contedo os dados dos produtos</param>
        private void analizaDados(List<String> dados)
        {
            Boolean grava = false;
            int Q = dados.Count;
            String campo = "";
            List<String> produto = new List<String>();

            for (int i = 0; i < Q; i++) {
                if (grava) {
                    produtos.Add(new List <String >( produto));
                    grava = false;
                    produto.Clear();
                }
                produto.Add(dados[i].ToString());
                if (i < (Q - 1)) {
                    campo = dados[i + 1].ToString().Split(new Char[] { ':' })[0];
                    if (campo.Equals("cProd")){
                        grava = true ;
                    }
                }
                
                if (i == (Q - 2)) { // Para o caso do último item
                    grava = true;
                }
                
            }
        }

        public String getDescricaoProduto(int idProduto) {
            if (idProduto > getQuantidade()) return null; 

            return retornoDado("xProd", idProduto);
        }

        public String getNCM(int idProduto)
        {
            if (idProduto > getQuantidade()) return null;

            return retornoDado("NCM", idProduto);
        }


        public Int16 getQuantidade() {
            return Convert.ToInt16 (produtos.Count);
        }

        public String getUnd(int idProduto)
        {
            if (idProduto > getQuantidade()) return null;

            return retornoDado("uCom", idProduto);
        }

        public String getCodProduto(int idProduto)
        {
            if (idProduto > getQuantidade()) return null;

            return retornoDado("cProd", idProduto);
        }

        public Double  getAiqICMS(int idProduto)
        {
            if (idProduto > getQuantidade()) return 0;


            String dadoRetorno = retornoDado("pICMS", idProduto);
            dadoRetorno = dadoRetorno.Replace('.', ',');
            return Convert.ToDouble(dadoRetorno);
        }

        public Double getValorICMSST(int idProduto) 
        {
            String dadoRetorno = retornoDado("vICMSST", idProduto);
            dadoRetorno = dadoRetorno.Replace('.', ',');

            return (dadoRetorno.Equals("")) ? 0 : Convert.ToDouble(dadoRetorno);
        }

        public Double getAiqIPI(int idProduto)
        {
            if (idProduto > getQuantidade()) return 0;

            String dadoRetorno = retornoDado("pIPI", idProduto);
            dadoRetorno = dadoRetorno.Replace('.', ',');

            return(dadoRetorno.Equals(""))?0: Convert.ToDouble(dadoRetorno);
        }

        public Double getQuantidadeProduto(int idProduto)
        {
            if (idProduto > getQuantidade()) return 0;

            String dadoRetorno = retornoDado("qCom", idProduto);
            dadoRetorno = dadoRetorno.Replace('.', ',');
            return Convert.ToDouble(dadoRetorno);
            
        }

        public Double getValorUnitario(int idProduto)
        {
            if (idProduto > getQuantidade()) return 0;

            String dadoRetorno = retornoDado("vUnCom", idProduto);
            dadoRetorno = dadoRetorno.Replace('.', ',');
            return Convert.ToDouble(dadoRetorno);//Convert.ToDouble(retornoDado("cUnCom", idProduto).ToString().Replace('.', ','));
        }

        override public String ToString() {
            String retorno = "QUANTIDADE DE INTES: ";
            Int16 Q = Convert.ToInt16(produtos.Count);
            retorno += getQuantidade() + "\n";
            for (int I = 0; I < Q; I++) {
                retorno += "\nCOD: " + getCodProduto(I) + " - " + "DESCRIÇÃO: " + getDescricaoProduto(I) +
                "\nEAN: " + getEAN(I) + " NCM: " + getNCM(I) + " CFOP: " + getCFOP(I) +
                "\nVALOR: " + getValorUnitario(I) + " QUANT.:" + getQuantidadeProduto(I) +
                "\nALIQ. ICMS: " + getAiqICMS(I) + " ALIQ. IPI: " + getAiqIPI(I) + " UNID.: " + getUnd(I);
            }
            return retorno;
        }

        /**
        public List<SIME.Class.Produto> getListaProdutos() 
        {
            List<SIME.Class.Produto> lista = new List<SIME.Class.Produto>();
            Int32 id = 0;
            SIME.Class.Produto item;
            Connection conex =  new SIME.Conexao().getDb4();

            for (int i = 0; i < produtos.Count; i++)
            {
                //sistema fara uma busca pelo EAN para encontrar o código
                id = BuscaProdutopporEAN(getEAN(i));
                if (id == 0)
                {
                    item = new SIME.Class.Produto(conex);
                    
                }
                else
                {
                    item = new SIME.Class.Produto(id,conex);
                    System.Threading.Thread.Sleep(1000);
                    
                }

                Console.WriteLine(i);
                
                item.setCodFabricante (getCodProduto(i));
                item.setCusto(getValorUnitario(i));
                String descricao = getDescricaoProduto(i);
                if (descricao.Length > 45) 
                {
                    descricao = (new SIME.Class.Uteis()).esquerda(descricao, 44);
                }
                item.setDescricao(descricao);
                item.setEAN(getEAN(i));
                item.setICMSCusto(getAiqICMS(i));
                item.setNCM(getNCM(i));
                item.settaxaIPI(getAiqIPI(i));

                lista.Add(item);
            }
            return lista;
        
        }
         **/

        private Int32 BuscaProdutopporEAN(String EAN) 
        {
            String SQL = "SELECT PRODUTOS.Cod FROM PRODUTOS WHERE (((PRODUTOS.Codbarras)='"+EAN+"')); ";
            Recordset rsDados = new Recordset();
            Connection conex = new SIME.Conexao().getDb4();
            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (rsDados.BOF || rsDados.EOF) 
            {
                return 0;
            }
            return Convert.ToInt32(rsDados.Fields["cod"].Value); 
        }

        public String getCFOP(int idProduto)
        {
            if (idProduto > getQuantidade()) return null;

            return retornoDado("CFOP", idProduto);
        }
        public String getEAN(int idProduto)
        {
            if (idProduto > getQuantidade()) return null;

            return retornoDado("cEAN", idProduto);
        }
        private String retornoDado(String chave, int idProduto)
        {
            String retorno = "";
            String campo = "";
            Boolean para = false;
            int cont = 0, Q = produtos[idProduto].Count - 1;
            while (!para && cont != Q)
            {
                campo = produtos[idProduto][cont].ToString().Split(new Char[] { ':' })[0];
                if (campo.Equals(chave))
                {
                    retorno = produtos[idProduto][cont].ToString().Split(new Char[] { ':' })[2];
                    para = true;
                }
                cont++;
            }

            return retorno;
        }

        public Int32 Count() { return produtos.Count; }
    }
}
