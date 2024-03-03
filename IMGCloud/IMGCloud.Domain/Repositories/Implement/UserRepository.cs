using IMGCloud.Data.Context;
using IMGCloud.Data.Entities;
using IMGCloud.Data.Enums;
using IMGCloud.Domain.Models;
using IMGCloud.Utilities.AutoMapper;
using IMGCloud.Utilities.PasswordHashExtension;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System.Linq.Expressions;
using System;

namespace IMGCloud.Domain.Repositories.Implement
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IStringLocalizer<UserRepository> _stringLocalizer;
        private readonly string className = typeof(UserRepository).FullName ?? string.Empty;


        public UserRepository(ILogger<UserRepository> logger,
            IMGCloudContext context,
            IStringLocalizer<UserRepository> stringLocalizer): base(context)
        {
            _logger = logger;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }
        public async Task<ResponeVM> CreateUserAsync(UserVM model)
        {
            var res = new ResponeVM();
            try
            {
                model.Password = model.Password.ToHashPassword();

                var user = new User().MapFor(model);
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                user.Status = Status.Active;
                _context.Users.Add(user);
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

        public async Task<bool> IsActiveUserAsync(string userName)
        {
            var user = await _context.Users
                .SingleAsync(x => x.UserName.ToLower() == userName.ToLower()
                 && x.Status == Status.Active);
            return user != null;
        }

        public int GetUserId(string userName)
        {
            return GetId(x => x.UserName == userName)?.Id ?? 0;
        }

        public async Task<User> GetUserbyUserName(string userName)
        {
            return await GetSingleAsync(x => x.UserName.ToLower() == userName.ToLower() && x.Status == Status.Active);
        }
    }
}
