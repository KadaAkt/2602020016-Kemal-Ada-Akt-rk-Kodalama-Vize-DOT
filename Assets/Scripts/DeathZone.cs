using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) //görünmez duvara bir şey değdiğinde çalışır
    {
        Debug.Log("Düştün!"); //konsola düştüğümüzü yazdırıyoruz
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //aktif sahneyi yeniden yükleyerek bölümü sıfırlıyoruz
    }
}
