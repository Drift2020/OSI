namespace Chat.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Correspondence_Contacts
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int id_correspondence { get; set; }

        public int id_contact { get; set; }

        public virtual Contacts Contacts { get; set; }

        public virtual Сorrespondence Сorrespondence { get; set; }
    }
}
