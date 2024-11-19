function downloadPDF() {
    var pdfContainer1 = document.getElementById('pdf-container');
    var canvasWidth = pdfContainer1.offsetWidth * 1.05;
    var canvasHeight = pdfContainer1.offsetHeight * 1.05;
    var downloadButton = document.querySelector('.btn.btn-outline-primary');
    var originalDisplay = downloadButton.style.display;
    downloadButton.style.display = 'none';
    html2canvas(pdfContainer1, {
        scale: 2,
        logging: true,
        dpi: window.devicePixelRatio * 192,
        useCORS: true,
        width: canvasWidth,
        height: canvasHeight
    }).then(function (canvas1) {
        downloadButton.style.display = originalDisplay;
        var imgData1 = canvas1.toDataURL('image/jpeg', 1.0);
        var pdf = new jspdf.jsPDF('p', 'mm', 'a4');
        var width = pdf.internal.pageSize.getWidth();
        var height = pdf.internal.pageSize.getHeight();
        pdf.addImage(imgData1, 'JPEG', 0, 0, width, height);
        pdf.save('Exam_Report.pdf');
    });
}
