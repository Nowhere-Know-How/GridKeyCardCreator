using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CreateCube : MonoBehaviour
{
    public GameObject redBox;
    public GameObject blueBox;
    public GameObject blackBox;

    public GameObject[] boxCheckers;

    public int save_counter = 0;
    public TextMeshProUGUI text;
    public TextMeshProUGUI loadText;
    public TextMeshProUGUI infoText;
    string title_prefix = "ID: ";
    string startingText = "";
    KeyCard activeCard;

    private void Start()
    {
        text.SetText(title_prefix + save_counter.ToString());
        startingText = infoText.text;
        Load();
    }
    KeyCard CreateKeyCardObject()
    {
        KeyCard keyCard = new KeyCard(save_counter);
        keyCard.SetFirstToMove(boxCheckers[0].GetComponent<ColliderUtil>().GetColor());
        for (int i = 1; i < boxCheckers.Length; i++)
        {
            CardColor color = boxCheckers[i].GetComponent<ColliderUtil>().GetColor();
            keyCard.SetMap(i - 1, color);
        }
        return keyCard;
    }

    public void Save()
    {
        string filename = @"C:\Users\Ling\Desktop\CodeNames\key_cards\data\" + save_counter.ToString() + ".txt";
        Debug.Log("Saved! " + filename);

        KeyCard keyCard = CreateKeyCardObject();

        using (System.IO.StreamWriter file = new System.IO.StreamWriter(filename))
        {
            file.WriteLine(keyCard.ToString());
        }
        save_counter += 1;
        text.SetText(title_prefix + save_counter.ToString());
        
        try
        {
            Load();
        }
        catch
        {
            print("Doesn't Exist Yet");
        }
    }

    public void FullClear()
    {
        GameObject[] blue = GameObject.FindGameObjectsWithTag("Blue");
        GameObject[] red = GameObject.FindGameObjectsWithTag("Red");
        GameObject[] black = GameObject.FindGameObjectsWithTag("Black");

        for (int i = 0; i < blue.Length; i++)
        {
            GameObject.Destroy(blue[i].gameObject);
        }
        for (int i = 0; i < red.Length; i++)
        {
            GameObject.Destroy(red[i].gameObject);
        }
        for (int i = 0; i < black.Length; i++)
        {
            GameObject.Destroy(black[i].gameObject);
        }
    }
    public void Load()
    {
        FullClear();
        string filename = @"C:\Users\Ling\Desktop\CodeNames\key_cards\data\" + save_counter.ToString() + ".txt";

        try
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Display the file contents by using a foreach loop.
            KeyCard card;
            string json = "";
            foreach (string line in lines)
            {
                json += line;
            }
            card = JsonUtility.FromJson<KeyCard>(json);
            activeCard = card;
            //Debug.Log("Keycard: " + card.ToString());

            //Debug.Log("First To Move: " + card.GetFirstToMove().ToString());
            if (card.GetFirstToMove() == CardColor.Blue)
            {
                GameObject obj = (GameObject)Instantiate<GameObject>(blueBox);
                obj.transform.position = boxCheckers[0].transform.position;
                obj.name = "Cube";

            }
            else if (card.GetFirstToMove() == CardColor.Red)
            {
                GameObject obj = (GameObject)Instantiate<GameObject>(redBox);
                obj.transform.position = boxCheckers[0].transform.position;
                obj.name = "Cube";

            }
            for (int i = 1; i < boxCheckers.Length; i++)
            {
                CardColor color = boxCheckers[i].GetComponent<ColliderUtil>().GetColor();
                if (card.GetMap(i - 1) == CardColor.Red)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(redBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
                else if (card.GetMap(i - 1) == CardColor.Blue)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(blueBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
                else if (card.GetMap(i - 1) == CardColor.Black)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(blackBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
            }
        }
        catch
        {
            print("ERROR LOADING FILE: " + filename);
        }
        UpdateInfoText();

    }

    public void LoadSpecificFile()
    {
        FullClear();
        save_counter = int.Parse(loadText.text.Substring(0, loadText.text.Length - 1));
        string filename = @"C:\Users\Ling\Desktop\CodeNames\key_cards\data\" + save_counter.ToString() + ".txt";

        try
        {
            string[] lines = System.IO.File.ReadAllLines(filename);

            // Display the file contents by using a foreach loop.
            KeyCard card;
            string json = "";
            foreach (string line in lines)
            {
                json += line;
            }
            card = JsonUtility.FromJson<KeyCard>(json);
            activeCard = card;
            Debug.Log("Keycard: " + card.ToString());

            //Debug.Log("First To Move: " + card.GetFirstToMove().ToString());
            if (card.GetFirstToMove() == CardColor.Blue)
            {
                GameObject obj = (GameObject)Instantiate<GameObject>(blueBox);
                obj.transform.position = boxCheckers[0].transform.position;
                obj.name = "Cube";

            }
            else if (card.GetFirstToMove() == CardColor.Red)
            {
                GameObject obj = (GameObject)Instantiate<GameObject>(redBox);
                obj.transform.position = boxCheckers[0].transform.position;
                obj.name = "Cube";

            }
            for (int i = 1; i < boxCheckers.Length; i++)
            {
                CardColor color = boxCheckers[i].GetComponent<ColliderUtil>().GetColor();
                if (card.GetMap(i - 1) == CardColor.Red)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(redBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
                else if (card.GetMap(i - 1) == CardColor.Blue)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(blueBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
                else if (card.GetMap(i - 1) == CardColor.Black)
                {
                    GameObject obj = (GameObject)Instantiate<GameObject>(blackBox);
                    obj.transform.position = boxCheckers[i].transform.position;
                    obj.name = "Cube";
                }
            }
        }
        catch
        {
            print("ERROR LOADING FILE: " + filename);
        }
        UpdateInfoText();

        text.SetText(title_prefix + save_counter.ToString());

    }


    void UpdateInfoText()
    {
        string text = startingText;
        string newText = text.Replace("{red}", activeCard.GetCount(CardColor.Red).ToString());
        newText = newText.Replace("{blue}", activeCard.GetCount(CardColor.Blue).ToString());
        newText = newText.Replace("{black}", activeCard.GetCount(CardColor.Black).ToString());
        newText = newText.Replace("{brown}", activeCard.GetCount(CardColor.Brown).ToString());
        newText = newText.Replace("{id}", activeCard.id.ToString());
        infoText.SetText(newText);
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Z) || Input.GetButton("Fire1"))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //Debug.Log(hit.collider.name);
                if (hit.collider.name == "BoxChecker")
                {
                    GameObject obj = (GameObject)Instantiate(redBox, hit.point, Quaternion.identity);
                    obj.transform.position = ExtensionMethods.Round(obj.transform.position);
                    obj.name = "Cube";
                }
                    
                    
            }
        }

        if (Input.GetKey(KeyCode.X) || Input.GetButton("Fire2"))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.name == "BoxChecker")
                {
                    GameObject obj = (GameObject)Instantiate(blueBox, hit.point, Quaternion.identity);
                    obj.transform.position = ExtensionMethods.Round(obj.transform.position);
                    obj.name = "Cube";
                }
            }
        }

        if (Input.GetKey(KeyCode.C))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.name == "BoxChecker")
                {
                    GameObject obj = (GameObject)Instantiate(blackBox, hit.point, Quaternion.identity);
                    obj.transform.position = ExtensionMethods.Round(obj.transform.position);
                    obj.name = "Cube";
                }
            }
        }

        if (Input.GetKey(KeyCode.D) || Input.GetButton("Fire3"))
        {
            Ray ray;
            RaycastHit hit;
            ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                if (hit.collider.name == "Cube")
                {
                    GameObject.Destroy(hit.collider.gameObject);
                }


            }
        }
    }
}
