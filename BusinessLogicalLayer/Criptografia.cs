using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entity;
using PWDTK_DOTNET451;

namespace BusinessLogicalLayer
{
    public sealed class Criptografia
    {
        /// <summary>
        /// Gera um salt aleatório e usa para encriptografar a <paramref name="senha"/>.
        /// </summary>
        /// <param name="senha">Senha a ser encriptografada.</param>
        /// <param name="salt">Salt que é gerado. Deve-se guardá-lo no banco.</param>
        /// <returns>O hash gerado</returns>
        public static byte[] Encriptar(string senha, out byte[] salt)
        {
            salt = PWDTK.GetRandomSalt();
            return PWDTK.PasswordToHash(salt, senha);
        }

        /// <summary>
        /// Verifica se uma senha informada é a senha encriptografada armazenada no banco.
        /// </summary>
        /// <param name="senha">A senha usada na tentativa de login</param>
        /// <param name="salt">O salt recuperado do banco para o usuario</param>
        /// <param name="hashedSenha">O hash recuperado do banco para o usuario</param>
        /// <returns>Se a senha é a mesma</returns>
        public static bool Verificar(string senha, byte[] salt, byte[] hashedSenha)
        {
            return PWDTK.ComparePasswordToHash(
                salt: salt,
                password: senha,
                hash: hashedSenha);
        }

        /// <summary>
        /// Encriptografa senha de um Usuario, armazenando no objeto o hash e salt
        /// </summary>
        /// <param name="item"></param>
        public static void EncriptografarEGuardarSalt(Usuario item)
        {
            byte[] salt;
            item.Hash = Criptografia.Encriptar(item.Senha, out salt);
            item.Salt = salt;
        }
    }
}
