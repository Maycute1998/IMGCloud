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
using IMGCloud.Domain.Repositories.Interfaces;

namespace IMGCloud.Domain.Repositories.Implement
{
    public class UserInfoRepository : Repository<UserInfo>, IUserInfoRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<UserRepository> _logger;
        private readonly IStringLocalizer<UserRepository> _stringLocalizer;
        private readonly string className = typeof(UserRepository).FullName ?? string.Empty;

        public UserInfoRepository(ILogger<UserRepository> logger,
            IMGCloudContext context,
            IStringLocalizer<UserRepository> stringLocalizer): base(context)
        {
            _logger = logger;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public Task<UserInfo> GetUserInfobyId(int id)
        {
            return GetSingleAsync(x => x.UserId == id && x.Status == Status.Active);
        }
    }
}
