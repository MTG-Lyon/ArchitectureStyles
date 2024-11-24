using System.Net;
using System.Text.Json;
using System.Text.Json.Nodes;
using Eventify.Tests.Acceptance.Configuration;
using FluentAssertions;
using FluentAssertions.Execution;
using Reqnroll;

namespace Eventify.Tests.Acceptance.Steps;

[Binding]
public class ErrorSteps(ErrorDriver errorDriver)
{
    [Then(@"an error occurred with message ""(.*)""")]
    public void ThenAnErrorOccurredWithTheMessage(string errorMessage) =>
        CheckError(message: errorMessage);
    
    [Then(@"an? (.*) error occurred")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue(string statusText) =>
        CheckError(statusText);

    [Then(@"an? (.*) error occurred with message ""(.*)""")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue4(string statusText, string message) =>
        CheckError(statusText, message: message);

    [Then(@"an? (.*) error occurred with type ([^ ]*)")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue2(string statusText, string type) =>
        CheckError(statusText, type: type);

    [Then(@"an? (.*) error occurred with type ([^ ]*) and message ""(.*)""")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue3(string statusText, string type, string message) =>
        CheckError(statusText, type, message);

    [Then(@"an? (.*) error occurred with type ([^ ]*) and extension ""(.*)"" to (.*)")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue2(string statusText, string type, string extensionKey, string extensionValue) =>
        CheckError(statusText, type: type, extensionKey: extensionKey, extensionValue: extensionValue);


    private void CheckError(
        string statusText = "",
        string type = "",
        string message = "",
        string extensionKey = "",
        string extensionValue = ""
    )
    {
        var error = errorDriver.GetLastError();

        if (!string.IsNullOrWhiteSpace(type))
        {
            error.ProblemDetails.Type.Should().Be(type);
        }

        if (!string.IsNullOrWhiteSpace(statusText))
        {
            var expectedStatusCode = ParseHttpStatusCode(statusText);
            if (error.HttpStatusCode != expectedStatusCode)
            {
                throw new AssertionFailedException(
                    $"Expected to have {expectedStatusCode} status code but have {error.HttpStatusCode}"
                );
            }
        }

        if (!string.IsNullOrWhiteSpace(message))
        {
            error.ProblemDetails.Title.Should().NotBeNull();
            error.ProblemDetails.Title.Should().MatchRegex(message);
        }

        if (!string.IsNullOrWhiteSpace(extensionKey))
        {
            var actualValue = error.ProblemDetails.Extensions[extensionKey];
            actualValue.Should().NotBeNull();
            var jsonElement = (JsonElement)actualValue!;
            jsonElement.GetString().Should().Be(extensionValue);
        }
    }

    [Then(@"une erreur de format est survenue avec l'erreur de validation ""(.*)""")]
    public void ThenUneErreurDeFormatEstSurvenueAvecLerreurDeValidation(string validationError)
    {
        var error = errorDriver.GetLastError();

        error.HttpStatusCode.Should().Be(HttpStatusCode.BadRequest);

        var actualValue = error.ProblemDetails.Extensions["errors"];
        actualValue.Should().NotBeNull();

        var jsonElement = (JsonElement)actualValue!;
        var obj = JsonObject.Create(jsonElement);
        var firstValidationErrorMessage = obj!.First().Value![0]!.GetValue<string>();

        firstValidationErrorMessage.Should().Be(validationError);
    }

    [Then(@"no error occurred")]
    public void ThenNoErrorOccurred() => errorDriver.AssertNoErrorOccurred();

    private static HttpStatusCode ParseHttpStatusCode(string statusString) =>
        Enum.Parse<HttpStatusCode>(statusString.Replace(" ", ""), true);
}