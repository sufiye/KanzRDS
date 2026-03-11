using the_alkanz.Website.DTOs;

namespace the_alkanz.Website.Services;

public interface IBoxService
{
    public Task<BoxResponseDto> AddToBoxAsync(CreatBoxRequestDto  creatBoxRequest);
    public Task<bool> Delete(Guid id);
}
