namespace CRM.Deal.Service.Client
{
  using System.Globalization;
  using System.Text;
  using CRM.Deal.Service.Dtos;
  using Newtonsoft.Json;
  using System = global::System;

  [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
  public partial class AccountClient
  {
    private string _baseUrl = "";
    private System.Net.Http.HttpClient _httpClient;
    private System.Lazy<Newtonsoft.Json.JsonSerializerSettings> _settings;

    public AccountClient(string baseUrl, System.Net.Http.HttpClient httpClient)
    {
      BaseUrl = baseUrl;
      _httpClient = httpClient;
      _settings = new System.Lazy<Newtonsoft.Json.JsonSerializerSettings>(CreateSerializerSettings, true);
    }

    private Newtonsoft.Json.JsonSerializerSettings CreateSerializerSettings()
    {
      var settings = new Newtonsoft.Json.JsonSerializerSettings();
      UpdateJsonSerializerSettings(settings);
      return settings;
    }

    public string BaseUrl
    {
      get { return _baseUrl; }
      set { _baseUrl = value; }
    }

    protected Newtonsoft.Json.JsonSerializerSettings JsonSerializerSettings { get { return _settings.Value; } }

    partial void UpdateJsonSerializerSettings(Newtonsoft.Json.JsonSerializerSettings settings);

    partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, string url);
    partial void PrepareRequest(System.Net.Http.HttpClient client, System.Net.Http.HttpRequestMessage request, System.Text.StringBuilder urlBuilder);
    partial void ProcessResponse(System.Net.Http.HttpClient client, System.Net.Http.HttpResponseMessage response);

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual System.Threading.Tasks.Task<System.Collections.Generic.ICollection<AccountDto>> AccountsAllAsync()
    {
      return AccountsAllAsync(System.Threading.CancellationToken.None);
    }

    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual async System.Threading.Tasks.Task<System.Collections.Generic.ICollection<AccountDto>> AccountsAllAsync(System.Threading.CancellationToken cancellationToken)
    {
      var urlBuilder_ = new System.Text.StringBuilder();
      urlBuilder_.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/accounts");

      var client_ = _httpClient;
      var disposeClient_ = false;
      try
      {
        using (var request_ = new System.Net.Http.HttpRequestMessage())
        {
          request_.Method = new System.Net.Http.HttpMethod("GET");
          request_.Headers.Accept.Add(System.Net.Http.Headers.MediaTypeWithQualityHeaderValue.Parse("text/plain"));

          PrepareRequest(client_, request_, urlBuilder_);

          var url_ = urlBuilder_.ToString();
          request_.RequestUri = new System.Uri(url_, System.UriKind.RelativeOrAbsolute);

          PrepareRequest(client_, request_, url_);

          var response_ = await client_.SendAsync(request_, System.Net.Http.HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
          var disposeResponse_ = true;
          try
          {
            var headers_ = System.Linq.Enumerable.ToDictionary(response_.Headers, h_ => h_.Key, h_ => h_.Value);
            if (response_.Content != null && response_.Content.Headers != null)
            {
              foreach (var item_ in response_.Content.Headers)
                headers_[item_.Key] = item_.Value;
            }

            ProcessResponse(client_, response_);

            var status_ = (int)response_.StatusCode;
            if (status_ == 200)
            {
              var objectResponse_ = await ReadObjectResponseAsync<System.Collections.Generic.ICollection<AccountDto>>(response_, headers_, cancellationToken).ConfigureAwait(false);
              if (objectResponse_.Object == null)
              {
                throw new ApiException("Response was null which was not expected.", status_, objectResponse_.Text, headers_, null);
              }
              return objectResponse_.Object;
            }
            else
            {
              var responseData_ = response_.Content == null ? null : await response_.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("The HTTP status code of the response was not expected (" + status_ + ").", status_, responseData_, headers_, null);
            }
          }
          finally
          {
            if (disposeResponse_)
              response_.Dispose();
          }
        }
      }
      finally
      {
        if (disposeClient_)
          client_.Dispose();
      }
    }

    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual Task<AccountDto> AccountsGETAsync(System.Guid id)
    {
      return AccountsGETAsync(id, System.Threading.CancellationToken.None);
    }

    /// <param name="cancellationToken">A cancellation token that can be used by other objects or threads to receive notice of cancellation.</param>
    /// <returns>Success</returns>
    /// <exception cref="ApiException">A server side error occurred.</exception>
    public virtual async Task<AccountDto> AccountsGETAsync(Guid id, CancellationToken cancellationToken)
    {
      if (id == null)
        throw new ArgumentNullException("id");

      var urlBuilder = new StringBuilder();
      urlBuilder.Append(BaseUrl != null ? BaseUrl.TrimEnd('/') : "").Append("/accounts/{id}");
      urlBuilder.Replace("{id}", Uri.EscapeDataString(ConvertToString(id, CultureInfo.InvariantCulture)));

      var client = _httpClient;
      var disposeClient = false;

      try
      {
        using (var request = new HttpRequestMessage())
        {
          request.Method = new HttpMethod("GET");

          PrepareRequest(client, request, urlBuilder);

          var url = urlBuilder.ToString();
          request.RequestUri = new Uri(url, UriKind.RelativeOrAbsolute);

          PrepareRequest(client, request, url);

          var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
          var disposeResponse = true;

          try
          {
            var headers = Enumerable.ToDictionary(response.Headers, h => h.Key, h => h.Value);
            if (response.Content != null && response.Content.Headers != null)
            {
              foreach (var item in response.Content.Headers)
                headers[item.Key] = item.Value;
            }

            ProcessResponse(client, response);

            var status = (int)response.StatusCode;
            if (status == 200)
            {
              // Assuming you can deserialize the response content to an AccountDto
              var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
              var accountDto = JsonConvert.DeserializeObject<AccountDto>(responseData);
              return accountDto;
            }
            else
            {
              var responseData = response.Content == null ? null : await response.Content.ReadAsStringAsync().ConfigureAwait(false);
              throw new ApiException("The HTTP status code of the response was not expected (" + status + ").", status, responseData, headers, null);
            }
          }
          finally
          {
            if (disposeResponse)
              response.Dispose();
          }
        }
      }
      finally
      {
        if (disposeClient)
          client.Dispose();
      }
    }

    protected struct ObjectResponseResult<T>
    {
      public ObjectResponseResult(T responseObject, string responseText)
      {
        this.Object = responseObject;
        this.Text = responseText;
      }

      public T Object { get; }

      public string Text { get; }
    }

    public bool ReadResponseAsString { get; set; }

    protected virtual async System.Threading.Tasks.Task<ObjectResponseResult<T>> ReadObjectResponseAsync<T>(System.Net.Http.HttpResponseMessage response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Threading.CancellationToken cancellationToken)
    {
      if (response == null || response.Content == null)
      {
        return new ObjectResponseResult<T>(default(T), string.Empty);
      }

      if (ReadResponseAsString)
      {
        var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        try
        {
          var typedBody = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(responseText, JsonSerializerSettings);
          return new ObjectResponseResult<T>(typedBody, responseText);
        }
        catch (Newtonsoft.Json.JsonException exception)
        {
          var message = "Could not deserialize the response body string as " + typeof(T).FullName + ".";
          throw new ApiException(message, (int)response.StatusCode, responseText, headers, exception);
        }
      }
      else
      {
        try
        {
          using (var responseStream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false))
          using (var streamReader = new System.IO.StreamReader(responseStream))
          using (var jsonTextReader = new Newtonsoft.Json.JsonTextReader(streamReader))
          {
            var serializer = Newtonsoft.Json.JsonSerializer.Create(JsonSerializerSettings);
            var typedBody = serializer.Deserialize<T>(jsonTextReader);
            return new ObjectResponseResult<T>(typedBody, string.Empty);
          }
        }
        catch (Newtonsoft.Json.JsonException exception)
        {
          var message = "Could not deserialize the response body stream as " + typeof(T).FullName + ".";
          throw new ApiException(message, (int)response.StatusCode, string.Empty, headers, exception);
        }
      }
    }

    private string ConvertToString(object value, System.Globalization.CultureInfo cultureInfo)
    {
      if (value == null)
      {
        return "";
      }

      if (value is System.Enum)
      {
        var name = System.Enum.GetName(value.GetType(), value);
        if (name != null)
        {
          var field = System.Reflection.IntrospectionExtensions.GetTypeInfo(value.GetType()).GetDeclaredField(name);
          if (field != null)
          {
            var attribute = System.Reflection.CustomAttributeExtensions.GetCustomAttribute(field, typeof(System.Runtime.Serialization.EnumMemberAttribute))
                as System.Runtime.Serialization.EnumMemberAttribute;
            if (attribute != null)
            {
              return attribute.Value != null ? attribute.Value : name;
            }
          }

          var converted = System.Convert.ToString(System.Convert.ChangeType(value, System.Enum.GetUnderlyingType(value.GetType()), cultureInfo));
          return converted == null ? string.Empty : converted;
        }
      }
      else if (value is bool)
      {
        return System.Convert.ToString((bool)value, cultureInfo).ToLowerInvariant();
      }
      else if (value is byte[])
      {
        return System.Convert.ToBase64String((byte[])value);
      }
      else if (value.GetType().IsArray)
      {
        var array = System.Linq.Enumerable.OfType<object>((System.Array)value);
        return string.Join(",", System.Linq.Enumerable.Select(array, o => ConvertToString(o, cultureInfo)));
      }

      var result = System.Convert.ToString(value, cultureInfo);
      return result == null ? "" : result;
    }
  }



  [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
  public partial class ApiException : System.Exception
  {
    public int StatusCode { get; private set; }

    public string Response { get; private set; }

    public System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> Headers { get; private set; }

    public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, System.Exception innerException)
        : base(message + "\n\nStatus: " + statusCode + "\nResponse: \n" + ((response == null) ? "(null)" : response.Substring(0, response.Length >= 512 ? 512 : response.Length)), innerException)
    {
      StatusCode = statusCode;
      Response = response;
      Headers = headers;
    }

    public override string ToString()
    {
      return string.Format("HTTP Response: \n\n{0}\n\n{1}", Response, base.ToString());
    }
  }

  [System.CodeDom.Compiler.GeneratedCode("NSwag", "13.20.0.0 (NJsonSchema v10.9.0.0 (Newtonsoft.Json v13.0.0.0))")]
  public partial class ApiException<TResult> : ApiException
  {
    public TResult Result { get; private set; }

    public ApiException(string message, int statusCode, string response, System.Collections.Generic.IReadOnlyDictionary<string, System.Collections.Generic.IEnumerable<string>> headers, TResult result, System.Exception innerException)
        : base(message, statusCode, response, headers, innerException)
    {
      Result = result;
    }
  }

}