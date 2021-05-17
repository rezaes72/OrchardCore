using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Localization;
using OrchardCore.AuditTrail.Models;

namespace OrchardCore.AuditTrail.Services.Models
{
    public class AuditTrailEventDescriptor
    {
        public string Name { get; set; }
        public string FullName { get; set; }
        public LocalizedString LocalizedName { get; set; }
        public LocalizedString Description { get; set; }
        public AuditTrailCategoryDescriptor Category { get; set; }
        public Action<AuditTrailEvent, Dictionary<string, object>> BuildEvent { get; set; }
        public bool IsEnabledByDefault { get; set; }
        public bool IsMandatory { get; set; }

        /// <summary>
        /// Returns a default descriptor based on an event record in case the related provider is no longer registered.
        /// </summary>
        public static AuditTrailEventDescriptor Default(AuditTrailEvent @event)
        {
            var categoryFullName = @event.FullName.Replace(@event.Name, "");

            return new AuditTrailEventDescriptor
            {
                Name = @event.Name,
                FullName = @event.FullName,
                LocalizedName = new LocalizedString(@event.Name, @event.Name),

                Category = new AuditTrailCategoryDescriptor
                {
                    Name = @event.Category,
                    FullName = categoryFullName,
                    ProviderName = categoryFullName.Replace(@event.Category, ""),
                    LocalizedName = new LocalizedString(@event.Category, @event.Category),
                    Events = Enumerable.Empty<AuditTrailEventDescriptor>()
                }
            };
        }
    }
}