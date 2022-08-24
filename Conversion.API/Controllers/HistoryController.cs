using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Conversion.Core.ApiModels;
using Conversion.DataAccess;
using Conversion.DataAccess.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Conversion.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HistoryController : ControllerBase
    {
        private readonly ILogger<HistoryController> _logger;
        private IRepository<ConversionHistory> _repository;
        private readonly IUnitOfWork<ConversionDbContext> _unitOfWork;
        private readonly IMapper _mapper;

        public HistoryController(ILogger<HistoryController> logger, IRepository<ConversionHistory> repository,
            IUnitOfWork<ConversionDbContext> unitOfWork, IMapper mapper)
        {
            _logger = logger;
            _repository = repository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("{userId}")]
        public async Task<IActionResult> GetHistoryByUser(string userId)
        {
            var userHistory = await _repository.LazyGet(h => h.UserId == userId).ToListAsync();
            
            return Ok(_mapper.Map<List<HistoryResponse>>(userHistory));
        }

        [HttpPost]
        public async Task<IActionResult> SaveHistory(HistoryRequest request)
        {
            var conversionHistory = _mapper.Map<ConversionHistory>(request);
            conversionHistory.CreatedAt = DateTime.Now;

            _repository.Insert(conversionHistory);

            await _unitOfWork.CommitAsync();

            return Ok(_mapper.Map<List<HistoryResponse>>(conversionHistory));
        }
    }
}