using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PostStoreApi.Models
{
    //[Table("tb_Post")]
    public class Post
    {
        //[Column(Order = 1)]
        public int Id { get; set; }
        [Required]
        //[Column(Order = 3)]
        public string Title { get; set; } = null!;

        public int UserId { get; set; }
        public string Description { get; set; }
        public string Imagepath { get; set; } = "";
        public DateTime Ts { get; set; }
        public bool Published { get; set; }

    }
}
