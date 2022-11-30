
namespace CollectionsOfMine.Services;

using AutoMapper;
using BCrypt.Net;
using CollectionsOfMine.Exceptions;
using CollectionsOfMine.ViewModels;
using CollectionsOfMine.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CollectionsOfMine.Authorization;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private IJwtUtils _jwtUtils;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper, IJwtUtils jwtUtils)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _jwtUtils = jwtUtils;
    }

    public async Task<IEnumerable<CreateOrUpdateUser>> GetAllAsync()
    {
        var users = await Task.Run(() => _unitOfWork
            .UserRepository.Query());

        var userModels = new List<CreateOrUpdateUser>();

        users.ToList().ForEach(u => userModels.Add(_mapper.Map<CreateOrUpdateUser>(u)));

        return userModels;
    }

    public async Task<CreateOrUpdateUser> GetByIdAsync(long id)
    {
        var user = await Task.Run(() =>
            _unitOfWork.UserRepository.Query()
            .Where(i => i.Id == id)
            .FirstOrDefault());

        return _mapper.Map<CreateOrUpdateUser>(user);
    }

    public async Task DeleteAsync(long id)
    {
        var entity = _mapper.Map<User>(await GetByIdAsync(id));
        await _unitOfWork.UserRepository.RemoveAsync(entity);
        await _unitOfWork.CommitAsync();
    }

    public async Task<CreateOrUpdateUser> UpdateAsync(CreateOrUpdateUser model)
    {
        var user = await GetByIdAsync(model.Id);

        // validate
        var userNameExists = model.Username != user.Username && _unitOfWork
            .UserRepository.Query().Any(x => x.Username == model.Username);

        if (userNameExists)
        {
            throw new AuthException("Username '" + model.Username + "' is already taken");
        }

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
        {
            user.PasswordHash = BCrypt.HashPassword(model.Password);
        }

        // copy model to user and save
        _mapper.Map(model, user);
        await _unitOfWork.UserRepository.SaveChangeAsync();

        return _mapper.Map<CreateOrUpdateUser>(user);
    }

    public async Task<CreateOrUpdateUser> CreateAsync(CreateOrUpdateUser model)
    {
        try
        {
            // validate
            var userExists = _unitOfWork.UserRepository.Query()
                    .Any(x => x.Username == model.Username);

            if (userExists)
            {
                throw new AuthException("Username '" + model.Username + "' is already taken");
            }

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.HashPassword(model.Password);

            // save user
            await _unitOfWork.UserRepository.AddAsync(user);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<CreateOrUpdateUser>(user);
        }
        catch (Exception ex)
        {
            throw;
        }

    }

    public async Task<AuthenticateResponse> AuthenticateAsync(AuthenticateRequest model)
    {
        var user = await Task.Run(() => _unitOfWork.UserRepository
            .Query().SingleOrDefault(x => x.Username == model.Username));

        // validate
        if (user == null || !BCrypt.Verify(model.Password, user.PasswordHash))
            throw new AuthException("Username or password is incorrect");

        // authentication successful
        var response = _mapper.Map<AuthenticateResponse>(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }
}

