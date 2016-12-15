namespace TalkBackWCF.Domain
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Data.Entity;
    using System.Linq;

    public class TalkBackDBContext : DbContext
    {

        public TalkBackDBContext()
            : base("name=TalkBackDBContext")
        {
        }

        public virtual DbSet<User> Users { get; set; }
    }

    public class User
    {
        [Key]
        public int Id { get; set; }
        [Required,StringLength(30)]
        public string UserName { get; set; }
        [Required,DataType(DataType.Password)]
        public string Password { get; set; }
    }
}