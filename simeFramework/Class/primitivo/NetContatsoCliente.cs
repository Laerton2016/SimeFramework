using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simeFramework.Class.primitivo
{
    public class NetContatosCliente:IEquatable<NetContatosCliente>
    {
        private Int32 _IDCliente = 0, _IDContato = 0;
        private string _contato = "", _tipo = "";

        public NetContatosCliente(int _IDCliente, int _IDContato, string _contato, string _tipo)
        {
            this._IDCliente = _IDCliente;
            this._IDContato = _IDContato;
            this._contato = _contato;
            this._tipo = _tipo;
        }

        public int IDCliente
        {
            get
            {
                return _IDCliente;
            }

            set
            {
                _IDCliente = value;
            }
        }

        public int IDContato
        {
            get
            {
                return _IDContato;
            }

            set
            {
                _IDContato = value;
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

        public bool Equals(NetContatosCliente outro)
        {
            if (outro == null) return false;
            return this.IDContato == outro.IDContato;
        }
    }
}
