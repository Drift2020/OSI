using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
      
        public string status_Now { get; set; }
        [NotMapped]
        public string Status_Now
        {
            get
            {

                return status_Now;
            }
            set
            {
                switch (Status)
                {
                    case 0:
                        status_Now = "Don't work";
                        break;
                    case 1:
                        status_Now = "Download: " + value;
                        break;
                    case 2:
                        status_Now = "Paus: " + value;
                        break;
                    case 3:
                        status_Now = "Stoped in: " + value;
                        break;
                    case 4:
                        status_Now = "Complete";
                        break;
                }

            }
        }

    }
}
