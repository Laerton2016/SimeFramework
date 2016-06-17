using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace WindowsFormsApplication2
{
    class Nota
    {
        private List<String> dados;
        public Nota(List<String> dados) {
            this.dados = dados;
        }

        public String getNumeroNF() {
            return retornoDado("nNF");
        }

        public String getNatureza() {
            return retornoDado("natOp");
        }

        public String getData() {
            DateTime dt = Convert.ToDateTime (retornoDado("dEmi"));
            return dt.Day.ToString() + "/" +  dt.Month.ToString() + "/" + dt.Year.ToString(); 
        }

        public override String ToString() {
            String retorno = "";
            retorno = "NÚMERO DA NF: " + getNumeroNF() +
                "\nNATUREZA DA OPERAÇÃO: " + getNatureza() +
                "\nDATA: " + getData();
            return retorno;
        }

        private String retornoDado(String chave)
        {
            String retorno = "";
            String campo = "";
            Boolean para = false;
            int cont = 0;
            while (!para && cont != (dados.Count - 1))
            {
                campo = dados[cont].ToString().Split(new Char[] { ':' })[0];
                if (campo.Equals(chave))
                {
                    retorno = dados[cont].ToString().Split(new Char[] { ':' })[2];
                    para = true;
                }
                cont++;
            }

            return retorno;
        }
    }
}
