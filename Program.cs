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