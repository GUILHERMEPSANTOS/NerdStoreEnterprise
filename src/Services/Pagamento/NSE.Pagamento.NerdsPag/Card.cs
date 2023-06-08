
using System.Security.Cryptography;
using System.Text;

namespace NSE.Pagamento.NerdsPag
{
    public class CardHash
    {
        private readonly NerdsPagService _nerdsPagService;
        public string CardHolderName { get; set; }
        public string CardNumber { get; set; }
        public string CardExpirationDate { get; set; }
        public string CardCvv { get; set; }
        public CardHash(NerdsPagService nerdspagService)
        {
            _nerdsPagService = nerdspagService;
        }

        public string Generate()
        {
            using var aesAlg = Aes.Create();

            aesAlg.IV = Encoding.UTF8.GetBytes(_nerdsPagService.EncryptionKey);
            aesAlg.Key = Encoding.UTF8.GetBytes(_nerdsPagService.ApiKey);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);

            csEncrypt.Write(Encoding.UTF8.GetBytes(CardHolderName + CardNumber + CardExpirationDate + CardCvv));

            var encryptedData = msEncrypt.ToArray();

            return Encoding.UTF8.GetString(encryptedData);
        }
    }
}