using Dapper.Contrib.Extensions;
using System.Runtime.Serialization;

namespace DapperCRUDAngular.Abstraction.Models
{
    public class BaseEntity
    {
        [Computed]
        [Write(false)]
        [IgnoreDataMember]
        public int RowNumber { get; set; }
        [Computed]
        [Write(false)]
        [IgnoreDataMember]
        public int TotalRecords { get; set; }
    }
}
