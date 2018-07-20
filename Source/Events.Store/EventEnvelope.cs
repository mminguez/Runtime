﻿using System;
using Dolittle.Concepts;
using Dolittle.Dynamic;
using Dolittle.Runtime.Events;
using Dolittle.Events;

namespace Dolittle.Runtime.Events.Store
{
    /// <summary>
    /// A combination of the <see cref="EventId" />,  <see cref="EventMetadata" /> and a <see cref="PropertyBag" /> that is the persisted version of an <see cref="IEvent" />
    /// </summary>
    public class EventEnvelope : Value<EventEnvelope>
    {
        /// <summary>
        /// Instantiates a new instance of an <see cref="EventEnvelope" />
        /// </summary>
        public EventEnvelope(EventId eventId, EventMetadata metadata, PropertyBag @event)
        {
            Metadata = metadata;
            Event = @event;
            Id = eventId;
        }
        /// <summary>
        /// The <see cref="EventMetadata" /> associated with this persisted <see cref="IEvent" />
        /// </summary>
        /// <value></value>
        public EventMetadata Metadata { get; }
        /// <summary>
        /// A <see cref="PropertyBag" /> of the values associated with this <see cref="IEvent" />
        /// </summary>
        /// <value></value>
        public PropertyBag Event { get; }
                /// <summary>
        /// The <see cref="EventId" /> of this <see cref="IEvent" />
        /// </summary>
        /// <value></value>
        public EventId Id { get; }
    }
}