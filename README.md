# Map File Generator
 A system that saves positions of gameobjects in scene, so that you can recreate that scene with pre-loaded gameobjects.
 In the target project this system is meant to make gameobjects present in the scene reusable, so we don't have to load them for every new level in the game.
 
 With this system, you can use just one scene, and change the level/ map by loading the appropriate objects with the saved positions, rotations etc. of that level.
 Which means that your levels would basically be prefabs, and your scene would change levels by loading objects and positions from those level prefabs.
 
 Setting up the system
 1. In Resources/Level Manager create these objects
 
![image](https://user-images.githubusercontent.com/91789757/211810715-f127c768-8e1d-4ec3-a13f-121d731640a9.png)

You can create the scriptable objects from the context menu --> Right click in project folder --> Scriptable Objects

![image](https://user-images.githubusercontent.com/91789757/211813403-502df3b6-4ada-479e-b665-7b42da9465cf.png)

2. In the folder "Resources/Level Manager/Scene Objects" you need to add the gameobjects (prefabs) you wish to be reused through loaded levels.
The gameobjects (prefabs) in the "Scene Objects" folder will be loaded into "LevelDataObjectsPool_SO", where you can set the amount of gameobjects to be instantiated when launching the game (field "Pool Size").

![image](https://user-images.githubusercontent.com/91789757/211814345-de4a3806-08f4-46af-9ec0-b2ba885f6670.png)

The LevelDataObjectsPool_SO will load the scene objects from "Scene Objects" folder on game start, but you can do it manually also --> Right click LevelDataObjectsPool_SO -->  Level Manager --> Level Data Objects Pool --> Load All Scene Objects

3. Now you need to create a level with the objects you put into the "Scene Objects" folder (IMPORTANT - ONLY OBJECTS FROM THE "Scene Objects" FOLDER WILL BE PROPERLY LOADED DURING RUNTIME!).

![image](https://user-images.githubusercontent.com/91789757/211815044-4599c84b-4cc0-4ce2-8df7-fc5d400c4b2c.png)

To save the level/ map --> Right click the level prefab/ gameobject in the scene --> Level Manager --> Generate Level Data

![image](https://user-images.githubusercontent.com/91789757/211816045-e702d500-edc0-4dc5-b948-2851e078405f.png)

You will get a popup editor window to choose a name for the level. After writing the name press "Generate Level Data", your generated level file (Scriptable Object) will be in "Resources/Level Manager/Levels Data".

After generating a LevelData_SO, LevelDataList_SO will automatically update it's list of levels (this also happens on game start!;

You can also load the levels manually by --> Right click LevelDataList_SO -->  Level Manager --> Level Data List --> Load All Levels Data

![image](https://user-images.githubusercontent.com/91789757/211820902-84c0b2f2-7286-48cf-89f1-4281ef5c337b.png)


4. Now you can load your level inside the game! 
After launching Play Mode you will notice a pool object in the scene hierarchy. Here you will have all the gameobjects instantiated from LevelDataObjectsPool_SO, in the amount you defined in that scriptable object.

![image](https://user-images.githubusercontent.com/91789757/211819135-26915c6f-c29c-445f-abb1-1c310c6ac7a7.png)

To load the level of course you need to connect the system to your own game, but you can do it in the editor too!
Make sure you have the LevelManager prefab inside the scene --> Click on LevelManager --> Right click Level Manager component --> Test Load Level (this will load the level from the LevelDataList_SO, with the index defined in "Level Index" under Debugging header in Level Manager component)

![image](https://user-images.githubusercontent.com/91789757/211820094-6aef3cd3-de32-4fb0-8e8c-00497ab27476.png)

Your objects should appear in the scene!
You can also unload the map by using the same index.

------------------------------
IMPORTANT NOTES
------------------------------

Every object inside the level/ map prefab needs to have it's prefab present in the "Scene Objects" folder, otherwise the system won't work.

------------------------------

If you need any help, contact me!

E-mail: davidkuc2@onet.pl
Discord: David Kuc#7866
Facebook: https://www.facebook.com/McMudzynek

