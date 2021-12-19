using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
//using Random = System.Random;

public class LudoGame : MonoBehaviour
{
    private int[,] grid;
    private List<GameObject> ToDelete;
    public GameObject tile;
    public GameObject plateau;
    public GameObject pion3D;
    public GameObject Dice;
    public Material test;
    public List<GameObject> yellowHome, redHome, arena, finishYellow, finishRed, gamelist, newPath, yellowPath;
    private bool jeton = true;
    public int deepness = 2;
    private string yellow;
    public static GameObject selectedPlayer;
    private static Rigidbody rb;
    public static Vector3 diceVelocity;
    public static int diceNumber;
    public int dice;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void DrawTable(int x, int y)
    {
        var g = GameObject.Instantiate(tile, new Vector3((plateau.transform.position.x-0.18f)+(x*0.03f), plateau.transform.position.y, (plateau.transform.position.z-0.18f+(y*0.03f))), Quaternion.identity);
        g.name = x.ToString() + "," + y.ToString() + "#" + string.Empty;
        g.transform.parent = plateau.transform;
        g.tag = "white";
        gamelist.Add(g);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(diceNumber.ToString());
        
    }

    public void StartGame()
    {
        Debug.Log("Game started");
        for (int x = 0; x < 13; x++)
        {
            for (int y = 0; y < 13; y++)
            {
                DrawTable(x, y);
            }
        }
        DrawCross();
        SortPlayers();
        Path(gamelist);
        diceNumber = 6;
        yellowPath = newPath;
        YellowPathMaker(yellowPath);
        StartCoroutine(TestPath(yellowPath));
    }

    private IEnumerator TestPath(List<GameObject> parth)
    {
        foreach(GameObject gameObject in parth)
        { 
            gameObject.GetComponent<MeshRenderer>().material = test;
            yield return new WaitForSecondsRealtime(1);
        }
    }

    private void Path (List<GameObject> path)
    {
        int a = 5, b = 7;
        
        for (int x = 0; x < 1; x++)
        {
            for (int y = 5; y <8; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 7; y < 8; y++)
        {
            for (int x = 1; x < 6; x++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int x = 5; x < 6; x++)
        {
            for (int y = 8; y < 13; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 12; y < 13; y++)
        {
            for (int x = 6; x < 8; x++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int x = 7; x < 8; x++)
        {
            for (int y = 11; y > 6; y--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 7; y < 8; y++)
        {
            for (int x = 8; x < 13; x++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int x = 12; x < 13; x++)
        {
            for (int y = 6; y > 4; y--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 5; y < 6; y++)
        {
            for (int x = 11; x > 6; x--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int x = 7; x < 8; x++)
        {
            for (int y = 4; y >= 0; y--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 0; y < 1; y++)
        {
            for (int x = 6; x > 4; x--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int x = 5; x < 6; x++)
        {
            for (int y = 1; y < 6; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
        for (int y = 5; y < 6; y++)
        {
            for (int x = 4; x > 0; x--)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                newPath.Add(g);
            }
        }
    }

    //Let's make a Yellow Path
    private void YellowPathMaker(List<GameObject> gameObjects)
    {
        gameObjects.AddRange( gameObjects.GetRange(0,39));
        gameObjects.RemoveRange(0, 39);
        yellowPath.RemoveAt(yellowPath.Count-1);
        yellowPath.AddRange(finishYellow.GetRange(0,5));  
    }
    private void SortPlayers()
    {
        yellowHome = GameObject.FindGameObjectsWithTag("yellow").ToList();
        GameObject yellowStart = GameObject.Find(5 + "," + 1 + "#" + string.Empty);
        yellowStart.tag = "startyellow";
        gamelist.Add(yellowStart);
    }

    public void DrawCross()
    {

        for (int x = 0; x < 5; x++)
        {
            for (int y = 0; y <5; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                g.tag = "Player";
                gamelist.Remove(g);
                Destroy(g);
               
                
            }
        }
        for (int x = 8; x < 13; x++)
        {
            for (int y = 0; y < 5; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                g.tag = "Player";
                gamelist.Remove(g);
                Destroy(g);

            }
        }
        for (int x = 0; x < 5; x++)
        {
            for (int y = 8; y < 13; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                g.tag = "Player";
                gamelist.Remove(g);
                Destroy(g);

            }
        }
        for (int x = 8; x < 13; x++)
        {
            for (int y = 8; y < 13; y++)
            {
                GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                g.tag = "Player";
                gamelist.Remove(g);
                Destroy(g);

            }
        }
        GameObject center = GameObject.Find(6.ToString() + "," + 6.ToString() + "#" + string.Empty);
        gamelist.Remove(center);
        Destroy(center);
        //Just to color some lines on the board in yellow and red
        int redLineY = 6;
        int[] redlineX = {1,2,3,4,5,7,8,9,10,11};
        int[] yellowLineY = {1,2,3,4,5,7,8,9,10,11};
        int yellowLineX = 6;
        foreach (int x in redlineX)
        {
            GameObject g = GameObject.Find(x.ToString() + "," + redLineY.ToString() + "#" + string.Empty);
            var mesh = g.GetComponent<MeshRenderer>().material.color = Color.red;
            gamelist.Remove(g);
            finishRed.Add(g);
            g.tag = "finishRed";
        }
        foreach (int y in yellowLineY)
        {
            GameObject g = GameObject.Find(yellowLineX.ToString() + "," + y.ToString() + "#" + string.Empty);
            var mesh = g.GetComponent<MeshRenderer>().material.color = Color.yellow;
            gamelist.Remove(g);
            finishYellow.Add(g);
            g.tag = "finishYellow";
        }
    }

    // Let's simulate the game
    // Game starts if a player can have a 6 from dice
    private void playGame()
    {
        if (jeton == false)
        {
            print("Au tour de l'ordinateur");
            StartCoroutine(CPUGame());
        }
        else
            if (jeton == true)
        {
            StartCoroutine(PlayerGame());
        }
        /*
        Random random = new Random();
        int dice = random.Next(1,7);
        print(dice);
        if(yellowHome.Count == 4)
        {
            if (dice == 6 && jeton == true)
            {
               // 
               // print(cost.Length);
               // print(yellowHome[0].name);
                //cost[1] = (39).ToString();
                yellowHome[0].name = yellowHome[0].name +"#"+ 39;
                yellowHome[0].transform.localPosition = newPath[39].transform.localPosition;
                arena.Add(yellowHome[0]);
                yellowHome.RemoveAt(0);
            }
             // On passe le tour au joueur suivant
           /* GameObject g = GameObject.Find(0 + "," + 7 + "#" + string.Empty);
            GameObject redPion = GameObject.Find("yellow1");
            redPion.transform.parent = plateau.transform;
            redPion.transform.localPosition = g.transform.localPosition;
        }
        else if (yellowHome.Count != 4 && yellowHome.Count !=0)
        {
            switch (dice)
            {
                default:
                   GameObject g = arena.Find(x => x.tag.Contains("yellow"));
                    String[] cost = g.name.Split('#');
                    int step = dice + int.Parse(cost[1]);
                    g.transform.localPosition = newPath[dice+1].transform.localPosition;
                    print(g.name);
                    break;
            }
        }*/

    }
    IEnumerator DiceTrick()
    {
        GameObject newDice = GameObject.Instantiate(Dice);
        newDice.transform.position = new Vector3(plateau.transform.position.x, plateau.transform.position.y+0.2f , plateau.transform.position.z );
        rb = newDice.GetComponent<Rigidbody>();
        newDice.tag = "dice";
        diceVelocity = rb.velocity;
        
        float dirX = UnityEngine.Random.Range(0, 500);
        float dirY = UnityEngine.Random.Range(0, 500);
        float dirZ = Random.Range(0, 500);
        transform.position = new Vector3(0, 2, 0);
        transform.rotation = Quaternion.identity;
        rb.AddForce(transform.up * 500);
        rb.AddTorque(dirX, dirY, dirZ);
        selectedPlayer = null;
        yield return new WaitForSeconds(5);
    }

    IEnumerator PlayerGame()
    {
        StartCoroutine(DiceTrick());

        //Check dice value
        print("Play for " + diceNumber);
        //dice = diceNumber;
        //Select a player
        print("Select a player");
        yield return StartCoroutine(WaitForPlayerSelection());
        Debug.Log("Ok. You selected a Player : " + selectedPlayer.name);
        Destroy(GameObject.FindGameObjectWithTag("dice"));
        if (yellowHome.Contains(selectedPlayer))
        {
            if (diceNumber == 6)
            {
                print("You can move");
                Vector3 positionToMove = new Vector3(yellowPath[0].transform.position.x, yellowPath[0].transform.position.y+0.028f, yellowPath[0].transform.position.z);
                selectedPlayer.transform.position = Vector3.MoveTowards(selectedPlayer.transform.position, positionToMove, 200 * Time.deltaTime);
                string[] split = selectedPlayer.name.Split('#');
                selectedPlayer.name = split[0].ToString() + "#" + 0.ToString();
                arena.Add(selectedPlayer);
                yellowHome.Remove(selectedPlayer);
                jeton ^= jeton;
                print("Joueur Décplacé");

            }
            if(diceNumber != 6)
            {
                print("Il faut jouer :" + diceNumber);
                print("Impossible de Déplacer le joueur. Il faut un 6. Tu as un : " + diceNumber);
                jeton ^= jeton;
                playGame();
            }
            selectedPlayer = null;   
        }else
        if (arena.Contains(selectedPlayer))
        {
            string[] split1 = selectedPlayer.name.Split('#');
            int posInPath = int.Parse(split1[1]) + diceNumber;
            print("Deplacement de : " + diceNumber);
            Vector3 newPos = new Vector3(yellowPath[posInPath].transform.position.x, yellowPath[posInPath].transform.position.y+0.028f, yellowPath[posInPath].transform.position.z);
            selectedPlayer.transform.position = newPos;
            selectedPlayer.name = split1[0].ToString() + "#" + posInPath.ToString();
            jeton ^= jeton;
            playGame();
        }
    }

    IEnumerator WaitForPlayerSelection()
    {
        while (selectedPlayer == null)
        {
            yield return null;
        }
        print("Fin d'attente");
    }

    IEnumerator CPUGame()
    {
        StartCoroutine(DiceTrick());
        print("Le CPU doit jouer " + diceNumber);
        if (redHome)
        yield return null;
        //Pseudo Code
       // MiniMax(arena, depth);

    }

    private int HeuristicValueOfNode()
    {
        throw new NotImplementedException();
    }
    
    /*
     * This function returns true, if there are moves remaining on the board. It returns false if there are no moves left to play.
     * 
     */
    private Boolean isMovesLeft(List<GameObject> gameObjects)
    {
        if (gameObjects.Contains(GameObject.FindGameObjectWithTag("red")) == true || redHome.Count != 0)
        {
            return true;
        }
        else
            return false;
    }

    /*
     This is the evalution function
     */
    private int evaluate(List<GameObject> gameObjects)
    {
        // Checking for victory
        return 0;
    }

    /*
     * This is the minimax function. It considers all the possible ways the game can go ans returns the value of the board
     */
    private int MiniMax(List<GameObject> gameObjects, int depth, Boolean isMax, int diceValue)
    {
        int score = evaluate(gameObjects);
        //if Maximizer has won the game, return his evaluate score
        if (score == 10)
            return score;

        //if Minimizer has won the game, return his evaluate score
        if (score == -10)
            return score;

        //if there are no more moves and no winners
        if (isMovesLeft(gameObjects) == false)
            return score;

        //If this maximizer's move
        if (isMax)
        {
            int best = -1000;
            // Traverse all game
            foreach(GameObject gameObject in GameObject.FindGameObjectsWithTag("red"))
            {
                if (arena.Contains(gameObject))
                {

                }
            }
        }
        return score;
    }
}
