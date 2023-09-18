# The Garden of Earthly Delights
This project is a personal project I've been working on throughout the summer of 2023.

I chose to make a roguelite game because this year, this genre has been a discovery for me and I've enjoyed it a lot, playing games like Hades, Cult of the Lumb, Bio-Prototype, Loop Hero, Enter the Gungeon, Dead Cells, Neon Abyss, and a few more!

During the development of this project, I've learned a lot. Specifically, gameplay programming, Scriptable Objects, AI programming (movement and decision-making), UI programming (using tweening libraries), design patterns, and UML.

**Be aware:** There are some bugs in the videos and gifs that have already been fixed, but I haven't been able to update the videos yet due to time constraints. Don't worry!

# Features
## Code
 - **Player:** Movement, Animation, Character(stats and Scriptable Object for persistance data container), Management of items, coins, weapons and experience
 - **Debugging** Mostly OnDrawGizmos
 - **Enemy:** Different types of enemies: Movement (Kinematic vs Dynamic), experience reward, damage and speed/acceleration.
 - **Enemy AI:** Blending Steering Behavior based on the book Artificial Intelligence - Ian Millington. 
 - **Tweening UI:** DOTween library
 - **Passive Items and different Types of Upgrades to Unlock**: Armor, Boots, Crit items, HP Regeneration, Max HP
 - **Different Weapons and Upgrades to Unlock**: Poison Rainbow (Area Damage), Brush (Melee Damage), Water Sling (Distance Weapon)
 - **Stats System for player:** Armor, Max HP, Damage, HP Regeneration, Speed, Crit Chance
 - **Upgrades System**
 - **Message System:** Popups (enemy damage, enemy crit damage, player damage, player healing)
 - **My own Object Pool System**
 - **Spawn Objectes System**
 - **Condition Flag System** to unlock new levels
 - **Managers:** Event Spawning Enemies Manager, Music Manager, Weapon Manager, Enemies Manager, Object Spawning Manager.
 - **Interfaces:** Interfaces to promote Abstraction, Polymorphism and Decoupling in the code. E.g: IDamageable, IPickUpObject, IPoolMember.
 - **Scriptable Objects** to save data and save different kind of objects from weapons, enemies, levels, condition flags,
   pools, popups...

   
## Levels
- 4 Different levels.
- When the game begins, only the first level is unlocked. To unlock the next level, the player must have successfully completed the previous level, which is achieved by surviving the time marked at the beginning of each match. As you progress through the levels, the 
  time required to complete each level increases, and more monsters, including stronger ones, will spawn. 
- Even though monsters and the player are paused when you level up and the upgrade panel is opened, monster spawning is not, so the player must make quick decisions about which upgrades to choose to avoid being overwhelmed by a horde of formidable monsters


### Art
  - Half of the art is made by me. The other half is taken from internet and edited by me to get the watercolor style of       the game


## UML
  - [UML Files](https://drive.google.com/drive/folders/16DbW0m3QNyMtEOaptFkmP0oJ878A7_4L?usp=sharing)

## To do's
    
### Code:
  - Gameplay SFX
  - Graphics and Audio Settings Panel (Menu)
  - Upload to itchio WebGL / Google Store
  - INPUT FOR MOBILE

# Videos - Gifs
## Weapons + Damage Popups - Gameplay
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Weapons.gif)

## Upgrades - Gameplay
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Upgrades.gif)

## Collectibles + Headling Popups - Gameplay
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Collectibles.gif)

## Win
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Win.gif)

## Game Over
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Game%20Over.gif)

## Loading Screen
![](https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/blob/main/Loading%20Screen.gif)

## Main Menu Flow
https://github.com/dendritaDev/The-Garden-of-Earthly-Delights/assets/107819892/149fa6ee-a710-440b-a0e5-2f09edcd2039



