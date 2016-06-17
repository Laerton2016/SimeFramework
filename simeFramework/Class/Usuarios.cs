using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using SIME.Class;


namespace Sime
{


    public class Usuarios
    {
        private static ADODB.Connection conexao;
        private static Recordset dados = new Recordset();
        private static String SQL;
        private static VendasUsuario vendas;
        private static int quant;
        private static DropDownList combUsuarios = new DropDownList();
        private static List<Usuario> listaUsuarios = new List<Usuario>();

        /// <summary>
        /// Classe cria um objeto do tipo usuário que contém informações de um determinado usuário do sistema
        /// </summary>
        /// <param name="conexao">recebe um objeto do tipo ADODB.Connection para linkar ao banco de dados</param>
        public Usuarios(Connection conexaoArg)
        {

            conexao = conexaoArg;
            if (dados.State == 0)
            {
                dados.LockType = LockTypeEnum.adLockOptimistic;
                dados.CursorLocation = CursorLocationEnum.adUseClient;
                dados.CursorType = CursorTypeEnum.adOpenDynamic;
            }
            SQL = "SELECT usuarios.* FROM usuarios ORDER BY usuarios.matricula;";
            abreConexao();
            criaListaUsuarios();
            fechaConexao();
        }

        /// <summary>
        /// Método retorna um objeto do tipo vendasUsusario
        /// </summary>
        /// <returns>Tipo vendasUsuário</returns>
        public VendasUsuario getVendas()
        {
            return vendas;
        }
        /// <summary>
        /// Metodo retorna se usuário é válido
        /// </summary>
        /// <returns></returns>
        public bool validaUsuario()
        {
            return (quant < 1) ? false : true;
        }

        private static void criaListaUsuarios()
        {
            dados.MoveFirst();
            listaUsuarios.Clear();
            while (!(dados.BOF || dados.EOF))
            {
                listaUsuarios.Add(new Usuario(Convert.ToInt16(dados.Fields["cod"].Value), Convert.ToString(dados.Fields["Matricula"].Value),
                    Convert.ToString(dados.Fields["senha"].Value), Convert.ToInt16(dados.Fields["tipo"].Value)));
                dados.MoveNext();
            }
        }
        private static void abreConexao()
        {
            if (dados.State != 0)
            {
                fechaConexao();
            }

            dados.Open(SQL, conexao);
            quant = dados.RecordCount;

        }

        private static void fechaConexao()
        {
            if (dados.State != 0)
            {
                dados.Close();
            }
        }

        /// <summary>
        /// Método que preenche um DropdownList com a lista de usuários baseado no tipo de usuário alimentado no construtor do objeto
        /// </summary>
        /// <param name="combo">Recebe um Dripdownlist a ser preenchido</param>
        public void preencheCombo(System.Web.UI.WebControls.DropDownList combo, Usuario usuarioArg)
        {

            combo.Items.Clear();

            if (usuarioArg.getTipo() == 1)
            {
                foreach (Usuario item in listaUsuarios)
                {
                    combo.Items.Add(new ListItem() { Value = Convert.ToString(item.getCod()), Text = item.getNome().ToUpper() });
                }
            }
            else
            {
                combo.Items.Add(new ListItem() {Value = Convert.ToString(usuarioArg.getCod()), Text = usuarioArg.getNome().ToUpper() });
            }

        }

        /// <summary>
        /// Metodo que retorna uma String contendo o resumo de vendas por um período pré-estabelecido.
        /// </summary>
        /// <param name="cod">Inteiro contendo o número do usuário</param>
        /// <param name="inicio">DateTime informando o inicio do período</param>
        /// <param name="fim">DateTime informando o fim do período</param>
        /// <returns>Retorna uma string contendo o resumo de vendas.</returns>
        public string producaoUsuario(int cod, DateTime inicio, DateTime fim)
        {
            String retorno = "Não há movimento.";
            SIME.Conexao conex = new SIME.Conexao();
            vendas = new VendasUsuario(cod, inicio, fim, conex.getDb4());
            retorno = vendas.resumeVendas();
            conex.desconectar();
            return retorno;
        }

        public List<Usuario> getlistaUsuarios()
        {
            return listaUsuarios;
        }
        /// <summary>
        /// Metodo busca por um usuário na lista de usuário e retorna o objeto do tipo Usuario ou null caso não encontre
        /// </summary>
        /// <param name="usuario">Recebe uma String com o nome ou matricula do usuário</param>
        /// <returns>Retorna um objeto do tipo usuario ou null</returns>
        public Usuario buscaUsuario(String usuario)
        {
            Usuario retorno = null;
            foreach (Usuario item in listaUsuarios)
            {
                if (item.getNome().ToUpper().Equals(usuario.ToUpper()))
                {
                    retorno = item;
                    break;
                }
            }

            return retorno;
        }

        public Usuario buscaUsuario(Int32 ID) {

            Usuario retorno = null;
            foreach (Usuario item in listaUsuarios)
            {
                if (item.getCod() == ID)
                {
                    retorno = item;
                    break;
                }
            }

            return retorno;
        }

        /// <summary>
        /// Metodo cria uma cópia do objeto em questão independente na alocação da memória.
        /// </summary>
        /// <returns>Retorna um objeto Usuarios</returns>
        public Usuarios ShalonCopy()
        {
            return (Usuarios)this.MemberwiseClone();
        }

    }
}