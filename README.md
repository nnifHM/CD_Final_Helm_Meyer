# Sokoban Game
made by Arjun Bhogal, Klemens Meyer, Finn Helm, Jonas Beer

# Installation
## Prerequisites
This Project is created using dotnet 8.0.204, so if you dont have it yet, go and install it here: https://dotnet.microsoft.com/en-us/

After installation, clone this repo, and open it in a C# IDE of your choice.

Create a new System Environment variable called `GAME_SETUP_PATH`. The value of this variable should be the absolute path of the Setup.json file. Then, from the value, delete the ```Setup.json``` part. 

It should now look like this: `..\SokobanGame\ `

## Starting the game

When the project is opened in your IDE, go to the `game` folder with ```cd game```.

Now, run `dotnet build`

After that, run `dotnet run`

If everything worked, tha game should have started.

# How to play
The goal of the game is to cover all the goals (represented like this:````#````) with the boxes (represented like this:```○```)

### Navigation
Arrow keys for navigation, `ctrl + z` to undo your step.
`ctrl + s` to save your current state, which will then be loaded if you restart the game.


# Contact

If there is anything unclear or should not work, contact either of us here:

Jonas Beer:
`cc221012@fhstp.ac.at`

Arjun Bhogal:
`cc221016@fhstp.ac.at`

Finn Helm:
`cc221039@fhstp.ac.at`

Klemens Meyer:
`cc221011@fhstp.ac.at`
