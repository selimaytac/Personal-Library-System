using AutoMapper;
using Microsoft.Extensions.Configuration;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.ConstTypes;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class TagService : ITagService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public TagService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }


    public async Task<IDataResult<TagDto>> GetAsync(int tagId)
    {
        var tag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

        if (tag != null)
        {
            return new DataResult<TagDto>(ResultStatus.Success, new TagDto
            {
                Tag = tag,
                ResultStatus = ResultStatus.Success
            });
        }

        return new DataResult<TagDto>(ResultStatus.Error, "Tag not found", null);
    }

    public async Task<IDataResult<TagListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true)
    {
        var tags = await _unitOfWork.Tags.GetAllAsync(t => t.IsDeleted == isDeleted && t.IsActive == isActive);

        if (tags.Any())
        {
            return new DataResult<TagListDto>(ResultStatus.Success, new TagListDto
            {
                Tags = tags,
                ResultStatus = ResultStatus.Success
            });
        }

        return new DataResult<TagListDto>(ResultStatus.Error, "No tag not found", null);
    }

    public async Task<IDataResult<int>> GetTagCount(bool isDeleted = false, bool isActive = true)
    {
        var tagCount = await _unitOfWork.Tags.CountAsync(t => isDeleted == t.IsDeleted && isActive == t.IsActive);

        if (tagCount > 0)
        {
            return new DataResult<int>(ResultStatus.Success, $"{tagCount} records found.", tagCount);
        }
        
        return new DataResult<int>(ResultStatus.Error, "No tag not found", 0);
    }

    public async Task<IDataResult<Tag>> AddAsync(TagAddDto tagAddDto, string addedByUser)
    {
        if (!await _unitOfWork.Tags.AnyAsync(t => t.TagName == tagAddDto.TagName))
            return new DataResult<Tag>(ResultStatus.Error, "Tag already exists", null);
        
        var user = await _unitOfWork.Users.GetAsync(u => u.UserName == addedByUser);
        if(user == null)
            return new DataResult<Tag>(ResultStatus.Error, "User cannot be null", null);

        var tag = _mapper.Map<Tag>(tagAddDto);

        tag.CreatedByName = addedByUser;
        tag.ModifiedByName = addedByUser;

        await _unitOfWork.Tags.AddAsync(tag);
        await _unitOfWork.SaveAsync();
        
        return new DataResult<Tag>(ResultStatus.Success, $"Tag {tag.Id} created successfully", tag);
    }

    public async Task<IResult> UpdateAsync(TagUpdateDto tagUpdateDto, string updatedByUser, string userRole)
    {
        var checkTag = await _unitOfWork.Tags.AnyAsync(t => t.Id == tagUpdateDto.Id);

        if (checkTag)
        {
            if (userRole == RoleTypes.User)
            {
                var checkTagOwner = await _unitOfWork.Sources.AnyAsync(s => s.Id == tagUpdateDto.Id && s.CreatedByName == updatedByUser);
                if(!checkTagOwner)
                    return new Result(ResultStatus.Error, "You are not the owner of this tag, you cannot update it.");
            }
            
            if(!await _unitOfWork.Tags.AnyAsync(t => t.TagName == tagUpdateDto.TagName && t.Id != tagUpdateDto.Id))
                return new Result(ResultStatus.Error, "Tag already exists");

            var tag = _mapper.Map<Tag>(tagUpdateDto);
            
            tag.ModifiedDate = DateTime.Now;
            tag.ModifiedByName = updatedByUser;

            await _unitOfWork.Tags.UpdateAsync(tag);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Tag {tag.Id} updated successfully");
        }
        
        return new Result(ResultStatus.Error, "Tag not found");
    }

    public async Task<IResult> DeleteAsync(int tagId, string deletedByUser, string userRole)
    {
        var deletedTag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

        if (deletedTag != null)
        {
            if (userRole == RoleTypes.User)
            {
                var checkTagOwner = await _unitOfWork.Sources.AnyAsync(s => s.Id == deletedTag.Id && s.CreatedByName == deletedByUser);
                if(!checkTagOwner)
                    return new Result(ResultStatus.Error, "You are not the owner of this tag, you cannot delete it");
            }
            
            deletedTag.IsDeleted = true;
            deletedTag.ModifiedDate = DateTime.Now;
            deletedTag.ModifiedByName = deletedByUser;
            
            await _unitOfWork.Tags.UpdateAsync(deletedTag);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Tag {deletedTag.Id} deleted successfully");
        }
        
        return new Result(ResultStatus.Error, "Tag not found");
    }

    public async Task<IResult> RestoreDeletedAsync(int tagId, string restoredByUser, string userRole)
    {
        var restoredTag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

        if (restoredTag != null)
        {
            if (userRole == RoleTypes.User)
            {
                var checkTagOwner = await _unitOfWork.Sources.AnyAsync(s => s.Id == restoredTag.Id && s.CreatedByName == restoredByUser);
                if(!checkTagOwner)
                    return new Result(ResultStatus.Error, "You are not the owner of this tag, you cannot restore it");
            }
            
            restoredTag.IsDeleted = false;
            restoredTag.ModifiedDate = DateTime.Now;
            restoredTag.ModifiedByName = restoredByUser;
            
            await _unitOfWork.Tags.UpdateAsync(restoredTag);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Tag {restoredTag.Id} restored successfully");
        }
        
        return new Result(ResultStatus.Error, "Tag not found");
    }

    public async Task<IResult> HardDeleteAsync(int tagId)
    {
        var deletedTag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

        if (deletedTag != null)
        {
            await _unitOfWork.Tags.DeleteAsync(deletedTag);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Tag {deletedTag.Id} hard deleted successfully");
        }
        
        return new Result(ResultStatus.Error, "Tag not found");
    }
}