namespace TB.WEBAPP.SUBMITMOTOR.APPLICATION.Interfaces
{
    public interface IJwtReaderService
    {
        (string AgentCode, string AgentToken)? ReadAgentInfo(string request);
    }
}
