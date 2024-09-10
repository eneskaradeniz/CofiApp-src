using CofiApp.Application.Abstractions.Common;

namespace CofiApp.Infrastructure.Common
{
    internal class MachineDateTime : IDateTime
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
