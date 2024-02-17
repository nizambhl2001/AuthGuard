
using AuthGuad.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace AuthGuad.Helper
{
    public class HashPasswrod
    {
 
        private static RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
        private static readonly int saltsize = 16;
        private static readonly int hashsize = 20;
        private static readonly int iterations = 10000;
        public static string PasswrodHash(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[saltsize]);
            var key = new Rfc2898DeriveBytes( password,salt,iterations);
            var hash = key.GetBytes(hashsize);
            var hashByte = new byte[saltsize + hashsize];
            Array.Copy(salt, 0, hashByte, 0, saltsize);
            Array.Copy(hash, 0, hashByte, saltsize, hashsize);
            var base64Hash = Convert.ToBase64String(hashByte);
            return base64Hash;
        }

        public static bool VeryfyPassWord(string password, string base64Hash)
        {
            var hashByte = Convert.FromBase64String(base64Hash);
            var salt = new byte[saltsize];
            Array.Copy(hashByte, 0, salt, 0, saltsize);
            var key = new Rfc2898DeriveBytes(password, salt, iterations);
            byte[] hash = key.GetBytes(hashsize);
            for(var i=0;i<hashsize; i++)
            {
                if (hashByte[i + saltsize] != hash[i])
                return false;
            }
            return true;
        }

       
    }
}
