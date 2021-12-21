using MediatR;
using Microsoft.EntityFrameworkCore;
using Pan.Common.Encryptions;
using Pan.Domain.Service;
using Pan.Infrastructure.Context;
using Pan.Infrastructure.Entity;
using Pan.Infrastructure.Enums;
using Pan.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Domain.Handler.Accounts
{
    public class AdminLoginHandler : IRequestHandler<AdminLoginRequest, AdminLoginResponse>
    {
        private readonly IRepository<Admin, int> _adminRepository;
        private readonly AccountService _accountService;

        public AdminLoginHandler(IRepository<Admin, int> adminRepository, AccountService accountService)
        {
            _adminRepository = adminRepository;
            _accountService = accountService;
        }

        public async Task<AdminLoginResponse> Handle(AdminLoginRequest request, CancellationToken cancellationToken)
        {
            var admin = await _adminRepository.GetQuery().FirstOrDefaultAsync(x => x.Account == request.Account);
            if (admin == null) throw new NotFound("用户不存在");

            if (admin.Status != EntityStatusEnums.Normal) throw new LogicException("账号已被禁用，请联系客服");
            var test = MD5Encryption.ComputeHash(request.Password + admin.Salt);

            if (MD5Encryption.ComputeHash(request.Password + admin.Salt) != admin.Password)
                throw new LogicException("登录失败：密码错误");

            admin.LastLoginTime = DateTime.Now;
            _adminRepository.Update(admin);

            var token = _accountService.GetToken(admin.Id, TimeSpan.FromDays(14));
            return new AdminLoginResponse
            {
                UserId = admin.Id,
                Token = token,
            };
        }
    }

    public class AdminLoginRequest : IRequest<AdminLoginResponse>
    {
        /// <summary>
        /// 登录账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        /// 密码加密串
        /// </summary>
        public string Password { get; set; }
    }

    public class AdminLoginResponse
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }
    }
}
