using System;

namespace RefitDemo.Entities.Base
{
	public interface IBaseAuditEntity
	{
		DateTime Created { get; set; }
		
		DateTime Modified { get; set; }
	}
}
