using System;
using System.Collections.Generic;

namespace RefitDemo.Entities
{
	// https://www.tektutorialshub.com/entity-framework-core-data-seeding/
    // https://docs.microsoft.com/en-us/aspnet/web-api/overview/data/using-web-api-with-entity-framework/part-5
	[Serializable]
	public class DepartmentDto
	{
		public int DepartmentId { get; set; }

		public string Name { get; set; }
		
		public ICollection<string> Employees { get; set; } = new HashSet<string>();
	}
}
