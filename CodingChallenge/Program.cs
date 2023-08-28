using CodingChallenge.Application;
using CodingChallenge.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CodingChallenge
{
    public class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Arguments not provided.");
                return;
            }

            var context = new DataContext();
            context.Database.Migrate();


            List<CommandArguments> commandArgs = new List<CommandArguments>();
            var commandResolver = new CommandResolver(context);

            try
            {
                if (args[0] == "--reset")
                {
                    commandResolver.ResetCommand();
                    return;
                }
                if (args.Length < 2)
                {
                    Console.WriteLine("Invalid argument or arguments combination.");
                    return;
                }
                switch (args[0])
                {

                    case "--read-inline":
                        var jsonArgument = string.Join("", args[1..]);
                        commandArgs = CommandArgumentsReader.ReadInline(jsonArgument);
                        Console.WriteLine($"Read {commandArgs?.Count} transactions (s)");
                        break;
                    case "--read-file":
                        commandArgs = CommandArgumentsReader.ReadFromFile(args[1]);
                        Console.WriteLine($"Read {commandArgs?.Count} transactions (s)");
                        break;
                    case "--nft":
                        commandResolver.NftCommand(args[1]);
                        break;
                    case "--wallet":
                        commandResolver.WalletCommand(args[1]);
                        break;
                    default:
                        Console.WriteLine($"Command {args[0]} not reconized.");
                        break;
                }

                foreach (var command in commandArgs)
                {
                    switch (command.Type)
                    {
                        case "Mint":
                            commandResolver.Mint(command);
                            break;
                        case "Burn":
                            commandResolver.Burn(command);
                            break;
                        case "Transfer":
                            commandResolver.Transfer(command);
                            break;
                        default:
                            Console.WriteLine($"Command {command} not recognized.");
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"There was a problem processing the command: {ex.Message}");
            }
        }
    }
}