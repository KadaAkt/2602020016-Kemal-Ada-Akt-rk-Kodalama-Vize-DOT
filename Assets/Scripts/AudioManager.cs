using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance; // diğer scriptlerden rahatça erişebilmek için instance tanımlıyoruz

    [SerializeField] private AudioSource sfxSource; // ses efektlerini oynatmak için ses kaynağı bileşeni atıyoruz
    [SerializeField] private AudioSource musicSource; // arka plan müziğini oynatmak için ses kaynağı bileşeni atıyoruz
    [SerializeField] private AudioClip greenBallSound; // yeşil topa deydiğimizde çalacak ses dosyası
    [SerializeField] private AudioClip redBallSound; // kırmızı topa deydiğimizde çalacak ses dosyası
    [SerializeField] private AudioClip jumpSound; // zıpladığımız zaman çalacak ses dosyası
    [SerializeField] private AudioClip backgroundMusic; // arka plan müzik dosyası

    private void Awake()
    {
        instance = this; // sınıfın referansını kendine eşitliyoruz ki her yerden çağırabilelim
    }

    void Start()
    {
        musicSource.clip = backgroundMusic; // müzik kaynağına arka plan müziğini yüklüyoruz
        musicSource.loop = true; // müziğin sürekli döngüde çalmasını sağlıyoruz
        musicSource.Play(); // müziği başlatıyoruz
        Debug.Log("music oynadı"); // konsola müziğin çaldığını yazdırıyoruz
    }

    public void PlayGreenBallSound() // yeşil top sesi için fonksiyon oluşturuyoruz
    {
        Debug.Log("green ball sesi oynatıldı"); // konsola yazdırıyoruz
        sfxSource.PlayOneShot(greenBallSound); // yeşil top ses efektini bir kerelik çalıyoruz
    }

    public void PlayRedBallSound() // kırmızı top sesi için fonksiyon oluşturuyoruz
    {
        Debug.Log("red ball sesi oynatıldı"); // konsola yazdırıyoruz
        sfxSource.PlayOneShot(redBallSound); // kırmızı top ses efektini bir kerelik çalıyoruz
    }

    public void PlayjumpSound() // zıplama sesi için fonksiyon oluşturuyoruz
    {
        Debug.Log("jump sesi oynatıldı"); // konsola yazdırıyoruz
        sfxSource.PlayOneShot(jumpSound); // zıplama ses efektini bir kerelik çalıyoruz
    }
}