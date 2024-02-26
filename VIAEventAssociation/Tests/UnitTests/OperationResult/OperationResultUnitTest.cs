using VIAEventAssociation.Core.Tools.OperationResult.Result;

namespace UnitTests.OperationResult;

using Xunit;

public class OperationResultUnitTest
{
    private string[] error_messages { get; set; }
    private HTTPCodes[] error_codes { get; set; }
    
    private string[] success_messages { get; set; }
    private HTTPCodes[] success_codes { get; set; }
    public OperationResultUnitTest()
    {
        error_messages = new string[] { "Error 1", "Error 2" };
        error_codes = new HTTPCodes[] { HTTPCodes.BadRequest, HTTPCodes.Unauthorized };
        success_messages = new string[] { "Success 1", "Success 2" };
        success_codes = new HTTPCodes[] { HTTPCodes.OK, HTTPCodes.Created };
    }

    [Fact]
    public void CreateEmptySuccess()
    {
        Result<int> result = ResultSuccess<int>.CreateEmptyResult();
        Assert.NotNull(result);
        Assert.Equal(0, result.GetObj());
        Assert.Null(result.GetMessages());
    }
    
    [Fact]
    public void CreateEmptyFailure()
    {
        Result<int> result = ResultFailure<int>.CreateEmptyResult();
        Assert.NotNull(result);
        Assert.Equal(0, result.GetObj());
        Assert.Null(result.GetMessages());
    }
    
    [Fact]
    public void CreateSimpleSuccess()
    {
        Result<int> result = ResultSuccess<int>.CreateSimpleResult(20);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.Null(result.GetMessages());
    }
    
    [Fact]
    public void CreateSimpleFailure()
    {
        Result<int> result = ResultFailure<int>.CreateSimpleResult(20);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.Null(result.GetMessages());
    }
    
    [Fact]
    public void CreateMessageSuccess()
    {
        Result<int> result = ResultSuccess<int>.CreateMessageResult(20, success_messages);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal("Success 1", result.GetMessages()[0].GetMessage());
        Assert.Equal("Success 2", result.GetMessages()[1].GetMessage());
    }
    
    [Fact]
    public void CreateMessageFailure()
    {
        Result<int> result = ResultFailure<int>.CreateMessageResult(20, error_messages);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal("Error 1", result.GetMessages()[0].GetMessage());
        Assert.Equal("Error 2", result.GetMessages()[1].GetMessage());
    }
    
    [Fact]
    public void CreateHTTPSuccess()
    {
        Result<int> result = ResultSuccess<int>.CreateHTTPResult(20, success_codes);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal(HTTPCodes.OK, result.GetMessages()[0].GetCode());
        Assert.Equal(HTTPCodes.Created, result.GetMessages()[1].GetCode());
    }
    
    [Fact]
    public void CreateHTTPFailure()
    {
        Result<int> result = ResultFailure<int>.CreateHTTPResult(20, error_codes);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal(HTTPCodes.BadRequest, result.GetMessages()[0].GetCode());
        Assert.Equal(HTTPCodes.Unauthorized, result.GetMessages()[1].GetCode());
    }
    
    [Fact]
    public void CreateSuccess()
    {
        Result<int> result = ResultSuccess<int>.CreateResult(20, success_codes, success_messages);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal("Success 1", result.GetMessages()[0].GetMessage());
        Assert.Equal("Success 2", result.GetMessages()[1].GetMessage());
        Assert.Equal(HTTPCodes.OK, result.GetMessages()[0].GetCode());
        Assert.Equal(HTTPCodes.Created, result.GetMessages()[1].GetCode());
    }
    
    [Fact]
    public void CreateFailure()
    {
        Result<int> result = ResultFailure<int>.CreateResult(20, error_codes, error_messages);
        Assert.NotNull(result);
        Assert.Equal(20, result.GetObj());
        Assert.NotNull(result.GetMessages());
        Assert.Equal("Error 1", result.GetMessages()[0].GetMessage());
        Assert.Equal("Error 2", result.GetMessages()[1].GetMessage());
        Assert.Equal(HTTPCodes.BadRequest, result.GetMessages()[0].GetCode());
        Assert.Equal(HTTPCodes.Unauthorized, result.GetMessages()[1].GetCode());
    }
}