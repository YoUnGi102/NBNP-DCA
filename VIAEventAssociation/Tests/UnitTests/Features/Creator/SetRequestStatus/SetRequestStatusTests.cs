using Domain.Common.Entities;
using Xunit;

namespace UnitTests.Features.Creator.SetRequestStatus;
using Domain.Aggregates.Creator;
using Domain.Common.Enums;
using Domain.Common.Entities;
public class SetRequestStatusTests
{
    [Fact]
    public void SetRequestStatus_WhenStatusIsUnanswered_ShouldSetRequestStatusAccepted()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var request = new Request(RequestStatus.Unanswered);
            

        // Act
        creator.setRequestedStatus(request, RequestStatus.Accepted);
        
        // Assert
        Assert.Equal(RequestStatus.Accepted, request.status);
    }
    [Fact]
    public void SetRequestStatus_WhenStatusIsnanswered_ShouldSetRequestStatusDeclined()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var request = new Request(RequestStatus.Unanswered);
            

        // Act
        creator.setRequestedStatus(request, RequestStatus.Declined);
        
        // Assert
        Assert.Equal(RequestStatus.Declined, request.status);
    }
    
}