using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Seguradora
    {
        private Conexao conex;
        private Recordset RsDados;
        private String SQL;
        private List<String[]> listaSeguradora;
        public Seguradora() {
            SQL = "SELECT Asseguradora.ID, Asseguradora.Asseguradora FROM Asseguradora ORDER BY Asseguradora.Asseguradora;";
            listaSeguradora = new List<string[]>();
            montaLista();
        }

        private void montaLista() {
            RsDados = new Recordset();
            RsDados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            listaSeguradora.Clear();
            //CASO DE SEM SEGURO
            listaSeguradora.Add(new String[] { "0", "SEM SEGURO"});
            while (!(RsDados.EOF || RsDados.BOF))
            {
                listaSeguradora.Add(new String[] { Convert.ToString(RsDados.Fields["ID"].Value), Convert.ToString(RsDados.Fields["Asseguradora"].Value) });
                RsDados.MoveNext();
            }
            RsDados.Close();
        }
        public List<String[]> getListaSeguradoras() {
            return listaSeguradora;
        }

        /// <summary>
        /// Método retorna a seguradora em String baseado no ID recebido como parametro.
        /// </summary>
        /// <param name="ID">ID da seguradora - Tipo int32</param>
        /// <returns>String contendo a seguradora</returns>
        public String getTipo(Int32 ID)
        {
            String tipo = "";
            foreach (String[] item in listaSeguradora)
            {
                if (item[0].Equals(Convert.ToString(ID)))
                {
                    tipo = item[1];
                    break;
                }
            }
            return tipo;
        }
    }
}