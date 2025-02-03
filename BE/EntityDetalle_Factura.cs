namespace BE
{
    public class EntityDetalle_Factura
    {
        public int? Id_Detalle { get; set; }
        public int? Id_Factura { get; set; }
        public int Id_Entrada { get; set; }
        public string Tipo_Entrada { get; set; }
        public int Cantidad_Entradas { get; set; }
        public double Costo_parcial { get; set; }
    }
}
