namespace PLS.Shared.Results.Abstract;

public interface IDataResult<out T> : IResult
{
    public T Data { get; }
}