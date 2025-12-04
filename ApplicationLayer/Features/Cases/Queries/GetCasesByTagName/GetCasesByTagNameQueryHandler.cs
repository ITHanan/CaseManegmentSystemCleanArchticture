using ApplicationLayer.Features.Cases.Dtos;
using ApplicationLayer.Interfaces;
using AutoMapper;
using DomainLayer.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLayer.Features.Cases.Queries.GetCasesByTagName
{
    public class GetCasesByTagNameQueryHandler
    : IRequestHandler<GetCasesByTagNameQuery, OperationResult<List<CaseDto>>>
    {
        private readonly ICaseRepository _caseRepo;
        private readonly IMapper _mapper;

        public GetCasesByTagNameQueryHandler(ICaseRepository caseRepo, IMapper mapper)
        {
            _caseRepo = caseRepo;
            _mapper = mapper;
        }

        public async Task<OperationResult<List<CaseDto>>> Handle(GetCasesByTagNameQuery request, CancellationToken cancellationToken)
        {
            var cases = await _caseRepo.GetCasesByTagNameAsync(request.TagName);

            var dtoList = _mapper.Map<List<CaseDto>>(cases);

            return OperationResult<List<CaseDto>>.Success(dtoList);
        }
    }
}
