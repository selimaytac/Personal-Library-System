using AutoMapper;
using Microsoft.Extensions.Configuration;
using PLS.Data.Abstract;
using PLS.Entities.Concrete;
using PLS.Entities.Dtos;
using PLS.Services.Abstract;
using PLS.Shared.Results.Abstract;
using PLS.Shared.Results.ComplexTypes;
using PLS.Shared.Results.Concrete;

namespace PLS.Services.Concrete;

public class SourceService : ISourceService
{
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;
    private readonly IUnitOfWork _unitOfWork;

    public SourceService(IMapper mapper, IConfiguration configuration, IUnitOfWork unitOfWork)
    {
        _mapper = mapper;
        _configuration = configuration;
        _unitOfWork = unitOfWork;
    }

    public async Task<IDataResult<SourceDto>> GetAsync(int sourceId)
    {
        var source = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (source != null)
        {
            return new DataResult<SourceDto>(ResultStatus.Success, new SourceDto
            {
                Source = source,
                ResultStatus = ResultStatus.Success
            });
        }

        return new DataResult<SourceDto>(ResultStatus.Error, "Source not found", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllAsync()
    {
        var sources = await _unitOfWork.Sources.GetAllAsync();

        if (sources.Any())
        {
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });
        }

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByNonDeletedAsync()
    {
        var sources = await _unitOfWork.Sources.GetAllAsync(s => !s.IsDeleted);

        if (sources.Any())
        {
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });
        }

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByNonDeletedAndActiveAsync()
    {
        var sources = await _unitOfWork.Sources.GetAllAsync(s => !s.IsDeleted && s.IsActive);

        if (sources.Any())
        {
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });
        }

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByCategoryAsync(int categoryId)
    {
        var categoryExists = await _unitOfWork.Categories.AnyAsync(s => s.Id == categoryId);

        if (!categoryExists) return new DataResult<SourceListDto>(ResultStatus.Error, "Category not found.", null);

        var sources = await _unitOfWork.Sources.GetAllAsync(s => s.CategoryId == categoryId);

        if (sources.Any())
        {
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });
        }

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<SourceListDto>> GetAllByTagsAsync(int[] tagIds)
    {
        var tagsExists = await _unitOfWork.Tags.AnyAsync(s => tagIds.Contains(s.Id));

        if (!tagsExists) return new DataResult<SourceListDto>(ResultStatus.Error, "Tag not found.", null);

        var sources = await _unitOfWork.Sources.GetAllAsync(s => tagIds.Contains(s.Id));

        if (sources.Any())
        {
            return new DataResult<SourceListDto>(ResultStatus.Success, $"{sources.Count} records found.",
                new SourceListDto
                {
                    Sources = sources,
                    ResultStatus = ResultStatus.Success
                });
        }

        return new DataResult<SourceListDto>(ResultStatus.Error, "No sources found.", null);
    }

    public async Task<IDataResult<Source>> AddAsync(SourceAddDto sourceAddDto, string addedByUser)
    {
        var source = _mapper.Map<Source>(sourceAddDto);

        if (sourceAddDto.TagIds?.Length > 0)
        {
            var tags = await _unitOfWork.Tags.GetAllAsync(s => sourceAddDto.TagIds.Contains(s.Id));
            source.Tags = tags;
        }
        
        source.CreatedByName = addedByUser;
        source.ModifiedByName = addedByUser;

        await _unitOfWork.Sources.AddAsync(source);
        await _unitOfWork.SaveAsync();

        return new DataResult<Source>(ResultStatus.Success, $"Source {source.Id} added.", source);
    }

    public async Task<IResult> UpdateAsync(SourceUpdateDto sourceUpdateDto, string updatedByUser)
    {
        var sourceExists = await _unitOfWork.Sources.AnyAsync(s => s.Id == sourceUpdateDto.Id);

        if (sourceExists)
        {
            var source = _mapper.Map<Source>(sourceUpdateDto);
            source.ModifiedByName = updatedByUser;
            source.ModifiedDate = DateTime.Now;

            await _unitOfWork.Sources.UpdateAsync(source);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Source {source.Id} updated.");
        }
        
        return new Result(ResultStatus.Error, "Source not found.");
    }

    public async Task<IResult> DeleteAsync(int sourceId, string deletedByUser)
    {
        var deletedSource = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (deletedSource != null)
        {
            deletedSource.IsDeleted = true;
            deletedSource.ModifiedDate = DateTime.Now;
            deletedSource.ModifiedByName = deletedByUser;

            await _unitOfWork.Sources.UpdateAsync(deletedSource);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"Source {deletedSource.Id} deleted.");
        }
        
        return new Result(ResultStatus.Error, "Source not found.");
    }

    public async Task<IResult> RestoreDeletedAsync(int sourceId, string restoredByUser)
    {
        var deletedSource = await _unitOfWork.Sources.GetAsync(s => s.Id == sourceId);

        if (deletedSource != null)
        {
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

        if (source != null)
        {
            await _unitOfWork.Sources.DeleteAsync(source);
            await _unitOfWork.SaveAsync();
            return new Result(ResultStatus.Success, $"SourceId: {source.Id} has been successfully deleted from the database.");
        }
        
        return new Result(ResultStatus.Error, "Source not found.");
    }
}