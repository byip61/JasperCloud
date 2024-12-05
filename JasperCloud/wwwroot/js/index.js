// // // document.addEventListener("DOMContentLoaded", function () {
// // //   const fileButtons = document.querySelectorAll(".file-button");
// // //   const actionButtons = document.getElementById("action-buttons");
// // //   const downloadButton = document.getElementById("download-button");
// // //   const deleteButton = document.getElementById("delete-button");
// // //   let selectedFile = null;

// // //   fileButtons.forEach(button => {
// // //       button.addEventListener("click", function () {
// // //           // Deselect previously selected file
// // //           if (selectedFile) {
// // //               selectedFile.classList.remove("selected");
// // //           }

// // //           // Select the current file
// // //           this.classList.add("selected");
// // //           selectedFile = this;

// // //           // Show action buttons
// // //           actionButtons.style.display = "block";

// // //           // Update download and delete actions
// // //           const fileId = this.dataset.fileId;
// // //           const fileName = this.dataset.fileName;
// // //           updateActionButtons(fileId, fileName);
// // //       });
// // //   });

// // //   // Update download and delete button actions
// // //   function updateActionButtons(fileId, fileName) {
// // //       downloadButton.onclick = () => downloadFile(fileId, fileName);
// // //       deleteButton.onclick = () => deleteFile(fileId, fileName);
// // //   }

// // //   // Download file function
// // //   function downloadFile(fileId, fileName) {
// // //       const downloadUrl = `/File/Download/${fileId}`;
// // //       window.location.href = downloadUrl;
// // //   }

// // //   // Delete file function
// // //   function deleteFile(fileId, fileName) {
// // //       if (confirm(`Are you sure you want to delete "${fileName}"?`)) {
// // //           fetch(`/File/Delete/${fileId}`, { method: "DELETE" })
// // //               .then(response => {
// // //                   if (response.ok) {
// // //                       alert(`File "${fileName}" deleted successfully.`);
// // //                       window.location.reload();
// // //                   } else {
// // //                       alert("Failed to delete file.");
// // //                   }
// // //               })
// // //               .catch(error => console.error("Error deleting file:", error));
// // //       }
// // //   }
// // // });

// // document.addEventListener("DOMContentLoaded", function () {
// //   const fileButtons = document.querySelectorAll(".file-button");
// //   const actionButtons = document.getElementById("action-buttons");
// //   const downloadButton = document.getElementById("download-button");
// //   const deleteButton = document.getElementById("delete-button");
// //   let selectedFile = null;

// //   // Handle file button click
// //   fileButtons.forEach(button => {
// //       button.addEventListener("click", function () {
// //           // If the same button is clicked again, deselect it
// //           if (selectedFile === this) {
// //               this.classList.remove("selected");
// //               selectedFile = null;
// //               actionButtons.style.display = "none"; // Hide action buttons
// //               return;
// //           }

// //           // Deselect previously selected file
// //           if (selectedFile) {
// //               selectedFile.classList.remove("selected");
// //           }

// //           // Select the current file
// //           this.classList.add("selected");
// //           selectedFile = this;

// //           // Show action buttons
// //           actionButtons.style.display = "block";

// //           // Update download and delete actions
// //           const fileId = this.dataset.fileId;
// //           const fileName = this.dataset.fileName;
// //           updateActionButtons(fileId, fileName);
// //       });
// //   });

// //   // Update download and delete button actions
// //   function updateActionButtons(fileId, fileName) {
// //       downloadButton.onclick = () => downloadFile(fileId, fileName);
// //       deleteButton.onclick = () => deleteFile(fileId, fileName);
// //   }

// //   // Download file function
// //   function downloadFile(fileId, fileName) {
// //       const downloadUrl = `/File/Download/${fileId}`;
// //       window.location.href = downloadUrl;
// //   }

// //   // Delete file function
// //   function deleteFile(fileId, fileName) {
// //       if (confirm(`Are you sure you want to delete "${fileName}"?`)) {
// //           fetch(`/File/Delete/${fileId}`, { method: "DELETE" })
// //               .then(response => {
// //                   if (response.ok) {
// //                       alert(`File "${fileName}" deleted successfully.`);
// //                       window.location.reload();
// //                   } else {
// //                       alert("Failed to delete file.");
// //                   }
// //               })
// //               .catch(error => console.error("Error deleting file:", error));
// //       }
// //   }
// // })

// document.addEventListener("DOMContentLoaded", function () {
//   const fileButtons = document.querySelectorAll(".file-button");
//   const downloadDeleteButtons = document.getElementById("download-delete-buttons");
//   const downloadFilename = document.getElementById("download-file-name");
//   const deleteFilename = document.getElementById("delete-file-name");
//   let selectedFile = null;

//   fileButtons.forEach(button => {
//       button.addEventListener("click", function () {
//           const filename = this.dataset.filename;

//           if (selectedFile === this) {
//               this.classList.remove("selected");
//               selectedFile = null;
//               downloadDeleteButtons.style.display = "none";
//               return;
//           }

//           if (selectedFile) {
//               selectedFile.classList.remove("selected");
//           }

//           this.classList.add("selected");
//           selectedFile = this;

//           downloadDeleteButtons.style.display = "inline-block";

//           downloadFilename.value = filename;
//           deleteFilename.value = filename;
//       });
//   });
// });

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