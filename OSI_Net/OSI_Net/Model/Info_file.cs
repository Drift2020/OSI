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
        public DateTime Date_start { get; set; }
        public DateTime Date_end { get; set; }
        public string Path_PC { get; set; }
        public string Path_Net { get; set; }
        [NotMapped]
        public string status_Now { get; set; }
        [NotMapped]
        public string Status_Now
        {
            get
            {
                if (status_Now == null || status_Now.Length == 0)
                {
                   
                    switch (Status)
                    {
                        case 0:
                            status_Now = "Don't work";
                            break;
                        case 2:
                        case 3:
                        case 1:
                        case 5:
                            status_Now = "Not fount";
                            break;
                        case 4:
                            status_Now = "Complete";
                            break;
                    }
                }
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
                        status_Now = "Paus";
                        break;
                    case 3:
                        status_Now = "Stoped";
                        break;
                    case 4:
                        status_Now = "Complete";
                        break;
                    case 5:
                        status_Now = "Not fount";
                        break;
                }

            }
        }

    }
}
