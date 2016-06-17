using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.primitivo
{
    /// <summary>
    /// Classe trata dos dados de uma Ordem de serviço.
    /// </summary>
    public class NetOS
    {
        private Int64 _id, _idCliente, _idTipo, _idMarca, _idEstoque, _idAtendimento, _idUser, idRetorno, _idTecnico, _idUserRecebedor;
        private String _modelo, _serie, _nf, _loja, _defeito, _voltagem, _avaria, _acessorios;
        private bool _garantia, _arranhado, _retorno;
        private float _valor;
        private DateTime _dataNF, _abertura, _fechamento, _dtInicio, _horaIncio, _horaFim;

        /// <summary>
        /// Cria um objeto NetOS
        /// </summary>
        public NetOS()
        {
            _id = 0;
            _idCliente = 0;
            _idTipo = 0;
            _idMarca = 0;
            _idEstoque = 0;
            _idAtendimento = 0;
            _idUser = 0;
            idRetorno = 0;
            _idTecnico = 0;
            _idUserRecebedor = 0;
            _garantia = false; _arranhado = false; _retorno = false;
            _valor = 0;
            _modelo = ""; _serie = ""; _nf = ""; _loja = ""; _defeito = ""; _voltagem = ""; _avaria = ""; _acessorios = "";

        }

        public long Id
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

        public long IdCliente
        {
            get
            {
                return _idCliente;
            }

            set
            {
                _idCliente = value;
            }
        }

        public long IdTipo
        {
            get
            {
                return _idTipo;
            }

            set
            {
                _idTipo = value;
            }
        }

        public long IdMarca
        {
            get
            {
                return _idMarca;
            }

            set
            {
                _idMarca = value;
            }
        }

        public long IdEstoque
        {
            get
            {
                return _idEstoque;
            }

            set
            {
                _idEstoque = value;
            }
        }

        public long IdAtendimento
        {
            get
            {
                return _idAtendimento;
            }

            set
            {
                _idAtendimento = value;
            }
        }

        public long IdUser
        {
            get
            {
                return _idUser;
            }

            set
            {
                _idUser = value;
            }
        }

        public long IdRetorno
        {
            get
            {
                return idRetorno;
            }

            set
            {
                idRetorno = value;
            }
        }

        public long IdTecnico
        {
            get
            {
                return _idTecnico;
            }

            set
            {
                _idTecnico = value;
            }
        }

        public long IdUserRecebedor
        {
            get
            {
                return _idUserRecebedor;
            }

            set
            {
                _idUserRecebedor = value;
            }
        }

        public string Modelo
        {
            get
            {
                return _modelo;
            }

            set
            {
                _modelo = value;
            }
        }

        public string Serie
        {
            get
            {
                return _serie;
            }

            set
            {
                _serie = value;
            }
        }

        public string Nf
        {
            get
            {
                return _nf;
            }

            set
            {
                _nf = value;
            }
        }

        public string Loja
        {
            get
            {
                return _loja;
            }

            set
            {
                _loja = value;
            }
        }

        public string Defeito
        {
            get
            {
                return _defeito;
            }

            set
            {
                _defeito = value;
            }
        }

        public string Voltagem
        {
            get
            {
                return _voltagem;
            }

            set
            {
                _voltagem = value;
            }
        }

        public string Avaria
        {
            get
            {
                return _avaria;
            }

            set
            {
                _avaria = value;
            }
        }

        public string Acessorios
        {
            get
            {
                return _acessorios;
            }

            set
            {
                _acessorios = value;
            }
        }

        public bool Garantia
        {
            get
            {
                return _garantia;
            }

            set
            {
                _garantia = value;
            }
        }

        public bool Arranhado
        {
            get
            {
                return _arranhado;
            }

            set
            {
                _arranhado = value;
            }
        }

        public bool Retorno
        {
            get
            {
                return _retorno;
            }

            set
            {
                _retorno = value;
            }
        }

        public float Valor
        {
            get
            {
                return _valor;
            }

            set
            {
                _valor = value;
            }
        }

        public DateTime DataNF
        {
            get
            {
                return _dataNF;
            }

            set
            {
                _dataNF = value;
            }
        }

        public DateTime Abertura
        {
            get
            {
                return _abertura;
            }

            set
            {
                _abertura = value;
            }
        }

        public DateTime Fechamento
        {
            get
            {
                return _fechamento;
            }

            set
            {
                _fechamento = value;
            }
        }

        public DateTime DtInicio
        {
            get
            {
                return _dtInicio;
            }

            set
            {
                _dtInicio = value;
            }
        }

        public DateTime HoraIncio
        {
            get
            {
                return _horaIncio;
            }

            set
            {
                _horaIncio = value;
            }
        }

        public DateTime HoraFim
        {
            get
            {
                return _horaFim;
            }

            set
            {
                _horaFim = value;
            }
        }
    }
}