using EduERPApi.DTO;

namespace EduERPApi.AdhocData
{
    public class ChalanInfoRequest
    {
        public int TermNo { get; set; }
        public Guid StudentCourseDetailAcdYearMapId { get; set; }
        public Guid CourseDetailId { get; set; }
        public List<FeeCollectionsResponse> CollectionList { get; set; }

        public List<FeeConcessionDTO> ConcessionList { get; set; }
    }
}
