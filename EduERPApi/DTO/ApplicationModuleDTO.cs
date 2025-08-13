using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EduERPApi.DTO
{
    public class ApplicationModuleDTO
    {
        public Guid? ModuleId { get; set; }        
        public string ModuleName { get; set; }       
        public int Status { get; set; }
    }
}
