using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using SIME;

namespace SIME.Class
{
    public class lojas
    {
        private List<String[]> dadosCombo = new List<string[]>();
        private String SQL;
        /// <summary>
        /// Classe pra criar um novo Objeto lojas.
        /// </summary>
        public lojas()
        {
            SQL = "SELECT loja_venda.*  FROM loja_venda ORDER BY loja_venda.razão;";
            coletaDados(SQL);
        }

        private void coletaDados(string SQL)
        {
            Recordset dados = new Recordset();
            dados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            dadosCombo.Clear();
            dadosCombo.Add(new String[] { "0", "NÃO SELECIONADO" });
            while (!(dados.EOF || dados.BOF))
            {
                dadosCombo.Add(new String[] { Convert.ToString(dados.Fields["ID"].Value), Convert.ToString(dados.Fields["Razão"].Value) });
                dados.MoveNext();
            }
            dados.Close();
        }
        /// <summary>
        /// Método retorna uma lista de Array de String contendo os dados das lojas 
        /// </summary>
        /// <returns></returns>
        public List<String[]> getListaLojas()
        {
            return dadosCombo;
        }

        /// <summary>
        /// Método retorna um Objeto do tipo loja baseada no CNPJ repassdo como argumento.
        /// caso a loja não esteja cadastrada retorna null
        /// </summary>
        /// <param name="ID">String</param>
        /// <returns>Loja</returns>
        public loja getLoja(String cnpj)
        {
            loja retorno = new loja(cnpj);
            
            return (retorno.getID() == 0)? null : retorno;
        }
        
        /// <summary>
        /// Método retorna um Objeto do tipo loja baseada no ID repassdo como argumento.
        /// caso a loja não esteja cadastrada retorna null
        /// </summary>
        /// <param name="ID">Inteiro</param>
        /// <returns>Loja</returns>
        public loja getLoja(Int32 ID)
        {
            loja retorno = new loja(ID);

            return (retorno.getID() == 0) ? null : retorno;
        }

        public void setLoja(String cnpj, String razão) {
            setLoja(cnpj, razão, true);
        }

        /// <summary>
        /// Método recebe parametros que cadastra uma nova loja revendedora 
        /// </summary>
        /// <param name="cnpj">String contendo o Cnpj da loja</param>
        /// <param name="razão">String contendo nome da loja </param>
        public void setLoja(String cnpj, String razão, Boolean atualizar) {

            loja loja1 = new loja(cnpj);
            if (atualizar) {
                loja1.setRazao(razão);
                loja1.salvar();
            }
            System.Threading.Thread.Sleep(500); // Metodo que faz a thread pausar por um determinado tempo.
            coletaDados(SQL);
        }


    }
}