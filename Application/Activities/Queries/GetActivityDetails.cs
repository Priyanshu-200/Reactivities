using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Domain;       // Activity lives here
using Persistence;
using Application.Core;  // DataContext lives here

public class GetActivityDetails
{
	public class Query: IRequest<Result<Activity>>
	{ 
		public required string Id { get; set; }
	}

	public class Handler(AppDbContext context) : IRequestHandler<Query, Result<Activity>>
	{
		public async Task<Result<Activity>> Handle(Query request, CancellationToken cancellationToken)
		{
			var activity = await context.Activities.FindAsync([request.Id], cancellationToken);

			if (activity == null) return Result<Activity>.Failure("Activity not found", 404);

			return Result<Activity>.Success(activity);
		}
	}
}
