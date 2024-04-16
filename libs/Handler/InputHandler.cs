namespace libs;

public sealed class InputHandler{

    private static InputHandler? _instance;
    private GameEngine engine;

    public static InputHandler Instance {
        get{
            if(_instance == null)
            {
                _instance = new InputHandler();
            }
            return _instance;
        }
    }

    private InputHandler() {
        // Initialize properties if needed
        engine = GameEngine.Instance;
    }

    public void Handle(ConsoleKeyInfo keyInfo)
    {
    Console.WriteLine($"Key pressed: {keyInfo.Key} with modifiers {keyInfo.Modifiers}");  // This will show what key is pressed

    GameObject focusedObject = engine.GetFocusedObject();

    if (focusedObject != null) {
        // Handle keyboard input to move the player and save the state before the move
        switch (keyInfo.Key)
        {
            case ConsoleKey.UpArrow:
                engine.SaveCurrentState();
                focusedObject.Move(0, -1);
                break;
            case ConsoleKey.DownArrow:
                engine.SaveCurrentState();
                focusedObject.Move(0, 1);
                break;
            case ConsoleKey.LeftArrow:
                engine.SaveCurrentState();
                focusedObject.Move(-1, 0);
                break;
            case ConsoleKey.RightArrow:
                engine.SaveCurrentState();
                focusedObject.Move(1, 0);
                break;
            case ConsoleKey.Z:
                if (keyInfo.Modifiers == ConsoleModifiers.Control) {
                    Console.WriteLine("Undoing last move...");
                    engine.UndoMove();
                }
                break;
            default:
                Console.WriteLine("No action assigned for this key.");
                break;
        }
        }
        else {
            Console.WriteLine("No focused object available.");
        }
    }

}