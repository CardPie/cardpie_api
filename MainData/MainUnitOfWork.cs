using AppCore.Data;
using MainData.Entities;

namespace MainData;

public class MainUnitOfWork : IDisposable
{
    private readonly DatabaseContext _context;

    public MainUnitOfWork(DatabaseContext context)
    {
        _context = context;
    }
    
    public BaseRepository<User> UserRepository => new(_context);
    public BaseRepository<Token> TokenRepository => new(_context);
    public BaseRepository<Deck> DeckRepository => new(_context);
    public BaseRepository<FlashCard> FlashCardRepository => new(_context);
    public BaseRepository<StudySession> StudySessionRepository => new(_context);
    public BaseRepository<Folder> FolderRepository => new(_context);
    public BaseRepository<Post> PostRepository => new(_context);
    public BaseRepository<Interaction> InteractionRepository => new(_context);

    public void Dispose()
    {
    }
}