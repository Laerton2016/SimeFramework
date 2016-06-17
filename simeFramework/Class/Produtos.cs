using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using System.Data.OleDb;

namespace SIME.Class
{
    public class Produtos
    {
        Recordset dados = new Recordset();

        public Produtos() { }

        public List<String[]> getListaProdutos(Int32 ID)
        {
            Connection conex = new Conexao().getDb4();
            List<String[]> retorno = new List<String[]>();
            String SQL = "SELECT PRODUTOS.Cod, PRODUTOS.Descrição, PRODUTOS.Codbarras, PRODUTOS.desc, expr5, expr7 FROM PRODUTOS WHERE (((PRODUTOS.Cod)=" + ID + "));";
            if (dados.State != 0) { this.dados.Close(); }
            dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (dados.EOF || dados.BOF)
            {
                throw new ArgumentException("ID de produto inválido.");
            }
            while (!(dados.BOF || dados.EOF))
            {
                String[] conteudo = new String[6];
                conteudo[0] = dados.Fields["cod"].Value.ToString();
                conteudo[1] = dados.Fields["Descrição"].Value.ToString();
                conteudo[2] = dados.Fields["Codbarras"].Value.ToString();
                conteudo[3] = dados.Fields["Desc"].Value.ToString();
                conteudo[4] = dados.Fields["Expr5"].Value.ToString();
                conteudo[5] = dados.Fields["expr7"].Value.ToString();
                retorno.Add(conteudo);
                dados.MoveNext();
            }
            return retorno;
        }

        public List<String[]> getListaProdutos(String dado)
        {
            return getListaProdutos(dado, false);
        }

        public List<String[]> getListaProdutos(String dado, Boolean EAN, Boolean SemEstoque)
        {
            Connection conex = new Conexao().getDb4();
            List<String[]> retorno = new List<String[]>();
            
            String SQL = "SELECT PRODUTOS.Cod, PRODUTOS.Descrição, PRODUTOS.Codbarras, PRODUTOS.desc FROM PRODUTOS";
            
            if (EAN == false)
            {
                SQL +=   " WHERE PRODUTOS.Descrição Like '%" + dado + "%'" ;
            }
            else
            {
                SQL += " WHERE PRODUTOS.Codbarras ='" + dado + "'";

            }

            if (!SemEstoque)
            {
                SQL += " and estoque > 0 ";
            }

            SQL += " ORDER BY PRODUTOS.Descrição;";

            if (dados.State != 0) { this.dados.Close(); }

            dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (dados.EOF || dados.BOF)
            {
                throw new ArgumentException("O resultado é de 0 registros. Reveja os dados de busca.");
            }
            while (!(dados.BOF || dados.EOF))
            {
                String[] conteudo = new String[4];
                conteudo[0] = dados.Fields["cod"].Value.ToString();
                conteudo[1] = dados.Fields["Descrição"].Value.ToString();
                conteudo[2] = dados.Fields["Codbarras"].Value.ToString();
                conteudo[3] = dados.Fields["Desc"].Value.ToString();
                retorno.Add(conteudo);
                dados.MoveNext();
            }
            return retorno;
        }
        public List<String[]> getListaProdutos(String dado, Boolean EAN)
        {
            return getListaProdutos(dado, EAN, true);
        }

    }
}