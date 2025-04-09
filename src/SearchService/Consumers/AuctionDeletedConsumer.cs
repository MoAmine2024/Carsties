using AutoMapper;
using Contracts;
using MassTransit;
using MongoDB.Entities;
using Polly;
using SearchService.Models;

namespace SearchService.Consumers;
public class AuctionDeletedConsumer : IConsumer<AuctionDeleted>
{
    public async Task Consume(ConsumeContext<AuctionDeleted> context)
    {
        Console.WriteLine("--> Consumming auction deleted: " + context.Message.Id);
        var result = await DB.DeleteAsync<Item>(context.Message.Id);
        if( !result.IsAcknowledged) throw new MessageException(typeof(AuctionDeleted),"Cannot sell cars with name of Foo");
    }

}