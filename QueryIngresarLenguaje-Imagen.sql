insert into Control_Idioma values 
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1),
('','',1)

INSERT INTO Evento (Imagen)
SELECT *
FROM OPENROWSET(BULK 'C:\ruta\a\tu\imagen.jpg', SINGLE_BLOB) AS Imagen;