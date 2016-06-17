using SIME.Class;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simeFramework.Class.primitivo
{
    /// <summary>
    /// Classe cuida dos dadosd e um fornecedor
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Date>05/06/2016</Date>
    /// </summary>
    public class NetForncedor
    {
        private Int32 _id;
        private String _nome;
        private String _razao;
        private String _CNPJ;
        private String _IE;
        private String _Endereco;
        private String _cidade;
        private String _UF;
        private String _CEP;
        private String _bairro;
        private Int32 _numero;
        private List<NetContatosFornecedor> _contatos;

        public NetForncedor()
        {
            _id = 0;
            _nome = "";
            _razao = "";
            _CNPJ = "";
            _IE = "";
            _Endereco = "";
            _cidade = "";
            _UF = "";
            _CEP = "";
            _bairro = "";
            _numero = 0;
            _contatos = new List<NetContatosFornecedor>();

        }
        public int Id
        {
            get
            {
                return _id;
            }

            set
            {
                _id = value;
            }
        }

        public string Nome
        {
            get
            {
                return _nome;
            }

            set
            {
                _nome = value;
            }
        }

        public string Razao
        {
            get
            {
                return _razao;
            }

            set
            {
                _razao = value;
            }
        }

        public string CNPJ
        {
            get
            {
                return _CNPJ;
            }

            set
            {
                _CNPJ = value;
            }
        }

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

        public string Endereco
        {
            get
            {
                return _Endereco;
            }

            set
            {
                _Endereco = value;
            }
        }

        public string Cidade
        {
            get
            {
                return _cidade;
            }

            set
            {
                _cidade = value;
            }
        }

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

        public string CEP
        {
            get
            {
                return _CEP;
            }

            set
            {
                _CEP = value;
            }
        }

        public string Bairro
        {
            get
            {
                return _bairro;
            }

            set
            {
                _bairro = value;
            }
        }

        public int Numero
        {
            get
            {
                return _numero;
            }

            set
            {
                _numero = value;
            }
        }

        internal List<NetContatosFornecedor> Contatos
        {
            get
            {
                return _contatos;
            }

            set
            {
                _contatos = value;
            }
        }
    }
}
