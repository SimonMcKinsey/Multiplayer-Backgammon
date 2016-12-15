using Backgammon.enumsFolder;

namespace Backgammon
{
    public interface IPlayer
    {
         string UserName { get; set; }
         PlayerState PlayerState { get; set; }
    }

    
}