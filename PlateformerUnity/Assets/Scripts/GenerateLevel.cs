using UnityEngine;
using System.Collections;

public class GenerateLevel : MonoBehaviour {

    public static int width = 64;
    public static int height = 32;
    public int numberOfSteps = 4;
    public int deathLimit = 4;
    public int birthLimit = 5;
    public int chanceToStartAlive = 45;
    public int chanceToPlaceTreasure = 15;
    public int chanceToPlaceDoor = 5;
    public float blocDimension = 0.75f;
    public int treasureHiddenLimit = 5;

    public GameObject[] blocPrefab;
    public GameObject chestPrefab;
    public GameObject entryPrefab;
    public GameObject exitPrefab;
    public GameObject playerPrefab;

    private float doorDimension = 1.05f;

	// public bool [,] map = new bool [width,height];
    public int[,] map = new int[width, height];

    
    // Use this for initialization
	void Start () {
        // bool[,] finalMap = generateMap(map);
        int nbDoors = 0;
        int[,] finalMap = generateMap(map);

        float nextPosition_x = 0.0f;
        float nextPosition_y = 0.0f;

        for (int i = 0; i < width-1; i++)
        {
            for (int j = 0; j < height-1; j++)
            {
                if (i == 0 | j == 0 | i == width - 2 | j == height - 2)
                {
                    Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                    nextPosition_y -= blocDimension;
                }
                else
                {
                    if (finalMap[i, j] == 1)
                    {
                        // Create a new bloc
                        //Instantiate(blocPrefab[0], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                        chooseBloc(finalMap, i, j, nextPosition_x, nextPosition_y);
                        nextPosition_y -= blocDimension;

                    }
                    else 
                    {
                        // Can we place an object ?
                        //int count = countAliveNeighbours(finalMap, i, j);
                        //if (count >= treasureHiddenLimit)
                        //{
                        //    Instantiate(chestPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                        //}

                        if (finalMap[i, j + 1] == 1)
                        {
                            if (finalMap[i - 1, j] != 2 && finalMap[i + 1, j] != 2) {
                                finalMap[i, j] = 2;
                                if (Random.Range(0, 100) < chanceToPlaceTreasure)
                                {
                                    Instantiate(chestPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                                }
                            }
                        }

                        // Can we place a door ?
                        // Entry Door
                        //if (j < height / 4)
                        //{
                        //    if (finalMap[i, j] == 0 & finalMap[i, j - 1] == 0 & finalMap[i, j + 1] == 1)
                        //    {
                        //        if (nbDoors == 0)
                        //        {
                        //            if (Random.Range(0, 100) < chanceToPlaceDoor)
                        //            {
                        //                Instantiate(entryPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                        //                nbDoors++;
                        //            }
                        //        }
                        //    }
                        //}


                        // Exit Door
                        //if (j > 3 * (height / 4))
                        //{
                        //    if (finalMap[i, j] == 0 & finalMap[i, j - 1] == 0 & finalMap[i, j + 1] == 1)
                        //    {
                        //        if (nbDoors == 1)
                        //        {
                        //            if (Random.Range(0, 100) < chanceToPlaceDoor)
                        //            {
                        //                Instantiate(exitPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                        //                nbDoors++;
                        //            }
                        //        }
                        //    }
                        //}

                        // Next position
                        nextPosition_y -= blocDimension;
                    }
                }
            }
            nextPosition_x += blocDimension;
            nextPosition_y = 0;
        }
        placeDoors(finalMap);
                
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    // Method for choosing a bloc in function of its neighbour
    private void chooseBloc(int[,] finalMap, int i, int j, float nextPosition_x, float nextPosition_y)
    {
        int count = 0;

        // bool hautGauche = finalMap[i - 1, j - 1];
        // bool haut = finalMap[i, j - 1];
        // bool hautDroite = finalMap[i+1, j - 1];
        // bool droite = finalMap[i + 1, j];
        // bool basDroite = finalMap[i + 1, j + 1];
        // bool bas = finalMap[i, j + 1];
        // bool basGauche = finalMap[i - 1, j + 1];
        // bool gauche = finalMap[i - 1, j];

        if (finalMap[i - 1, j - 1]==1)
        {
            count += 1;
        }
        if (finalMap[i, j - 1]==1)
        {
            count += 2;
        }
        if (finalMap[i + 1, j - 1]==1)
        {
            count += 4;
        }
        if (finalMap[i + 1, j]==1)
        {
            count += 8;
        }
        if (finalMap[i + 1, j + 1]==1)
        {
            count += 16;
        }
        if (finalMap[i, j + 1]==1)
        {
            count += 32;
        }
        if (finalMap[i - 1, j + 1]==1)
        {
            count += 64;
        }
        if (finalMap[i - 1, j]==1)
        {
            count += 128;
        }

        switch (count)
        {
            case 1:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 2:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 3:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 4:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 5:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 6:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 7:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 8:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 9:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 10: // New Bloc
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 11: // New Bloc
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 12: 
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 13:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 14:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 15:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 16:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 17:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 18:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 19:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 20:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 21:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 22:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 23:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 24:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 25:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 26:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 27:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 28:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 29:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 30:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 31:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 32:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 33:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 34:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 35:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 36:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 37:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 38:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 39:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 40:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 41:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 42:
                Instantiate(blocPrefab[42], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 43:
                Instantiate(blocPrefab[42], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 44:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 45:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 46:
                Instantiate(blocPrefab[41], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 47:
                Instantiate(blocPrefab[41], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 48:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 49:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 50:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 51:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 52:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 53:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 54:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 55:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 56:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 57:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 58:
                Instantiate(blocPrefab[40], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 59:
                Instantiate(blocPrefab[40], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 60:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 61:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 62:
                Instantiate(blocPrefab[4], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 63:
                Instantiate(blocPrefab[4], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 64:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 65:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 66:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 67:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 68:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 69:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 70:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 71:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 72:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 73:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 74:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 75:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 76:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 77:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 78:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 79:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 80:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 81:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 82:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 83:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 84:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 85:
                Instantiate(blocPrefab[13], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 86:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 87:
                Instantiate(blocPrefab[11], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 88:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 89:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 90:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 91:
                Instantiate(blocPrefab[22], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 92:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 93:
                Instantiate(blocPrefab[10], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 94:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 95:
                Instantiate(blocPrefab[7], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 96:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 97:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 98:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 99:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 100:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 101:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 102:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 103:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 104:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 105:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 106:
                Instantiate(blocPrefab[42], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 107:
                Instantiate(blocPrefab[42], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 108:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 109:
                Instantiate(blocPrefab[23], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 110:
                Instantiate(blocPrefab[41], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 111:
                Instantiate(blocPrefab[41], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 112:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 113:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 114:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 115:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 116:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 117:
                Instantiate(blocPrefab[12], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 118:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 119:
                Instantiate(blocPrefab[15], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 120:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 121:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 122:
                Instantiate(blocPrefab[40], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 123:
                Instantiate(blocPrefab[40], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 124:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 125:
                Instantiate(blocPrefab[8], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 126:
                Instantiate(blocPrefab[4], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 127:
                Instantiate(blocPrefab[4], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 128:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 129:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 130:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 131:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 132:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 133:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 134:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 135:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 136:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 137:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 138:
                Instantiate(blocPrefab[39], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 139:
                Instantiate(blocPrefab[37], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 140:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 141:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 142:
                Instantiate(blocPrefab[38], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 143:
                Instantiate(blocPrefab[3], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 144:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 145:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 146:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 147:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 148:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 149:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 150:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 151:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 152:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 153:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 154:
                Instantiate(blocPrefab[39], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 155:
                Instantiate(blocPrefab[37], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 156:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 157:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 158:
                Instantiate(blocPrefab[38], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 159:
                Instantiate(blocPrefab[3], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 160:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 161:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 162:
                Instantiate(blocPrefab[36], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 163:
                Instantiate(blocPrefab[35], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 164:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 165:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 166:
                Instantiate(blocPrefab[36], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 167:
                Instantiate(blocPrefab[35], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 168:
                Instantiate(blocPrefab[33], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 169:
                Instantiate(blocPrefab[33], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 170:
                Instantiate(blocPrefab[44], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 171:
                Instantiate(blocPrefab[46], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 172:
                Instantiate(blocPrefab[33], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 173:
                Instantiate(blocPrefab[33], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 174:
                Instantiate(blocPrefab[45], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 175:
                Instantiate(blocPrefab[43], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 176:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 177:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 178:
                Instantiate(blocPrefab[36], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 179:
                Instantiate(blocPrefab[35], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 180:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 181:
                Instantiate(blocPrefab[20], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 182:
                Instantiate(blocPrefab[36], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 183:
                Instantiate(blocPrefab[35], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 184:
                Instantiate(blocPrefab[32], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 185:
                Instantiate(blocPrefab[32], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 186:
                Instantiate(blocPrefab[28], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 187:
                Instantiate(blocPrefab[26], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 188:
                Instantiate(blocPrefab[32], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 189:
                Instantiate(blocPrefab[32], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 190:
                Instantiate(blocPrefab[29], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 191:
                Instantiate(blocPrefab[18], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 192:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 193:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 194:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 195:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 196:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 197:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 198:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 199:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 200:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 201:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 202:
                Instantiate(blocPrefab[39], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 203:
                Instantiate(blocPrefab[37], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 204:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 205:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 206:
                Instantiate(blocPrefab[38], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 207:
                Instantiate(blocPrefab[3], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 208:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 209:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 210:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 211:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 212:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 213:
                Instantiate(blocPrefab[9], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 214:
                Instantiate(blocPrefab[21], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 215:
                Instantiate(blocPrefab[6], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 216:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 217:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 218:
                Instantiate(blocPrefab[39], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 219:
                Instantiate(blocPrefab[37], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 220:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 221:
                Instantiate(blocPrefab[14], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 222:
                Instantiate(blocPrefab[38], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 223:
                Instantiate(blocPrefab[3], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 224:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 225:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 226:
                Instantiate(blocPrefab[34], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 227:
                Instantiate(blocPrefab[2], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 228:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 229:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 230:
                Instantiate(blocPrefab[34], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 231:
                Instantiate(blocPrefab[2], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 232:
                Instantiate(blocPrefab[31], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 233:
                Instantiate(blocPrefab[31], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 234:
                Instantiate(blocPrefab[27], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 235:
                Instantiate(blocPrefab[24], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 236:
                Instantiate(blocPrefab[31], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 237:
                Instantiate(blocPrefab[31], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 238:
                Instantiate(blocPrefab[30], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 239:
                Instantiate(blocPrefab[17], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 240:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 241:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 242:
                Instantiate(blocPrefab[34], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 243:
                Instantiate(blocPrefab[2], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 244:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 245:
                Instantiate(blocPrefab[5], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 246:
                Instantiate(blocPrefab[34], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 247:
                Instantiate(blocPrefab[2], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 248:
                Instantiate(blocPrefab[1], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 249:
                Instantiate(blocPrefab[1], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 250:
                Instantiate(blocPrefab[25], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 251:
                Instantiate(blocPrefab[16], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 252:
                Instantiate(blocPrefab[1], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 253:
                Instantiate(blocPrefab[1], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
            case 254:
                Instantiate(blocPrefab[19], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;          
            default:
                Instantiate(blocPrefab[0], new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                break;
               
        }
        
    }

    // Method call to generate matrix
    private int[,] generateMap(int[,] map)
    {
        // Create a new map
        int[,] cellMap = new int[width, height];
        // Set up the map with random values
        cellMap = initialiseMap(cellMap);
        // And now run the simulation for a set number of steps
        for (int i = 0; i < numberOfSteps; i++)
        {
            cellMap = doSimulationStep(cellMap);
        }

        return cellMap;
    }

    // Method to initialize the matrix the first time.
    private int[,] initialiseMap(int[,] map)
    {
        for (int i = 0; i < width-1; i++)
        {
            for (int j = 0; j < height-1; j++)
            {
                if (Random.Range(0,100) < chanceToStartAlive)
                {
                    map[i, j] = 1;
                }
            }
        }

        return map;
    }

    // Method to create a more logic map
    private int[,] doSimulationStep(int[,] map)
    {
        int[,] newMap = new int[width, height];
        // Loop over each row and column of the map
        for (int i = 0; i < width-1; i++)
        {
            for (int j = 0; j < height-1; j++)
            {
                int nbsAlive = countAliveNeighbours(map, i, j);
                // The new value is based on our simulation rules
                // First, if a cell is alive but has too few neighbours, kill it.
                if (map[i, j] == 1)
                {
                    if (nbsAlive < deathLimit) newMap[i, j] = 0;
                    else newMap[i, j] = 1;
                } // Otherwise, if the cell is dead now, check if it has the right number of neighbours to be alive
                else
                {
                    if (nbsAlive > birthLimit) newMap[i, j] = 1;
                    else newMap[i, j] = 0;
                }
            }
        }

        return newMap;
    }

    // Method to determine the number of neighbour with the value 1
    private int countAliveNeighbours(int[,] map, int x, int y)
    {
        int count = 0;
        for (int i = -1; i < 2; i++)
        {
            for (int j = -1; j < 2; j++)
            {
                int neighbour_x = x + i;
                int neighbour_y = y + j;

                // If we're looking at the middle point
                if (i == 0 && j == 0)
                {
                    // Do nothing
                }
                // In case the index we're looking at it off the edge of map
                else if (neighbour_x < 0 || neighbour_y < 0 || neighbour_x > width || neighbour_y > height)
                {
                    count++;
                }
                // Otherwise a normal check of the neighbour
                else if (map[neighbour_x, neighbour_y] == 1)
                {
                    count++;
                }
            }
        }

        return count;

    }

    // Method to place objects in the world
    private void placeObjects(int[,] finalMap)
    {
        for (int i = 1; i < width - 2; i++)
        {
            for (int j = 1; j < height - 2; j++)
            {
                if (finalMap[i, j]==0)
                {
                    int count = countAliveNeighbours(finalMap, i, j);
                    if (count >= treasureHiddenLimit)
                    {
                        float nextPosition_x = i * blocDimension;
                        float nextPosition_y = j * blocDimension * -1;
                        Instantiate(chestPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                    }
                }
            }
        }
    }

    // Method to place objects in the world
    private void placeDoors(int[,] finalMap)
    {
        int nbDoor = 0;

        do
        {
            // Entry Door
            for (int i = 1; i < width / 4; i++)
            {
                for (int j = 1; j < height / 4; j++)
                {
                    if (finalMap[i, j] == 0 & finalMap[i, j - 1] == 0 & finalMap[i, j + 1] == 1)
                    {
                        if (nbDoor == 0)
                        {
                            if (Random.Range(0, 100) < chanceToPlaceDoor)
                            {
                                float nextPosition_x = i * blocDimension;
                                float nextPosition_y = j * blocDimension * -1;
                                Instantiate(entryPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                                Instantiate(playerPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                                nbDoor++;
                            }
                        }
                    }
                }
            }
        }
        while (nbDoor == 0);

        do
        {
            // Exit Door
            for (int i = width - 2; i > width / 4; i--)
            {
                for (int j = height - 2; j > height / 4; j--)
                {
                    if (finalMap[i, j] == 0 & finalMap[i, j - 1] == 0 & finalMap[i, j + 1] == 1)
                    {
                        if (nbDoor == 1)
                        {
                            if (Random.Range(0, 100) < chanceToPlaceDoor)
                            {
                                float nextPosition_x = i * blocDimension;
                                float nextPosition_y = j * blocDimension * -1;
                                Instantiate(exitPrefab, new Vector3(nextPosition_x, nextPosition_y, 0), Quaternion.Euler(0, 0, 0));
                                nbDoor++;
                            }
                        }
                    }
                }
            }
        }
        while (nbDoor == 1);
    }

}
