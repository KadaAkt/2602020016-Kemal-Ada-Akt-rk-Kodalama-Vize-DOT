using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    [SerializeField] private int requiredScore = 30; //bölümü geçmek için gereken minimum puan değerini belirliyoruz

    private void OnTriggerEnter(Collider other) //tetiklenme alanına girdiğinde çalışacak çarpışma fonksiyonunu oluşturuyoruz
    {
        PlayerController player = other.GetComponent<PlayerController>(); //çarpan nesnenin üzerinden PlayerController bileşenine ulaşıyoruz

        if (player.Score >= requiredScore) //eğer oyuncunun puanı gereken puana eşit veya fazlaysa bölümü geçmesine izin veriyoruz
        {
            LoadNextLevel(player); //sonraki seviyeyi yüklemek için fonksiyonumuzu çağırıyoruz
        }
        else //puan yetersiz kaldığı durumlar için else kısmını belirliyoruz
        {
            Debug.Log("Yeterli puan yok! Gerekli: " + requiredScore + ""); //puanın yetmediğini ve gereken puanı konsola yazdırıyoruz
        }
    }

    private void LoadNextLevel(PlayerController player) //sonraki seviyeye geçiş yapmak için fonksiyon oluşturuyoruz
    {
        int bulunanSeviye = SceneManager.GetActiveScene().buildIndex; //şu an aktif olan sahnenin sıra numarasını öğreniyoruz
        int sıradakiSeviye = bulunanSeviye + 1; //geçmek istediğimiz bir sonraki sahnenin numarasını hesaplıyoruz

        if (sıradakiSeviye < SceneManager.sceneCountInBuildSettings) //sıradaki seviye projedeki toplam sahne sınırları içinde mi diye kontrol ediyoruz
        {
            Debug.Log("Seviye geçildi!"); //konsola seviyenin başarıyla geçildiğini yazdırıyoruz
            SceneManager.LoadScene(sıradakiSeviye); //hesapladığımız sıradaki yeni sahneyi yüklüyoruz
        }
        else //oyundaki başka sahne kalmadıysa, yani oyun bittiyse çalışacak else kısmını yazzıyoruz
        {
            Debug.Log("Tebrikler, oyunu bitirdin!"); //oyunun bittiğini konsola tebrikler mesajımızı yazdırıyoruz
            player.CompleteGame(); //oyunu bitirme fonksiyonumuzu devreye sokuyoruz
        }
    }
}