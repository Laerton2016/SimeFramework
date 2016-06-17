using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class loja 
    {
        private int ID = 0;
        private String razao , cnpj ;
        private Recordset RSDados;
        private String SQL;
        /// <summary>
        /// Classe cria um objeto do tipo loja vazio
        /// </summary>
        /// 
        public loja()
        {
           
        }
        /// <summary>
        /// Classe cria um objeto do tipo loja contendo os dados preenchidos baseado no CNPJ repassado
        /// </summary>
        /// <param name="CNPJ">String CNPJ da Loja</param>
        
        public loja(String CNPJ)
        {
            this.cnpj = CNPJ;
            String SQL = "SELECT loja_venda.* FROM loja_venda WHERE(((loja_venda.CNPJ)='" + cnpj + "'));";
            coletaDados(SQL);
        }
        /// <summary>
        /// Classe cria um objeto do tipo loja contendo os dados preenchidos baseado no ID repassado
        /// </summary>
        /// <param name="ID">Inteiro</param>
        
        public loja(Int32 ID) {
            String SQL = "SELECT loja_venda.* FROM loja_venda WHERE(((loja_venda.id)=" + ID + "));";
            coletaDados(SQL);
        }

        private void coletaDados(String SQL)
        {
            RSDados = new Recordset();
            
            RSDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(RSDados.EOF || RSDados.BOF))
            {
                razao = Convert.ToString(RSDados.Fields["Razão"].Value);
                cnpj = Convert.ToString(RSDados.Fields["CNPJ"].Value);
                ID = Convert.ToInt16(RSDados.Fields["ID"].Value);
            }
            RSDados.Close();
        }

        /// <summary>
        /// Metodo criado para Salvar os dados do cliente 
        /// </summary>
        /// <param name="conex">ADODB.Connection</param>
        /// <returns>Boolean </returns>
        public Boolean salvar()
        {
            RSDados = new Recordset();

            if (this.cnpj != null && this.razao != null)
            {
                if (this.ID == 0)
                {
                    SQL = "SELECT loja_venda.* FROM loja_venda;";
                    RSDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                    RSDados.AddNew();
                }
                else
                {
                    SQL = "SELECT loja_venda.* FROM loja_venda WHERE(((loja_venda.CNPJ)='" + cnpj + "'));";
                    RSDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                }

                RSDados.Fields["CNPJ"].Value = this.cnpj;
                RSDados.Fields["Razão"].Value = this.razao;
                RSDados.Update();
                this.ID = Convert.ToInt16(RSDados.Fields["ID"].Value);

                RSDados.Close();
                return true;

            }
            return false;
        }
        /// <summary>
        /// Método exclui os dados do atual cliente no banco de dados
        /// </summary>
        /// <param name="conex">ADODB.Connection</param>
        /// <returns></returns>
        public Boolean excluir()
        {
            if (cnpj != null)
            {
                SQL = "SELECT loja_venda.* FROM loja_venda WHERE(((loja_venda.CNPJ)='" + cnpj + "'));";
                RSDados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                RSDados.Delete();
                this.cnpj = "";
                this.razao = "";
                this.ID = 0;
                RSDados.Close();
                return true;
            }
            return false;
        }

        public String getRazao()
        {
            return razao;
        }

        public String getCnpj()
        {
            return cnpj;
        }


        public override string ToString()
        {
            return "Razão: " + getRazao() + "\nCNPJ: " + getCnpj(); ;
        }

        public void setCNPJ(String cnpj)
        {
            this.cnpj = cnpj;
        }

        public void setRazao(String razao)
        {
            this.razao = razao;
        }

        public int getID()
        {
            return this.ID;
        }

        
    }
}