using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CollectionsOfMine.Data.Models
{
    public class ContentType: EntityBase
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<Collection> Collections { get; set; }
    }
}
