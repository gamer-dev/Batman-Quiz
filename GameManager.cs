using System.Collections;
using System.Collections.Generic; //to use Lists
using UnityEngine;
using System.Linq; //to convert arrays to list
using UnityEngine.UI; //using UI
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	//A list of questions - array :
	//We use Arrays not list, we don't need to resize dynamically
	//We'll have only fixed questions
	
	//This is an array of type Question, that is specified by the question class
	//We'll store our questions in this array.
	public Question[] questions; 
	
	//List to contain all unasked questions: (Initially, all questions are present here. And as we answer more and more, we'll keep removing them from the list. )
	//The List is static as this list should persist between scenes
	private static List<Question> unansweredQuestions;
	
	//Question indicating the current question being asked
	private Question currentQuestion;
	
	[SerializeField]
	private Text factText; //Text to display as the question/fact
	
	[SerializeField]
	private float timeBetweenQuestions = 1f; //Delay between questions
	
	[SerializeField]
	private Text trueAnswerText; //refer to the text to display when TRUE button is pressed
	
	[SerializeField]
	private Text falseAnswerText;//refer to the text to display when FALSE button is pressed
	
	public GameObject gameOverScreen;//refer to the message to show after running out of questions
	
	public GameObject mainGameScreen;
	
	public Text gameOverText;
	
	[SerializeField]
	private Animator animator;
	
	[SerializeField]
	private static int scorePoints;
	
	[SerializeField]
	private Text scoreText; //Refer to the score text
	
	public GameObject correctSound;
	public GameObject wrongSound;
	
	
	IEnumerator TransitionToNextQuestion()
	{
		
			//remove the asked question from the list:
			unansweredQuestions.Remove(currentQuestion);
			
			yield return new WaitForSeconds(timeBetweenQuestions);
		
			//Reload the Scene to load the next question i.e Restart Scene, only if there are still more unanswered questions
			
			if(unansweredQuestions.Count != 0)
			{
			SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
			}
	}
	
	
	
	IEnumerator JustWait()
	{
			

			yield return new WaitForSeconds(1f);
			//Load the GameOver Screen
			mainGameScreen.SetActive(false);
			gameOverText.text = "Game Over! Your Score: " + scorePoints;
			gameOverScreen.SetActive(true);
			
			
			
		
	}
	
	
	void Start()
	{
		//Let's load all of the available questions from array to list
		//But first, make sure that it will happen only once, not in every scene
		
		//A list is null when 1st initialized
		if(unansweredQuestions == null)
		{
			unansweredQuestions = questions.ToList<Question>(); //Converting array to List
		}
		
		SetCurrentQuestion();
		
		
		if(unansweredQuestions.Count != 0)
		{
			mainGameScreen.SetActive(true);
			gameOverScreen.SetActive(false);
		}
				
		//Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
	
		UpdateScore();
	
	}	
	
		public void AddScore(int newScore)
		{
			scorePoints += newScore;
			UpdateScore();			
			
		}
		
		void UpdateScore()
		{
			scoreText.text = "Score: "+ scorePoints;
		}
		
		
		//Let's pick a random question:
		
		void SetCurrentQuestion()
		{
			//Get a random number that's going to be the index of the list
			
			int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count);
			
			//set the current question now:
			
			currentQuestion = unansweredQuestions[randomQuestionIndex];
			
			factText.text = currentQuestion.fact;
			
			if(currentQuestion.isTrue)
			{
				trueAnswerText.text = "CORRECT!";
				
				falseAnswerText.text = "WRONG :(";
			}
			else
			{
				trueAnswerText.text = "WRONG :(";
				falseAnswerText.text = "CORRECT!";
			}
			
			
		}
		
		public void UserSelectTrue()
		{
			
			animator.SetTrigger("True");
			
			if(currentQuestion.isTrue)
			{
				AddScore(10);
				correctSound.GetComponent<AudioSource>().Play();
				Debug.Log("CORRECT");
			}
			else
			{
				AddScore(-5);
				wrongSound.GetComponent<AudioSource>().Play();
				Debug.Log("WRONG!");
			}
			
			StartCoroutine(TransitionToNextQuestion());
		}
		
		public void UserSelectFalse()
		{
			animator.SetTrigger("False");
			if(!currentQuestion.isTrue)
			{
				AddScore(10);
				correctSound.GetComponent<AudioSource>().Play();
				Debug.Log("CORRECT");
			}
			else
			{
				AddScore(-5);
				wrongSound.GetComponent<AudioSource>().Play();
				Debug.Log("WRONG!");
			}
			
			StartCoroutine(TransitionToNextQuestion());
			
		}
		
		
		void Update()
		{
			
			if(unansweredQuestions.Count == 0)
			{
				StartCoroutine(JustWait());
				
			
			}
			
			
		}
	
	
}
