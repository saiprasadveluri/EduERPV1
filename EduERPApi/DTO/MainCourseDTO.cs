using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace EduERPApi.DTO
{   
    public class MainCourseDTO
    {
        public Guid? MainCourseId { get; set; }
        
        public Guid? OrgId { get; set; }

        [Required]
        [MaxLength(50)]
        public string CourseName { get; set; }
        
        [Required]
        public int IsSpecializationsAvailable { get; set; } = 0;

        [Required]
        public int DurationInYears { get; set; } = 1;
        [Required]
        public int NumOfTermsInYear { get; set; } = 1;
        public string Description { get; set; }
        public int Status { get; set; } = 1;
        //public List<string>? SpecialzationNames { get; set; }
    }

    /*public class ValidCourseSpecialzationDataAttribute : Attribute, IModelValidator
    {
        public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
        {
            var Obj = context.Model as MainCourseDTO;
            if (Obj != null)
            {
                if (Obj.IsSpecializationsAvailable == 1)
                {
                    if (Obj.SpecialzationNames != null && Obj.SpecialzationNames.Count>0)
                    {
                        return Enumerable.Empty<ModelValidationResult>();
                    }
                    else
                    {
                        return new List<ModelValidationResult>
                        {
                            new ModelValidationResult(null,"Specialzations not Provided")
                        };
                    }
                }
                else
                {
                    return Enumerable.Empty<ModelValidationResult>();
                }
            }
            else
            {
                return new List<ModelValidationResult>
                {
                    new ModelValidationResult(null,"Not a Requierd Object")
                };
            }


        }
    }*/
}
