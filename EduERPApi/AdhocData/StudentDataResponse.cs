using EduERPApi.Data;

namespace EduERPApi.AdhocData
{
    public class StudentDataResponse
    {
        public StudentYearStreamMap DetailObj {  get; set; }
        public string StudentName { get; set; }
        public string StreamName { get; set; }
        public string RegdNumber { get; set; }
        public string AcdYearText { get; set; }
    }
}
