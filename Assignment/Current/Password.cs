using System; 
using System.Collections.Generic; 
using System.Security.Cryptography;
using System.Text; 

namespace CAB201
{
    class Password
    {
        private const int pwdIteration = 50;
        private const int pwdLength = 32;

        private byte[] pwdSalt;
        private byte[] pwdHash;

        public Password(string userPwd)
        {
            Rfc2898DeriveBytes userOwnPwd = new Rfc2898DeriveBytes(userPwd, 8, pwdIteration);
            byte[] hashedPwd = userOwnPwd.GetBytes(pwdLength);
            byte[] saltedPwd = userOwnPwd.Salt;
            pwdHash = hashedPwd;
            pwdSalt = saltedPwd;
        }

        public bool pwdCheck(string userPwdInput)
        {
            Rfc2898DeriveBytes userOwnPwd = new Rfc2898DeriveBytes(userPwdInput, pwdSalt, pwdIteration);
            byte[] hashedPwdInput = userOwnPwd.GetBytes(pwdLength);
            bool matchPwd = hashCheck(hashedPwdInput);
            return matchPwd; 
        }

        private bool hashCheck(byte[] hash)
        {
            int hashCheckLength = pwdHash.Length;

            if (hashCheckLength != hash.Length)
            {
                return false; 
            }

            for (int i = 0; i < hashCheckLength; i++)
            {
                if (pwdHash[i] != hash[i])
                {
                    return false; 
                }
            }
            return true; 
        }
    }
}