using SIME.Class.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe trata da persistência de dados de uma série de uma maquina
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>07/05/2016</Data>
    /// </summary>
    public class DAOSerieMaquina : IDAO<SerieMaquina>
    {
        public SerieMaquina Buscar(long id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(SerieMaquina t)
        {
            throw new NotImplementedException();
        }

        public SerieMaquina Salvar(SerieMaquina t)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Método deve buscar todos os seriais vinculados ao orçamento em questão
        /// </summary>
        /// <param name="id">ID do orçamento</param>
        /// <returns>Lista de Série de Maquinas</returns>
        public List<SerieMaquina> BuscarLista(long id)
        {
            throw new NotImplementedException();
        }
    }
}
