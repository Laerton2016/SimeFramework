using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class ContatosCliente
    {
        Int32 IDCliente = 0, IDContato = 0;
        String contato = "", Tipo = "";

        public ContatosCliente()
        {
        }
        /// <summary>
        /// Classe que cria um objeto do tipo contatosCliente com informações zeradas para um novo contato
        /// </summary>
        /// <param name="IDCliente">Id do cliente</param>
        /// <param name="contato">Ifnormações do contato</param>
        /// <param name="Tipo">Tipo de contato</param>
        public ContatosCliente(Int32 IDCliente, String contato, String Tipo)
        {
            this.IDCliente = IDCliente;
            this.Tipo = Tipo;
            this.contato = contato;
            
        }
        /// <summary>
        /// Classe que cria um Objeto do tippo ContatosCliente com informação recolhida do banco de dados baseado no ID do contato repassado
        /// </summary>
        /// <param name="IDContato">ID do contato</param>
        public ContatosCliente(Int32 IDContato)
        {
            this.IDContato = IDContato;
            coletaDados();
        }

        private void coletaDados()
        {
            Recordset dados = new Recordset();
            String SQL = "SELECT clientes_contatos.* FROM clientes_contatos WHERE (((clientes_contatos.ID)=" + this.IDContato + "));";
            dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(dados.EOF || dados.BOF))
            {
                IDCliente = Convert.ToInt32(dados.Fields["Cod_cliente"].Value);
                contato = Convert.ToString(dados.Fields["contatos"].Value);
                Tipo = Convert.ToString(dados.Fields["Tipo"].Value);
            }
            dados.Close();
        }

        /// <summary>
        /// Método retorna uma lista contendo todos os contatos de um cliente baseado no ID do cliente repassado
        /// </summary>
        /// <param name="ID_cliente">ID do cliente </param>
        /// <returns>Lista de contatos de um determinado cliente</returns>
        public List<ContatosCliente> getContatosClientes(Int32 ID_cliente)
        {
            List<ContatosCliente> retorno = new List<ContatosCliente>();
            Recordset dados = new Recordset();
            String SQL = "SELECT clientes_contatos.ID FROM clientes_contatos WHERE (((clientes_contatos.cod_cliente)=" + ID_cliente + "));";
            dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (!(dados.EOF || dados.BOF))
            {
                retorno.Add(new ContatosCliente(Convert.ToInt32(dados.Fields["ID"].Value)));
                dados.MoveNext();
            }
            dados.Close();
            return retorno;
        }

        public Int32 getIDCliente()
        {
            return IDCliente;
        }

        public Int32 getID()
        {
            return IDContato;
        }
        public String getTipo()
        {
            return Tipo;
        }
        public String getContato()
        {
            return contato;
        }

        public void setTipo(String Tipo)
        {
            this.Tipo = Tipo;
        }

        public void setContato(String contato)
        {
            this.contato = contato;
        }

        public override string ToString()
        {
            return "Tipo: " + getTipo() + " Contato: " + getContato();
        }
        /// <summary>
        /// Método grava os dados no banco de dados como também atualiza caso já exista.
        /// </summary>
        public void gravar()
        {
            Recordset dados = new Recordset();
            if (IDContato == 0)
            {
                String SQL = "SELECT clientes_contatos.* FROM clientes_contatos";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                dados.AddNew();
               
            }
            else
            {
                String SQL = "SELECT clientes_contatos.* FROM clientes_contatos WHERE (((clientes_contatos.ID)=" + this.IDContato + "));";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }
            dados.Fields["cod_cliente"].Value = IDCliente;
            dados.Fields["Tipo"].Value = Tipo;
            dados.Fields["contatos"].Value = contato;
            dados.Update();
            IDContato = Convert.ToInt32(dados.Fields["ID"].Value);
            dados.Close();
            
        }
        /// <summary>
        /// Método remove o corrente contato
        /// </summary>
        public void remove() {
            if (IDContato != 0)
            {
                Recordset dados = new Recordset();
                String SQL = "DELETE clientes_contatos.* FROM clientes_contatos WHERE (((clientes_contatos.ID)=" + this.IDContato + "));";
                dados.Open(SQL, new Conexao().getDb4(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                IDContato = 0;
                IDCliente = 0;
                contato = "";
                Tipo = "";
            }
        }
    }

}