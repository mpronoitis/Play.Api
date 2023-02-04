using AutoMapper;
using FluentValidation.Results;
using NetDevPack.Mediator;
using Play.Application.Core.Interfaces;
using Play.Application.Core.ViewModels;
using Play.Domain.Core.Commands;
using Play.Domain.Core.Interfaces;

namespace Play.Application.Core.Services;

public class UserProfileService : IUserProfileService
{
    private readonly IMapper _mapper;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IUserProfileRepository _userProfileRepository;

    public UserProfileService(IUserProfileRepository userProfileRepository, IMapper mapper,
        IMediatorHandler mediatorHandler)
    {
        _userProfileRepository = userProfileRepository;
        _mapper = mapper;
        _mediatorHandler = mediatorHandler;
    }

    public async Task<UserProfileViewModel> GetAsync(Guid id)
    {
        return _mapper.Map<UserProfileViewModel>(await _userProfileRepository.GetByIdAsync(id));
    }

    public async Task<UserProfileViewModel> GetByUserId(Guid user_id)
    {
        return _mapper.Map<UserProfileViewModel>(await _userProfileRepository.GetByUserId(user_id));
    }

    public async Task<ValidationResult> Register(UserProfileViewModel userProfileViewModel)
    {
        var registerCommand = _mapper.Map<RegisterUserProfileCommand>(userProfileViewModel);
        var res = await _mediatorHandler.SendCommand(registerCommand);
        return res;
    }

    public async Task<ValidationResult> Update(UserProfileViewModel userProfileViewModel)
    {
        var updateCommand = _mapper.Map<UpdateUserProfileCommand>(userProfileViewModel);
        var res = await _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    public async Task<ValidationResult> Remove(UserProfileViewModel userProfileViewModel)
    {
        var updateCommand = _mapper.Map<UpdateUserProfileCommand>(userProfileViewModel);
        var res = await _mediatorHandler.SendCommand(updateCommand);
        return res;
    }

    public async Task<IEnumerable<UserProfileViewModel>> GetAll(int page = 1, int pageSize = 10)
    {
        var userProfiles = await _userProfileRepository.GetAllAsync(page, pageSize);
        return _mapper.Map<IEnumerable<UserProfileViewModel>>(userProfiles);
    }
}