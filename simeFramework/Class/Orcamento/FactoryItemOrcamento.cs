using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe cria um item para um orçamento
    /// <autor>Laerton Marques de Figueiredo</autor>
    /// <data>27/07/2016</data>
    /// </summary>
    public class FactoryItemOrcamento
    {
        /// <summary>
        /// Cria um item para um orçamento
        /// </summary>
        /// <param name="id_orcamento">Id do orçamento</param>
        /// <returns>Item novo criado</returns>
        public static Item_orcamento CriaItem(Int64 id_orcamento)
        {
            return new Item_orcamento(id_orcamento);
        }
    }
}