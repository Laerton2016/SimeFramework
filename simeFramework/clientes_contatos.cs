//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace simeFramework
{
    using System;
    using System.Collections.Generic;
    
    public partial class clientes_contatos
    {
        public Nullable<int> cod_cliente { get; set; }
        public string Tipo { get; set; }
        public string contatos { get; set; }
        public int ID { get; set; }
    
        public virtual clientes cliente { get; set; }
    }
}