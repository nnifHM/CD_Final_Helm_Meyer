namespace libs;

public class Collision
{
    private static Collision? _instance;
    private GameEngine engine;
    public static Collision Instance {
        get{
            if(_instance == null)
            {
                _instance = new Collision();
            }
            return _instance;
        }
    }

    private Collision() {
        //INIT PROPS HERE IF NEEDED
        engine = GameEngine.Instance;
    }

    public bool canMove(int x, int y)
    {
        Map map = engine.GetMap();
        char lolchar = map.GetChar(y, x);
        GameObject obj = map.Get(y, x);
        Console.WriteLine("char: ");
        Console.WriteLine(lolchar);
        
        Console.WriteLine(obj.ToString());
        Console.WriteLine("X: ");
        Console.WriteLine(x.ToString());
        Console.WriteLine("Y: ");
        Console.WriteLine(y.ToString());
        if ((int)obj.Type == 1)
        {
            return false;
        }
        else
        {
            return true;
        }
        
    }
}