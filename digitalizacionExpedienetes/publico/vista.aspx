<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPages/Site.Master" AutoEventWireup="true" CodeBehind="vista.aspx.cs" Inherits="recepciondeDocumentos.publico.cargaDocumentos.vista" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="/scripts/mbd/css/style.css" rel="stylesheet" />
    <script src="js/validaciones.js" type="text/javascript"></script>
    <script src="js/eventos.js" type="text/javascript"></script>
    <script src="../../js/utilerias.js" type="text/javascript"></script>
    <script src="../../scripts/js/pdf.worker.js"></script>
    <script src="../../scripts/js/pdf.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <main class="mt-5 pt-5">
        <div class="container">
            <section id="forms" class="text-left">
                <div id="divBarra">
                    <h1 class="title h1 my-4">1. Revisa tus datos</h1>
                    <div class="progress">
                        <div class="progress-bar" role="progressbar" style="width: 0%" aria-valuenow="25" aria-valuemin="0" aria-valuemax="100"></div>
                    </div>
                </div>
                <div id="divRevisaDatos">
                    <section class="contact-section my-5">
                        
                        <!-- Form with header -->
                        <div class="card">
                            <!-- Grid row -->
                            <div class="row">
                                <!-- Grid column -->
                                <div class="col-lg-4">
                                    <div class="card-body form">
                                        <!-- Header -->
                                        <h3 class="mt-4"><i class="fas fa-user pr-2"></i>&nbsp;Datos del alumno:</h3>
                                        <!-- Grid row -->
                                        <div class="row">
                                            <!-- Grid column -->
                                            <table id="DatosAlumno" class="table table-striped"></table>
                                            <!-- Grid column -->
                                        </div>
                                        <!-- Grid row -->
                                    </div>
                                </div>
                                <!-- Grid column -->
                                <div class="col-lg-8">
                                    <div class="card-body text-center">
                                        <h3 class="mb-0">Estado de preinscripción</h3>
                                        <hr class="my-2">
                                        <div id="EstatusProceso" class="alert" role="alert"></div>
                                        <div class="text-center">
                                            <a id="btnContinuarRD" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">Continuar
                                                <i class="fas fa-angle-double-right ml-2"></i>
                                            </a>
                                            <a id="btnSalir" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion" style="display: none;">Salir
                                                <i class="fas fa-angle-double-right ml-2"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <!-- Grid column -->
                            </div>
                            <!-- Grid row -->
                        </div>
                        <!-- Form with header -->
                    </section>
                </div>
                <div id="divContacto" style="display: none;">
                    <section class="contact-section my-5">
                        <!-- Form with header -->
                        <div class="card">
                            <!-- Grid row -->
                            <div class="row">
                                <!-- Grid column -->
                                <div class="col-lg-8">
                                    <div class="card-body form">
                                        <!-- Header -->
                                        <h3 class="mt-4"><i class="fas fa-user pr-2"></i>&nbsp;Agrega tus datos de contacto para continuar</h3>
                                        <!-- Grid row -->
                                        <div class="row">
                                            <!-- Grid column -->
                                            <div class="col-md-6">
                                                <div class="md-form">
                                                    <i class="fas fa-phone prefix grey-text"></i>
                                                    <input type="text" id="phone1" class="form-control tel">
                                                    <label for="orangeForm-phone">Teléfono</label>
                                                </div>
                                            </div>
                                            <!-- Grid column -->
                                            <!-- Grid column -->
                                            <div class="col-md-6">
                                                <div class="md-form">
                                                    <i class="fas fa-envelope prefix grey-text"></i>
                                                    <input type="text" id="email1" class="form-control correo">
                                                    <label for="orangeForm-email">Correo</label>
                                                </div>
                                            </div>
                                            <!-- Grid column -->
                                        </div>
                                        <!-- Grid row -->
                                        <div class="col-md-12">
                                            <h5><i class="fas fa-check-circle pr-2"></i>Confirma tus datos:</h5>
                                        </div>
                                        <!-- Grid row -->
                                        <div class="row">
                                            <!-- Grid column -->
                                            <div class="col-md-6">
                                                <div class="md-form">
                                                    <i class="fas fa-phone prefix grey-text"></i>
                                                    <input type="text" id="phone2" class="form-control tel">
                                                    <label for="orangeForm-phone">Teléfono</label>
                                                </div>
                                            </div>
                                            <!-- Grid column -->
                                            <div class="col-md-6">
                                                <div class="md-form">
                                                    <i class="fas fa-envelope prefix grey-text"></i>
                                                    <input type="text" id="email2" class="form-control correo">
                                                    <label for="orangeForm-email">Correo</label>
                                                </div>
                                            </div>
                                            <!-- Grid column -->
                                        </div>
                                        <!-- Grid row -->
                                    </div>
                                </div>
                                <!-- Grid column -->
                                <!-- Grid column -->
                                <div class="col-lg-4">
                                    <div class="card-body text-center">
                                        <h3 class="my-3">Nota:</h3>
                                        <hr class="my-3">
                                        <div class="alert alert-info" role="alert">
                                            Necesitas proporcionar tus datos de contacto para poder avistarte cuando tus documentos sean aprobados, 
                                            o tengan algún inconveniente.
                                        </div>
                                        <div class="text-center">
                                            <a id="btnRegresarC" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">
                                                <i class="fas fa-angle-double-left ml-2"></i>&nbsp;&nbsp;&nbsp;Regresar
                                            </a>
                                            <a id="btnContinuarC" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">Continuar
                                                <i class="fas fa-angle-double-right ml-2"></i>
                                            </a>
                                        </div>
                                    </div>
                                </div>
                                <!-- Grid column -->
                            </div>
                            <!-- Grid row -->
                        </div>
                        <!-- Form with header -->
                    </section>
                </div>
                <div id="divDocumentos" style="display: none;">
                    <!-- Card deck -->
                    <div class="col-md-12 mt-5">
                        <h4>Sube los documentos del alumno para su revisión:</h4>
                    </div>
                    <div class="container-fluid">
                        <div id="contentCardDeck">
                        </div>
                    </div>
                    <div class="text-center mb-4">
                        <a id="btnRegresarD" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">
                            <i class="fas fa-angle-double-left ml-2"></i>&nbsp;&nbsp;&nbsp;Regresar
                        </a>
                        <a id="btnEnviarD" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">Enviar
                            <i class="fas fa-paper-plane ml-2"></i>
                        </a>
                    </div>
                    <div id="pdf" class="modal fade show" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" style="display: none; padding-right: 17px; background-color: #00000082;" aria-modal="true">
                        <div class="modal-dialog modal-lg" role="document">
                            <div class="modal-content">
                                <div class="modal-footer py-0">
                                    <button id="btnCerra" type="button" class="btn-floating btn-sm py-0 px-0 mx-0 my-0" data-dismiss="modal" style="position: absolute; top: 1%; right: 1%; border: none;">
                                        <i class="fa fa-times fa-2x red"></i>
                                    </button>
                                </div>
                                <div class="modal-body mb-0 p-0">
                                    <div id="divContenedor" class="embed-responsive embed-responsive-4by3 z-depth-1-half" style="overflow-y: scroll; overflow-x: hidden;">
                                        <%--<iframe id="iframePreview" class="embed-responsive-item" src="" allowfullscreen="" style="display:none;" ></iframe>--%>
                                        <img id="imagePreview" class="embed-responsive-item" src="#" style="display: none; width: 100%; height: auto; padding: 2%;" />
                                        <canvas id="canvasPreview" class="embed-responsive-item" style="width: 100%; height: auto; padding: 2%;"></canvas>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="divFinal" style="display: none;">
                    <section class="contact-section my-5">
                        <div class="col-md-12 mb-3">
                            <div class="card">
                                <div class="card-body">
                                    <p>
                                        Ya recibimos tus documentos y un equipo capacitado se encuentra revizándolos.<br />
                                        Ahora <strong>sólo debes monitorear tu correo</strong> para ver nuestra respuesta.<br />
                                        Una vez revisados <strong>te enviaremos un correo electrónico para avisarte</strong> si tus documentos fueron aprobados, o si presentan algún inconveniente.<br />
                                        Los documentos se validarán en un plazo <strong>mínimo de 3 días hábiles</strong>, si tienes dudas comunícate al 01 800 2273792.
                                    </p>
                                </div>
                            </div>
                        </div>
                        <section class="col-md-12 mb-3">
                            <div class="card text-center">
                                <div class="card-header p-0">
                                    <h4 class="my-2">Recibirás una de estas respuestas en tu correo:</h4>
                                </div>
                                <div class="card-body">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="card alert alert-success text-sm">
                                                <div class="card-body">
                                                    <h5 class="card-title"><strong>Documentos aprobados</strong></h5>
                                                    <hr class="my-2" />
                                                    <p class="card-text text-left">
                                                        <strong>Significa que puedes continuar</strong><br />
                                                        Si recibes este mensaje, ya puedes descargar tu <i>Constancia de Entrega de Documentos Digitales</i> para entregar el primer día de clases.<br />
                                                        Al entregarla, terminarás el proceso.
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="card alert alert-danger text-sm">
                                                <div class="card-body">
                                                    <h5 class="card-title"><strong>Documentos rechazados</strong></h5>
                                                    <hr class="my-2" />
                                                    <p class="card-text text-left">
                                                        <strong>Significa que debes volver</strong><br />
                                                        Si recibes este mensaje, es necesario que subas de nuevo alguno de tus documentos, ya que presentó algún inconveniente.<br />
                                                        No te preocupes si tus documentos son rechazados, en el correo que recibes te explicamos paso a paso cómo puedes continuar.
                                                    </p>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="text-center">
                                        <h6><strong>¿Todo claro?, ¡bien!, puedes ver el estado de tu proceso en el siguiente enlace:</strong></h6>
                                        <a id="btnFinalizar" class="btn btn-outline-blue btn-sm waves-effect btn-rounded btnNavegacion">Finalizar
                                            <i class="fas fa-angle-double-right ml-2"></i>
                                        </a>
                                    </div>
                                </div>
                            </div>
                        </section>
                        <div class="col-md-12 mb-3">
                            <section class="text-center">
                                <h3><strong>¿Dudas o comentarios?<br />
                                    ¡Comunícate con nosotros!</strong></h3>
                                <h2><strong>01 800 2273792</strong></h2>
                            </section>
                        </div>
                        <div class="alert alert-secondary mb-0" role="alert">
                            <div class="card-title"><strong>Aviso de privacidad</strong></div>
                            <hr class="my-2" />
                            <p class="card-text">Los datos personales se encuentran protegidos y serán tratados solo para los fines que se establecen en nuestras Políticas de Privacidad, mismas que puedes consultar en <a href="http://www.sepyc.gob.mx">www.sepyc.gob.mx</a></p>
                        </div>
                    </section>
                </div>
                <!-- Button trigger modalPush-->
                <button id="btnMensaje" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalPush" style="display: none;">MensajeAlerta</button>
                <!--Modal: modalPush-->
                <div class="modal fade" id="modalPush" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div id="dialog" class="modal-dialog modal-sm modal-notify modal-danger" role="document">
                        <!--Content-->
                        <div class="modal-content text-center">
                            <!--Header-->
                            <div class="modal-header d-flex justify-content-center">
                                <p class="heading"><strong>AVISO</strong></p>
                            </div>
                            <!--Body-->
                            <div class="modal-body">
                                <i id="IconMensaje" class="fas fa-times fa-3x animated rotateIn mb-3"></i>
                                <p id="mensaje"></p>
                            </div>
                            <!--Footer-->
                            <div class="modal-footer flex-center py-2">
                                <a type="button" id="SIGuarda" class="btn  btn-outline-danger" style="display: none;">SI</a>
                                <a type="button" id="NOGuarda" class="btn  btn-danger waves-effect" data-dismiss="modal">OK</a>
                            </div>
                        </div>
                        <!--/.Content-->
                    </div>
                </div>
                <!--Modal: modalPush  fade -->
                <!-- Button trigger modalPoll-->
                <button id="btnMuestraDoc" type="button" class="btn btn-primary" data-toggle="modal" data-target="#modalPoll-1" style="display: none;">MuestraDocumentos</button>
                <!-- Modal: modalPoll -->
                <div class="modal fade right" id="modalPoll-1" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                    <div class="modal-dialog modal-full-height modal-right modal-notify modal-info" role="document">
                        <div class="modal-content">
                            <!--Header-->
                            <div class="modal-header">
                                <p class="heading lead"><strong>Estamos para ayudarte</strong></p>
                                <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                                    <span aria-hidden="true" class="white-text">×</span>
                                </button>
                            </div>
                            <!--Body-->
                            <div class="modal-body">
                                <div class="text-center">
                                    <i class="fas fa-file-alt fa-4x mb-3 animated rotateIn"></i>
                                    <p>Nos apena tener que pedirte que vuelvas a subir tus documentos, esto fue lo que sucedió...</p>
                                </div>
                                <hr>
                                <!-- Radio -->
                                <div id="divEstatusDoc"></div>
                                <!-- Radio -->
                                <p class="text-center">Te recomendamos que tomes la foto en un lugar iluminado y sin movimiento, o bien envíes tu documento escaneado en formato .PDF o .JPG.</p>
                                <p class="text-center">Recuerda que debes subir todos los documentos que se piden para que puedas descargar tu constancia.</p>
                            </div>
                            <!--Footer-->
                            <div class="modal-footer justify-content-center">
                                <a type="button" id="SubirDocumentos" class="btn btn-primary btn-md waves-effect waves-light">Subir documentos <i class="fa fa-upload ml-1"></i></a>
                                <a type="button" class="btn btn-outline-primary btn-md waves-effect" data-dismiss="modal">Cerra</a>
                            </div>
                        </div>
                    </div>
                </div>
                <!-- Modal: modalPoll -->
                <div id="mdb-preloader" class="modal-backdrop show flex-center" style="background-color: #fff; z-index: 50;">
                    <div id="load" style="z-index: 100;">
                        <div class="preloader-wrapper big active">
                            <div class="spinner-layer spinner-blue">
                                <div class="circle-clipper left">
                                    <div class="circle"></div>
                                </div>
                                <div class="gap-patch">
                                    <div class="circle"></div>
                                </div>
                                <div class="circle-clipper right">
                                    <div class="circle"></div>
                                </div>
                            </div>
                            <div class="spinner-layer spinner-red">
                                <div class="circle-clipper left">
                                    <div class="circle"></div>
                                </div>
                                <div class="gap-patch">
                                    <div class="circle"></div>
                                </div>
                                <div class="circle-clipper right">
                                    <div class="circle"></div>
                                </div>
                            </div>
                            <div class="spinner-layer spinner-yellow">
                                <div class="circle-clipper left">
                                    <div class="circle"></div>
                                </div>
                                <div class="gap-patch">
                                    <div class="circle"></div>
                                </div>
                                <div class="circle-clipper right">
                                    <div class="circle"></div>
                                </div>
                            </div>
                            <div class="spinner-layer spinner-green">
                                <div class="circle-clipper left">
                                    <div class="circle"></div>
                                </div>
                                <div class="gap-patch">
                                    <div class="circle"></div>
                                </div>
                                <div class="circle-clipper right">
                                    <div class="circle"></div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </section>
        </div>
    </main>
</asp:Content>
