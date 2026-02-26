using System;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;

public class GetActivityList
{
	public class Query: IRequest<List<Activity>> { }

	public class Handler(AppDbContext context): IRequestHandler<Query, List<Activity>>
	{
		public async Task<List<Activity>> Handle(Query request, CancellationToken cancellationToken)
		{
			return await context.Activities.ToListAsync(cancellationToken);
		}
	}
	
}
