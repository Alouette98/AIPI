using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetaballParticleClass : MonoBehaviour {


	public GameObject MObject;
	public float LifeTime;
	public bool Active{
		get{ return _active;}
		set{ _active = value;
			if (MObject) {
				MObject.SetActive (value);

				if (tr)
					tr.Clear ();

			}
		}
	}
	public bool witinTarget; // si esta dentro de la zona de vaso de vidrio en la meta

	public Slider slider1;
	
 

	bool _active;
	float delta;
	Rigidbody2D rb;
	TrailRenderer tr;
	SpriteRenderer sr;

	void Start () {
		//MObject = gameObject;
		rb = GetComponent<Rigidbody2D> ();
		tr = GetComponent<TrailRenderer> ();
		sr = GetComponent<SpriteRenderer>();
	}

	void Update () {
		
		if (Active == true) {

			VelocityLimiter ();

			if (LifeTime < 0)
				return;

			if (delta > LifeTime) {
				delta *= 0;
				Active = false;
			} else {
				delta += Time.deltaTime;
			}


		}

		if (transform.position.x > -8.12 && slider1.value > 0) {
			changeColour(new Color(0f, slider1.value/3.0f, 0));
        }

		if (transform.position.x > -8.12 && slider1.value < 0)
		{
			changeColour(new Color( (-slider1.value)/3.0f, 0f, 0));
		}
		 
		if (transform.position.x > -4.4 && slider1.GetComponent<WeightBar>().weightID == 2)
        {
			GetComponent<Rigidbody2D>().gravityScale = 1;
        }

		////  WARNING: The Water2D repo is very strange... The particles will be recycled so you should reset the particle status(like color, gravity or so) according to its position.
		////  Simon
		

		/// Reset the status
		
		if (transform.position.x < -8.12){
			changeColour(new Color(67f / 255f, 143f / 255f, 241f / 255f));
        }

        if (transform.position.x < -4.4 && slider1.GetComponent<WeightBar>().weightID == 2)
        {
            GetComponent<Rigidbody2D>().gravityScale = -1;
        }
    }


	void changeColour(Color newColor)
    {
		Gradient gradient = new Gradient();
		GradientColorKey[] colorKey = new GradientColorKey[2];
		GradientAlphaKey[] alphaKey = new GradientAlphaKey[2];
		colorKey[0].color = newColor;
		colorKey[1].color = Color.white;
		alphaKey[0].alpha = 1.0f;
		alphaKey[1].alpha = 1.0f;
		gradient.SetKeys(colorKey, alphaKey);
		sr.color = newColor;
		tr.colorGradient = gradient;
	}

	void VelocityLimiter()
	{
		
		
		Vector2 _vel = rb.velocity;
		if (_vel.y < -8f) {
			_vel.y = -8f;
		}
		rb.velocity = _vel;
	}

}
