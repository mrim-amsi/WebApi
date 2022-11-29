using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PostStoreApi.DTOs
{
    public class PostDto
    {
        [Required]
        //[RegularExpression("",ErrorMessage ="لا يمكن ادخال رقم في هذا النص")]
        public string Title { get; set; }
        public int UserId { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.Always)]
        public string? ImagePath { get; set; }



    }
}
