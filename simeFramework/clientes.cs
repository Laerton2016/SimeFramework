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
    
    public partial class clientes
    {
        public clientes()
        {
            this.clientes_contatos = new HashSet<clientes_contatos>();
            this.clientes_credito = new HashSet<clientes_credito>();
            this.clientes_filiacao = new HashSet<clientes_filiacao>();
            this.clientes_indica = new HashSet<clientes_indica>();
        }
    
        public int Cod_cliente { get; set; }
        public string Nome { get; set; }
        public string End { get; set; }
        public string Tele1 { get; set; }
        public string ddd { get; set; }
        public string Dado1 { get; set; }
        public Nullable<bool> Mala { get; set; }
        public string CNPJ { get; set; }
        public string E_mail { get; set; }
        public string CEP { get; set; }
        public Nullable<bool> FJ { get; set; }
        public string cidade { get; set; }
        public string UF { get; set; }
        public string AGCOB { get; set; }
        public string insc { get; set; }
        public Nullable<bool> Restrito { get; set; }
        public string classificação { get; set; }
        public string SIM { get; set; }
        public string PCMCIA { get; set; }
        public string referencia { get; set; }
        public Nullable<int> dt_cliente { get; set; }
        public string bairro { get; set; }
        public Nullable<System.DateTime> nascimento { get; set; }
        public Nullable<bool> Fidelidade { get; set; }
        public Nullable<System.DateTime> Dt_inicio { get; set; }
        public Nullable<System.DateTime> Data { get; set; }
        public Nullable<int> Id_vendedor { get; set; }
        public Nullable<System.DateTime> Data_adesao { get; set; }
    
        public virtual ICollection<clientes_contatos> clientes_contatos { get; set; }
        public virtual ICollection<clientes_credito> clientes_credito { get; set; }
        public virtual ICollection<clientes_filiacao> clientes_filiacao { get; set; }
        public virtual ICollection<clientes_indica> clientes_indica { get; set; }
    }
}
