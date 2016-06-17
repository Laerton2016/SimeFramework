using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Net;
using System.IO;

namespace SIME.Class
{

    public  class UteisWeb
    {
        private Color corPadrao = Color.Cyan;
        public UteisWeb()
        {
        }
     
                       

        public String montaTab(List<String[]> dados, String rotulo, System.Drawing.Color cor)
        {
            String retorno = "";
            String cabeca = "<span style='font-size: medium'>" + rotulo + "</span>";
            String inicioTabela = "<table style='width:100%;'>";
            String fimTabela = "</table>";
            String inicioLinha = "<tr>";
            String fimLinha = "</tr>";
            String[] linha;

            retorno = cabeca + inicioTabela;
            for (int i = 0; i < dados.Count; i++)
            {
                retorno += inicioLinha;
                linha = dados[i];
                for (int j = 0; j < linha.Count(); j++)
                {
                    if (i % 2 != 0) // Verifica se a linha é colorida ou branca
                    {

                        retorno += montaCelula(linha[j], false, EnunAlinhamentos.RIGHT, Color.White);
                    }
                    else
                    {
                        retorno += montaCelula(linha[j], false, EnunAlinhamentos.RIGHT, cor);

                    }
                }
                retorno += fimLinha;
            }

            retorno += fimTabela;
            return retorno;
        }

        private String[] extraiDados(String linha)
        {
            return linha.Split(',');
        }

        private String montaCelula(String texto, Boolean negrito, EnunAlinhamentos alinhamento, System.Drawing.Color cor)
        {
            String retorno = "<td align='" + alinhamento.ToString() + "'" +
                "bgcolor='" + cor.ToKnownColor() + "'" + " >" +
                ((negrito) ? "<B>" : "") + texto + ((negrito) ? "</B>" : "") + "</td>";
            return retorno;
        }

        private String montaCelula(String texto)
        {
            return montaCelula(texto, false, EnunAlinhamentos.LEFT, corPadrao);
        }

        public Image consultaCNPJ(String cnpj) {
            Image imagem = null;
            WebClient clienteWeb = new WebClient();
            String urlCaptcha = "http://www.nfe.fazenda.gov.br/scripts/srf/intercepta/captcha.aspx?opt=image";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlCaptcha);
            request.ContentType = "application/x-www-form-urlencoded";
            request.CookieContainer = new CookieContainer();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            foreach (Cookie cookieCaptcha in response.Cookies) {
                if (cookieCaptcha.Name == "cookieCaptcha") {
                    
                    Stream stream = response.GetResponseStream();
                    imagem = Image.FromStream(stream);

                }
            }
            return imagem;
            
            

        }
    }

}