using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain;       // Activity lives here
using Persistence;  // DataContext lives here

public class GetActivityDetails
{
	public class Query: IRequest<Activity>
	{
		public required string Id { get; set; }
	}

	public class Handler(AppDbContext context) : IRequestHandler<Query, Activity>
	{
		public async Task<Activity> Handle(Query request, CancellationToken cancellationToken)
		{
			var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

			if (activity == null) throw new Exception("Activity not Found");

			return activity;
		}
	}
}
