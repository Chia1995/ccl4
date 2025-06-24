# Code Review Report

**Course:** CCL4 SS 2025 (5 ECTS, 3 SWS)  
**CCL4 Group:**  11  
**Names:**  
- Milena Biasova - CC231001  
- Christina Gamperl - CC231023  
- Augustine Onyirioha - CC231027  
- Ecem Tasali - CC221049

**Your Project Name:** **Don't Die High**   

---

## A Short Summary to Promote the Project  
`Don’t Die High` is a 3D endless runner where the player races along a colorful rainbow road through a psychedelic world. The aim is to survive and achieve a high score by collecting coins and hearts to restore lives. The player must avoid poisonous mushrooms and jump over walls — because each mistake costs a life. When a life is lost, Death begins to chase the player, adding tension. As the player runs, Death gradually falls behind and disappears — until the next mistake. Our motivation was to blend vibrant visuals, fast-paced action, and a subtle message: don’t overdo it.  

---

## Key Features and Implementation Details

### 3D Modeling

#### Made by Blender
- Player  
    - Animation Idle, Run, Jump  
- Villain  
    - Animation Run  
- Environment  
    - Road  
    - Flower  
    - Tree  
    - RedMushroom  
- Obstacles  
    - Mushroom  
        - Animation Idle  

#### Unity Asset Store
- 3D Gamekit - Environment Pack  
    - Ground, mushroom, trees, rocks, and flowers used in `StartGame` scene and `GameOver` scene
- FantasyEnvironments  
    - Ground, mushroom, trees, rocks, and flowers used in `StartGame` scene and `GameOver` scene

#### free3d.com
- Magnet  
- Heart  

### Game Audio
- Item  
- Item, and so forth  


[Music]([url](https://github.com/user-attachments/assets/3c46b328-c9fd-4253-8994-27fa22248451))

[Mushroom Sound]([url](https://github.com/user-attachments/assets/0261022b-3573-4a13-8c6a-425fa74f615f))

[Coin Sound]([url](https://github.com/user-attachments/assets/e080bf33-8d7e-4f2d-a08e-2117af5d9a9a))




### Unity Coding
- Endless tile generation system  
- Power-up mechanics (Magnet attraction)  
- Villain follower AI  
- Collision detection  
- Score/lives management via singleton `GameManager`  
- UI transitions and Game Over logic  

### C# & Theory of CG&A
- Custom shaders via ShaderToy  
- Use of Unity’s physics system and animation blending  
- Collision detection with layers and tags  
- Input system integration for jump/move  

---

## Implementation Logic Explanation  
*(Explain how you implement the idea step by step compactly and clearly.)*  
The game begins with the player auto-running on a procedurally generated track. `GroundSpawner` instantiates new tiles using `GroundTile`, which randomly spawns coins and obstacles. The player can jump and move horizontally using Unity's Input System. Collisions with coins increase score, obstacles reduce lives. If the player collects a magnet, nearby collectibles are drawn toward them for 5 seconds. A villain chases the player and catches them if they've been hit 3 times. All state is tracked via a singleton `GameManager`, which also handles scene transitions and UI updates.  

---

## Three Important Achievements  
*(List and explain 3 important achievements you are proud of (e.g., features, techniques, etc.) in the project. Please explain in detail.)*  

1. **Creating trippy Shader with Shadertoy**  
Shader is used on the Sphere, which follows the position of the player  

2. **Creation and Animation of 3D Models and Scene Elements**  
The creation of the mushroom, death character, and player models, along with their animations, was a major achievement. Each model was carefully designed, rigged, and animated to fit the game’s surreal theme. The death character's entrance when the player loses adds strong visual feedback. Additionally, several scene elements like mushrooms, hearts, and background objects were custom-made to create a consistent and immersive environment.  

3. **Villain AI and Dynamic Ground Tile Spawning**  
One of the most technically interesting features was our Villain AI system. The villain begins following the player, but only catches them after three hits. It dynamically rotates toward the player and adjusts its movement based on proximity and the game state. Here’s a snippet from `VillainFollower.cs`:
---
```csharp
if (GameManager.Instance != null && GameManager.Instance.HitCount < 3)
{
    float distance = Vector3.Distance(transform.position, player.position);
    if (distance > followDistance)
    {
        Vector3 direction = (player.position - transform.position).normalized;
        transform.position += direction * moveSpeed * Time.deltaTime;

        Vector3 lookDirection = player.position - transform.position;
        lookDirection.y = 0;
        transform.rotation = Quaternion.LookRotation(lookDirection) * Quaternion.Euler(0, 180, 0);
    }
}
else
{
    // Player hit 3 times — villain catches them
    transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);
}
````

In addition, we implemented a continuous ground spawning system using `GroundSpawner.cs` and `GroundTile.cs`, where new tiles instantiate ahead of the player and self-destruct behind them. This made the game infinite while managing performance by limiting object count.

---

## Learned Knowledge from the Project

* Gained hands-on experience with Blender modeling and UV unwrapping
* Learned how to work with Unity’s Input System and Rigidbody-based movement
* Implemented collision-based interactions, tag-based logic, and UI systems
* Practiced scene management, singleton patterns, and coroutine-based timing
* Enhanced understanding of shader integration using ShaderToy in Unity

---

## Major Challenges and Solutions

*(List and explain the major challenges. Did you solve it? How? Please explain in detail.)*

1. **Baking Textures in Blender**  
   One of the biggest challenges we faced was baking textures in Blender. We weren’t able to get the desired results on our own, which affected the visual quality of our models. With the help of Hannes, we were able to understand the process and fix the issue. This allowed us to correctly apply textures and improve the overall appearance of our assets in Unity.

2. **Making the Villain Follow the Player**  
   Getting the villain to follow the player smoothly, without looking jittery or unnatural, was a major challenge. Initially, the movement lagged behind or turned awkwardly. We solved this by using vector math to calculate direction, applying smooth movement using `Vector3.Lerp`, and adding rotation logic to ensure the villain always faces the player correctly. We also made sure the villain only initiates the "catch" phase after `GameManager.HitCount` reaches 3. This created suspense and made the gameplay more reactive.

---

## Minor Challenges and Solutions

*(List and explain the minor challenges. Did you solve it? How? Please explain in detail.)*

1. **UI Button Linking and Scene Management**  
   Buttons on the Game Over and Main Menu screens occasionally broke after reloading. We resolved this by using the `SceneLoader` script and ensuring all buttons were properly linked in the inspector.

2. Item

---

## Reflections on the Own Project  

*(List and explain what you could improve and add if you have more time.)*

1. **Villain Animation and Behavior**  
   If I had more time, I would definitely improve the animation of the death character. I’m not entirely satisfied with how it turned out, but due to time constraints, I couldn’t refine it as much as I wanted. With more time, I would focus on making the movement smoother and more expressive to better match the dramatic moment when the character appears.

2. **Level Design Variety**  
   We would add visual and gameplay variation between tiles, perhaps rotating themes or dynamic lighting to make long play sessions more engaging and less repetitive.  
--- 
```
