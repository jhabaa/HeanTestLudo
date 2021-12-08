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
    }

    private void Path (List<GameObject> path)
    {
        int a = 5, b = 7;
        int x = 0 , y;

        switch (x)
        {
            case 0:
                for (y = 5; y < b + 1; y++)
                {
                    GameObject g = GameObject.Find(x.ToString() + "," + y.ToString() + "#" + string.Empty);
                    newPath.Add(g);
                }
                break;
            default:
                switch (y)
                {

                }
                break;

        }
            
       



        for (int x = 0; x < a+1; x = x +5)
        {
            
        }
    }

    private void SortPlayers()
    {
        yellowHome = GameObject.FindGameObjectsWithTag("yellow").ToList();
        GameObject yellowStart = GameObject.Find(5 + "," + 1 + "#" + string.Empty);
        yellowStart.tag = "startyellow";
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
            if (dice == 6)
            {
                yellowHome[0].transform.localPosition = GameObject.FindGameObjectWithTag("startyellow").transform.localPosition;
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
                    print(g.name);
                    break;
            }
        }
        
    }
}
