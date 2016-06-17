using System;
using System.Collections.Generic;
using System.Linq;
using ADODB;

namespace SIME.Class
{
    public class Empresas
    {
        private DAO.DAOEmpresa _dao;   
        public Empresas() { }
        public List<Fornecedor> montaComboEmpresas ()
        {
            List<Fornecedor> retorno = new List<Fornecedor>();

            return retorno;
        }
        /** METODO USADO NA FACE
        public void montaComboEmpresas(DropDownList combEmpresas) 
        {
            combEmpresas.Items.Clear();
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String sql = "SELECT DESTINO.* FROM DESTINO ORDER BY DESTINO.Descrição; ";
            rsDados.Open(sql, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(rsDados.BOF|| rsDados.EOF))
            {
                combEmpresas.Items.Add(new ListItem(rsDados.Fields["Descrição"].Value.ToString().ToUpper(), rsDados.Fields["cod"].Value.ToString()));
                rsDados.MoveNext();
            }
        }
        */
    }
}