using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;
using UnityEngine.Networking;

public class GameManager2 : MonoBehaviour
{

    #region Singleton

    private static GameManager2 instance;

    public static GameManager2 Instance
    {
        get
        {
            return instance;
        }
    }

    #endregion


    [Serializable]
    public class Questions
    {
        public string name;
        public string question;
        public string answerTrue;
        public string answerFalse01;
        public string answerFalse02;
        public string answerFalse03;
    }


    [Header("Gameplay")]
    public int actualQuestion = 0;
    public int totalQuestions = 0;
    public int answerTrueId = 0; //Representa las respuestas correctas.
    public int answerCorrect = 0; //Representa la cantidad de respuestas correctas.
    public int answerIncorrect = 0; //Representa la cantidad de respuestas incorrectas.


    [Header("Questions")]
    [Range(0f, 5f)]
    public float timeToSelect = 3f;

    public TextMeshProUGUI txtTitle;

    [Space]
    public List<TextMeshProUGUI> txtAnswers;

    [Space]
    public Image[] imgAnswers;

    [Space]
    public Color colorEnter;
    public Color colorExit;
    public Color colorTrue;
    public Color colorFalse;


    [Header("JSON")]
    public string FileName = "Questions";
    public Questions[] questions;
    public string fileFormat = ".json";


    List<string> answers = new List<string>(); //Almacena temporalmente las respuestas falsas.
    List<TextMeshProUGUI> texts = new List<TextMeshProUGUI>();


    [Header("Next Question")]
    public bool isChanging = false;

    [Range(0f, 5f)]
    public float timeToChange = 3; //Lapso de tiempo de pausa entre animaciones.

    [Space]
    public PlayableAsset playableShowOn;
    public PlayableAsset playableShowOff;
    public PlayableAsset playableNext;

    PlayableDirector playableDirector;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        playableDirector = GetComponent<PlayableDirector>();

        answers = new List<string>();
        texts = new List<TextMeshProUGUI>();
    }


    public void FinishGame()
    {
        for (int i = 0; i < imgAnswers.Length; i++)
        {
            BoxCollider collider = imgAnswers[i].GetComponent<BoxCollider>();

            if (collider != null)
            {
                collider.enabled = false;
            }
        }

        txtTitle.text = "Results";

        imgAnswers[0].color = colorTrue;
        txtAnswers[0].text = answerCorrect.ToString();

        txtAnswers[1].text = "-";

        imgAnswers[2].color = colorFalse;
        txtAnswers[2].text = answerIncorrect.ToString();
    }


    private void Start()
    {
        StartCoroutine(LoadJsonData());
    }


    #region JSON ------------------------------------

    private IEnumerator LoadJsonData()
    {
        string filePath = Path.Combine(
            Application.streamingAssetsPath,
            FileName + fileFormat
        );

#if UNITY_WEBGL && !UNITY_EDITOR

        UnityWebRequest request = UnityWebRequest.Get(filePath);

        yield return request.SendWebRequest();

        if (request.isNetworkError || request.isHttpError)
        {
            Debug.LogError("JSON load error: " + request.error);
            yield break;
        }

        string jsonData = request.downloadHandler.text;

#else

        string jsonData = "";

        if (File.Exists(filePath))
        {
            jsonData = File.ReadAllText(filePath);
        }
        else
        {
            Debug.LogError("JSON file not found at: " + filePath);
            yield break;
        }

#endif

        // Tomamos nuestro array Questions desde el JSON
        questions = JsonHelper.FromJsonArray<Questions>(jsonData);

        totalQuestions = questions.Length - 1;

        actualQuestion = 0;

        ChangeQuestions();
        ShowQuestion(true);
    }

    #endregion ----------------------------------



    public void CheckQuestion(int id)
    {

        isChanging = true;

        //Setteamos los colores de verdadero y falso.
        for (int i = 0; i < imgAnswers.Length; i++)
        {
            imgAnswers[i].color =
                i == answerTrueId ? colorTrue : colorFalse;
        }

        //Agregamos puntos.
        if (answerTrueId == id)
        {
            answerCorrect++;
        }
        else
        {
            answerIncorrect++;
        }

        Invoke("NextQuestion", timeToChange);
    }


    public void NextQuestion()
    {
        playableDirector.playableAsset = playableNext;
        playableDirector.Play();
    }


    public void ShowQuestion(bool isShowing)
    {
        playableDirector.playableAsset =
            isShowing ? playableShowOn : playableShowOff;

        playableDirector.Play();
    }


    #region Timeline-----------------------------


    public void SetChange(bool _isChanging)
    {
        isChanging = _isChanging;
    }


    public void ChangeQuestions()
    {

        //Reseteo de color
        for (int i = 0; i < imgAnswers.Length; i++)
        {
            imgAnswers[i].color = colorExit;
        }

        //Termina el juego si se terminan las preguntas.
        if (actualQuestion == questions.Length)
        {
            FinishGame();
            return;
        }

        // Setteamos la pregunta principal.
        txtTitle.text = questions[actualQuestion].question;

        // Limpiamos las listas temporales antes de rellenarlas.
        answers.Clear();
        texts.Clear();

        //Se agrega contenido a las listas temporales
        texts.AddRange(txtAnswers);

        answers.Add(questions[actualQuestion].answerFalse01);
        answers.Add(questions[actualQuestion].answerFalse02);
        answers.Add(questions[actualQuestion].answerFalse03);

        //Random para pregunta correcta.
        int r = UnityEngine.Random.Range(0, texts.Count);

        answerTrueId = r;

        texts[r].text = questions[actualQuestion].answerTrue;
        texts.RemoveAt(r);

        //Random para preguntas falsas.
        for (int i = 0; i < texts.Count; i++)
        {
            r = UnityEngine.Random.Range(0, answers.Count);

            texts[i].text = answers[r];
            answers.RemoveAt(r);
        }

        actualQuestion++;

        answers.Clear();
        texts.Clear();
    }


    #endregion ----------------------------------


}