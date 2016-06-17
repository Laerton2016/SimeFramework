using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class IndicacoesCliente
    {
        Int32 IDCliente = 0, IDIndica = 0; String Tipo = "", contato = "", dado = "";
        /// <summary>
        /// Classe cria um objeto do tipo IndicacoesCiente com dados zerados
        /// </summary>
        public IndicacoesCliente()
        {

        }
        /// <summary>
        /// Classe cria um obsjeto do tipo IndicacoesCliente com os dados de um ID 
        /// </summary>
        /// <param name="IDIndica">ID da indicação</param>
        public IndicacoesCliente(Int32 IDIndica)
        {
            this.IDIndica = IDIndica;
            coletaDados();
        }
        /// <summary>
        /// Classe cria um objeto do tipo IndicaCliente já com dados repassados na sua criação
        /// </summary>
        /// <param name="IDCliente">ID do cliente</param>
        /// <param name="Tipo">Tipo de indicação</param>
        /// <param name="Contato">Informações de contato</param>
        /// <param name="dado">Dados associados ao contato</param>
        public IndicacoesCliente(Int32 IDCliente, String Tipo, String Contato, String dado)
        {
            this.IDCliente = IDCliente;
            this.Tipo = Tipo;
            this.contato = Contato;
            this.dado = dado;
        }

        private void coletaDados()
        {
            Recordset dados = new Recordset();
            String SQL = "SELECT clientes_indica.* FROM clientes_contatos WHERE (((clientes_indica.ID)=" + this.IDIndica + "));";
            dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(dados.EOF || dados.BOF))
            {
                IDCliente = Convert.ToInt32(dados.Fields["Cod_cliente"].Value);
                contato = Convert.ToString(dados.Fields["contatos"].Value);
                Tipo = Convert.ToString(dados.Fields["Tipo"].Value);
                dado = Convert.ToString(dados.Fields["tel"].Value);

            }
            dados.Close();
        }
        /// <summary>
        /// Método retorna uma lista contendo todos os contatos de um ID de cliente
        /// </summary>
        /// <param name="ID_cliente">Id do cliente</param>
        /// <returns>Lista de objetos do tipo IndicaçoesCiente</returns>
        public List<IndicacoesCliente> getIndicacoesClientes(Int32 ID_cliente)
        {
            List<IndicacoesCliente> retorno = new List<IndicacoesCliente>();
            Recordset dados = new Recordset();
            String SQL = "SELECT clientes_indica.* FROM clientes_contatos WHERE (((clientes_indica.ID)=" + this.IDIndica + "));";
            dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (!(dados.EOF || dados.BOF))
            {
                retorno.Add(new IndicacoesCliente(Convert.ToInt32(dados.Fields["ID"].Value)));
                dados.MoveNext();
            }
            dados.Close();
            return retorno;
        }

        public Int32 getID() { return IDIndica; }
        public Int32 getIDClinete() { return IDCliente; }
        public String getTipo() { return Tipo; }
        public String getContato() { return contato; }
        public String getDado() { return dado; }
        public void setTipo(String Tipo) { this.Tipo = Tipo; }
        public void setContao(String Contato) { this.contato = Contato; }
        public void setDado(String dado) { this.dado = dado; }
        public override string ToString()
        {
            return "Tipo: " + getTipo() + " Contato: " + getContato() + " Dado: " + getDado();
        }
        /// <summary>
        /// Grava os dados da indicação
        /// </summary>
        public void gravar()
        {
            Recordset dados = new Recordset();
            if (IDIndica == 0)
            {
                String SQL = "SELECT clientes_Indica.* FROM clientes_indica";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                dados.AddNew();

            }
            else
            {
                String SQL = "SELECT clientes_indica.* FROM clientes_indica WHERE (((clientes_indica.ID)=" + this.IDIndica + "));";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }
            dados.Fields["cod_cliente"].Value = IDCliente;
            dados.Fields["Tipo"].Value = Tipo;
            dados.Fields["contatos"].Value = contato;
            dados.Fields["tel"].Value = dado;

            dados.Update();
            IDIndica = Convert.ToInt32(dados.Fields["ID"].Value);
            dados.Close();

        }

        /// <summary>
        /// Método remove o corrente contato
        /// </summary>
        public void remove()
        {
            if (IDIndica != 0)
            {
                Recordset dados = new Recordset();
                String SQL = "DELETE clientes_indica.* FROM clientes_indica WHERE (((clientes_indica.ID)=" + this.IDIndica + "));";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                IDIndica = 0;
                IDCliente = 0;
                contato = "";
                Tipo = "";
                dado = "";
            }
        }


    }
}