using UnityEngine;
using System;
using System.Collections;
using  System.Collections.Generic;

public class ProceduralGeneration : MonoBehaviour {

    public static int      width              = 40;
    public static int      height             = 32;
    public int             chanceToStartAlive = 45;
    private static int[,]  level              = new int[width,height];
    public float           blocDimension      = 0.75f;
    private int            entreX;
    private int            entreY;
    private int            sortieX;
    private int            sortieY;
    private bool           hasPath;
    public int             deathLimit         = 4;
    public int             birthLimit         = 5;
    
	void Start () {
        createMap();
        placeDoors();
        level = initialiseMap(level);
        do
        {
            level = doSimulationStep(level);
            correctPath(entreX, entreY, null);
        } while (!hasPath);

        displayLevel();

	}

    private void createMap()
    {
        level = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if ((i == 0)||(j == 0)||(i == width - 1)||(j== height - 1)) {
                    level[i, j] = BlocConstant.BLOC_NUMBER;
                }
            }
        }
    }

    private void displayLevel()
    {

        float nextPosition_x = 0.0f;
        float nextPosition_y = 0.0f;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                if (level[i, j] == BlocConstant.BLOC_NUMBER)
                {
                    Instantiate(Resources.Load(BlocConstant.BLOC_NAME), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                }
                if (level[i, j] == BlocConstant.ENTER_NUMBER)
                {   
                    Instantiate(Resources.Load("Character"), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                    Instantiate(Resources.Load(BlocConstant.ENTER_NAME), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                }
                if (level[i, j] == BlocConstant.EXIT_NUMBER)
                {
                    Instantiate(Resources.Load(BlocConstant.EXIT_NAME), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                }
                nextPosition_y -= blocDimension;
            }
            nextPosition_x += blocDimension;
            nextPosition_y = 0;
        }
    }

    private void correctPath(int entryPointX, int entryPointY, int[,] listDejaVu) {
        if (listDejaVu == null) {
            listDejaVu = new int[width,height];
        }
        listDejaVu[entryPointX,entryPointY] = BlocConstant.BLOC_NUMBER;
        if (((entryPointX == sortieX) && (entryPointY == sortieY)) || hasPath) {
            hasPath = true;
        } else {
            List<Coordonnee> listCoordonne = getVoisinAtteignable(entryPointX, entryPointY, listDejaVu);
            foreach (Coordonnee coordonne in listCoordonne) {
                correctPath(coordonne.getX(), coordonne.getY(), listDejaVu);
            }
        }
    }

    private static List<Coordonnee> getVoisinAtteignable(int entryPointX, int entryPointY, int[,] listDejaVu)
    {
        List<Coordonnee> result = new List<Coordonnee>();
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = entryPointX + i;
                int neighbour_y = entryPointY + j;

                if (Math.Abs(i) == Math.Abs(j))
                {
                }
                else if ((neighbour_x < 0) || (neighbour_y < 0) || (neighbour_x >= width) || (neighbour_y >= height))
                {
                }
                else if ((level[neighbour_x,neighbour_y] != 1) && (listDejaVu[neighbour_x,neighbour_y] == 0))
                {
                    Coordonnee toAdd = new Coordonnee(neighbour_x, neighbour_y);
                    result.Add(toAdd);
                }
            }
        }
        return result;
    }

    private int[,] initialiseMap(int[,] map) {
        for (int i = 1; i < (width-1); i++) {
            for (int j = 1; j < (height-1); j++) {
                if (map[i, j] == 0)
                {
                    if ((UnityEngine.Random.Range(0,100)) < chanceToStartAlive) {
                        map[i,j] = BlocConstant.BLOC_NUMBER;
                    }
                }
            }
        }

        return map;
    }

    private void placeDoors() {
        Coordonnee entryChoice = new Coordonnee(UnityEngine.Random.Range(1, 4), UnityEngine.Random.Range(1, 4));
        int[,] map = new int[4,4];
        List<Coordonnee> exitChoice = getNotVoisinAtSomeDistance(map, 4, 4, entryChoice, 2, 0);
        
        int choice = UnityEngine.Random.Range(0,exitChoice.Count);
        Coordonnee exitCoordonne = exitChoice[choice];
        placeEntryDoors(entryChoice);
        placeExitDoors(exitCoordonne);
    }

    private void placeEntryDoors(Coordonnee doorPlace){
        entreX = UnityEngine.Random.Range((1 + (doorPlace.getX() * 10)), (((doorPlace.getX()+1) * 10)-1));
        entreY = UnityEngine.Random.Range((2 + (doorPlace.getY()) * 8), (((doorPlace.getY()+1) * 8) - 1));
        level[entreX, entreY] = BlocConstant.ENTER_NUMBER;
        level[entreX, entreY + 1] = BlocConstant.BLOC_NUMBER;
    }

    private void placeExitDoors(Coordonnee doorPlace)
    {
        sortieX = UnityEngine.Random.Range((1 + (doorPlace.getX() * 10)), (((doorPlace.getX() + 1) * 10) - 1));
        sortieY = UnityEngine.Random.Range((2 + (doorPlace.getY()) * 8), (((doorPlace.getY() + 1) * 8) - 1));
        level[sortieX, sortieY] = BlocConstant.EXIT_NUMBER;
        level[sortieX, sortieY + 1] = BlocConstant.BLOC_NUMBER;
    }

    private List<Coordonnee> getVoisinAtSomeDistance(int[,] map, int mapWidth, int mapHeight, Coordonnee startPoint, double distance, int forJump) {
        List<Coordonnee> result = new List<Coordonnee>();

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                Coordonnee distantePoint = new Coordonnee(i, j);
                double distanceCalcule = startPoint.getDistance(distantePoint);
                if (distanceCalcule <= distance && map[i, j] == forJump && distanceCalcule != 0)
                {
                    result.Add(distantePoint);
                }
            }
        }

        return result;
    }

    private List<Coordonnee> getNotVoisinAtSomeDistance(int[,] map, int mapWidth, int mapHeight, Coordonnee startPoint, double distance, int forJump)
    {
        List<Coordonnee> result = new List<Coordonnee>();

        for (int i = 0; i < mapWidth; i++)
        {
            for (int j = 0; j < mapHeight; j++)
            {
                Coordonnee distantePoint = new Coordonnee(i, j);
                double distanceCalcule = startPoint.getDistance(distantePoint);
                if (distanceCalcule > distance && map[i, j] == forJump)
                {
                    result.Add(distantePoint);
                }
            }
        }

        return result;
    }

    private int[,] doSimulationStep(int[,] map)
    {
        int[,] newMap = new int[width, height];
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                int nbsAlive = countAliveNeighbours(map, i, j);
                if ((i == 0) || (j == 0) || (i == width - 1) || (j == height - 1))
                {
                    newMap[i, j] = BlocConstant.BLOC_NUMBER;
                }else if(((i == entreX)&&(j == entreY +1))||((i== sortieX)&&(j==sortieY + 1))){
                    newMap[i, j] = BlocConstant.BLOC_NUMBER;
                }else if (map[i, j] == BlocConstant.ENTER_NUMBER) {
                    newMap[i, j] = BlocConstant.ENTER_NUMBER;
                }
                else if (map[i, j] == BlocConstant.EXIT_NUMBER)
                {
                    newMap[i, j] = BlocConstant.EXIT_NUMBER;
                }
                else if (getVoisinAtSomeDistance(map, width, height, new Coordonnee(i, j), 2, 1).Count == 0)
                {
                    newMap[i,j] = BlocConstant.BLOC_NUMBER;
                }
                else if (map[i, j] == 1)
                {
                    if (nbsAlive < deathLimit) newMap[i, j] = 0;
                    else newMap[i, j] = BlocConstant.BLOC_NUMBER;
                }
                else if (map[i, j] == 0)
                {
                    if (nbsAlive > birthLimit) newMap[i, j] = BlocConstant.BLOC_NUMBER;
                    else newMap[i, j] = 0;
                }
            }
        }

        return newMap;
    }

    private int countAliveNeighbours(int[,] map, int x, int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;

                if (i == 0 && j == 0)
                {
                }
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x >= width  || neighbour_y >= height )
                {
                    count++;
                }
                else if (map[neighbour_x, neighbour_y] == 1)
                {
                    count++;
                }
            }
        }

        return count;

    }
}
