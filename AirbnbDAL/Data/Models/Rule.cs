using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;


[ModelMetadataType(typeof(RuleMetadata))]
public class Rule
{
    public Guid RuleId { get; set; }
    public string? Content { get; set; }
    public string? Icon { get; set; }


    public ICollection<PropertyRule> PropertyRules { get; set; } = new HashSet<PropertyRule>();
}
