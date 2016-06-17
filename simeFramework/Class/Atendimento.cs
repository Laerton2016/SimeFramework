using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Atendimento
    {
        private Conexao conex;
        private Recordset RsDados;
        private String SQL;
        private List<String[]> listaAtendimento;

        public Atendimento()
        {
            SQL = "SELECT Tipo_Atendimento.* FROM Tipo_Atendimento ORDER BY Tipo_Atendimento.Atendimento;";
            listaAtendimento = new List<string[]>();
            montaLista();
        }

        private void montaLista()
        {
            RsDados = new Recordset();
            RsDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            listaAtendimento.Clear();
            //Atendimento por padrão deve ser Balcão
            while (!(RsDados.EOF || RsDados.BOF))
            {
                listaAtendimento.Add(new String[] { Convert.ToString(RsDados.Fields["cod"].Value), Convert.ToString(RsDados.Fields["Atendimento"].Value), Convert.ToString(RsDados.Fields["ncobrar"].Value) });
                RsDados.MoveNext();
            }
            RsDados.Close();

        }

        public List<String[]> getListaAtendimento()
        {
            return listaAtendimento;
        }

        public bool servicoCobrar(string cod)
        {
            foreach (String[] item in listaAtendimento)
            {
                if (item[0].Equals(cod))
                {
                    return Convert.ToBoolean(item[2]);
                }

            }
            return false;
            {

            }
        }
    }
}