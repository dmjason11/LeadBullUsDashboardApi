using Api.DTOS;
using Api.Errors;
using AutoMapper;
using Core;
using Core.IRepos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    public class ServiceProfileController : BaseController
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public ServiceProfileController(IMapper mapper , IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("getUserProfiles/{id}")]
        public async Task<ActionResult> getProfiles(string id)
        {
            var profiles = await _unitOfWork._serviceProfile.GetUserProfiles(id);
            var profilesDto = _mapper.Map<List<ShowServiceProfileDto>>(profiles);
            return Ok(profilesDto);
        }
        [HttpPost("AddServiceProfile")]
        public async Task<ActionResult> AddServiceProfile(AddServiceProfileDto model)
        {
            if(await _unitOfWork._serviceProfile.IsServiceExists(model.ServiceName))
            {
                return BadRequest(new ApiResponse(400, "service name already exist"));
            }
            var profile = _mapper.Map<ServiceProfile>(model);
            await _unitOfWork._serviceProfile.AddServiceProfile(profile);
            await _unitOfWork.saveChanges();
            return Ok();
        }

    }
}
