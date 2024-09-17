using CofiApp.Application.Abstractions.Data;
using CofiApp.Application.Abstractions.Emails;
using CofiApp.Contracts.Emails;
using CofiApp.Domain.OrderItemOptionGroups;
using CofiApp.Domain.OrderItemOptions;
using CofiApp.Domain.OrderItems;
using CofiApp.Domain.Orders;
using MassTransit;
using Microsoft.EntityFrameworkCore;

namespace CofiApp.Application.Orders.Commands.ProcessShopOrder
{
    public sealed class OrderProcessingEventConsumer : IConsumer<OrderProcessingEvent>
    {
        private readonly IEmailService _emailService;
        private readonly IDbContext _dbContext;

        public OrderProcessingEventConsumer(IEmailService emailService, IDbContext dbContext)
        {
            _emailService = emailService;
            _dbContext = dbContext;
        }

        public async Task Consume(ConsumeContext<OrderProcessingEvent> context)
        {
            var order = await _dbContext.Set<Order>()
                .AsNoTracking()
                .Include(o => o.User)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.OrderItemOptionGroups)
                    .ThenInclude(oig => oig.OrderItemOptions)
                .Where(o => o.Id == context.Message.Id)
                .Select(o => new
                {
                    o,
                    User = new
                    {
                        o.User.FirstName,
                        o.User.LastName,
                        o.User.Email
                    }
                })
                .SingleOrDefaultAsync();

            if (order is null) return;

            string emailBody = $"Merhaba {order.User.FirstName} {order.User.LastName}, aşağıdaki ürünlerin siparişi alınmıştır:<br><br>";

            foreach (OrderItem orderItem in order.o.OrderItems)
            {
                emailBody += $"{orderItem.ProductName} - {orderItem.ProductPrice} TL<br>";

                foreach (OrderItemOptionGroup orderItemOptionGroup in orderItem.OrderItemOptionGroups)
                {
                    emailBody += $"&nbsp;&nbsp;&nbsp;&nbsp;{orderItemOptionGroup.ProductOptionGroupName}<br>";

                    foreach (OrderItemOption orderItemOption in orderItemOptionGroup.OrderItemOptions)
                    {
                        emailBody += $"&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;{orderItemOption.ProductOptionName} - {orderItemOption.ProductOptionPrice} TL<br>";
                    }
                }
            }

            MailRequest mailRequest = new()
            {
                EmailTo = order.User.Email,
                Subject = "Siparişiniz Alınmıştır",
                Body = emailBody,
                IsHtml = true
            };

            await _emailService.SendEmailAsync(mailRequest);
        }
    }
}
