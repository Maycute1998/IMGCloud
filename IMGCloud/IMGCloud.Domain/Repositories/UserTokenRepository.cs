using IMGCloud.Data.Context;
using IMGCloud.Data.Entities;
using IMGCloud.Data.Enums;
using IMGCloud.Domain.Models;
using IMGCloud.Domain.Repositories.Implement;
using IMGCloud.Utilities.AutoMapper;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Repositories
{
    public class UserTokenRepository : IUserTokenRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<UserTokenRepository> _logger;
        private readonly IStringLocalizer<UserTokenRepository> _stringLocalizer;
        private readonly string className = typeof(UserTokenRepository).FullName ?? string.Empty;

        public UserTokenRepository(ILogger<UserTokenRepository> logger,
            IMGCloudContext context,
            IStringLocalizer<UserTokenRepository> stringLocalizer)
        {
            _logger = logger;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public async Task<ResponeVM> StoreTokenAsync(TokenVM tokenModel)
        {
            var result = new ResponeVM();
            try
            {
                var user = _context.Users.Single(x => x.UserName.ToLower() == tokenModel.UserName.ToLower());
                if (user is not null)
                {

                    var userToken = _context.UserTokens.Single(x => x.UserId == user.Id);
                    if (userToken is not null)
                    {
                        var existedToken = new UserToken().MapFor(tokenModel);
                        existedToken.CreatedDate = DateTime.UtcNow;
                        existedToken.ModifiedDate = DateTime.UtcNow;
                        _context.UserTokens.Update(userToken);
                    }
                    else
                    {
                        var newUserToken = new UserToken()
                        {
                            UserId = user.Id,
                            Token = tokenModel.Token,
                            ExpireDays = tokenModel.ExpireDays,
                            Status = tokenModel.IsActive ? Status.Active : Status.InActive,
                            CreatedDate = user.CreatedDate,
                            ModifiedDate = user.ModifiedDate
                        };
                        _context.UserTokens.Add(newUserToken);
                    }
                    _context.SaveChanges();
                    result.Status = true;
                    result.Message = _stringLocalizer["createSuccess"].ToString();
                }
                else 
                {
                    var errorMsg = _stringLocalizer["createSuccess"].ToString();
                    _logger.LogError($"Method [{className}] {Environment.NewLine} Error: {errorMsg}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Method [{className}] {Environment.NewLine} Error: {ex.Message}");
            }
            return result;
        }
    }
}
