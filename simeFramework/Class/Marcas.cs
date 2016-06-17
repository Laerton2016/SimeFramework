using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Marcas
    {
        private Conexao conex;
        private Recordset RsDados;
        private String SQL;
        private List<String[]> listaMarcas = new List<string[]>();

        public Marcas() {
            SQL = "SELECT Tipo_marca.cod, Tipo_marca.Marca FROM Tipo_marca ORDER BY Tipo_marca.Marca";
            MontaLista();
        }

        private void MontaLista()
        {
            RsDados = new Recordset();
            RsDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            listaMarcas.Clear();
            
            //Informação para o campo sem preenchiemnto
            listaMarcas.Add(new String[] { "0", "NÃO SELECIONADO" });

            while (!(RsDados.EOF || RsDados.BOF))
            {
                listaMarcas.Add(new String[] { Convert.ToString(RsDados.Fields["Cod"].Value), Convert.ToString(RsDados.Fields["Marca"].Value) });
                RsDados.MoveNext();
            }
            RsDados.Close();
        }

        public List<String[]> getListaTiposMarcas()
        {
            return listaMarcas;
        }

        /// <summary>
        /// Método retorna a marca em String baseado no ID recebido como parametro.
        /// </summary>
        /// <param name="ID">ID Marca - Tipo int32</param>
        /// <returns>String contendo a marca</returns>
        public String getTipo(Int32 ID)
        {
            String tipo = "";
            foreach (String[] item in listaMarcas)
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