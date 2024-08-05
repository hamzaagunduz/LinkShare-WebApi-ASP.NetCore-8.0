using Link.Application.Common;
using Link.Application.Features.Mediator.Commands.LikeCommands;
using Link.Application.Interfaces;
using Link.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System.Net;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;

namespace Link.Application.Features.Mediator.Handlers.LikeHandlers
{
    public class CreateLikeCommandHandler : IRequestHandler<CreateLikeCommand, CustomResult<string>>
    {
        private readonly IRepository<ProfileComment> _commentRepository;
        private readonly IRepository<Answer> _answerRepository;
        private readonly UserManager<AppUser> _userManager;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CreateLikeCommandHandler(
            IRepository<ProfileComment> commentRepository,
            IRepository<Answer> answerRepository,
            UserManager<AppUser> userManager,
            IHttpContextAccessor httpContextAccessor)
        {
            _commentRepository = commentRepository;
            _answerRepository = answerRepository;
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<CustomResult<string>> Handle(CreateLikeCommand request, CancellationToken cancellationToken)
        {
            var userIdClaim = _httpContextAccessor.HttpContext.User.Claims
                .FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier || c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier");

            if (userIdClaim == null)
            {
                var error = "User ID claim Alanı Tokende Bulunamadı Unauthorized.";
                return new CustomResult<string>(null, HttpStatusCode.Unauthorized, new List<string> { error });
            }

            var loginUserId = int.Parse(userIdClaim.Value);
            var user = await _userManager.FindByIdAsync(userIdClaim.Value);
            if (user == null)
            {
                return new CustomResult<string>(null, HttpStatusCode.NotFound, new List<string> { "Kullanıcı bulunamadı." });
            }

            if (request.EntityType == EntityType.Comment)
            {
                var comment = await _commentRepository.GetByIdAsync(request.Id);
                if (comment == null)
                {
                    return new CustomResult<string>(null, HttpStatusCode.NotFound, new List<string> { "Yorum bulunamadı." });
                }

                comment.Like++;
                await _commentRepository.UpdateAsync(comment);

                return new CustomResult<string>("Beğeni başarıyla eklendi.", HttpStatusCode.OK);
            }
            else if (request.EntityType == EntityType.Answer)
            {
                var answer = await _answerRepository.GetByIdAsync(request.Id);
                if (answer == null)
                {
                    return new CustomResult<string>(null, HttpStatusCode.NotFound, new List<string> { "Cevap bulunamadı." });
                }

                answer.LikeCount++;
                await _answerRepository.UpdateAsync(answer);

                return new CustomResult<string>("Beğeni başarıyla eklendi.", HttpStatusCode.OK);
            }

            return new CustomResult<string>(null, HttpStatusCode.BadRequest, new List<string> { "Geçersiz tür." });
        }
    }
}
