using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class eventCaller : MonoBehaviour {

	[SerializeField]
	private float timeScaleSlow;

	public enum eventAction
	{
		set_active_true,
		set_active_false,
		sound_play,
		sound_stop,
		timeScaleChange,
		timeScaleResume
	}

	[System.Serializable]
	public class watanabeEvent
	{
		public bool end = false;
		public GameObject eventObject;
		public eventAction action;
	}
	public watanabeEvent[] events;

	private void OnCollisionEnter(Collision collision)
	{
		if(collision.transform.tag == "event")
		{
			for (int i = 0; i < events.Length; i++) 
			{
				if (events [i].end)
					return;

				switch (events [i].action)
				{
					case eventAction.set_active_true:
					{
						events [i].eventObject.SetActive (true);
						break;
					}
					case eventAction.set_active_false:
					{
						events [i].eventObject.SetActive (false);
						break;
					}
					case eventAction.sound_play:
					{
						AudioSource audio;
						audio = events [i].eventObject.GetComponent<AudioSource> ();
						audio.Play ();
						break;
					}
					case eventAction.sound_stop:
					{
						AudioSource audio;
						audio = events [i].eventObject.GetComponent<AudioSource> ();
						audio.Stop ();
						break;
					}
					case eventAction.timeScaleChange:
					{
						Time.timeScale = timeScaleSlow;
						break;
					}
					case eventAction.timeScaleResume:
					{
						Time.timeScale = 1.0f;
						break;
					}
				}

				events [i].end = true;
				Debug.Log("call -> effect");

			}
			Debug.Log("call -> event");
		}
	}
}
