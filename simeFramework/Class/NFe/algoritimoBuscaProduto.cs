using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ADODB;
using SIME;
using SIME.Class;

namespace TestaSolucao.nfE
{
    class algoritimoBuscaProduto
    {
        private List<String[]> listaProdutos;
        /// <summary>
        /// Classe que contém métodos para buscar produtos por semelhanças e por EAN
        /// </summary>
        public algoritimoBuscaProduto()
        {
            criaArray();
        }
        // Devemos colocar em mente que devemos retornar sempre o ID dos produtos e não o objeto produto visto que isso pode criar uma sobrecarga de muitos produtos
        // O tratamento dos id's deve ser feito na aplicação

        /// <summary>
        /// Método que deve retornar o ID de um produto baseado em uma busca por EAN
        /// </summary>
        /// <param name="EAN">Código de barras do produto.</param>
        /// <returns>Inteiro contendo ID do produto, caso não localizado retorna 0(zero)</returns>
        public Int32 BuscaProdutopporEAN(String EAN)
        {
            String SQL = "SELECT PRODUTOS.Cod FROM PRODUTOS WHERE (((PRODUTOS.Codbarras)='" + EAN + "')); ";
            Recordset rsDados = new Recordset();
            Connection conex = new SIME.Conexao().getDb4();
            Int32 id = 0;
            rsDados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);

            if (!(rsDados.BOF || rsDados.EOF))
            {
                id = Convert.ToInt32(rsDados.Fields["cod"].Value);
            }
            rsDados.Close();
            conex.Close();
            return id;
        }
        /// <summary>
        /// Método retorna um arry com todos os produtos da tabela produtos.
        /// </summary>
        private void criaArray()
        {
            Recordset rsdados = new Recordset();
            Connection conex = new Conexao().getDb4();
            List<String[]> produtos = new List<String[]>();
            String SQL = "Select produtos.cod, produtos.descrição, produtos.desc, produtos.estoque From produtos;";

            rsdados.Open(SQL, conex, CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            while (!(rsdados.BOF || rsdados.EOF))
            {
                String[] item = new String[4];
                item[0] = rsdados.Fields["cod"].Value.ToString();
                item[1] = rsdados.Fields["descrição"].Value.ToString();
                item[2] = rsdados.Fields["desc"].Value.ToString();
                item[3] = rsdados.Fields["estoque"].Value.ToString();
                produtos.Add(item);
                rsdados.MoveNext();

            }
            rsdados.Close();
            conex.Close();
            this.listaProdutos = produtos;
        }
        /// <summary>
        /// Método que retorna uma lista de arrays de produtos onde a sequência é Cod, Descricão, Descontinuado e Estoque.
        /// </summary>
        /// <returns></returns>
        public List<String[]> getProdutos() { return this.listaProdutos; }

        private Double notifica(String texto1, String texto2)
        {
            Double  nota = 0;
            String P1 = texto1 , P2 = texto2;

            
            if ((texto1.Length == 3 || texto2.Length == 3) && !(texto1.ToUpper().Equals(texto2.ToUpper())))
            {
                if(texto1.Length > 3)
                {
                    P1 = tresLetras(P1);
                }
                if (texto2.Length > 3)
                {
                    P2 = tresLetras(P2);
                }

                if (P1.ToUpper().Equals(P2.ToUpper())) { return 0.5; }
            }
            

            if (P1.ToUpper().Equals(P2.ToUpper()))
            {
                nota = 1;
               // if (P1.Where(c => char.IsNumber(c)).Count() > 0) { nota += 0.2; }
            }

            return nota;
        }

        /// <summary>
        /// Método que retorna uma nova string somente com as três primeiras letras da palavra de origem.
        /// Usado para o método de comparação de abreviaturas.
        /// </summary>
        /// <param name="palavra">Palavra a ser reduzida</param>
        /// <returns>String com as três primeiras letras da string de origem.</returns>
        private String tresLetras(String palavra) { return new Uteis().esquerda(palavra, 2); }

        /// <summary>
        /// Método que compara duas frases e faz a pontuação em sua semelhança de palavras
        /// Não há impostância com a sequência de origem.
        /// </summary>
        /// <param name="frase1">String com a primeira frase</param>
        /// <param name="frase2">String com a segunda frase</param>
        /// <returns>Double com o percentual de semelhança</returns>
        private Double pontua(String frase1, String frase2)
        {
            Double nota = 0;
            String[] lista1 = frase1.Split(' ');
            String[] lista2 = frase2.Split(' ');
            Int32 conta = 0;
            String[] exclusao = { "TEM", "COM", "PARA", "COMO", "TÊM", "SEM", "TER", "SER", "NÃO", "NEM", "VEM", "DEM" };
            double valor = 0;

            List<String> ListaExluida1 = new List<string>(), ListaExcluida2 = new List<string>();

            for (int i = 0; i < lista1.Length; i++)
            {
                if (((lista1[i].Length >= 3) && (!(exclusao.Contains(lista1[i].ToUpper())))) || (contemNumeros(lista1[i]))) // verifica se a palavra não é descartavel
                {
                    lista1[i] = lista1[i].Replace(".", "");
                    ListaExluida1.Add(lista1[i]);
                }
            }

            for (int i = 0; i < lista2.Length; i++)
            {
                if (((lista2[i].Length >= 3) && (!(exclusao.Contains(lista2[i].ToUpper())))) || (contemNumeros(lista2[i]))) // verifica se a palavra não é descartavel
                {
                    lista2[i] = lista2[i].Replace(".", "");
                    ListaExcluida2.Add(lista2[i]);
                }
            }

            
            

            if (ListaExluida1.Count < ListaExcluida2.Count)
            {
                for (int i = 0; i < ListaExluida1.Count; i++) // varre os itens da lista1
                {
                    for (int j = 0; j < ListaExcluida2.Count; j++) // varre na lista2
                    {
                        valor = notifica(ListaExluida1[i], ListaExcluida2[j]);
                        if (valor == 1 || valor == 0.5)
                        {
                            nota +=valor;
                            break;
                        }


                    }

                }
                conta = ListaExcluida2.Count;
            }
            else
            {
                for (int i = 0; i < ListaExcluida2.Count; i++) // varre os itens da lista1
                {

                    for (int j = 0; j < ListaExluida1.Count; j++) // varre na lista2
                    {


                        valor = notifica(ListaExcluida2[i], ListaExluida1[j]);

                            if (valor == 1 || valor == 0.5)
                            {
                                nota += valor;
                                break;
                            }




                    }
                }
                conta = ListaExluida1.Count;
            }
            return (nota * 100) / conta;

        }
        /// <summary>
        /// Método que retorna uma lisa de array de string contendo o resultado de uma busca baseada na string de consulta de entrada.
        /// </summary>
        /// <param name="consulta">String de entrada a ser buscada</param>
        /// <returns>Lista de Array de string contendo o resultado, retorna uma lista vazia caso não tenha nenhum resultado.</returns>
        public List<String[]> listaBusca(String consulta) 
        {
            List<String[]> resultado = new List<string[]>();
            Double nota = 0;
            criaArray();
            for (int i = 0; i < listaProdutos.Count; i++)
            {
                
                nota = pontua(consulta, listaProdutos[i][1]);
               
                if (nota > 0) 
                {
                    resultado.Add(new String[] {listaProdutos[i][0], listaProdutos[i][1], nota.ToString()});
                }
            }

            return RevQuickSort (resultado, 0, resultado.Count -1);
        }
        /// <summary>
        /// Método que verifica se a String contém letras.
        /// </summary>
        /// <param name="texto">String para analise</param>
        /// <returns>Boolean de resultado</returns>
        private bool contemLetras(string texto)
        {
            if (texto.Where(c => char.IsLetter(c)).Count() > 0)
                return true;
            else
                return false;
        }

        /// <summary>
        /// Métod;o que verifica se a String contém números.
        /// </summary>
        /// <param name="texto">String para analise</param>
        /// <returns>Boolean</returns>
        private bool contemNumeros(string texto)
        {
            if (texto.Where(c => char.IsNumber(c)).Count() > 0)
                return true;
            else
                return false;
        }
        /// <summary>
        /// Metodo que ordena um array de interiros
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        private int[] QuickSort(int[] a) { return QuickSort(a, 0, a.Length - 1); }

        private int[] QuickSort(int[] a, int left, int right)
        {   
            int i = left;
            int j = right;
            double pivotValue = ((left + right) / 2);
            int x = a[Convert.ToInt32(pivotValue)];
            int w = 0;
            while (i <= j)
            {
                while (a[i] < x)
                {
                    i++;
                }
                while (x < a[j])
                {
                    j--;
                }
                
                if (i <= j)
                {
                    w = a[i];
                    a[i++] = a[j];
                    a[j--] = w;
                }
            }
            
            if (left < j)
            {
                QuickSort(a, left, j);
            }
            
            if (i < right)
            {
                QuickSort(a, i, right);
            }
            
            return a;
        }

        private int[] RevQuickSort(int[] a) { return RevQuickSort(a, 0, a.Length - 1); }

        private int[] RevQuickSort(int[] a, int left, int right)
        {
            int i = left;
            int j = right;
            double pivotValue = ((left + right) / 2);
            int x = a[Convert.ToInt32(pivotValue)];
            int w = 0;
            
            while (i <= j) //Posição de I é menor igual a J
            {
                while (a[j] < x)
                {
                    j--;
                }
                while (x < a[i])
                {
                    i++;
                }

                if (i <= j)
                {
                    w = a[i];
                    a[i++] = a[j];
                    a[j--] = w;
                }
            }

            if (left < j)
            {
                RevQuickSort(a, left, j);
            }

            if (i < right)
            {
                RevQuickSort(a, i, right);
            }

            return a;
        }

        private List<String[]> RevQuickSort(List<String[]> a, int left, int right)
        {
            int i = left;
            int j = right;
            double pivotValue = ((left + right) / 2);
            double x = Convert.ToDouble(a[Convert.ToInt32(pivotValue)][2]);
            String[] w;

            while (i <= j) //Posição de I é menor igual a J
            {
                while (Convert.ToDouble( a[j][2] )< x)
                {
                    j--;
                }
                while (x < Convert.ToDouble(a[i][2]))
                {
                    i++;
                }

                if (i <= j)
                {
                    w = a[i];
                    a[i++] = a[j];
                    a[j--] = w;
                }
            }

            if (left < j)
            {
                RevQuickSort(a, left, j);
            }

            if (i < right)
            {
                RevQuickSort(a, i, right);
            }

            return a;
        }
    }
}
