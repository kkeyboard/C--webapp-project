using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Entities
{
    public class Game
    {
        public int Id { get; set; }
        //public string Name { get; set; } = string.Empty;
        //public string? Name { get; set; };
        public required string Name { get; set; }
        public required string Genre { get; set; }
        public decimal Price { get; set; }
        public DateTime ReleaseDate { get; set; }
        public required string ImageUri { get; set; }
    }
}