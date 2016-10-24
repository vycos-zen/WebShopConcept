using MagicDay.BusinessLogic.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using MagicDay.DataModel;

namespace MagicDay.BusinessLogic.Authentication
{
    public sealed class LoginManager
    {
        public string HashPassword(string pwdEntered)
        {
            byte[] salt;
            using (RNGCryptoServiceProvider rngCryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                rngCryptoServiceProvider.GetBytes(salt = new byte[16]);
                var pbkdf2 = new Rfc2898DeriveBytes(pwdEntered, salt, 1000);
                byte[] hash = pbkdf2.GetBytes(20);
                byte[] hashedBytes = new byte[36];
                Array.Copy(salt, 0, hashedBytes, 0, 16);
                Array.Copy(hash, 0, hashedBytes, 16, 20);
                string savedPwdHash = Convert.ToBase64String(hashedBytes);
                return savedPwdHash;
            }
        }

        public static User UserLoginCheck(string userEmailEntered, string passwordEntered)
        {
            using (MagicDayEntities dataModel = new MagicDayEntities())
            {
                User matchingUser = (from user in dataModel.Users
                                     where user.EmailAddress == userEmailEntered
                                     select user).FirstOrDefault();
                if (matchingUser != null)
                {
                    bool isValid = false;
                    byte[] hashedBytes = Convert.FromBase64String(matchingUser.PasswordHash);
                    byte[] salt = new byte[16];
                    Array.Copy(hashedBytes, 0, salt, 0, 16);
                    var pbkdf2 = new Rfc2898DeriveBytes(passwordEntered, salt, 1000);
                    byte[] hash = pbkdf2.GetBytes(20);
                    for (int i = 0; i < 20; i++)
                    {
                        if (hashedBytes[i + 16] != hash[i])
                        {
                            isValid = false;
                            break;
                        }
                        else
                        {
                            isValid = true;
                        }
                    }

                    return (isValid) ? matchingUser : null;
                }
                return null;
            }
        }

        public static bool IsUserExists(string userEmailToCheck)
        {
            using (MagicDayEntities dataModel = new MagicDayEntities())
            {
                User mdUser = (from users in dataModel.Users
                               where users.EmailAddress == userEmailToCheck
                               select users).First();
                if (mdUser != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }
    }
}



