using Microsoft.EntityFrameworkCore;

namespace UrlShortenerApi.Services
{
    public class UrlShorteningService
    {
        public const int CharsInShortLink = 7;
        private const string Alphabet = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

        private readonly Random _random = new Random();
        private readonly AppDbContext _appDbContext;

        public UrlShorteningService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<string> GenerateUniqueCode()
        {
            var codeChars = new char[CharsInShortLink];
            while (true)
            {
                for (int i = 0; i < CharsInShortLink; i++)
                {
                    var index = _random.Next(Alphabet.Length - 1);
                    codeChars[i] = Alphabet[index];
                }

                var code = new string(codeChars);

                var isThereAnyCodeEqual = await _appDbContext.ShortenedUrls.AnyAsync(s => s.Code == code);

                if (!isThereAnyCodeEqual)
                {
                    return code;
                }
            }
        }
    }
}
