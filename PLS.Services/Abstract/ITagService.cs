using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Shared.Results.Abstract;

namespace PLS.Services.Abstract;

public interface ITagService
{
    Task<IDataResult<TagDto>> GetAsync(int tagId);
    Task<IDataResult<TagListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true);
    Task<IDataResult<int>> GetTagCount(bool isDeleted = false, bool isActive = true);
    Task<IDataResult<Tag>> AddAsync(TagAddDto tagAddDto, string addedByUser);
    Task<IResult> UpdateAsync(TagUpdateDto tagUpdateDto, string updatedByUser, string userRole);
    Task<IResult> DeleteAsync(int tagId, string deletedByUser, string userRole);
    Task<IResult> RestoreDeletedAsync(int tagId, string restoredByUser, string userRole);
    Task<IResult> HardDeleteAsync(int tagId);
}