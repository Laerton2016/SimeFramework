using System;
using System.Web;
using System.Collections.Generic;
//using System.Windows.Forms;
using System.Linq;
using System.Text;
using System.Xml;
using MSXML2 ;

namespace WindowsFormsApplication2
    {
    class LeituraXML
    {
        private String Caminho;
        private XmlTextReader leitura;
        private String tipo = "";
        private String informe = "";
        private int cont = 0; //Remover a linha depois
        private FornecedorNFe dadosFornecedor;
        private Nota dadosNota;
        private FormaPagamento dadosFormas;
        private ProdutosNFe dadosProdutos;
        private String chave;

        /// <summary>
        /// Classe que coleta os dados de um arquivo XML de uma nota fiscal eletronica que retorna em seus metodos os dados da nota como 
        /// fornecedores, produtos, dados do destinatário, etc...
        /// </summary>
        /// <param name="Caminho">Recebe um arquivo XML do tipo Nfe</param>
        public LeituraXML(String Caminho) {
            this.Caminho = Caminho;
            leitura = new XmlTextReader(Caminho);
            coletaDados();
        }

        private void coletaDados() {
        List<String> listFornecedor = new List<String>();
        List<String> listNota = new List<String>();
        List<String> listaForma = new List<String>();
        List<String> listaProdutos = new List<String>();
        List<String> listaNFe = new List<string>();
            while (leitura.Read())
            {
                coletaDadosNFe(leitura, listNota, "ide");
                coletaDadosNFe(leitura, listFornecedor, "emit");
                coletaDadosNFe(leitura, listaForma, "dup");
                coletaDadosNFe(leitura, listaProdutos, "prod");
                coletaDadosNFe(leitura, listaNFe, "chNFe");
            }

            dadosFornecedor = new FornecedorNFe(listFornecedor);
            dadosNota = new Nota(listNota);
            dadosFormas = new FormaPagamento(listaForma);
            dadosProdutos = new ProdutosNFe(listaProdutos);
            //String[] informe = listaNFe[0].Split(':');
            this.chave = (listaNFe[0].Split(':'))[2];
        }

        public String getChave() { return this.chave; }

        /// <summary>
        /// Metodo que coleta os dados do arquivo de NFe baseado em uma chave identificadora
        /// </summary>
        /// <param name="objXML">Objeto tipo XmlTextReader</param>
        /// <param name="lista">Objeto tipo List de String</param>
        /// <param name="campo">Objeto tipo String que contem o campo identificador para pesquisa</param>
        private void coletaDadosNFe(XmlTextReader objXML, List<String> lista, String campo)
        {
            //Coletando dados para o fornecedor
            if (objXML.Name.Equals(campo) && (!tipo.Equals(campo)))
            {
                tipo = campo;
            }
            else if (objXML.Name.Equals(campo) && (tipo.Equals(campo)))
            {
                tipo = "";
            }

            if (tipo.Equals(campo) && (!objXML.Name.Equals(campo)))
            {

                if (!objXML.Value.Equals(""))
                {
                    lista.Add(informe + ":" + objXML.Value);
                    informe = "";
                }
                else
                {
                    informe = objXML.Name + ":";
                }
                
            }

            if (campo.Equals("prod"))
            {
                coletaDadosNFe(objXML, lista, "imposto");
            }
        }
        /// <summary>
        /// Metodo retorna os dados do Fornecedor do Arquivo XML 
        /// </summary>
        /// <returns>Objeto do tipo fornecedor</returns>
        public FornecedorNFe getFornecedor() {
            return dadosFornecedor;
        }
        /// <summary>
        /// Metodo que retorna um objeto do tipo Nota com os dados da Nfe repassada 
        /// </summary>
        /// <returns>Objeto do tipo Nota</returns>
        public Nota getNota() {
            return dadosNota;
        }

        /// <summary>
        /// Metodo que retorna as formas de pagamento da NFe Informada
        /// </summary>
        /// <returns>Objeto do tippo FormaPagamnto</returns>
        public FormaPagamento getFormasPagamento() {
            return dadosFormas;
        }
        /// <summary>
        /// Metodo que retorna os itens que estão na NFe informada
        /// </summary>
        /// <returns>Retorna um objeto do tipo ProdutosNfe</returns>
        public ProdutosNFe getProdutosNfe() {
            return dadosProdutos;
        }

    }
}
