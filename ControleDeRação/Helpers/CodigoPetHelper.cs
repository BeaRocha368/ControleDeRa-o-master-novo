using System;
using System.Text;

namespace ControleDeRacao.Helpers // Ajuste o namespace se você criar uma pasta Helpers
{
    public static class CodigoPetHelper
    {
        private static Random random = new Random();

        // Caracteres que serão usados no código (letras maiúsculas e números)
        private const string Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

        /// <summary>
        /// Gera um código alfanumérico curto e aleatório.
        /// </summary>
        /// <param name="length">O tamanho do código (ex: 6 para 6 caracteres).</param>
        /// <returns>O código gerado.</returns>
        public static string GerarCodigoCurto(int length = 6)
        {
            var codigo = new char[length];

            for (int i = 0; i < length; i++)
            {
                // Seleciona um caractere aleatório da lista Chars
                codigo[i] = Chars[random.Next(Chars.Length)];
            }

            return new string(codigo);
        }
    }
}