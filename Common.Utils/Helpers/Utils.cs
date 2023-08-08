using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Common.Utils.Helpers
{
    public static class Utils
    {
        public static bool ValidateEmail(string email)
        {
            bool result = false;
            string expresion = "^[a-zA-Z0-9_\\.-]+@([a-zA-Z0-9-]+\\.)+[a-zA-Z]{2,6}$";
            if (Regex.IsMatch(email, expresion))
            {
                if (Regex.Replace(email, expresion, String.Empty).Length == 0)
                    result = true;
                else
                    result = false;
            }
            else
                result = false;
            return result;
        }

        public static double ConvertToUnixTimestamp(DateTime date)
        {
            DateTime origin = DateTime.UtcNow;
            TimeSpan diff = date.ToUniversalTime() - origin;
            return Math.Floor(diff.TotalSeconds);
        }



        /// <summary>
        /// Method to get value claim from JwtToken
        /// </summary>
        /// <param name="authorization"> Request.Headers["Authorization"] </param>
        /// <param name="claim"></param>
        /// <returns></returns>
        public static string GetClaimValue(string token, string claim) // Claim = id usuario, roles...
        {
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            string authHeader = token.Replace("Bearer ", "").Replace("bearer ", "");
            JwtSecurityToken tokenS = handler.ReadToken(authHeader) as JwtSecurityToken;

            Claim claimData = tokenS.Claims.FirstOrDefault(cl => cl.Type.ToUpper() == claim.ToUpper());

            if (claimData == null || string.IsNullOrEmpty(claimData.Value))
                throw new UnauthorizedAccessException();

            return claimData.Value;
        }

        /// <summary>
        /// Encriptar las contraseñas de los usuarios
        /// </summary>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Encrypt(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();

            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int i = 0; i < stream.Length; i++)
            {
                sb.AppendFormat("{0:x2}", stream[i]);
            }
            return sb.ToString();
        }

    }
}
