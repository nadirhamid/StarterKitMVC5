﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Website.Identity.Constants.Roles;
using Website.Identity.Repositories;

namespace $safeprojectname$.Controllers.Identity
{
    [Authorize(Roles = ApplicationRoles.ADMIN)]
    public class RefreshTokensController : BaseApiController
    {

        private IAuthRepository _authRepository;

        public RefreshTokensController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public IHttpActionResult Get()
        {
            return Ok(_authRepository.GetAllRefreshTokens());
        }

        public async Task<IHttpActionResult> Delete(string tokenId)
        {
            var result = await _authRepository.RemoveRefreshToken(tokenId);
            if (result)
            {
                return Ok();
            }
            return BadRequest("Token Id does not exist");

        }
        
    }
}