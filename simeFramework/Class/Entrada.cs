using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Entrada
    {
        private Int32 Id = 0;
        private Int32 Id_produto = 0;
        private Int32 id_fornecedor = 0;
        private String Nf = "";
        private Int32 quantidade = 0;
        private Boolean sn = false;
        private DateTime dataNF;
        private Int32 Id_empresa = 0;
        private Int32 Id_operador = 0;

        public Entrada(Int32 Id_produto, Int32 id_fornecedor, String Nf, Int32 quantidade, Boolean sn, DateTime dataNF, Int32 Id_operador, Int32 Id_empresa) 
        {
            if (quantidade < 1) { throw new ArgumentException("Não é permitido quantidades inferiores a 1."); }
            if (id_fornecedor < 1 || Id_operador < 1 || Id_empresa < 1) { throw new ArgumentException("Argumento de entrada Id_operador, Id_fornecedor ou Id_empresa não pode ser menores que 1."); }
            this.Id_produto = Id_produto;
            this.id_fornecedor = id_fornecedor;
            this.Nf = Nf;
            this.quantidade = quantidade;
            this.sn = sn;
            this.dataNF = dataNF;
            this.Id_operador = Id_operador;
            this.Id_empresa = Id_empresa;
            salvar();
        }
        public Entrada(Int32 Id) 
        {
            this.Id = Id;
            coletaDados();
        }

        public Int32 getId() { return this.Id; }
        public Int32 getId_fornecedor() { return this.id_fornecedor; }
        public Int32 getId_produto() { return this.Id_produto; }
        public Int32 getId_empresa() { return this.Id_empresa; }
        public Int32 getId_operador() { return this.Id_operador; }
        public String getNf() { return this.Nf; }
        public Boolean getSN() { return this.sn; }
        public DateTime getDataNf() { return this.dataNF; }
        public Int32 getQuantidade() { return this.quantidade; }

        public void setId_fornecedor(Int32 id_fornecedor)
        {
            if (id_fornecedor < 0) { throw new ArgumentException("Não é permitido valores negativos para id_fornecedor."); }
            this.id_fornecedor = id_fornecedor;
        }

        public void setSN(Boolean sn) { this.sn = sn; }
        public void setId_empresa(Int32 Id_empresa)
        {
            if (Id_empresa < 0) { throw new ArgumentException("Não é permitido valores negativos para id_empresa."); }
            this.Id_empresa = Id_empresa;
        }

        public void setId_operador(Int32 Id_operador)
        {
            if (Id_operador < 0) { throw new ArgumentException("Não é permitido valores negativos para id_operador."); }
            this.Id_operador = Id_operador;
        }
        public void setId_produto(Int32 id_produto)
        {
            if (id_produto < 0) { throw new ArgumentException("Não é permitido valores negativos para id_produto."); }
            this.Id_produto = id_produto;
        }
        public void setNF(String NF) { this.Nf = NF; }
        public void setDataNf(DateTime dataNF) { this.dataNF = dataNF; }
        public void setQuantidade(Int32 quantidade)
        {
            if (quantidade < 0) { throw new ArgumentException("Não é permitido valores negativos para quantidade."); }
            this.quantidade = quantidade;
        }
        public Boolean salvar() 
        {
            String SQL = "";
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getDb4();
            DateTime hoje = DateTime.Now;

            if (this.Id == 0)
            {
                SQL = "INSERT INTO Entradas ( Cod, Quantidade, Data, [S/n], Cod_fornecedor, Nota, DataNota, [Local], Op ) " +
                      "SELECT " + this.Id_produto + ", " + 
                      this.quantidade + ", #" + 
                      hoje.Day + "/" + hoje.Month + "/" + hoje.Year + "#, " +
                      this.sn + ", " + 
                      this.id_fornecedor + ", '" + 
                      this.Nf + "', #" + 
                      this.dataNF.Day + "/" + this.dataNF.Month + "/" + this.dataNF.Year + "#, " + 
                      this.Id_empresa +  ", "+ 
                      this.Id_operador + ";";
            }
            else
            {
                
                SQL = "UPDATE Entradas SET " +
                    "Entradas.Cod = " + this.Id_produto + ", " +
                    "Entradas.Quantidade = " + this.quantidade + ", " +
                    "Entradas.Data = #" + hoje.Day + "/" + hoje.Month + "/" + hoje.Year + "#, " +
                    "Entradas.[S/n] = " + this.sn + ", " +
                    "Entradas.Cod_fornecedor = " + this.id_fornecedor + ", " +
                    "Entradas.Nota = '" + this.Nf + "', " +
                    "Entradas.DataNota = #" + this.dataNF.Day + "/" + this.dataNF.Month + "/" + this.dataNF.Year + "#, " +
                    "Entradas.[Local] = " + this.Id_empresa + ", " +
                    "Entradas.Op = " + this.Id_operador + " WHERE (((Entradas.Id)=" + this.Id + "));";
            }

            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            
            if (Id == 0)
            {
                SQL = "SELECT Last(Entradas.Id) AS ÚltimoDeId FROM Entradas;";
                
                RSDados.Open(SQL, conex , CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                Id = Convert.ToInt32(RSDados.Fields["ÚltimoDeId"].Value.ToString());
                RSDados.Close();
            }

            conex.Close();

            return true; 
        }
        public Boolean excluir() 
        { 
            if(Id == 0 ) { throw new ArgumentException("Não é possível excluir um registro ainda não gravado.");}
            
            String SQL = "DELETE Entradas.*, Entradas.Id FROM Entradas WHERE (((Entradas.Id)="+ this.Id +"));";
            Recordset RSdados = new Recordset();
            Connection conex = new Conexao().getDb4();
            RSdados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            Id = 0;
            return true; 
        }
        private void coletaDados() 
        {
            String SQL = "SELECT Entradas.* FROM Entradas WHERE (((Entradas.Id)=" + this.Id + ")  AND ((Entradas.Quantidade)<>0 ));";
            Recordset RSdados = new Recordset();
            Connection conex = new Conexao().getDb4();
            RSdados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (RSdados.BOF || RSdados.EOF) 
            { 
                
                throw new ArgumentException("Id de entrada não válido."); 
            }
            this.dataNF = (RSdados.Fields["DataNota"].Value.Equals(DBNull.Value))? DateTime.Now :Convert.ToDateTime ( RSdados.Fields["DataNota"].Value.ToString());
            this.Id_empresa =(RSdados.Fields["local"].Value.Equals(DBNull.Value))? 1 :  Convert.ToInt32(RSdados.Fields["Local"].Value.ToString());
            this.id_fornecedor = (RSdados.Fields["Cod_Fornecedor"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToInt32(RSdados.Fields["Cod_Fornecedor"].Value.ToString());
            this.Id_operador = (RSdados.Fields["OP"].Value.Equals(DBNull.Value))? 0 : Convert.ToInt32(RSdados.Fields["Op"].Value.ToString());
            this.Id_produto = (RSdados.Fields["Cod"].Value.Equals(DBNull.Value))? 0 : Convert.ToInt32(RSdados.Fields["Cod"].Value.ToString());
            this.Nf = (RSdados.Fields["Nota"].Value.Equals(DBNull.Value))? " " :  RSdados.Fields["Nota"].Value.ToString();
            this.quantidade = (RSdados.Fields["Quantidade"].Value.Equals(DBNull.Value))? 0 : Convert.ToInt32(RSdados.Fields["Quantidade"].Value.ToString());
            this.sn = (RSdados.Fields[3].Value.Equals(DBNull.Value)) ? false : Convert.ToBoolean(RSdados.Fields[3].Value.ToString());
            RSdados.Close();
            conex.Close();
            


        }

        public override string ToString()
        {
            return "ID: " + this.Id.ToString() + Environment.NewLine + 
                   "ID_produto: " + this.Id_produto.ToString() + Environment.NewLine +
                   "ID_Fornecedor: " + this.id_fornecedor.ToString() + Environment.NewLine + 
                   "ID_empresa: " + this.Id_empresa.ToString() + Environment.NewLine + 
                   "ID_operador: " + this.Id_operador.ToString() + Environment.NewLine +
                   "NF Data: " + this.dataNF.ToShortDateString() + Environment.NewLine + 
                   "NF: " + this.Nf + Environment.NewLine +
                   "S/N: " + this.sn + Environment.NewLine +
                   "Quantidade: " + this.quantidade ;
        }

        

        /// <summary>
        /// Método que retorna uma lista de objetos entradas com todas as entradas de uma determinada NF.
        /// </summary>
        /// <param name="Nf">String com o n° da NF</param>
        /// <param name="id_fornecedor">Inteiro com o ID do fornecedor.</param>
        /// <returns></returns>
        public List<Entrada> getListaEntradasNF() 
        {
            List<Entrada> entradas = new List<Entrada>();
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String SQL = "SELECT Entradas.Id, Entradas.Nota, Entradas.Cod_fornecedor FROM Entradas WHERE (((Entradas.Nota)='" + Nf + "') AND ((Entradas.Cod_fornecedor)=" + id_fornecedor + ") AND ((Entradas.Quantidade)<>0));";
            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(RSDados.EOF || RSDados.BOF))
            {
                entradas.Add(new Entrada(Convert.ToInt32(RSDados.Fields["id"].Value)));
                RSDados.MoveNext();
            }
            return entradas; 
        }
    }
}