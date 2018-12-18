﻿using DigitalRuby.SoundManagerNamespace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivityCompare : MonoBehaviour {

    //Doméstico, Perjudicial o Salvaje



    public GameObject animalOptionPrefab;
    public GameObject optionContainer;

    public Sprite[] nonLivingSprites;
    public Sprite[] livingSprites;
    public Color[] myColors; //0 = white, 1 = green, 2 = red

    private string activityName;
    private readonly int numberOfSubLevels = 10;
    private int score = 0;
    private int errors = 0;
    private int activity3Flag = 0;
    private bool subLevelFinished = false;
    private ArrayList spritesAlreadyInUse;

    private byte order;             //0 = Terrestre/Marino
    private byte answer;            //
    private string orderSoundName;  //el nombre del audio que dará la orden en esta actividad

    private readonly string[] LoliVoicesCorrect = { "LoliExcelente", "LoliRespondisteMuybien", "LoliBienSigueAsi", "LoliEsoEstuvoGenial" };
    private readonly string[] LoliVoicesWrong = { "LoliDeNuevo", "LoliIntentaloOtraVez", "LoliNoTeDesanimes", "LoliPuedesHacerloMejor" };

    public LevelMetaData levelData;


    // Use this for initialization
    void Start()
    {
        //AudioManager.Instance.PlayMusic("BGM");
        activityName = GameStateManager.Instance.getCurrentSceneName();

        levelData = new LevelMetaData(SessionManager.Instance.nombre_jugador, "Animales-" + activityName);

        spritesAlreadyInUse = new ArrayList();

        ResetOrder();

        print(AnimalManager.Instance.FilterByUtility(0).Count);

        Spawn(0);
        Spawn(1);

    }

    public void ResetOrder()
    {

        //byte i = System.Convert.ToByte(Random.Range(0, 4));
        //order = 0;
        //byte a = 0;         //Número de opciones en cada clasificación

        //switch (order)
        //{
        //    case 0:
        //        //Type
        //        a = 2;


        //        break;
        //    case 1:
        //        //Utility
        //        a = 3;
        //        break;
        //    case 2:
        //        //SkinCover
        //        a = 3;
        //        break;
        //    case 3:
        //        //Movement
        //        a = 6;
        //        break;

        //    default:
        //        break;
        //}

        //answer = System.Convert.ToByte(Random.Range(0, a));

        //AudioManager.Instance.PlayVoice(orderSoundName);
    }

    public void DisableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = false;
        }
    }

    public void EnableAllButtons()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (Button button in buttons)
        {
            //print(button);
            button.interactable = true;
        }
    }

    public void Spawn(int u)
    {

        Animal animal = AnimalManager.Instance.getRandomAnimalByUtility(u);
        byte modo;
        modo = System.Convert.ToByte(Random.Range(0, 3));


        GameObject newAnimalOption = Instantiate(animalOptionPrefab);
        newAnimalOption.GetComponent<AnimalOptionDisplay>().animal = animal;


        newAnimalOption.name = animal.name;
        //newAnimalOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


        newAnimalOption.transform.SetParent(optionContainer.transform);

        //newAnimalOption.transform.localPosition = new Vector3(0f, 2f, 0f);
        newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);

    }


    public void ResPawn()
    {
        spritesAlreadyInUse.Clear();

        foreach (Transform child in optionContainer.transform)
        {
            Destroy(child.gameObject);
        }

        if (activityName == "Activity2")
        {
            ColorBlock oldCB;
            GameObject ans = GameObject.Find("Answer");
            oldCB = ans.GetComponent<Button>().colors;
            oldCB.disabledColor = myColors[0];
            ans.GetComponent<Button>().colors = oldCB;

            ans.transform.GetChild(0).GetComponent<Image>().sprite = null;
        }

        activity3Flag = 0;
        subLevelFinished = false;
        Spawn(0);

        EnableAllButtons();
    }


    public byte GetOrder()
    {
        return order;
    }



    public void SpawnAnimal()
    {
        GameObject newAnimalOption = Instantiate(animalOptionPrefab);
        //newAnimalOption.GetComponent<AnimalOptionDisplay>().animal.tipo = isAlive;
        ColorBlock oldCB = newAnimalOption.GetComponent<Button>().colors;

        if (activityName != "Activity4")
        {
            //if (isAlive == order)
            //{
            //    oldCB.pressedColor = myColors[1];
            //}
            //else
            //{
            //    oldCB.pressedColor = myColors[2];
            //}

        }
        oldCB.disabledColor = myColors[0];


        newAnimalOption.GetComponent<Button>().colors = oldCB;

        Sprite beingSprite;

        Sprite spriteAux;

        //if (isAlive)
        //{

        //    spriteAux = livingSprites[Random.Range(0, livingSprites.Length)];

        //    while (VerifySprite(spriteAux))
        //    {
        //        spriteAux = livingSprites[Random.Range(0, livingSprites.Length)];
        //    }
        //    spritesAlreadyInUse.Add(spriteAux.name);
        //    beingSprite = spriteAux;

        //}
        //else
        //{
        //    spriteAux = nonLivingSprites[Random.Range(0, nonLivingSprites.Length)];

        //    while (VerifySprite(spriteAux))
        //    {
        //        spriteAux = nonLivingSprites[Random.Range(0, nonLivingSprites.Length)];
        //    }

        //    spritesAlreadyInUse.Add(spriteAux.name);
        //    beingSprite = spriteAux;

        //}

        //newAnimalOption.name = beingSprite.name;
        //newAnimalOption.transform.GetChild(0).GetComponent<Image>().sprite = beingSprite;


        newAnimalOption.transform.SetParent(optionContainer.transform);

        if (activityName == "Activity2")
        {
            newAnimalOption.transform.GetChild(0).transform.localPosition = new Vector3(0f, 2f, 0f);
            newAnimalOption.transform.GetChild(0).transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        }


        if (activityName != "Activity4")
        {
            newAnimalOption.GetComponent<Button>().onClick.AddListener(delegate { EvaluateOnClick(newAnimalOption.GetComponent<Button>()); });
        }

        newAnimalOption.transform.localScale = new Vector3(1f, 1f, 1f);

    }

    public bool VerifySprite(Sprite rndSprite)
    {

        foreach (string item in spritesAlreadyInUse)
        {
            if (item == rndSprite.name)
            {
                return true;
            }
        }

        return false;
    }

    public string RndVoiceGenerator(string[] voices)
    {
        return voices[Random.Range(0, voices.Length)];
    }

    public void EvaluateOnClick(Button buttonClicked)
    {
        //DisableAllButtons();
        ColorBlock oldCB;

        if (buttonClicked.GetComponent<AnimalOptionDisplay>().animal.utilidad.Equals(this.GetOrder()))
        {
            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[1];
            buttonClicked.colors = oldCB;

            int[] scores = SessionManager.Instance.getPlayerScore();
            //bool[] levels = SessionManager.Instance.getLevels();

            switch (activityName)
            {
                case "Activity1":


                    scores[0] = scores[0] + 1;
                    score++;
                    Debug.Log(score);
                    subLevelFinished = true;

                    break;
                case "Activity2":
                    oldCB = GameObject.Find("Answer").GetComponent<Button>().colors;
                    oldCB.disabledColor = myColors[1];

                    GameObject.Find("Answer").GetComponent<Button>().colors = oldCB;

                    GameObject.Find("Answer").transform.GetChild(0).GetComponent<Image>().sprite = buttonClicked.transform.GetChild(0).GetComponent<Image>().sprite;

                    scores[1] = scores[1] + 1;
                    score++;
                    subLevelFinished = true;

                    break;
                case "Activity3":
                    oldCB = buttonClicked.colors;

                    if (buttonClicked.GetComponent<ActivityOption>().selected)
                    {
                        oldCB.normalColor = myColors[0];
                        oldCB.highlightedColor = myColors[0];
                        oldCB.disabledColor = myColors[0];
                        buttonClicked.colors = oldCB;
                        buttonClicked.GetComponent<ActivityOption>().selected = false;
                        activity3Flag = activity3Flag - 1;
                    }
                    else
                    {
                        oldCB.normalColor = myColors[1];
                        oldCB.highlightedColor = myColors[1];
                        oldCB.disabledColor = myColors[1];
                        buttonClicked.colors = oldCB;
                        buttonClicked.GetComponent<ActivityOption>().selected = true;
                        activity3Flag++;
                    }


                    Debug.Log(activity3Flag);

                    if (activity3Flag >= 2)
                    {
                        scores[2] = scores[2] + 1;
                        score++;
                        subLevelFinished = true;
                    }

                    break;

                default:
                    break;
            }


            if (subLevelFinished)
            {
                SessionManager.Instance.setPlayerScore(scores);
                //SessionManager.Instance.setLevels(levels);
                //AudioManager.Instance.PlaySound("Win");
                DisableAllButtons();
                StartCoroutine(Win());
            }

        }
        else
        {
            oldCB = buttonClicked.colors;
            oldCB.disabledColor = myColors[2];
            buttonClicked.colors = oldCB;

            DisableAllButtons();
            StartCoroutine(Wrong());


        }


    }


    
    public void ReplayOrder()
    {
        AudioManager.Instance.PlayVoice(orderSoundName);
    }


    IEnumerator Win()
    {
        //DisableAllButtons();

        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesCorrect));

        yield return new WaitForSeconds(3);

        if (score >= numberOfSubLevels)
        {
            EndLevel("completado");
        }
        else
        {
            ResetOrder();
            ResPawn();
        }

    }

    IEnumerator Wrong()
    {
        AudioManager.Instance.PlayVoice(RndVoiceGenerator(LoliVoicesWrong));
        errors++;
        yield return new WaitForSeconds(3);
        EnableAllButtons();

        ColorBlock oldCB;

        foreach (Button button in optionContainer.GetComponentsInChildren<Button>())
        {
            button.GetComponent<ActivityOption>().selected = false;

            oldCB = button.colors;
            oldCB.normalColor = myColors[0];
            oldCB.highlightedColor = myColors[0];
            oldCB.disabledColor = myColors[0];
            button.colors = oldCB;
        }

        if (activityName == "Activity3")
        {
            activity3Flag = 0;
        }

    }

    public void EndLevel(string status)
    {
        if (status == "abandonado")
        {
            AudioManager.Instance.PlaySFX("TinyButtonPush");
        }

        levelData.estado = status;
        levelData.fecha_fin = System.DateTime.Now.ToString("yyyy/MM/dd");
        levelData.tiempo_juego = System.Math.Round(Time.timeSinceLevelLoad).ToString();
        levelData.correctas = score.ToString();
        levelData.incorrectas = errors.ToString();
        GameStateManager.Instance.AddJsonToList(JsonUtility.ToJson(levelData));

        GameStateManager.Instance.LoadScene("ActivityHub");
    }
}