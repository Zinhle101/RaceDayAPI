using System;
using RaceDay.API.Models;
using Xunit;

namespace RaceDay.API.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void RaceEvent_ShouldStoreEventDetails()
        {
            // Arrange
            var raceEvent = new RaceEvent
            {
                RaceEventId = 1,
                EventName = "Johannesburg City Run",
                EventDescription = "A city running event.",
                Location = "Johannesburg",
                EventDate = new DateTime(2026, 11, 15),
                OrganiserId = 1
            };

            // Act and Assert
            Assert.Equal(1, raceEvent.RaceEventId);
            Assert.Equal("Johannesburg City Run", raceEvent.EventName);
            Assert.Equal("A city running event.", raceEvent.EventDescription);
            Assert.Equal("Johannesburg", raceEvent.Location);
            Assert.Equal(new DateTime(2026, 11, 15), raceEvent.EventDate);
            Assert.Equal(1, raceEvent.OrganiserId);
        }
    }
}