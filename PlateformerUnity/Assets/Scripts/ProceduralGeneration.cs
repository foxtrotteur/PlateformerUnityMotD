using UnityEngine;
using System;
using System.Collections;
using  System.Collections.Generic;

public class ProceduralGeneration : MonoBehaviour {

    public static int      width              = 40;
    public static int      height             = 32;
    public int      chanceToStartAlive = 45;
    private static int[,] level              = new int[width,height];
    public float           blocDimension      = 0.75f;
    private int     entreX;
    private int     entreY;
    private int     sortieX;
    private int     sortieY;
    private bool hasPath;
    
	void Start () {
        
        level = initialiseMap(level);
        placeDoors(level);

        correctPath(entreX, entreY, null);
        Debug.Log("la matrice est correct : " + hasPath);

        displayLevel();

	}

    private void displayLevel()
    {

        float nextPosition_x = 0.0f;
        float nextPosition_y = 0.0f;
        for (int i = 0; i < width - 1; i++)
        {
            for (int j = 0; j < height - 1; j++)
            {
                if (level[i, j] == 1)
                {
                    Instantiate(Resources.Load("prefabBloc_13"), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                }
                if (level[i, j] == 2)
                {   
                    if(i == entreX && j == entreY){
                        Instantiate(Resources.Load("Character"), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                    }
                    Instantiate(Resources.Load("exitPrefab"), new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                }
                nextPosition_y -= blocDimension;
            }
            nextPosition_x += blocDimension;
            nextPosition_y = 0;
        }
    }

    private void correctPath(int entryPointX, int entryPointY, int[,] listDejaVu) {
        // Instanciation de la matrice du déjà vu (seulement pour le premier loop)
        if (listDejaVu == null) {
            listDejaVu = new int[width,height];
        }
        // Mettre à jour la matrice des déjà vu
        listDejaVu[entryPointX,entryPointY] = 1;
        // Si on arrive sur la sortie ou que la sortie est déjà trouvé, on arrête les recherches et on dis qu'on a trouvé
        if (((entryPointX == sortieX) && (entryPointY == sortieY)) || hasPath) {
            hasPath = true;
        } else {
            // On cherche les voisins atteignable (haut bas gauche droite)
            List<Coordonnee> listCoordonne = getVoisinAtteignable(entryPointX, entryPointY, listDejaVu);
            foreach (Coordonnee coordonne in listCoordonne) {
                // et pour chaque on relance la recherche
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

                // If we're looking at the middle point
                if (Math.Abs(i) == Math.Abs(j))
                {
                    // Do nothing
                }
                // In case the index we're looking at it off the edge of map
                else if ((neighbour_x < 0) || (neighbour_y < 0) || (neighbour_x >= width) || (neighbour_y >= height))
                {
                }
                // Otherwise a normal check of the neighbour
                else if ((level[neighbour_x,neighbour_y] != 1) && (listDejaVu[neighbour_x,neighbour_y] == 0))
                {
                    Coordonnee toAdd = new Coordonnee();
                    toAdd.setX(neighbour_x);
                    toAdd.setY(neighbour_y);
                    result.Add(toAdd);
                }
            }
        }
        return result;
    }

    // Method to initialize the matrix the first time.
    private int[,] initialiseMap(int[,] map) {
        Debug.Log("constructing matrice");
        for (int i = 0; i < (width - 1); i++) {
            for (int j = 0; j < (height - 1); j++) {
                if ((UnityEngine.Random.Range(0,100)) < chanceToStartAlive) {
                    map[i,j] = 1;
                }
            }
        }

        return map;
    }

    // Method to place objects in the world
    private void placeDoors(int[,] finalMap) {
        Debug.Log("placing door");
        int nbDoor = 0;

        do {
            // Entry Door
            for (int i = 1; i < (width - 2); i++) {
                for (int j = 1; j < (height - 2); j++) {
                    if ((finalMap[i,j] == 0) & (finalMap[i,j - 1] == 0) & (finalMap[i,j + 1] == 1)) {
                        if (nbDoor < 2) {
                            if ((UnityEngine.Random.Range(0, 100)) < 5)
                            {
                                finalMap[i,j] = 2;
                                nbDoor++;
                                if (nbDoor == 1) {
                                    entreX = i;
                                    entreY = j;

                                } else if (nbDoor == 2) {
                                    sortieX = i;
                                    sortieY = j;

                                }
                            }
                        }
                    }
                }
            }
            Debug.Log("nbDoor : " + nbDoor);
        } while (nbDoor < 2);

    }
	

}
