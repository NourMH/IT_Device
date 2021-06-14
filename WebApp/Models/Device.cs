namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Device")]
    public partial class Device
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Device()
        {
            DeviceProps = new HashSet<DeviceProp>();
        }

        public int Id { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string DeviceName { get; set; }

        [DisplayName("Category")]
        public int cat_id { get; set; }

        [DisplayName("Image")]
        [Column(TypeName = "text")]
        public string img { get; set; }

        [Column(TypeName = "text")]
        public string description { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DeviceProp> DeviceProps { get; set; }
    }
}
