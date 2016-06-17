using SIME.Class.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.Orcamento
{
    public class DAOOrcamento : IDAO<Orcamento>
    {
        public Orcamento Buscar(long id)
        {
            throw new NotImplementedException();
        }

        public void Excluir(Orcamento t)
        {
            throw new NotImplementedException();
        }

        public Orcamento Salvar(Orcamento t)
        {
            throw new NotImplementedException();
        }

        internal List<Orcamento> BuscarOrcamentos(long Id_cliente)
        {
            throw new NotImplementedException();
        }

        internal List<Orcamento> BuscarOrcamentos(bool p)
        {
            throw new NotImplementedException();
        }

        internal List<Orcamento> BuscarOrcamentos(bool v, long idUser)
        {
            throw new NotImplementedException();
        }
    }
}
