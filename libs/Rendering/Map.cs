namespace libs;
using Newtonsoft.Json;

public class Map {
    private char[,] RepresentationalLayer;
    private GameObject?[,] GameObjectLayer;

    private int _mapWidth;
    private int _mapHeight;


    public Map () {
        _mapWidth = 30;
        _mapHeight = 8;
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];
    }

    public Map (int width, int height) {
        _mapWidth = width;
        _mapHeight = height;
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];
    }

    

    public void Initialize()
    {
        RepresentationalLayer = new char[_mapHeight, _mapWidth];
        GameObjectLayer = new GameObject[_mapHeight, _mapWidth];

        // Initialize the map with some default values
        for (int i = 0; i < GameObjectLayer.GetLength(0); i++)
        {
            for (int j = 0; j < GameObjectLayer.GetLength(1); j++)
            {
                GameObjectLayer[i, j] = new Floor();
            }
        }
    }

    public int MapWidth
    {
        get { return _mapWidth; } // Getter
        set { _mapWidth = value; Initialize();} // Setter
    }

    public int MapHeight
    {
        get { return _mapHeight; } // Getter
        set { _mapHeight = value; Initialize();} // Setter
    }

    public GameObject Get(int x, int y){
        return GameObjectLayer[x, y];
    }
    
    public char GetChar(int x, int y){
        return RepresentationalLayer[x, y];
    }

    public void Set(GameObject gameObject){
        int posY = gameObject.PosY;
        int posX = gameObject.PosX;
        int prevPosY = gameObject.GetPrevPosY();
        int prevPosX = gameObject.GetPrevPosX();

        // Ensure the previous position is cleared if it's still holding a reference to the moving object
        if (GameObjectLayer[prevPosY, prevPosX] == gameObject) {
            GameObjectLayer[prevPosY, prevPosX] = null;  // Set to null or a new Floor as needed
        }

        // Place the object in the new position
        GameObjectLayer[posY, posX] = gameObject;
        RepresentationalLayer[gameObject.PosY, gameObject.PosX] = gameObject.CharRepresentation;
    }

}