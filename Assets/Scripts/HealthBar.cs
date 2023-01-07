using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
	public Slider slider;
	public Gradient gradient;
	public Image fill;
	
	void Update() 
	{
		transform.SetAsLastSibling();
	}
	public void SetMaxHealth(int health)
	{
		slider.maxValue = health;
		slider.value = health;
		fill.color = gradient.Evaluate(1f);
	}

	public void SetHealth(int health)
	{
		slider.value = health;
		/*foreach (Transform child in this.transform)
		{
			if (child.name == "Fill")
				child.transform.color	 = gradient.Evaluate(slider.normalizedValue);
		}*/
		
		fill.color = gradient.Evaluate(slider.normalizedValue);
	}

}
