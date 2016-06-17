using System;
using System.Collections.Generic;
using System.Linq;
using ADODB;

namespace SIME.Class
{
    public class Fornecedores
    {
        public Fornecedores() { }

        public List<NetFornecedor> MontaListaForncedores()
        {

        } 

        /*
        public void MontaListaFornecedores(DropDownList combFornecedores) 
        {
            combFornecedores.Items.Clear();
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "SELECT Fornecedores.* FROM Fornecedores ORDER BY Fornecedores.Fornecedor;";

            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(rsDados.BOF|| rsDados.EOF))
            {
                combFornecedores.Items.Add(new ListItem(rsDados.Fields["Fornecedor"].Value.ToString().ToUpper(),rsDados.Fields["cod"].Value.ToString()));
                rsDados.MoveNext();
            }
        }
        */
    }
}