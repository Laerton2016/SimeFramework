using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.primitivo
{
    public class NetVenda
    {
        private DateTime _date;
        private Int64 _id_cliente, _id, _id_operador, _id_caixa;
        private float _especie, _cheque, _vale, _cartao;

        public DateTime Date
        {
            get
            {
                return _date;
            }

            set
            {
                _date = value;
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

        public long Id_operador
        {
            get
            {
                return _id_operador;
            }

            set
            {
                _id_operador = value;
            }
        }

        public long Id_caixa
        {
            get
            {
                return _id_caixa;
            }

            set
            {
                _id_caixa = value;
            }
        }

        public float Especie
        {
            get
            {
                return _especie;
            }

            set
            {
                _especie = value;
            }
        }

        public float Cheque
        {
            get
            {
                return _cheque;
            }

            set
            {
                _cheque = value;
            }
        }

        public float Vale
        {
            get
            {
                return _vale;
            }

            set
            {
                _vale = value;
            }
        }

        public float Cartao
        {
            get
            {
                return _cartao;
            }

            set
            {
                _cartao = value;
            }
        }

        public NetVenda()
        {
            Date = DateTime.Now;
            Id = 0;
            Id_caixa = 0;
            Id_cliente = 0;
            Id_operador = 0;
            Especie = 0;
            Cheque = 0;
            Vale = 0;
            Cartao = 0;
        }
        
    }
}