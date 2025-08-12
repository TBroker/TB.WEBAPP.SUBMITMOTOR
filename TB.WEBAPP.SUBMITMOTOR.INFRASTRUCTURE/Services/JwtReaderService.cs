using System.IdentityModel.Tokens.Jwt;
using TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces;

namespace TB.WEBAPP.SUBMITMOTOR.INFRASTRUCTURE.Services
{
    public class JwtReaderService : IJwtReaderService
    {
        public (string AgentCode, string AgentToken)? ReadAgentInfo(string request)
        {
            var token = request;
            if (string.IsNullOrEmpty(token)) return null;

            try
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(token);

                var agentCode = jwtToken.Claims.FirstOrDefault(c => c.Type == "AgentCode")?.Value;
                var agentToken = jwtToken.Claims.FirstOrDefault(c => c.Type == "AgentToken")?.Value;

                return string.IsNullOrEmpty(agentCode) || string.IsNullOrEmpty(agentToken)
                    ? null
                    : (agentCode, agentToken);
            }
            catch
            {
                return null;
            }
        }
    }
}