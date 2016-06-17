using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI.WebControls;
using ADODB;

namespace SIME.Class
{
    /// <summary>
    /// Métodos para uso diverços
    /// </summary>
    public class Uteis
    {
        public ListItem[] listaCidades(String UF, System.Web.UI.WebControls.DropDownList combo)
        {

            Recordset dados = new Recordset();
            String Sql = "SELECT MUNICIPIOS.NOME, MUNICIPIOS.UF " +
                         "FROM MUNICIPIOS WHERE (((MUNICIPIOS.UF)='" + UF + "')) ORDER BY MUNICIPIOS.NOME;";


            dados.LockType = LockTypeEnum.adLockBatchOptimistic;
            dados.CursorLocation = CursorLocationEnum.adUseClient;
            dados.CursorType = CursorTypeEnum.adOpenDynamic;
            dados.Open(Sql, new Conexao().getSmall());
            ListItem[] teste = new ListItem[dados.RecordCount];
            combo.Items.Clear();
            while (!(dados.EOF || dados.BOF))
            {
                combo.Items.Add(Convert.ToString(dados.Fields["nome"].Value).ToUpper());
                dados.MoveNext();
            }

            return teste;
        }

        /// <summary>
        /// Metodo verifica se na String repassada como argumento contém algum caracter simbolico.
        /// </summary>
        /// <param name="dado">String para a analise.</param>
        /// <returns>Booleano de retorno.</returns>
        public Boolean ContemPontuacao(string dado)
        {
            if (dado.Where(c => char.IsPunctuation(c)).Count() > 0)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método verifica se a string recebida contem algum número.
        /// </summary>
        /// <param name="dado">String para analise</param>
        /// <returns>Bolean de retorno</returns>
        public Boolean ContemNumeros(String dado)
        {
            if (dado.Where(c => char.IsNumber(c)).Count() > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// Método verifica se a string recebida como atributo de analise contém somente carcateres alfabeticos.
        /// </summary>
        /// <param name="dados">String para analiese</param>
        /// <returns>Boolean de retorno</returns>
        public Boolean Soletras(string dados)
        {
            if ((dados.Where(c => char.IsLetter(c)).Count() > 0) && (dados.Where(c => char.IsNumber(c)).Count() == 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Método verifica se os dados contem somente números
        /// </summary>
        /// <param name="dados">String contendo dados para analise</param>
        /// <returns>Retorna um Boolean</returns>
        public bool Sonumeros(string dados)
        {
            if ((dados.Where(c => char.IsLetter(c)).Count() == 0) && (dados.Where(c => char.IsNumber(c)).Count() > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Método analisa uma string com numeros e uma letra no final
        /// </summary>
        /// <param name="dado">String a ser analizada</param>
        /// <returns>Boolean com resultado</returns>
        public bool VerificaNumero(String dado)
        {
            String carcater = "";
            for (int i = 0; i < dado.Length; i++)
            {
                carcater = dado[i].ToString();
                if (!(Sonumeros(carcater)))
                {
                    if ((i + 1) != dado.Length)
                    {
                        return false;
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Método para validar o CPF
        /// </summary>
        /// <param name="doc">String contendo o CPF</param>
        /// <returns>Retorna boolean</returns>
        public Boolean validaCPF(String doc)
        {
            String dado = doc.Trim();
            dado = dado.Replace(".", "");
            dado = dado.Replace("-", "");
            switch (dado)
            {
                case "00000000000":
                    return false;
                case "11111111111":
                    return false;
                case "22222222222":
                    return false;
                case "33333333333":
                    return false;
                case "44444444444":
                    return false;
                case "55555555555":
                    return false;
                case "66666666666":
                    return false;
                case "77777777777":
                    return false;
                case "88888888888":
                    return false;
                case "99999999999":
                    return false;

            }
            return modulo11CPF(dado);
        }

        /// <summary>
        /// Método que valida o CPNJ
        /// </summary>
        /// <param name="doc">String contendo o cnpj</param>
        /// <returns>Retorna um boolean</returns>
        public Boolean validaCNPJ(String doc)
        {
            String dado = doc.Trim();
            dado = dado.Replace(".", "");
            dado = dado.Replace("-", "");
            dado = dado.Replace("/", "");
            return modulo11CNPJ(dado);

        }

        private Boolean modulo11CNPJ(String dado)
        {
            if (dado.Length < 14)
            {
                return false;
            }
            int soma = 0, digito = 0, contar = 0, multiplica = 6;
            String DV;
            String digitos = dado;

            foreach (var item in dado)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (multiplica * digito);
                contar++;

                multiplica = (multiplica == 9) ? 2 : multiplica + 1;

                if (contar > dado.Length - 3) { break; }
            }

            DV = ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);

            soma = 0;
            contar = 0;
            multiplica = 5;

            foreach (var item in dado)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (multiplica * digito);
                contar++;
                multiplica = (multiplica == 9) ? 2 : multiplica + 1;
                if (contar > dado.Length - 2) { break; }
            }

            DV += ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);
            String DVOriginal = Convert.ToString(digitos[(digitos.Length - 2)]) + Convert.ToString(digitos[(digitos.Length - 1)]);

            return DV.Equals(DVOriginal);
        }
        private Boolean modulo11CPF(String dado)
        {
            int soma = 0, digito = 0, contar = 1;
            String DV;
            String digitos = dado.Replace("/", "");

            foreach (var item in digitos)
            {

                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (contar * digito);
                contar++;
                if (contar > dado.Length - 2) { break; }
            }

            DV = ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);

            soma = 0;
            contar = 0;

            foreach (var item in digitos)
            {
                digito = Convert.ToInt16(Convert.ToString(item));
                soma += (contar * digito);
                contar++;
                if (contar > dado.Length - 2) { break; }
            }

            DV += ((soma % 11) >= 10) ? "0" : Convert.ToString(soma % 11);
            String DVOriginal = Convert.ToString(digitos[(digitos.Length - 2)]) + Convert.ToString(digitos[(digitos.Length - 1)]);

            return DV.Equals(DVOriginal);
        }

        /// <summary>
        /// Método verifica se os dados contem somente números
        /// </summary>
        /// <param name="dados">String contendo dados para analise</param>
        /// <returns>Retorna um Boolean</returns>
        public bool Sonumeros1(string dados)
        {
            if ((dados.Where(c => char.IsLetter(c)).Count() == 0) && (dados.Where(c => char.IsNumber(c)).Count() > 0))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Método verifica se é um e-mail
        /// </summary>
        /// <param name="email">String contendo o e-mail</param>
        /// <returns>Retorna um Boolean</returns>
        public bool ValidaEmail(string email)
        {
            return email.Contains('@');

        }

        /// <summary>
        /// Metodo Recebe uma string com o tamnho dos caracteres limite que precisa a direita.
        /// </summary>
        /// <param name="campo">String que deve ser analizada</param>
        /// <param name="posicao">Posição final da String</param>
        /// <returns>String novo formato</returns>

        public String direita(String campo, Int16 posicao)
        {
            if (posicao < 0)
            {
                return null;
            }
            String retorno = null;
            
            Int16 indice = Convert.ToInt16(campo.Length);
            indice--;

            Int16 loca = indice;
            
            loca -= (posicao);
            
            //loca -= (posicao);
            //loca += 1;

            //String[] grupo = campo.Split();
            /**
            for (int i = grupo.Length - 1; i >= posicao; i--)
            {
                retorno += grupo[i];
            }
             **/
            for (int i = loca; i <= indice; i++)
            {
                retorno += campo[i];
            }
            return retorno;
        }
        /// <summary>
        /// Metodo Recebe uma string com o tamnho dos caracteres limite que precisa a esquerda.
        /// </summary>
        /// <param name="campo">String que deve ser analizada</param>
        /// <param name="posicao">Posição final da String</param>
        /// <returns>String novo formato</returns>


        public String esquerda(String campo, Int16 posicao)
        {

            String retorno = null;
            String[] grupo = campo.Split();
            /**
            if (posicao >= grupo.Length)
            {
                return null;
            }
            **/
            for (int i = 0; i <= posicao; i++)
            {
                retorno += campo[i];
            }
            return retorno;
        }
        /// <summary>
        /// Método para verificação de inscrição instadual 
        /// </summary>
        /// <param name="IE"></param>
        /// <returns></returns>
        private Boolean IEPB(String IE)
        {
            if (!(IE.Contains("-")))
            {

                return false;
            }
            Int32 Soma = 0, peso = 9, digito = 0, dv1 = 0;
            String IEParcial = IE.Replace(".", "").Trim();
            String DV = IEParcial.Split('-')[1];
            String Dverificador = "";
            IEParcial = IEParcial.Split('-')[0];
            foreach (var item in IEParcial)
            {

                digito = Convert.ToInt16(Convert.ToString(item));
                Soma += (digito * peso);
                peso--;
            }
            dv1 = 11 - (Soma % 11);
            Dverificador = (dv1 >= 10) ? "0" : Convert.ToString(dv1);
            return DV.Equals(Dverificador);
        }
        /// <summary>
        /// Método valida uma Inscrição estadual
        /// </summary>
        /// <param name="IE">String contendo a IE</param>
        /// <returns>Retorna boolean</returns>

        public Boolean ValidaIE(String IE, String UF)
        {
            switch (UF)
            {
                case ("PB"):
                    return IEPB(IE);
                case ("AC"):
                    return IEPB(IE);
                default:
                    break;
            }


            return true;
        }

        public String aplicaMascara(String dado, String mascara)
        {
            String retorno = "";
            int j = 0;
            for (int i = 0; i < mascara.Length; i++)
            {
                if (mascara[i].Equals('9'))
                {
                    retorno += dado[j];
                    j++;
                }
                else if (mascara[i].Equals('.') || mascara[i].Equals('-') || mascara[i].Equals('/') || mascara[i].Equals(","))
                {
                    retorno += mascara[i];
                }
                else 
                {
                    throw new ArgumentException("Mascara de entrada inválida.");
                }

            }
            return retorno;
        }


        /// <summary>
        /// Método que verifica os dados e cria uma mascara especifica para ele
        /// </summary>
        /// <param name="dado"></param>
        /// <returns>Uma mascara de acordo com o dado, como padrão segue a de telefone</returns>
        public string criaMascara(string dado)
        {
            return criaMascara(dado, "PB");
        }


        public string criaMascara(string dado, String UF) {
            string mascara = "";
            string informa = dado.Replace(" ", "");
            informa = informa.Replace(".", "");
            informa = informa.Replace("-", "");
            informa = informa.Replace("/", "");
            informa = informa.Replace("(", "" );
            informa = informa.Replace(")", "" );

            if (validaCPF(informa)) {
                mascara = "999.999.999-99";
            }
            else if (validaCNPJ(informa))
            {
                mascara = "99.999.999/9999-99";
            }
            else{

                mascara = "(99)9999-9999";
                
            }
            return mascara;
        }

    }
    class DadosCEP
    {
        private String endereco, cidade, uf, cep, bairro, tipoLogradouro;

        public DadosCEP(String CEP)
        {
            coletaEndereco(CEP);
        }
        /// <summary>
        /// Metodo efetua pesquisa na internet de dados de um determina CEP repassado no argumento de entrada
        /// Este metodo tem os seguintes limite:
        ///     - Precisa-se de Internet.
        ///     - Só possível duas buscas por minutos.
        /// </summary>
        /// <param name="CEP">String contendo o CEP a ser pesquisado</param>
        public void coletaEndereco(String CEP)
        {
            cep = null;
            DataSet ds = new DataSet();
            String resultado = null;
            ds.ReadXml("http://cep.republicavirtual.com.br/web_cep.php?cep=" + CEP.Replace("-", "").Trim() + "&formato=xml");
            if (ds != null)
            {
                if (ds.Tables[0].Rows.Count > 0)
                {
                    resultado = ds.Tables[0].Rows[0]["resultado"].ToString();
                    switch (resultado)
                    {
                        case "1":
                            uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            bairro = ds.Tables[0].Rows[0]["bairro"].ToString().Trim();
                            tipoLogradouro = ds.Tables[0].Rows[0]["tipo_logradouro"].ToString().Trim();
                            endereco = ds.Tables[0].Rows[0]["logradouro"].ToString().Trim();
                            cep = CEP;
                            break;
                        case "2":
                            uf = ds.Tables[0].Rows[0]["uf"].ToString().Trim();
                            cidade = ds.Tables[0].Rows[0]["cidade"].ToString().Trim();
                            bairro = "";
                            tipoLogradouro = "";
                            endereco = "";
                            cep = CEP;
                            break;
                        default:

                            uf = "";
                            cidade = "";
                            bairro = "";
                            tipoLogradouro = "";
                            endereco = "";

                            cep = null;
                            break;
                    }
                }
            }



        }
        /// <summary>
        /// Metodo retorna o CEP da pesquisa, retorna como padrão null
        /// </summary>
        /// <returns>retrona String</returns>
        public String getCep()
        {
            return cep;
        }
        /// <summary>
        /// Metodo retorna uma string contendo o endereço do CEP pesquisado, valor padrão Null.
        /// </summary>
        /// <returns>Retorna String</returns>
        public String getEndereco()
        {
            return endereco;
        }
        /// <summary>
        /// Metodo retorna a sigla do estado da pesquisa do CEP, valor padrão é null.
        /// </summary>
        /// <returns>Retorna uma string</returns>
        public String getUf()
        {
            return uf;
        }
        /// <summary>
        /// Metodo retorna a cidade da pesquisa do CEP, valor padrão é null.
        /// </summary>
        /// <returns>Retorna string</returns>
        public String getCidade()
        {
            return cidade;
        }
        /// <summary>
        /// Metodo retorna o bairro referente do CEP, Valor padrão é null.
        /// </summary>
        /// <returns>Retorna uma String</returns>
        public String getBairro()
        {
            return bairro;
        }
        /// <summary>
        /// Metodo retorna o tipo de logradouro do CEP, valor padrão é null.
        /// </summary>
        /// <returns>Retorna uma String</returns>
        public String getTipoLogradouro()
        {
            return tipoLogradouro;
        }

    }
}