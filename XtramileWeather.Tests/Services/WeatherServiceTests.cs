using Moq;
using Moq.Protected;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using XtramileWeather.Services;
using Xunit;

namespace XtramileWeather.Tests.Services
{
    public class WeatherServiceTests
    {
        //Mock API response
        public static string MockApiResponse = "{\r\n    \"coord\": {\r\n        \"lon\": -0.1257,\r\n        \"lat\": 51.5085\r\n    },\r\n    \"weather\": [\r\n        {\r\n            \"id\": 803,\r\n            \"main\": \"Clouds\",\r\n            \"description\": \"broken clouds\",\r\n            \"icon\": \"04d\"\r\n        }\r\n    ],\r\n    \"base\": \"stations\",\r\n    \"main\": {\r\n        \"temp\": 47.08,\r\n        \"feels_like\": 41.56,\r\n        \"temp_min\": 45.86,\r\n        \"temp_max\": 48.27,\r\n        \"pressure\": 1005,\r\n        \"humidity\": 87\r\n    },\r\n    \"visibility\": 10000,\r\n    \"wind\": {\r\n        \"speed\": 12.66,\r\n        \"deg\": 140\r\n    },\r\n    \"clouds\": {\r\n        \"all\": 75\r\n    },\r\n    \"dt\": 1647152557,\r\n    \"sys\": {\r\n        \"type\": 2,\r\n        \"id\": 2019646,\r\n        \"country\": \"GB\",\r\n        \"sunrise\": 1647152376,\r\n        \"sunset\": 1647194437\r\n    },\r\n    \"timezone\": 0,\r\n    \"id\": 2643743,\r\n    \"name\": \"London\",\r\n    \"cod\": 200\r\n}";

        [Fact]
        public async Task GetCurrentWeather_ReturnsAWeatherObj()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockApiResponse),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockUtilityService = new Mock<IUtilityService>();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient).Verifiable();
            var service = new WeatherService(mockHttpClientFactory.Object, mockUtilityService.Object);

            var result = await service.GetCurrentWeather("London");

            Assert.NotNull(result);
            Assert.Equal("London, GB", result.Location);
        }

        [Fact]
        public async Task GetWeather_ReturnsAWeatherResponseObj()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(MockApiResponse),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockUtilityService = new Mock<IUtilityService>();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient).Verifiable();
            var service = new WeatherService(mockHttpClientFactory.Object, mockUtilityService.Object);

            var result = await service.GetWeather("London");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            Assert.NotNull(result);
            Assert.Equal("London", result.Name);
        }

        [Fact]
        public async Task GetWeather_ReturnsBadRequest()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent(MockApiResponse),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockUtilityService = new Mock<IUtilityService>();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient).Verifiable();
            var service = new WeatherService(mockHttpClientFactory.Object, mockUtilityService.Object);

            var result = await service.GetWeather("London");

            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            Assert.Null(result);
        }

        [Fact]
        public async Task GetWeather_ReturnsNotFound()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();

            var response = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(MockApiResponse),
            };

            handlerMock
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>())
               .ReturnsAsync(response);

            var mockHttpClientFactory = new Mock<IHttpClientFactory>();
            var mockUtilityService = new Mock<IUtilityService>();

            var httpClient = new HttpClient(handlerMock.Object)
            {
                BaseAddress = new Uri("http://test.com/"),
            };

            mockHttpClientFactory.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClient).Verifiable();
            var service = new WeatherService(mockHttpClientFactory.Object, mockUtilityService.Object);

            var result = await service.GetWeather("asdfgh");

            Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
            Assert.Null(result);
        }
    }
}