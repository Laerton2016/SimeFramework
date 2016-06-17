using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;

namespace SIME.Class
{
    /// <summary>
    /// Classe trata da conexão com o banco de dados
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>15/01/2016</Data>
    /// </summary>
    public class NetConexao
    {
        private OleDbConnection _simeconnect;
        private OleDbConnection _contas;
        private OleDbConnection _smallConect;
        private static NetConexao _instance;

        private String _simeRede = @"\\100.0.0.254\c\novo\", _smallRede = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";
        private String _simeLocal = @"~\dados\", _smallLocal = @"Dsn=Small;Driver={Firebird/InterBase(r) driver};dbname=100.0.0.250:C:/base/SMALL.GDB;charset=NONE;uid=SYSDBA";

        private NetConexao()
        {
            String sime = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + _simeRede + "BD4.mdb;Persist Security Info=False;Jet OLEDB:Database Password=''";
            String contas = @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= Server.MapPath(" + _simeRede + "contas.mdb);Persist Security Info=False;Jet OLEDB:Database Password=495798";

            
            _simeconnect = new OleDbConnection(sime);
            _smallConect = new OleDbConnection(_smallRede);
            _contas = new OleDbConnection(contas);
        }

        /// <summary>
        /// Cria uma instancia do objeto NetConexao
        /// </summary>
        /// <returns>Objeto netconexao instanciado</returns>
        public static NetConexao Instance()
        {
            if (_instance == null)
            {
                _instance = new NetConexao();
            }
            return _instance;
        }

        /// <summary>
        /// Método retorna uma cocnexão com o banco de dados DB4 do sime 
        /// </summary>
        /// <returns>Oledb Connection</returns>
        public OleDbConnection GetSimeConnect()
        {
            return _simeconnect;
        }
        /// <summary>
        /// Método retorna uma conexão com o banco de dados Small.GBD
        /// </summary>
        /// <returns>Oldb Connection</returns>
        public OleDbConnection GetSmallConnect()
        {
            return _smallConect;
        }

        /// <summary>
        /// Método retona uma conexão com o banco de dados contas
        /// </summary>
        /// <returns>Oledb Connection</returns>
        public OleDbConnection GetContasConnect()
        {
            return _contas;
        }

    }
}