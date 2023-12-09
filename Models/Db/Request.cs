using System.ComponentModel.DataAnnotations.Schema;

namespace Module32.Models.Db
{
   [Table("Requests")]
    public class Request
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Url { get; set; }
    }
}
