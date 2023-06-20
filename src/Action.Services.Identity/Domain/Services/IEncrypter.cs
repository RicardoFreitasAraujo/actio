namespace Actio.Services.Identity.Domain.Services
{
    public interface IEncrypter
    {
        string GetSalt(string value);
        string Gethash(string value, string salt);
    }
}
