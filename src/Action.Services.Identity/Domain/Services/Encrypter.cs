﻿using Amazon.Runtime.Internal.Transform;
using System;
using System.Security.Cryptography;

namespace Actio.Services.Identity.Domain.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int SaltSize = 128;
        private static readonly int DeriveBytesIterationsCount = 10000;

        public string GetSalt(string value)
        {
            var random = new Random();
            var saltBytes = new Byte[SaltSize];
            var rng = RandomNumberGenerator.Create();
            rng.GetBytes(saltBytes);
            return Convert.ToBase64String(saltBytes);
        }
        
        public string Gethash(string value, string salt)
        {
            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);
            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length * sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        
    }
}
