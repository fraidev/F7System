using System;
using MediatR;

namespace F7System.Api.Infrastructure.CQRS
{
    public class BaseCommand: IRequest
    {
        public DateTime TimeStamp { get; set; }

        public BaseCommand()
        {
            TimeStamp = DateTime.Now;
        } 
    }
}