using AirbnbDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public class RuleManager : IRuleManager
    {
        private readonly IUnitOfWork _unitOfWork;
        public RuleManager(IUnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        public IEnumerable<RuleReadDTO> GetAllRules()
        {
            IEnumerable<RuleReadDTO> rules = _unitOfWork.RuleRepo
                .GetAll()
                .Select(rule => new RuleReadDTO
                {
                    RuleId = rule.RuleId,
                    Content = rule.Content,
                    Icon = rule.Icon
                })
                .ToList();

            return rules;
        }

        public async Task<RuleDetailsDTO> GetRuleById(Guid id)
        {
            var rule = await _unitOfWork.RuleRepo.GetById(id);

            if (rule is null) 
                return null;

            return new RuleDetailsDTO
            {
                RuleId = rule.RuleId,
                Content = rule.Content,
                Icon = rule.Icon
            };
        }

        public async Task<bool> Add(RuleAddDTO ruleDTO)
        {
            _unitOfWork.RuleRepo.Add(new Rule
            {
                RuleId = Guid.NewGuid(),
                Content = ruleDTO.Content,
                Icon = ruleDTO.Icon
            });


            var resultState = await _unitOfWork.Save();
            return resultState != 0;
        }

        public IEnumerable<RuleReadDTO> GetAllRulesForProperty(Guid propId)
        {
            var rules = _unitOfWork.RuleRepo.GetRulesForProperty(propId);

            return rules.Select(rule => new RuleReadDTO
            {
                RuleId= rule.RuleId,
                Content = rule.Content,
                Icon = rule.Icon
            });
        }

    }
}
