using AirbnbBL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Airbnb.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RuleController : ControllerBase
    {
        private readonly IRuleManager _ruleManager;

        public RuleController(IRuleManager ruleManager)
        {
            _ruleManager = ruleManager;
        }


        #region Get All Rules
        [HttpGet]
        public IActionResult GetAll()
        {
            try
            {
                var rules = _ruleManager.GetAllRules();

                if (rules.Count() == 0)
                    return Ok("There is no rules added yet");

                Response.StatusCode = 200;

                return new JsonResult(new
                {
                    Rules = rules,
                    Count = rules.Count(),
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

        #region Get Rule By Id
        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetRuleById([FromRoute] Guid id)
        {
            try
            {
                var rule = await _ruleManager.GetRuleById(id);

                if (rule is null)
                    return Ok("Couldn't Find Rule!!!");

                return Ok(rule);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

        #region Add Rule
        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> AddRule([FromBody] RuleAddDTO rule)
        {
            try
            {
                var state = await _ruleManager.Add(rule);

                if (!state)
                    return BadRequest("Couldn't add the rule");

                return Created("", "Rule Added Succefully");
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

        #region Get All Rules For Property
        [HttpGet]
        [Route("PropertyRule/{id}")]
        public IActionResult GetAll(Guid id)
        {
            try
            {
                var rules = _ruleManager.GetAllRulesForProperty(id);

                if (rules.Count() == 0)
                    return Ok(new
                    {
                        Message = "There is no rules for this property"
                    });

                Response.StatusCode = 200;

                return new JsonResult(new
                {
                    Rules = rules,
                    Count = rules.Count(),
                });
            }
            catch (Exception ex)
            {
                Response.StatusCode = 500;
                return new JsonResult(new
                {
                    StatusCode = 500,
                    Message = $"Server Error: {ex.InnerException?.Message ?? ex.Message}",
                });
            }
        }
        #endregion

    }
}
