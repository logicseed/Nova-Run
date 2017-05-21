using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class ScoreController : MonoBehaviour {

    // Pseudo-singleton
    public static ScoreController Instance;

    private double Score;
    private List<HighScore> HighScores = new List<HighScore>();

    private Scene activeScene;
    private Text playHighScoreText;
    private Text playScoreText;
    private Text menuHighScoresText;
    private InputField seedInputField;
    //private GameObject creditsInputField;

    private float lifetime;
    private float age;
    private bool gameOver = false;
    private bool hasHighScore = false;
    private bool firstLoad = true;
    public bool hasLoaded = false;
    public bool seedChanged = false;

    void Awake () {
        
        // Pseudo-singleton
        if (Instance == null) {
            DontDestroyOnLoad (gameObject);
            Instance = this;
        } else if (Instance != this) {
            Destroy (gameObject);
        }

        //Instantiate(Resources.Load("RandomSeeder"));

        activeScene = SceneManager.GetActiveScene ();
        SceneManager.activeSceneChanged += UpdateActiveScene;

        
    }

    void Start()
    {
        LoadScores ();
    }

    void OnDestroy () {
        SceneManager.activeSceneChanged -= UpdateActiveScene;
        SaveScores ();
    }

    void UpdateActiveScene (Scene previousScene, Scene newScene) {
        
        activeScene = SceneManager.GetActiveScene ();

        if (activeScene.name == "Menu") {
            LoadScores();
            menuHighScoresText = GameObject.Find ("HighScoresText").GetComponent<Text> ();
            seedInputField = GameObject.Find ("SeedInputField").GetComponent<InputField>();

            RandomSeeder.Instance.ResetSeeds();
            //LoadScores();
            
        } else if (activeScene.name == "Play") {
            playScoreText = GameObject.Find ("PlayerScore").GetComponent<Text> ();
            playHighScoreText = GameObject.Find ("HighScore").GetComponent<Text> ();
            ResetScore ();
        } else if (activeScene.name == "Credits") {
            
        }
    }

    void ResetScore () {
        Score = 0.0;
        hasHighScore = false;
    }

    void OnGUI () {
        //Debug.Log("ON GUI!!!!!");
        if (activeScene.name == "Menu") {
            if (seedChanged)
            {
                seedInputField.text = RandomSeeder.Instance.seed;
            }
            if (hasLoaded)
            {
                UpdateMenuHighScores();
                hasLoaded = false;
            }
        } else if (activeScene.name == "Play") {
            UpdatePlayHighScores ();
        }
    }

    void UpdateMenuHighScores () {
        string highScoreString = "";
        foreach (var highScore in HighScores) {
            highScoreString += highScore.Name + " - " + highScore.ScoreAsInt + "\n";
        }
        menuHighScoresText.text = highScoreString;

       seedInputField.text = RandomSeeder.Instance.seed;
    }

    void UpdatePlayHighScores () {
        playScoreText.text = "SCORE: " + Convert.ToInt32 (Score);
        playHighScoreText.text = "HIGHEST SCORE: " + HighScores [0].ScoreAsInt;
    }

    public void SaveScores () {
        BinaryFormatter binFormatter = new BinaryFormatter ();
        FileStream file = File.Create (SeedFileName);

        HighScoreSavable data = new HighScoreSavable (HighScores);

        binFormatter.Serialize (file, data);
        file.Close ();
    }

    public void LoadScores () {

        //if (firstLoad) FindLastSeed();
        Debug.Log("Loading Scores");
        Directory.CreateDirectory(Application.persistentDataPath + "/High Scores/");
        if (File.Exists (SeedFileName)) {
            BinaryFormatter binFormatter = new BinaryFormatter ();
            FileStream file = File.Open (SeedFileName, FileMode.Open);
            HighScoreSavable data = (HighScoreSavable)binFormatter.Deserialize (file);
            file.Close ();
            HighScores = data.highScores;
        } else {
            HighScores = new List<HighScore> ();
            HighScores.Add (new HighScore ("Alexis", 20000.0));
            HighScores.Add (new HighScore ("Khalid", 10000.0));
            HighScores.Add (new HighScore ("Ryan", 5000.0));
            HighScores.Add (new HighScore ("Samer", 2500.0));
            HighScores.Add (new HighScore ("Joe", 1000.0));

            SaveScores ();
        }
        hasLoaded = true;
    }

    private void FindLastSeed ()
    {

    }

    public void AddScore(double score) {
        Score += score;
        if (Score < 0) Score = 0;
    }

    public bool HasHighScore() {
        if (hasHighScore)
            return hasHighScore;

        foreach (HighScore highScore in HighScores) {
            if (Score > highScore.Score)
                hasHighScore = true;
        }
        return hasHighScore;
    }

    public void AddHighScore (string name) {
        if (name == "")
            return;
        
        int count = 0;
        var newHighScore = new HighScore (name.Substring (0, Mathf.Min(name.Length, 20)), Score);
        var newHighScores = new List<HighScore> ();
        var added = false;

        foreach (HighScore highScore in HighScores) {
            if (count < 5) {
                if (!added && newHighScore.Score > highScore.Score) {
                    newHighScores.Add (newHighScore);
                    count++;
                    added = true;
                    if (count < 5) {
                        newHighScores.Add (highScore);
                        count++;
                    }
                } else {
                    newHighScores.Add (highScore);
                    count++;
                }
            }
        }

        HighScores = newHighScores;
        SaveScores();
    }

    public void GameOver (float lifetime) {
        this.lifetime = lifetime;
        this.gameOver = true;
    }

    void Update () {
        if (gameOver) {
            age += Time.deltaTime;

            if (age > lifetime) {
                age = 0.0f;
                gameOver = false;
                SceneManager.LoadScene ("Credits");
            }
        }

        if (Input.GetKey (KeyCode.RightControl) || Input.GetKey (KeyCode.RightCommand)) {
            if (Input.GetKey (KeyCode.RightAlt) && Input.GetKey (KeyCode.RightShift)) {
                if (Input.GetKeyDown (KeyCode.R)) {
                    ResetHighScores ();
                }
            }
        }
    }

    void ResetHighScores () {
        if (File.Exists (SeedFileName)) {
            File.Delete (SeedFileName);
        }
        LoadScores ();
    }

    public string SeedFileName
    {
        get
        {
            var fileName = Application.persistentDataPath;
            fileName += "/High Scores/";
            fileName += RandomSeeder.Instance.seed;
            //fileName += "bullshit";
            fileName += ".seed-save";
            return fileName;
        }
    }
}

[Serializable]
class HighScoreSavable
{
    public List<HighScore> highScores;

    public HighScoreSavable (List<HighScore> highScores){
        this.highScores = highScores;
    }
}

[Serializable]
class HighScore
{
    public double Score;
    public string Name;
    public int ScoreAsInt {
        get {
            return Convert.ToInt32 (Score);
        }
    }
    public HighScore(string name, double score) {
        this.Name = name;
        this.Score = score;
    }
}