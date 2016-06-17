using System;

namespace SIME.Class.primitivo
{
    public class NetInformacaoCredito:IEquatable<NetInformacaoCredito>
    {
        private Int32 _ID, _idCliente;
        private String _informacao;

        public NetInformacaoCredito(int _ID, string _informacao, Int32 _IDCliente)
        {
            this._ID = _ID;
            this._informacao = _informacao;
            this._idCliente = _IDCliente;
        }

        
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

        public int IdCliente
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

        public string Informacao
        {
            get
            {
                return _informacao;
            }

            set
            {
                _informacao = value;
            }
        }

        public bool Equals(NetInformacaoCredito outro)
        {
            if (outro == null) return false;
            return outro.ID == _ID;      
            
        }
    }
}