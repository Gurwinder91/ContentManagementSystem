using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContentManagementSystem.ViewModel
{
    public class Question_DTO : Technology_DTO
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [Remote("ValidateQuestion", "Question")]
        public string Question { get; set; }
        [Required]
        [AllowHtml]
        public string Answer { get; set; }
        [Required]
        public int TechnologyId { get; set; }

        public string PublishedBy { get; set; }

        public string UploadedBy { get; set; }
        public List<Author_DTO> AuthorList { get; set; }
        public System.DateTime PublishedDate { get; set; }
        public Nullable<int> Count { get; set; }

        public List<Technology_DTO> TechnologyList { get; set; }
    }
}
