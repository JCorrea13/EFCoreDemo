using EFCore.Model;
using System.Collections.Generic;

namespace EfCore.Model
{
    public partial class Samurai
    {
        public Samurai()
        {
            Quotes = new HashSet<Quote>();
            SamuraiBattles = new List<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClanId { get; set; }

        public virtual Clan Clan { get; set; }
        public virtual ICollection<Quote> Quotes { get; set; }

        public List<SamuraiBattle> SamuraiBattles { get; set; }
    }
}
