using BE;
using BLL;
using DAL;
using Services;
using System.Collections.Generic;
using System.Data;

public class BusinessDigitoVerificador
{
    private readonly DAL_DigitoVerificador DataAccess;

    public BusinessDigitoVerificador()
    {
        DataAccess = new DAL_DigitoVerificador();
    }

    public BusinessResponse<EntityDigitoVerificador> BuscarDigitoVerificador()
    {
        return new BusinessResponse<EntityDigitoVerificador>(true, DataAccess.GetDigitoVerificador());
    }

    public BusinessResponse<bool> ActualizarDigitoVerificador()
    {
        DataAccess.UpdateDigitoVerificador(CalcularDigitoVerificador().Data);
        return new BusinessResponse<bool>(true, true);
    }

    public BusinessResponse<EntityDigitoVerificador> CalcularDigitoVerificador()
    {
        List<string> tablesNames = new List<string>
        {
            "Evento",
            "Factura",
            "Detalle_Factura",
            "Entrada",
            "Cliente",
        };

        string dvh = string.Empty;
        string dvv = string.Empty;

        foreach (string tablesName in tablesNames)
        {
            DataTable dataTable = DataAccess.CountDigitoVerificador(tablesName);
            dvh += CalcularDVH(dataTable);
            dvv += CalcularDVV(dataTable);
        }

        EntityDigitoVerificador digitoVerificador = new EntityDigitoVerificador
        {
            DVH = CryptoManager.EncryptString(dvh),
            DVV = CryptoManager.EncryptString(dvv),
        };

        return new BusinessResponse<EntityDigitoVerificador>(true, digitoVerificador);
    }

    private string CalcularDVH(DataTable table)
    {
        string DVHHash = string.Empty;

        string DataConcat = string.Empty;

        foreach (DataRow row in table.Rows)
        {
            DataConcat = string.Empty;

            foreach (DataColumn column in row.Table.Columns)
            {
                DataConcat += row[column].ToString();
            }

            DVHHash += CryptoManager.EncryptString(DataConcat);
        }

        return CryptoManager.EncryptString(DVHHash);
    }

    private string CalcularDVV(DataTable table)
    {
        string DVVHash = string.Empty;

        string DataConcat = string.Empty;

        foreach (DataColumn column in table.Columns)
        {
            DataConcat = string.Empty;

            foreach (DataRow row in table.Rows)
            {
                DataConcat += row[column].ToString();
            }

            DVVHash += CryptoManager.EncryptString(DataConcat);
        }

        return CryptoManager.EncryptString(DVVHash);
    }

}