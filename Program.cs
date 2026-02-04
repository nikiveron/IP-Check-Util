using IPCheckUtil.Services;

var httpClient = new HttpClient();
var fileReader = new FileReaderService(httpClient);


List<string> IPs = [];

await foreach (var ip in fileReader.ReadLinesAsync())
{
    IPs.Add(ip);
    Console.WriteLine(ip);
}