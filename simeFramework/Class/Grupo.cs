using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Grupo:ITrataDados
    {
        private Int32 ID = 0;
        private String Tipo;
        private String imagem;
        private Recordset rsDados = new Recordset();
        private Connection conex = new SIME.Conexao().getDb4();
        private String SQL;
        public Grupo()
        {

        }

        public Grupo(Int32 ID)
        {
            this.ID = ID;
            coletaDados();
        }

        private void coletaDados()
        {
            SQL = "SELECT Tipos.* FROM Tipos WHERE (((Tipos.Cod)=" + ID + "));";
            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (rsDados.EOF || rsDados.BOF)
            {
                throw new ArgumentException("Id não localizado.");
            }
            else
            {
                Tipo = Convert.ToString(rsDados.Fields["tipo"].Value);
                imagem = (rsDados.Fields["caminho"].Value.Equals(DBNull.Value)) ? " " : Convert.ToString(rsDados.Fields["caminho"].Value);
            }
        }

        public Int32 getID() { return this.ID; }
        public String getTipo() { return this.Tipo; }
        public String getImagem() { return this.imagem; }

        public void setTipo(String Tipo)
        {
            if (Tipo.Equals("") || Tipo == null)
            {
                throw new ArgumentNullException("Tipo não pode conter dados nulo.");
            }
            else
            {
                this.Tipo = Tipo;
            }
        }

        public void setImagem(string imagem)
        {
            if (imagem.Equals("") || Tipo == null)
            {
                throw new ArgumentNullException("Imagem não pode conter dados nulo.");
            }
            else
            {
                this.imagem = imagem;

            }
        }

        public Boolean salvar()
        {
            return true;
        }

        public Boolean excluir()
        {
            return true;
        }

        public override string ToString()
        {
            return "ID: " + ID + Environment.NewLine +
                   "Tipo: " + Tipo;

        }
    }

}