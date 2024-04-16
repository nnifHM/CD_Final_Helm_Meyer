using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Nodes;

namespace libs;

using System.Security.Cryptography;
using Newtonsoft.Json;

public sealed class GameEngine
{
    private static GameEngine? _instance;
        private IGameObjectFactory gameObjectFactory;

        public static GameEngine Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameEngine();
                }
                return _instance;
            }
        }

        private GameEngine()
        {
            gameObjectFactory = new GameObjectFactory();
            gameObjects = new List<GameObject>();
            stateHistory = new Stack<List<GameObject>>();
            map = new Map();
            playerHistory = new Stack<(int, int)>();  // Stack to store player positions for undo functionality
        }

        private GameObject? _focusedObject;
        private List<GameObject> gameObjects;
        private Map map;
        private Stack<List<GameObject>> stateHistory;  // Stack to store map states for undo functionality

        private Stack<(int, int)> playerHistory;

        public void SaveCurrentState()
        {
            List<GameObject> gameObjectsCopy = new List<GameObject>();
            foreach (GameObject gameObject in gameObjects)
            {
                if (gameObject.Type != GameObjectType.Player)
                {
                    gameObjectsCopy.Add((GameObject)gameObject.Clone());
                }
            }
            foreach (GameObject gameObject in gameObjects)
            {
                Console.WriteLine(gameObject.Type + " " + gameObject.PosX + " " + gameObject.PosY + " " + gameObject.CharRepresentation + " " + gameObject.Color);
            }
            stateHistory.Push(gameObjectsCopy);
            playerHistory.Push((_focusedObject.PosX, _focusedObject.PosY));
            
        }

        
        public void UndoMove()
        {
            Console.WriteLine("UndoMove" + stateHistory.Count);
            if (stateHistory.Count > 0)
            {
                var gameObjectsCopy = new List<GameObject>(gameObjects); // Create a copy of gameObjects

                foreach (GameObject gameObject in gameObjectsCopy)
                {
                    if (gameObject.Type != GameObjectType.Player)
                    {
                        gameObjects.Remove(gameObject); // Remove elements from the original list
                    }
                }
                
                // Add the gameObjects from the previous state to the gameObjects list
                foreach (GameObject gameObject in stateHistory.Pop())
                {
                    gameObjects.Add(gameObject);
                }

                // Restore the player position from playerHistory
                (int x, int y) = playerHistory.Pop();
                _focusedObject.PosX = x;
                _focusedObject.PosY = y;
                Render(); 
            }
            else
            {
                Console.WriteLine("No more moves to undo.");
            }
        }


        public bool CanUndo()
        {
            return stateHistory.Count > 0;
        }

        public Map GetMap()
        {
            return map;
        }

        public GameObject GetFocusedObject()
        {
            return _focusedObject;
        }

        public void Setup()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            dynamic gameData = FileHandler.ReadJson();  // Load game data
            map.MapWidth = gameData.map.width;
            map.MapHeight = gameData.map.height;

            foreach (var gameObject in gameData.gameObjects)
            {
                AddGameObject(CreateGameObject(gameObject));
            }

            _focusedObject = gameObjects.OfType<Player>().FirstOrDefault();  // Ensure there is a player
        }

        public void SetFocused(GameObject gameObject)
        {
            _focusedObject = gameObject;
        }

        public void Render()
        {
            Console.Clear();
            

            
                map.Initialize();
                PlaceGameObjects();
            
            
            for (int i = 0; i < map.MapHeight; i++)
            {
                for (int j = 0; j < map.MapWidth; j++)
                {
                    DrawObject(map.Get(i, j));
                }
                Console.WriteLine();
            }
        }

        


        public GameObject CreateGameObject(dynamic obj)
        {
            return gameObjectFactory.CreateGameObject(obj);
        }

        public void AddGameObject(GameObject gameObject)
        {
            if (!gameObjects.Any(p => p.Type == GameObjectType.Player && gameObject.Type == GameObjectType.Player))
            {
                gameObjects.Add(gameObject);
            }
        }

        private void PlaceGameObjects()
        {
            foreach (GameObject obj in gameObjects)
            {
                map.Set(obj);
            }
        }

        private void DrawObject(GameObject gameObject)
        {
            Console.ResetColor();
            if (gameObject != null)
            {
                Console.ForegroundColor = gameObject.Color;
                Console.Write(gameObject.CharRepresentation);
            }
            else
            {
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