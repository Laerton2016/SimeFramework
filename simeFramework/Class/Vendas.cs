using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.DataVisualization.Charting;
using System.Data;
using ADODB;

namespace Sime
{
    public class VendasUsuario
    {
        System.Globalization.CultureInfo culture = new System.Globalization.CultureInfo("pt-BR");
        private int codUsuario;
        private Connection conexao;
        private Recordset dados = new Recordset();
        private String SQL;
        private Double especie = 0, cheque = 0, vale = 0, cartao = 0, meta = 0;
        private Dictionary<DateTime, List<String[]>> dadosVenda = new Dictionary<DateTime, List<string[]>>();
        private Dictionary<DateTime, Double> resumoData = new Dictionary<DateTime, double>();
        private List<String[]> MetaAtingida = new List<string[]>();
        private List<string> listaVendas = new List<string>();
        
       
        
        /// <summary>
        /// Metodo que cria um objeto do tipo vendas, onde retorna as vendas realizada por um determinado usuário em um determinado perioudo
        /// </summary>
        /// <param name="codUsuario">interiro com cod de um usuário válido</param>
        /// <param name="inicio">DateTime contendo a data de inicio do pedíodo</param>
        /// <param name="fim">DateTime contendo a data do fim do período</param>
        /// <param name="conexao">ADODB.Connection contendo o link com o banco de dados</param>
        public VendasUsuario (int codUsuario, DateTime inicio, DateTime fim, Connection conexao)
        {
            inicia(codUsuario, inicio, fim, conexao);
        }
        public VendasUsuario(int codUsuario, DateTime inicio, DateTime fim, Connection conexao, Double metaDia)
        {
            inicia(codUsuario, inicio, fim, conexao);
            this.meta = metaDia;
        }
        /// <summary>
        /// Metodo que inicia o porcesso de coleta de dados nos bancos de dados.
        /// </summary>
        /// <param name="codUsuario">interiro com cod de um usuário válido</param>
        /// <param name="inicio">DateTime contendo a data de inicio do pedíodo</param>
        /// <param name="fim">DateTime contendo a data do fim do período</param>
        /// <param name="conexao">ADODB.Connection contendo o link com o banco de dados</param>
        private void inicia(int codUsuario, DateTime inicio, DateTime fim, Connection conexao)
        {
            if (inicio > fim)
            {
                throw new ArgumentException("Data inicial maior que a data final.");
            }

            this.codUsuario = codUsuario;
            this.conexao = conexao;
            dados.LockType = LockTypeEnum.adLockBatchOptimistic;
            dados.CursorLocation = CursorLocationEnum.adUseServer;
            SQL = @"SELECT Cod_sai.Data, Cod_sai.Especie, Cod_sai.Cheque, Cod_sai.Vale, Cod_sai.cartao, Cod_sai.OP, Clientes.Nome, Cod_sai.Cod_sai " +
                  "FROM Cod_sai INNER JOIN Clientes ON Cod_sai.Cod_cliente = Clientes.Cod_cliente " +
                  "WHERE (((Cod_sai.Data) Between #" + (inicio.Month + "/" + inicio.Day + "/" + inicio.Year) + "# And #" + (fim.Month + "/" + fim.Day + "/" + fim.Year) + "#) AND ((Cod_sai.OP)=" + codUsuario + "));";
            coletaDados();
        }

        /// <summary>
        /// Método que faz a coleta das informações no banco de dados.
        /// </summary>
        private void coletaDados() {
            abreConexao();
            
            while (!(dados.EOF || dados.BOF )){
                double totalAnterior = especie + cheque + vale + cartao, atual = 0;
                String[] informacoes = new string[6];
                List<String[]> infoVenda = new List<string[]>();
                // Linha para somar o valor total pago.
                atual = Convert.ToDouble( dados.Fields["especie"].Value) + Convert.ToDouble( dados.Fields["cheque"].Value) + Convert.ToDouble(dados.Fields["cartao"].Value) +Convert.ToDouble( dados.Fields["vale"].Value);
                
                if (atual > 0)
                {
                    informacoes[1] = Convert.ToString (dados.Fields["especie"].Value) ;
                    especie += Convert.ToDouble(dados.Fields["especie"].Value);
                    informacoes[2] = Convert.ToString (dados.Fields["cheque"].Value);
                    cheque += Convert.ToDouble(dados.Fields["cheque"].Value);
                    informacoes[3] = Convert.ToString (dados.Fields["cartao"].Value);
                    cartao += Convert.ToDouble(dados.Fields["cartao"].Value);
                    informacoes[4] = Convert.ToString (dados.Fields["vale"].Value);
                    vale += Convert.ToDouble(dados.Fields["vale"].Value);
                    informacoes[0] = Convert.ToString (dados.Fields["nome"].Value);
                    informacoes[5] = informacoes[1] + informacoes[2] + informacoes[3] + informacoes[4];
                     

                    //Agrupondo as somas por datas usando as mesmas como chaves
                    if (dadosVenda.ContainsKey(Convert.ToDateTime(dados.Fields["data"].Value, culture)) == false)
                    {   
                        infoVenda.Add(informacoes);
                        dadosVenda.Add(Convert.ToDateTime(dados.Fields["data"].Value, culture), infoVenda);
                        resumoData.Add( (Convert.ToDateTime(dados.Fields["data"].Value, culture) ),atual );
                    }
                    else 
                    { 
                        infoVenda = dadosVenda[Convert.ToDateTime(dados.Fields["data"].Value, culture)];
                        infoVenda.Add(informacoes);
                        dadosVenda[Convert.ToDateTime(dados.Fields["data"].Value, culture)] = infoVenda;
                        resumoData[Convert.ToDateTime(dados.Fields["data"].Value, culture)] += atual;
                    }

                    //Incluido a venda na lista de vendas 
                    listaVendas.Add(dados.Fields["cod_sai"].Value.ToString());
                    
                }

                

                dados.MoveNext();
            }
            fechaConexao();
            
        }
        /// <summary>
        /// Método que abre a conexão com o banco de dados
        /// </summary>
        private void abreConexao() { 
            if (dados.State != 0 ) {
                fechaConexao();
            }
            dados.Open(SQL, conexao);
        }
        /// <summary>
        /// Método que retona uma lista de Arrays de String contendo os dados das datas cuja meta foi atendido
        /// </summary>
        /// <returns>Lista de Arrays String </returns>
        public List<String[]> getMetasAtingidas() {
            return MetaAtingida;
        }
        /// <summary>
        /// Método que fecha a conexão com o banco de dados.
        /// </summary>
        private void fechaConexao() {
            if (dados.State != 0) {
                dados.Close();
            }
        }

        /// <summary>
        /// Metodo que apresenta um resumo do resultado de vendas por grupo e um totalizador
        /// </summary>
        /// <returns>Retorna um String contendo o resumo</returns>
        public String resumeVendas() {
            String resumo = null;
            String teste = "<table style='width:100%;'>" +
                        "<tr>" +
                            "<td align='left' > Especie:" +
                            "</td>" +
                            "<td align='right' > R$ " + getTotalEspecie() +
                            "</td>" +
                        "</tr>" +
                        "<tr> " +
                            "<td align='left' bgcolor='#CCE6FF' > Cheques: " +
                                "</td> " +
                            "<td align='right' bgcolor='#CCE6FF'> R$ "+ getTotalCheque() +
                                "</td> " +
                        "</tr>" +
                         "<tr>" +
                            "<td align='left' > Cartão:" +
                            "</td>" +
                            "<td align='right' > R$ " + getTotalCartao() +
                            "</td>" +
                        "</tr>" +
                        "<tr> " +
                            "<td align='left' bgcolor='#CCE6FF' > Vale: " +
                                "</td> " +
                            "<td align='right' bgcolor='#CCE6FF'> R$ " + getTotalVale() +
                                "</td> " +
                        "</tr>" +
                        "<tr>" +
                            "<td align='left' > <B>Total:</B>" +
                            "</td>" +
                            "<td align='right' > <B> R$ " + getTotalGeral() +
                            "</B></td>" +
                        "</tr>" +
                    "</table>";
            resumo = "Especie: R$ " + getTotalEspecie() + "<br>Cheques: R$ "+ getTotalCheque() + "<br>Cartão: R$ "+ getTotalCartao() + "<br>Vale: R$ "+ getTotalVale() + "<br><B>Total: R$ " + getTotalGeral() + "</B>";
            return teste;
        }


        public void setMetaDia(double meta) {
            this.meta = meta;
        }
       
        public void geraGrafico(Chart Grafico) {
            
            int contar = 0;
            double soma = 0;
            Grafico.Series.Add("Vendas por pereioudo");
            
            foreach (var chave in resumoData.Keys)
            {
                String data =Convert.ToString(chave, culture);
                data = data.Replace("00:00:00", "");
              
                 
                Grafico.Series[0].Points.Add().SetValueXY(data , resumoData[chave]);
                
                
                if (resumoData[chave] > this.meta) {
                    Grafico.Series[0].Points[contar].Color = System.Drawing.Color.Red;
                    MetaAtingida.Add(new String[] { chave.ToShortDateString(), String.Format("R$ {0:#,##0.00}", resumoData[chave]) });
                    soma += resumoData[chave];
                }
                contar++;
            }
            MetaAtingida.Add(new String[] { "<B>Total:</B>", "<B>" + String.Format("R$ {0:#,##0.00}", soma) + "</B>" });
        }

        /// <summary>
        /// Método que apresente o total geral em espécie
        /// </summary>
        /// <returns>String contendo o valor total em espécie</returns>
        public String getTotalEspecie() {
            return String.Format("{0:#,##0.00}", especie);
        }
        /// <summary>
        /// Método que apresenta o total geral em cheque
        /// </summary>
        /// <returns>String contendo o total geral em cheque</returns>
        public String getTotalCheque()
        {
            return String.Format("{0:#,##0.00}", cheque);
        }
        /// <summary>
        /// Método que apresenta o total geral em cartão
        /// </summary>
        /// <returns>String com total geral em cartão</returns>
        public String getTotalCartao()
        {
            return String.Format("{0:#,##0.00}", cartao);
        }
        /// <summary>
        /// Método que apresenta o total geral em vale
        /// </summary>
        /// <returns>String que apresenta o total geral em vale</returns>
        public String getTotalVale()
        {
            return String.Format("{0:#,##0.00}", vale);
        }
        /// <summary>
        /// Método que apresenta o total geral em vendas 
        /// </summary>
        /// <returns>String com a soma total das vendas </returns>
        public String getTotalGeral()
        {
            return String.Format("{0:#,##0.00}", (especie + cheque + vale + cartao));
        }
    }
}