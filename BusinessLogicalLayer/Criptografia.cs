using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PWDTK_DOTNET451;

namespace BusinessLogicalLayer
{
    public sealed class Criptografia
    {
        public static byte[] Encriptar(string senha, out byte[] salt)
        {
            //Gerar salt random
            salt = PWDTK.GetRandomSalt();

            //Usar salt e senha para hashear
            return PWDTK.PasswordToHash(salt, senha);
        }

        public static bool Verificar(string senha, byte[] salt, byte[] hashedSenha)
        {
            return PWDTK.ComparePasswordToHash(
                salt: salt,
                password: senha,
                hash: hashedSenha);
        }
    }
}
