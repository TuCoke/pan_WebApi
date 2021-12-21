using AutoMapper;
using MediatR;
using Pan.Common.Encryptions;
using Pan.Infrastructure.Context;
using Pan.Infrastructure.Entity;
using Pan.Infrastructure.Enums;
using Pan.Infrastructure.Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pan.Domain.Handler.Accounts
{
	public class CreateAdminHandler : IRequestHandler<CreateAdminRequest, bool>
	{
		private readonly IRepository<Admin, int> _adminRepository;
		private readonly IMapper _mapper;

		public CreateAdminHandler(IRepository<Admin, int> adminRepository, IMapper mapper)
		{
			_adminRepository = adminRepository;
			_mapper = mapper;
		}

		public async Task<bool> Handle(CreateAdminRequest request, CancellationToken cancellationToken)
		{
			if (string.IsNullOrEmpty(request.Account)) throw new ParameterValidException("账号不能为空");
			if (_adminRepository.GetQuery().Any(x => x.Account == request.Account))
				throw new LogicException($"账号：{request.Name}已存在");
			if (string.IsNullOrEmpty(request.Password)) request.Password = "123456";

			var admin = _mapper.Map<Admin>(request);
			admin.Salt = Path.GetRandomFileName();
			admin.Password = MD5Encryption.ComputeHash(admin.Password + admin.Salt);
			await _adminRepository.AddAsync(admin);

			return true;
		}
	}

	public class CreateAdminRequest : IRequest<bool>
	{
		/// <summary>
		/// 姓名
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// 登录账号
		/// </summary>
		public string Account { get; set; }

		/// <summary>
		/// 手机号
		/// </summary>
		public string Phone { get; set; }

		/// <summary>
		/// 邮箱
		/// </summary>
		public string Mail { get; set; }

		/// <summary>
		/// 性别
		/// </summary>
		public GenderEnums? Gender { get; set; }

		/// <summary>
		/// 密码
		/// </summary>
		public string Password { get; set; }
	}
}
