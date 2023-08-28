using CodingChallenge.Domain;
using CodingChallenge.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge.Application
{
    public class CommandResolver
    {
        private DataContext _context { get; set; }

        public CommandResolver(DataContext context)
        {
            _context = context;
        }
        public void NftCommand(string tokenId)
        {
            var tokenFromDb = _context.Tokens.Find(tokenId);
            if (tokenFromDb != null)
            {
                Console.WriteLine($"Token {tokenFromDb.TokenId} is owned by {tokenFromDb.WalletAddress}");
            }
            else
            {
                Console.WriteLine($"Token {tokenId} is not owned by any wallet.");
            }
        }

        public void WalletCommand(string walletAddress)
        {
            var walletFromDb = _context.Wallets.Include(w => w.Tokens).SingleOrDefault(w => w.Address == walletAddress);
            if (walletFromDb != null)
            {
                var tokensCount = walletFromDb.Tokens.Count;
                if (tokensCount > 0)
                {
                    Console.WriteLine($"Wallet {walletFromDb.Address} holds {tokensCount} Tokens:");
                    foreach (var token in walletFromDb.Tokens)
                    {
                        Console.WriteLine(token.TokenId);
                    }
                    return;
                }

            }
            Console.WriteLine($"Wallet {walletAddress} holds no Tokens");
        }

        public void ResetCommand()
        {
            _context.Tokens.ExecuteDelete();
            _context.Wallets.ExecuteDelete();
            _context.SaveChanges();

            Console.WriteLine("Program was reset");
        }

        public void Mint(CommandArguments commandArguments)
        {
            var walletFromDb = _context.Wallets.Find(commandArguments.Address);
            if (walletFromDb == null)
            {
                _context.Wallets.Add(new Wallet { Address = commandArguments.Address });
            }

            _context.Tokens.Add(new Token
            {
                TokenId = commandArguments.TokenId,
                WalletAddress = commandArguments.Address
            });

            _context.SaveChanges();
        }

        public void Burn(CommandArguments commandArguments)
        {
            var token = _context.Tokens.Find(commandArguments.TokenId);
            if (token != null)
            {
                _context.Tokens.Remove(token);
                _context.SaveChanges();
            }
        }

        public void Transfer(CommandArguments commandArguments)
        {
            var wallletFromDb = _context.Wallets.Include(w => w.Tokens).SingleOrDefault(w => w.Address == commandArguments.From);
            if (wallletFromDb == null)
            {
                Console.WriteLine($"Wallet address {commandArguments.From} does not exist.");
                return;
            }

            var tokenFromDb = wallletFromDb.Tokens.SingleOrDefault(t => t.TokenId == commandArguments.TokenId);
            if (tokenFromDb == null)
            {
                Console.WriteLine($"Token {commandArguments.TokenId} does not exist or does not belong to wallet {wallletFromDb.Address}");
                return;
            }

            var walletToTransfer = _context.Wallets.Find(commandArguments.To);
            if (walletToTransfer == null)
            {
                _context.Wallets.Add(new Wallet { Address = commandArguments.To });
            }

            tokenFromDb.WalletAddress = commandArguments.To;

            _context.SaveChanges();
        }
    }
}
