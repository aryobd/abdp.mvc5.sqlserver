namespace abdp.Data.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("tm_olss_model_vehicle")]
    public partial class tm_olss_model_vehicle
    {
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        //public tm_olss_model_vehicle()
        //{
        //    this.tm_olss_model_vehicle_prev = new HashSet<tm_olss_model_vehicle>();
        //}

        [Key]
        public int tm_olss_model_vehicle_id { get; set; }
        public Nullable<int> tm_olss_model_vehicle_id_prev { get; set; }
        public int tm_olss_brand_id { get; set; }
        public string model_vehicle_name { get; set; }
        public string model_vehicle_desc { get; set; }

        //public virtual tm_olss_brand tm_olss_brand { get; set; }
        //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        //public virtual ICollection<tm_olss_model_vehicle> tm_olss_model_vehicle_prev { get; set; }
        //public virtual tm_olss_model_vehicle tm_olss_model_vehicle2 { get; set; }
    }
}
