using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ContentManagementSystem.ViewModel
{
    public abstract class Layout_DTO
    {
        public string AuthorId { get; set; }

        [Required]
        [RegularExpression(@"^\S*$", ErrorMessage = "No white space allowed")]
        [Remote("ValidateQueryString", "Question")]
        public string QueryString { get; set; }
    }
}
