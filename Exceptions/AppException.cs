
namespace IPCheckUtil.Exceptions;

public class AppException : Exception
{
    public AppException(string message) : base(message) { }
}

public class FileLoadExceptionEx : AppException
{
    public FileLoadExceptionEx(string msg) : base(msg) { }
}

public class IpApiException : AppException
{
    public IpApiException(string msg) : base(msg) { }
}

public class AnalysisException : AppException
{
    public AnalysisException(string msg) : base(msg) { }
}
