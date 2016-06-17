using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.DAO
{
    public interface IDAO<T>
    {
        T Salvar(T t);
        void Excluir(T t);
        T Buscar(Int64 id);


    }
}
