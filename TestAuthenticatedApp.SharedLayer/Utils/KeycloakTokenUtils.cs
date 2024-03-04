using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestAuthenticatedApp.AppLayer.Dtos;
using TestAuthenticatedApp.SharedLayer.Consts;

namespace TestAuthenticatedApp.SharedLayer.Utils
{
    public static class KeycloakTokenUtils
    {
        public static FormUrlEncodedContent GetTokenRequestBody(
            KeycloakTokenRequestDto keycloakTokenDto)
        {
            var keyValuePairs = new List<KeyValuePair<string, string>>()
            {
                new(KeycloakAccessTokenConsts.GrantType, keycloakTokenDto.GrantType),
                new(KeycloakAccessTokenConsts.ClientId, keycloakTokenDto.ClientId),
                new(KeycloakAccessTokenConsts.ClientSecret, keycloakTokenDto.ClientSecret),
                new(KeycloakAccessTokenConsts.Username, keycloakTokenDto.Username),
                new(KeycloakAccessTokenConsts.Password, keycloakTokenDto.Password)
            };
            return new FormUrlEncodedContent(keyValuePairs);
        }
    }
}
