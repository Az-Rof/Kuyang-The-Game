using UnityEngine;
using UnityEngine.VFX;

public class HideScript : MonoBehaviour
{
    private PlayerController player;
    private VisualEffect visualEffect;
    private Rigidbody2D playerRb;

    private void Start()
    {
        visualEffect = GetComponent<VisualEffect>();
        if (visualEffect != null)
        {
            visualEffect.enabled = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            player = other.GetComponent<PlayerController>();
            playerRb = other.GetComponent<Rigidbody2D>();
            if (player != null)
            {
                player.SetHidingSpot(this);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Hanya bersihkan jika pemain tidak sedang bersembunyi
        if (other.CompareTag("Player") && player != null && !player.isHiding)
        {
            player.ClearHidingSpot(this);
        }
    }

    public void ToggleHide()
    {
        if (player == null) return;

        if (player.isHiding)
        {
            // Player keluar dari persembunyian
            player.isHiding = false;
            if (visualEffect != null) visualEffect.enabled = false;

            // Aktifkan kembali gerak dan physics
            if (playerRb != null)
            {
                playerRb.simulated = true;
                playerRb.velocity = Vector2.zero; // reset kecepatan
            }

            // tampilkan sprite lagi
            player.GetComponent<SpriteRenderer>().enabled = true;
        }
        else
        {
            // Player masuk ke persembunyian
            player.isHiding = true;
            if (visualEffect != null) visualEffect.enabled = true;

            // Jangan nonaktifkan physics sepenuhnya, cukup hentikan gerak
            if (playerRb != null)
            {
                playerRb.simulated = false; // <-- Tambahkan baris ini
                playerRb.velocity = Vector2.zero;
            }

            // Sembunyikan sprite player
            player.GetComponent<SpriteRenderer>().enabled = false;

            // Setelah bersembunyi, kita bisa membersihkan referensi di PlayerController
            // agar pemain tidak bisa berinteraksi dengan tempat persembunyian lain saat sudah di dalam.
            // Namun, kita tetap perlu referensi untuk bisa keluar.
            // Jadi, kita biarkan saja referensinya.
        }
    }

}
