using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME
{
    public class TiposAparelhos
    {
        private Conexao conex;
        private Recordset RsDados;
        private String SQL;
        private List<String[]> listaTipos = new List<string[]>();
        public TiposAparelhos() {
            SQL = "SELECT Tipo_aparelho.Cod, Tipo_aparelho.Aparelho FROM Tipo_aparelho ORDER BY Tipo_aparelho.Aparelho;";
            montaLista();
        }

        private void montaLista()
        {
            RsDados = new Recordset();
            RsDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            listaTipos.Clear();
            //Para o caso não seleionado
            listaTipos.Add(new String[] { "0", "NÃO SELECIONADO" });
            while (!(RsDados.EOF || RsDados.BOF))
            {
                listaTipos.Add(new String[] { Convert.ToString(RsDados.Fields["Cod"].Value), Convert.ToString(RsDados.Fields["Aparelho"].Value) });
                RsDados.MoveNext();
            }
            RsDados.Close();
        }

        /// <summary>
        /// Método retorna o tipo em String baseado no ID recebido como parametro.
        /// </summary>
        /// <param name="ID">ID do tipo de aparelho - Tipo int32</param>
        /// <returns>String contendo o tipo de aparelho</returns>
        public String getTipo(Int32 ID) {
            String tipo = "";
            foreach (String[] item in listaTipos)
            {
                if (item[0].Equals(Convert.ToString(ID))) {
                    tipo = item[1];
                    break;
                }
            }
            return tipo;
        }

        public List<String[]> getListaTiposAparelho()
        {
            return listaTipos;
        }
    }
}