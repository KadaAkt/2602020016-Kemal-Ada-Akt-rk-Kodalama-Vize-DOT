
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 10f; // topumuzun hızı  
    [SerializeField] private float jumpForce = 5f; // topumuzun zıplama gücü
    private bool gameCompleted = false; //oyun bittiğinde kullanmamız için değişken atıyoruz

    private Rigidbody rb; //rigbody bileşeni ekledik
    private bool isGrounded = false; //sadece yerdeyken zıplamasını sağlamak için yerde olup olmadığını kontrol ediyoruz

    private float horizontal; //yatay 
    private float vertical; //dikey 
    public int Score = 0; //skor tablomuzdaki skor değerimizin başlangıç değerini girdik
    [SerializeField] private TextMeshProUGUI scoreText; //skor tablomuzu yazdırmak için bileşen atıyoruz


    private void Awake()
    {
        rb = GetComponent<Rigidbody>(); //rigbody bileşeninin çalışmasını sağlıyoruz
    }

    private void Update() 
    {
        if (gameCompleted is true) return; //oyun bittiyse kontrolu bırakmamızı sağlıyoruz
        horizontal = Input.GetAxisRaw("Horizontal"); //ham yatay değer 
        vertical = Input.GetAxisRaw("Vertical"); //ham dikey değer

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //eğer yerde ise zıplamamıza izin veriyoruz
        {
            Jump();
        }
    }

    private void FixedUpdate() //fizik hesaplamaları için FixedUpdate kullanıyoruz
    {
        if (gameCompleted is true) return; //oyun bittiyse fizik hesaplamasını bırakıyoruz
        Movement();
    }

    private void Movement() //hareket fonksiyonu oluşturuyoruz
    {
        Vector3 direction = new Vector3(horizontal, 0f, vertical); //dikeyde hareket etmeyeceğimiz için sıfıra kitliyoruz

        direction = direction.normalized; //vektörün yönünü bozmadan uzunluğunu 1 yapıyoruz

        rb.AddForce(direction * moveSpeed); //ivme almasını sağlıyoruz
    }

    private void Jump() //zıplamak için fonksiyon oluşturuyoruz
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); //ivmeli şekilde zıplıyoruz
        AudioManager.instance.PlayjumpSound(); //AudioManagerda atadığımız ses dosyasını çekip kullanıyoruz
        isGrounded = false; //yerde olmadığını belirtiyoruz
        print("Zıpladın!"); //konsola yazdırıyoruz

    }
    private void UpdateScoreUi() //skor tablomuz için fonskiyon oluşturuyoruz
    { 
        scoreText.text = "Score : " + Score.ToString(); //skor tablomuzu güncelliyoruz
    }

    private void OnCollisionEnter(Collision collision) //fizikel çarpışma olduğu için OnCollision kullandık
    {
        if (collision.gameObject.CompareTag("Ground")) //yere değdiğimiz zaman konsoldan bildirmesini sağlıyoruz ayrıca tekrar zıplayabilmemiz için isGrounded değerini true yapıyoruz
        {
            isGrounded = true; //tekrardan yerde olduğunu belirtiyoruz
            Debug.Log("Yerdesin!"); //Konsola yazdırıyoruz
        }
    }
    private void OnTriggerEnter(Collider other) //nesneler birbiri içinden geçtiği için OnTriggerEnter kullandık
    {
        if (other.CompareTag("GreenBall")) //yeşil topa değdiğimiz zaman puan kazanmamızı, ses oynatmasını ve topun yok olmasını sağlıyoruz
        {
            Score += 10; //skor değerimizi arttırıyoruz
            UpdateScoreUi(); //skor tablomuzu güncelliyoruz
            AudioManager.instance.PlayGreenBallSound(); //AudioManagerda atadığımız ses dosyasını çekip kullanıyoruz
            Debug.Log("GreenBall! Score" + Score); //konsola yazdırıyoruz
            Destroy(other.gameObject); //çarpışma yaşandıktan sonra topun silinmesini sağlıyoruz
        }
        else if (other.CompareTag("RedBall")) //kırmızı topa değdiğimiz zaman puan kaybetmemizi, ses oynatmasını ve topun yok olmasını sağlıyoruz
        { 
            Score -= 5; //skor değerimizi azaltıyoruz
            UpdateScoreUi(); //skor tablomuzu güncelliyoruz
            AudioManager.instance.PlayRedBallSound(); //AudioManagerda atadığımız ses dosyasını çekip kullanıyoruz
            Debug.Log("RedBall! Score" + Score); //konsola yazdırıyoruz
            Destroy(other.gameObject); //çarpışma yaşandıktan sonra topun silinmesini sağlıyoruz
        }
    }

    public void CompleteGame() //oyun bitince SceneController tarafından çağrılır
    {
        gameCompleted = true;
        rb.linearVelocity = Vector3.zero; //top yuvarlanmaya devam etmesin
        rb.angularVelocity = Vector3.zero;
    }
}