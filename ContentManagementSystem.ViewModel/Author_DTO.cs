using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ContentManagementSystem.ViewModel
{

    public partial class Author_DTO
    {
        public int Id { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorUserName { get; set; }
        public bool AuthorByBlog { get; set; }
        public string Description { get; set; }
        public byte[] Imgbytes { get; set; }
        public HttpPostedFileBase File { get; set; }
        public MemoryFile FileCopy { get; set; }
        public Nullable<System.DateTime> DOB { get; set; }
        public Nullable<int> Experience { get; set; }
        public string city { get; set; }

        public string GetAuthorExperience
        {
            get
            {
                return "Experience: " + Experience + " yrs";
            }
        }

        public string Img
        {
            get
            {
                string imageBase64Data = Imgbytes == null ? null : Convert.ToBase64String(Imgbytes);
                return string.Format("data:image/jpeg;base64,{0}", imageBase64Data);
            }
        }

        public string GetAuthorHeader
        {
            get
            {
                return this.AuthorByBlog ? "This blog by:" : "Maximum blogs by:";
            }
        }

    }
}
