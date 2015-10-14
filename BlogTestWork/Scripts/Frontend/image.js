(function() {
    $(document).ready(function() {
        $('#chooseAvatar').change(function() {
            imgPreview(this);
        });
    });

    function imgPreview(input) {
        var reader;

        if (input.files && input.files[0]) {
            reader = new FileReader();

            reader.onload = function(e) {
                $('#avatarBig').attr('src', e.target.result);
            }

            reader.readAsDataURL(input.files[0]);
        }
    }
})();