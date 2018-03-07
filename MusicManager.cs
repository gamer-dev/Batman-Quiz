using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour {

	AudioSource m_MyAudioSource;
	

	static bool AudioStart = false;
	
	
	void Awake()
	{
		m_MyAudioSource = GetComponent<AudioSource>();
		
		if(!AudioStart)
		{
			m_MyAudioSource.Play();
			DontDestroyOnLoad (gameObject);
			AudioStart = true;
			
		}
	}
	
}
