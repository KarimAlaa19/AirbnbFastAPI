using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbBL
{
    public interface IRuleManager
    {

        IEnumerable<RuleReadDTO> GetAllRules();

        Task<RuleDetailsDTO> GetRuleById(Guid id);

        Task<bool> Add(RuleAddDTO ruleDTO);
        IEnumerable<RuleReadDTO> GetAllRulesForProperty(Guid propId);

    }
}
