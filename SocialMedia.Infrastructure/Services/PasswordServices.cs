using Microsoft.Extensions.Options;
using SocialMedia.Infrastructure.Interfaces;
using SocialMedia.Infrastructure.Options;
using System;
using System.Linq;
using System.Security.Cryptography;

namespace SocialMedia.Infrastructure.Services
{
    public class PasswordServices : IPasswordService
    {
        private readonly PasswordOptions _options;

        public PasswordServices(IOptions<PasswordOptions> options) 
        {
            this._options = options.Value;
        }
        public bool Check(string hash, string password)
        {
            var parts = hash.Split('.');
            if (parts.Length != 3)
            {
                throw new FormatException("Unexperted hash format");
            }

            var interations = Convert.ToInt32(parts[0]);
            var salt= Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);


            using (var algoritm = new Rfc2898DeriveBytes(
                password,
                salt,
                interations
                ))
            {
                var keyToCheck = algoritm.GetBytes(_options.KeySize);
                return keyToCheck.SequenceEqual(key);


            };
        }

        public string Hash(string password)
        {
            //Rfc2898DeriveBytes REVISAR ESTA DOCUMENTACION

            using (var algoritm = new Rfc2898DeriveBytes(
                password,
                _options.SaltSize,
                _options.Iterations
                ))
            {
                var key = Convert.ToBase64String(algoritm.GetBytes(_options.KeySize));
                var salt = Convert.ToBase64String(algoritm.Salt);

                return $"{_options.Iterations}.{salt}.{key}";

            };
        }
    }
}
