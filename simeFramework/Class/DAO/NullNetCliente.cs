using SIME.Class.primitivo;
using simeFramework.Class.primitivo;
using System;
using System.Collections.Generic;

namespace SIME.Class.DAO
{
    public class NullNetCliente : NetCliente
    {
        /// <summary>
        /// Codigo do cliente - ID
        /// </summary>
        public int Cod
        {
            get
            {
                return 0;
            }

            set
            {
                
            }
        }
        /// <summary>
        /// Nome do cliente
        /// </summary>
        public string Nome
        {
            get
            {
                return "CLIENTE NÃO LOCALIZADO!";
            }

            set
            {
                
            }
        }
        /// <summary>
        /// Endereço do cliente
        /// </summary>
        public string End
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Telefone do cliente
        /// </summary>
        public string Telefone
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Nome da operadora do telefone do cliente
        /// </summary>
        public string Operadora
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// CPF ou CNPJ do cliente
        /// </summary>
        public string Cpfcnpj
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// E-mail do cliente
        /// </summary>
        public string Email
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Cep do endereço do cliente
        /// </summary>
        public string Cep
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Nome da cidade do cliente
        /// </summary>
        public string Cidade
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Uf do estado
        /// </summary>
        public string UF
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Inscrição estadual
        /// </summary>
        public string IE
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Ponto de referência do cliente
        /// </summary>
        public string Referencia
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Bairro do cliente
        /// </summary>
        public string Bairro
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Cliente que utiliza processo de mala direta
        /// </summary>
        public bool Mala
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Se cliente pessoa juridida true fisica false
        /// </summary>
        public bool PessoaJuridica
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

        /// <summary>
        /// Informa se o cliente está bloqueado 
        /// </summary>
        public bool Restrito
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Data que foi cadastrado o cliente no banco de dados
        /// </summary>
        /// <returns></returns>
        public DateTime GetDataCadastro()
        {
            return DateTime.Now;
        }
        /// <summary>
        /// Classificação do cliente
        /// </summary>
        public string Classificacao
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Nome do pai
        /// </summary>
        public string Pai
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Nome da mãe
        /// </summary>
        public string Mae
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Endereço dos pais
        /// </summary>
        public string EndPais
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Data de nascimento para cliente pessoa física e fundação para Jurídica
        /// </summary>
        public DateTime Datanascimento
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Valor limite para crédito do cliente
        /// </summary>
        public double Limite
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Cliente fidelizado
        /// </summary>
        public bool Fidelizado
        {
            get
            {
                return "";
            }

            set
            {

            }
        }
        /// <summary>
        /// Data que o cliente foi fidelizado
        /// </summary>
        public DateTime DataFidelizacao
        {
            get
            {
                return DateTime.Now;
            }

            set
            {

            }
        }
        /// <summary>
        /// Informações de credito de credito
        /// </summary>
        internal List<NetInformacaoCredito> Informacoes
        {
            get
            {
                return new List<NetInformacaoCredito>();
            }

            set
            {
                
            }
        }
        /// <summary>
        /// Contatos do cliente
        /// </summary>
        public List<NetContatosCliente> Contatos
        {
            get
            {
                return new List<NetContatosCliente>();
            }

            set
            {
                
            }
        }
        /// <summary>
        /// Locais de compra do cliente
        /// </summary>
        public List<NetIndicacoesCliente> Indicacoes
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

    }
}