using BE;
using System;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Windows.Forms;
using QRCoder;
using ClosedXML.Excel;
using System.Data; // Agregar la referencia a QRCoder para generar códigos QR

public static class PDFGenerator
{
    public static void GeneratePDF(EntityFactura factura, EntityDetalle_Factura detalle, EntityEvento evento, EntityEntrada entrada)
    {
        PrintDocument printDocument = new PrintDocument();
        printDocument.PrintPage += (sender, e) => PrintPage(sender, e, factura, detalle, entrada, evento);

        PrintPreviewDialog printPreviewDialog = new PrintPreviewDialog
        {
            Document = printDocument,
            Width = 900,
            Height = 700,
            StartPosition = FormStartPosition.CenterScreen, 
            FormBorderStyle = FormBorderStyle.SizableToolWindow,
        };

        if (printPreviewDialog.ShowDialog() == DialogResult.OK)
        {
            printDocument.Print();
        }
    }

    private static void PrintPage(object sender, PrintPageEventArgs e, EntityFactura factura, EntityDetalle_Factura detalle, EntityEntrada entrada, EntityEvento evento)
    {
        float yPos = 10f;
        float leftMargin = e.MarginBounds.Left;
        float rightMargin = e.MarginBounds.Right;
        Font titleFont = new Font("Arial", 14, FontStyle.Bold);
        Font infoFont = new Font("Arial", 14);
        Font legalFont = new Font("Arial", 12); // Aumentar tamaño de la fuente legal

        // Convertir byte[] a Image y dibujarla con tamaño adecuado
        using (Image image = ByteArrayToImage(evento.Imagen))
        {
            int imageHeight = 400; // Aumentar tamaño de la imagen
            int imageWidth = (int)(e.PageBounds.Width - leftMargin * 2); // Ocupa todo el ancho
            e.Graphics.DrawImage(image, leftMargin, yPos, imageWidth, imageHeight);
            yPos += imageHeight + 20; // Ajustar yPos después de la imagen
        }

        e.Graphics.DrawString("Evento:", titleFont, Brushes.Black, leftMargin, yPos);
        e.Graphics.DrawString(evento.Nombre, infoFont, Brushes.Black, leftMargin + 80, yPos); // Ajustar sangría
        yPos += infoFont.GetHeight(e.Graphics) + 5;
        e.Graphics.DrawString($"{evento.Fecha} - {evento.Horario} - {evento.Ubicacion}", infoFont, Brushes.Black, leftMargin + 80, yPos); // Ajustar sangría
        yPos += infoFont.GetHeight(e.Graphics) + 5;
        int spacing = 100; 
        e.Graphics.DrawString("Factura N°  ", titleFont, Brushes.Black, leftMargin, yPos);
        e.Graphics.DrawString(factura.Id.ToString(), infoFont, Brushes.Black, leftMargin + spacing, yPos); // Ajustar sangría con más espacio
        yPos += infoFont.GetHeight(e.Graphics) + 5;
        e.Graphics.DrawString("Cliente:", titleFont, Brushes.Black, leftMargin, yPos);
        e.Graphics.DrawString(factura.DNI_Cliente.ToString(), infoFont, Brushes.Black, leftMargin + 80, yPos); // Ajustar sangría
        yPos += infoFont.GetHeight(e.Graphics) + 5;
        e.Graphics.DrawString("Total:", titleFont, Brushes.Black, leftMargin, yPos);
        e.Graphics.DrawString($"{factura.Monto_Total:C}", infoFont, Brushes.Black, leftMargin + 80, yPos); // Ajustar sangría
        yPos += infoFont.GetHeight(e.Graphics) + 20;

        // Generar y añadir el código QR usando QRCoder
        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        QRCodeData qrCodeData = qrGenerator.CreateQrCode($"Factura No: {factura.Id}, Total: {factura.Monto_Total:C} , Entrada nro°: {entrada.Id}", QRCodeGenerator.ECCLevel.Q);
        QRCode qrCode = new QRCode(qrCodeData);
        Bitmap qrImage = qrCode.GetGraphic(10);

        int qrSize = 300;
        int qrXPos = (int)(leftMargin + (e.MarginBounds.Width - qrSize) / 2); // Centrar QR horizontalmente considerando los márgenes
        e.Graphics.DrawImage(qrImage, qrXPos, yPos, qrSize, qrSize);

        // Ajustar yPos después del código QR
        yPos += qrSize + 20;

        // Añadir texto legal
        string legalText = "Este es un documento generado por TicketPro. Para más información sobre políticas y términos, por favor visite nuestro sitio web.\n" +
                           "Generado por TicketPro, Lucas Antiñolo. Proyecto de la facultad.";
        e.Graphics.DrawString(legalText, legalFont, Brushes.Black, new RectangleF(leftMargin, e.PageBounds.Height - 100, e.PageBounds.Width - leftMargin * 2, 100)); // Ajustar posición y tamaño
    }

    private static Image ByteArrayToImage(byte[] byteArray)
    {
        using (MemoryStream ms = new MemoryStream(byteArray))
        {
            return Image.FromStream(ms);
        }
    }


    public static void SaveToExcel(DataTable dataTable, string filePath)
    {
        using (XLWorkbook workbook = new XLWorkbook())
        {
            workbook.Worksheets.Add(dataTable, "Sheet1");
            workbook.SaveAs(filePath);
        }
    }

}
