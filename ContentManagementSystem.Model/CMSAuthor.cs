namespace ContentManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CMSAuthor")]
    public partial class CMSAuthor
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Column(TypeName = "image")]
        public byte[] Image { get; set; }

        public DateTime? DOB { get; set; }

        public int? Experience { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(450)]
        public string UserId { get; set; }

        [StringLength(100)]
        public string AuthorName { get; set; }

        public virtual CMSUser CMSUser { get; set; }
    }
}
