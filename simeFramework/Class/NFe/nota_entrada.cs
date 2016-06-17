using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class.NFe
{
    public class nota_entrada
    {
        private String chaveNFE = "", NF_numero = "", local = "";
        private Int32[] itens = null;
        private Int32 id = 0, id_fornecedor = 0;
        private DateTime data_emissao = DateTime.Now, data_entrada = DateTime.Now;
        private Double icms_antecipado = 0;
        

        /// <summary>
        /// Classe cria objeto do tipo nota de entrada nova já com n. de itens
        /// </summary>
        /// <param name="numeroItens"></param>
        public nota_entrada(Int32 numeroItens) 
        {
            itens = new Int32[numeroItens];
        }
        
        /// <summary>
        /// Classe que cria objeto nota de entrada baseado na consulta da chave da nota de entrada.
        /// </summary>
        /// <param name="chaveNFE">String contendo a chave de entrada de uma nota fiscal</param>
        public nota_entrada(String chaveNFE) 
        {
            
            
            string chave1 = new SIME.Class.Uteis().esquerda(chaveNFE, 42);
            Int32 dvChave = Convert.ToInt32(new SIME.Class.Uteis().direita(chaveNFE, 0));
            if (GerarDigitoVerificadorNFe(chave1) != dvChave) { throw new ArgumentException("Chave inválida."); }
            String SQL = "SELECT notas_entradas.* FROM notas_entradas WHERE (((notas_entradas.Chave_NFE)='" + chaveNFE + "'));";
            try
            {
                coletaDados(SQL);
            }
            catch (Exception erro)
            {
                
                throw erro;
            }
            
            this.chaveNFE = chaveNFE;
        }
        /// <summary>
        /// Classe que cria objeto nota de entrada baseado no N° da NF e ID do fornecedor para consulta na base de dados
        /// </summary>
        /// <param name="NF_numero">String com N° da NF de entrada</param>
        /// <param name="id_fornecedor">Inteiro contendo  ID do fornecedor, caso não esteja cadastrador o objeto necessitara de tratamento.</param>
        public nota_entrada(String NF_numero, Int32 id_fornecedor) 
        {
            if (NF_numero.Equals("")) { throw new ArgumentException("Não é possível localizar o NF comprimento zero."); }
            if (id_fornecedor <=0) { throw new ArgumentException("Id de fornecedor não pode ser menor que 1.");}

            String SQL = "SELECT notas_entradas.* FROM notas_entradas WHERE (((notas_entradas.ID_Forncedor)="+id_fornecedor+") AND ((notas_entradas.NF_Numero)='"+NF_numero+"'));";
            coletaDados(SQL);
            this.id_fornecedor = id_fornecedor;
            this.NF_numero = NF_numero;
        }

        public string getChave() { return chaveNFE; }
        public string getNF_numero() { return NF_numero; }
        public string getLocal() { return local; }
        public double getICMSAntecipado() { return icms_antecipado; }

        /// <summary>
        /// Método retorna um array de inteiros contendo os itens da nota e ID dos produtos relacionados.
        /// </summary>
        /// <returns>Array de inteiros composto no campo 0 o item da NF em questão e campo 1 o ID do produto no banco de dados Produtos</returns>
        public Int32[] getitens() { return itens; }
        public Int32 getID() { return id; }
        public Fornecedor getFornecedor() { return (id==0)? null : new Fornecedor(id_fornecedor); }
        public DateTime getDataEmissao() { return data_emissao; }
        public DateTime getDataEntrada() { return data_entrada; }
        /// <summary>
        /// Método que seta a chave da NF
        /// </summary>
        /// <param name="chave">String contendo a chave do produto</param>
        public void setChave(String chave) 
        {
            if (chave.Equals("")) { throw new ArgumentException("Não é permitido chave vazia"); }
            if (chave.Length != 44) { throw new ArgumentException("A chave da NF-e deve conter 44 dígitos."); }
            string chave1 = new SIME.Class.Uteis().esquerda(chave, 42);
            Int32 dvChave = Convert.ToInt32(new SIME.Class.Uteis().direita(chave, 0));
            if (GerarDigitoVerificadorNFe(chave1) != dvChave) { throw new ArgumentException("Chave inválida."); }

            this.chaveNFE = chave;
            coletaInfChave(chave);
        }
        /// <summary>
        /// Método para setar o valor do ICMS antecipado nesta nota. Essa informação não consta no XML e deve ser alimentado pelo usuário
        /// o mesmo tem como padão valor zero.
        /// </summary>
        /// <param name="ICMSAntecipado">Recebe o valor do ICMS antecipado pago em reais, não sendo aceito valores negativos.</param>
        public void setICMSAntecipado(Double ICMSAntecipado)
        {
            if (ICMSAntecipado < 0) { throw new ArgumentException("Icms não pode conter valor negativo."); }
            this.icms_antecipado = ICMSAntecipado;
        }

        private void coletaDados()
        {
            String SQL = "SELECT notas_entradas.* FROM notas_entradas WHERE (((notas_entradas.ID_NF)=" + this.id + "));";
            coletaDados(SQL);
        }


        private void coletaDados(String SQL) 
        {
            Recordset dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (!(dados.EOF || dados.BOF))
            {
                this.chaveNFE = dados.Fields["Chave_NFE"].Value.ToString();
                this.id = Convert.ToInt32(dados.Fields["ID_NF"].Value.ToString());
                this.id_fornecedor = Convert.ToInt32(dados.Fields["ID_Forncedor"].Value.ToString());
                this.NF_numero = dados.Fields["NF_numero"].Value.ToString();
                this.data_emissao = Convert.ToDateTime(dados.Fields["data"].Value.ToString());
                this.data_entrada = Convert.ToDateTime(dados.Fields["Data_inclusão"].Value.ToString());
                this.icms_antecipado = Convert.ToDouble(dados.Fields["Imposto_pago"].Value.ToString());
                //processo que reconhe os itens guaradado em string no banco de dados
                String[] listaItens = dados.Fields["itens"].Value.ToString().Split(',');
                this.itens = new Int32[listaItens.Length];
                for (int i = 0; i < listaItens.Length; i++)
                {
                    itens[i] = Convert.ToInt32(listaItens[i]);
                }

                this.local = dados.Fields["local"].Value.ToString();


            }
            else
            {
                throw new ArgumentException("NF não cadastrada.");
            }
            dados.Close();
            conex.Close();
        
        }
        /// <summary>
        /// Metodo reira da chave da NF as informações como data da NF , N° da NF, CNPJ do forneedor evitando o recebimento de dados desnecessários.
        /// </summary>
        /// <param name="chave">String de 44 dígitos </param>
        private void coletaInfChave(string chave)
        {
            Uteis util = new Uteis();
            //string dataemissao = util.esquerda(chave, 5);
            //this.data_emissao = Convert.ToDateTime(util.direita(dataemissao, 3));
            String cnpj = util.esquerda(chave, 19);
            cnpj = util.direita(cnpj, 13);
            cnpj = util.aplicaMascara(cnpj, util.criaMascara(cnpj));
            try
            {
                this.id_fornecedor = new Fornecedor(cnpj).getID();
            }
            catch (Exception)
            {
                this.id_fornecedor = 0;
            }
            
            string NNF = util.esquerda(chave, 33);
            NNF = util.direita(NNF, 8);
            this.NF_numero = NNF;
        }
        /// <summary>
        /// Método seta o item na lista de intes. 
        /// </summary>
        /// <param name="item">Posição do item na nota fiscal de entrada.</param>
        /// <param name="valor">Id do Banco de dados de Produtos.</param>
        public void setItem(Int32 item, Int32 valor)
        {
            if (item > itens.Length + 1) { throw new ArgumentException("Item fora do limite criado para a NF."); }
            //if (item <= 0) { throw new ArgumentException("Item não pode conter valores menores que 1."); }
            itens[item] = valor;
        }
        /// <summary>
        /// Método que seta a data de emissão
        /// </summary>
        /// <param name="data_emissao">Objeto do tipo datatime com a data de emissão</param>
        public void setDataEmissao(DateTime data_emissao) { this.data_emissao = data_emissao; }

        /// <summary>
        /// Metodo retorna o ID do produto baseado na posição da NF repassada
        /// </summary>
        /// <param name="item">Inteiro que contem a posição do produto.</param>
        /// <returns>Inteiro com ID do produto da base de dados</returns>
        public Int32 getItem(Int32 item)
        {
            if (item > itens.Length + 1) { throw new ArgumentException("Item fora do limite criado para a NF."); }
            //if (item <= 0) { throw new ArgumentException("Item não pode conter valores menores que 1."); }
            return itens[item];
        }

        public void setLocal(String local) { this.local = local; }

        /// <summary>
        /// Metodo retorna o dígito verificador na NF-E
        /// </summary>
        /// <param name="chave">Chave de entrada</param>
        /// <returns>DV de resultado</returns>
        private int GerarDigitoVerificadorNFe(string chave)
        {
            int soma = 0; // Vai guardar a Soma
            int mod = -1; // Vai guardar o Resto da divisão
            int dv = -1;  // Vai guardar o DigitoVerificador
            int pesso = 2; // vai guardar o pesso de multiplicacao

            //percorrendo cada caracter da chave da direita para esquerda para fazer os calculos com o pesso
            for (int i = chave.Length - 1; i != -1; i--)
            {
                int ch = Convert.ToInt32(chave[i].ToString());
                soma += ch * pesso;
                //sempre que for 9 voltamos o pesso a 2
                if (pesso < 9)
                    pesso += 1;
                else
                    pesso = 2;
            }

            //Agora que tenho a soma vamos pegar o resto da divisão por 11
            mod = soma % 11;
            //Aqui temos uma regrinha, se o resto da divisão for 0 ou 1 então o dv vai ser 0
            if (mod == 0 || mod == 1)
                dv = 0;
            else
                dv = 11 - mod;

            return dv;
        }

        public Boolean salvar() 
        {
            Recordset dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String lista = "";
            for (int i = 0; i < this.itens.Count(); i++)
            {
                if (i != (this.itens.Count() - 1))
                {
                    lista += itens[i] + ",";
                }
                else 
                {
                    lista += itens[i];
                }
            }
            String SQL = "";

            if (this.id == 0)
            {
                SQL = "INSERT INTO notas_entradas ( Chave_NFE,  ID_Forncedor, NF_Numero, Data, Data_Inclusão, Imposto_pago, Itens, [Local] ) " +
                             "SELECT '" + this.chaveNFE + "'," +
                             this.id_fornecedor.ToString() + ", " +
                             "'" + this.NF_numero + "', " +
                             "#" + this.data_emissao.Day + "/" + this.data_emissao.Month + "/" + this.data_emissao.Year + "#," +
                             "#" + this.data_entrada.Day + "/" + this.data_entrada.Month + "/" + this.data_entrada.Year + "#," +
                             this.icms_antecipado.ToString().Replace(",", ".") + ", " +
                             "'" + lista + "', " +
                             "'" + this.local + "'; ";
            }
            else
            {
                SQL = "UPDATE notas_entradas SET " + 
                      "notas_entradas.Chave_NFE = '"+ this.chaveNFE +"', " + 
                      "notas_entradas.ID_Forncedor = " + this.id_fornecedor +", " + 
                      "notas_entradas.NF_Numero = '"+ this.NF_numero +"', " + 
                      "notas_entradas.Data = #"+this.data_emissao.Day + "/" + this.data_emissao.Month + "/" + this.data_emissao.Year+"#, " + 
                      "notas_entradas.Imposto_pago = "+ this.icms_antecipado.ToString().Replace(",", ".") + ", " + 
                      "notas_entradas.Itens = '"+ lista +"', " + 
                      "notas_entradas.[Local] = '"+ this.local +"' " +
                      "WHERE (((notas_entradas.ID_NF)="+this.id+"));";
            }
            try
            {
                dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }
            catch (Exception erro)
            {
                 throw erro;
            }
            return true; 
        }
        public Boolean excluir() 
        {
            Recordset dados = new Recordset();
            Connection conex = new Conexao().getDb4();
            String SQL = "DELETE notas_entradas.ID_NF FROM notas_entradas WHERE (((notas_entradas.ID_NF)="+this.id+"));";

            try
            {
                dados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }
            catch (Exception erro)
            {
                
                throw erro;
            }
            
            return true; 
        }
    }
}