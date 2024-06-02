using QueryContracts.Queries;
using VIAEventAssociation.Infrastructure.EfcQueries;
using Xunit;

namespace IntegrationTests.Queries;

public class MyEventsPageQueryTest
{
    private readonly ViaEventAssociationContext _context;
    private readonly MyEventsPageQueryHandler _sut;

    public MyEventsPageQueryTest()
    {
        _context = new ViaEventAssociationContext();
        _sut = new MyEventsPageQueryHandler(_context);
    }

    [Fact]
    public async Task HandleAsync_WhenCalled_ShouldReturnMyEventsPageViewAnswer()
    {
        // Arrange
        var query = new MyEventsPageView.Query();

        // Act
        var result = await _sut.HandleAsync(query);

        // Assert
        Assert.NotNull(result);
        Assert.NotNull(result.Guest);
    }
}