using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;

namespace PLS.Shared.Results.Concrete;

public class DataResult<T> : IDataResult<T>
{
    public DataResult(ResultStatus resultStatus, T data)
    {
        ResultStatus = resultStatus;
        Data = data;
    }
    public DataResult(ResultStatus resultStatus, string message, T data)
    {
        ResultStatus = resultStatus;
        Data = data;
        Message = message;
    }
    public DataResult(ResultStatus resultStatus, string message, T data, Exception exception)
    {
        ResultStatus = resultStatus;
        Data = data;
        Message = message;
        Exception = exception;
    }

    public T Data { get; }

    public ResultStatus ResultStatus { get; }

    public string Message { get; }

    public Exception Exception { get; }
}