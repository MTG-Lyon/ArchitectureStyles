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
        errorDriver.AssertErrorOccurredWithMessage(errorMessage);
    
    [Then(@"an (.*) error occurred")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue(string statusText) =>
        ThenAnErrorIsThrownWithCodeAndMessage(statusText);

    [Then(@"an (.*) error occurred with message ""(.*)""")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue4(string statusText, string message) =>
        ThenAnErrorIsThrownWithCodeAndMessage(statusText, message: message);

    [Then(@"an (.*) error occurred with type ([^ ]*)")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue2(string statusText, string type) =>
        ThenAnErrorIsThrownWithCodeAndMessage(statusText, type: type);

    [Then(@"an (.*) error occurred with type ([^ ]*) and message ""(.*)""")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue3(string statusText, string type, string message) =>
        ThenAnErrorIsThrownWithCodeAndMessage(statusText, type, message);

    [Then(@"an (.*) error occurred with type ([^ ]*) and extension ""(.*)"" to (.*)")]
    public void ThenUneErreurDeRessourceIntrouvableEstSurvenue2(string statusText, string type, string extensionKey, string extensionValue) =>
        ThenAnErrorIsThrownWithCodeAndMessage(statusText, type: type, extensionKey: extensionKey, extensionValue: extensionValue);

    // [Then(
    //     "une erreur (?<statusText>.*) est survenue"
    //     + @"(?: avec)?(?: le type (?<errorType>\S*))?"
    //     + @"(?: avec| et)?(?: le message ""?(?<message>[^""]*)""?)?"
    //     + @"(?: avec)?(?: l'extension ""?(?<extensionKey>[^""]*)""? à (?<extensionValue>[^""]*))?"
    // )]
    private void ThenAnErrorIsThrownWithCodeAndMessage(
        string statusText,
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
                    $"Expected to have {expectedStatusCode} status code but have {error.HttpStatusCode}");
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

    [Then(@"aucune erreur n'est survenue")]
    public void ThenNoErrorOccurred() => errorDriver.AssertNoErrorOccurred();

    private static HttpStatusCode ParseHttpStatusCode(string statusString)
    {
        var status = statusString switch
        {
            "d'autorisation" => HttpStatusCode.Unauthorized,
            "de format" => HttpStatusCode.BadRequest,
            "de conflit" => HttpStatusCode.Conflict,
            "d'interdiction" => HttpStatusCode.Forbidden,
            "de ressource introuvable" => HttpStatusCode.NotFound,
            _ => throw new InvalidOperationException($"Reqnroll: unknown status code '{statusString}'.")
        };
        return status;
    }
}