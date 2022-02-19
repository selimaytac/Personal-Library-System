using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface ISourceService
{
    Task<IDataResult<SourceDto>> GetAsync(int sourceId);
    Task<IDataResult<SourceListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true);
    Task<IDataResult<SourceListDto>> GetAllByCategoryAsync(int categoryId,bool isDeleted = false, bool isActive = true);
    Task<IDataResult<SourceListDto>> GetAllByTagsAsync(int[] tagIds, bool isDeleted = false, bool isActive = true);
    Task<IDataResult<int>> GetSourceCount(bool isDeleted = false, bool isActive = true);
    Task<IDataResult<Source>> AddAsync(SourceAddDto sourceAddDto, string addedByUser);
    Task<IResult> UpdateAsync(SourceUpdateDto sourceUpdateDto, string updatedByUser, string userRole);
    Task<IResult> DeleteAsync(int sourceId, string deletedByUser, string userRole);
    Task<IResult> RestoreDeletedAsync(int sourceId, string restoredByUser, string userRole);
    
    Task<IResult> HardDeleteAsync(int sourceId);
}