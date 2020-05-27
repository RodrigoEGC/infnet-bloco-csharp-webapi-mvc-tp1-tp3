using Domain.Model.Entities;
using Domain.Model.Exceptions;
using Domain.Model.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MusicalGroupController: ControllerBase
    {
        private readonly IGroupService _groupService;

        public MusicalGroupController(IGroupService groupService)
        {
            _groupService = groupService;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GroupEntity>>> GetGroupEntity()
        {
            var groups = await _groupService.GetAllAsync();
            return groups.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GroupEntity>> GetGroupEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id);

            if (groupEntity == null)
            {
                return NotFound();
            }

            return groupEntity;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutGroupEntity(int id, GroupEntity groupEntity)
        {
            if (id != groupEntity.GroupId)
            {
                return BadRequest();
            }

            try
            {
                await _groupService.UpdateAsync(groupEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
            catch (RepositoryException e)
            {
                ModelState.AddModelError(string.Empty, e.Message);
                return BadRequest(ModelState);
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<ActionResult<GroupEntity>> PostGroupEntity(GroupEntity groupEntity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _groupService.InsertAsync(groupEntity);

                return CreatedAtAction(
                    "GetGroupEntity",
                    new { id = groupEntity.GroupId }, groupEntity);
            }
            catch (EntityValidationException e)
            {
                ModelState.AddModelError(e.PropertyName, e.Message);
                return BadRequest(ModelState);
            }
        }

        // DELETE: api/MusicalGroup/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<GroupEntity>> DeleteEntity(int id)
        {
            if (id <= 0)
            {
                return NotFound();
            }

            var groupEntity = await _groupService.GetByIdAsync(id);
            if (groupEntity == null)
            {
                return NotFound();
            }

            await _groupService.DeleteAsync(id);

            return groupEntity;
        }

        [HttpGet("CheckMascot/{mascot}/{id}")]
        public async Task<ActionResult<bool>> CheckMascotAsync(string mascot, int id)
        {
            var isMascotValid = await _groupService.CheckMascotAsync(mascot, id);

            return isMascotValid;
        }
    }
}
