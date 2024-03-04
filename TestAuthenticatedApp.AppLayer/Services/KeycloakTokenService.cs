using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TestAuthenticatedApp.AppLayer.Dtos;
using TestAuthenticatedApp.SharedLayer.Consts;

namespace TestAuthenticatedApp.AppLayer.Services
{
    public class KeycloakTokenService
    {
        public async Task<KeycloakTokenResponseDto?> GetTokenResponseAsync(
            KeycloakUserDto keycloakUserDto)
        {
            using (var httpClient = HttpClientFactory.CreateClient())
            {

                var keycloakTokenRequestDto = new KeycloakTokenRequestDto
                {
                    GrantType = KeycloakAccessTokenConsts.GrantTypePassword,
                    ClientId = keycloakSettings.ClientId ??
                        throw new KeycloakException(nameof(keycloakSettings.ClientId)),
                    ClientSecret = keycloakSettings.ClientSecret ??
                        throw new KeycloakException(nameof(keycloakSettings.ClientSecret)),
                    Username = keycloakUserDto.Username,
                    Password = keycloakUserDto.Password
                };


                var tokenRequestBody = KeycloakTokenUtils.GetTokenRequestBody(keycloakTokenRequestDto);

                var response = await httpClient
                    .PostAsync($"{keycloakSettings.BaseUrl}/token", tokenRequestBody)
                    .ConfigureAwait(false);


                var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

                var keycloakTokenResponseDto = JsonConvert.DeserializeObject<KeycloakTokenResponseDto>(
                                    responseJson);

                return keycloakTokenResponseDto;
            }
        }
    }

    //The $"{keycloakSettings.BaseUrl}/token" is equivalent to

    //http://{keycloakHost}:{keycloakPort}/realms/{yourRealm}/protocol/openid-connect/token 
}
