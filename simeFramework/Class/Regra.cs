using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Regra : ITrataDados
    {
        Connection conex = new Conexao().getContas();
        Recordset RsDados = new Recordset();
        String SQL;
        private Int32 ID = 0;
        private String regra;
        private Double ICMSD = 0;
        private Double percentualICMS = 0;
        private Double federal = 0;
        private Double PercentualFederal = 0;
        private Double ICMSD_fora = 0;
        private Double percentualICMS_fora = 0;
        private Double TxFixa = 0;
        private String informativo;
        private String CSOSN;
        private String ST;
        private String CST;
        private String IAT;
        private String IPPT;
        


        public Regra()
        {

        }

        public Regra(Int32 ID)
        {
            SQL = "SELECT Dados_impostos.* FROM Dados_impostos WHERE (((Dados_impostos.cod)=" + ID + "));";
            coletaDados();
        }

        private void coletaDados()
        {
            if (RsDados.State != 0)
            {
                RsDados.Close();
            }
            RsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (RsDados.EOF || RsDados.BOF)
            {
                throw new ArgumentException("Regra repassada não é válida.");
            }

            this.ID = Convert.ToInt32(RsDados.Fields["cod"].Value);
            this.regra = Convert.ToString(RsDados.Fields["Regra"].Value);
            this.ICMSD = Convert.ToDouble(RsDados.Fields["ICMSD"].Value);
            this.percentualICMS = Convert.ToDouble(RsDados.Fields["PercentualICMS"].Value);
            this.federal = Convert.ToDouble(RsDados.Fields["Federal"].Value);
            this.PercentualFederal = Convert.ToDouble(RsDados.Fields["PercentualFederal"].Value);
            this.ICMSD_fora = Convert.ToDouble(RsDados.Fields["ICMSD_fora"].Value);
            this.percentualICMS_fora = Convert.ToDouble(RsDados.Fields["PercentualICMS_fora"].Value);
            this.informativo = Convert.ToString(RsDados.Fields["Texto"].Value);
            this.TxFixa = (RsDados.Fields["TxFixas"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToDouble(RsDados.Fields["TxFixas"].Value.ToString());
            this.CSOSN = (RsDados.Fields["csosn"].Value.Equals(DBNull.Value)) ? "102" : RsDados.Fields["csosn"].Value.ToString();
            this.ST = (RsDados.Fields["ST"].Value.Equals(DBNull.Value)) ? "" : RsDados.Fields["ST"].Value.ToString();
            this.CST = (RsDados.Fields["CST"].Value.Equals(DBNull.Value)) ? "0" : RsDados.Fields["CST"].Value.ToString();
            this.IAT = (RsDados.Fields["IAT"].Value.Equals(DBNull.Value)) ? "T" : RsDados.Fields["IAT"].Value.ToString();
            this.IPPT = (RsDados.Fields["IPPT"].Value.Equals(DBNull.Value)) ? "T" : RsDados.Fields["IPPT"].Value.ToString();

        }

        public Int32 getID() { return this.ID; }
        public Double getTaxaDespesasFixas() { return this.TxFixa; }
        public String getRegra() { return this.regra; }
        public Double getICMSD() { return this.ICMSD; }
        public Double getpercentualICMS() { return this.percentualICMS; }
        public Double getFederal() { return this.federal; }
        public Double getPercentualFederal() { return this.PercentualFederal; }
        public Double getICMSD_fora() { return this.ICMSD_fora; }
        public Double getPercentualICMS_fora() { return this.percentualICMS_fora; }
        public String getInformativo() { return this.informativo; }
        public String getST() { return this.ST; }
        public String getCST() { return this.CST; }
        public String getIAT() { return this.IAT; }
        public String getIPPT() { return this.IPPT; }
        public String getCSOSN() { return this.CSOSN.Replace(" ", ""); }


        public void setST(String ST) 
        {
            if (ST.Length > 3) { throw new ArgumentException("ST Não pode conter mais que 3 caracteres."); }
            for (int i = 0; i < ST.Length; i++)
            {
                if (!(ST[i].Equals("F")))
                {
                    if (!(ST[i].Equals("I")))
                    {
                        if (!(ST[i].Equals("N")))
                        {
                            throw new ArgumentException("ST só pode ser composto pelas lestras I, N ou F");
                        }
                    }
                }

                
            }
            this.ST = ST;
        }

        public void setCST(Int32 CST)
        {
            if (CST< 0 ) { throw new ArgumentException ("CST não pode conter valores negativos.");}

            this.CST = CST.ToString();
        }

        public void setIAT(String IAT)
        {
            if (IAT.Length > 1) { throw new ArgumentException("IAT não pode conter mais que 1 caracter."); }
            this.IAT = IAT;
        }

        public void setIPPT(String IPPT)
        {
            if (IPPT.Length > 1) { throw new ArgumentException("IPPT não pode conter mais que 1 caracter."); }
            this.IPPT = IPPT;
        }

        public void setTaxaDespesasFixas(double TxFixa)
        {
            if (TxFixa < 0) {
                throw new ArgumentException("Não é permitivo valor negativo para taxa de despesas fixas.");
            }
            this.TxFixa = TxFixa;
        }

        public void setRegra(String Regra)
        {

            if (regra.Equals(""))
            {
                throw new ArgumentException("O campo regra não pode campo em branco.");
            }
            else if (regra == null)
            {
                throw new ArgumentNullException("O campo regra não pode conter dados nulo.");
            }
            else
            {
                this.regra = Regra;
            }


        }
        public void setICMSD(Double ICMSD) { this.ICMSD = ICMSD; }
        public void setpercentualICMS(Double percentualICMS) { this.percentualICMS = percentualICMS; }
        public void setFederal(Double federal) { this.federal = federal; }
        public void setICMSD_Fora(Double ICMSD_fora) { this.ICMSD_fora = ICMSD_fora; }
        public void setPercentualICMS_fora(Double percentualICMS_fora) { this.percentualICMS_fora = percentualICMS_fora; }
        public void setInformativo(String informativo) { this.informativo = informativo; }

        public override string ToString()
        {
            return "ID: " + ID + Environment.NewLine +
                "Regra: " + regra + Environment.NewLine +
                "ICMSD: " + ICMSD + Environment.NewLine +
                "Percentual ICMS: " + percentualICMS + Environment.NewLine +
                "Federal: " + federal + Environment.NewLine +
                "ICMSD Fora: " + ICMSD_fora + Environment.NewLine +
                "Percentual ICMSD Fora: " + percentualICMS_fora + Environment.NewLine +
                "Informativo: " + informativo;
        }

        public Boolean salvar()
        {
            return true;
        }

        public Boolean excluir()
        {
            return true;
        }

        

    }
}