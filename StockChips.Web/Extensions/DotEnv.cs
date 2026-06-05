namespace StockChips.Web.Extensions;

public static class DotEnv
{
    public static void Load()
    {
        var candidates = new[]
        {
            Path.Combine(Directory.GetCurrentDirectory(), ".env"),
            Path.Combine(AppContext.BaseDirectory, ".env")
        };

        foreach (var path in candidates.Where(File.Exists))
        {
            foreach (var line in File.ReadAllLines(path))
            {
                var trimmed = line.Trim();
                if (string.IsNullOrWhiteSpace(trimmed) || trimmed.StartsWith('#'))
                {
                    continue;
                }

                var separatorIndex = trimmed.IndexOf('=');
                if (separatorIndex <= 0)
                {
                    continue;
                }

                var key = trimmed[..separatorIndex].Trim();
                var value = trimmed[(separatorIndex + 1)..].Trim().Trim('"');
                Environment.SetEnvironmentVariable(key, value);
            }
        }
    }
}
