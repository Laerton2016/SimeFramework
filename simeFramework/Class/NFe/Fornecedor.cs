using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;
using SIME;

namespace WindowsFormsApplication2
{
    class FornecedorNFe
    {
        private List<String> dados;
        public FornecedorNFe(List<String> dados) {
            this.dados = dados;
        }

        public String getFantasia() { return retornoDado("xFant"); }
        public String getNome() {
            return retornoDado("xNome");
        }

        public String getCNPJ() {
            String cnpj  = retornoDado("CNPJ");
            String maskCNPJ = "";
            for (int i = 0; i < cnpj.Length; i++)
            {
                //02.648.013/0001-05
                if (i == 2 || i == 5)
                {
                    maskCNPJ = maskCNPJ + "." + cnpj[i];
                }
                else if (i == 8)
                {
                    maskCNPJ = maskCNPJ + "/" + cnpj[i];
                }
                else if (i == 12)
                {
                    maskCNPJ = maskCNPJ + "-" + cnpj[i];
                }
                else
                {
                    maskCNPJ = maskCNPJ + cnpj[i];
                }

            }

            return maskCNPJ;
        }
        
        public String getEnd() {
            return retornoDado("xLgr") + " " + retornoDado("nro");
        }

        public String getBairro() {
            return retornoDado("xBairro");
        }

        public String getCidade() {
            return retornoDado("xMun");
        }

        public String getUF() {
            return retornoDado("UF");
        }

        public String getCEP() {
            return retornoDado("CEP");
        }

        public String getIE() {
            return retornoDado("IE");
        }

        public String getComplemento() {
            return retornoDado("xCpl");
        }

        public override string ToString(){
            String retorno = "";
                retorno = "FORNECEDOR: " + getNome() +
            "\nCNPJ: " + getCNPJ() +
            "\nIE: "+ getIE() +
            "\nENDEREÇO: " + getEnd()  + 
            "\nCOMPLEMENTO: " + getComplemento() +
            "\nCEP: " + getCEP() +
            "\nCIDADE: " + getCidade() +
            " UF: " + getUF();
            return retorno;
        }

        private String retornoDado(String chave) {
            String retorno = "";
            String campo = "";
            Boolean para = false ;
            int cont = 0;
            while (!para && cont != (dados.Count -1) ) {
                campo = dados[cont].ToString().Split(new Char[] { ':' })[0];
                if (campo.Equals(chave)) {
                    retorno = dados[cont].ToString().Split(new Char[] { ':' })[2];
                    para = true;
                }
                cont++;
            }
            
            return retorno;
        }

        public SIME.Class.Fornecedor getFornecedor()
        {
            Int32 id = buscaID();
            SIME.Class.Fornecedor retorno;
            if (id != 0)
            {
                retorno = new SIME.Class.Fornecedor(id);
            }
            else 
            {
                retorno = new SIME.Class.Fornecedor();
                retorno.setBairro (getBairro());
                retorno.setCEP (getCEP());
                retorno.setCidade(getCidade());
                retorno.setCNPJ(getCNPJ());
                retorno.setEndereco(getEnd());
                retorno.setIE(getIE());
                retorno.setNome(getFantasia());
                retorno.setRazao(getNome());
                retorno.setUF(getUF());
                
            }

            return retorno;
        }

        private Int32 buscaID() 
        {
            Int32 id = 0;

            Recordset RSDados = new Recordset();
            String SQL = "SELECT Fornecedores.* FROM Fornecedores WHERE (((Fornecedores.CNPJ)='" + this.getCNPJ() + "'));"; ;
            Connection conex = new Conexao().getContas();

            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(RSDados.EOF || RSDados.BOF)) { id = Convert.ToInt32(RSDados.Fields["cod"].Value.ToString()); }
           
            return id;
        }
        
    }
}