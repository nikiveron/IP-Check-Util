using IPCheckUtil.Exceptions;
using IPCheckUtil.Models;
using IPCheckUtil.Services;
using IPCheckUtil.Utils;

try
{
    // 1. Загрузите все ip адреса из файла в память 
    List<string> IPs = await FileReaderService.GetAllIps();

    // 2. Получение информации об IP
    List<IpData> ipDatas = await IpApiService.GetAllIpDatas(IPs);

    // 3. Статистика по странам
    Dictionary<string, int> сountries = AnalysisService.GetCountryStatistics(ipDatas);
    AnalysisService.ShowCountryStatistics(сountries);

    // 4. Города в стране с наибольшим кол-вом ip
    AnalysisService.ShowMaxFrequencyCountryCities(ipDatas, сountries);
}
catch (AppException ex)
{
    Console.Error.WriteLine($"Ошибка: {ex.Message}");
    Environment.Exit(2);
}
catch (HttpRequestException ex)
{
    Console.Error.WriteLine($"Ошибка при отправке запроса на {Constants.API_URL}: {ex.Message}");
    Environment.Exit(2);
}
catch (Exception ex)
{
    Console.Error.WriteLine("Неизвестная ошибка");
    Console.Error.WriteLine(ex.Message);
    Environment.Exit(1);
}

Environment.Exit(0);