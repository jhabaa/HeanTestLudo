using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = System.Random;

public class LudoGame : MonoBehaviour
{
    private int[,] grid;
    private List<GameObject> ToDelete;
    public GameObject tile;
    public GameObject plateau;
    public GameObject pion3D;
    public Material test;
    public List<GameObject> yellowHome, redHome, arena, finishYellow, finishRed, gamelist, newPath;
    private bool jeton = true;
    public int deepness = 2;
    private string yellow;

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
        StartCoroutine(TestPath(newPath));
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
    public void playGame()
    {
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
            redPion.transform.localPosition = g.transform.localPosition;*/
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
        }
        
    }
}
