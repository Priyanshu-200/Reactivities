

using Application.Activities.DTOs;
using FluentValidation;

namespace Application.Core.Validators
{
    public class CreateActivityValidator: BaseActivityValidator<CreateActivity.Command, CreateActivityDto>
    {
        public CreateActivityValidator(): base(x=>x.ActivityDto)
        {
           
        }
    }
}
