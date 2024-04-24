namespace SQSV_lab_3.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BOOK")]
    public partial class BOOK
    {
        public int ID { get; set; }

        [StringLength(200)]
        public string TITLE { get; set; }

        [StringLength(50)]
        public string AUTHOR { get; set; }

        [StringLength(50)]
        public string GENRE { get; set; }
    }
}
