using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GoTour.Helper {
    public abstract class Messages {
        public const string vInternalServerError = "La solicitud ha sido procesada pero ha generado una excepción";
        public const string vOkInserted = "Registro insertado correctamente";
        public const string vOkUpdated = "Registro actualizado correctamente";
        public const string vOkListed = "Información mostrada correctamente";
        public const string vNotUpdate = "Registro no actualizado";
        public const string vNotListed = "Ningun Resultado";
        public const string vLoginUnsuccessfully = "Usuario o Clave Incorrectos";
        public const string vLoginSuccessfully = "Autenticación Correcta";
        public const string vListContainNullValue = "La lista proporcionada de elementos contiene un valor nulo";
        public const string vNotInserted = "El Registro no pudo ser creado";
        public const string vEmailDuplicated = "El Email ya se encuentra registrado";
        public const string vPaymentGatewayError = "La solicitud no ha sido procesada. Error generado por la pasarela de pagos";
        public const string vInsertPaymentError = "Pago procesado. Pero no se ha podido guardar su pedido";
        public const string vCancelPaymentError = "Reembolso procesado. Pero no se ha podido Cancelar su pedido";
    }

    public abstract class GlobalValues {
        public const int vDefaultValueState = 1;
        public const int vZeroValuePosition = 0;
        public const int vUserStateNotActive = 2;
        public const string vTypeDocumentDefault = "0";
        public const string vTextExceptionParameterNull = "Parameter not be null";
    }
}