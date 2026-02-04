using IPCheckUtil.Models;
using IPCheckUtil.Services;

// 1. Загрузите все ip адреса из файла в память 
List<string> IPs = [];
await foreach (var ip in FileReaderService.ReadLinesAsync())
{
    IPs.Add(ip);
}

// 2. Получение информации об IP
List<IpData> ipDatas = await IpApiService.GetAllIpDatas(IPs);

// 3. Статистика по странам
Dictionary<string, int> сountries = AnalysisService.GetCountryStatistics(ipDatas);
AnalysisService.ShowCountryStatistics(сountries);

// 4. Города в стране с наибольшим кол-вом ip
AnalysisService.ShowMaxFrequencyCountryCities(ipDatas, сountries);