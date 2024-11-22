using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace Eventify.Tests.Acceptance.Configuration;

public record TestHttpClient(
    HttpClient HttpClient,
    ErrorDriver ErrorDriver,
    JsonSerializerOptions Options
)
{
    public Task Post(string path, object? body = null) =>
        TryExecute(() => HttpClient.PostAsync(path, body.MapIfNotNull(ToStringContent)));

    public Task<T?> Post<T>(string path, object? body = null) =>
        TryExecute<T>(() => HttpClient.PostAsync(path, body.MapIfNotNull(ToStringContent)));

    public Task Post(string path, HttpContent content) =>
        TryExecute(() => HttpClient.PostAsync(path, content));

    public Task Put(string path, object? body = null) =>
        TryExecute(() => HttpClient.PutAsync(path, body.MapIfNotNull(ToStringContent)));
    public Task Patch(string path, object? body = null) =>
        TryExecute(() => HttpClient.PatchAsync(path, body.MapIfNotNull(ToStringContent)));

    public Task Delete(string path, object? body = null) =>
        TryExecute(() =>
        {
            if (body is null)
            {
                return HttpClient.DeleteAsync(path);
            }
            var request = new HttpRequestMessage(HttpMethod.Delete, path);
            request.Content = new StringContent(JsonSerializer.Serialize(body), Encoding.UTF8, "application/json");
            return HttpClient.SendAsync(request);
        });

    public Task<string?> GetRaw(string path, IDictionary<string, object?>? queryParameters = null) =>
        ErrorDriver.TryExecute(
            async () =>
            {
                var response = await HttpClient.GetAsync(path.AddQueryParameters(queryParameters));

                await ThrowIfError(path, response);

                return await response.Content.ReadAsStringAsync();
            }
        );

    public Task<T?> Get<T>(string path, IDictionary<string, object?>? queryParameters = null) =>
        ErrorDriver.TryExecute(
            async () =>
            {
                var response = await HttpClient.GetAsync(path.AddQueryParameters(queryParameters));

                await ThrowIfError(path, response);

                var json = await response.Content.ReadAsStringAsync();

                return Deserialize<T>(json);
            }
        );

    // ----- Private

    private async Task TryExecute(Func<Task<HttpResponseMessage>> executeRequest) =>
        await ErrorDriver.TryExecute(
            async () =>
            {
                var response = await executeRequest();

                await ThrowIfError(response.RequestMessage?.RequestUri?.AbsolutePath ?? string.Empty, response);
            });

    private Task<T?> TryExecute<T>(Func<Task<HttpResponseMessage>> executeRequest) =>
        ErrorDriver.TryExecute(
            async () =>
            {
                var response = await executeRequest();

                await ThrowIfError(response.RequestMessage?.RequestUri?.ToString() ?? string.Empty, response);

                var json = await response.Content.ReadAsStringAsync();

                return Deserialize<T>(json);
            });

    private StringContent ToStringContent(object body)
    {
        var json = JsonSerializer.Serialize(body, Options);

        return new StringContent(json, Encoding.UTF8, new MediaTypeHeaderValue("application/json"));
    }

    private async Task ThrowIfError(string path, HttpResponseMessage response)
    {
        if (response.IsSuccessStatusCode)
        {
            return;
        }

        var problemDetailsJson = await response.Content.ReadAsStringAsync();

        if (string.IsNullOrWhiteSpace(problemDetailsJson))
        {
            throw HttpException.From(path, response);
        }

        var problemDetails = Deserialize<ProblemDetails>(problemDetailsJson);

        throw HttpException.From(path, problemDetails);
    }

    private T Deserialize<T>(string json) =>
        JsonSerializer.Deserialize<T>(json, Options)
        ?? throw new InvalidOperationException($"Unable to deserialize the response to {typeof(T)}");

    
}