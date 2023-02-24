using System;

namespace TodoApi.Models.Base
{
    public interface IAuditEntity
    {
        DateTime InsertDate { get; set; }

        DateTime? ModifiedDate { get; set; }
    }
}
