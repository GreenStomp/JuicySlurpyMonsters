using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpUsers : MonoBehaviour
{
    private Power_Up powerUp;
    private GameObject toInstance;
    public List<GameObject> Objs;
    private GameObject go;
    // Update is called once per frame
    void Update()
    {
        if (this.powerUp != null)
            this.powerUp.DoPowerUp(this.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        go = other.gameObject;

        PickUpPowerUp pickUpPowerUp = other.GetComponent<PickUpPowerUp>();

        if (other.GetComponent(typeof(PickUpPowerUp)))
        {
            this.powerUp = other.GetComponent<PickUpPowerUp>().PowerUp;
            this.powerUp.Init();
            go.SetActive(false);

            if (!Objs.Contains(this.powerUp.ToInstance) || toInstance == null)
            {
                toInstance = GameObject.Instantiate(this.powerUp.ParticleEffect, this.transform.position, Quaternion.identity);
                this.powerUp.ToInstance = toInstance;
                toInstance.transform.parent = this.transform;
                Objs.Add(this.powerUp.ToInstance);
            }
            else if (Objs.Contains(this.powerUp.ToInstance))
            {
                this.powerUp.ToInstance.SetActive(true);
            }
        }
    }

    private void OnGUI()
    {
        if (powerUp == null)
            return;

        Color c = Color.white;

        if (!powerUp.Active || powerUp.Passive) c.a = 0.5f;
        GUI.color = c;

        string s = "";
        //if (Uses > 1) s = Uses.ToString();

        GUIContent content = new GUIContent(powerUp.Name, powerUp.ImageIcon);

        Rect rect = new Rect(powerUp.IconPosition.x, powerUp.IconPosition.y, powerUp.Width, powerUp.Height);

        GUI.Box(rect, content);
        s = "";
        //if (Uses > 1) s = Uses.ToString();

        if (this.powerUp.Passive)
        {
            float t = powerUp.DieAt - Time.time;
            s = (Mathf.Round((powerUp.DieAt - Time.time) * powerUp.Offset) / powerUp.Offset).ToString();
            if (t > 3) s = (Mathf.Floor(powerUp.DieAt - Time.time)).ToString();
            if (t <= 0f)
            {
                this.powerUp = null;
                if (toInstance != null)
                {
                    foreach (GameObject items in Objs)
                    {
                        if(items!=null)
                        {
                            items.SetActive(false);
                        }
                    }
                }
            }

        }

        if (s != "")
        {
            GUI.color = Color.white;
            GUI.Label(rect, s);
        }
    }
}
