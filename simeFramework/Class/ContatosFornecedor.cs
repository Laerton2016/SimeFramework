using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;

namespace SIME.Class
{
    class ContatosFornecedor
    {
        private  Int32 Id = 0;
        private  Int32 IdFornecedor = 0;
        private  String tipo;
        private  String contato;
        private  String dado;

        public  ContatosFornecedor() { }

        public  ContatosFornecedor(Int32 IDFornecedor, string Tipo, string Contato, string Dado) 
        {
            IdFornecedor = IDFornecedor;
            tipo = Tipo;
            contato = Contato;
            dado = Dado;
            salvar();
        }
        public  ContatosFornecedor(Int32 ID) 
        {
            Id = ID;
            coletarDados();
        }
        public Int32 getID() { return this.Id; }
        public Int32 getIDFornecedor() { return this.IdFornecedor; }
        public String getTipo() { return this.tipo; }
        public String getContato() { return this.contato; }
        public String getDado() { return this.dado; }

        public void setTipo(String tipo) { this.tipo = tipo; }
        public void setContato(String contato) { this.contato = contato; }
        public void setDado(String dado) { this.dado = dado; }

        private  void coletarDados() 
        {
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "SELECT DADOS_FORNECEDORES.* FROM DADOS_FORNECEDORES WHERE(((DADOS_FORNECEDORES.COD)="+ Id +"));";
            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (RSDados.BOF || RSDados.EOF) { throw new ArgumentException("Id Contado de forncedor inválido!"); }

            IdFornecedor = Convert.ToInt32(RSDados.Fields["COD_FORNECEDOR"].Value.ToString()) ;
            tipo = RSDados.Fields["TIPO"].Value.ToString();
            contato = RSDados.Fields["Contato"].Value.ToString();
            dado = RSDados.Fields["dados"].Value.ToString();
            RSDados.Close();
            conex.Close();
        }
        public override string ToString()
        {
            return "ID = " + Id + Environment.NewLine +
                   "ID_FORNECEDOR = " + IdFornecedor + Environment.NewLine +
                   "TIPO = " + tipo + Environment.NewLine + 
                   "CONTATO = " + contato + Environment.NewLine +
                   "DADOS = " + dado ;
        }

        public  Boolean salvar() 
        {
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "";
            if (Id == 0)
            {
                SQL = "INSERT INTO Dados_fornecedores ( Cod_fornecedor, Tipo, Contato, Dados ) " +
                      "SELECT " + IdFornecedor + " , '" + tipo + "' , '" + contato + "' , '" + dado + "' ;";

            }
            else
            {
                SQL = "UPDATE Dados_fornecedores SET " +
                      "Dados_fornecedores.Cod_fornecedor = " + IdFornecedor + ", " +
                      "Dados_fornecedores.Tipo = '" + tipo + "', " +
                      "Dados_fornecedores.Contato = '" + contato + "', " +
                      "Dados_fornecedores.Dados = '" + dado + "' " +
                      "WHERE (((Dados_fornecedores.Cod)=" + Id + "));";

            }

            try
            {
                RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                
                if (Id == 0)
                {
                    SQL = "SELECT Last(Dados_fornecedores.Cod) AS ÚltimoDeCod FROM Dados_fornecedores ORDER BY Last(Dados_fornecedores.Cod);";
                    RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    Id = Convert.ToInt32(RSDados.Fields[0].Value);
                    RSDados.Close();
                }

                conex.Close();
            }
            catch (Exception erro)
            {

                throw new ArgumentException(erro.Message) ;
            }
            
            return true; 
        }
        public Boolean excluir() 
        {
            if (Id == 0) { throw new ArgumentException("Não pode excluir um registro ainda não gravado."); }
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "DELETE Dados_fornecedores.Cod FROM Dados_fornecedores WHERE (((Dados_fornecedores.Cod)="+ Id +"));";

            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            conex.Close();
            Id = 0;
            return true; 
        }

    }

    
}
