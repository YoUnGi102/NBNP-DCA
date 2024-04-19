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
        var result = creator.setRequestedStatus(request, RequestStatus.Accepted);
        bool isSuccess = result.GetObj().Status == RequestStatus.Accepted;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(RequestStatus.Accepted, result.GetObj().Status);
    }
    [Fact]
    public void SetRequestStatus_WhenStatusIsUnanswered_ShouldSetRequestStatusDeclined()
    {
        // Arrange
        var creator = new Creator(1, "creator", "123");
        var request = new Request(RequestStatus.Unanswered);
            

        // Act
        var result = creator.setRequestedStatus(request, RequestStatus.Declined);
        bool isSuccess = result.GetObj().Status == RequestStatus.Declined;
        
        // Assert
        Assert.True(isSuccess);
        Assert.Equal(RequestStatus.Declined, result.GetObj().Status);
    }
    
}