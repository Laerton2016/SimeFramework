using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe trata de orçamentos lançados no sistema
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>26/04/2016</Data>
    /// </summary>
    public class Orcamento
    {
        private Int64 _id, _id_user, _id_venda, _id_cliente;
        private Boolean _execultado;
        private DateTime _data;
        private float _total, _merkup;
        

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

        public long Id_user
        {
            get
            {
                return _id_user;
            }

            set
            {
                _id_user = value;
            }
        }

        public long Id_venda
        {
            get
            {
                return _id_venda;
            }

            set
            {
                _id_venda = value;
            }
        }

        public long Id_cliente
        {
            get
            {
                return _id_cliente;
            }

            set
            {
                _id_cliente = value;
            }
        }

        public DateTime Data
        {
            get
            {
                return _data;
            }

            set
            {
                _data = value;
            }
        }

        public float Total
        {
            get
            {
                return _total;
            }

            set
            {
                _total = value;
            }
        }

        public float Merkup
        {
            get
            {
                return _merkup;
            }

            set
            {
                _merkup = value;
            }
        }

        public bool Execultado
        {
            get
            {
                return _execultado;
            }

            set
            {
                _execultado = value;
            }
        }
        /// <summary>
        /// Cria um orçamento
        /// </summary>
        /// <param name="id_user">Id do usuário</param>
        public Orcamento(Int64 id_user)
        {
            Id = 0;
            Id_user = id_user;
            Data = DateTime.Now;
            Id_cliente = 0;
            Total = 0;
            Merkup = 0;
            Id_venda = 0;
        }
        public override string ToString()
        {
            return "Id: " + _id + " - Data: " + _data.ToShortDateString() + " - Total: " + _total + " - Execultado: " + ((_execultado) ? "SIM" : "NÃO");
        }
    }
}