using IPCheckUtil.Exceptions;
using IPCheckUtil.Models;
using IPCheckUtil.Utils;
using Newtonsoft.Json;
using System.Net;

namespace IPCheckUtil.Services;

public class IpApiService
{
    public static async Task<IpData> GetIpData(string ip)
    {
        var response = await Constants.httpClient.GetAsync($"{Constants.API_URL}/{ip}/json");

        if (response.StatusCode == HttpStatusCode.TooManyRequests) throw new IpApiException("Превышен лимит ipinfo.io");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();

        IpData data = JsonConvert.DeserializeObject<IpData>(json) ?? throw new IpApiException("Не удалось десериализовать IpData");
        return data;
    }

    public static async Task<List<IpData>> GetAllIpDatas(List<string> IPs)
    {
        var tasks = IPs.Select(GetIpData);
        var ipDatas = await Task.WhenAll(tasks);

        return [.. ipDatas];
    }
}
