using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL;

[ModelMetadataType(typeof(ImageMetadata))]
public class Image
{
    public Guid ImageId { get; set; }
    public string? Source { get; set; }

    public Guid PropertyId { get; set; }
    public Property? Property { get; set; }
}
