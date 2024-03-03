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
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace IMGCloud.Domain.Repositories
{
    public class UserTokenRepository : Repository<UserToken>, IUserTokenRepository
    {
        private readonly IMGCloudContext _context;
        private readonly ILogger<UserTokenRepository> _logger;
        private readonly IStringLocalizer<UserTokenRepository> _stringLocalizer;
        private readonly string className = typeof(UserTokenRepository).FullName ?? string.Empty;

        public UserTokenRepository(ILogger<UserTokenRepository> logger,
            IMGCloudContext context,
            IStringLocalizer<UserTokenRepository> stringLocalizer) : base(context)
        {
            _logger = logger;
            _context = context;
            _stringLocalizer = stringLocalizer;
        }

        public string GetExistedUserTokenFromDB(int userId)
        {
            var userToken = _context.UserTokens.SingleOrDefault(x => x.UserId == userId);
            if (userToken is not null)
            {
                return userToken.Token; 
            }
            return string.Empty;
        }

        public ResponeVM StoreToken(TokenVM tokenModel)
        {
            var result = new ResponeVM();
            try
            {
                var user = _context.Users.Single(x => x.UserName.ToLower() == tokenModel.UserName.ToLower());
                if (user is not null)
                {

                    var userToken = _context.UserTokens.SingleOrDefault(x => x.UserId == user.Id);
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
                            ExpireDays = tokenModel.ExpireDate,
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

        public ResponeVM RemveToken(int curentUserId)
        {
            var res = new ResponeVM();
            try
            {
                var userToken = GetById(curentUserId);
                if (userToken != null)
                {
                    Delete(userToken);
                    _context.SaveChanges();

                    res.Status = true;
                    res.Message = "Logout Successfully";
                }
                else
                {
                    res.Message = "Entity not found";
                    _logger.LogError($"Method [{className}] {Environment.NewLine} Error-{res.Message}");
                }

            }
            catch (Exception ex)
            {
                _logger.LogError($"Method [{className}] {Environment.NewLine} Error-{ex.Message}");
            }
            return res;
        }

    }
}
