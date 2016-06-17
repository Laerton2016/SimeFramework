using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SIME.Class
{
    public class Usuario
    {
        private Int16 cod, tipo;
        private String nome, senha;

        public Usuario(Int16 cod, String nome, String senha, Int16 tipo) {
            this.cod = cod;
            this.tipo = tipo;
            this.nome = nome;
            this.senha = senha;
        }

        /// <summary>
        /// Método que compara se a senha informada confere
        /// </summary>
        /// <param name="senha">Recebe String contendo a senha</param>
        /// <returns>Retorna boolean </returns>
        public Boolean validaSenha(String senha) {
            return this.senha.Equals(senha);
        }

        public Int16 getTipo() {
            return tipo;
        }

        public Int16 getCod() {
            return cod;
        }

        public String getNome() {
            return nome;
        }

        public String ToString() {
            return Convert.ToString(cod) + "," + nome + "," +  senha + "," + Convert.ToString(tipo);
        }

    }
}