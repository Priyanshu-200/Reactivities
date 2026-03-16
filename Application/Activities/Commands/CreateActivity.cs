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
using Application.Interfaces;

public class CreateActivity
{
	public class Command: IRequest<Result<string>>
	{
		public required CreateActivityDto ActivityDto { get; set; }
	}

	public class Handler(AppDbContext context, IMapper mapper, IUserAccessor userAccessor) : IRequestHandler<Command, Result<String>>
	{
		public async Task<Result<string>> Handle(Command request, CancellationToken cancellationToken)
		{
			var user = await userAccessor.GetUserAsync();
			var activity = mapper.Map<Activity>(request.ActivityDto);
			context.Activities.Add(activity);
			var attendee = new ActivityAttendee
			{
				ActivityId = activity.Id,
				UserId = user.Id,
				IsHost = true
			};
			activity.Attendees.Add(attendee);
            var result = await context.SaveChangesAsync(cancellationToken) > 0;
            if (!result) return Result<string>.Failure("Failed to update the activity", 400);
            return Result<string>.Success(activity.Id);
        }
	}
}
