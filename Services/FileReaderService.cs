using IPCheckUtil.Utils;

namespace IPCheckUtil.Services;

public class FileReaderService
{
    public FileReaderService(HttpClient http)
    {
        _http = http;
        _http.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/144.0.0.0 Safari/537.36 Edg/144.0.0.0");
    }

    private readonly HttpClient _http;

    public async IAsyncEnumerable<string> ReadLinesAsync()
    {
        using var response = await _http.GetAsync(Constants.TXT_URL, HttpCompletionOption.ResponseHeadersRead);
        response.EnsureSuccessStatusCode();

        await using var stream = await response.Content.ReadAsStreamAsync();
        using var reader = new StreamReader(stream);

        string? line;
        while ((line = await reader.ReadLineAsync()) != null)
        {
            yield return line;
        }
    }
}
