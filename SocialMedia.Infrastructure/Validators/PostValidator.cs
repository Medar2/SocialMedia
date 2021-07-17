using FluentValidation;
using SocialMedia.Core.DTOs;
using System;

namespace SocialMedia.Infrastructure.Validators
{
    public class PostValidator : AbstractValidator<PostDto>
    {
        public PostValidator()
        {
            RuleFor(post => post.Description)
                .NotNull()
                .WithMessage("La Descripcion no puede ser nula");

            RuleFor(post => post.Description)
                .Length(5, 500)
                .WithMessage("La Descripcion no puede ser nula");

            RuleFor(post => post.Date)
                .NotNull()
                .LessThan(DateTime.Now)
                .WithMessage("La fecha no puede ser nula");
        }
    }
}
