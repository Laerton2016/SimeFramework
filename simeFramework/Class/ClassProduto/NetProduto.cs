using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.ProdutoClass
{
    /// <summary>
    /// Class que cuida dos dados de um produto
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>15/01/2016</Data>
    /// </summary>
    public class NetProduto
    {
        private Int32 _ID;
        private String descricao;
        private String complemento;
        private String codFabricante;
        private Double custo;
        private Double _ICMSCusto;
        private Double taxaFrete;
        private Double taxaIPI;
        private Int32 idGrupo;
        private Double valorVenda;
        private String imagem;
        private Boolean art33;
        private Double peso;
        private String _EAN;
        private Boolean descontinuado;
        private Int32 idRegra;
        private Double txLucroMaximo;
        private Double txLucroMinimo;
        private Int32 quantMinima;
        private Int32 idMedida;
        private Double txDesconto;
        private String politicaVenda;
        private String _NCM;
        private Int32 quantEstoque;
        private Double valorVendaDesconto;
        private Double txComissao;
        /// <summary>
        /// Cria um objeto produto
        /// </summary>
        public NetProduto()
        {
            _ID = 0;
            descricao = "";
            complemento = "";
            codFabricante = "";
            custo = 0;
            _ICMSCusto = 0;
            taxaFrete = 0;
            taxaIPI = 0;
            idGrupo = 0;
            valorVenda = 0;
            imagem = " ";
            art33 = false;
            peso = 0;
            _EAN = "";
            descontinuado = false;
            idRegra = 0;
            txLucroMaximo = 0;
            txLucroMinimo = 0;
            quantMinima = 0;
            idMedida = 0;
            txDesconto = 0;
            politicaVenda = "";
            _NCM = "";
            quantEstoque = 0;
            valorVendaDesconto = 0;
            txComissao = 0;

        }
        /// <summary>
        /// Id do produto
        /// </summary>
        public int ID
        {
            get
            {
                return _ID;
            }

            set
            {
                _ID = value;
            }
        }
        /// <summary>
        /// Descrição do produto
        /// </summary>
        public string Descricao
        {
            get
            {
                return descricao;
            }

            set
            {
                descricao = value;
            }
        }
        /// <summary>
        /// Informações complementares de um produto
        /// </summary>
        public string Complemento
        {
            get
            {
                return complemento;
            }

            set
            {
                complemento = value;
            }
        }
        /// <summary>
        /// Código do fabricante
        /// </summary>
        public string CodFabricante
        {
            get
            {
                return codFabricante;
            }

            set
            {
                codFabricante = value;
            }
        }
        /// <summary>
        /// Valor de custo de um produto
        /// </summary>
        public double Custo
        {
            get
            {
                return custo;
            }

            set
            {
                custo = value;
            }
        }
        /// <summary>
        /// Taxa de custo de ICMS da mercadoria
        /// </summary>
        public double ICMSCusto
        {
            get
            {
                return _ICMSCusto;
            }

            set
            {
                _ICMSCusto = value;
            }
        }
        /// <summary>
        /// Taxa de frete que referenciava  a mercadoria
        /// </summary>
        public double TaxaFrete
        {
            get
            {
                return taxaFrete;
            }

            set
            {
                taxaFrete = value;
            }
        }
        /// <summary>
        /// Taxa de ipi que inside sobre a mercadoria
        /// </summary>
        public double TaxaIPI
        {
            get
            {
                return taxaIPI;
            }

            set
            {
                taxaIPI = value;
            }
        }
        /// <summary>
        /// Id do grupo que de pertence o produto
        /// </summary>
        public int IdGrupo
        {
            get
            {
                return idGrupo;
            }

            set
            {
                idGrupo = value;
            }
        }
        /// <summary>
        /// Valor de venda do produto
        /// </summary>
        public double ValorVenda
        {
            get
            {
                return valorVenda;
            }

            set
            {
                valorVenda = value;
            }
        }
        /// <summary>
        /// Endereço da imagem do produto
        /// </summary>
        public string Imagem
        {
            get
            {
                return imagem;
            }

            set
            {
                imagem = value;
            }
        }
        /// <summary>
        /// Informa se o produto incide sobre o artigo 33
        /// </summary>
        public bool Art33
        {
            get
            {
                return art33;
            }

            set
            {
                art33 = value;
            }
        }
        /// <summary>
        /// Peso da mercadoria
        /// </summary>
        public double Peso
        {
            get
            {
                return peso;
            }

            set
            {
                peso = value;
            }
        }
        /// <summary>
        /// Código de barras padrão EAN
        /// </summary>
        public string EAN
        {
            get
            {
                return _EAN;
            }

            set
            {
                _EAN = value;
            }
        }
        /// <summary>
        /// Informa se o produto foi descontinuado
        /// </summary>
        public bool Descontinuado
        {
            get
            {
                return descontinuado;
            }

            set
            {
                descontinuado = value;
            }
        }
        /// <summary>
        /// Informa o Id da regra do produto
        /// </summary>
        public int IdRegra
        {
            get
            {
                return idRegra;
            }

            set
            {
                idRegra = value;
            }
        }
        /// <summary>
        /// Informa a taxa de lucro máximo da mercadoria
        /// </summary>
        public double TxLucroMaximo
        {
            get
            {
                return txLucroMaximo;
            }

            set
            {
                txLucroMaximo = value;
            }
        }
        /// <summary>
        /// Informa a taxa de lucro mínimo da mercadoria
        /// </summary>
        public double TxLucroMinimo
        {
            get
            {
                return txLucroMinimo;
            }

            set
            {
                txLucroMinimo = value;
            }
        }
        /// <summary>
        /// Informa a quantidade mínima que deve ser mantido em estoque
        /// </summary>
        public int QuantMinima
        {
            get
            {
                return quantMinima;
            }

            set
            {
                quantMinima = value;
            }
        }
        /// <summary>
        /// Id da medidad a ser usado
        /// </summary>
        public int IdMedida
        {
            get
            {
                return idMedida;
            }

            set
            {
                idMedida = value;
            }
        }
        /// <summary>
        /// Taxa de desconto
        /// </summary>
        public double TxDesconto
        {
            get
            {
                return txDesconto;
            }

            set
            {
                txDesconto = value;
            }
        }
        /// <summary>
        /// Politica de venda sobre o produto
        /// </summary>
        public string PoliticaVenda
        {
            get
            {
                return politicaVenda;
            }

            set
            {
                politicaVenda = value;
            }
        }
        /// <summary>
        /// Código fiscal NCM 
        /// </summary>
        public string NCM
        {
            get
            {
                return _NCM;
            }

            set
            {
                _NCM = value;
            }
        }
        /// <summary>
        /// Quantida em estoque atualmente de uma mercadoria.
        /// </summary>
        public int QuantEstoque
        {
            get
            {
                return quantEstoque;
            }

            set
            {
                quantEstoque = value;
            }
        }
        /// <summary>
        /// Valores de venda com desconto
        /// </summary>
        public double ValorVendaDesconto
        {
            get
            {
                return valorVendaDesconto;
            }

            set
            {
                valorVendaDesconto = value;
            }
        }
        /// <summary>
        /// Taxa de comissão sobre o produto
        /// </summary>
        public double TxComissao
        {
            get
            {
                return txComissao;
            }

            set
            {
                txComissao = value;
            }
        }
        public override string ToString()
        {
            return Descricao.ToUpper();
        }
    }
    
}