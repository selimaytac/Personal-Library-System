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

public class SourceService : ISourceService
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _unitOfWork;

    public SourceService(IConfiguration configuration, IMapper mapper, IUnitOfWork unitOfWork)
    {
        _configuration = configuration;
        _mapper = mapper;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<SourceDto>> GetAsync(int sourceId)
    {
        var source = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (source != null)
            return new DataResult<SourceDto>(ResultStatus.Success, new SourceDto
            {
                Source = source,
                ResultStatus = ResultStatus.Success
            });

        return new DataResult<SourceDto>(ResultStatus.Error, "Source not found", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllAsync(bool isDeleted = false, bool isActive = true)
    {
        var sources = await _unitOfWork.Sources.GetAllAsync(s => s.IsDeleted == isDeleted && s.IsActive == isActive);

        if (sources.Any())
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByCategoryAsync(int categoryId, bool isDeleted = false,
        bool isActive = true)
    {
        var categoryExists = await _unitOfWork.Categories.AnyAsync(s => s.Id == categoryId);

        if (!categoryExists) return new DataResult<SourceListDto>(ResultStatus.Error, "Category not found.", null);

        var sources = await _unitOfWork.Sources.GetAllAsync(s =>
            s.CategoryId == categoryId && s.IsDeleted == isDeleted && s.IsActive == isActive);

        if (sources.Any())
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByTagsAsync(int[] tagIds, bool isDeleted = false,
        bool isActive = true)
    {
        //Nulcheck and max value check for tagIds
        var tagsExists = await _unitOfWork.Tags.AnyAsync(s => tagIds.Contains(s.Id));

        if (!tagsExists) return new DataResult<SourceListDto>(ResultStatus.Error, "Tag not found.", null);

        var sources = await _unitOfWork.Sources.GetAllAsync(s =>
            s.IsDeleted == isDeleted && s.IsActive == isActive && s.Tags.Any(t => tagIds.Contains(t.Id)));

        if (sources.Any())
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<int>> GetSourceCount(bool isDeleted = false, bool isActive = true)
    {
        var count = await _unitOfWork.Sources.CountAsync(s => s.IsDeleted == isDeleted && s.IsActive == isActive);

        if (count > 0) return new DataResult<int>(ResultStatus.Success, $"{count} records found.", count);

        return new DataResult<int>(ResultStatus.Error, "No sources found.", 0);
    }

    public async Task<IDataResult<Source>> AddAsync(SourceAddDto sourceAddDto, string addedByUser)
    {
        if (!await _unitOfWork.Categories.AnyAsync(c => c.Id == sourceAddDto.CategoryId))
            return new DataResult<Source>(ResultStatus.Error, "Category not found.", null!);

        var user = await _unitOfWork.Users.GetAsync(u => u.UserName == addedByUser);
        if (user == null) return new DataResult<Source>(ResultStatus.Error, "User can not be null.", null!);

        var source = _mapper.Map<Source>(sourceAddDto);

        if (source == null) return new DataResult<Source>(ResultStatus.Error, "Source can not be null.", null!);

        source.UserId = user.Id;

        if (sourceAddDto.TagIds?.Length > 0)
        {
            var tags = new List<Tag>();

            foreach (var tagId in sourceAddDto.TagIds)
            {
                if (tagId == 0) continue;

                var tag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

                if (tag == null) continue;

                tags.Add(tag);
            }

            source.Tags = tags;
        }

        source.CreatedByName = addedByUser;
        source.ModifiedByName = addedByUser;

        await _unitOfWork.Sources.AddAsync(source);
        await _unitOfWork.SaveAsync();

        return new DataResult<Source>(ResultStatus.Success, $"Source {source.Id} added.", source);
    }

    public async Task<IResult> UpdateAsync(SourceUpdateDto sourceUpdateDto, string updatedByUser, string userRole)
    {
        var checkSource = await _unitOfWork.Sources.AnyAsync(s => s.Id == sourceUpdateDto.Id);

        if (checkSource)
        {
            if (userRole == RoleTypes.User)
            {
                var userHasSource = await _unitOfWork.Sources.AnyAsync(s =>
                    s.User.UserName == updatedByUser && s.Id == sourceUpdateDto.Id);
                if (!userHasSource)
                    return new Result(ResultStatus.Error,
                        "A user with the User role cannot edit another user's resource.");
            }

            if (!await _unitOfWork.Categories.AnyAsync(c => c.Id == sourceUpdateDto.CategoryId))
                return new DataResult<Source>(ResultStatus.Error, "Category not found.", null!);

            if (!await _unitOfWork.Users.AnyAsync(u => u.Id == sourceUpdateDto.UserId))
                return new DataResult<Source>(ResultStatus.Error, "User not found.", null!);

            var source = _mapper.Map<Source>(sourceUpdateDto);

            source.ModifiedByName = updatedByUser;
            source.ModifiedDate = DateTime.Now;

            await _unitOfWork.Sources.UpdateAsync(source);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Source {source.Id} updated.");
        }

        return new Result(ResultStatus.Error, "Source not found.");
    }

    public async Task<IResult> DeleteAsync(int sourceId, string deletedByUser, string userRole)
    {
        var deletedSource = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (deletedSource != null)
        {
            if (userRole == RoleTypes.User)
            {
                var userHasSource = await _unitOfWork.Sources.AnyAsync(s =>
                    s.User.UserName == deletedByUser && s.Id == deletedSource.Id);
                if (!userHasSource)
                    return new Result(ResultStatus.Error,
                        "A user with the User role cannot delete another user's resource.");
            }

            deletedSource.IsDeleted = true;
            deletedSource.ModifiedDate = DateTime.Now;
            deletedSource.ModifiedByName = deletedByUser;

            await _unitOfWork.Sources.UpdateAsync(deletedSource);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Source {deletedSource.Id} deleted.");
        }

        return new Result(ResultStatus.Error, "Source not found.");
    }

    public async Task<IResult> RestoreDeletedAsync(int sourceId, string restoredByUser, string userRole)
    {
        var deletedSource = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (deletedSource != null)
        {
            if (userRole == RoleTypes.User)
            {
                var userHasSource = await _unitOfWork.Sources.AnyAsync(s =>
                    s.User.UserName == restoredByUser && s.Id == deletedSource.Id);
                if (!userHasSource)
                    return new Result(ResultStatus.Error,
                        "A user with the User role cannot restore another user's resource.");
            }

            deletedSource.IsDeleted = false;
            deletedSource.ModifiedDate = DateTime.Now;
            deletedSource.ModifiedByName = restoredByUser;

            await _unitOfWork.Sources.UpdateAsync(deletedSource);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"SourceId: {deletedSource.Id} has been successfully restored.");
        }

        return new Result(ResultStatus.Error, "Source not found.");
    }

    public async Task<IResult> HardDeleteAsync(int sourceId)
    {
        var source = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (source == null)
            return new Result(ResultStatus.Error, "Source not found.");

        await _unitOfWork.Sources.DeleteAsync(source);
        await _unitOfWork.SaveAsync();

        return new Result(ResultStatus.Success,
            $"SourceId: {source.Id} has been successfully deleted from the database.");
    }

    public async Task<IResult> AddTagsToSource(int sourceId, int[] tagIds, string updatedByUser, string userRole)
    {
        var source = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId, s => s.Tags!);

        if (source == null)
            return new Result(ResultStatus.Error, "Source not found.");

        if (userRole == RoleTypes.User)
        {
            var userHasSource = await _unitOfWork.Sources.AnyAsync(s =>
                s.User.UserName == updatedByUser && s.Id == sourceId);
            if (!userHasSource)
                return new Result(ResultStatus.Error,
                    "A user with the User role cannot edit another user's resource.");
        }

        if (tagIds.Length <= 0)
            return new Result(ResultStatus.Error, "Tags can not be null.");

        foreach (var tagId in tagIds)
        {
            if (tagId <= 0) continue;

            var tag = await _unitOfWork.Tags.GetAsync(t => t.Id == tagId);

            if (tag == null) continue;

            source.Tags?.Add(tag);
        }

        source.ModifiedDate = DateTime.Now;
        source.ModifiedByName = updatedByUser;
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, $"Tags added to source {source.Id}.");
    }

    public async Task<IResult> DeleteTagsFromSource(int sourceId, int[] tagIds, string updatedByUser, string userRole)
    {
        var source = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId, s => s.Tags!);

        if (source == null) return new Result(ResultStatus.Error, "Source not found.");

        if (userRole == RoleTypes.User)
        {
            var userHasSource = await _unitOfWork.Sources.AnyAsync(s =>
                s.User.UserName == updatedByUser && s.Id == sourceId);
            if (!userHasSource)
                return new Result(ResultStatus.Error,
                    "A user with the User role cannot edit another user's resource.");
        }

        var tagsToDelete = await _unitOfWork.Tags.GetAllAsync(t => tagIds.Contains(t.Id));

        if (tagsToDelete.Count > 0 && source.Tags != null)
            foreach (var tag in tagsToDelete)
                source.Tags?.Remove(tag);

        source.ModifiedDate = DateTime.Now;
        source.ModifiedByName = updatedByUser;
        await _unitOfWork.SaveAsync();
        return new Result(ResultStatus.Success, "Tags deleted.");
    }
}