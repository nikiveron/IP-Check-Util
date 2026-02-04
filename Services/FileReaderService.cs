using IPCheckUtil.Exceptions;
using IPCheckUtil.Utils;
using System.Net.Http;

namespace IPCheckUtil.Services;

public class FileReaderService
{
    public static async IAsyncEnumerable<string> ReadLinesAsync()
    {
        using var response = await Constants.httpClient.GetAsync(Constants.TXT_URL, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            yield return line;
        }
    }

    public static async Task<List<string>> GetAllIps()
    {
        List<string> IPs = [];
        await foreach (var ip in FileReaderService.ReadLinesAsync())
        {
            IPs.Add(ip);
        }

        if (IPs.Count == 0) throw new FileLoadExceptionEx("Файл с IP пуст");

        return IPs;
    }
}
