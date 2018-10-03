using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace OSI_Net.Model
{
    public class Info_file 

    {

        public int Info_fileId { get; set; }
        public string Name { get; set; }
      
        public int Status { get; set; }
        public DateTime Date { get; set; }
        public string Path_PC { get; set; }
        public string Path_Net { get; set; }
    }
}
