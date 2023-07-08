using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirbnbDAL {
    public class ImageRepo : GenericRepo<Image>, IImageRepo
    {
        private readonly AirbnbContext _context;
        public ImageRepo(AirbnbContext context) : base(context)
        {
            _context = context;
        }
    }
}
