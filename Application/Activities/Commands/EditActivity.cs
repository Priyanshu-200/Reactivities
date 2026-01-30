using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Persistence;
using MediatR;
using AutoMapper;
using System.CodeDom;
using Microsoft.EntityFrameworkCore.Metadata;
/// <summary>
/// Summary description for Class1
/// </summary>
public class EditActivity
{
	public class Command: IRequest
	{
		public required Activity Activity { get; set; }
	}

	public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command>
	{
		public async Task Handle(Command request, CancellationToken cancellationToken)
		{
			var activity = await context.Activities.FindAsync([request.Activity.Id], cancellationToken)?? throw new Exception("Cannot find activity");
			mapper.Map(request.Activity, activity);
			await context.SaveChangesAsync(cancellationToken);
		}
	}
}
