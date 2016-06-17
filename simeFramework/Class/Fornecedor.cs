using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using SIME.Class;

namespace SIME.Class
{
    public class Fornecedor: ITrataDados
    {
        private Int32 id = 0;
        private String nome= "";
        private String razao= "";
        private String CNPJ= "";
        private String IE="";
        private String Endereco= "";
        private String cidade= "";
        private String UF = "";
        private String CEP= "";
        private String bairro = "";
        private Int32 numero = 0;
        private List<ContatosFornecedor> contatos;
        /// <summary>
        /// Classe que cria objeto do tipo Fornecedor para a inclusão de um novo fornecedor.
        /// </summary>
        public Fornecedor() { }
        /// <summary>
        /// Classe que cria objeto do tipo Fornecedor e baseado no argumento de entrada retorna os dados de um fornecedor já cadastrado.
        /// </summary>
        /// <param name="id">Inteiro que contem ID do fornecedor, não permitido valores menores que 1.</param>
        public Fornecedor(Int32 id)
        {
            if (id < 1) { throw new ArgumentException("ID inválido - não pode ser 0 ou negativo."); }
            this.id = id;
            coletaDados();
        }

        /// <summary>
        /// Classe que cria objeto do tipo Fornecedor e baseado no argumento de entrada retorna os dados de um fornecedor já cadastrado.
        /// </summary>
        /// <param name="CNPJ">Cnpj para busca no banco de dados já existente.</param>
        public Fornecedor(String CNPJ) 
        {
            if (CNPJ.Equals("")) { throw new ArgumentException("CNPJ não pode ser em branco ou nulo."); }
            this.id = buscaID(CNPJ);
            if (id != 0)
            {
                coletaDados();
            }
            else { throw new ArgumentException("CNPJ não localizado."); }
        }

        private Int32 buscaID(string cnpj)
        {
            Int32 id = 0;
            Connection conex = new Conexao().getContas();
            Recordset rsDados = new Recordset();
            String SQL = "Select fornecedores.cod, Fornecedores.CNPJ FROM Fornecedores Where(((Fornecedores.cnpj)='" + cnpj + "'));";
            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(rsDados.EOF || rsDados.BOF))
            {
                id = Convert.ToInt32(rsDados.Fields["cod"].Value.ToString());
            }
            rsDados.Close();
            conex.Close();
            return id;
        }
        private void coletaDados() 
        {
            String SQL = "SELECT Fornecedores.* FROM Fornecedores WHERE (((Fornecedores.Cod)="+id+"));";
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getContas();
            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (RSDados.BOF || RSDados.EOF) { throw new ArgumentException("ID inválido."); }
            this.nome = (RSDados.Fields["FORNECEDOR"].Value.Equals(DBNull.Value))?"": RSDados.Fields["FORNECEDOR"].Value.ToString().ToUpper();
            this.razao = (RSDados.Fields["RAZÃO"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["RAZÃO"].Value.ToString().ToUpper();
            this.CNPJ = (RSDados.Fields["CNPJ"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["CNPJ"].Value.ToString().ToUpper();
            this.IE = (RSDados.Fields["ESTADUAL"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["ESTADUAL"].Value.ToString().ToUpper();
            this.Endereco = (RSDados.Fields["END"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["END"].Value.ToString().ToUpper();
            this.cidade = (RSDados.Fields["cidade"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["cidade"].Value.ToString().ToUpper();
            this.UF = (RSDados.Fields["estado"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["estado"].Value.ToString().ToUpper();
            this.CEP = (RSDados.Fields["CEP"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["CEP"].Value.ToString().ToUpper();
            this.bairro = (RSDados.Fields["Bairro"].Value.Equals(DBNull.Value)) ? "" : RSDados.Fields["Bairro"].Value.ToString().ToUpper();
            this.numero = (RSDados.Fields["numero"].Value.Equals(DBNull.Value)) ? 0 : Convert.ToInt32( RSDados.Fields["numero"].Value.ToString());
            RSDados.Close();
            conex.Close();
        }

        public Int32 getID() { return this.id; }
        public String getNome() { return this.nome; }
        public String getRazao() { return this.razao; }
        public String getCNPJ() { return this.CNPJ; }
        public String getIE() { return this.IE; }
        public String getEndereco() { return this.Endereco; }
        public String getCidade() { return this.cidade; }
        public String getUF() { return this.UF; }
        public String getCEP() { return this.CEP; }
        public String getBairro() { return this.bairro; }
        public Int32 getNumero() { return this.numero; }

        public void setBairro(String bairro) { this.bairro = bairro; }

        public void setNumero(Int32 numero)
        {
            if (numero < 0) { throw new ArgumentException("Não é permitido número negativo."); }
            this.numero = numero;
        }

        public void setNome(String nome)
        {
            //if (nome.Length > 20){throw new ArgumentException("Nome do forncedor deve ser resumido em 20 caracteres.");}
            if (nome.Replace(" ", "").Length == 0) { throw new ArgumentException("Nome não pode ser vazio."); }
            if (jaCadastrado(nome)) { throw new ArgumentException("Já existe fornecedor cadastrado com esse nome."); }

            this.nome = nome;
        }

        public void setRazao(String razao)
        {
            if (razao.Length > 45)
            {
                throw new ArgumentException("Razão Social não pode ultrapassar 45 caracteres.");
            }
            this.razao = razao;
        }

        public void setCNPJ(String CNPJ)
        {
            Uteis util = new Uteis();
            if (!(util.validaCNPJ(CNPJ))) {throw new ArgumentException("CNPJ inválido.");}
            if (jaCadastradoCNPJ(CNPJ)) { throw new ArgumentException("CNPJ já cadastrado."); }
            
            this.CNPJ = CNPJ;
        }

        public void setIE(String IE) 
        {
            if (!(new Uteis().Sonumeros(IE))){ throw new ArgumentException("IE só pode conter números.");}
            if (IE.Replace(" ", "").Length == 0) { throw new ArgumentException("IE não pode ser vazio."); }
            this.IE = IE; 
        }
        public void setEndereco(String Endereco)
        {
            if (Endereco.Length > 45) { throw new ArgumentException("Endereço não pode conter mais que 45 caracteres."); }
            if (Endereco.Replace(" ", "").Length == 0) { throw new ArgumentException("Endereço não pode ser vazio."); }

            this.Endereco = Endereco;
        }

        public void setCidade(String cidade)
        {
            if (cidade.Length > 25) { throw new ArgumentException("Cidade não pode ter mais que 25 caracteres."); }
            if (cidade.Replace(" ", "").Length == 0) { throw new ArgumentException("Cidade não pode ser vazio."); }
            this.cidade = cidade;
        }

        public void setUF(String UF) 
        {
            if (UF.Length > 2) { throw new ArgumentException("UF não pode ter mais que 2 caractres."); }
            if (UF.Replace(" ", "").Length == 0) { throw new ArgumentException("UF não pode ser vazio."); }
            this.UF = UF;
        }
        public void setCEP(String CEP) 
        {
            if (CEP.Length > 9) { throw new ArgumentException("CEP não pode ter mais que 9 dígitos."); }
            if (!(new Uteis().Sonumeros(CEP.Replace("-", "")))) { throw new ArgumentException("CEP só pode conter números"); }
            this.CEP = CEP;
        }

        private Boolean jaCadastrado(String nome)
        {
            Recordset RSDados = new Recordset();
            String SQL = "SELECT Fornecedores.* FROM Fornecedores WHERE (((Fornecedores.Fornecedor)='" + nome + "'));"; ;
            Connection conex = new Conexao().getContas();

            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            
            return (!(RSDados.EOF||RSDados.BOF));
        }

        private Boolean jaCadastradoCNPJ(String CNPJ)
        {
            Recordset RSDados = new Recordset();
            String SQL = "SELECT Fornecedores.* FROM Fornecedores WHERE (((Fornecedores.CNPJ)='" + CNPJ + "'));"; ;
            Connection conex = new Conexao().getContas();
            
            RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            return (!(RSDados.EOF || RSDados.BOF) && !(RSDados.Fields["cod"].Value.ToString().Equals(this.id.ToString())) );
        }

        public Boolean salvar() 
        {
            
            String SQL = "";
            if (id == 0)
            {
                SQL = "INSERT INTO Fornecedores ( Fornecedor, Razão, [End], Cidade, Estado, CEP, CNPJ, ESTADUAL, Bairro, Numero ) " +
                      "SELECT '"+ nome + "', '" + razao + "', '" + Endereco + "', '" + cidade + "', '" + UF + "', '" + CEP + "', '" + CNPJ + "', '" + IE +"', '" + bairro + 
                      "', " + numero + ";";

            }
            else 
            {
                SQL = "UPDATE Fornecedores SET " +
                      "Fornecedores.Fornecedor = '" + this.nome + "', " +
                      "Fornecedores.Razão = '" + razao + "', " +
                      "Fornecedores.[End] = '" + Endereco + "', " +
                      "Fornecedores.Cidade = '" + cidade + "' , " +
                      "Fornecedores.Estado = '" + UF + "', " +
                      "Fornecedores.CEP = '" + CEP + "', " +
                      "Fornecedores.CNPJ = '" + CNPJ + "', " +
                      "Fornecedores.ESTADUAL = '" + IE + "', " +
                      "Fornecedores.Bairro = '" + this.bairro + "', " +
                      "Fornecedores.numero = " + this.numero + 
                      "WHERE (((Fornecedores.Cod)=" + this.id.ToString() + "));";

            }
            Recordset rsDados = new Recordset();
            Connection conex = new Conexao().getContas();
            try
            {
                rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                SQL = "SELECT Fornecedores.* FROM Fornecedores WHERE (((Fornecedores.CNPJ)='" + CNPJ + "'));";
                rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                this.id = Convert.ToInt32(rsDados.Fields["cod"].Value);
                rsDados.Close();
            }
            catch (Exception erro)
            {
                
                throw new ArgumentException(erro.Message);
            }
            
            conex.Close();


            return true; 
        }
        public Boolean excluir() 
        {
            if (id == 0) { throw new ArgumentException("Não é possível excluir um dado ainda não gravado."); }
            Recordset RSDados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "DELETE Fornecedores.Fornecedor, Fornecedores.Razão, Fornecedores.Cod " +
                         "FROM Fornecedores WHERE (((Fornecedores.Cod)="+ this.id +"));";
            try
            {
                RSDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }
            catch (Exception erro)
            {
                
                throw new ArgumentException( erro.Message);
            }
            
            conex.Close();

            return true; 
        }

        public override string ToString()
        {
            return "ID: " + this.id + Environment.NewLine + 
                   "Nome: " + this.nome + Environment.NewLine + 
                   "CNPJ: " + this.CNPJ + Environment.NewLine + 
                   "Razão social: " + this.razao + Environment.NewLine +
                   "IE: " + this.IE + Environment.NewLine + 
                   "Cidade: " + this.cidade + " - " + this.UF + Environment.NewLine +
                   "CEP: " + this.CEP;
        }

        public List<Int32> getContatos() 
        {
            Recordset Rsdados = new Recordset();
            Connection conex = new Conexao().getContas();
            String SQL = "SELECT Dados_fornecedores.Cod FROM Dados_fornecedores WHERE (((Dados_fornecedores.Cod_fornecedor)="+id+"));";
            List<Int32> contatos = new List<Int32>();

            Rsdados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            
            while (!(Rsdados.BOF|| Rsdados.EOF))
            { 
                contatos.Add(Convert.ToInt32(Rsdados.Fields["Cod"].Value.ToString()));
                Rsdados.MoveNext();
            }
            return contatos; 
        }

        public void setContato( string Tipo, string Contato, string Dado) 
        {
            ContatosFornecedor contatoSetado = new ContatosFornecedor(this.id, Tipo, Contato, Dado);
            contatoSetado.salvar();
        }

    }
}