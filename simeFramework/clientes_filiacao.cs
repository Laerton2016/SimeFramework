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
    
    public partial class clientes_filiacao
    {
        public int Cod { get; set; }
        public Nullable<int> Cod_cliente { get; set; }
        public string Pai { get; set; }
        public string mae { get; set; }
        public string Endereço { get; set; }
        public Nullable<double> Limite { get; set; }
        public string classifica { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
    
        public virtual clientes cliente { get; set; }
    }
}
