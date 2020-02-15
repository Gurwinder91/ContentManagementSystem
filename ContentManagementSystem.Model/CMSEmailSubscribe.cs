namespace ContentManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CMSEmailSubscribe")]
    public partial class CMSEmailSubscribe
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Email { get; set; }

        public DateTime? SubscribedOn { get; set; }
    }
}
