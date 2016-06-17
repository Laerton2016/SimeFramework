using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class
{
    public enum classificaConta
    {
        //DESPESAS = "d", MERCADORIA = "m", TELEFONE = "t", IMPOSTOS = "i", FRETE = "f", SALARIOS = "s", CONTADOR = "c", BANCO = "b", OUTRAS = "o"
    }
    public enum empresas
    {
        SIL = 1, LAERTON = 2, TVSOM = 3
    }


    public class ContaApagar : ITrataDados
    {
        string origem, informacao, complemento, classificacao;
        Int32 cod, empresa;
        Boolean pago, alterado = true;
        DateTime vencimento, dataNota;
        Double valor;

        /// <summary>
        /// Contrutor geral para uma conta nova onde os dados ainda não foram gravados no banco de dados 
        /// </summary>
        public ContaApagar()
        {
            origem = "";
            informacao = "";
            complemento = "";
            classificacao = "d";
            cod = 0;
            empresa = 1;
            pago = false;
            valor = 0;
            
        }
        public ContaApagar(Int32 cod, Int32 empresa, String origem, String informacao, String complemento, String classificacao,
            Boolean pago, DateTime vencimento, DateTime dataNota, Double valor)
        {
            this.cod = cod;
            this.empresa = empresa;
            this.origem = origem;
            this.informacao = informacao;
            this.complemento = complemento;
            this.classificacao = classificacao;
            this.pago = pago;
            this.vencimento = vencimento;
            this.dataNota = dataNota;
            this.valor = valor;
            alterado = false;
        }

        public string Classificacao
        {
            get { return classificacao; }
            set
            {
                classificacao = value;
                alterado = true;
            }
        }

        public string Complemento
        {
            get { return complemento; }
            set
            {
                complemento = value;
                alterado = true;
            }
        }

        public string Informacao
        {
            get { return informacao; }
            set
            {
                informacao = value;
                alterado = true;
            }
        }

        public string Origem
        {
            get { return origem; }
            set
            {
                origem = value;
                alterado = true;
            }
        }
        public Int32 Empresa
        {
            get { return empresa; }
            set
            {
                empresa = value;
                alterado = true;
            }
        }
        public Boolean Pago
        {
            get { return pago; }
            set
            {
                pago = value;
                alterado = true;
            }
        }

        public DateTime DataNota
        {
            get { return dataNota; }
            set
            {
                if (dataNota <= DateTime.Now)
                {
                    dataNota = value;
                    alterado = true;
                }
                else {
                    throw new Exception("Data não pode ser superior a data atual."); 
                }
            }
        }

        public DateTime Vencimento
        {
            get { return vencimento; }
            set
            {
                //O venciemnto não pode ser anterior a 2 anos do atual
                if ((Convert.ToInt32(DateTime.Now.Year.ToString()) - Convert.ToInt32(vencimento.Year.ToString())) <= 2)
                {
                    vencimento = value;
                    alterado = true;
                }
                else {
                    throw new Exception("Vencimento não pode ser inferior a 1 ano.");
                }
            }
        }


        public Double Valor
        {
            get { return valor; }
            set
            {
                if (valor >= 0)
                {
                    valor = value;
                    alterado = true;
                }
                else {
                    throw new Exception("Não são permitidos valores menores ou igual a zero.");
                }
            }
        }




        public bool salvar()
        {
            String SQL;
            if (alterado) {
                ADODB.Recordset RSDados = new ADODB.Recordset();
                RSDados.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                RSDados.LockType = ADODB.LockTypeEnum.adLockOptimistic;
                RSDados.CursorType = ADODB.CursorTypeEnum.adOpenKeyset;
                if (cod != 0)
                {
                    SQL = "SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.Código)=" + cod + "));";
                    
                }
                else {
                    SQL = "SELECT Duplicatas.* FROM Duplicatas;";
                }

                RSDados.Open(SQL, new Conexao().getContas());
                if (cod == 0) {
                    RSDados.AddNew();
                }

                RSDados.Fields["data"].Value = Vencimento.ToShortDateString();
                RSDados.Fields["Origem"].Value = Origem;
                RSDados.Fields["informação"].Value = Informacao;
                RSDados.Fields["complemento"].Value = Complemento;
                RSDados.Fields["Valor"].Value = Valor;
                RSDados.Fields["Pago"].Value = Pago;
                RSDados.Fields["Classificação"].Value = Classificacao;
                RSDados.Fields["DataNota"].Value = DataNota.ToShortDateString();
                RSDados.Fields["Empresa"].Value = Empresa;
                RSDados.Update();
                if (cod == 0) {
                    cod = Convert.ToInt32(RSDados.Fields["cod"].Value);
                }
                RSDados.Close();
                return true ;
            }

                return false ;
            
        }

        public bool excluir()
        {
            if (cod != 0) {
                String SQL = "DELETE Duplicatas.Código FROM Duplicatas WHERE (((Duplicatas.Código)= " + cod + "));";
                ADODB.Recordset RSDados = new ADODB.Recordset();
                RSDados.CursorLocation = ADODB.CursorLocationEnum.adUseClient;
                RSDados.LockType = ADODB.LockTypeEnum.adLockOptimistic;
                RSDados.CursorType = ADODB.CursorTypeEnum.adOpenKeyset;
                RSDados.Open(SQL,new Conexao().getContas());
                return true;
            }
            return false;
        }

        public String[] toArray() { 
            String[] retorno = {"Vencimento:" + vencimento.ToShortDateString() , "Origem: " + origem, "Informação: " + informacao , "Complemento: "+ complemento, "Data NF: "+ dataNota.ToShortDateString(), "Valor: R$ "+ valor };
            return retorno;
        }
    }
}