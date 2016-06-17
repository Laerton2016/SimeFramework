using SIME.Class.DAO;
using SIME.Class.primitivo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe que cuida das regrada de negócio para orçameto
    /// <autor>Laerton Marques de Figueiredo</autor>
    /// <Data>30/04/2016</Data>
    /// </summary>
    public class FacedeOrcamento
    {
        private Orcamento _orcamento;
        private NetOS _OS;
        private Usuario _user;
        private NetCliente _cliente;
        private List<Int64> _lista_itens_exluidos;
        private List<Item_orcamento> _itens;
        private List<SerieMaquina> _listaSeries;
        private bool _montagem;
        private Int64 _quantidade;
        private DAOProduto _daoProduto;
        private DAOOrcamento _daoOrca;
        private DAOItemOrcamento _daoItem;
        private DAOCliente _daoCliente;
        private DAOSerieMaquina _daoSerie;
        private DAOOS _daoOS;

        /// <summary>
        /// Quantidade de maquinas a serie;
        /// </summary>
        public long Quantidade
        {
            get
            {
                return _quantidade;
            }

            set
            {
                _quantidade = value;
            }
        }


        /// <summary>
        /// Cria um FacedeOrçamento para um novo orçamento
        /// </summary>
        /// <param name="user">Usuário que está ministrando o Facede</param>
        /// <param name="cliente">Cliente a qual pertence o Orçamento</param>
        /// <param name="valor">Valor do Orçamento</param>
        /// <param name="merkup">Taxa de Merkup aplicada ao orçamento</param>
        /// <param name="montagem">Boleno que confirma se é montagem</param>
        public FacedeOrcamento(Usuario user, NetCliente cliente, float valor, float merkup, bool montagem)
        {
            CriaDAOS();
            _user = user;
            _cliente = cliente;
            _montagem = montagem;
            if (montagem)
            {
                Quantidade = 1;
            }

            NovoOrcamento(cliente.Cod, valor, merkup, montagem);
        }

        /// <summary>
        /// Cria um objeto FacedeOrcamento para um orçamento já existente
        /// </summary>
        /// <param name="user">Usuário que ministra o orçamento</param>
        /// <param name="idOrcamento">Id do orçamento</param>
        public FacedeOrcamento(Usuario user, Int64 idOrcamento)
        {
            CriaDAOS();
            _user = user;
            _orcamento = _daoOrca.Buscar(idOrcamento);
            _itens = _daoItem.BuscarItens(idOrcamento);
            _cliente = _daoCliente.Buscar(_orcamento.Id_cliente);
            MontaListas();
            if (VerificaMontagem())
            {
                _montagem = true;
            }
            else
            {
                _montagem = false;
            }
        }


        /// <summary>
        /// Cria um FacedeOrcamento para um orçamento repassado como parametro
        /// </summary>
        /// <param name="user">Usuário que irá ministrar os dados</param>
        /// <param name="orcamento">Orçamento que será usado como paramentro</param>
        public FacedeOrcamento(Usuario user, Orcamento orcamento)
        {
            CriaDAOS();
            _user = user;
            _orcamento = orcamento;
            _itens = _daoItem.BuscarItens(_orcamento.Id);
            _cliente = _daoCliente.Buscar(_orcamento.Id_cliente);
            MontaListas();
            if (VerificaMontagem())
            {
                _montagem = true;
            }
            else
            {
                _montagem = false;
            }
        }
        /// <summary>
        /// Método utilizado para montar todas as listas pertencetes ao Orçamento já existente para este Facede
        /// </summary>
        private void MontaListas()
        {
            _listaSeries = _daoSerie.BuscarLista(_orcamento.Id);
            _itens = _daoItem.BuscarItens(_orcamento.Id);
        }

        /// <summary>
        /// Método retorna se o Orçamento já foi finalizado e transformado em venda.
        /// </summary>
        /// <returns>Booleano que confirma se o Orçamento já está fechado.</returns>
        public Boolean OrcamentoFechado()
        {
            return _orcamento.Execultado;
        }

        /// <summary>
        /// Método cria todos os Daos Necessários para o FacedeOrcamento
        /// </summary>
        private void CriaDAOS()
        {
            _daoOrca = FactoryDAO.CriaDAOOrcamento();
            _daoItem = FactoryDAO.CriaDAOItemOrcamento();
            _daoCliente = FactoryDAO.CriaDAOCliente();
            _daoProduto = FactoryDAO.CriaDAOProduto();
            _daoSerie = FactoryDAO.CriaDaoSerieMaquina();
            _daoOS = FactoryDAO.CriaDaoOS();
        }
        /// <summary>
        /// Método efetua a busca por um produto a partir do código informado no paramentro. O mesmo deve ser positivo maior que zero.
        /// </summary>
        /// <param name="cod">Inteiro positivo maior que zero.</param>
        /// <returns>Produto localizado a aprtir do código informado.</returns>
        public NetProduto BuscaProduto(Int64 cod)
        {
            if (cod <= 0)
            {
                throw new Exception("Código do produto inválido.");
            }

            NetProduto resposta = _daoProduto.Buscar(cod);
            if (resposta == null)
            {
                throw new Exception("Produto não localizado com o código informado. Favor verificar o código.");
            }

            return resposta;
        }

        /// <summary>
        /// Método retorna booleano que confirma se orçamento é do typo Orçamento_montagem
        /// </summary>
        /// <returns>true para tipo Orçamento_montagem e false para Orçamento simples</returns>
        private Boolean VerificaMontagem()
        {
            return (_orcamento.GetType() == typeof(Orcamento_Montagem));
        }

        /// <summary>
        /// Método efetua a busca no banco de dados por uma lista de produtos pertencente ao mesmo grupo podendo ser filtrado removendo os descontinuados da lista.
        /// </summary>
        /// <param name="IDGrupo">Id do grupo a ser filtrado</param>
        /// <param name="FiltraDescontinuado">Booleando que filtra os descontinuados, caso True será removido os descontinuados da lista.</param>
        /// <returns>Lista de produtos localizado</returns>
        public List<NetProduto> BuscaGrupo(Int64 IDGrupo, Boolean FiltraDescontinuado)
        {
            if (IDGrupo <= 0)
            {
                throw new Exception("Grupo não cadastrado.");
            }

            return _daoProduto.BuscarGrupo(IDGrupo, FiltraDescontinuado);
        }

        /// <summary>
        /// Busca por produtos cadastrados e filtra produtos por estoque disponível.
        /// </summary>
        /// <param name="Termo">Termo de busca </param>
        /// <param name="FiltraEstoque">Filtra, disponibilizando caso true só os que apresentarem estoque positívo.</param>
        /// <returns>Lista de produtos cadastrados localizados pelo termo informado, casos não encontre retorna uma lista em branco.</returns>
        public List<NetProduto> BuscaGrupo(String Termo, Boolean FiltraEstoque)
        {
            if (Termo.Trim().Equals(""))
            {
                throw new Exception("Termo de busca não pode ser em branco.");
            }

            if (new Uteis().ContemPontuacao(Termo))
            {
                throw new Exception("Termo de busca não pode conter caracteres especiais.");
            }

            return _daoProduto.Buscar(Termo, FiltraEstoque);
        }

        /// <summary>
        /// Método busca por um produto a partir do EAN informado
        /// </summary>
        /// <param name="EAN">EAN sendo verificado dígito verificado, permitido somente com 13 dígitos</param>
        /// <returns>Produto localizado a partir do EAN informado</returns>
        public NetProduto BuscaProduto(String EAN)
        {
            ///Validação dos dados.
            if (EAN.Trim().Equals(""))
            {
                throw new Exception("EAN não informado.");
            }

            Ean13Barcode2005.Ean13 ean = new Ean13Barcode2005.Ean13();
            if (EAN.Length > 13)
            {
                throw new Exception("EAN só pode conter 13 dígitos.");
            }

            if (EAN.Length < 13)
            {
                EAN = "0000000000000" + EAN;
                EAN = new Uteis().direita(EAN, 13);
            }

            if (!ean.chekDigitoEAN(EAN))
            {
                throw new Exception("Código EAN inválido.");
            }
            //Busca
            NetProduto resporta = _daoProduto.BuscaEAN(EAN);
            if (resporta == null)
            {
                throw new Exception("Não foi localizado nenhum produto com o EAN informado.");
            }

            return resporta;
        }


        /// <summary>
        /// Método abre um orçamento novo com os dados repassados pelos paramentros, não sendo permitido caso já tenha um orçamento aberto na sessão
        /// </summary>
        /// <param name="idCliente">ID do cliente não sendo aceito valores negativos. Para clientes não cadastrados usa-se o id 0</param>
        /// <param name="valor">Valor do orçamento não sendo permitido valores negativos</param>
        /// <param name="merkup">Mercakup do orçamento não sendo permitido valores megativos e nem maiores e igual a 1</param>
        /// <param name="montagem">Boeando para informar se orçamento trata-se de uma montagem</param>
        public void NovoOrcamento(Int64 idCliente, float valor, float merkup, Boolean montagem)
        {
            if (_orcamento.Execultado == false) { throw new Exception("Já existe um orçamento aberto!"); }
            if (idCliente < 0) { throw new Exception("Cliente não localizado, id não pode ser menor que zero."); }
            if (merkup < 0) { throw new Exception("Merkup não pode ser negativo!"); }
            if (valor < 0) { throw new Exception("Valor do orçamento não pode ser negativo!"); }
            if (valor == 0 && merkup == 0) { throw new Exception("Merkup e valor não podem ser ambos zero."); }
            // Cria o orçameto se montagem ou simples
            if (montagem)
            {
                _orcamento = FactoryOrcamento.CriaOrcamentoMontagem(_user.getCod());

            }
            else
            {
                _orcamento = FactoryOrcamento.CriaOrcamento(_user.getCod());
            }
            _montagem = montagem;
            _orcamento.Id_cliente = idCliente;
            _orcamento.Total = valor;
            _orcamento.Merkup = merkup;
            _daoOrca.Salvar(_orcamento);
            _itens = new List<Item_orcamento>();
            _listaSeries = new List<SerieMaquina>();
            _lista_itens_exluidos = new List<long>();
        }

        /// <summary>
        /// Busca por um orçamento baseado no id do orçamento.
        /// </summary>
        /// <param name="Id">Id do orçameto sendo positivo maior que zero.</param>
        public void BuscarOrcamento(Int64 Id)
        {
            if (Id <= 0)
            {
                throw new Exception("Id do orçamento não pode ser negativo.");
            }
            _orcamento = _daoOrca.Buscar(Id);
            _itens = BuscaItens(_orcamento.Id);
        }

        /// <summary>
        /// Busca todos os orçamentos de um determinado cliente a partir do id do cliente
        /// </summary>
        /// <param name="Id_cliente">Id do cliente sendo positivo maior que zero.</param>
        /// <returns>Lista de orçamentos de um determiado cliente</returns>
        public List<Orcamento> BuscaOrcamentos(Int64 Id_cliente)
        {
            if (Id_cliente <= 0)
            {
                throw new Exception("Id do cliente não pode ser negativo.");
            }
            return _daoOrca.BuscarOrcamentos(Id_cliente);
        }

        /// <summary>
        /// Busca por todos os orçamentos que não tem clientes associados
        /// </summary>
        /// <returns>Lista de orçamentos sem clientes</returns>
        public List<Orcamento> BuscaOrcamentos()
        {
            return _daoOrca.BuscarOrcamentos(0);
        }

        /// <summary>
        /// Busca por todos os orçamentos que ainda estão abertos
        /// </summary>
        /// <returns>Lista de orçamentos que ainda estão abertos</returns>
        public List<Orcamento> OrcamentosAbertos()
        {
            return _daoOrca.BuscarOrcamentos(true);
        }
        /// <summary>
        /// Método busca por orçamentos abertos de um determinado usuário.
        /// </summary>
        /// <param name="idUser">Id do usuário que deve ter orçamentos abertos</param>
        /// <returns>Lista de orçamentos localizados pelo id do usuário, caso não encontre retorna uma lista vazia.</returns>
        public List<Orcamento> OrcamentosAbertos(Int64 idUser)
        {
            return _daoOrca.BuscarOrcamentos(true, idUser);
        }



        /// <summary>
        /// Produra por todos os itens de um orçamento a partir do id repassado, caso não tenha volta um lista vazia.
        /// </summary>
        /// <param name="idOrcamento">Id do orçamento cujos itens serão buscado</param>
        /// <returns>Lista de itens do orçamento pelo id repassado. Caso não haja retorna uma lista vazia.</returns>
        private List<Item_orcamento> BuscaItens(Int64 idOrcamento)
        {
            return _daoItem.BuscarItens(idOrcamento);
        }

        /// <summary>
        /// Cria um novo item para o orçamento atual
        /// </summary>
        /// <returns>Retorna um item de um orçamento e novo</returns>
        public Item_orcamento CriaItem()
        {

            return FactoryItemOrcamento.CriaItem(_orcamento.Id);
        }
        /// <summary>
        /// Adiciona um item a lista de orçamento
        /// </summary>
        /// <param name="item">Item a ser adicionado</param>
        public void AddItem(Item_orcamento item)
        {
            _itens.Add(item);
        }
        /// <summary>
        /// Lista os itens incluso no orçamento
        /// </summary>
        /// <returns></returns>
        public List<Item_orcamento> GetListaItens()
        {
            return _itens;
        }
        /// <summary>
        /// Exlui um item da lista de itens cadastrado no orçamento
        /// </summary>
        /// <param name="index">Indice do item a ser removido da lista, não podendo ser negativo ou fora da faixa .</param>
        public void ExcluiItem(Int64 index)
        {
            if (index < 0 || index >= _itens.Count)
            {
                throw new Exception("Item fora da lista de indexação.");
            }
        }

        /// <summary>
        /// Método informa se o orçameto está aberto ou realizado
        /// </summary>
        /// <returns>True para Orçamento Fechado ou realizado ou False para Aberto ainda não realizado.</returns>
        public Boolean Status()
        {
            if (_orcamento == null || _orcamento.Execultado)
            {
                return true;
            }
            return false;

        }
        /// <summary>
        /// Método transforma um orçamento em uma venda.
        /// </summary>
        public void ViraVenda()
        {
            if (_orcamento.Execultado)
            {
                throw new Exception("Orçamento já transformado em venda.");
            }

            if (ItensSemEstoque())
            {
                throw new Exception("Exitem itens sem estoque, orçamento não pode ser transformado em venda.");
            }


            ///Código a digitar

            _orcamento.Execultado = true;
            _daoOrca.Salvar(_orcamento);
            if (VerificaMontagem())
            {
                CriaOS();
                CriaListaSeries(Quantidade);

            }
        }
        /// <summary>
        /// Método cria a OS vinculada ao orçamento
        /// </summary>
        private void CriaOS()
        {
            _OS = new NetOS();
            _OS.IdCliente = _orcamento.Id_cliente;
            _OS.Abertura = DateTime.Now;
            _OS.DataNF = DateTime.Now;
            _OS.Defeito = "OS Aberto para montagem da(s) maquina(s) do orçamento nº " + _orcamento.Id;
            _OS.Garantia = true;
            _OS.IdAtendimento = _user.getCod();

            ///Preenche os dados da os
            _daoOS.Salvar(_OS);
        }

        /// <summary>
        /// Método cria os números de sérire para o orçamento em questão
        /// </summary>
        /// <param name="quantidade">Quantidade de seriais a serem criados</param>
        private void CriaListaSeries(Int64 quantidade)
        {

            for (int i = 1; i <= quantidade; i++)
            {
                SerieMaquina serie = new SerieMaquina(_orcamento.Id, i);
                _daoSerie.Salvar(serie);
                _listaSeries.Add(serie);

            }
        }
        /// <summary>
        /// Método verifica se há itens sem estoque no orçamento e já atauliza a lista de itens sobre sua disponibilidade.
        /// </summary>
        /// <returns>Booleano de confirmação</returns>
        private bool ItensSemEstoque()
        {
            Boolean resposta = false;
            foreach (var item in _itens)
            {
                //Atualizando a disponibilidade
                NetProduto produto = _daoProduto.Buscar(item.Id_produto);

                if (produto.QuantEstoque == 0)
                {
                    item.Status = Class.Orcamento.Status.FALTA;
                    resposta = true;
                }
                else if (produto.QuantEstoque < item.Quantidade)
                {
                    item.Status = Class.Orcamento.Status.INSUFICIENTE;
                    resposta = true;
                }
                else if (produto.Descontinuado)
                {
                    item.Status = Class.Orcamento.Status.DISCONTINUADO;
                    resposta = true;
                }
                else
                {
                    item.Status = Class.Orcamento.Status.DISPONIVEL;

                }
            }
            return resposta;
        }






    }
}
