
document.querySelector('.custom-file-input').addEventListener('change', function (e) {
    var file = e.target.files[0];
    if (file) {
        var reader = new FileReader();
        reader.onload = function (e) {
            document.getElementById('thumbnail').src = e.target.result;
        }
        reader.readAsDataURL(file);
    }
});
