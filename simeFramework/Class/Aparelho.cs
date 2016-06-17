using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ADODB;

namespace SIME.Class
{
    public class Aparelho : ITrataDados
    {
        private Sime.Usuarios users = new Sime.Usuarios(new Conexao().getContas());
        private Recordset RSdados = new Recordset();
        private Int32 ID, ID_cliente,
            ID_tipoAparelho, ID_Marca,
            ID_loja, ID_Estoque,
            ID_atendimento, ID_Operador,
            ID_OSRetorno, ID_tecnico,
            ID_OPRecebedor, ID_seguradora = 0;

        public Int32 ID_Seguradora
        {
            get { return ID_seguradora; }
            set { ID_seguradora = value; }
        }

        private String acessorios;
        private String modelo, serie,
            nf, defeito, voltagem,
            avarias, SQL;
        private Boolean garantia, retorno, arranhado;
        private DateTime dataNF, dtAberturaOS, dtFechamento, dtInicio, dtRecebimento;
        private Double vlServico;
        private Boolean entregue;
        private DateTime dataEntrega;
        private Int32 id_OpEntregue;


        /// <summary>
        /// Classe cria um objeto do tipo aparelho novo
        /// </summary>
        public Aparelho()
        {
            this.ID = 0;
        }
        /// <summary>
        /// Classe cria um objeto do tipo aparelho baeado no ID informado retornando os dados do banco de dados
        /// </summary>
        /// <param name="ID">Inteiro 32  bits</param>
        public Aparelho(Int32 ID)
        {
            this.ID = ID;
            buscaDados(ID);
        }
        public Int32 Id_OpEntregue
        {
            get { return id_OpEntregue; }

        }

        public Int32 getID() {
            return ID;
        }
        public DateTime DataEntrega
        {
            get { return dataEntrega; }

        }

        public Boolean Entregue
        {
            get { return entregue; }

        }

        public Double VlServico
        {
            get { return vlServico; }
            set { vlServico = value; }
        }

        public DateTime DtRecebimento
        {
            get { return dtRecebimento; }
            set { dtRecebimento = value; }
        }

        public DateTime DtInicio
        {
            get { return dtInicio; }
            set { dtInicio = value; }
        }

        public DateTime DtFechamento
        {
            get { return dtFechamento; }
            set { dtFechamento = value; }
        }

        public DateTime DtAberturaOS
        {
            get { return dtAberturaOS; }
            set { dtAberturaOS = value; }
        }

        public DateTime DataNF
        {
            get { return dataNF; }
            set { dataNF = value; }
        }



        public Boolean Arranhado
        {
            get { return arranhado; }
            set { arranhado = value; }
        }

        public Boolean Retorno
        {
            get { return retorno; }
            set { retorno = value; }
        }

        public Boolean Garantia
        {
            get { return garantia; }
            set { garantia = value; }
        }



        public String Acessorios
        {
            set { acessorios = value; }
        }

        public String[] getAcessorios()
        {
            return ((acessorios != null)? acessorios.Split(';'): new String[1]);
        }

        public String Avarias
        {
            get { return avarias; }
            set { avarias = value; }
        }

        public String Voltagem
        {
            get { return voltagem; }
            set { voltagem = value; }
        }

        public String Defeito
        {
            get { return defeito; }
            set { defeito = value; }
        }

        public String Nf
        {
            get { return nf; }
            set { nf = value; }
        }

        public String Serie
        {
            get { return serie; }
            set { serie = value; }
        }

        public String Modelo
        {
            get { return modelo; }
            set { modelo = value; }
        }
        public Int32 IDOPRecebedor
        {
            get { return ID_OPRecebedor; }
            set { ID_OPRecebedor = value; }
        }

        public Int32 IDtecnico
        {
            get { return ID_tecnico; }
            set { ID_tecnico = value; }
        }

        public Int32 IDOSRetorno
        {
            get { return ID_OSRetorno; }
            set { ID_OSRetorno = value; }
        }

        public Int32 IDOperador
        {
            get { return ID_Operador; }
            set { ID_Operador = value; }
        }

        public Int32 IDatendimento
        {
            get { return ID_atendimento; }
            set { ID_atendimento = value; }
        }

        public Int32 IDEstoque
        {
            get { return ID_Estoque; }
            set { ID_Estoque = value; }
        }

        public Int32 IDloja
        {
            get { return ID_loja; }
            set { ID_loja = value; }
        }

        public Int32 IDMarca
        {
            get { return ID_Marca; }
            set { ID_Marca = value; }
        }

        public Int32 IDTipoAparelho
        {
            get { return ID_tipoAparelho; }
            set { ID_tipoAparelho = value; }
        }

        public Int32 IDcliente
        {
            get { return ID_cliente; }
            set { ID_cliente = value; }
        }

        //Método para coletar os dados em questão do cliente.
        private void buscaDados(Int32 ID)
        {
            SQL = "SELECT Aparelhos.* FROM Aparelhos WHERE (((Aparelhos.cod) = " + ID + "));";
            String informativo = "";
            if (RSdados.State != 0)
            {
                RSdados.Close();
            }
            Connection conex1 = new Conexao().getContas();
            RSdados.Open(SQL, conex1);
            //coletando dados.
            if (!(RSdados.EOF && RSdados.BOF))
            {
                this.ID = Convert.ToInt32(RSdados.Fields["cod"].Value);
                this.ID_cliente = (Convert.ToInt32((RSdados.Fields["cod_cliente"].Value == null) ? 0 : RSdados.Fields["cod_cliente"].Value));
                this.ID_tipoAparelho = (Convert.ToInt32((RSdados.Fields["cod_tipo"].Value == null) ? 0 : RSdados.Fields["cod_tipo"].Value));
                this.ID_Marca = (Convert.ToInt32((RSdados.Fields["cod_marca"].Value == null) ? 0 : RSdados.Fields["cod_marca"].Value));
                this.Modelo = (Convert.ToString((RSdados.Fields["modelo"].Value == null) ? "" : RSdados.Fields["modelo"].Value));
                this.Garantia = (Convert.ToBoolean((RSdados.Fields["garantia"].Value == null) ? false : RSdados.Fields["garantia"].Value));
                this.Serie = (Convert.ToString((RSdados.Fields["serie"].Value == null) ? "" : RSdados.Fields["serie"].Value));
                this.Nf = (Convert.ToString((RSdados.Fields["NF"].Value == null) ? "" : RSdados.Fields["NF"].Value));
                this.DataNF = (Convert.ToDateTime((RSdados.Fields["data"].Value.Equals(DBNull.Value) ) ? null : RSdados.Fields["data"].Value));
                informativo = (Convert.ToString((RSdados.Fields["loja"].Value == null) ? "0" : RSdados.Fields["loja"].Value));
                this.ID_loja = (new Uteis().Sonumeros(informativo)) ? Convert.ToInt32(informativo) : 0;
                this.ID_Estoque = (Convert.ToInt32((RSdados.Fields["cod_Estoque"].Value == null) ? 0 : RSdados.Fields["cod_estoque"].Value));
                this.ID_atendimento = (Convert.ToInt32((RSdados.Fields["cod_atendimento"].Value == null) ? 0 : RSdados.Fields["cod_atendimento"].Value));
                this.Defeito = (Convert.ToString((RSdados.Fields["Defeito"].Value == null) ? "" : RSdados.Fields["Defeito"].Value));
                this.ID_Operador = (Convert.ToInt32((RSdados.Fields["OP"].Value == null) ? 0 : RSdados.Fields["OP"].Value));
                this.DtAberturaOS = (Convert.ToDateTime((RSdados.Fields["Abertura"].Value.Equals(DBNull.Value)) ? null : RSdados.Fields["Abertura"].Value));
                this.DtFechamento = (Convert.ToDateTime((RSdados.Fields["Fechamento"].Value.Equals(DBNull.Value)) ? null : RSdados.Fields["Fechamento"].Value));
                this.Voltagem = (Convert.ToString((RSdados.Fields["Voltagem"].Value == null) ? "" : RSdados.Fields["Voltagem"].Value));
                this.Arranhado = (Convert.ToBoolean((RSdados.Fields["Arranhado"].Value == null) ? false : RSdados.Fields["Arranhado"].Value));
                this.Avarias = (Convert.ToString((RSdados.Fields["Avarias"].Value == null) ? "" : RSdados.Fields["Avarias"].Value));
                this.Acessorios = (Convert.ToString((RSdados.Fields["Acessórios"].Value == null) ? "" : RSdados.Fields["Acessórios"].Value));
                this.VlServico = (Convert.ToDouble((RSdados.Fields["valor_Servico"].Value == null) ? 0 : RSdados.Fields["valor_servico"].Value));
                this.ID_OSRetorno = (Convert.ToInt32((RSdados.Fields["OS_retorno"].Value == null) ? 0 : RSdados.Fields["OS_retorno"].Value));
                this.Retorno = (Convert.ToBoolean((RSdados.Fields["retorno"].Value == null) ? false : RSdados.Fields["retorno"].Value));
                this.DtInicio = (Convert.ToDateTime((RSdados.Fields["data_inicio_serviço"].Value.Equals(DBNull.Value)) ? null : RSdados.Fields["data_inicio_serviço"].Value));
                this.DtRecebimento = (Convert.ToDateTime((RSdados.Fields["hora_inicio_serviço"].Value.Equals(DBNull.Value)) ? null : RSdados.Fields["Abertura"].Value));
                this.ID_tecnico = (Convert.ToInt32((RSdados.Fields["ID_tecnico"].Value == null) ? 0 : RSdados.Fields["ID_tecnico"].Value));
                this.ID_OPRecebedor = (Convert.ToInt32((RSdados.Fields["ID_OPRecebedor"].Value == null) ? 0 : RSdados.Fields["ID_OPRecebedor"].Value));
                RSdados.Close();
                
                //Coletando os dados de entrega
                SQL = "SELECT OS_Entragas.* FROM OS_Entragas WHERE (((OS_Entragas.OS)=" + ID + "));";
                RSdados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                if (!(RSdados.BOF || RSdados.EOF))
                {
                    entregue = true;
                    this.dataEntrega = (Convert.ToDateTime((RSdados.Fields["Data"].Value.Equals(DBNull.Value)) ? null : RSdados.Fields["Data"].Value));
                    this.id_OpEntregue = (Convert.ToInt32((RSdados.Fields["OP"].Value == null) ? 0 : RSdados.Fields["op"].Value));
                }
                else
                {
                    entregue = true;
                    dataEntrega = Convert.ToDateTime(null);
                    this.id_OpEntregue = 0;
                }
            }
        }
        /// <summary>
        /// Metódo que eferua a entrega do aparelho
        /// </summary>
        public void entregar(Int32 ID_op)
        {
            this.dataEntrega = DateTime.Now;
            this.id_OpEntregue = ID_op;
            if (RSdados.State != 0)
            {
                RSdados.Close();
            }
            SQL = "SELECT OS_Entregas.* FROM OS_Entregas WHERE (((Entredas.OS) = " + ID + "));";
            RSdados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            if (RSdados.RecordCount == 1)
            {
                RSdados.AddNew();
                RSdados.Fields["OS"].Value = this.ID;
                RSdados.Fields["DATA"].Value = this.DataEntrega;
                RSdados.Fields["OP"].Value = this.Id_OpEntregue;
                RSdados.Update();
                RSdados.Close();
            }
        }
        /// <summary>
        /// Métofo que adiciona ou atualiza os dados de um aparelho no banco de dados
        /// </summary>
        /// <param name="conex">ADODB.Connection</param>
        /// <returns>Boolean</returns>
        public bool salvar()
        {
            Recordset dados = new Recordset();


            if (this.ID == 0)
            {
                SQL = "SELECT Aparelhos.* FROM Aparelhos;";
                dados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                dados.AddNew();
            }
            else
            {

                SQL = "SELECT Aparelhos.* FROM Aparelhos WHERE (((Aparelhos.cod) = " + this.ID + "));";
                dados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
            }


            dados.Fields["cod_cliente"].Value = this.ID_cliente;
            dados.Fields["cod_tipo"].Value = this.ID_tipoAparelho;
            dados.Fields["cod_marca"].Value = this.ID_Marca;
            dados.Fields["Modelo"].Value = this.modelo;
            dados.Fields["garantia"].Value = (this.Garantia) ? 1 : 0;
            dados.Fields["Serie"].Value = this.Serie;
            dados.Fields["NF"].Value = this.Nf;
            if (this.DataNF != DateTime.MinValue)
            {
                dados.Fields["data"].Value = this.DataNF;
            }

            dados.Fields["loja"].Value = this.ID_loja;
            dados.Fields["cod_estoque"].Value = this.ID_Estoque;
            dados.Fields["cod_atendimento"].Value = this.ID_atendimento;
            dados.Fields["Defeito"].Value = this.Defeito;
            dados.Fields["OP"].Value = this.ID_Operador;
            if (this.dtAberturaOS != DateTime.MinValue)
            {
                dados.Fields["Abertura"].Value = this.DtAberturaOS;
            }
            if (this.DtFechamento != DateTime.MinValue)
            {
                dados.Fields["Fechamento"].Value = this.DtFechamento;
            }

            dados.Fields["Voltagem"].Value = this.Voltagem;
            dados.Fields["Arranhado"].Value = this.Arranhado;
            dados.Fields["Avarias"].Value = this.Avarias;
            dados.Fields["Acessórios"].Value = this.acessorios;
            dados.Fields["Valor_servico"].Value = this.vlServico;
            dados.Fields["OS_retorno"].Value = this.ID_OSRetorno;
            dados.Fields["retorno"].Value = this.Retorno;
            if (this.DtInicio != DateTime.MinValue)
            {
                dados.Fields["data_inicio_serviço"].Value = this.DtInicio;
            }

            if (this.DtRecebimento != DateTime.MinValue)
            {
                dados.Fields["hora_inicio_serviço"].Value = this.DtRecebimento;
            }

            dados.Fields["ID_tecnico"].Value = this.ID_tecnico;
            dados.Fields["ID_OPrecebedor"].Value = this.ID_OPRecebedor;

            dados.Update();
            this.ID = Convert.ToInt32(dados.Fields["Cod"].Value);
            dados.Close();

            return true;
        }
        /// <summary>
        /// Método para excluir os dados do banco de dados de um aparelho
        /// </summary>
        /// <param name="conex">ADODB Connection</param>
        /// <returns>Boolean</returns>
        public bool excluir()
        {
            if (this.ID != 0)
            {
                if (RSdados.State != 0)
                {
                    RSdados.Close();
                }
                SQL = "SELECT Aparelhos.* FROM Aparelhos WHERE (((Aparelho.cod) = " + this.ID + "));";
                RSdados.Open(SQL, new Conexao().getContas(), CursorTypeEnum.adOpenDynamic, LockTypeEnum.adLockOptimistic);
                if (RSdados.RecordCount != 1)
                {
                    return false;
                }
                else
                {
                    RSdados.Delete();
                }
                RSdados.Close();
                this.ID = 0;
                return true;
            }
            return false;
        }

        public string ToStringWeb()
        {
            String mensagem = "<td>";
            mensagem = "<b>INFORMAÇÕES DO APARELHO</b> <br> " +
                "<b>Aparelho: </b>" + new TiposAparelhos().getTipo(IDTipoAparelho) + " <b>Marca: </b>" + new Marcas().getTipo(IDMarca) + " <b>Modelo:</b> " + Modelo + "<br><br>";
            mensagem += "<b>INFORMAÇÕES DE GARANTIA</b><br><br>";
            if (Garantia)
            {
                mensagem += "<b>N° de Série: </b>" + Serie + " <b>N° da NF:</b> " + Nf + " <b>Data da Nf:</b> " + DataNF.ToShortDateString() + "<br>" +
                    " <b>Loja vendedora:</b> " + new lojas().getLoja(ID_loja) + " <b>Produto assegurado:</b> " + ((ID_seguradora == 0) ? "Não" : "Sim " + "<b>Seguradora:</b> " + new Seguradora().getTipo(ID_seguradora)) + "<br>";
            }
            else
            {
                mensagem += "Produto fora de garantia. <br><br>";
            }

            if (retorno)
            {
                mensagem += "<b>APARELHO DE RETORNO</b><br>" + "<b>OS anterior:</b> " + ID_OSRetorno + "<br><br>";
            }

            mensagem += "<b>OUTRAS INFORMAÇÕES DO APARELHO</b><br> <b>Voltagem:</b> " + voltagem + " <b>Tipo de estoque: </b>" + new tipoEstoque().getTipo(ID_Estoque) + "<br>" +
                "<b>Avarias:</b> " + avarias + " <b>Acessórios:</b>" + informeAcessorios() + "<br><br>" + "<b>DEFEITO</b><br>" + defeito + "<br><br><b>INFORMAÇÕES DA OS</b><br> <b>OS: </b>" +
                ((ID == 0) ? " OS ainda não gravada" : Convert.ToString(ID)) +
                "<b>Data abertura OS: </b>" + dtAberturaOS.ToShortDateString();
            return mensagem + "</td>";
        }

        public override string ToString()
        {
            String mensagem = "";
            mensagem = "INFORMAÇÕES DO APARELHO \n" +
                "Aparelho: " + new TiposAparelhos().getTipo(IDTipoAparelho) + " \nMarca: " + new Marcas().getTipo(IDMarca) + "\nModelo: " + Modelo + "";
            mensagem += "\nINFORMAÇÕES DE GARANTIA\n";
            if (Garantia)
            {
                mensagem += "N° de Série: " + Serie + " N° da NF: " + Nf + " Data da Nf: " + DataNF.ToShortDateString() + "\n" +
                    "Loja vendedora: " + new lojas().getLoja(ID_loja) + " \nProduto assegurado: " + ((ID_seguradora == 0) ? "Não" : "Sim " + " Seguradora: " + new Seguradora().getTipo(ID_seguradora)) + "\n";
            }
            else
            {
                mensagem += "Produto fora de garantia.";
            }

            if (retorno)
            {
                mensagem += "APARELHO DE RETORNO \n" + "OS anterior: " + ID_OSRetorno + "\n";
            }

            mensagem += "OUTRAS INFORMAÇÕES DO APARELHO \nVoltagem: " + voltagem + "\nTipo de estoque: " + new tipoEstoque().getTipo(ID_Estoque) + "\n" +
                "Avarias: " + avarias + "\nAcessórios:" + informeAcessorios() + "\n" + "DEFEITO " + defeito + "\nINFORMAÇÕES DA OS \nOS: " +
                ((ID == 0) ? " OS ainda não gravada" : Convert.ToString(ID)) +
                " Data abertura OS: " + dtAberturaOS.ToShortDateString();
            return mensagem;
        }
        /// <summary>
        /// Método que retorna a lista de acessórios.
        /// </summary>
        /// <returns></returns>
        public String informeAcessorios()
        {
            String retornoMensagem = "";
            String[] acessoriosAparelho = getAcessorios();
            int tipo;
            for (int i = 0; i < acessoriosAparelho.Length - 1; i++)
            {
                tipo = Convert.ToInt16(acessoriosAparelho[i]);
                retornoMensagem += (EnumAcessorios)tipo + ((tipo != Convert.ToInt16(EnumAcessorios.OUTROS)) ? "; " : ":");
            }
            retornoMensagem += acessoriosAparelho[acessoriosAparelho.Length - 1];
            return retornoMensagem;

        }
        /// <summary>
        /// Método que retorna uma String formatada em HTML com os dados da OS do aparelho
        /// </summary>
        /// <returns>String com dados formatado em HTML</returns>
        public String WebDadosOS() {

            
            String retorna = "<b>Data abertura OS: </b>" + DtAberturaOS.ToShortDateString() + "<br>" +
                    "&nbsp;<b>Data recebimento: </b>" + DtRecebimento.ToShortDateString() + "<br>" +
                    "&nbsp;<b>N° da OS: </b>" + ID + "<br>" +
                    "&nbsp;<b>Operador: </b>" + ((IDOperador!=0)?users.buscaUsuario(IDOperador).getNome():"Sem operador") + "<br>" +
                    "&nbsp;<b>Recebedor: </b>" + ((IDOPRecebedor!=0)?users.buscaUsuario(IDOPRecebedor).getNome():"") + "<br>";
            return retorna;
        }
        /// <summary>
        /// Método retorna uma String contendo os dados do aparelho 
        /// </summary>
        /// <returns>Retorna uma string com dados do aparelho</returns>
        public String getDadosOS()
        {
           String retorna = "Data abertura OS: " + DtAberturaOS.ToShortDateString()  +
                    "Data recebimento: " + DtRecebimento.ToShortDateString() +
                    "N° da OS: " + ID  +
                    "Operador: " + users.buscaUsuario(IDOperador).getNome() +
                    "Recebedor: " + users.buscaUsuario(IDOPRecebedor).getNome() ;
            return retorna;
        }

        public String webDadosAparelho() {
            String mensagem = "<b>INFORMAÇÕES DO APARELHO</b><br><br> " +
                "<b>Aparelho: </b>" + new TiposAparelhos().getTipo(IDTipoAparelho) + "<br><b>Marca: </b>" + new Marcas().getTipo(IDMarca) + " <b>Modelo:</b> " + Modelo + "<br><br>";
            mensagem += "<b>INFORMAÇÕES DE GARANTIA</b><br><br>";
            if (Garantia)
            {
                mensagem += "<b>N° de Série: </b>" + Serie + "<br><b>N° da NF:</b> " + Nf + " <b>Dt:</b> " + DataNF.ToShortDateString() + "<br>" +
                    " <b>Loja:</b> " + new lojas().getLoja(ID_loja) + "<br><b>Produto assegurado:</b> " + ((ID_seguradora == 0) ? "Não" : "Sim " + "<b>Seguradora:</b> " + new Seguradora().getTipo(ID_seguradora)) + "<br><br>";
            }
            else
            {
                mensagem += "Produto fora de garantia. <br><br>";
            }

            if (retorno)
            {
                mensagem += "<b>APARELHO DE RETORNO</b><br>" + "<b>OS anterior:</b> " + ID_OSRetorno + "<br><br>";
            }

            mensagem += "<b>OUTRAS INFORMAÇÕES DO APARELHO</b><br><br> <b>Voltagem:</b> " + voltagem + "<br><b>Estoque: </b>" + new tipoEstoque().getTipo(ID_Estoque) + "<br>" +
                "<b>Avarias:</b> " + avarias + "<br><b>Acessórios:</b>" + informeAcessorios() + "<br><br>" + "<b>DEFEITO</b><br>" + defeito;
            return mensagem;
        }


    }

}