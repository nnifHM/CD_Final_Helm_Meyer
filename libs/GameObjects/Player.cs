namespace libs;

public sealed class Player : GameObject {

    private static Player? _instance;
    private Collision collision;
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

    public override void Move(int dx, int dy)
    {
        collision = Collision.Instance;
        if (collision.canMove(this.PosX + dx , this.PosY + dy)){
        SetPrevPosX(this.PosX);
        SetPrevPosY(this.PosY);
        this.PosX += dx;
        this.PosY += dy;
        }
    }
}