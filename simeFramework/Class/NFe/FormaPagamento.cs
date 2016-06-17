using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using System.Windows.Forms;

namespace WindowsFormsApplication2
{
    class FormaPagamento
    {
        private List<String> dados;
        private List<List<String>> parcelas;
        public FormaPagamento(List<String> dados) {
            this.dados = dados;
            parcelas = new List<List<string>>();
            analizaDados(this.dados);

        }

        private void analizaDados(List<String> dados) {
            String nDup="", dVenc="", vDup="";
            int cont = 0;
            if (dados.Count >0 ) { 
            String informe = dados[0].ToString().Split(new Char[] { ':' })[0];
            if (!informe.Equals("nDup")) {
                this.dados.Clear();//Os dados que veio para ca não trata-se de duplicata devemos disconsiderar os dados
                return;
            }

            //Laço que vc separa os objetos
            for (int i = 0; i < dados.Count; i++)
            {
                informe = dados[i].ToString().Split(new Char[] { ':' })[0]; // Verifica qual o campo

                if (informe.Equals("nDup"))
                {
                    nDup = dados[i].ToString().Split(new Char[] { ':' })[2];
                }

                if (informe.Equals("vDup"))
                {
                    vDup = dados[i].ToString().Split(new Char[] { ':' })[2];
                }

                if (informe.Equals("dVenc"))
                {
                    dVenc = dados[i].ToString().Split(new Char[] { ':' })[2];
                }

                if (cont == 2)
                {
                    List<String> dadosParcelas = new List<string>();
                    dadosParcelas.Add(nDup);
                    dadosParcelas.Add(dVenc);
                    dadosParcelas.Add(vDup);
                    parcelas.Add(dadosParcelas);
                    cont = 0;
                }
                else
                {
                    cont++;
                }
            }
            }


        }

        public Int16 getNumeroParcelas() {
            return Convert.ToInt16( parcelas.Count);
        }

        public String getNDuplicata(int numeroParcela)  { 
            if (numeroParcela > getNumeroParcelas()-1) {
                return null;            
            }
            return parcelas[numeroParcela][0];
        }

        public String getDtVencimento(int numeroParcela) {
            if (numeroParcela > getNumeroParcelas() - 1)
            {
                return null;
            }
            DateTime dt = Convert.ToDateTime(parcelas[numeroParcela][1]);
            return dt.Day.ToString() + "/" + dt.Month.ToString() + "/" + dt.Year.ToString();
        }

        public Double  getValorParcela(int numeroParcela) {
            if (numeroParcela > getNumeroParcelas() - 1)
            {
                return 0;
            }
            return Convert.ToDouble(parcelas[numeroParcela][2].Replace('.', ','));
        }
        public override String ToString() {
            String retorno = "NUMERO DE PARCELAS: " + getNumeroParcelas ();
            for (int i = 0; i < getNumeroParcelas(); i++) {
                retorno += "\nPARCELA: " + getNDuplicata(i) +
                           "\nVENCIMENTO: " + getDtVencimento(i) +
                           "\nVALOR: " + getValorParcela(i);
            }
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
