using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrainingWebAPI.Service.Models
{
    public class ActorModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "FirstName is a required field")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "LastName is a required field")]
        public string LastName { get; set; }
        [RegularExpression("(M|F)", ErrorMessage = "Gender must to be 'M' or 'F'")]
        public string Gender { get; set; }

    }
}
