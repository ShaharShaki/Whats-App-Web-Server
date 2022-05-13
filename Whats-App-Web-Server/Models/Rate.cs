using System.ComponentModel.DataAnnotations;
namespace Whats_App_Web_Server.Models
{
    public class Rate
    {
        public int Id { get; set; }

        [Required]
        [Range(0, 10)]
        [RegularExpression("^[0-9]+$")]
        public int givenRate { get; set; }

        public string Description { get; set; }

    }
}
