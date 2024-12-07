using Dapper.Contrib.Extensions;

namespace DapperCRUDAngular.Abstraction.Models
{
    [Table("tblEmployeeDetails")]
    public class EmployeeDetails : BaseEntity
    {
        [Key]
        public int EmpID { get; set; }
        public string EmpName { get; set; }
        public string EmailID { get; set; }
        public string Gender { get; set; }
        public string EmpAddress { get; set; }
        public string Pincode { get; set; }
        public string DateOfBirth { get; set; }
        
    }
}
