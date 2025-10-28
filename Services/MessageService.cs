namespace Learn.Identity.Services;

// Esta classe retorna uma mensagem aleatória para a página de login
public class MessageService : IMessageService
{
    private readonly string[] messages = ["Quem é você?", "Faça login", "Identifique-se", "Username e senha. Fácil, certo?"];
    private readonly Random random;
    private readonly int words_count;

    public MessageService()
    {
        words_count = messages.Length;
        random = new Random();
    }

    public string GetMessage()
    {
        return messages[random.Next(0, words_count)];
    }
}