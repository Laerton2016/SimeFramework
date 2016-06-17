using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;
using SIME.Class;

namespace SIME.Class
{
    /// <summary>
    /// Objeto do tipo cliente contendo todos os dados de um determinado cliente baseado no banco de dados SIME
    /// </summary>
    public class Cliente : ITrataDados
    {
        //Variáveis de informações de clientes
        //Tabela Clientes
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
        private Int32 cod = 0;
        private String nome = "", end = "", telefone = "", operadora = "", cpfcnpj = "", email = "", cep = "", cidade = "", UF = "", IE = " ", referencia = "", bairro = "";
        private Boolean mala = false, pessoaJuridica = false, restrito = false;
        private DateTime dataCadastro = DateTime.Now;
        private String erro = "";
        //Tabela Clientes_Filiacao
        private String classificacao = "", pai = "", mae = "", endPais = "";
        private DateTime datanascimento;
        private Double limite = 0;
        private Boolean _fidelizado = false;
        private DateTime _dataFidelizacao;

        //Variáveis de dados 

        private Connection conex;
        private Recordset dadosCliente = new Recordset();
        private Conexao conexao;
        private List<InformacaoCredito> informacoes = new List<InformacaoCredito>();
        private List<ContatosCliente> contatos = new List<ContatosCliente>();
        private List<IndicacoesCliente> indicacoes = new List<IndicacoesCliente>();
        /// <summary>
        /// Classic cria um objeto cliente pronto limpo pronto para ser adicionado no sistema, usado para um novo cliente
        /// </summary>
        /// <param name="conex">Objeto do tipo ADODB Connection</param>
        public Cliente(Connection conex)
        {
            this.conex = conex;
        }
        /// <summary>
        /// Cria o objeto cliente com os dados do cliente pelo codigo informado
        /// </summary>
        /// <param name="cod">Inteiro de 32 bits contendo o cod do cliente válido</param>
        /// <param name="conex">ADODB connection contendo a conexão para o banco de dados</param>
        public Cliente(Int32 cod, Connection conex)
        {
            this.cod = cod;
            this.conex = conex;
            coletaDados();
            coletaDadosCredito();

        }
        /// <summary>
        /// Cria o objeto cliente com os dados do cliente pelo codigo informado
        /// </summary>
        /// <param name="p">Interiro contendo o cod do cliente válido</param>
        /// <param name="conexao">ADODB connection contendo a conexão para o banco de dados</param>
        public Cliente(int p, Conexao conexao)
        {
            this.cod = p;
            this.conexao = conexao;
            coletaDados();
            coletaDadosCredito();
        }

        private Boolean inserirDadosPricipais()
        {

            String SQL = "INSERT INTO CLIENTES (NOME, [END], TELE1, DADO1, MALA, CNPJ, [E-MAIL], CEP, FJ, CIDADE, UF, INSC, RESTRITO, REFERENCIA, nascimento, BAIRRO, fidelidade, Dt_inicio)" +
                 " SELECT '" + getNome() + "','" + getEnd().Replace(',', '-') + "','" + getTelefone() + "','" + getOperadora() + "'," + getMala() + ",'" + getCPFCNPJ() +
                 "','" + getEmail() + "','" + getCEP() + "'," + getPessoaJuridica() + ",'" + getCidade() + "','" + getUF() + "','" + getIE() + "'," + getRestrito() + ",'" +
                 getReferencia().Replace(',', '-') + "',#" + getDataNascimento().ToShortDateString() + "#,'" + getBairro() + "'," + _fidelizado + ",#" + _dataFidelizacao.ToShortDateString() + "#;";
            try
            {
                conectar(SQL);
                desconectar();
                return true;
            }
            catch (Exception e)
            {

                erro = e.Message;
                return false;
            }

        }

        private Boolean inserirDadosPais(Int32 ID)
        {
            String SQL = @"INSERT INTO Clientes_filiacao (PAI, MAE, ENDEREÇO, LIMITE, CLASSIFICA, DATA, [COD_CLIENTE]) " +
                    "SELECT '" + getPai() + "', '" + getMae() + "', '" + getEndPais() + "', " + getLimite() + ", 'Sem classificação', " +
                    "#" + getDataNascimento().ToShortDateString() + "#" + "," + ID + ";";
            try
            {
                conectar(SQL);
                return true;

            }
            catch (Exception e)
            {

                erro = e.Message;
                return false;
            }

        }

        private Boolean verificaPais()
        {
            //Metodo que verifica se o cliente já tem dados de pais
            String SQL = "SELECT Clientes_filiacao.cod FROM Clientes_filiacao WHERE Clientes_filiacao.cod_cliente = " + this.cod + ";";
            Recordset dados = new Recordset();
            Connection conect = new Conexao().getDb4();
            dados.LockType = LockTypeEnum.adLockOptimistic;
            dados.CursorLocation = CursorLocationEnum.adUseClient;
            dados.Open(SQL, conect);

            return dados.RecordCount > 0;
        }

        private Boolean atualizaDadosPais(Int32 ID)
        {

            String SQL = @"UPDATE Clientes_filiacao SET " +
                "Pai = '" + getPai() + "', " +
                "mae = '" + getMae() + "', " +
                "Endereço = '" + getEndPais() + "', " +
                "Limite = " + getLimite() + ", " +
                "classifica = '" + getClassificacao() + "', " +
                "Data =  #" + getDataNascimento().ToShortDateString() + "# " +
                "WHERE Clientes_filiacao.Cod_cliente =" + ID + ";";
            try
            {
                if (verificaPais())
                {
                    conectar(SQL);
                }
                else
                {
                    inserirDadosPais(ID);
                }

                erro = "";
                return true;
            }
            catch (Exception e)
            {
                erro = e.Message;
                return false;
            }

        }
        /// <summary>
        /// Fidelização do cliente do processo de fidelidade
        /// </summary>
        public bool Fidelidade { get { return _fidelizado; } set { this._fidelizado = value; } }

        /// <summary>
        /// Data do inicio da fidelização do cliente 
        /// </summary>
        public DateTime Dt_inicializacao { get { return _dataFidelizacao; } set { this._dataFidelizacao = value; } }
        
        private Boolean atualizaDadosPrincipais()
        {
            String SQL = "UPDATE Clientes SET " +
                    "Nome = '" + getNome() + "', " +
                    "[End] = '" + getEnd() + "'," +
                    "Tele1 = '" + getTelefone() + "', " +
                    "Dado1 = '" + getOperadora() + "', " +
                    "Mala = " + getMala() + ", " +
                    "CNPJ = '" + getCPFCNPJ() + "', " +
                    "[E-mail] = '" + ((email==null)?" ":email )+ "', " +
                    "CEP = '" + getCEP() + "', " +
                    "FJ = " + getPessoaJuridica() + ", " +
                    "cidade = '" + getCidade() + "', " +
                    "UF = '" + getUF() + "', " +
                    "insc = '" + getIE() + "', " +
                    "Restrito = " + getRestrito() + ", " +
                    "referencia = '" + getReferencia() + "', " +
                    "nascimento = #" + getDataNascimento().ToShortDateString() + "#, " +
                    "bairro = '" + getBairro() + "'," +
                    "fidelidade = " + _fidelizado + "," +
                    "dt_inicio = #" + _dataFidelizacao.ToShortDateString() + "# " +  
                    "WHERE [Cod_cliente]=" + cod + ";";

            try
            {
                conectar(SQL);
                erro = "";
                return true;
            }
            catch (Exception e)
            {
                erro = e.Message;
                return false;
            }
        }
        /**
        /// <summary>
        /// Método para gravar os dados do cliente no banco de dados.
        /// </summary>
        /// <returns>Retorna um boolean para o resultado da gravação</returns>
        public Boolean gravaDados()
        {
            if (cod == 0) 
            {
                return inserirDadosPricipais() && inserirDadosPais(); ;
            }
            else
            {
                
                return atualizaDadosPrincipais() && atualizaDadosPais();
            }


        }
         **/
        public String toErro() { return erro; }

        /// <summary>
        /// Nome do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getNome() { return nome; }
        /// <summary>
        /// Endereço do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getEnd() { return end; }
        /// <summary>
        /// Cpf ou CNPJ do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getCPFCNPJ() { return cpfcnpj; }
        /// <summary>
        /// Telefone do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getTelefone() { return telefone; }
        /// <summary>
        /// Operadora do telefone 
        /// </summary>
        /// <returns>String</returns>
        public String getOperadora() { return operadora; }
        /// <summary>
        /// E-mail do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getEmail() { return email; }
        /// <summary>
        /// Cep do endereço do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getCEP() { return cep; }
        /// <summary>
        /// Cidade do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getCidade() { return cidade; }
        /// <summary>
        /// Estado do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getUF() { return UF; }
        /// <summary>
        /// Inscrição estadual ou RG do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getIE() { return IE; }
        /// <summary>
        /// Mome do pai do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getPai() { return pai; }
        /// <summary>
        /// Nome da mãe do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getMae() { return mae; }
        /// <summary>
        /// Endereço dos pais do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getEndPais() { return endPais; }
        /// <summary>
        /// Classificação financeira do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getClassificacao() { return classificacao; }
        /// <summary>
        /// Envio de mala direta para o cliente
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean getMala() { return mala; }
        /// <summary>
        /// Se é pessoa Juridica caso seja o retorna é true
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean getPessoaJuridica() { return pessoaJuridica; }
        /// <summary>
        /// Se o cliente está bloqueado no sistema, retorna como padrão false (não bloqueado).
        /// </summary>
        /// <returns>Boolean</returns>
        public Boolean getRestrito() { return restrito; }
        /// <summary>
        /// Data do cadastro do cliente
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDataCadastro() { return dataCadastro; }
        /// <summary>
        /// Data de nascimento do cliente
        /// </summary>
        /// <returns>DateTime</returns>
        public DateTime getDataNascimento() { return datanascimento; }
        /// <summary>
        /// Limite de crédito para o cliente
        /// </summary>
        /// <returns>Double</returns>
        public Double getLimite() { return limite; }
        /// <summary>
        /// Retorna o código do cliente
        /// </summary>
        /// <returns>Int32</returns>
        public Int32 getCod() { return cod; }
        /// <summary>
        /// Retorna o ponto de referencia do cliente
        /// </summary>
        /// <returns>String</returns>
        public String getReferencia() { return referencia; }
        /// <summary>
        /// Método para setar o endereço de bairro
        /// </summary>
        /// <param name="bairro">Recebe uma String contendo o bairro</param>
        public void setBairro(String bairro)
        {
            if (bairro.Length <= 30)
            {
                this.bairro = bairro;
            }
            else
            {
                throw new ArgumentException("Bairro não pode ser superior a 30 caracteres.");
            }
        }
        /// <summary>
        /// Seta nome do cliente
        /// </summary>
        /// <param name="nome">String</param>
        public void setNone(String nome)
        {
            if (nome.Length > 35)
            {
                throw new ArgumentException("Campo nome não pode ser superior a 35 caracteres");
            }
            this.nome = nome;
        }
        /// <summary>
        /// Seta o endereço do cliente
        /// </summary>
        /// <param name="end">String</param>
        public void setEnd(String end)
        {
            if (end.Length > 400)
            {
                throw new ArgumentException("Campo endereço não pode ser superior a 40 caracteres");
            }
            this.end = end;

        }
        /// <summary>
        /// Seta o telefone do cliente
        /// </summary>
        /// <param name="telefone">String</param>
        public void setTelefone(String telefone)
        {
            if (telefone.Length > 16)
            {
                throw new ArgumentException("Campo telefone não pode ser superior a 16 caracteres");
            }

            if (new Uteis().Sonumeros(telefone) == false)
            {
                throw new ArgumentException("Campo telefone só pode conter caracteres numéricos.");
            }
            this.telefone = telefone;
        }
        /// <summary>
        /// Seta operadora do cliente
        /// </summary>
        /// <param name="operadora">String</param>
        public void setOperadora(String operadora) { this.operadora = operadora; }
        /// <summary>
        /// Seta o E-mail do cliente
        /// </summary>
        /// <param name="email"></param>
        public void setEmail(String email)
        {
            if (email.Length > 80)
            {
                throw new ArgumentException("Campo e-mail não pode ultrapassar 80 caracteres.");
            }

            if (email.Trim().Length != 0)
            {
                if (new Uteis().ValidaEmail(email) == false)
                {
                    throw new ArgumentException("E-mail informado inválido.");
                }
            }

            this.email = email;
        }
        /// <summary>
        /// Seta o CEP do cliente
        /// </summary>
        /// <param name="cep">String</param>
        public void setCep(String cep)
        {
            this.cep = cep;
        }
        /// <summary>
        /// Seta a cidade do cliente
        /// </summary>
        /// <param name="cidade">String</param>
        public void setCidade(String cidade)
        {
            if (cidade.Length > 40)
            {
                throw new ArgumentException("O campo Cidade não pode ultrapassar 40 caracteres.");
            }

            this.cidade = cidade;
        }
        /// <summary>
        /// Seta o estado (UF) do cliente
        /// </summary>
        /// <param name="UF">String</param>
        public void setUF(String UF)
        {

            this.UF = UF;
        }
        /// <summary>
        /// Seta a Insc. estadual ou RG do cliente
        /// </summary>
        /// <param name="IE">String</param>
        public void setIE(String IE)
        {
            if (IE.Equals("Isento"))
            {
                this.IE = IE;
                return;
            }

            if (IE.Length > 16)
            {
                throw new ArgumentException("Campo inscrição e/ou RG só pode conter até 16 caracteres.");
            }

            Uteis valida = new Uteis();
            if (getPessoaJuridica())
            {

                if (!(IE.Equals("Isento")) && !(valida.Sonumeros(IE)))
                {
                    throw new ArgumentException("A inscrição estadual só pode conter número e caracteres especiais.");
                }
                if (!(valida.ValidaIE(IE, UF)))
                {
                    throw new ArgumentException("Inscrição estadual inválida.");
                }
            }

            this.IE = IE;
        }
        /// <summary>
        /// Seta o ponto de referencia
        /// </summary>
        /// <param name="referencia">String</param>
        public void setReferecia(String referencia) { this.referencia = referencia; }
        /// <summary>
        /// Seta o nome do pai
        /// </summary>
        /// <param name="pai"></param>
        public void setPai(String pai) { this.pai = pai; }
        /// <summary>
        /// Seta nome da mãe
        /// </summary>
        /// <param name="mae"></param>
        public void setMae(String mae) { this.mae = mae; }
        /// <summary>
        /// Seta o endereço dos pais
        /// </summary>
        /// <param name="endPais">String</param>
        public void setEndPais(String endPais) { this.endPais = endPais; }
        /// <summary>
        /// Seta a classificação do cliente - Financeiro
        /// </summary>
        /// <param name="classificacao">String</param>
        public void setClassificacao(EnumClassificacao classificacao)
        {
            this.classificacao = classificacao.ToString();
        }
        /// <summary>
        /// Seta a mala direta 
        /// </summary>
        /// <param name="mala">Boolean</param>
        public void setMala(Boolean mala) { this.mala = mala; }
        /// <summary>
        /// Seta pessoa juridica
        /// </summary>
        /// <param name="PJ">Boolean</param>
        public void setPessoaJuridica(Boolean PJ) { this.pessoaJuridica = PJ; }
        /// <summary>
        /// Seta se o cliente é bloqueado
        /// </summary>
        /// <param name="restrito">Boolean</param>
        public void setRestrito(Boolean restrito) { this.restrito = restrito; }
        /// <summary>
        /// Seta data cadastro - preenchido automatico ao criar o objeto com a data de hoje ou do cadastro do cliente
        /// </summary>
        /// <param name="dataCadastro">DateTime</param>
        public void setDataCadastro(DateTime dataCadastro)
        {

            this.dataCadastro = dataCadastro;
        }
        /// <summary>
        /// Seta data de nascimento
        /// </summary>
        /// <param name="datanascimento">DateTime</param>
        public void setDataNascimento(DateTime datanascimento)
        {
            TimeSpan dif = DateTime.Now.Subtract(datanascimento);
            if ((dif.Days / 365) < 7)
            {
                throw new ArgumentException("Data de nascimento inválido o cliente tem menos de 7 anos.");
            }

            this.datanascimento = datanascimento;
        }
        /// <summary>
        /// Seta limite de crédito, como padrão o sistema disponibiliza limite 0
        /// </summary>
        /// <param name="limite">Double</param>
        public void setLimite(Double limite)
        {
            if (limite < 0)
            {
                throw new ArgumentException("Não é permitido limite negativo.");
            }
            this.limite = limite;
        }


        /// <summary>
        /// Método que seta o CPF ou CNPJ do cliente, podendo sem com ou sem pontos e traços, sistema lança exceção para a validação do documento.
        /// </summary>
        /// <param name="doc"> String contendo o numero com documento</param>
        public void setCPFCNPJ(String doc)
        {
            Uteis valida = new Uteis();
            if (doc.Length == 14 || doc.Length == 12)
            {
                if (!valida.validaCPF(doc))
                {
                    throw new System.ArgumentException("CPF invalido!");
                }


            }
            else if (doc.Length == 18 || doc.Length == 15)
            {

                if (!valida.validaCNPJ(doc))
                {

                    throw new System.ArgumentException("CNPJ invalido!");
                }

            }
            else
            {
                throw new System.ArgumentException("CPF ou CNPJ invalido!");
            }

            this.cpfcnpj = doc;
        }

        private void conectar(String SQL)
        {
            desconectar();
            dadosCliente.LockType = LockTypeEnum.adLockOptimistic;
            dadosCliente.CursorLocation = CursorLocationEnum.adUseClient;
            dadosCliente.CursorType = CursorTypeEnum.adOpenDynamic;
            dadosCliente.Open(SQL, conex);


            // }
        }

        private void desconectar()
        {
            if (dadosCliente.State != 0)
            {
                dadosCliente.Close();
            }
        }

        private void coletaDadosCredito()
        {
            String SQL1 = "SELECT clientes_credito.* FROM clientes_credito WHERE (((clientes_credito.cod_cliente)=" + this.cod + "));";
            Recordset dados = new Recordset();
            dados.Open(SQL1, new Conexao().getDb4());


            while (!(dados.EOF || dados.BOF))
            {
                informacoes.Add(new InformacaoCredito(Convert.ToInt32(dados.Fields["cod"].Value), Convert.ToString(dados.Fields["Credito"].Value)));
                dados.MoveNext();
            }

            dados.Close();
        }

        private void coletaDados()
        {
            String CodCliente = Convert.ToString(cod);
            String SQL = "SELECT Clientes.* FROM Clientes WHERE (((Clientes.Cod_cliente)=" + CodCliente + "))";
            String SQLFilicacao = "SELECT Clientes_filiacao.* FROM Clientes_filiacao WHERE (((Clientes_filiacao.Cod_cliente)=" + CodCliente + "));";

            conectar(SQL);
            if (dadosCliente.EOF || dadosCliente.BOF)
            {
                this.cod = 0;

            }
            else
            {

                cep = (dadosCliente.Fields["CEP"].Value.ToString() == null) ? "" : Convert.ToString(dadosCliente.Fields["CEP"].Value);
                cidade = (Convert.ToString((dadosCliente.Fields["cidade"].Value == null) ? "" : dadosCliente.Fields["cidade"].Value));
                cod = (Convert.ToInt32((dadosCliente.Fields["cod_cliente"].Value == null) ? "" : dadosCliente.Fields["cod_cliente"].Value));
                nome = (Convert.ToString((dadosCliente.Fields["nome"].Value == null) ? "" : dadosCliente.Fields["nome"].Value));
                telefone = (Convert.ToString((dadosCliente.Fields["tele1"].Value == null) ? "" : dadosCliente.Fields["tele1"].Value));
                end = (Convert.ToString((dadosCliente.Fields["end"].Value == null) ? "" : dadosCliente.Fields["end"].Value));
                operadora = (Convert.ToString((dadosCliente.Fields["Dado1"].Value == null) ? "" : dadosCliente.Fields["Dado1"].Value));
                cpfcnpj = (Convert.ToString((dadosCliente.Fields["CNPJ"].Value == null) ? "" : dadosCliente.Fields["CNPJ"].Value));
                
                email = (Convert.ToString((dadosCliente.Fields["e-mail"].Value == null) ? "" : dadosCliente.Fields["e-mail"].Value));
                UF = (Convert.ToString((dadosCliente.Fields["UF"].Value == null) ? "" : dadosCliente.Fields["UF"].Value));
                IE = (Convert.ToString((dadosCliente.Fields["insc"].Value == null) ? "" : dadosCliente.Fields["insc"].Value));
                referencia = (Convert.ToString((dadosCliente.Fields["referencia"].Value == null) ? "" : dadosCliente.Fields["referencia"].Value));
                mala = (Convert.ToBoolean((dadosCliente.Fields["mala"].Value == null) ? false : dadosCliente.Fields["Mala"].Value));
                //pessoaJuridica = (Convert.ToBoolean((dadosCliente.Fields["FJ"].Value == null) ? false : dadosCliente.Fields["FJ"].Value));
                if (cpfcnpj.Replace("/", "").Replace(".", "").Replace("-", "").Length != 14)
                {
                    pessoaJuridica = false;
                }
                else
                {
                    pessoaJuridica = true;
                }
                restrito = (Convert.ToBoolean((dadosCliente.Fields["restrito"].Value == null) ? false : dadosCliente.Fields["restrito"].Value));
                bairro = (Convert.ToString((dadosCliente.Fields["bairro"].Value == null) ? "" : dadosCliente.Fields["bairro"].Value));
                _fidelizado = (Convert.ToBoolean((dadosCliente.Fields["Fidelidade"].Value == null) ? false : dadosCliente.Fields["fidelidade"].Value));
                _dataFidelizacao = (Convert.ToDateTime((dadosCliente.Fields["dt_inicio"].Value.Equals(DBNull.Value) ) ? DateTime.Now.ToShortDateString() : dadosCliente.Fields["dt_inicio"].Value, culture));
                this.datanascimento = (Convert.ToDateTime((dadosCliente.Fields["nascimento"].Value.Equals(DBNull.Value)) ? DateTime.Now.ToShortDateString() : dadosCliente.Fields["nascimento"].Value, culture));
                
                if (dadosCliente.Fields["data"].Value.Equals(DBNull.Value))
                {
                    dataCadastro = DateTime.Now;
                }
                else
                {
                    dataCadastro = (Convert.ToDateTime(dadosCliente.Fields["data"].Value.Equals(DBNull.Value)? DateTime.Now.ToShortDateString(): dadosCliente.Fields["data"].Value ,culture));
                }



                desconectar();
                //Processo para a parte de clientes_filiação

                conectar(SQLFilicacao);

                if (!(dadosCliente.EOF || dadosCliente.BOF))
                {

                    classificacao = (Convert.ToString((dadosCliente.Fields["classifica"].Value == null) ? "" : dadosCliente.Fields["classifica"].Value));
                    pai = (Convert.ToString((dadosCliente.Fields["pai"].Value == null) ? "" : dadosCliente.Fields["pai"].Value));
                    mae = (Convert.ToString((dadosCliente.Fields["mae"].Value == null) ? "" : dadosCliente.Fields["mae"].Value));
                    endPais = (Convert.ToString((dadosCliente.Fields["endereço"].Value == null) ? "" : dadosCliente.Fields["endereço"].Value));
                    limite = (Convert.ToDouble((dadosCliente.Fields["limite"].Value == null) ? 0 : dadosCliente.Fields["limite"].Value)); ;
                }

                contatos = new ContatosCliente().getContatosClientes(getCod());


            }

        }
        
        public override String ToString()
        {

            return nome + "<br><b>Doc:</b> " + getCPFCNPJ() + "<br><b>Endereço:</b>" + getEnd() + "<br><b>Bairro:</b> " + getBairro() + "<br><b>Cidade:</b>" + getCidade() +
                " <b> UF:</b> " + getUF() + "<br><b> CEP:</b>" + getCEP() + " <b>Teleforne:</b> " + getTelefone() + " <b> Operadora:</b> " +
                getOperadora() + "<br><b>Ponto de referência: </b>" + getReferencia();
        }


        /// <summary>
        /// Retorna um lista de array de String contendo todos os dados de informações de credito.
        /// </summary>
        /// <returns>Lista de Array de String</returns>
        public List<String[]> getInformacoesdeCredito()
        {
            List<String[]> retorno = new List<string[]>();
            foreach (InformacaoCredito item in informacoes)
            {
                retorno.Add(item.ToArray());
            }

            return retorno;
        }
        /// <summary>
        /// Metodo adiciona uma informação de crédito do cliente, retornando um boolean sobre se a gravação foi bem sucedida.
        /// </summary>
        /// <param name="cod">Int16 bits </param>
        /// <param name="informacao">String</param>
        /// <returns>Boolean</returns>
        public Boolean addInformacoesdeCredito(Int16 cod, String informacao)
        {
            if (cod != 0)
            {
                Recordset dados = new Recordset();
                dados.LockType = LockTypeEnum.adLockOptimistic;
                dados.CursorType = CursorTypeEnum.adOpenDynamic;
                dados.CursorLocation = CursorLocationEnum.adUseServer;
                String SQL1 = "INSERT INTO clientes_credito ( cod_cliente, Credito ) " +
                              "SELECT " + cod + ", " + informacao + ";";
                dados.Open(SQL1, new Conexao().getDb4());
                return true;
            }
            return false;
        }
        /// <summary>
        /// Metodo atualiza uma innformação de crédito do cliente baseado no ID deste, retorna um booelan para confirmação da atualiazção.
        /// </summary>
        /// <param name="ID">Int16 contendo o numerador da informação</param>
        /// <param name="informacao">String</param>
        /// <returns>Booelan</returns>
        public Boolean updateInformacoesdeCredito(Int16 ID, String informacao)
        {
            if (cod != 0)
            {
                Recordset dados = new Recordset();
                dados.LockType = LockTypeEnum.adLockOptimistic;
                dados.CursorType = CursorTypeEnum.adOpenDynamic;
                dados.CursorLocation = CursorLocationEnum.adUseServer;
                String SQL1 = "UPDATE clientes_credito SET clientes_credito.Credito = '" + informacao + "' " +
                              "WHERE (((clientes_credito.cod)=" + ID + "));";
                dados.Open(SQL1, new Conexao().getDb4());
                return true;
            }
            return false;
        }
        /// <summary>
        /// Remove a informação de um cliente, retornando um booelan para confirmar a exclusão
        /// </summary>
        /// <param name="ID">Int16 com ID de numeração</param>
        /// <returns>Boolean</returns>
        public Boolean removeInformacoesCredito(Int16 ID)
        {
            if (cod != 0)
            {
                Recordset dados = new Recordset();
                dados.LockType = LockTypeEnum.adLockOptimistic;
                dados.CursorType = CursorTypeEnum.adOpenDynamic;
                dados.CursorLocation = CursorLocationEnum.adUseServer;
                String SQL1 = "DELETE clientes_credito.cod FROM clientes_credito WHERE (((clientes_credito.cod)=" + ID + "));";
                dados.Open(SQL1, new Conexao().getDb4());
                return true;
            }
            return false;
        }


        internal string getBairro()
        {
            return bairro;
        }
        public Boolean salvar()
        {
            Boolean resposta = false;

            if (this.cod == 0)
            {

                resposta = inserirDadosPricipais();
                // Processo de busca do cliente após sua inclusão para agora inseri os dados dos pais em outra tabela.
                // Este processo é nescessário por que o banco do dados  tem as informações dos pais gardados em separado.
                System.Threading.Thread.Sleep(1000);
                String[] dado = new Clientes().filtraCPFCNPJ(getCPFCNPJ())[0];
                this.cod = Convert.ToInt32(dado[0]);
                //Concatena os resultados para dar positivo ou negativo.
                resposta &= inserirDadosPais(this.cod);
            }
            else
            {
                //processo para o caso de cliente já cadastrado e precisa ser alterado.
                resposta = atualizaDadosPrincipais();
                resposta &= atualizaDadosPais(this.cod);
            }
            return resposta;
        }

        public Boolean excluir()
        {
            /**
             * Não deve ter este método implementado pois os cliente excluidos criam dados orfãos, neste caso cosideramos como clientes inativos os exluidos
             **/
            setRestrito(true);
            salvar();
            return true;
        }
        /// <summary>
        /// Retorna uma lista contendo todos os contatos do cliente
        /// </summary>
        /// <returns>Lista de objetos do tipo ContatosCliente</returns>
        public List<ContatosCliente> getContatos()
        {
            return contatos;
        }
        /// <summary>
        /// Retorna uma lista contendo todos as indicações do cliente
        /// </summary>
        /// <returns></returns>
        public List<IndicacoesCliente> getIndicacoes() { return indicacoes; }
    }




}