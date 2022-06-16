using System.Collections.Generic;

namespace TempProject.Core.Entity
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
