namespace ContentManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CMSQuestion")]
    public partial class CMSQuestion
    {
        public int Id { get; set; }

        [Required]
        public string Question { get; set; }

        [Required]
        public string Answer { get; set; }

        public int TechnologyId { get; set; }

        public string PublishedBy { get; set; }

        public DateTime PublishedDate { get; set; }

        public int? Count { get; set; }

        public string QueryString { get; set; }

        [StringLength(450)]
        public string UploadedBy { get; set; }

        public virtual CMSTechnology CMSTechnology { get; set; }

        public virtual CMSUser CMSUser { get; set; }
    }
}
