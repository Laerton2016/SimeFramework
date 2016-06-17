using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Medida:ITrataDados
    {
        private Int32 ID = 0;
        private String medida;
        private Connection conex = new Conexao().getContas();
        private Recordset rsDados = new Recordset();
        private String SQL;

        public Medida()
        {

        }

        public Medida(Int32 ID)
        {
            this.ID = ID;
            coletaDados();
        }

        private void coletaDados()
        {
            if (rsDados.State != 0)
            {
                rsDados.Close();
            }
            SQL = "SELECT Medidas.* FROM Medidas WHERE (((Medidas.cod)=" + ID + "));";

            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (rsDados.EOF || rsDados.BOF)
            {
                throw new ArgumentException("Id de medida inválido.");
            }
            else
            {
                this.medida = Convert.ToString(rsDados.Fields["medida"].Value);
                rsDados.Close();
            }
        }

        public override string ToString()
        {
            return "ID: " + ID + Environment.NewLine +
                   "Medida: " + medida;
        }

        public Boolean salvar() {
            return true;
        }
        public Boolean excluir()
        {
            return true;
        }

        public String getMedida() { return this.medida; }

    }
}