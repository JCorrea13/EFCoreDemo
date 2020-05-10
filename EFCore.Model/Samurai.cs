using EfCore.Model;
using System.Collections.Generic;

namespace EfCore.Model
{
    public partial class Samurai
    {
        public Samurai()
        {
            Quotes = new List<Quote>();
            SamuraiBattles = new List<SamuraiBattle>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? ClanId { get; set; }
        public Clan Clan { get; set; }
        public List<Quote> Quotes { get; set; }
        public List<SamuraiBattle> SamuraiBattles { get; set; }
        public Horse Horse { get; set; }
    }
}
