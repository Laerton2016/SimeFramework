using System;
using System.Collections.Generic;
using ADODB;

namespace SIME.Class
{
    class infCredito
    {
        private Int64 codCliente;
        private List<InformacaoCredito> informacoes = new List<InformacaoCredito>();
        private Recordset dados = new Recordset();
        private Connection conex;
        

        public infCredito(Int64 codCliente, Connection conex)
        {
            this.codCliente = codCliente;
            this.conex = conex;
            coletaDados();
        }

        private void coletaDados()
        {
            String SQL = "SELECT clientes_credito.* FROM clientes_credito WHERE (((clientes_credito.cod_cliente)=" + this.codCliente + "));";
            conectar(SQL);
            while (!(dados.EOF || dados.BOF))
            {
                informacoes.Add(new InformacaoCredito(Convert.ToInt32(dados.Fields["cod"].Value), Convert.ToString(dados.Fields["Credito"].Value)));
                dados.MoveNext();
            }
            desconectar();
        }

        private void conectar(String SQL)
        {
            if (dados.State == 0)
            {
                dados.LockType = LockTypeEnum.adLockOptimistic;
                dados.CursorLocation = CursorLocationEnum.adUseClient;
                dados.CursorType = CursorTypeEnum.adOpenDynamic;
                dados.Open(SQL, conex);
            }
        }

        private void desconectar()
        {
            if (dados.State != 0)
            {
                dados.Close();
            }
        }

        public Int64 getCodCliente()
        {
            return codCliente;
        }

       
        public void gravarInformacoes(InformacaoCredito informacao) {
           String SQL = "INSERT INTO clientes_credito ( cod_cliente, Credito ) "+
                        "SELECT "+ informacao.ToString() +";";
           conectar(SQL);
           informacoes.Clear();
           coletaDados(); 
        }
        public List<InformacaoCredito> getInformacoes() { return informacoes; }

    }
}

