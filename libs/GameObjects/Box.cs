namespace libs;

public class Box : GameObject {

    public Box () : base(){
        Type = GameObjectType.Box;
        CharRepresentation = '○';
        Color = ConsoleColor.DarkGreen;
    }
    
    public override void Move(int dx, int dy)
    {
        SetPrevPosX(this.PosX);
        SetPrevPosY(this.PosY);
        this.PosX += dx;
        this.PosY += dy;
    }
}