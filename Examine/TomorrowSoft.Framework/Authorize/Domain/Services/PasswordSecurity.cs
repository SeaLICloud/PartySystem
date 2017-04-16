using System.Security.Cryptography;
using System.Text;
using TomorrowSoft.Authorize.Domain.Services;
using TomorrowSoft.Framework.Infrastructure.Crosscutting.Container;

namespace TomorrowSoft.Framework.Authorize.Domain.Services
{
    [RegisterToContainer]
    public class PasswordSecurity : IPasswordSecurity
    {
        private readonly int saltLength = 4;

        /// <summary>
        /// 生成加密后的密码
        /// </summary>
        /// <param name="password">密码原文</param>
        /// <returns>加密后的密码</returns>
        public byte[] CreateDbPassword(string password)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] unsaltedPassword = sha1.ComputeHash(Encoding.Unicode.GetBytes(password));
            return CreateDbPassword(unsaltedPassword);
        }

        // create salted password to save in Db
        private byte[] CreateDbPassword(byte[] unsaltedPassword)
        {
            //Create a salt value
            byte[] saltValue = new byte[saltLength];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            //用加密型强随机字节填充的数组
            rng.GetBytes(saltValue);

            return CreateSaltedPassword(saltValue, unsaltedPassword);
        }

        // create a salted password given the salt value
        private byte[] CreateSaltedPassword(byte[] saltValue, byte[] unsaltedPassword)
        {
            // add the salt to the hash
            byte[] rawSalted = new byte[unsaltedPassword.Length + saltValue.Length];

            // Copies all the elements of the current one-dimensional System.Array to the specified one-dimensional System.Array starting at the specified destination System.Array index.

            unsaltedPassword.CopyTo(rawSalted, 0);
            saltValue.CopyTo(rawSalted, unsaltedPassword.Length);

            //Create the salted hash			
            SHA1 sha1 = SHA1.Create();
            byte[] saltedPassword = sha1.ComputeHash(rawSalted);

            // add the salt value to the salted hash
            byte[] dbPassword = new byte[saltedPassword.Length + saltValue.Length];
            saltedPassword.CopyTo(dbPassword, 0);
            saltValue.CopyTo(dbPassword, saltedPassword.Length);

            return dbPassword;
        }

        // 对散列后的密码和数据库中存储的密码进行比较
        public bool ComparePasswords(byte[] storedPassword, string password)
        {
            SHA1 sha1 = SHA1.Create();
            byte[] hashedPassword = sha1.ComputeHash(Encoding.Unicode.GetBytes(password));

            if (storedPassword == null || hashedPassword == null || hashedPassword.Length != storedPassword.Length - saltLength)
                return false;

            // get the saved saltValue
            // 获取已保存在password数据字段中的Salt值
            byte[] saltValue = new byte[saltLength];
            int saltOffset = storedPassword.Length - saltLength;
            for (int i = 0; i < saltLength; i++)
                saltValue[i] = storedPassword[saltOffset + i];

            byte[] saltedPassword = CreateSaltedPassword(saltValue, hashedPassword);

            // compare the values
            return CompareByteArray(storedPassword, saltedPassword);
        }

        // 比较两个字节数组（逐字比较）
        private bool CompareByteArray(byte[] array1, byte[] array2)
        {
            if (array1.Length != array2.Length)
                return false;
            for (int i = 0; i < array1.Length; i++)
            {
                if (array1[i] != array2[i])
                    return false;
            }
            return true;
        }
    }
}