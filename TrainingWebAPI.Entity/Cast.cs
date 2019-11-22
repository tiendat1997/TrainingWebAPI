namespace TrainingWebAPI.Entity
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Cast")]
    public partial class Cast
    {
        public int Id { get; set; }

        public int ActorId { get; set; }

        public int MovieId { get; set; }

        [StringLength(50)]
        public string Role { get; set; }

        public virtual Actor Actor { get; set; }

        public virtual Movie Movie { get; set; }
    }
}
