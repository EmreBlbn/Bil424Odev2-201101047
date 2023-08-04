# Bil424 Ödev2
 Emre Balaban 201101047
# Karşılaşılan Zorluklar
- Radar ekranı için bir resim eklerken transparant resim eklerken arka planı da gözüküyordu. Çözüm olarak:
- Unity'nin shader ayarlarını araştırdım ve bu sorunu çözdüm

- Bir diğer zorluk ise oyuncunun döndüğünde LRF ekranınındaki düşmanların bu dönüşe uygun gözükmemesiydi. Bu sorunu da:
 > newPosition = Quaternion.AngleAxis(player.transform.eulerAngles.y, Vector3.up) * newPosition;
>
 koduyla, düşmanların Ekrandaki pozisyonunu oyuncunun dönme açısıyla uyumlu olmasını sağladım.
 
- Karşılaştığım son zorluk ise karakter seçme ekranındaki seçimleri diğer ekranlarda kullanmak oldu. İlk yaklaşımım public değişkenler kullanmaktı.
  Ama daha sonrasında PlayerPrefs ile bu sorunu kolaylıkla çözebileceğimi keşfettim.

  
# Oyun Videosu
https://github.com/EmreBlbn/Bil424Odev2-201101047/assets/78085195/55d6af8f-810c-4607-8576-150ca2b9d1e3

