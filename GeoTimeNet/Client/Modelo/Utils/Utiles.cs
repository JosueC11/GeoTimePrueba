using BlazorBootstrap;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Reflection.Metadata.Ecma335;

namespace GeotimeNet.Client.Modelo.Utils
{
    public class Utiles
    {
        public static async Task SetFocus(InputText txt)
        {
            if (txt == null) return;

            if (txt.Element.HasValue)
            {
                await txt.Element.Value.FocusAsync();
            }
        }

        public static async Task SetFocus(InputNumber<decimal> txt)
        {
            if (txt == null) return;

            if (txt.Element.HasValue)
            {
                await txt.Element.Value.FocusAsync();
            }
        }

        public static string GetTipoMarca(int tipo)
        {
            string strTipo="Indefinido";
            switch (tipo)
            {
                case 1: strTipo = "Entrada"; break;
                case 2: strTipo = "Salida"; break;
                case 3: strTipo = "Descanso"; break;

            }
            return strTipo;
        }

        /// <summary>
        /// Get substring of specified number of characters on the right.
        /// </summary>
        public static string Right(string value, int length)
        {
            if (string.IsNullOrEmpty(value)) return string.Empty;

            return value.Length <= length ? value : value.Substring(value.Length - length);
        }
    }
}
