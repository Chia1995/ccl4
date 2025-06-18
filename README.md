# CCL4 - Game "Don't Die High"
 
# 1. Logo

![Logo](Assets\Documents\logo.png)

# 2. The Game

# 3. Motivation

# 4. 3D Modeling and Animation

## 3D Models done by Blender
- Player  
    - Animation Idle, Run, Jump
- Villain
    - Animation Run
- Enviroment
    - Road
    - Flower
    - Tree
    - RedMushroom
- Obstacles
    - Mushroom
        - Animation Idle
## Unity Asset Store
- 3D Gamekit - Environment Pack
    - Ground, mushroom, trees, rocks and flowers used in `StartGame` scene and `GameOver`
- FantasyEnvironments
    - Ground, mushroom, trees, rocks and flowers used in `StartGame` scene and `GameOver`

## free3d.com
- Magnet
- Heart

## Scenes

# 5. Game Audio

# 6. System Design

# 7. System Infrastructure

# 8. How to run the program (User guideline)

## 1️⃣ Requirements
- Unity version: 2022.3.59fi
- Platform: Windows
- Input device: Keyboard

---

## 2️⃣ Setup Instructions
1. Download ZIP folder and extract it.
2. Open the project in **Unity Hub** and select the correct Unity version.
3. In Unity, open the scene:
   - `StartGame` (main menu scene)
4. Check that the build settings are correct:
   - Go to `File > Build Settings`
   - Ensure your all 3 scenes are in the build list.
      - `StartGame` (main menu scene)
      - `MainGameScene` (main menu scene)
      - `GameOverScene` (main menu scene)
---

## 3️⃣ How to Play
- **Movement:** Use `A` to move to the right and `D` to move to the left
- **Jump:** Press `Spacebar` to jump.
- **Collect items:** Collect coins to get a score, collect magnet to get all nearby coins automatically
- **Avoid obstacles:** Avoid walls and mushrooms. (loose hearts)

---

## 4️⃣ Run the Program
- Press the **Play** button in Unity editor to start.
- Or build the game:
  1. Go to `File > Build Settings`
  2. Select your platform (e.g., PC, Mac)
  3. Click **Build and Run**

---

## 5️⃣ Exiting
- Press the **Quit** button in the game over scene to return to the Main menu scene `StartGame`. In this scene you click "Exit Game" to close the application.

---

## 6️⃣ Notes
- The game is designed for fullscreen, but works in windowed mode.
- For best experience, play in 16:9 resolution.