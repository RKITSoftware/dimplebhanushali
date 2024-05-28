using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resume_Builder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AIImage : ControllerBase
    {
        private readonly HttpClient _httpClient;
        private readonly SemaphoreSlim _requestSemaphore;

        public AIImage()
        {
            _httpClient = new HttpClient();
            _requestSemaphore = new SemaphoreSlim(1, 1); // Allow only one request at a time
        }

        [HttpPost("generate")]
        public async Task<IActionResult> GenerateImage([FromBody] string prompt)
        {
            try
            {
                // Acquire the semaphore before making the request
                await _requestSemaphore.WaitAsync();

                // Ensure the Certificates directory exists
                var certificateDirectory = Path.Combine(Directory.GetCurrentDirectory(), "Certificates");
                if (!Directory.Exists(certificateDirectory))
                {
                    Directory.CreateDirectory(certificateDirectory);
                }

                // Tokenize the prompt to extract keywords
                var keywords = await ExtractKeywordsFromPrompt(prompt);

                // Generate image based on the prompt and keywords
                var generatedImage = await GenerateImageFromPrompt(keywords);

                // Generate a file name based on the first four letters of the prompt
                var fileNamePrefix = new string(prompt.Take(4).ToArray()).Replace(" ", "_"); // Replace spaces to avoid issues in file names
                var imagePath = Path.Combine(certificateDirectory, $"certificate_{fileNamePrefix}.png");

                // Save the image to a file
                await System.IO.File.WriteAllBytesAsync(imagePath, generatedImage);

                // Return the image file path as the response
                return Ok(new { ImagePath = imagePath });
            }
            catch (HttpRequestException httpEx)
            {
                return StatusCode((int)httpEx.StatusCode, $"HTTP error occurred: {httpEx.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            finally
            {
                // Release the semaphore after the request is completed
                _requestSemaphore.Release();
            }
        }

        private async Task<string[]> ExtractKeywordsFromPrompt(string prompt)
        {
            // Retry parameters
            const int maxRetries = 5;
            const int baseDelayMilliseconds = 2000; // Initial delay of 2 seconds

            for (int attempt = 1; attempt <= maxRetries; attempt++)
            {
                try
                {
                    // Configure the HTTP client and request
                    var apiUrl = "https://api-inference.huggingface.co/models/distilbert-base-uncased";
                    var requestBody = JsonSerializer.Serialize(new { inputs = prompt });
                    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

                    // Add your Hugging Face API token
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

                    // Send the HTTP request to Hugging Face's API
                    var response = await _httpClient.PostAsync(apiUrl, content);
                    if (response.IsSuccessStatusCode)
                    {
                        // Parse the response to extract keywords
                        var responseContent = await response.Content.ReadAsStringAsync();
                        var keywords = JsonSerializer.Deserialize<string[]>(responseContent);

                        return keywords;
                    }
                    else
                    {
                        var errorContent = await response.Content.ReadAsStringAsync();
                        var errorResponse = JsonSerializer.Deserialize<HuggingFaceErrorResponse>(errorContent);

                        if (errorResponse?.Error.Contains("loading") == true && attempt < maxRetries)
                        {
                            // Wait before retrying
                            await Task.Delay(baseDelayMilliseconds * (int)Math.Pow(2, attempt - 1));
                        }
                        else
                        {
                            throw new HttpRequestException($"Error response from API: {errorContent}", null, response.StatusCode);
                        }
                    }
                }
                catch (HttpRequestException httpEx)
                {
                    if (attempt == maxRetries)
                    {
                        throw;
                    }

                    // Wait before retrying
                    await Task.Delay(baseDelayMilliseconds * (int)Math.Pow(2, attempt - 1));
                }
            }

            // If all retries fail, throw an exception
            throw new Exception("Failed to extract keywords after multiple attempts.");
        }

        //private async Task<byte[]> GenerateImageFromPrompt(string prompt)
        //{
        //    // Configure the HTTP client and request

        //    //var apiUrl = "https://api-inference.huggingface.co/models/CompVis/stable-diffusion-v1-4";
        //    //var apiUrl = "https://api-inference.huggingface.co/models/gpt2";
        //    var apiUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-2";
        //    //var apiUrl = "https://api-inference.huggingface.co/models/EleutherAI/gpt-j-6B";

        //    //var apiUrl = "https://api-inference.huggingface.co/models/EleutherAI/gpt-neo-2.7B";

        //    var requestBody = $"{{\"inputs\":\"{prompt}\"}}";
        //    var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

        //    // Add your Hugging Face API token
        //    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

        //    // Send the HTTP request to Hugging Face's API
        //    var response = await _httpClient.PostAsync(apiUrl, content);
        //    if (!response.IsSuccessStatusCode)
        //    {
        //        var errorContent = await response.Content.ReadAsStringAsync();
        //        throw new HttpRequestException($"Error response from API: {errorContent}", null, response.StatusCode);
        //    }

        //    // Read the response content (generated image)
        //    return await response.Content.ReadAsByteArrayAsync();
        //}

        private async Task<byte[]> GenerateImageFromPrompt(string[] keywords)
        {
            // Configure the HTTP client and request
            var apiUrl = "https://api-inference.huggingface.co/models/stabilityai/stable-diffusion-2";
            var requestBody = JsonSerializer.Serialize(new { inputs = string.Join(" ", keywords) });
            var content = new StringContent(requestBody, Encoding.UTF8, "application/json");

            // Add your Hugging Face API token
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "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");

            // Send the HTTP request to Hugging Face's API
            var response = await _httpClient.PostAsync(apiUrl, content);
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Error response from API: {errorContent}", null, response.StatusCode);
            }

            // Read the response content (generated image)
            return await response.Content.ReadAsByteArrayAsync();
        }

        private class HuggingFaceErrorResponse
        {
            [JsonPropertyName("error")]
            public string Error { get; set; }
        }
    }   
}
