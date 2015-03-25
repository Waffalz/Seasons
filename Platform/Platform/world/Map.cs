using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using Platform.Mobs;
using Platform.Graphics;
using Platform.GameFlow;

namespace Platform.World
{
    public class Map
    {
        public const int MAP_BOUNDS = 256;//max number of tiles in each dimension
        public const float WALL_BUFFER = 1;

        public const float MAX_WUBS = MAP_BOUNDS * Tile.TILE_WIDTH;

        private Tile[,] tiles;
        private List<Entity> entList;
        private List<Entity> addEList;
        private List<Entity> removeEList;

        private List<Particle> partList;
        private List<Particle> addPList;
        private List<Particle> removePList;

        private Camera cam;
        private Player player;
        private float gAccel; //default -150

        private List<BackgroundObject> backList;

        public Tile[,] Tiles
        {
            get { return tiles; }
            set { tiles = value; }
        }
        public List<Entity> Entities
        {
            get { return entList; }
            set { entList = value; }
        }
        public List<Particle> Particles
        {
            get { return partList; }
            set { partList = value; }
        }
        public float Gravity
        {
            get { return gAccel; }
            set { gAccel = value; }
        }
        public Player Player
        {
            get { return player; }
            set { player = value; }
        }
        public Camera Camera
        {
            get { return cam; }
            set { cam = value; }
        }
        public List<BackgroundObject> BackList
        {
            get { return backList; }
            set { backList = value; }
        }

        public Map()
        {
            tiles = new Tile[MAP_BOUNDS, MAP_BOUNDS];
            entList = new List<Entity>();
            addEList = new List<Entity>();
            removeEList = new List<Entity>();
            partList = new List<Particle>();
            addPList = new List<Particle>();
            removePList = new List<Particle>();
            gAccel = -150;
            cam = new DefaultCamera(this);
            backList = new List<BackgroundObject>();

        }

        public void AddEntity(Entity toAdd)
        {
            addEList.Add(toAdd);
        }
        public void RemoveEntity(Entity toRemove)
        {
            removeEList.Add(toRemove);
        }

        public void AddParticle(Particle toAdd)
        {
            addPList.Add(toAdd);
        }
        public void RemoveParticle(Particle toRemove)
        {
            removePList.Add(toRemove);
        }

        public void Tick(GameTime gameTime)
        {
            //the method that updates all stuff going on in the map, calling to it will update the map by 1 tick (ideally 60 times per sec)

            float timeDifference = (float)gameTime.ElapsedGameTime.TotalSeconds;

            foreach(Entity ent in addEList){//add entities in queue
                entList.Add(ent);
                ent.parent = this;
            }
            addEList.Clear();
            foreach (Particle p in addPList){//add particles in queue
                partList.Add(p);
                p.parent = this;
            }
            addPList.Clear();

            List<Entity> tileEnts = new List<Entity>();//since I don't wanna make tile to entity collisions, create an entity based off of tiles to check for entity collision
            for (int y = 0; y<tiles.GetLength(0); y++){
                for (int x = 0; x<tiles.GetLength(1); x++){
                    if (tiles[y, x] != null){
                        Entity lent = new Entity();
                        lent.Size = new Vector2(Tile.TILE_WIDTH, Tile.TILE_WIDTH);
                        lent.Position = new Vector2(x * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, y * Tile.TILE_WIDTH + Tile.TILE_WIDTH/2);
                        lent.Anchored = true;
                        lent.Gravity = false;
                        lent.Solid = true;
                        tileEnts.Add(lent);
                    }
                }
            }

            foreach (Entity ent in entList){ //iterate over each entity in entlist to calculate all happenings
                Vector2 oldPos = ent.Position;//old position of ent in case we ever need to revert to it

                if (ent is Behavior) { //if ent implements the Behavior interface, do that stuff
                    ((Behavior)ent).Behave(timeDifference);
                }

                ent.Update(gameTime);//if ent has to do some specific things, do them
                if (ent.Solid) {
                ent.CorrectCollisionPosition(tileEnts);
                }

                //TODO: fix onground problems due to the fact that entities no longer collide after repositioning after collision

                foreach (Entity other in entList){ //check for interactions between entities in entlis
                    if (ent != other){
                        if (ent.Collides(other)){
                            ent.OnCollide(other);
                            other.OnCollide(ent);
                        }
                    }
                }

                if (ent.Position.Y < -50) {//remove ent if below the kill level
                    RemoveEntity(ent);
                }

                
            }
            foreach (Particle p in partList) {//call to each particle's specific behaviors
                p.Update(gameTime);
            }

            foreach(Entity ent in removeEList){//removes entities in queue
                if (entList.Contains(ent)){
                    entList.Remove(ent);
                    ent.parent = null;
                    if (ent is Player && player == ent){
                        player = null;
                    }
                }
            }
            removeEList.Clear();
            foreach (Particle p in removePList){//removes entities in queue
                if (partList.Contains(p)){
                    partList.Remove(p);
                    p.parent = null;
                }
            }
            removePList.Clear();

            cam.Update(gameTime);
        }

        /* Old Map loading stuff
        public static Map LoadMap(string pathname)
        {
            Map nMap = new Map();
            try
            {
                using (StreamReader file = new StreamReader(pathname)){
                    int y = 0;
                    while (!file.EndOfStream)
                    {
                        string line = file.ReadLine();
                        char[] cLine = line.ToCharArray();

                        for (int x = 0; x < line.Length; x++)
                        {
                            nMap.Tiles[y, x] = Tile.getTile(cLine[x]);
                        }
                        y++;
                    }
                }
            }
            catch (FileNotFoundException e)
            {
                Console.WriteLine("File couldn't be read.");
                Console.WriteLine(e.StackTrace);
            }

            return nMap;
            
        }
        */

        public static Map LoadMap2(string pathname)
        {
            Map nMap = new Map();
            try{
                using (StreamReader file = new StreamReader(pathname)){
                    Dictionary<char, string> tileKey = new Dictionary<char,string>();

                    List<List<char>> rawmap = new List<List<char>>();

                    int y = 0;
                    while (!file.EndOfStream){
                        string line = file.ReadLine();
                        if (line.StartsWith("<")){
                            while (!file.EndOfStream){
                                
                                line = file.ReadLine();
                                Console.WriteLine(line);

                                if (line.StartsWith(">")){
                                    line = file.ReadLine();
                                    break;
                                }
                                try{
                                    char[] kLine = line.ToCharArray();
                                    tileKey.Add(kLine[0], line.Substring(line.IndexOf('=')+1).Trim());
                                }
                                catch (Exception o){
                                    Console.WriteLine("bad file line");
                                    Console.WriteLine(o);
                                }
                            }
                        }
                        char[] cLine = line.ToCharArray();
                        List<char> toAdd = new List<char>();
 
                        for (int x = 0; x < line.Length; x++){
                            char raw = cLine[x];
                            
                            toAdd.Add(raw);
                        }
                        rawmap.Add(toAdd);
                        y++;
                    }
                    rawmap.Reverse();
                    //
                    for (int y1 = 0; y1 < rawmap.Count; y1++){
                        for (int x1 = 0; x1 < rawmap[y1].Count; x1++){
                            //Load tile
                            string tileDat = tileKey[rawmap[y1][x1]];
                            switch (tileDat){
                                case "null":
                                    nMap.Tiles[y1, x1] = null;
                                    break;
                                case "Player":
                                    Player p = Game1.CurrentGame.Player;
                                    p.Position = new Vector2(x1 * Tile.TILE_WIDTH + Tile.TILE_WIDTH/2, y1 * Tile.TILE_WIDTH + p.Size.Y/2);
                                    p.Parent = nMap;
                                    nMap.Player = p;
                                    break;
                                case "Baddu":
                                    Baddu b = new Baddu();
                                    b.Position = new Vector2(x1 * Tile.TILE_WIDTH + Tile.TILE_WIDTH / 2, y1 * Tile.TILE_WIDTH + b.Size.Y / 2);
                                    b.Parent = nMap;
                                    break;
                                default:
                                    try{
                                        string type = tileDat.Substring(0, tileDat.IndexOf(','));
                                        int multi = Convert.ToInt32(tileDat.Substring(tileDat.IndexOf(',') + 1).Trim());

                                        nMap.Tiles[y1, x1] = Tile.getVariation(type, multi * Tile.VARS);
                                    }
                                    catch (Exception i){
                                        Console.WriteLine("Tile couldn't be loaded, bad key");
                                        Console.WriteLine(i);
                                    }
                                    break;
                            }

                        }
                    }
                }
            }
            catch (FileNotFoundException e){
                Console.WriteLine("File couldn't be read.");
                Console.WriteLine(e.StackTrace);
            }
            return nMap;
        }

    }
}
