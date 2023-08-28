namespace CodingChallenge.Domain
{
    public class Wallet
    {
        public string Address { get; set; }
        public ICollection<Token> Tokens { get; set; }
    }
}
