using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Microsoft.IdentityModel.Tokens;

namespace Common.Configs;
public class AuthOptions
{
    public string Issuer { get; set; }

    public string Audience { get; set; }

    public string PublicKeyString { get; set; }

    public string PrivateKeyString { get; set; }

    public int TokenLifetime { get; set; }

    public int RefreshTokenLifetime { get; set; }

    private static RsaSecurityKey _publicKey;
    public RsaSecurityKey PublicKey
    {
        get
        {
            if(string.IsNullOrEmpty(PublicKeyString))
                return null;

            if(_publicKey is null)
            {
                var key = RSA.Create();
                key.ImportSubjectPublicKeyInfo(
                    source: Convert.FromBase64String(PublicKeyString),
                    bytesRead: out int _
                );

                _publicKey = new RsaSecurityKey(key);
            }

            return _publicKey;
        }
    }

    private static RsaSecurityKey _privateKey;
    public RsaSecurityKey PrivateKey
    {
        get
        {
            if(string.IsNullOrEmpty(PrivateKeyString))
                return null;

            if(_privateKey is null)
            {
                var key = RSA.Create();
                key.ImportRSAPrivateKey(
                    source: Convert.FromBase64String(PrivateKeyString),
                    bytesRead: out int _
                );

                _privateKey = new RsaSecurityKey(key);
            }

            return _privateKey;
        }
    }
}
