using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopControl : MonoBehaviour
{
	// Start is called before the first frame update

	[SerializeField] private Transform Shop;
	GameObject newShop;
	void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OpenShop()
	{
		StartCoroutine(enumerator());
	}
	public void CloseShop(GameObject newShop)
	{
		this.newShop = newShop;
		StartCoroutine(newEnumerator());
		
	}
	public IEnumerator enumerator()
    {

		for (float alpha = 0f; alpha <1 ; alpha += 0.01f)
		{
			Shop.GetComponent<CanvasGroup>().alpha = alpha;
			yield return new WaitForSeconds(0.01f);
		}
    }
	public IEnumerator newEnumerator()
	{

		for (float alpha = 1f; alpha > 0; alpha -= 0.01f)
		{
			Shop.GetComponent<CanvasGroup>().alpha = alpha;
			yield return new WaitForSeconds(0.01f);
		}
		newShop.SetActive(false);
	}
}
