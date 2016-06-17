using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class
{
    public class Empresa: ITrataDados
    {
        public Empresa() { }
        public Empresa(Int32 Id) { }


        private void coletaDados() { }
        public Boolean salvar() { return true; }
        public Boolean excluir() { return false; }
        public override string ToString()
        {
            return base.ToString();
        }
    }
}