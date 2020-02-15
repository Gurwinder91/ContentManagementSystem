namespace ContentManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CMSNotFoundQuestion
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Question { get; set; }
    }
}
