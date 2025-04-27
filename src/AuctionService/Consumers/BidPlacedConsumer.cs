using AuctionService.Data;
using Contracts;
using MassTransit;
using Microsoft.EntityFrameworkCore;
namespace Consumers;
public class BidPlacedConsumer : IConsumer<BidPlaced>
{
    private readonly AuctionDbContext _dbContext;

    public BidPlacedConsumer(AuctionDbContext dbContext){
        _dbContext = dbContext;
    }
    public async Task Consume(ConsumeContext<BidPlaced> context)
    {
        Console.WriteLine("--->Consuming bid placed");
        var auction = await _dbContext.Auctions.FindAsync(context.Message.AuctionId);
        if(auction.currentHighBid == null || context.Message.BidStatus.Contains("Accepted") && context.Message.Amount >auction.currentHighBid){
            auction.currentHighBid = context.Message.Amount;
            await _dbContext.SaveChangesAsync();
        }
    }
}