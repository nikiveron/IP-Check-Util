
using IPCheckUtil.Models;

namespace IPCheckUtil.Services;

public class AnalysisService
{
    public static Dictionary<string, int> GetCountryStatistics(List<IpData> ipDatas)
    {
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

        return сountries;
    }

    public static void ShowCountryStatistics(Dictionary<string, int> сountries)
    {
        Console.WriteLine("---СТАТИСТИКА ПО СТРАНАМ---");
        foreach (var сountry in сountries)
        {
            Console.WriteLine($"{сountry.Key} - {сountry.Value}");
        }
        Console.WriteLine();
    }

    public static void ShowMaxFrequencyCountryCities(List<IpData> ipDatas, Dictionary<string, int> сountries)
    {
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
        Console.WriteLine();
    }
}
