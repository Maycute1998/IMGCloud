using IMGCloud.Data.Context;
using IMGCloud.Data.Entities;
using IMGCloud.Domain.Models;
using IMGCloud.Utilities.AutoMapper;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;

namespace IMGCloud.Domain.Repositories.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IStringLocalizer<UserRepository> _stringLocalizer;
        private readonly string className = typeof(UserRepository).FullName ?? string.Empty;


        public UserRepository(ILogger<UserRepository> logger, IMGCloudContext context, IStringLocalizer<UserRepository> stringLocalizer)
        {
            _logger = logger;
            _context = context;
        }
        public async Task<ResponeVM> CreateUserAsync(CreateUserVM model)
        {
            var res = new ResponeVM();
            try
            {
                model.Password = model.Password.ToHashPassword();
                var user = new Data.Entities.User().MapFor(model);
                _context.Add(user);
                await _context.SaveChangesAsync();
                res.Status = true;
                res.Message = _stringLocalizer["createSuccess"].ToString();
            }
            catch (Exception ex)
            {
                res.Message = ex.Message;
                _logger.LogError($"Method [{className}] {Environment.NewLine} Error-{ex.Message}");
            }
            return res;
        }

        public async Task<bool> IsExitsUserEmailAsync(string email)
        {
            return await _context.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<bool> IsExitsUserNameAsync(string userName)
        {
            return await _context.Users.AnyAsync(x => x.UserName == userName);
        }
    }
}
