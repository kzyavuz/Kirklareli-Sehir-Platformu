let ilData = [];

// İl verilerini al ve il seçeneklerini oluştur
fetch('/Location/GetIl')
    .then(response => response.json())
    .then(data => {
        ilData = data; // ilData değişkenine il verilerini atıyoruz
        // Verileri alfabetik olarak sırala
        data.sort((a, b) => a.name.localeCompare(b.name));

        const ilSelect = document.getElementById('il');
        data.forEach(il => {
            const option = document.createElement('option');
            option.value = il.name;
            option.textContent = il.name;
            ilSelect.appendChild(option);
        });
    });

// İlçe verilerini al ve ilçe seçeneklerini oluştur
document.getElementById('il').addEventListener('change', () => {
    const ilAdi = document.getElementById('il').value; // Seçilen il adını al
    const ilId = ilData.find(il => il.name === ilAdi)?.id; // İlgili il adına karşılık gelen il id'sini bul

    fetch('/Location/GetIlce')
        .then(response => response.json())
        .then(data => {
            // Verileri alfabetik olarak sırala
            data.sort((a, b) => a.name.localeCompare(b.name));

            const ilceSelect = document.getElementById('ilce');
            ilceSelect.innerHTML = '<option value="">İlçe Seçiniz</option>';
            data.forEach(ilce => {
                if (ilce.il_id == ilId) {
                    const option = document.createElement('option');
                    option.value = ilce.name;
                    option.textContent = ilce.name;
                    ilceSelect.appendChild(option);
                }
            });
        });
});