using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ContentManagementSystem.ViewModel
{
    public class Technology_DTO : Layout_DTO
    {
        public Technology_DTO()
        {
            QuestionList = new List<Question_DTO>();
        }
        public int Id { get; set; }
        public string TechnologyName { get; set; }

        public string TechnologyQueryString { get; set; }

        public List<Question_DTO> QuestionList { get; set; }
    }
}
