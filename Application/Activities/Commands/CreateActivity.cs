using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Persistence;
using MediatR;

public class CreateActivity
{
	public class Command: IRequest<String>
	{
		public required Activity Activity { get; set; }
	}

	public class Handler(AppDbContext context) : IRequestHandler<Command, string>
	{
		public async Task<string> Handle(Command request, CancellationToken cancellationToken)
		{
			context.Activities.Add(request.Activity);
			await context.SaveChangesAsync(cancellationToken);
			return request.Activity.Id;
		}
	}
}
