using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe que cuida de itens de um orçamento
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>26/04/2016</Data>
    /// </summary>
    public class Item_orcamento
    {
        private Int64 _id, _id_orcamento, _quantidade, _id_produto;
        private float _unitario, _custo;
        private String _serie;
        private Status _status;

        public Item_orcamento(Int64 id_orcamento)
        {
            Id = 0;
            Id_orcamento = id_orcamento;
            Quantidade = 0;
            _unitario = 0;
            _custo = 0;
            _serie = "";
            _status = Status.DISPONIVEL;
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

        public long Id_orçamento
        {
            get
            {
                return Id_orcamento;
            }

            set
            {
                Id_orcamento = value;
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

        public float Unitario
        {
            get
            {
                return _unitario;
            }

            set
            {
                _unitario = value;
            }
        }

        public float Custo
        {
            get
            {
                return _custo;
            }

            set
            {
                _custo = value;
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

        public Status Status
        {
            get
            {
                return _status;
            }

            set
            {
                _status = value;
            }
        }

        
        public long Id_orcamento
        {
            get
            {
                return _id_orcamento;
            }

            set
            {
                _id_orcamento = value;
            }
        }

        
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
    }

    /// <summary>
    /// Status atual de um produto
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>26/04/2016</Data>
    /// </summary>
    public enum Status
    {
        /// <summary>
        /// Quando um produto está com estoque normal e disponível
        /// </summary>
        DISPONIVEL,
        /// <summary>
        /// Quando um produto está discontinuado
        /// </summary>
        DISCONTINUADO,
        /// <summary>
        /// Quando o estoque está zerado 
        /// </summary>
        FALTA,
        /// <summary>
        /// Quando a quantidade é insuficiente para atender a demanda
        /// </summary>
        INSUFICIENTE
    }
}
