using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using SIME.Class;

namespace SIME
{
    public class Aparelhos
    {
        Recordset RSDados = new Recordset();
        String SQL;
        List<String[]> listaAparelhos = new List<string[]>();
        public Aparelhos()
        {

        }

        private void coletaDados()
        {
            coletaDados(0);
        }

        private void coletaDados(Int32 ID_cliente)
        {
            if (ID_cliente != 0)
            {
                SQL = "SELECT Aparelhos.Cod_cliente, Tipo_aparelho.Aparelho, Tipo_marca.Marca, OS_Entragas.Data, Aparelhos.modelo, Aparelhos.Cod " +
                      "FROM ((Aparelhos INNER JOIN Tipo_aparelho ON Aparelhos.cod_tipo = Tipo_aparelho.Cod) INNER JOIN Tipo_marca ON Aparelhos.cod_marca = Tipo_marca.Cod) INNER JOIN OS_Entragas ON Aparelhos.Cod = OS_Entragas.OS " +
                      "WHERE (((Aparelhos.Cod_cliente)=" + ID_cliente + "))ORDER BY Aparelhos.Cod DESC;";


            }
            else
            {
                SQL = "SELECT Aparelhos.Cod, Tipo_aparelho.Aparelho, Tipo_marca.Marca, Aparelhos.modelo, Aparelhos.Cod_cliente, OS_Entragas.Data " +
                      "FROM (Tipo_marca INNER JOIN (Tipo_aparelho INNER JOIN Aparelhos ON Tipo_aparelho.Cod = Aparelhos.cod_tipo) ON Tipo_marca.Cod = Aparelhos.cod_marca) LEFT JOIN OS_Entragas ON Aparelhos.Cod = OS_Entragas.Cod " +
                      "WHERE (((Aparelhos.Cod_cliente)=%)) ORDER BY Aparelhos.Cod DESC;";
            }
            listaAparelhos.Clear();
            if (RSDados.State != 0)
            {
                RSDados.Close();
            }

            RSDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            
            while (!(RSDados.EOF || RSDados.BOF))
            {
                listaAparelhos.Add(new String[] { Convert.ToString(RSDados.Fields["cod"].Value), Convert.ToString(RSDados.Fields["Aparelho"].Value), 
                    Convert.ToString(RSDados.Fields["Marca"].Value), Convert.ToString( RSDados.Fields["Modelo"].Value ), Convert.ToString(RSDados.Fields["data"].Value) });
                RSDados.MoveNext();
            }

        }

        public Aparelho get_OSRetorno(Int32 ID_OS)
        {
            return new Aparelho(ID_OS);
        }

        public List<String[]> get_listaAparelhosRretorno(Int32 ID_cliente, String aparelho, String marca, String modelo)
        {
            List<String[]> listaRetorno = new List<string[]>();
            coletaDados(ID_cliente);
            for (int i = 0; i < listaAparelhos.Count; i++)
            {
                String[] os = listaAparelhos[i];
                if (os[1].ToUpper().Equals(aparelho.ToUpper()) && os[2].ToUpper().Equals(marca.ToUpper()) && os[3].ToUpper().Equals(modelo.ToUpper()))
                {
                    TimeSpan dias = DateTime.Now - Convert.ToDateTime(os[4]);
                    if (dias.Days < 91)
                    {
                        listaRetorno.Add(listaAparelhos[i]);
                    }
                }
            }

            return listaRetorno;
        }
    }
}