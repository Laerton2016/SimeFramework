using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{

    public class ContasAPagar
    {
        Recordset RSDados = new Recordset();
        Connection Conex = new Connection();
        String SQL = "SELECT Duplicatas.* FROM Duplicatas;";
        List<ContaApagar> contas = new List<ContaApagar>();
        private Conexao conexao;

        /// <summary>
        /// Contrutor de uso geral com todos os dados de contas a pagar
        /// </summary>
        public ContasAPagar(Connection conex)
        {
            this.Conex = conex;
            conectar(SQL);
        }
        /// <summary>
        /// Contrutor retorna um objeto do tipo contas a pagar com dados de contas vencidas
        /// </summary>
        /// <param name="vencidos">Boolean contendo se deve ser vencidas ou não, se não ele chama o contrutor normal</param>
        public ContasAPagar(Connection conex, bool vencidos)
        {
            this.Conex = conex;
            if (vencidos)
            {
                conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.Pago)=False) AND ((Duplicatas.data)<#"+ DateTime.Now.ToShortDateString() +"#));");
            }
            else
            {
                conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.Pago)=False) AND ((Duplicatas.data)>=#" + DateTime.Now.ToShortDateString() + "#));");
            }
        }

        /**
        /// <summary>
        /// Contrutor que retorna um objeto do tipo contas a pagar a vencer 
        /// </summary>
        /// <param name="aVencer">Boolean decide se deve ver só os a vencer caso seja false sera contruido o normal</param>
        public ContasAPagar(Connection conex, Boolean aVencer)
        {
            if (aVencer)
            {
                conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.Pago)=False) AND ((Duplicatas.data)>=Now()));");
            }
            else
            {
                conectar(SQL);
            }
        }
         */
        /// <summary>
        /// Contrutor que retorna os contas a pagar de um determinado mês e ano.
        /// </summary>
        /// <param name="mesAno">mes e ano de referencia </param>
        public ContasAPagar(Connection conex, DateTime mesAno)
        {
        }
        /// <summary>
        /// Contrutor que retorna o contas a pagar de um determinado ano 
        /// </summary>
        /// <param name="ano">Tipo DataTime contendo o ano </param>
        public ContasAPagar(Connection conex, DateTime ano, Boolean soMes)
        {
            DateTime primeiroDia;
            DateTime ultimoDia;
            this.Conex = conex;
            if (soMes)
            {

                primeiroDia = new DateTime(ano.Year, ano.Month, 1);
                ultimoDia = new DateTime(ano.Year, ano.Month, DateTime.DaysInMonth(ano.Year, ano.Month));
                conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.data) Between #" + primeiroDia.ToShortDateString() + "# And #" +
                    ultimoDia.ToShortDateString() + "#));");

            }
            else
            {
                primeiroDia = Convert.ToDateTime("01/01/" + ano.Year.ToString());
                ultimoDia = Convert.ToDateTime("31/12/" + ano.Year.ToString());
                conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.data) Between #" + primeiroDia.ToShortDateString() + "# And #" +
                    ultimoDia.ToShortDateString() + "#));");
            }
        }
        /// <summary>
        /// Contrutor que retorna o contas a pagar de um determinado periodo
        /// </summary>
        /// <param name="inicio">Data inicial tipo datetime</param>
        /// <param name="fim">Data final tipo datetime</param>
        public ContasAPagar(Connection conex, DateTime inicio, DateTime fim)
        {
            this.Conex = conex;
            conectar("SELECT Duplicatas.* FROM Duplicatas WHERE (((Duplicatas.data) Between #" + inicio.ToShortDateString() + "# And #" +
               fim.ToShortDateString() + "#));");
        }


        private void conectar(String SQL)
        {
            RSDados.CursorLocation = CursorLocationEnum.adUseClient;
            RSDados.CursorType = CursorTypeEnum.adOpenKeyset;
            RSDados.LockType = LockTypeEnum.adLockOptimistic;
            RSDados.Open(SQL, Conex);
            RSDados.MoveFirst();
            //coletando dados
            while (!(RSDados.EOF || RSDados.BOF))
            {
                contas.Add(new ContaApagar(Convert.ToInt32(RSDados.Fields["código"].Value),
                    Convert.ToInt32(RSDados.Fields["Empresa"].Value), RSDados.Fields["Origem"].Value.ToString(), RSDados.Fields["Informação"].Value.ToString(),
                    RSDados.Fields["Complemento"].Value.ToString(), RSDados.Fields["Classificação"].Value.ToString(), Convert.ToBoolean(RSDados.Fields["pago"].Value), Convert.ToDateTime(
                    RSDados.Fields["data"].Value), Convert.ToDateTime(((RSDados.Fields["DataNota"].Value == null) ? DateTime.Now : RSDados.Fields["DataNota"].Value)), Convert.ToDouble(RSDados.Fields["Valor"].Value)));
                RSDados.MoveNext();
            }
            desconectar();
        }

        private void desconectar()
        {
            if (RSDados.State != 0)
            {
                RSDados.Close();
            }
        }

        public Int32 getQuantidade()
        {
            return contas.Count;
        }

        public string getTabelaDuplicatas() {
            List<String[]> dadoslista = new List<string[]>();
            UteisWeb util= new UteisWeb();
            for (int i = 0 ; i < contas.Count; i++){
                dadoslista.Add (contas[i].toArray());
            }
             
            return util.montaTab(dadoslista, "Títulos", System.Drawing.Color.GreenYellow);
        }

    }

}