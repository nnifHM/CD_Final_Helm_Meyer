namespace libs;

public sealed class Player : GameObject {

    private static Player? _instance;
    public static Player Instance {
        get{
            if(_instance == null)
            {
                _instance = new Player();
            }
            return _instance;
        }
    }
    private Player () : base(){
        Type = GameObjectType.Player;
        CharRepresentation = '☻';
        Color = ConsoleColor.DarkYellow;
    }
}