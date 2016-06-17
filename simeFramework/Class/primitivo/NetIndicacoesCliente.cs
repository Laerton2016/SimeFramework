using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SIME.Class.primitivo
{
    public class NetIndicacoesCliente:IEquatable<NetIndicacoesCliente>
    {
        private Int32 _IDCliente , _IDIndica ;
        private string Tipo , contato , dado ;

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

        public int IDIndica
        {
            get
            {
                return _IDIndica;
            }

            set
            {
                _IDIndica = value;
            }
        }

        public string Tipo1
        {
            get
            {
                return Tipo;
            }

            set
            {
                Tipo = value;
            }
        }

        public string Contato
        {
            get
            {
                return contato;
            }

            set
            {
                contato = value;
            }
        }

        public string Dado
        {
            get
            {
                return dado;
            }

            set
            {
                dado = value;
            }
        }

        public NetIndicacoesCliente(int _IDCliente, int _IDIndica, string tipo, string contato, string dado)
        {
            this.IDCliente = _IDCliente;
            this.IDIndica = _IDIndica;
            Tipo1 = tipo;
            this.Contato = contato;
            this.Dado = dado;
        }

        public bool Equals(NetIndicacoesCliente other)
        {
            if (other == null) return false;
            return other.IDIndica == this.IDIndica;
            throw new NotImplementedException();
        }
    }
}