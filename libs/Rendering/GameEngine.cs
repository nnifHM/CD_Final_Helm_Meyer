using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{
    private static GameEngine? _instance;
    private IGameObjectFactory gameObjectFactory;

    public static GameEngine Instance {
        get{
            if(_instance == null)
            {
                _instance = new GameEngine();
            }
            return _instance;
        }
    }

    private GameEngine() {
        //INIT PROPS HERE IF NEEDED
        gameObjectFactory = new GameObjectFactory();
    }

    private GameObject? _focusedObject;

    private Map map = new Map();

    private List<GameObject> gameObjects = new List<GameObject>();


    public Map GetMap() {
        return map;
    }

    public GameObject GetFocusedObject(){
        return _focusedObject;
    }


    public void Setup(){

        //Added for proper display of game characters
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        dynamic gameData = FileHandler.ReadJson();

        map.MapWidth = gameData.map.width;
        map.MapHeight = gameData.map.height;

        foreach (var gameObject in gameData.gameObjects)
        {
            AddGameObject(CreateGameObject(gameObject));
        }

        _focusedObject = gameObjects.OfType<Player>().First();

    }
    public void SetFocused(GameObject gameObject){
        _focusedObject = gameObject;
    }

    public void Render() {

        //Clean the map
        Console.Clear();

        map.Initialize();

        PlaceGameObjects();

        //Render the map
        for (int i = 0; i < map.MapHeight; i++)
        {
            for (int j = 0; j < map.MapWidth; j++)
            {
                DrawObject(map.Get(i, j));
            }
            Console.WriteLine();
        }
    }

    // Method to create GameObject using the factory from clients
    public GameObject CreateGameObject(dynamic obj)
    {
        return gameObjectFactory.CreateGameObject(obj);
    }

    public void AddGameObject(GameObject gameObject){
        foreach(GameObject player in gameObjects){
            if(player.Type == 0 && gameObject.Type == 0){
                return;
            }
        }
        gameObjects.Add(gameObject);
    }

    private void PlaceGameObjects(){

        gameObjects.ForEach(delegate(GameObject obj)
        {
            map.Set(obj);
        });
    }

    private void DrawObject(GameObject gameObject){

        Console.ResetColor();

        if(gameObject != null)
        {
            Console.ForegroundColor = gameObject.Color;
            Console.Write(gameObject.CharRepresentation);
        }
        else{
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(' ');
        }
    }

    public bool CheckWinCondition() {
        List<GameObject> goals = new List<GameObject>();
        List<GameObject> boxes = new List<GameObject>();


        foreach (GameObject gameObject in gameObjects) {
            if (gameObject.Type == GameObjectType.Goal) {
                goals.Add(gameObject);
            }
            else if (gameObject.Type == GameObjectType.Box) {
                boxes.Add(gameObject);
            }
        }

        // Check if each goal is covered by at least one box
        foreach (GameObject goal in goals) {
            bool goalIsCovered = false;
            foreach (GameObject box in boxes) {
                if (goal.PosX == box.PosX && goal.PosY == box.PosY) {
                    goalIsCovered = true;
                    break; // Stop checking other boxes once a covering box is found
                }
            }
            if (!goalIsCovered) {
                return false; // Return false as soon as an uncovered goal is found
            }
        }

        return true; // Return true if all goals are covered
    }
}