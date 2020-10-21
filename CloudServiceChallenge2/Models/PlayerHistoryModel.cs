using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CloudServiceChallenge2.Models
{
    public class PlayerHistoryModel
    {
        [Key]
        public int Id { get; set; }
        public int Player1Id { get; set; }
        public int Player2Id { get; set; }
        public int Result { get; set; }

    }
}
