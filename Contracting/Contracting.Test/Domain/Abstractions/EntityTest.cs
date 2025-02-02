using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracting.Domain.Abstractions;

namespace Contracting.Test.Domain.Abstractions;

public class EntityTest
{
    private class TestEntity : Entity
    {
        public TestEntity(Guid id) : base(id) { }
    }

    private record TestDomainEvent : DomainEvent
    {
        public TestDomainEvent() : base() { }
    }

    [Fact]
    public void EntityWhenIdIsEmpty()
    {
        var exception = Assert.Throws<ArgumentException>(() => new TestEntity(Guid.Empty));
        Assert.Equal("Id cannot be empty (Parameter 'id')", exception.Message);
    }

    public void EntityIdIsValid()
    {
        Guid validId = Guid.NewGuid();
        List<DomainEvent> domainEvents = new List<DomainEvent>();

        var entity = new TestEntity(validId);
   

        Assert.Equal(validId, entity.Id);
    }

    [Fact]
    public void AddDomainEventTest()
    {
        var entity = new TestEntity(Guid.NewGuid());
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);

        Assert.Contains(domainEvent, entity.DomainEvents);
    }

    [Fact]
    public void ClearDomainEventsTest()
    {
        var entity = new TestEntity(Guid.NewGuid());
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);

        entity.ClearDomainEvents();

        Assert.Empty(entity.DomainEvents);
    }
}
