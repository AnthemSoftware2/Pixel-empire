# Pixel-empire
if your going to go through the code. here's where you should go first

first is a script called Tile which contains the class for tiles
LandManager which creates the settings to create the map
then Land which is a class that creates and contains data for the map(ignore the void SpawnStructure and StructureIsDone. you'll get to them later)

next is StructureScriptableObjects which contains the info that creates the structure class

structures which contain data for what goes on top of tiles

now it's time to the actual building of structures

first is the script BuildingStragedy which contains the interface and classes to how items of a type will spawn
then the script BuildManager which as voids to change the stragedy and StructureScriptableObject(which is changed on runtime by UI buttons)
finally once a button is pressed it will create a selectedStructure
to learn more about that go To JobInterface then the scripts SideJob and Job to learn more
finally go to JobManager and the void BuildingJob to learn more how a job is created

finally then agents to learn how agents handle jobs

sorry if everything is badly explain
