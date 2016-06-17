using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.primitivo
{
    /// <summary>
    /// Classe que cuida de um item de uma venda;
    /// </summary>
    public class NetItemVenda
    {
        private Int64 _id_produto, _quantidade, _id, _id_venda, _loja, _id_fornecedor;
        private float _valor;
        private String _nf;

        public long Id_produto
        {
            get
            {
                return _id_produto;
            }

            set
            {
                _id_produto = value;
            }
        }

        public long Quantidade
        {
            get
            {
                return _quantidade;
            }

            set
            {
                _quantidade = value;
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

        public long Loja
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

        public long Id_fornecedor
        {
            get
            {
                return _id_fornecedor;
            }

            set
            {
                _id_fornecedor = value;
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

        public NetItemVenda()
        {
            Id = 0;
            Id_fornecedor = 0;
            Id_produto = 0;
            Id_venda = 0;
            Quantidade = 0;
            Valor = 0;
            Nf = "0";

        }
    }
}
