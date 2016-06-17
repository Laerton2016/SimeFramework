using simeFramework.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.primitivo
{
    /// <summary>
    /// Classe cuida dos dados de um cliente
    /// <autor>Laerton Marques de Figueiredo</autor>
    /// <data>16/01/2016</data>
    /// </summary>
    public class NetCliente
    {
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
        private Int32 cod;
        private String nome, end, telefone, operadora, cpfcnpj, email, cep, cidade, _UF, _IE, referencia, bairro;
        private Boolean mala, pessoaJuridica, restrito;
        private DateTime dataCadastro;

        private String classificacao, pai, mae, endPais;
        private DateTime datanascimento;
        private Double limite;
        private Boolean _fidelizado;
        private DateTime _dataFidelizacao;

        //Variáveis de dados 
        private List<NetInformacaoCredito> informacoes;
        private List<NetContatosCliente> contatos;
        private List<NetIndicacoesCliente> indicacoes;
        
        /// <summary>
        /// Cria um objeto cliente novo
        /// </summary>
        public NetCliente()
        {
            cod = 0;
            nome = ""; end = ""; telefone = ""; operadora = ""; cpfcnpj = ""; email = ""; cep = ""; cidade = ""; _UF = ""; _IE = ""; referencia = ""; bairro = "";
            mala = false; pessoaJuridica = false; restrito = false;
            dataCadastro = DateTime.Now;
            classificacao = ""; pai = ""; mae = ""; endPais = "";
            limite = 0;
            _fidelizado = false;
            Informacoes = new List<NetInformacaoCredito>();
            Contatos = new List<NetContatosCliente>();
            Indicacoes = new List<NetIndicacoesCliente>();

        }
        /// <summary>
        /// Codigo do cliente - ID
        /// </summary>
        public int Cod
        {
            get
            {
                return cod;
            }

            set
            {
                cod = value;
            }
        }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome
        {
            get
            {
                return nome;
            }

            set
            {
                nome = value;
            }
        }
        /// <summary>
        /// Endereço do cliente
        /// </summary>
        public string End
        {
            get
            {
                return end;
            }

            set
            {
                end = value;
            }
        }
        /// <summary>
        /// Telefone do cliente
        /// </summary>
        public string Telefone
        {
            get
            {
                return telefone;
            }

            set
            {
                telefone = value;
            }
        }
        /// <summary>
        /// Nome da operadora do telefone do cliente
        /// </summary>
        public string Operadora
        {
            get
            {
                return operadora;
            }

            set
            {
                operadora = value;
            }
        }
        /// <summary>
        /// CPF ou CNPJ do cliente
        /// </summary>
        public string Cpfcnpj
        {
            get
            {
                return cpfcnpj;
            }

            set
            {
                cpfcnpj = value;
            }
        }
        /// <summary>
        /// E-mail do cliente
        /// </summary>
        public string Email
        {
            get
            {
                return email;
            }

            set
            {
                email = value;
            }
        }
        /// <summary>
        /// Cep do endereço do cliente
        /// </summary>
        public string Cep
        {
            get
            {
                return cep;
            }

            set
            {
                cep = value;
            }
        }
        /// <summary>
        /// Nome da cidade do cliente
        /// </summary>
        public string Cidade
        {
            get
            {
                return cidade;
            }

            set
            {
                cidade = value;
            }
        }
        /// <summary>
        /// Uf do estado
        /// </summary>
        public string UF
        {
            get
            {
                return _UF;
            }

            set
            {
                _UF = value;
            }
        }
        /// <summary>
        /// Inscrição estadual
        /// </summary>
        public string IE
        {
            get
            {
                return _IE;
            }

            set
            {
                _IE = value;
            }
        }
        /// <summary>
        /// Ponto de referência do cliente
        /// </summary>
        public string Referencia
        {
            get
            {
                return referencia;
            }

            set
            {
                referencia = value;
            }
        }
        /// <summary>
        /// Bairro do cliente
        /// </summary>
        public string Bairro
        {
            get
            {
                return bairro;
            }

            set
            {
                bairro = value;
            }
        }
        /// <summary>
        /// Cliente que utiliza processo de mala direta
        /// </summary>
        public bool Mala
        {
            get
            {
                return mala;
            }

            set
            {
                mala = value;
            }
        }
        /// <summary>
        /// Se cliente pessoa juridida true fisica false
        /// </summary>
        public bool PessoaJuridica
        {
            get
            {
                return pessoaJuridica;
            }

            set
            {
                pessoaJuridica = value;
            }
        }
        
        /// <summary>
        /// Informa se o cliente está bloqueado 
        /// </summary>
        public bool Restrito
        {
            get
            {
                return restrito;
            }

            set
            {
                restrito = value;
            }
        }
        /// <summary>
        /// Data que foi cadastrado o cliente no banco de dados
        /// </summary>
        /// <returns></returns>
        public DateTime GetDataCadastro()
        {
            return dataCadastro;
        }
        /// <summary>
        /// Classificação do cliente
        /// </summary>
        public string Classificacao
        {
            get
            {
                return classificacao;
            }

            set
            {
                classificacao = value;
            }
        }
        /// <summary>
        /// Nome do pai
        /// </summary>
        public string Pai
        {
            get
            {
                return pai;
            }

            set
            {
                pai = value;
            }
        }
        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string Mae
        {
            get
            {
                return mae;
            }

            set
            {
                mae = value;
            }
        }
        /// <summary>
        /// Endereço dos pais
        /// </summary>
        public string EndPais
        {
            get
            {
                return endPais;
            }

            set
            {
                endPais = value;
            }
        }
        /// <summary>
        /// Data de nascimento para cliente pessoa física e fundação para Jurídica
        /// </summary>
        public DateTime Datanascimento
        {
            get
            {
                return datanascimento;
            }

            set
            {
                datanascimento = value;
            }
        }
        /// <summary>
        /// Valor limite para crédito do cliente
        /// </summary>
        public double Limite
        {
            get
            {
                return limite;
            }

            set
            {
                limite = value;
            }
        }
        /// <summary>
        /// Cliente fidelizado
        /// </summary>
        public bool Fidelizado
        {
            get
            {
                return _fidelizado;
            }

            set
            {
                _fidelizado = value;
            }
        }
        /// <summary>
        /// Data que o cliente foi fidelizado
        /// </summary>
        public DateTime DataFidelizacao
        {
            get
            {
                return _dataFidelizacao;
            }

            set
            {
                _dataFidelizacao = value;
            }
        }
        /// <summary>
        /// Informações de credito de credito
        /// </summary>
        internal List<NetInformacaoCredito> Informacoes
        {
            get
            {
                return informacoes;
            }

            set
            {
                informacoes = value;
            }
        }
        /// <summary>
        /// Contatos do cliente
        /// </summary>
        public List<NetContatosCliente> Contatos
        {
            get
            {
                return contatos;
            }

            set
            {
                contatos = value;
            }
        }
        /// <summary>
        /// Locais de compra do cliente
        /// </summary>
        public List<NetIndicacoesCliente> Indicacoes
        {
            get
            {
                return indicacoes;
            }

            set
            {
                indicacoes = value;
            }
        }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return Nome.ToUpper();
        }
    }
}
