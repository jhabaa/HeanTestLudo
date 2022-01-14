using Assets;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
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
    public List<GameObject> yellowHome, redHome, arena, finishYellow, finishRed, gamelist, newPath, yellowPath, redPath;
    public bool jeton = true;
    public int deepness = 2;
    private string yellow;
    public static GameObject selectedPlayer;
    private static Rigidbody rb;
    public static Vector3 diceVelocity;
    public static int diceNumber;
    public int dice;
    public GameObject textScreen;
    private TextMeshProUGUI screenText;
    private String[] minimaxi;
    public List<GameObject> feuillesEnArene;
    public AudioSource audioData;

    // Start is called before the first frame update
    void Start()
    {
        screenText = textScreen.GetComponent<TextMeshProUGUI>();
        audioData = GetComponent<AudioSource>();
        audioData.Play(0);
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
        //Debug.Log(diceNumber.ToString());
        
    }
    IEnumerator ShowText(String textToShow)
    {
        screenText.SetText(textToShow);
        yield return new WaitForSeconds(5);
        screenText.SetText("");
       
    }
    public void DiceRecognition()
    {
        StartCoroutine(ShowText("Dés reconnu"));
        print("Dés reconnu");
    }

    public void StartGame()
    {
        StartCoroutine(ShowText("Game Started"));
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
        yellowPath =new List<GameObject>(newPath);
        redPath = new List<GameObject>(newPath);
        YellowPathMaker(yellowPath);
        RedPathMaker(redPath);
        // StartCoroutine(TestPath(redPath));
        // playGame();


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
        //Colorions la première case
        yellowPath[0].GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    private void RedPathMaker(List<GameObject> gameObjects)
    {
        gameObjects.AddRange(gameObjects.GetRange(0, 3));
        gameObjects.RemoveRange(0, 3);
        redPath.RemoveAt(redPath.Count - 1);
        redPath.AddRange(finishRed.GetRange(0, 5));
        //On colorie la première case 
        redPath[0].GetComponent<MeshRenderer>().material.color = Color.red;
    }
    private void SortPlayers()
    {
        yellowHome = GameObject.FindGameObjectsWithTag("yellow").ToList();
        GameObject yellowStart = GameObject.Find(5 + "," + 1 + "#" + string.Empty);
        yellowStart.tag = "startyellow";
        gamelist.Add(yellowStart);
        //For the Red Team
        redHome = GameObject.FindGameObjectsWithTag("red").ToList();

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
            StartCoroutine(ShowText("Au tour de l'ordinateur"));
            StartCoroutine(CPUGame());
        }
        else
        if(jeton == true)
        {
            StartCoroutine(ShowText("A votre tour de jouer"));
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
        selectedPlayer = null;
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
        
        yield return null;
    }

    IEnumerator PlayerGame()
    {
        StartCoroutine(DiceTrick());

        //Check dice value
       // print("Play for " + diceNumber);
        //dice = diceNumber;
        //Select a player
        
        if (yellowHome.Count == 4)
        {
            if (diceNumber == 6)
            {
                StartCoroutine(ShowText("Select a player"));
                yield return StartCoroutine(WaitForPlayerSelection());
                Debug.Log("Ok. You selected a Player : " + selectedPlayer.name);
                Destroy(GameObject.FindGameObjectWithTag("dice"));

                print("You can move");
                Vector3 positionToMove = new Vector3(yellowPath[0].transform.position.x, yellowPath[0].transform.position.y+0.028f, yellowPath[0].transform.position.z);
                selectedPlayer.transform.position = Vector3.MoveTowards(selectedPlayer.transform.position, positionToMove, 200 * Time.deltaTime);
                string[] split = selectedPlayer.name.Split('#');
                selectedPlayer.name = split[0].ToString() + "#" + 0.ToString();
                arena.Add(selectedPlayer);
                yellowHome.Remove(selectedPlayer);
                jeton ^= jeton;
                print("Joueur Décplacé");
                playGame();

            }
            if(diceNumber != 6)
            {
                //print("Il faut jouer :" + diceNumber);
                StartCoroutine(ShowText(diceNumber + "Pas de deplacement possible"));
                jeton ^= jeton;
                playGame();
            }
            selectedPlayer = null;   
        }else
        {
            if (arena.Contains(selectedPlayer))
            {
                string[] split1 = selectedPlayer.name.Split('#');
                int posInPath = int.Parse(split1[1]) + diceNumber;
                print(posInPath);
                StartCoroutine(ShowText("Deplacement de : " + diceNumber));
                Vector3 newPos = new Vector3(yellowPath[posInPath].transform.position.x, yellowPath[posInPath].transform.position.y + 0.028f, yellowPath[posInPath].transform.position.z);
                selectedPlayer.transform.position = newPos;
                selectedPlayer.name = split1[0].ToString() + "#" + posInPath.ToString();
                jeton ^= jeton;
                playGame();
            }
        }
        
    }

    IEnumerator WaitForPlayerSelection()
    {
        while (selectedPlayer == null)
        {
            yield return null;
        }
    }

    IEnumerator JetonSwap()
    {
        jeton ^= jeton;
        yield return jeton;
    }

    IEnumerator CPUGame()
    {
        yield return StartCoroutine(DiceTrick());
        yield return new WaitForSeconds(3);
        StartCoroutine(ShowText("Le CPU doit jouer " + diceNumber));
        if (redHome.Count == 5)
        {
            if (diceNumber == 6)
            {
                print("Un joueur peu être déplacé pour l'arène");
                LaunchMinMax(arena, 2, true, diceNumber);
                jeton = true;
                
            }
            else
                print("No Move possible");
                LaunchMinMax(arena, 2, true, diceNumber);
                jeton = true;

        }
        else
            if(diceNumber == 6 && arena.Contains(GameObject.FindGameObjectWithTag("red")))
        {
            print("Nous pouvons soit faire sortir un joueur, soit avancer un pion de l'arène");
        }
        else
            if(diceNumber != 6 && arena.Contains(GameObject.FindGameObjectWithTag("red")))
        {
            print("Nous pouvons juste déplacer un pion de l'arène");
        }

        //Pseudo Code
        // MiniMax(arena, depth);
        yield return new WaitForSeconds(3);
        Destroy(GameObject.FindGameObjectWithTag("dice"));
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
    private int Evaluate(List<GameObject> gameObjects)
    {
        int score = 200;
        List<GameObject> redTeam = new List<GameObject>();
        List<GameObject> yellowTeam = new List<GameObject>();
        foreach(GameObject gameObject in gameObjects)
        {
            if (gameObject.tag == "red")
            {
                redTeam.Add(gameObject);
            }
            else
            if (gameObject.tag == "yellow")
            {
                yellowTeam.Add(gameObject);
            }
        }
        //Maintenant on peut comparer les pions de léquipe rouge avec ceux de l'équipe jaune
        foreach(GameObject pionRed in redTeam)
        {
            string[] redSplit = pionRed.name.Split('#');
            foreach(GameObject pionYellow in yellowTeam)
            {
                string[] yellowSplit = pionYellow.name.Split('#');
                //On peut retrouver les indexes des pions sur le chemin et les comparer pour savoir s'ils sont sur la même position
                int redPos = newPath.FindIndex(x=> x == redPath[int.Parse(redSplit[1])]);
                int yellowPos = newPath.FindIndex(y => y == redPath[int.Parse(yellowSplit[1])]);
                //Maintenant comparons
                if(yellowPos == redPos)
                {
                    //Alors nous venons de nous prendre un pion jaune dans la gueule. Mauvais plan... -100
                    score = score - 100;

                }
                if((yellowPos-redPos)<7)
                {
                    //dans ce cas, nous avons un point jaune à un tour de dé. Bon plan +200
                    score = score + 200;
                }
                if(redPos - yellowPos < 7)
                {
                    //C'est chaud nous avons un pion aux trousses + 100
                    score = score + 200;
                }
                if(redPos == null)
                {
                    //C'est bon signe, nous sortons de l'arène + 500
                    score = score + 500;
                }
            }
        }

        // Checking for victory
        return score;
    }

    private void MoveInArena(List<GameObject> list, GameObject gameObject, int value)
    {
        GameObject exGameObject;
        exGameObject = list[list.IndexOf(gameObject) + value];

    }
    private void LaunchMinMax(List<GameObject> gameObjects, int depth, Boolean isMax, int diceValue)
    {
        print("Start Min MAx");
        //List<GameObject> fakeArena = gameObjects;
        //Creation du Root
        if(diceValue == 6)
        {
            if(redHome.Count !=0 ) // La maison n'est pas vide
            {
                bool pionOnEntry = false;
               foreach(GameObject gameObject in arena)
                {
                    string[] splitName = gameObject.name.Split('#');
                    if (splitName[1] == "0" && gameObject.tag == "red")
                    {
                        pionOnEntry = true;
                    }
                }

               //Si un pion occupe l'entrée nous ne pouvons pas sortir un nouveau pion
               if (pionOnEntry == true)
                {
                    Feuille arbre = DrawTree(gameObjects, diceValue);
                    MoveAfterMinMax(arbre,diceValue);
                }
                else
                {
                    //Pas le choix, on sort un joueur
                    List<GameObject> state = gameObjects;
                    GameObject pion = Instantiate(redHome[0]);
                    pion.name = redHome[0].name;
                    string[] split = pion.name.Split('#');
                    pion.name = split[0].ToString() + "#" + 0.ToString();
                    state.Add(pion);
                    //Déplacement d'un pion 
                    MoveFromHome(pion.tag);


                    //Ici un pion fictif est déjà crée. Et l'état du jeu contient au moins un pion. On peut donc construire l'arbre
                    Feuille root = DrawTree(state, diceValue);

                    //  Evaluation(root);
                }


            }
            else 
                if (redHome.Count == 0)
                {
                    //On peut jouer sur les joueurs existants. On construit l'arbre 
                    Feuille arbre = DrawTree(gameObjects, diceValue);
                    MoveAfterMinMax(arbre,diceValue);
                    
                }
        }
        else
        {
            if(redHome.Count == 4)
            {
                StartCoroutine(ShowText("Pas de déplacement possible"));
            }else
            {
                Feuille arbre = DrawTree(gameObjects, diceValue);
                MoveAfterMinMax(arbre,diceValue);
            }

        }

    }

    private void MoveAfterMinMax(Feuille arbre, int dice)
    {
       
        //On récupère la feuille avec plus le haut score
        Feuille feuille = arbre.GetMax();
        feuillesEnArene = feuille.arene;

        foreach (GameObject gameObject in arena)
        {
            if(gameObject.tag == "red")
            {
                GameObject pionToMove = feuille.arene.Find(x => x == gameObject);
                if (pionToMove == null)
                {
                    print("Un pion peu être touvé dans l'arène");
                    string[] pionToMoveSplit = gameObject.name.Split('#');
                    print("La pièce recherchée s'appelle : " + gameObject.name);
                    GameObject item = feuille.arene.Find(y => y.name.Contains(pionToMoveSplit[0]) == true);
                    print(item);
                    if (item == null)
                    {
                        print("Object non trouvé");
                    }
                    else
                    {
                        print("Deplacement possible");
                        print("Destination : " + item.name);
                        string[] splitNewPos = item.name.Split('#');
                        //Maintenant il suffit de déplacer le pion
                        int newPos = int.Parse(splitNewPos[1]) + dice;
                        //Nouveau nom
                        gameObject.name = splitNewPos[0] + "#" + newPos.ToString();
                        print("Nouvelle position à : " + newPos);
                        Vector3 positionToMove = new Vector3(redPath[newPos].transform.position.x, redPath[newPos].transform.position.y + 0.028f, redPath[newPos].transform.position.z);
                        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, positionToMove, 200 * Time.deltaTime);
                        break;
                    }
                }
            }

        }
    }

    private void MoveFromHome(string tag)
    {
        if(tag == "red")
        {
            GameObject selectedPlayer = redHome[0];

            Vector3 positionToMove = new Vector3(redPath[0].transform.position.x, redPath[0].transform.position.y + 0.028f, redPath[0].transform.position.z);
            selectedPlayer.transform.position = Vector3.MoveTowards(selectedPlayer.transform.position, positionToMove, 200 * Time.deltaTime);
            selectedPlayer.name = selectedPlayer.name + "#" + 0.ToString();
            arena.Add(selectedPlayer);
            redHome.Remove(selectedPlayer);
        }
    }

    /*
* ******************************************************************************************************************************
* *****************************************************************************************************************************
* *******************************************************************************************************************************
* ******************************************************************************************************************************
* ********************************************* Dessin de l'arbre ***************************************************************
*/
    private Feuille DrawTree(List<GameObject> state, int dice)
    {
        Feuille root = new Feuille();
        root.SetValues(state);
        root.SetRoot();


        /*=====================================   MAX   ===========================================================================*/
        //root créee
        //Pour la valeur du dé, nous déplaçons chaque joueur rouge
        
           
        foreach (GameObject pion in state){
            //Pour ne pas modifier l'état général, on crée une nouvelle liste
            List<GameObject> temp = new List<GameObject>();
            if (pion.tag == "red")
                {
                    GameObject pionTemp = Instantiate(pion);
                    pionTemp.name = pion.name;
                    //Si le pion est rouge alors on le déplace de la valeur du dé
                    string[] split = pionTemp.name.Split('#');
                    int newPos = int.Parse(split[1]) + dice;
                    pionTemp.name = split[0] + "#" + newPos.ToString();
                    //On ajoute le nouveau nom à la liste
                    temp.Add(pionTemp);
                print("Pion ajouté : " + pionTemp.name);
                }
            if(pion.tag == "yellow")
                {
                    GameObject pionTemp = Instantiate(pion);
                    pionTemp.name = pion.name;
                    temp.Add(pionTemp);
                }
            Feuille feuille = new Feuille();
            feuille.SetValues(temp);
            root.AddChild(feuille);

        }
            
        

        /*========================================       MIN   ========================================================================*/

        //Pour la valeur Max, nous devons prendre les feuilles de chaque branche du root, et ajouter la valeur du dé aux pion rouges.

        foreach(Feuille feuille1 in root.branche)
        {
            print("Pions dans Home :" + redHome.Count);
            for(int i = 0; i <7;i++)
            {
                //Pour ne pas modifier l'état général, on crée une nouvelle liste
                List<GameObject> temp = new List<GameObject>();
                foreach (GameObject pion in state)
                {
                    if (pion.tag == "yellow")
                    {
                        GameObject pionTemp = Instantiate(pion);
                        pionTemp.name = pion.name;
                        //Si le pion est jaune alors on le déplace de i cases et on ajoute l'état
                        string[] split = pionTemp.name.Split('#');
                        int newPos = int.Parse(split[1]) + i;
                        pionTemp.name = split[0] + "#" + newPos.ToString();
                        //On ajoute le nouveau nom à la liste
                        temp.Add(pionTemp);
                    }
                    if (pion.tag == "red")
                    {
                        GameObject pionTemp = Instantiate(pion);
                        pionTemp.name = pion.name;
                        temp.Add(pionTemp);
                    }
                }
                Feuille feuille = new Feuille();
                feuille.SetValues(temp, Evaluate(temp));
                feuille1.AddChild(feuille);
                AddScoreToParent(feuille, Evaluate(temp));
            }
        }

        //On retourne l'arbre dessiné
        return root;
    }

    private void AddScoreToParent(Feuille feuille, int v)
    {
        if (feuille.parent.score == 0)
        {
            feuille.parent.score = v;
            feuille.parent.SetValues(feuille.arene);
        }else
            if(v < feuille.parent.score)
            {
                feuille.parent.score = v;
                feuille.parent.SetValues(feuille.arene);
        }
        
    }

}
