using UnityEngine;

//Este Script está hecho por chatGPT para no llenar el GameManager de datos de música de las escenas :D
//Este Script está hecho por chatGPT para no llenar el GameManager de datos de música de las escenas :D
//Este Script está hecho por chatGPT para no llenar el GameManager de datos de música de las escenas :D
//Este Script está hecho por chatGPT para no llenar el GameManager de datos de música de las escenas :D



//Podria haber puesto un audiosource en cada escena que reprodujera los audios, pero así es más limpio
//Podria haber puesto un audiosource en cada escena que reprodujera los audios, pero así es más limpio
//Podria haber puesto un audiosource en cada escena que reprodujera los audios, pero así es más limpio
//Podria haber puesto un audiosource en cada escena que reprodujera los audios, pero así es más limpio



[CreateAssetMenu(fileName = "SceneMusicData", menuName = "Audio/Scene Music Data")]
public class SceneMusicData : ScriptableObject
{
    [System.Serializable]
    public class SceneMusic
    {
        public string sceneName;
        public AudioClip musicClip;
    }

    public SceneMusic[] sceneMusics;
}