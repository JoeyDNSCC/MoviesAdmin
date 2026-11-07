document.addEventListener("DOMContentLoaded", function () {

    const dropZone = document.getElementById("imageDropZone");
    const fileInput = document.getElementById("ImageFile");
    const selectedImage = document.getElementById("selectedImage");

    if (!dropZone || !fileInput) return;

    // open file broser on click
    dropZone.addEventListener("click", function () {
        fileInput.click();
    });

    // prevent default browser drag behaviour
    ["dragenter", "dragover", "dragleave", "drop"].forEach(event => {
        dropZone.addEventListener(event, function (e) {
            e.preventDefault();
            e.stopPropogation();
        });
    });

    // Highlight drop zone
    ["dragenter", "dragover"].forEach(event => {
        dropZone.addEventListener(event, function () {
            dropZone.classList.add("drag-over");
        });
    });

    // Handle dropped files
    dropZone.addEventListener("drop", function (e) {
        const files = e.dataTransfer.files;

        if (files.length > 0) {
            fileInput.files = files;
            displayFile(files[0]);
        }
    });

    // Handle manually selected files
    fileInput.addEventListener("change", function () {
        if (fileInput.files.length > 0) {
            displayFile(fileInput.files[0]);
        }
    });

    // Display selected filename

    let previewUrl = null;

    // Display selected filename and image preview
    function displayFile(file) {

        // Make sure the selected file is an image
        if (!file.type.startsWith("image/")) {
            alert("Please select a valid image file.");
            fileInput.value = "";
            selectedImage.innerHTML = "";
            return;
        }

        selectedImage.innerHTML = "";

        // Display filename
        const fileInfo = document.createElement("div");
        fileInfo.className = "d-flex align-items-center gap-2";

        const icon = document.createElement("i");
        icon.className = "bi bi-file-earmark-image";

        const fileName = document.createElement("span");
        fileName.textContent = file.name;

        fileInfo.appendChild(icon);
        fileInfo.appendChild(fileName);

        selectedImage.appendChild(fileInfo);

        // Update image preview if one exists
        const imagePreview = document.getElementById("movieImagePreview");

        if (imagePreview) {

            // Release previous temporary preview URL
            if (previewUrl) {
                URL.revokeObjectURL(previewUrl);
            }

            // Generate new preview
            previewUrl = URL.createObjectURL(file);

            imagePreview.src = previewUrl;
        }
    }

});