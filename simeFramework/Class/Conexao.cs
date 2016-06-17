using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using System.Data;

namespace SIME
{
    public class Conexao
    {
        private Connection DB4, Contas, Small;
        private System.Data.OleDb.OleDbConnection DB4net, Contasnet, Smallnet;
        private String endereco = @"c:\cdg\", endSamll = @"Dsn=small_local;Driver={Firebird/InterBase(r) driver};dbname=C:/Base/SMALL.GDB;charset=NONE;uid=SYSDBA";

        //private String endereco = @"\\100.0.0.254\c\novo\", endSamll = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
        //private String endereco = @"~\dados\", endSamll = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
        /// <summary>
        /// Classe que cria um objeto do tipo conexão que contém 03 conecction do tipo ADODB
        /// Trata dos banco de dados DB4, contas e Small
        /// </summary>
        public Conexao() {
            DB4 = new Connection();
            Contas = new Connection();
            Small = new Connection();
            DB4net = new System.Data.OleDb.OleDbConnection();
            Contasnet = new System.Data.OleDb.OleDbConnection();
            Smallnet = new System.Data.OleDb.OleDbConnection();
            //conectar();
        }
        /// <summary>
        /// Método que conectar os links via ado.net 4.0 aos bancos de dados.
        /// </summary>
        public void conectarNet() {
            if (DB4net.State == ConnectionState.Closed)
            {
                DB4net.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source="+ endereco +"BD4.mdb;Persist Security Info=False;Jet OLEDB:Database Password=''";
                DB4net.Open();
            }

            if (Contasnet.State == ConnectionState.Closed) {
                Contasnet.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= Server.MapPath(" + endereco + "contas.mdb);Persist Security Info=False;Jet OLEDB:Database Password=495798";
                Contasnet.Open();
            }
            
            if (Smallnet.State == ConnectionState.Closed) {
                Smallnet.ConnectionString = endSamll;
                Smallnet.Open();
            }
            
        }
        /// <summary>
        /// Método que linka aos bancos de dados por meio do MDAC 2.8
        /// </summary>
        public void conectar()
        {
            if (DB4.State == 0)
            {
                DB4.CursorLocation = CursorLocationEnum.adUseServer;
                DB4.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + endereco + "BD4.mdb;Persist Security Info=False;Jet OLEDB:Database Password=''";
                DB4.Open();
            }

            if (Contas.State == 0)
            {
                Contas.CursorLocation = CursorLocationEnum.adUseServer;
                Contas.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + endereco + "contas.mdb;Persist Security Info=False;Jet OLEDB:Database Password=495798";
                Contas.Open();
            }
            
            if (Small.State == 0)
            {
                Small.CursorLocation = CursorLocationEnum.adUseServer;
                //Small.ConnectionString = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
                //Small.ConnectionString = @"Dsn=small_local;Driver={Firebird/InterBase(r) driver};dbname=C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
                Small.ConnectionString = endSamll;
                Small.Open();
            }
             
        }
        /// <summary>
        /// Método desconecta todos os links 
        /// </summary>
        public void desconectar() {
            DB4.Close();
            if (Contas.State != 0)
            {
                Contas.Close();
            }
            //Small.Close();
            if (DB4.State != 0)
            {
                DB4net.Close();
            }
            if (Contasnet.State != 0)
            {
                Contasnet.Close();
            }
            //Smallnet.Close();

        }

        /// <summary>
        /// Método retorna objeto do tipo ADODB.Connection para o banco de dados DB4
        /// </summary>
        /// <returns></returns>
        public Connection getDb4() {
            if (DB4.State == 0)
            {
                DB4.CursorLocation = CursorLocationEnum.adUseServer;
                DB4.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + endereco + "BD4.mdb;Persist Security Info=False;Jet OLEDB:Database Password=''";
                DB4.Open();
            }
            return (DB4.State ==0)? null: DB4;
        }
        /// <summary>
        /// Método retorna objeto do tipo oleDbConnection para o banco de dados DB4
        /// </summary>
        /// <returns></returns>
        public System.Data.OleDb.OleDbConnection getDB4net() {
            return (DB4net.State == ConnectionState.Closed) ? null : DB4net;
        }
        /// <summary>
        /// Método retorna objeto do tipo oleDbConnection para o banco de dados Contas
        /// </summary>
        /// <returns></returns>
        public System.Data.OleDb.OleDbConnection getContasnet()
        {
            return (Contasnet.State == ConnectionState.Closed) ? null : Contasnet;
        }
        /// <summary>
        /// Método retorna objeto do tipo oleDbConnection para o banco de dados Small
        /// </summary>
        /// <returns></returns>
        public System.Data.OleDb.OleDbConnection getSmallnet()
        {
            return (Smallnet.State == ConnectionState.Closed) ? null : Smallnet;
        }

        /// <summary>
        /// Método retorna objeto do tipo ADODB.Connection para o banco de dados Contas
        /// </summary>
        /// <returns></returns>
        public Connection getContas() {
            if (Contas.State == 0)
            {
                Contas.CursorLocation = CursorLocationEnum.adUseServer;
                Contas.ConnectionString = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + endereco + "contas.mdb;Persist Security Info=False;Jet OLEDB:Database Password=495798";
                Contas.Open();
            }
            return (Contas.State == 0) ? null : Contas;
        }
        /// <summary>
        /// Método retorna objeto do tipo ADODB.Connection para o banco de dados Small.gdb
        /// </summary>
        /// <returns></returns>
        public Connection getSmall() {
            if (Small.State == 0)
            {
                Small.CursorLocation = CursorLocationEnum.adUseServer;
                //Small.ConnectionString = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
                //Small.ConnectionString = @"Dsn=small_local;Driver={Firebird/InterBase(r) driver};dbname=C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
                Small.ConnectionString = endSamll;
                Small.Open();
            }
            return (Small.State == 0) ? null : Small;
        }
    }
}