using PLS.Shared.Results.ComplexTypes;

namespace PLS.Shared.Entities.Abstract;

public class DtoGetBase
{
    public virtual ResultStatus ResultStatus { get; set; }
    public virtual string Message { get; set; }
}