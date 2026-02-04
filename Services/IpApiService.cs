using Newtonsoft.Json;
using IPCheckUtil.Models;
using IPCheckUtil.Utils;

namespace IPCheckUtil.Services;

public class IpApiService
{
    public static async Task<IpData> GetIpData(string ip)
    {
        var response = await Constants.httpClient.GetAsync($"{Constants.API_URL}/{ip}/json");

        response.EnsureSuccessStatusCode();

        var json = await response.Content.ReadAsStringAsync();
        
        IpData data = JsonConvert.DeserializeObject<IpData>(json) ?? throw new Exception("Не удалось десериализовать IpData");
        return data;
    }
}
