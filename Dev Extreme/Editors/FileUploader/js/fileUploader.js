$(() => {
    const fileUploaderWidget = $('#fileUploader').dxFileUploader({
        accept: '*',
        allowCanceling: true,
        uploadMode: 'useButtons', // useButtons, instantly, useForm
        chunkSize: 2000000, // Size in bytes for each chunk, example: 2MB
        allowedFileExtensions: ['.jpg', '.jpeg', '.gif', '.png'], // Specify allowed file extensions
        invalidMaxFileSizeMessage: 'Itna Zyada kam mat kr yaar, thodi chhoti file daal !!!',
        invalidMinFileSizeMessage: 'Thodi badi file daal !!!',
        labelText: 'File Ko Utha ke Idhar rakh de 😊😊😊',
        multiple: true,
        uploadAbortedMessage: 'File Upload nahi krni ',
        maxFileSize: 4000000, // Maximum file size in bytes, example: 4MB
        onBeforeSend: function (e) {
            console.log('Before sending:', e);
        },
        onUploadAborted: function (e) {
            console.log('Upload aborted:', e);
        },
        onUploaded: function (e) {
            console.log('File uploaded:', e);
            const file = e.file;
            const fileInfo = `
                <p>File Name: ${file.name}</p>
                <p>File Size: ${(file.size / 1024).toFixed(2)} KB</p>
                <p>File Type: ${file.type}</p>
            `;
            $('#fileDetails').html(fileInfo);
        },
        onUploadError: function (e) {
            console.log('Upload error:', e);
        },
        onUploadStarted: function (e) {
            console.log('Upload started:', e);
        },
        onProgress: function (e) {
            const chunkSize = 2000000; // Size in bytes for each chunk, example: 2MB
            const uploadedBytes = e.segmentSize;
            const totalBytes = e.bytesTotal;
            const uploadedChunks = Math.ceil(uploadedBytes / chunkSize);
            const totalChunks = Math.ceil(totalBytes / chunkSize);

            const chunkInfo = `
                <p>Uploaded Chunks: ${uploadedChunks} / ${totalChunks}</p>
                <p>Chunk Size: ${(chunkSize / 1024).toFixed(2)} KB</p>
                <p>Uploaded Bytes: ${(uploadedBytes / 1024).toFixed(2)} KB</p>
                <p>Total Bytes: ${(totalBytes / 1024).toFixed(2)} KB</p>
            `;
            $('#chunkDetails').html(chunkInfo);
        },
        uploadButtonText: 'Ghusa Do !!!',
        uploadFailedMessage: 'Nahi Daal Paya 🥺🥺🥺',
        uploadedMessage: 'Kaam Ho Gaya 🥳🥳🥳',
        showFileList: true,
        selectButtonText: 'File Leke Aa Chal',
        readyToUploadMessage: 'Daal du ???',
        invalidFileExtensionMessage: 'Ye Extension wali file nahi chalegi BRO !!!!',
        uploadUrl: 'https://js.devexpress.com/Demos/NetCore/FileUploader/Upload',
    }).dxFileUploader('instance');
});
