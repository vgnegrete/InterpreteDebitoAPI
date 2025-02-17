using System;
using InterpreteDebitoAPI.Entities;

namespace InterpreteDebitoAPI;

public static class Utilerias
{

    public static List<DetalleDatosAdicionales> DesglosarDatosAdicionales (string pDatosAdicionales)
    {
        List<DetalleDatosAdicionales> lstDatosAdicionales = new List<DetalleDatosAdicionales>();
         //Desgloce de campo 48 Datos Adicionales
        string DatosAdicionales  = new string ( pDatosAdicionales);
        while (DatosAdicionales.Count() > 0) 
        {
            //Tomar longitud
            int lon = Int16.Parse(DatosAdicionales[0..2]);
            DatosAdicionales = DatosAdicionales[2..];

            //Tomar clave+Campo
            string subcampo = DatosAdicionales[0..lon];
            DatosAdicionales = DatosAdicionales[lon..];

            //extraer cod y data
            string cod = subcampo[0..2];
            string Data = subcampo[2..];

            lstDatosAdicionales.Add(new DetalleDatosAdicionales() {Codigo = cod, Descripcion=ObtenerDescripcionxCodigo(cod), Data= Data});
        }

        return lstDatosAdicionales;
    }

    public static List<DescripcionPuntoServicio> DesglosarDatosPuntoServicio(string pDatosPuntoServicio)
    {
        List<DescripcionPuntoServicio> lstDatosPuntoServicio = new List<DescripcionPuntoServicio>();

        try
        {
            string value = pDatosPuntoServicio[0..1];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =1, Valor = value, Description ="Capacidad de captura de Datos del terminal.",  ValorTrama=ObtenerMedioCapturaP1(value) } );
            
            value = pDatosPuntoServicio[1..2];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =2, Valor = value, Description ="Capacidad de autenticar al cliente en la terminal.",  ValorTrama=ObtenerMedioAutenticacionP2(value) } );
            
            value = pDatosPuntoServicio[2..3];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =3, Valor = value, Description ="Capacidad de retencion de tarjeta.",  ValorTrama=ObtenerCapacidadRetencionP3(value) } );

            value = pDatosPuntoServicio[3..4];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =4, Valor = value, Description ="Tipo de terminal.",  ValorTrama=ObtenerTipoTerminaP4(value) } );
            
            value = pDatosPuntoServicio[4..5];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =5, Valor = value, Description ="Presencia del cliente.",  ValorTrama=ObtenerPrecenciaClienteP5(value) } );
            
            value = pDatosPuntoServicio[5..6];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =6, Valor = value, Description ="Presencia de la tarjeta.",  ValorTrama=ObtenerPresenciaTarjetaP6(value) } );

            value = pDatosPuntoServicio[6..7];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =7, Valor = value, Description ="Método utilizado para capturar los datos de la tarjeta en el terminal.",  ValorTrama=ObtenerMedioCapturaTarjetaP7(value) } );

            value = pDatosPuntoServicio[7..8];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =8, Valor = value, Description ="Método de atentación del cliente.",  ValorTrama=ObtenerMetodoAtencionClienteP8(value) } );

            value = pDatosPuntoServicio[8..9];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =9, Valor = value, Description ="Dispositivo o Entidad que debe autenticar al cliente.",  ValorTrama=ObtenerEntidadQueAutenticaP9(value) } );
            
            value = pDatosPuntoServicio[9..10];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =10, Valor = value, Description ="Capacidad del terminal de actualización de los datos de la tarjeta.",  ValorTrama=ObtenerCapacidadActualizacionP10(value) } );
            
            value = pDatosPuntoServicio[10..11];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =11, Valor = value, Description ="Capacidad del terminal para imprimir y/o mostrar mensajes.",  ValorTrama=ObtenerCapacidadImpresion11(value) } );
            
            value = pDatosPuntoServicio[11..12];
            lstDatosPuntoServicio.Add(new DescripcionPuntoServicio() { Posicion =12, Valor = value, Description ="Longitud máxima de PIN que el terminal es capaz de tratar.",  ValorTrama=ObtenerMaximaTrataPinP12(value) } );

        } catch{}
        
        return lstDatosPuntoServicio;
    }

    public static string ObtenerDescripcionxCodigo(string Codigo)
    {
        switch(Codigo){
            case "01":
                return "SIA-IDE-CD";
            case "02":
                return "Datos de Consulta de últimos movimientos.";
            case "03":
                return "Pagos de recibos";
            case "06":
                return "Datos de Visaphone";
            case "07":
                return "Datos de E-pay";
            case "08":
                return "Datos de Monedero";
            case "09":
                return "Datos de Cuenta elegida";
            case "10":
                return "Datos de Traspasos";
            case "11":
                return "Datos de cuenta asociada";
            case "12":
                return "Datos de CVV2";
            case "13":
                return "Datos de consulta de usuario";
            case "14":
                return "Datos de cheques";
            case "15":
                return "Datos de transaccion forzada";
            case "16":
                return "Datos de importes adicionales";
            case "17":
                return "Datos de domiciliacion"; 
            case "18":
                return "Datos de cambio de PIN"; 
            case "19":
                return "Datos de MVV"; 
            case "23":
                return "Datos de indicador de transaccion"; 
            case "25":
                return "Datos de forma de pago";   
            case "28":
                return "Datos de Impuestos";
            case "29":
                return "Datos de propina";
            case "30":
                return "Datos de comision";
            case "31":
                return "Datos de Token de negocio";    
            default:
                return "";

        }

    }

    public static string ObtenerMedioCapturaP1(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Sin especificar";
            case "1":
                return "Datos manuales";
            case "2":
                return "Banda magnetica";
            case "3":
                return "Codigo de barras";
            case "4":
                return "OCR";
            case "5":
                return "Tarjeta chip";
            case "6":
                return "Entrada clave";
            case "7":
                return "Banda Magnetica ICC";
            case "8":
                return "Banda Magnetica clave";
            case "9":
                return "Banda ICC Clave";
            case "A":
                return "Capt. Terminal EMV";
            case "B":
                return "Capt. Terminal S-set";
            case "C":
                return "Capt. Terminal S-no-set";
            case "D":
                return "Capt. Terminal SSL";
            case "E":
                return "Capt. Terminal Virtual"; 
            case "F":
                return "Chip Sin Contacto"; 
            case "G":
                return "Lectura Banda sin contacto"; 
            default:
                return "";

        }

    }

    public static string ObtenerMedioAutenticacionP2(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Sin capacidad de lectura";
            case "1":
                return "Autentica pin";
            case "2":
                return "Firma Electronica";
            case "5":
                return "No operativa";
            case "6":
                return "Otros IdClien";
            default:
                return "";
        }
    }

    public static string ObtenerCapacidadRetencionP3(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Sin capacidad de captura";
            case "1":
                return "Con capacidad de captura";
            default:
                return "";
        }
    }

    public static string ObtenerTipoTerminaP4(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "No Terminal";
            case "1":
                return "Terminal atendido comercio";
            case "2":
                return "Terminal no atendido comercio";
            case "3":
                return "Terminal atendido fuera";
            case "4":
                return "Terminal no atendido fuera";
            case "5":
                return "Terminal no atendido casa";
            case "9":
                return "Terminal móvil mpos";
            case "A":
                return "Dispositivo Tarjeta";
            case "B":
                return "Mobile Net Operation";
            case "C":
                return "Dispositivo Token";
            case "D":
                return "Dispositivo Reloj";
            case "E":
                return "Dispositivo Telepeaje"; 
            case "F":
                return "Dispositivo Muñequera"; 
            case "G":
                return "Dispositivo Base Lectora"; 
            case "H":
                return "Dispositivo móvil con elemento extraíble (SIM) controlado por el operador móvil"; 
            case "I":
                return "Dispositivo móvil con elemento fijo controlado por el operador móvil"; 
            case "J":
                return "Elemento seguro extraíble no controlado por el operador móvil usado en móviles"; 
            case "K":
                return "Tablet con elemento extraíble (SIM) controlado por el operador móvil"; 
            case "L":
                return "Tablet con elemento fijo controlado por el operador móvil"; 
            case "M":
                return "Elemento seguro extraíble no controlado por el operador móvil usado en tablets."; 
            case "N":
                return "Elemento seguro fijo no controlado por el operador móvil"; 
            default:
                return "";
        }
    }

    public static string ObtenerPrecenciaClienteP5(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Cliente Presente";
            case "1":
                return "Cliente no presente";
            case "2":
                return "Cliente correo";
            case "3":
                return "Cliente telefono";
            case "4":
                return "Cliente no presente Aut. Per.";
            case "7":
                return "Cliente electronico seguro";
            case "8":
                return "Cliente electronico no seguro";
            default:
                return "";
        }
    }

    public static string ObtenerPresenciaTarjetaP6(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Tarjeta no presente";
            case "1":
                return "Tarjeta presente";
            default:
                return "";
        }
    }

    public static string ObtenerMedioCapturaTarjetaP7(string Codigo)
    {
        switch(Codigo){
            case "0":
            case "A":
                return "Sin especificar";
            case "1":
            case "B":
                return "Manual sin terminal";
            case "2":
            case "C":
                return "Aut. banda magnética";
            case "3":
            case "D":
                return "Codigo de barras";
            case "4":
            case "E":
                return "OCR";
            case "5":
            case "F":
                return "Tarjeta chip";
            case "6":
            case "G":
                return "Pista 1";
            case "H":
                return "Ing Err. Chip Manual";
            case "I":
                return "Ing. Err Chip Banda";
            case "J":
                return "Internet";
            case "K":
                return "Banda Magnética y Pistas";
            case "L":
                return "Tarjeta Chip No CVV";
            case "M":
                return "Clave";
            case "O":
                return "Voz";
            case "Q":
                return "eBanking";
            default:
                return "";
        }
    }

    public static string ObtenerMetodoAtencionClienteP8(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Cliente no autenticado";
            case "1":
                return "Cliente autentica PIN";
            case "2":
                return "Cliente Firma Electronica";
            case "5":
                return "Verificacion manual firma";
            case "6":
                return "Verificacion documento";
            case "7":
                return "Certificado SET";
            case "8":
                return "Autentica Cliente OT Certificado";
            case "9":
                return "Autentica Cliente PIN Offline";
            case "A":
                return "Datos 3D presentes";
            case "B":
                return "Datos 3D no presentes";
            default:
                return "";
        }
    }

    public static string ObtenerEntidadQueAutenticaP9(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Dispositivo no autentica cliente";
            case "1":
                return "Autentica Tarjeta Chip";
            case "2":
                return "Autentica Terminal";
            case "3":
                return "Autentica emisor";
            case "4":
                return "Autentica Establecimiento";
            case "5":
                return "Autentica otros";
            default:
                return "";
        }
    }

    public static string ObtenerCapacidadActualizacionP10(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Capacidad de actualización desconocida";
            case "1":
                return "Sin capacidad de actualización";
            case "2":
                return "Capacidad de actualización de la banda";
            case "3":
                return "Capacidad de actualización de la Tarjeta Chip";
            default:
                return "";
        }
    }

    public static string ObtenerCapacidadImpresion11(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "Capacidad de impresion desconocida";
            case "1":
                return "Sin capacidad de impresion";
            case "2":
                return "Con capacidad de impresion";
            case "3":
                return "Con capacidad de mostrar displays";
            default:
                return "";
        }
    }

    public static string ObtenerMaximaTrataPinP12(string Codigo)
    {
        switch(Codigo){
            case "0":
                return "No trata PIN";
            case "1":
                return "Sin longitud maxima";
            case "4":
                return "Trata cuatro";
            case "5":
                return "Trata cinco";
            case "6":
                return "Trata seis";
            case "7":
                return "Trata siete";
            case "8":
                return "Trata ocho";
            case "9":
                return "Trata nueve";
            case "A":
                return "Trata diez";
            case "B":
                return "Trata once";
            case "C":
                return "Trata doce";
            default:
                return "";
        }
    }

}
