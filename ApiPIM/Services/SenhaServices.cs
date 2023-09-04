using System.Security.Cryptography;
using System.Text;

namespace ApiPIM.Services
{
    public class SenhaServices
    {
        private static readonly string letras = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private static readonly string numeros = "1234567890";
        private static readonly string especiais = "!@#$%^&*()_-+=[{]}|;:,<.>/?";
        private static readonly int tamanho = 8;

        public string Gerar()
        {
            var senha = new char[tamanho];
            var rng = new RNGCryptoServiceProvider();

            for(var i =0; i < tamanho; i++)
            {
                switch(i % 4)
                {
                    case 0:
                        senha[i] = CharAleatorio(letras.ToUpper(), rng);
                        break;
                    case 1:
                        senha[i] = CharAleatorio(letras.ToLower(), rng);
                        break;
                    case 2:
                        senha[i] = CharAleatorio(numeros, rng);
                        break;
                    case 3:
                        senha[i] = CharAleatorio(especiais, rng);
                        break;
                }
            }
            return new string(senha);
        }

        private static char CharAleatorio(string charValidos, RNGCryptoServiceProvider rng)
        {
            var max = byte.MaxValue - (byte.MaxValue % charValidos.Length);

            byte[] numeroAleatorio = new byte[1];
            do
            {
                rng.GetBytes(numeroAleatorio);
            }
            while (numeroAleatorio[0] >= max);

            return charValidos[numeroAleatorio[0] % charValidos.Length];
        }

        public string ComputeHash(string input)
        {
            var Algorithm = new SHA256CryptoServiceProvider();
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashedBytes = Algorithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashedBytes);
        }
    }
}
