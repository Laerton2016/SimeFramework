using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;

namespace SIME
{
    class tipoEstoque
    {
        private Conexao conex;
        private Recordset RsDados;
        private String SQL;
        private List<String[]> listaEstoque = new List<string[]>();

        public tipoEstoque() {
            SQL = "SELECT Tipo_estoque.cod, Tipo_estoque.estoque FROM Tipo_estoque ORDER BY Tipo_estoque.estoque";
            MontaLista();
        }

        private void MontaLista()
        {
            RsDados = new Recordset();
            RsDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            listaEstoque.Clear();
                        

            while (!(RsDados.EOF || RsDados.BOF))
            {
                listaEstoque.Add(new String[] { Convert.ToString(RsDados.Fields["Cod"].Value), Convert.ToString(RsDados.Fields["Estoque"].Value) });
                RsDados.MoveNext();
            }
            RsDados.Close();
        }

        internal List<string[]> getListaEstoque()
        {
            return listaEstoque;
        }
        /// <summary>
        /// Método retorna o tipo de estoque em String baseado no ID recebido como parametro.
        /// </summary>
        /// <param name="ID">ID do tipo de estoque - Tipo int32</param>
        /// <returns>String contendo o tipo de estoque</returns>
        public String getTipo(Int32 ID)
        {
            String tipo = "";
            foreach (String[] item in listaEstoque)
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
