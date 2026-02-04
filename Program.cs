using IPCheckUtil.Models;
using IPCheckUtil.Services;

var fileReader = new FileReaderService();

// 1. Загрузите все ip адреса из файла в память 
List<string> IPs = [];
await foreach (var ip in fileReader.ReadLinesAsync())
{
    IPs.Add(ip);
}

// 2. Получение информации об IP
List<IpData> ipDatas = [];
foreach (var ip in IPs)
{
    ipDatas.Add(await IpApiService.GetIpData(ip));
}

// 3. Статистика по странам
Dictionary<string, int> сountries = [];
foreach (var ipData in ipDatas)
{
    string ipCountry = ipData.Country;
    if (сountries.TryGetValue(ipCountry, out _))
    {
        сountries[ipCountry]++;
    }
    else
    {
        сountries.Add(ipCountry, 1);
    }
}

Console.WriteLine("---СТАТИСТИКА ПО СТРАНАМ---");
foreach (var сountry in сountries)
{
    Console.WriteLine($"{сountry.Key} - {сountry.Value}");
}

// 4. Города в стране с наибольшим кол-вом ip
int maxValue = сountries.Values.Max();
var maxCountry = сountries.First(kv => kv.Value == maxValue).Key;
Console.WriteLine($"---ГОРОДА В СТРАНЕ {maxCountry}---");
foreach (var ipData in ipDatas)
{
    if (ipData.Country == maxCountry)
    {
        Console.Write($"{ipData.City}; ");
    }
}