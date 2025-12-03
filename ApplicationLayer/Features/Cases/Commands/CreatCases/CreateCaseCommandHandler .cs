using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using DomainLayer.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Commands.CreatCases
{
    public class CreateCaseCommandHandler
     : IRequestHandler<CreateCaseCommand, OperationResult<CaseDto>>
    {
        private readonly IGenericRepository<Case> _repo;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUser;

        public CreateCaseCommandHandler(
            IGenericRepository<Case> repo,
            IMapper mapper,
            ICurrentUserService currentUser)
        {
            _repo = repo;
            _mapper = mapper;
            _currentUser = currentUser;
        }

        public async Task<OperationResult<CaseDto>> Handle(CreateCaseCommand request, CancellationToken cancellationToken)
        {
            var entity = new Case
            {
                Title = request.Title,
                Description = request.Description,
                ClientId = request.ClientId,
                AssignedToUserId = request.AssignedToUserId,

                CreatedByUserId = _currentUser.UserId,   
                CreatedAt = DateTime.UtcNow,
                Status = CaseStatus.Open
            };

            var result = await _repo.AddAsync(entity, cancellationToken);

            if (!result.IsSuccess)
                return OperationResult<CaseDto>.Failure(result.ErrorMessage!);

            var dto = _mapper.Map<CaseDto>(result.Data!);
            return OperationResult<CaseDto>.Success(dto);
        }
    }

}
