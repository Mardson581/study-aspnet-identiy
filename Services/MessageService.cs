namespace Learn.Identity.Services;

/* Esta classe retorna uma mensagem aleatória para a página de login */
public class MessageService : IMessageService
{
    private readonly string[] messages = ["Quem é você?", "Faça login", "Identifique-se"];
    private readonly Random random;

    public MessageService()
    {
        random = new Random();
    }

    public string GetMessage()
    {
        return messages[random.Next(0, 3)];
    }
}