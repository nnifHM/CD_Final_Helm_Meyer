﻿using libs;

class Program
{
    static void Main(string[] args)
    {
        //Setup
        Console.CursorVisible = false;
        var engine = GameEngine.Instance;
        var inputHandler = InputHandler.Instance;
        var collision = Collision.Instance;
        //var player = Player.Instance;

        engine.Setup();
        //engine.SetFocused(player);

        // Main game loop
        while (true)
        {
            engine.Render();

            // Handle keyboard input
            ConsoleKeyInfo keyInfo = Console.ReadKey(true);
            inputHandler.Handle(keyInfo);

            // After a move is made
            if(!engine.isInDialog){

            
                if (engine.CheckWinCondition()) {
                
               
                Console.Clear();
                //Console.WriteLine("Congratulations! You Win!");
                FileHandler.nextLevel();
                
                engine.Setup();
                //Set Patth paapjwadajdw
                //Console.ReadKey(true);
                //Environment.Exit(0); // Exit the game after displaying the win message.
                
                }
            
            }
        }
    }
}