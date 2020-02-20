using System;

namespace Entity
{
    [Serializable]
    public class Enumeradores
    {
        public enum Estatus_Cat_Centra
        {
            Activo = 1,
            Baja = 2,
            Clausura = 3,
            Reapertura = 4,
            Proceso_Clausura = 7
        }
        public enum CE_C_documentos
        {
            ACTA_DE_NACIMIENTO_INICIAL = 2,
            ACTA_DE_NACIMIENTO = 3,
            CONSTANCIA_DE_TRANSLADO = 4,
            CONSTANCIA_DE_TRANSLADO_Kinde = 5,
            CURP = 6,
            COMPROBANTE_DE_DOMICILIO = 7

        }
        public enum C_documentos
        {
            FORMATO_UNICO_DE_PERSONAL = 1,
            RELACION_VALIDACION = 2,
            REPORTE_DE_INCIDENCIAS = 3,
            VOLANTE = 4,
            RELACION_DE_VOLANTES_DE_SUSPENSION_DE_SUELDOS = 5,
            RECHAZO = 6,
            DEVOLUCION = 7
        }

        public enum C_estatusAlumnos
        {
            ACTIVOS = 8
        }


        public enum C_Areas
        {
            SECUNDARIA_TECNICA = 1,
            TELESECUNDARIA = 2,
            EDUCACION_BASICA_PARA_ADULTOS = 3,
            FISICA = 4,
            INICIAL = 5,
            EDUCACION_ESPECIAL = 6,
            PREESCOLAR = 7,
            PRIMARIA_GENERAL = 8,
            EDUCACION_INDIGENA = 9,
            SECUNDARIA_GENERAL = 10,
            SEPDES_ADG = 11,
            MEJORAMIENTO_PROFESIONAL = 12,
            NORMAL_EXPERIMENTAL_DEL_FUERTE = 13,
            SUBSECRETARIA_DE_EDUCACION_BASICA_CONTROLYGESTION = 14,
            RECEPCION_Y_ENVIO = 15,
            VALIDACION_DE_PLAZAS = 16,
            DIRECCION_GENERAL_DE_SERVICIOS_ADMINISTRATIVOS = 17,
            SUBSECRETARIA_DE_ADMINISTRACION_DE_PERSONAL = 18,
            DIRECCION_DE_RECURSOS_HUMANOS = 22,
            UNIDAD_DE_REGISTRO_Y_CONTROL_DE_TRAMITE = 24,
            VALIDACION_DE_EFECTOS = 25,
            SUBJEFATURA_DE_CAPTURA = 26,
            SUBJEFATURA_DE_ARCHIVO_Y_REGISTRO = 27
        }
        public enum C_parentescos
        {
            Padre = 1,
            Madre = 2,
            Tutor = 3
        }
        //public enum Tipo_Usuario
        //{


        //    Director = 1,
        //    Supervisor = 2,
        //    Jefe_Sector = 3,
        //    Regional = 4,
        //    Administrador = 5,
        //    Captura_Area_Influencia = 6,
        //    Consulta = 7
        //}

        //public enum Estatus_Actas_Constitutivas
        //{
        //    Sin_Captura = 0,
        //    Activo = 1,
        //    Registrado = 2,

        //}
        //public enum Tipo_Integrante_Mesa_Debates
        //{
        //    Presidente = 1,
        //    Secretario = 2,
        //    Primer_Escrutador = 3,
        //    Segundo_Escrutador = 4,
        //    Tercer_Escrutador = 5
        //}

        //public enum Tipo_Integrante_Mesa_Directiva
        //{
        //    Presidente = 1,
        //    VicePresidente = 2,
        //    Tesorero = 3,
        //    Secretario = 4,
        //    Primer_Vocal = 5,
        //    Segundo_Vocal = 6,
        //    Tercer_Vocal = 7,
        //    Cuarto_Vocal = 8,
        //    Quinto_Vocal = 9,
        //    Sexto_Vocal = 10
        //}
        public enum Regionales
        {
            LOS_MOCHIS = 1,
            GUASAVE = 2,
            GUAMUCHIL = 3,
            CULIACAN = 4,
            MAZATLAN = 5,
            ROSARIO = 6,
            EL_FUERTE = 7,
            LA_CRUZ = 8

        }

        public enum cat_sistemas
        {
            Formato_Único_de_Personal = 1,
            Sistema_de_Honorarios = 2,
            Administración = 3,
            Recepcion_de_facturas = 4,
            Control_Escolar = 5,
            Consultas_Ejecutivas = 6,
            Correspondencia =7,
            Consejos_Tecnicos_Escolares=8,
            Sistema_Estatal = 9 ,
            Digitalizacion = 10

        }

        public enum C_tramitesTipos
        {
            FORMATO_UNICO_DE_PERSONAL_ALTA = 1,
            FORMATO_UNICO_DE_PERSONAL_BAJA = 2,
            FORMATO_UNICO_DE_PERSONAL_ESTATAL_ALTA = 5,
            FORMATO_UNICO_DE_PERSONAL_ESTATAL_BAJA = 6

        }


        public enum C_motivosTipo
        {
            NINGUNO = -1,
            ALTA = 0,
            BAJA = 1,
            LICENCIA = 2,
            PRORROGA = 3,
            REANUDACIÓN = 4,
            EMBARGO = 5,
            RECHAZO_PROPUESTA = 6,
            CAMBIOS = 7,
            PROVISIONAL = 11,
            TIEMPO_FIJO = 12,
            DEFINITIVO = 13,
            DOCENTE = 14,
            TECNICO_DOCENTE = 15,
            DIRECTIVO = 16,
            PERSONAL_DE_APOYO_Y_ASISTENCIA_A_LA_EDUCACION = 17

        }




        public enum C_tramites
        {
            FORMATO_RECHAZADO_BAJA = -4,
            FORMATO_CAPTURADO_BAJA = -3,
            FORMATO_CAPTURADO_ALTA = -2,
            PROPUESTA_RECHAZADA_ALTA = -1,
            CAPTURA_DE_DATOS_FORMATO_AREA_CORRESPONDIENTE_ALTA = 1,
            APLICACION_DE_EFECTOS = 2,
            IMPRESION_DEL_FORMATO = 3,
            ENVIANDO_AREA_CORRESPONDIENTE = 4,
            RECEPCION_SUBSECRETARÍA_DE_ADMINISTRACION__DE_PERSONAL = 5,
            ENVIANDO__SUBSECRETARÍA_DE_ADMINISTRACION_DE_PERSONAL = 6,
            RECEPCION_UNIDAD_ADMINISTRATIVA_DE_PERSONAL = 7,
            ENVIANDO_UNIDAD_ADMINISTRATIVA_DE_PERSONAL_COMPATIBILIDAD = 8,
            RECEPCION_UNIDAD_ADMINISTRATIVA_DE_PERSONAL_VALIDACION_Y_CONTROL_PLAZAS = 9,
            ENVIANDO_UNIDAD_ADMINISTRATIVA_DE_PERSONAL_VALIDACION_Y_CONTROL_PLAZAS = 10,
            RECEPCION_DIRECCION_DE_RECUSROS_HUMANOS = 11,
            ENVIANDO_DIRECCION_DE_RECURSOS_HUMANOS = 12,
            RECEPCION_DIRECCION_GENERAL_DE_SERVICIOS_ADMINISTRATIVOS = 13,
            ENVIANDO_DIRECCION_GENERAL_DE_SERVICIOS_ADMINISTRATIVOS = 14,
            RECEPCION_AREA_DE_CAPTURA = 15,
            CAPTURA_DE_DATOS_FORMATO_AREA_CORRESPONDIENTE_BAJA = 16,
            IMPRESION_DEL_FORMATO_BAJA = 17



        }

        public enum C_motivos
        {
            NINGUNO = -1,
            ALTA_INICIAL = 09,
            ALTA_DEFINITIVA = 10,
            ALTA_INTERNIA = 20,
            ALTA_CONFIANZA = 96,
            ALTA_INTERINA_CUBRE_PERSONAL_EN_ESTADO_DE_GRAVIDEZ = 24,
            ALTA_INTERINA_CUBRE_LICENCIA_PREPENCIONARIA = 25,
            ALTA_ILIMITADA = 95,
            BAJAR_POR_DEFUNCIÓN = 31,
            BAJAR_POR_REANUDACIÓN = 32,
            BAJA_POR_JUBILACIÓN_O_PRENSIÓN = 33,
            BAJA_POR_ABANDONO_DE_EMPLEO = 34,
            BAJA_POR_TÉRMINO_DE_NOMBRAMIENTO = 35,
            BAJA_POR_DICTAMEN_ESCALAFONARIO = 36,
            BAJA_POR_PASAR_A_OTRO_EMPLEO = 37,
            BAJA_POR_INSUBSISTENCIA_DE_NOMBRAMIENTO = 38,
            BAJA_POR_REGULARIZACIÓN_DE_PLANTILLA = 39,
            BAJA_POR_SENTENCIA_JUDICIAL = 73,
            BAJA_POR_RESOLUCIÓN_DEL_TRIBUNAL_DE_CONCILIACIÓN_Y_ARBITRAJE = 74,
            BAJA_POR_INCAPACIDAD_ISSSTE = 75,
            BAJA_POR_CAMBIO_DE_ADSCRIPCIÓN = 76,
            LICENCIA_POR_ASUNTOS_PARTICULARES_SIN_SUELDO = 41,
            LICENCIA_PARA_PASAR_A_OTRO_EMPLEO = 42,
            LICENCIA_POR_COMISIÓN_SINDICAL_O_ELECCIÓN_POPULAR = 43,
            LICENCIA_POR_GRAVIDEZ = 44,
            LICENCIA_POR_INCAPACIDAD_MÉDICA_CON_MEDIO_SUELDO = 45,
            LICENCIA_POR_INCAPACIDAD_MEDICA_SIN_SUELDO = 46,
            LICENCIA_POR_BECA_EN_EL_EXTRANJERO = 47,
            LICENCIA_PREPENSIONARIA = 48,
            LICENCIA_MEDICA_CON_SUELDO = 49,
            PRÓRROGA_DE_LICENCIAS_POR_ASUNTOS_PARTICULARES = 51,
            PRÓRROGA_DE_LICENCIAS_POR_POR_OTRO_EMPLEO = 52,
            PRÓRROGA_DE_LICENCIAS_POR_COMISIÓN_SINDICAL_O_ELECCIÓN_POPULAR = 53,
            PRÓRROGA_DE_LICENCIAS_POR_INCAPACIDAD_MÉDICA_CON_SUELDO_Y_MEDIO_SUELDO = 55,
            PRÓRROGA_DE_LICENCIAS_POR_INCAPACIDAD_MÉDICA_SIN_SUELDO = 56,
            PRÓRROGA_DE_LICENCIAS_POR_BECA_EN_EL_EXTRANJERO = 57,
            PRÓRROGA_DE_LICENCIA_MEDICA_CON_SUELDO = 59,
            REANUDACIÓN__DE_LABORES_POR_TÉRMINO_DE_LICENCIA_DE_ASUNTOS_PARTICULARES = 61,
            REANUDACIÓN_DE_LABORES_POR_TÉRMINO_DE_LICENCIA_POR_OTRO_EMPLEO = 62,
            REANUDACIÓN__DE_LABORES_POR_TÉRMINO_DE_LICENCIA_POR_COMISIÓN_SINDICAL_O_ELECCIÓN_POPULAR = 63,
            REANUDACIÓN__DE_LABORES_POR_LICENCIA_POR_INCAPACIDAD_MÉDICA_CON_MEDIO_SUELDO = 65,
            REANUDACIÓN__DE_LABORES_POR_INCAPACIDAD_MÉDICA_SIN_SUELDO = 66,
            REANUDACIÓN_DE_LABORES_POR_LICENCIA_POR_BECA_EN_EL_EXTRANJERO = 67,
            REANUDACIÓN__DE_LABORES_POR_DESPUÉS_DE_GOZAR_LICENCIA_PREJUBILATORIA_O_PREPENSIONARIA = 68,
            REANUDACIÓN__DE_LABORES_POR_REANUDACIÓN_DE_LABORES_POR_LICENCIAEN_PLAZ_CONGELADA_POR_TITULAR = 69,
            BAJA_DE_EMBARGO = 2,
            CAMBIO_DE_EMBARGO = 3,
            UNICA = 0,
            INICIAL = 1,
            BASE = 11,
            PENSION_POR_EDAD_Y_TIEMPO_DE_SERVICIO = 30,
            AUSENCIA_EN_SERV_DESAPARECIDO__DENUNCIA_EN_MP = 40,
            ALTA_MAYOR_A_6_y_Medio_MESES = 97,
            PRORROGA_AUSENCIA_EN_SERV = 50
        }
        public enum CE_C_alumnosEstatus
        {
            NINGUNO = 1,
            BAJA = 2,
            PREINSCRITO = 3,
            VALIDACIONESCUELA = 4,
            REVISIONZONAESCOLAR = 5,
            VALIDACIONZONAESCOLAR = 6,
            REVISIONSEPYC = 7,
            INSCRITO = 8

        }

        public enum CE_C_tipoDocumentosGenerados
        {
            IAE = 1,
            R1 = 2,
            R2 = 3,
            R3 = 4,
            CREL = 5,
            REL = 6,
            SYRCER = 7,
            CERTIFICADO = 8,
            CERTIFICACION = 9,
            CERTIFICACION_PARCIAL = 10,
            ACT_NAC = 11,
            CURP=12,
            COMP_DOM=13,
            COMP_EST=14,
            REPORTE_DE_EVALIACION=15,
            IAR=16

        }


        public enum CE_C_TipoCertificadoDuplicado
        {
            TOTAL = 1,
            PARCIAL = 2

        }
        public enum CE_C_alumnosFoliosCertificadosEstatus
        { 
            GENERADO=3,
            IMPRESO=4,
            SOLICITUDREIMPRESION=5
        }

        public enum C_permisos_especiales
        {
            BAJA_DE_ALUMNOS_EN_MODALIDAD_FRN = 1            
        }
        public enum PRE_C_alumnosEstatus
        {
            BAJA=0,
            PREINSCRIPCION_LINEA = 1,
            PREINSCRIPCION_SIEE = 2
            

        }
    }
}
