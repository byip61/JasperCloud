document.addEventListener("DOMContentLoaded", function () {
  const fileButtons = document.querySelectorAll(".file-button");
  const downloadFileGuid = document.getElementById("download-file-name");
  const deleteFileGuid = document.getElementById("delete-file-name");
  const downloadDeleteButtons = document.getElementById("download-delete-buttons");
  let selectedFile = null;

  fileButtons.forEach(button => {
      button.addEventListener("click", function () {
          const fileGuid = this.dataset.fileGuid;

          if (!fileGuid) {
            console.error("File name is undefined. Ensure the data-file-name attribute is set.");
            return;
          }

          if (selectedFile === this) {
              this.classList.remove("selected");
              selectedFile = null;
              downloadDeleteButtons.style.display = "none";
              downloadFileGuid.value = "";
              deleteFileGuid.value = "";
              return;
          }

          if (selectedFile) {
              selectedFile.classList.remove("selected");
          }

          this.classList.add("selected");
          selectedFile = this;

          downloadFileGuid.value = fileGuid;
          deleteFileGuid.value = fileGuid;

          downloadDeleteButtons.style.display = "inline-block";
      });
  });
});