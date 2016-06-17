using SIME.Class.Orcamento;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using simeFramework.Class.DAO;

namespace SIME.Class.DAO
{
    /// <summary>
    /// Fábrica de DAOs
    /// <autor>Laerton Marques de Figueiredo</autor>
    /// <Data>27/04/2016</Data>
    /// </summary>
    public class FactoryDAO
    {
        /// <summary>
        /// Cria um DAO de Orçamento
        /// </summary>
        /// <returns>DAOOrçamento</returns>
        public static DAOOrcamento CriaDAOOrcamento() { return new DAOOrcamento(); }
        /// <summary>
        /// Cria um DAO de Itens de Orçamento
        /// </summary>
        /// <returns>DAOItemOrcamento</returns>
        public static DAOItemOrcamento CriaDAOItemOrcamento() { return new DAOItemOrcamento(); }
        /// <summary>
        /// Cria um DAO de Produto
        /// </summary>
        /// <returns>DAOProduto</returns>
        public static DAOProduto CriaDAOProduto() { return new DAOProduto();}
        /// <summary>
        /// Cria um DAO de Cliente
        /// </summary>
        /// <returns>DAOCliente</returns>
        public static DAOCliente CriaDAOCliente() { return new DAOCliente(); }
        /// <summary>
        /// Cria um DAO para Série de Maquinas para montagem
        /// </summary>
        /// <returns>DAOSerieMaquina</returns>
        public static DAOSerieMaquina CriaDaoSerieMaquina() { return new DAOSerieMaquina(); }
        /// <summary>
        /// Cria um DAO para OS 
        /// </summary>
        /// <returns>DAOOS</returns>
        public static DAOOS CriaDaoOS(){return new DAOOS();}
        /// <summary>
        /// Cria um DAO para persise os contatos de um forncedor
        /// </summary>
        /// <returns></returns>
        public static DAOContatoFornecedor CriaDAOContatoForncedor() { return new DAOContatoFornecedor(); }
    }
}
