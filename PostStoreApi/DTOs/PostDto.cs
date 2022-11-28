using System.ComponentModel.DataAnnotations;

namespace PostStoreApi.DTOs
{
    public class PostDto
    {
        [Required]
        //[RegularExpression("",ErrorMessage ="لا يمكن ادخال رقم في هذا النص")]
        public string Title { get; set; }




    }
}
