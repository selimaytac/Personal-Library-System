using PLS.Shared.Results.ComplexTypes;

namespace PLS.Shared.Results.Abstract;

public interface IResult
{
    public ResultStatus ResultStatus { get; }
    public string Message { get; }
    public Exception Exception { get; }
}