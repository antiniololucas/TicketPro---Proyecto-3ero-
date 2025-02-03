using BE;
using Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public static class MailSender
{
    public static void EnviarCorreos(EntityEvento ev ,List<EntityCliente> destinatarios)
    {

        // Configuración de la cuenta emisora
        string smtpServer = "smtp.gmail.com"; // Ejemplo para Gmail
        int smtpPort = 587; // Puerto para Gmail
        string senderEmail = "ticketpro.recordatorios@gmail.com"; // Tu correo electrónico
        string senderPassword = "esyt fknm vbxe vaxh"; // La contraseña de la cuenta de correo

        // Crear el mensaje
        string subject = "Nuevo show que podria interesarte!!!!"

        try
        {
            // Configurar el cliente SMTP
            using (SmtpClient smtpClient = new SmtpClient(smtpServer, smtpPort))
            {
                smtpClient.Credentials = new NetworkCredential(senderEmail, senderPassword);
                smtpClient.EnableSsl = true;

                // Enviar el mensaje a cada destinatario
                foreach (EntityCliente destinatario in destinatarios)
                {
                    string body = $"Hola, {destinatario.Nombre}!! \n Nos comunicamos con vos porque este {ev.Fecha} a las {ev.Horario} en {ev.Ubicacion} podras disfrutar del espectaculo de {ev.Artista} y creemos que encaja a la perfeccion contigo \n No te lo podes perder... Te esperamos en nuestros locales para conseguir tu entrada";
                    MailMessage mailMessage = new MailMessage
                    {
                        From = new MailAddress(senderEmail),
                        Subject = subject,
                        Body = body
                    };

                    mailMessage.To.Add(CryptoManager.ReversibleDecrypt(destinatario.Mail));

                    // Enviar el correo
                    smtpClient.Send(mailMessage);
                }
            }

            MessageBox.Show("Correos enviados correctamente.");
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error al enviar los correos: {ex.Message}");
        }
    }
}
