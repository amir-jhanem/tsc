using System;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TSC.Controllers.Resources;
using TSC.Core;
using TSC.Core.Models;

namespace TSC.Controllers
{
    [Route("/api/Groups")]
    public class GroupController:Controller
    {
        private readonly IMapper mapper;
        private readonly IGroupRepository repository;
        private readonly IUnitOfWork unitOfWork;
        public GroupController(IMapper mapper,IGroupRepository repository, IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.repository = repository;
        }
        [HttpPost]
        public async Task<IActionResult> CreateGroup([FromBody] SaveGroupResource saveGroupResource)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            repository.Add(saveGroupResource);
            await unitOfWork.CompleteAsync();
            return Ok(saveGroupResource);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateGroup([FromBody] SaveGroupResource saveGroupResource)
        {
            if(!ModelState.IsValid){
                return BadRequest(ModelState);
            }
            repository.Update(saveGroupResource);
            await unitOfWork.CompleteAsync();
            return Ok(saveGroupResource);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGroup(int id)
        {
            var group = await repository.Get(id);

            if(group == null)
                return NotFound();

            repository.Remove(group);
            await unitOfWork.CompleteAsync();

            return Ok(id);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetGroup(int id)
        {
            var group = await repository.GetGroup(id);

            if(group == null)
                return NotFound();

            return Ok(group);
        }

        [HttpGet]
        public async Task<QueryResult<GetGroupResource>> GetGroups(QueryResource filterResource)
        {
            var filter = mapper.Map<QueryResource, ModelQuery>(filterResource);
            var queryResult = await repository.GetGroups(filter);

            return queryResult;
        }
    }
}