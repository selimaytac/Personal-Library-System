using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface ISourceService
{
    Task<IDataResult<SourceDto>> GetAsync(int sourceId);
    Task<IDataResult<SourceListDto>> GetAllAsync();
    Task<IDataResult<SourceListDto>> GetAllByNonDeletedAsync();
    Task<IDataResult<SourceListDto>> GetAllByNonDeletedAndActiveAsync();
    Task<IDataResult<SourceListDto>> GetAllByCategoryAsync(int categoryId);
    Task<IDataResult<SourceListDto>> GetAllByTagsAsync(int[]? tagIds);
    Task<IDataResult<Source>> AddAsync(SourceAddDto sourceAddDto, string addedByUser);
    Task<IResult> UpdateAsync(SourceUpdateDto sourceUpdateDto, string updatedByUser);
    Task<IResult> DeleteAsync(int sourceId, string deletedByUser);
    Task<IResult> RestoreDeletedAsync(int sourceId, string restoredByUser);
    Task<IResult> HardDeleteAsync(int sourceId);
}