# CCL4 - Game "Don't Die High"

## System Design

The game consists of three main scenes: **Startscreen**, **Playscreen**, and **Gameoverscreen**. Each scene represents a distinct state of the game flow and is managed through a central game controller. The core mechanics revolve around real-time obstacle interaction, life point management, and gradual difficulty scaling.

### Startscreen
- Displays the player and a “Start” button.
- Upon clicking the Start button, the game transitions to the Playscreen.

### Playscreen
- The player character auto-runs forward.
- Speed increases over time or based on distance covered (to be defined).
- Obstacles (mushrooms, trees) are dynamically spawned.
- Collision with mushrooms reduces the player's lifepoints:
  - Mushroom 1: -10
  - Mushroom 2: -30
  - Mushroom 3: takes - random lifepoints between 0 to 80
- Collisions with walls (trees):
  - -5 lifepoints
  - Triggers a stumble animation
- Hearts can be collected to regain lifepoints.
- If lifepoints drop below 20:
  - A death character appears (animation only; no gameplay effect).
- **Game Over** is triggered when lifepoints reach 0.

### Gameoverscreen
- Displays a “Game Over” message.
- Shows the player’s score (e.g., meters run or total time).
- Offers a button to restart the game and return to the Startscreen.

## System Infrastructure
