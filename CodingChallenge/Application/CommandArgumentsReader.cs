using Newtonsoft.Json;

namespace CodingChallenge.Application
{
    public static class CommandArgumentsReader
    {
        public static List<CommandArguments> commandArgs = new();

        public static List<CommandArguments> ReadInline(string jsonToCompute)
        {
            var argumentString = string.Join("", jsonToCompute);
            argumentString = JsonFormater.FormatInputStringToJson(argumentString);
            commandArgs = JsonConvert.DeserializeObject<List<CommandArguments>>(argumentString);

            return commandArgs;
        }

        public static List<CommandArguments> ReadFromFile(string jsonFileName)
        {
            var workingDirectory = Environment.CurrentDirectory;
            var inlineFilePath = Path.Combine(workingDirectory, jsonFileName);
            if (File.Exists(inlineFilePath))
            {
                var argumentString = File.ReadAllText(inlineFilePath);
                argumentString = JsonFormater.FormatFileInputStringToJson(argumentString);
                commandArgs = JsonConvert.DeserializeObject<List<CommandArguments>>(argumentString);
            }

            return commandArgs;
        }
    }
}
