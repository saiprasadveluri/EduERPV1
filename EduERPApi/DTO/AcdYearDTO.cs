using EduERPApi.Data;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class AcdYearDTO
    {
       public Guid? AcdYearId { get; set; }

       public string AcdYearText { get; set; }
       
    }
}
