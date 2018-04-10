Game Design I - CIS487 Fall 2016
======
>As out first foray into game design, we were tasked with learning the basics of the Unity game engine
>and creating a small playable prototype. We chose my design for a infinite scrolling side scrolling
>shooter *(See one-sheet design below)* and Jeff proceeded to create most of the artwork while I 
>implemented the desired features.

### Details

__Students:__ Marc King, Jeff Wright

__Professor:__ Dr. Bruce Maxim

__School:__ University of Michigan - Dearborn

__My Contributions:__ Design, implementation, logo, story, background

__Timeline:__ 1 week

### Technologies

* .NET Framework 3.5
* Unity 5.5.0

### One-Sheet Design

*Prior to picking the two-person teams for this project, each student was tasked with quickly creating
a one-sheet game design sketch. Teams were then formed and each team chose among their designs for 
which game to create. Below is my one-sheet design sketch that was used during the creation of this game.*

[![OneSheetDesign](Screenshots/OneSheetDesign_480.PNG?raw=true "OneSheetDesign")](Screenshots/OneSheetDesign.PNG?raw=true)

### Screenshots

*Players are initially presented with the main menu; which shows the current high scores, the random
seed used for procedural generation, and buttons to start a game or quit. Since each unique random
seed has its own high score list, that feature was disabled for the 2D Game Festival release to foster
competition among the players.*

[![MainMenu](Screenshots/MainMenu_480.png?raw=true "MainMenu")](Screenshots/MainMenu.png?raw=true)

*After starting a new game, players are show a brief backstory and given basic instructions for how
to control the ship.*

[![StoryHelp](Screenshots/StoryHelp_480.png?raw=true "StoryHelp")](Screenshots/StoryHelp.png?raw=true)

*Players are able to shoot in the direction of the cursor, but each shot cost some of their score.
This was done to allow players that are selective with their shots and those with better aim to achieve
higher scores.*

[![Shooting](Screenshots/Shooting.gif?raw=true "Shooting")](Screenshots/Shooting.gif?raw=true)

*Below is an example of the primary gameplay: flying through tight caves while destorying enemy
ships and avoid their fire. The cave system is procedurally generated using the random seed and
gets progressively faster as the game continues.*

[![Gameplay](Screenshots/Gameplay.gif?raw=true "Gameplay")](Screenshots/Gameplay.gif?raw=true)

*After being destroyed (which inevitably occurs due to the increase of cave speed) the player is
presented with a game over screen which displays the credits for the game and provides options for
restarting immediately or returning to the main menu. If the player surpassed an existing high score
they are able to enter their name for the high score list.*

[![GameOver](Screenshots/GameOver_480.png?raw=true "GameOver")](Screenshots/GameOver.png?raw=true)

[![NewHighScore](Screenshots/NewHighScore_480.png?raw=true "NewHighScore")](Screenshots/NewHighScore.png?raw=true)


