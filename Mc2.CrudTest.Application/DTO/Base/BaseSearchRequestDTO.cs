namespace Mc2.CrudTest.Application.DTO.Base
{
    public class BaseSearchRequestDto
    {
        protected BaseSearchRequestDto()
        {
            PageSize = 50;
            PageIndex = 0;
        }
 
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }
 
}
