namespace ContentManagementSystem.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CMSTechnology")]
    public partial class CMSTechnology
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CMSTechnology()
        {
            CMSQuestions = new HashSet<CMSQuestion>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string TechnologyName { get; set; }

        [StringLength(100)]
        public string QueryString { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CMSQuestion> CMSQuestions { get; set; }
    }
}
