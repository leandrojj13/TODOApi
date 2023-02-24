using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Domain;
using TodoApi.DTOs;
using TodoApi.Models;
using TodoApi.Models.Enums;
using TodoApi.Repositories;

namespace TodoApi.Services
{
    public interface ITradeService
    {
        ServiceResult<IEnumerable<TradeDto>> GetAll(int? userId, string type);

        ServiceResult<TradeDto> GetById(int id);

        Task<ServiceResult<TradeDto>> CreateAsync(TradeDto tradeDto);
    }

    public class TradeService : ITradeService
    {
        private readonly ITradeRepository _tradeRepository;

        public TradeService(ITradeRepository tradeRepository)
        {
            _tradeRepository = tradeRepository;
        }

        public ServiceResult<IEnumerable<TradeDto>> GetAll(int? userId, string type)
        {
            var result = new ServiceResult<IEnumerable<TradeDto>>();

            try
            {
                var isTypeSent = !string.IsNullOrEmpty(type);

                TradeType tradeType = TradeType.Buy;

                if (isTypeSent && !Enum.TryParse(type,  true, out tradeType))
                {
                    result.Messages.Add("Type should be either 'Sell' or 'Buy'");

                    return result;
                }

                var trades = _tradeRepository.GetAll()
                    .Where(t => !userId.HasValue || userId <= 0 || t.UserId == userId)
                    .Where(t => !isTypeSent || t.Type == tradeType)
                    .Select(t => new TradeDto() {
                        Id = t.Id,
                        Price = t.Price, 
                        Shares = t.Shares,
                        Symbol = t.Symbol,
                        Timestamp = t.Timestamp,
                        UserId = t.UserId,
                        Type = t.Type.ToString()
                    })
                    .AsEnumerable();

                result.Success = true;

                result.Result = trades;
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public ServiceResult<TradeDto> GetById(int id)
        {
            var result = new ServiceResult<TradeDto>();

            try
            {
                var trade = _tradeRepository.GetById(id);

                if (trade is null) 
                {
                    return new ServiceResult<TradeDto>()
                    {
                        Success = true
                    };
                }

                var tradeDto = new TradeDto() 
                {
                    Id = trade.Id,
                    Price = trade.Price,
                    Shares = trade.Shares,
                    Symbol = trade.Symbol,
                    Timestamp = trade.Timestamp,
                    UserId = trade.UserId,
                    Type = trade.Type.ToString()
                };

                result.Success = true;

                result.Result = tradeDto;
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }

            return result;
        }

        public async Task<ServiceResult<TradeDto>> CreateAsync(TradeDto tradeDto)
        {
            var result = new ServiceResult<TradeDto>();

            try
            {
                if (tradeDto.Shares < 1 || tradeDto.Shares > 100)
                {
                    result.Messages.Add("Shares values must be within [1, 100]");
                }

                if(!Enum.TryParse(tradeDto.Type, true, out TradeType tradeType))
                {
                    result.Messages.Add("Type should be either 'Sell' or 'Buy'");
                }

                if (result.Messages.Any())
                {
                    return result;
                }

                var trade = new Trade()
                {
                    Price = tradeDto.Price,
                    Shares = tradeDto.Shares,
                    Symbol = tradeDto.Symbol,
                    Type = tradeType,
                    Timestamp = tradeDto.Timestamp,
                    UserId = tradeDto.UserId
                };

                await _tradeRepository.AddAsync(trade);

                tradeDto.Id = trade.Id;

                result.Success = true;

                result.Result = tradeDto;
            }
            catch (Exception ex)
            {
                result.Messages.Add(ex.Message);
            }
                
            return result;
        }
    }
}
