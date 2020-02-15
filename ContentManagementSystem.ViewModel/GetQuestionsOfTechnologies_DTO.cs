using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContentManagementSystem.ViewModel
{
    public class GetQuestionsOfTechnologies_DTO
    {
        [Key]
        public int Id { get; set; }
        public string TechnologyName { get; set; }
        public string QueryString { get; set; }
        public string Answer { get; set; }
        public string Question { get; set; }
        public DateTime PublishedDate { get; set; }
        public int TechnologyId { get; set; }


    }
}
