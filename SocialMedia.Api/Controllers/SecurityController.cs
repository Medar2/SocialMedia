using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SocialMedia.Api.Responses;
using SocialMedia.Core.DTOs;
using SocialMedia.Core.Entities;
using SocialMedia.Core.Enumerations;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Interfaces;
using System.Threading.Tasks;

namespace SocialMedia.Api.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize(Roles =nameof(RoleType.Administrator))]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController] //recomendado para API
    public class SecurityController : ControllerBase
    {
        private readonly ISecurityService _securityService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SecurityController(ISecurityService securityService,IMapper mapper, IUriService uriService)
        {
            this._securityService = securityService;
            this._mapper = mapper;
            this._uriService = uriService;
        }
        [HttpPost]
        public async Task<IActionResult> AddUserts(SecurityDTO securityDTO)
        {
            var security = _mapper.Map<Security>(securityDTO);

            await _securityService.RegisterUser(security);
            securityDTO = _mapper.Map<SecurityDTO>(security);
            var response = new ApiResponse<SecurityDTO>(securityDTO);

            return Ok(response);
        }
    }
}
