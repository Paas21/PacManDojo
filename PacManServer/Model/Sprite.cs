
namespace PacManServer.Model
{
    public record struct Sprite
    (

        CharacterType characterType,
        string color,
        double XPosition,
        double YPosition,
        double With,
        double Height
    );

    public enum CharacterType
    {
        Player,
        Gost
    }
}
