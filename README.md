# CCL4 - Game "Don't Die High"
 
# 1. Feature Description (What the Game Does)

## Core Gameplay:
- Auto-Runner Platformer: Player runs forward at a constant speed.
- Controls: Left, Right, Jump.
 
Difficulty Progression:
- Player speed increases over time or distance (method TBD).
 
Obstacles: 
- Mushroom 1 → -10 lifepoints
- Mushroom 2 → -30 lifepoints
- Mushroom 3 → -random(0–80) lifepoints
- Wall → -5 lifepoints and triggers stumble animation
 
Collectibles:
- Heart → restores +20 lifepoints (up to 100 max)

Scene Flow:
- Startscreen → press Start → loads Playscreen
- Playscreen: player auto-runs, interacts with obstacles/collectibles
- Gameoverscreen: triggered when lifepoints = 0
 
 
# 2. System Design
 
Player System:
- Movement
- Health System
- Speed Increase

 
Collision Logic:
- All obstacle/collectible collisions use triggers or colliders.
- Mushroom 3 triggers both:
- Wall also triggers a stumble animation.

 
Visual Effects:
- Death Character appears only after hitting Mushroom 3, not based on HP threshold.
- No direct gameplay effect — only visual.
 
 
# 3. System Infrastructure 
 
Scenes:
Startscreen, Playscreen, Gameoverscreen
Managed via SceneManager.LoadScene("SceneName")

Scripts: 
- GameManager (singleton) → tracks lifepoints, game state transitions
- PlayerMovement → handles input and physics
- ObstacleHandler or per-obstacle logic → processes collisions and HP deduction
- HeartPickup → restores HP on collection
- VisualEffectsController → triggers death character animation

UI:
- HP bar: probably Slider or Image.fillAmount
- Heart/lifepoint visuals in Playscreen
- Possibly uses Unity’s new Input System (TBD)
 
Audio & Animations:
- Animation: running, jump, stumble, death character appear
- Audio: pickup, hit, ambient sounds
 
HP loss
- Visual “Death Character” animation (regardless of final HP)
- Implemented (method not yet confirmed).
- Starts at 100 HP.
- Decreases with obstacle hits.
- Increases with heart pickups (+20 HP, max 100).
- Game over when HP reaches 0.
- Forward movement is automatic.
- Left/Right and Jump are player-controlled.
- Also triggers Death Character appearance (visual only; no gameplay effect)
 
