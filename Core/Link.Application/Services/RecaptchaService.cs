using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

public class RecaptchaService
{
    private readonly IConfiguration _configuration;
    private readonly HttpClient _httpClient;

    public RecaptchaService(IConfiguration configuration, HttpClient httpClient)
    {
        _configuration = configuration;
        _httpClient = httpClient;
    }

    public async Task<bool> VerifyRecaptchaAsync(string token)
    {
        
        var secretKey = _configuration["RecaptchaSettings:SecretKey"];
        var response = await _httpClient.GetStringAsync($"https://www.google.com/recaptcha/api/siteverify?secret={secretKey}&response={token}");
        var recaptchaResponse = JsonConvert.DeserializeObject<RecaptchaResponse>(response);

        return recaptchaResponse.Success;
    }
}

public class RecaptchaResponse
{
    [JsonProperty("success")]
    public bool Success { get; set; }

    [JsonProperty("challenge_ts")]
    public string ChallengeTimestamp { get; set; }

    [JsonProperty("hostname")]
    public string Hostname { get; set; }

    [JsonProperty("error-codes")]
    public List<string> ErrorCodes { get; set; }
}
