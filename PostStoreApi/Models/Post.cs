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

        //public string TitleAr12 { get; set; } = "fsh";

        //[Column(Order = 2)]
        //public string Description { get; set; }

    }
}
