using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class.Orcamento
{
    /// <summary>
    /// Classe que cuida dos dados de série de uma maquina
    /// <Autor>Laerton Marques de Figueiredo</Autor>
    /// <Data>07/05/2016</Data>
    /// </summary>
    public class SerieMaquina
    {
        private Int64 _idOrcamento, _idOS, _idMaquina, _idTecnico;

        public SerieMaquina(Int64 idOrcamento, Int64 idMaquina)
        {
            _idOrcamento = idOrcamento;
            _idOS = 0;
            _idTecnico = 0;
            _idMaquina = idMaquina;
        }
        /// <summary>
        /// Id que identifica o número da maquiana montada para esse orçamento
        /// </summary>
        public long IdMaquina
        {
            get
            {
                return _idMaquina;
            }

            
        }
        /// <summary>
        /// Orçamento da qual a série pertence
        /// </summary>
        public long IdOrcamento
        {
            get
            {
                return _idOrcamento;
            }

            
        }
        /// <summary>
        /// Os a qual a maquina pertence
        /// </summary>
        public long IdOS
        {
            get
            {
                return _idOS;
            }
            set
            {
                _idOS = value;
            }
            
        }
        /// <summary>
        /// Técnico que efetuou o chekout 
        /// </summary>
        public long IdTecnico
        {
            get
            {
                return _idTecnico;
            }

            set
            {
                _idTecnico = value;
            }
        }
        /// <summary>
        /// Retorna o Número de série completo
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return _idOrcamento + "-"+ _idOS + "-" + _idMaquina;
        }
    }
}