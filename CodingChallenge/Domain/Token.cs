namespace CodingChallenge.Domain
{
    public class Token
    {
        public string TokenId { get; set; }
        public virtual Wallet Wallet { get; set; }
        public string WalletAddress { get; set; }
    }
}
