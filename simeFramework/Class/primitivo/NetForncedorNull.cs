using simeFramework.Class.primitivo;
using System.Collections.Generic;

namespace simeFramework.Class.primitivo
{
    public class NetForncedorNull : NetForncedor
    {
        public int Id
        {
            get
            {
                return 0;
            }

            set
            {
                
            }
        }

        public string Nome
        {
            get
            {
                return "FORNECEDOR NÃO LOCALIZADO!";
            }

            set
            {
                
            }
        }

        public string Razao
        {
            get
            {
                return "";
            }

            set
            {
                
            }
        }

        public string CNPJ
        {
            get
            {
                return "";
            }

            set
            {
                
            }
        }

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

        public string Endereco
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

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

        public string CEP
        {
            get
            {
                return "";
            }

            set
            {

            }
        }

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

        public int Numero
        {
            get
            {
                return 0;
            }

            set
            {
                
            }
        }

        public List<NetContatosFornecedor> Contatos
        {
            get
            {
               
                return  new List<NetContatosFornecedor>();
            }

            set
            {
                
            }
        }
    }
}