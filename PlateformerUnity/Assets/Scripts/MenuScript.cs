using UnityEngine;
/// <summary>
/// Title screen script
/// </summary>
public class MenuScript : MonoBehaviour
{
    public string buttonName = "Start !";
    public string sceneName = "";
    public int width = 84;
    public int heigth = 60;
    public float screenWidth = 2;
    public float screenHeigth = 1.5f;

    public GUISkin skin = null;

	void OnGUI()
	{
        if(skin != null){
            GUI.skin = skin;
        }
		// Determine the button's place on screen
		// Center in X, 2/3 of the height in Y
        float rectWidth = (Screen.width / screenWidth) - (width / 2);
        float rectHeigth = (Screen.height / screenHeigth) - (heigth / 2);
        Rect buttonRect = new Rect(rectWidth, rectHeigth, width, heigth);
		// Draw a button to start the game
        if (GUI.Button(buttonRect, buttonName))
		{
			// On Click, load the first level.
			// "Stage1" is the name of the first scene we created.
            if(sceneName!="quit"){
                Application.LoadLevel(sceneName);
            }else {
                Application.Quit();
            }
        }

	}
}