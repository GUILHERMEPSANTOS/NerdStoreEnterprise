namespace NSE.Pagamento.NerdsPag
{
    public class NerdsPagService
    {
        public readonly string ApiKey;
        public readonly string EncryptionKey;

        public NerdsPagService(string apiKey, string encryptionKey)
        {
            ApiKey = apiKey;
            EncryptionKey = encryptionKey;
        }
    }
}
