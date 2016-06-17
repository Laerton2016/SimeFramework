using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe encarregada em contriui os tipo de orçamentos
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>26/04/2016</Data>
    /// </summary>
    public class FactoryOrcamento
    {
        /// <summary>
        /// Cria um orçamento do tipo simples 
        /// </summary>
        /// <param name="IdUser">Id do Usuário que abriu o orçamento</param>
        /// <returns>Orçamento aberto</returns>
        public static Orcamento CriaOrcamento(Int64 IdUser) { return new Orcamento(IdUser); }
        /// <summary>
        /// Cria um orçamento do tipo Montagem
        /// </summary>
        /// <param name="IdUser">Id do Usuário que abriu o orçamento</param>
        /// <returns>Orçamento aberto do tipo montagem</returns>
        public static Orcamento_Montagem CriaOrcamentoMontagem(Int64 IdUser) { return new Orcamento_Montagem(IdUser); }
    }
}