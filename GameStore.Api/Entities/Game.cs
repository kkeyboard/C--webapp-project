using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace GameStore.Api.Entities
{
    public class Game
    {
        public int Id { get; set; }
        //public string Name { get; set; } = string.Empty;
        //public string? Name { get; set; };

        // Data validation
        [Required] // Can't be empty
        [StringLength(50)] // max 50 characters
        public required string Name { get; set; }

        [Required] // Can't be empty
        [StringLength(20)] // max 20 characters
        public required string Genre { get; set; }

        [Range(1, 100)] // $1  ~ $100
        public decimal Price { get; set; }

        public DateTime ReleaseDate { get; set; }

        [Url]
        [StringLength(100)]
        public required string ImageUri { get; set; }
    }
}