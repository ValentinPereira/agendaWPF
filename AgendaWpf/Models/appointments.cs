namespace AgendaWpf
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class appointments
    {
        [Key]
        public int idAppointment { get; set; }

        public DateTime datehour { get; set; }

        [Column(TypeName = "text")]
        [Required]
        public string subject { get; set; }

        public int idBrokers { get; set; }

        public int idCustomers { get; set; }

        public virtual brokers brokers { get; set; }

        public virtual customers customers { get; set; }
    }
}
