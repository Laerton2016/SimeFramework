using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace simeFramework.Class.primitivo
{
    /// <summary>
    /// Classe cuida dos dados de um contato de um fornecedor
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>05/06/2016</Data>
    /// </summary>
    public class NetContatosFornecedor: IEquatable<NetContatosFornecedor>
    {
        private Int32 _cod, _cod_fornecedor;
        private string _tipo, _contato, _dado;

        public NetContatosFornecedor()
        {
            _cod = 0;
            _cod_fornecedor = 0;
            _tipo = "";
            _contato = "";
            _dado = "";

        }
        public int Cod
        {
            get
            {
                return _cod;
            }

            set
            {
                _cod = value;
            }
        }

        public int Cod_fornecedor
        {
            get
            {
                return _cod_fornecedor;
            }

            set
            {
                _cod_fornecedor = value;
            }
        }

        public string Contato
        {
            get
            {
                return _contato;
            }

            set
            {
                _contato = value;
            }
        }

        public string Dado
        {
            get
            {
                return _dado;
            }

            set
            {
                _dado = value;
            }
        }

        public string Tipo
        {
            get
            {
                return _tipo;
            }

            set
            {
                _tipo = value;
            }
        }
        
        public bool Equals(NetContatosFornecedor outro)
        {
            if (outro == null) { return false; }
            return outro.Cod == Cod;
        }
    }
}