using System;
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
    public void EntityWithEmptyId()
    {
        var exception = Assert.Throws<ArgumentException>(() => new TestEntity(Guid.Empty));

        Assert.NotNull(exception);
        Assert.Equal("Id cannot be empty (Parameter 'id')", exception.Message);
    }

    [Fact]
    public void EntityWithValidId()
    {
        Guid validId = Guid.NewGuid();
        List<DomainEvent> domainEvents = new List<DomainEvent>();

        var entity = new TestEntity(validId);
   
        Assert.NotNull(entity);
        Assert.Equal(validId, entity.Id);
    }

    [Fact]
    public void AddDomainEventTest()
    {
        var entity = new TestEntity(Guid.NewGuid());
        var domainEvent = new TestDomainEvent();

        entity.AddDomainEvent(domainEvent);

        Assert.NotNull(entity);
        Assert.NotNull(domainEvent);
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
