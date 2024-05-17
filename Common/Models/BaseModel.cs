using System.ComponentModel.DataAnnotations;

namespace PagelightPrime_BunyanSamuel_WebApp.Common.Models
{
    public class BaseModel
    {
       
        public int Id { get; set; }
        public DateTime? DataCreated { get; set; }
        public DateTime? DataUpdated { get; set; }
    }
}
