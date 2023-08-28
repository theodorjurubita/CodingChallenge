namespace CodingChallenge.Application
{
    public static class JsonFormater
    {
        public static string FormatInputStringToJson(string jsonUnformated)
        {
            var jsonFormated = jsonUnformated
                .Replace("’", "'")
                .Replace("'{", "'[{")
                .Replace("}'", "}]'")
                .Replace("{", "{\"")
                .Replace(":", "\":\"")
                .Replace(",", "\",\"")
                .Replace("}", "\"}")
                .Replace("'", "")
                .Replace("}\",\"{", "},{");

            return jsonFormated;
        }

        public static string FormatFileInputStringToJson(string jsonUnformated)
        {
            var jsonFormated = jsonUnformated
                .Replace("'", "")
                .Replace("'{", "'[{")
                .Replace("}'", "}]'");

            return jsonFormated;
        }
    }
}
