using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< HEAD
        [SerializeField] Slider musicSlider;
        [SerializeField] GameObject settingsPanel;

        public float musicVolumeSet;
        public Action a_musicSettings;

        GraphicRaycaster m_Raycaster;
        PointerEventData m_PointerEventData;
        EventSystem m_EventSystem;

        private void Start()
        {
            a_musicSettings += MusicVolumeSettings;
        }
        private void Update()
        {
            Settings();
        }
        private void MusicVolumeSettings()
        {
            musicVolumeSet = FindObjectOfType<SliderSettings>().GetComponent<Slider>().value;            
        }

        private void Settings()
        {        
            //Check if the left Mouse button is clicked
            if (Input.GetKey(KeyCode.Mouse0))
            {
                //Set up the new Pointer Event
                m_PointerEventData = new PointerEventData(m_EventSystem);
                //Set the Pointer Event Position to that of the mouse position
                m_PointerEventData.position = Input.mousePosition;

                //Create a list of Raycast Results
                List<RaycastResult> results = new List<RaycastResult>();

                //Raycast using the Graphics Raycaster and mouse click position
                m_Raycaster.Raycast(m_PointerEventData, results);

                //For every result returned, output the name of the GameObject on the Canvas hit by the Ray
                foreach (RaycastResult result in results)
                {
                    Debug.Log("Hit " + result.gameObject.name);
                }
            }
            GraphicRaycaster ray;

            
        }

=======
        
>>>>>>> parent of 31fc16d... test
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
