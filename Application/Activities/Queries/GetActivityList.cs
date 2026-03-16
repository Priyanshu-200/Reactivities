using System;
using Domain;
using MediatR;
using Persistence;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.Logging;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Activities.DTOs;

public class GetActivityList
{
	public class Query: IRequest<List<ActivityDto>> { }

	public class Handler(AppDbContext context, IMapper mapper): IRequestHandler<Query, List<ActivityDto>>
	{
		public async Task<List<ActivityDto>> Handle(Query request, CancellationToken cancellationToken)
		{
			return await context.Activities
				.ProjectTo<ActivityDto>(mapper.ConfigurationProvider)
				.ToListAsync(cancellationToken);
		}
	}
	
}
