using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class
{
    class InformacaoCredito
    {
        private Int32 ID;
        private String informacao;
        public InformacaoCredito(Int32 ID, String informacao) { this.ID = ID; this.informacao = informacao; }
        public override String ToString()
        {
             return ID + "," + informacao; 
        }
        
        public String[] ToArray() { return new String[] { Convert.ToString(ID), informacao }; }
    }
}