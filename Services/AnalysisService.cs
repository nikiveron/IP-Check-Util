
using IPCheckUtil.Exceptions;
using IPCheckUtil.Models;
using System.Xml.Linq;

namespace IPCheckUtil.Services;

public class AnalysisService
{
    public static Dictionary<string, int> GetCountryStatistics(List<IpData> ipDatas)
    {
        if (ipDatas.Count == 0) throw new AnalysisException("Нет данных для анализа");

        Dictionary<string, int> сountries = [];
        foreach (var ipData in ipDatas)
        {
            string ipCountry = ipData.Country;
            if (сountries.TryGetValue(ipCountry, out _))
            {
                сountries[ipCountry]++;
            }
            else if (ipCountry != string.Empty)
            {
                сountries.Add(ipCountry, 1);
            }
        }

        return сountries;
    }

    public static void ShowCountryStatistics(Dictionary<string, int> сountries)
    {
        if (сountries.Count == 0) throw new AnalysisException("Статистика по странам отсутствует. Ни в одном IP не указана страна.");

        Console.WriteLine("---СТАТИСТИКА ПО СТРАНАМ---");
        foreach (var сountry in сountries)
        {
            Console.Write($"{сountry.Key} - {сountry.Value}; ");
        }
        Console.WriteLine("\n");
    }

    public static void ShowMaxFrequencyCountryCities(List<IpData> ipDatas, Dictionary<string, int> сountries)
    {
        int maxValue = сountries.Values.Max();
        var maxCountry = сountries.First(kv => kv.Value == maxValue).Key;
        Console.WriteLine($"---ГОРОДА В СТРАНЕ {maxCountry}---");

        HashSet<string> cities = [];
        foreach (var ipData in ipDatas)
        {
            if (ipData.Country == maxCountry)
            {
                cities.Add(ipData.City);
            }
        }

        foreach (var city in cities)
        {
            Console.Write($"{city}; ");
        }
        Console.WriteLine();
    }
}
