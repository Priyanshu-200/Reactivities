using System;
using System.Threading;
using System.Threading.Tasks;
using Domain;
using Persistence;
using MediatR;
using Application.Activities.DTOs;
using AutoMapper;
using FluentValidation;
using Application.Core;

public class CreateActivity
{
	public class Command: IRequest<Result<string>>
	{
		public required CreateActivityDto ActivityDto { get; set; }
	}

	public class Handler(AppDbContext context, IMapper mapper) : IRequestHandler<Command, Result<String>>
	{
		public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
		{
			var activity = mapper.Map<Activity>(request.ActivityDto);
			context.Activities.Add(activity);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return Result<string>.Failure("Failed to update the activity", 400);
            return Result<string>.Success(activity.Id);
        }
	}
}
