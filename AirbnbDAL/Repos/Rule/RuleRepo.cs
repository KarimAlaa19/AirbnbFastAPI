using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL
{
    public class RuleRepo : GenericRepo<Rule>, IRuleRepo
    {
        private readonly AirbnbContext _context;
        public RuleRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }

        public IEnumerable<Rule> GetRulesForProperty(Guid propId)
        {
           return _context.PropertyRules
                .Where(pr => pr.PropertyId == propId)
                .Include(pr => pr.Rule)
                .Select(r => new Rule
                {
                    RuleId = r.Rule.RuleId,
                    Content = r.Rule.Content,
                    Icon = r.Rule.Icon,
                })
                .ToList();
        }
    }
}
