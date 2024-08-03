using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GameStore.Api.Entities
{
    public class Game
    {
        public int Id { get; set; }

        [Required] // These are data annotations.
        [StringLength(50)]
        public required string Name { get; set; }

        [Required]
        [StringLength(20)]
        public required string Genre { get; set; }

        [Range(1, 100)]
        public decimal Price { get; set; }
        public DateTime ReleaseDate {get; set; }

        [Url]
        [StringLength(100)]
        public required string ImageUri { get; set; }
    }
}