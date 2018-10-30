namespace Chat.Model
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Files
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int Letters_id { get; set; }

        public DateTime? path_save { get; set; }

        public DateTime? date_push { get; set; }

        public DateTime? date_save { get; set; }

        public string size { get; set; }

        public int status { get; set; }

        public virtual List List { get; set; }
    }
}
