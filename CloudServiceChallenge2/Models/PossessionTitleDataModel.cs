using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge2.Models
{
    public class PossessionTitleDataModel
    {
        [Key]
        public int Id { get; set; }
        public UserDataModel User { get; set; }
        [ForeignKey("User")] 
        public int UserId { get; set; }
        public TitleMasterModel Title { get; set; }
        [ForeignKey("Title")]
        public int TitleId { get; set; }

       
    }
}
