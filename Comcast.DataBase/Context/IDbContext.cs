using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Comcast.DataBase.Context
{
	public interface IDbContext
	{
		int SaveChanges();
		DbEntityEntry Entry(Object entity);
		Task<int> SaveChangesAsync(CancellationToken cancellationToken);
		Task<int> SaveChangesAsync();
	}

}
