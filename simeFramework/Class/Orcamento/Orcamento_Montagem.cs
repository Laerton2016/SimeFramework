using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Orçamento que de uma montagem
    /// <autor>Laerton Marques de Figueiredo</autor>
    /// <Data>26/04/2016</Data>
    /// </summary>
    
    public class Orcamento_Montagem : Orcamento
    {
        private Int64  _id_os, _id_tecnico;
        private String  _lacre;
        public Orcamento_Montagem(long id_user) : base(id_user)
        {
            
            Id_os = 0;
            Id_tecnico = 0;
            Lacre = "0";
        }

        public long Id_os
        {
            get
            {
                return _id_os;
            }

            set
            {
                _id_os = value;
            }
        }

        public long Id_tecnico
        {
            get
            {
                return _id_tecnico;
            }

            set
            {
                _id_tecnico = value;
            }
        }

        public string Lacre
        {
            get
            {
                return _lacre;
            }

            set
            {
                _lacre = value;
            }
        }

        
        
    }
}