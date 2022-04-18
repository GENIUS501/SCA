$.extend($.fn.dataTableExt.oStdClasses, {
    "sFilterInput": "input-text with-border",
    "sLengthSelect": ""
});


function CargarDt(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        iniciaMp();
                        var data = row.data();
                        return 'Información';
                    },
                    renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                        tableClass: 'table'
                    })
                })
            }
        },
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"></i> Vista de impresión',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                "className": ''
            },

            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            }

        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};

function CargarDtSinResponsive(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, 'desc']],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "extend": 'excelHtml5',
                "text": '<i class="icon-line-awesome-file-excel-o"></i> Exportar a excel',
                "titleAttr": 'Excel',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",
                },
                "autoFilter": true,
                "sheetName": 'Datos exportado SNE'
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"></i> Vista de impresión',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                "className": ''
            },

            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            }

        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};

function CargarDtNotificadorYEventos(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "extend": 'excelHtml5',
                "text": '<i class="icon-line-awesome-file-excel-o"></i> Exportar a excel',
                "titleAttr": 'Excel',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",
                },
                "autoFilter": true,
                "sheetName": 'Datos exportado SNE'
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"></i> Vista de impresión',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                "className": ''
            },
            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            },
            {
                "text": "<i class='icon-material-outline-notifications'></i> Notificar",
                "action": function () {
                    var tabla = $("#table_personas_interesadas").DataTable();

                    GetIds(tabla);

                    var contador = localStorage.getItem('IdsPersNotifCusosInte');
                    if (contador.length > 0) {
                        $(".Crear-tab").html('<div class="text-center"><div class="lds-dual-ring"></div></div>');

                        $(".Crear-tab").load(UrlNotificaciones, { rol: "Persona Usuaria", validacion: true }, function (responseTxt, statusTxt, xhr) {
                            if (statusTxt == "error") {
                                $(".Crear-tab").append("<span style='color:red'>Ha ocurrido un problema. Por favor, contacte con un especialista</span>");
                            }
                        });

                        $.magnificPopup.open({
                            items: {
                                src: '.modal-crear'
                            },
                            type: 'inline',
                            closeOnBgClick: false,
                            fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                        });
                    } else {
                        Snackbar.show({
                            text: '¡No hay personas interesadas en el curso!',
                            pos: 'top-center'
                        });
                    }
                }
            },
            {
                "text": "<i class='icon-feather-users'></i> Eventos",
                "action": function () {
                    var tabla = $("#table_personas_interesadas").DataTable();

                    GetIds(tabla);
                    var contador = localStorage.getItem('IdsPersNotifCusosInte');

                    if (contador.length > 0) {
                        window.location.href = UrlEventos;
                    } else {
                        Snackbar.show({
                            text: '¡No hay personas interesadas en el curso!',
                            pos: 'top-center'
                        });
                    }
                }
            }
        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};

function CargarDtExcel(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        iniciaMp();
                        var data = row.data();
                        return 'Información';
                    },
                    renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                        tableClass: 'table'
                    })
                })
            }
        },
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            }
        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
}

function CargarDtDinamica(TablaId, Ajax, Columns, Processing, ServerSide, StateSave, LocalStorageKey) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring' style='z-index:999999'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        sScrollX: true,
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        autoWidth: true,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-refresh'></i> Restaurar tabla y filtros",
                "action": function () {
                    n = new Noty({
                        layout: 'center',
                        text: 'Restableciendo la tabla a valores iniciales...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    window.localStorage.removeItem(LocalStorageKey);
                    this.state.clear();

                    location.reload();
                }
            },
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    this.ajax.params().filters = ObtenerFiltros();
                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "text": '<i class="icon-line-awesome-columns"></i> Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "action": function () {
                    $.magnificPopup.open({
                        items: {
                            src: '.modal-columnas'
                        },
                        type: 'inline',
                        closeOnBgClick: false,
                        fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                    });
                }
            }
        ],
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
}

function CargarDtDinamicaPri(TablaId, Ajax, Columns, Processing, ServerSide, StateSave, LocalStorageKey) {
    tablaPri = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring' style='z-index:999999'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        sScrollX: true,
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        searching: false,
        autoWidth: true,
        order: [[1, "asc"], [2, "asc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-refresh'></i> Restaurar tabla y filtros",
                "action": function () {
                    n = new Noty({
                        layout: 'center',
                        text: 'Restableciendo la tabla a valores iniciales...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    window.localStorage.removeItem(LocalStorageKey);
                    this.state.clear();

                    location.reload();
                }
            },
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    this.ajax.params().filters = ObtenerFiltros();
                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "text": '<i class="icon-line-awesome-columns"></i> Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "action": function () {
                    $.magnificPopup.open({
                        items: {
                            src: '.modal-columnas'
                        },
                        type: 'inline',
                        closeOnBgClick: false,
                        fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                    });
                }
            }
        ],
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
}

function CargarDtSinBotones(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        responsive: {
            details: {
                display: $.fn.dataTable.Responsive.display.modal({
                    header: function (row) {
                        iniciaMp();
                        var data = row.data();
                        return 'Información';
                    },
                    renderer: $.fn.dataTable.Responsive.renderer.tableAll({
                        tableClass: 'table'
                    })
                })
            }
        },
        dom: 'Bfrtip',
        buttons: [
            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            }
        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};

function CargarDtSinSeleccionarColumnas(TablaId, Ajax, Columns, Processing, ServerSide, StateSave, LocalStorageKey) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring' style='z-index:999999'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        sScrollX: true,
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        autoWidth: true,
        order: [[0, "desc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-refresh'></i> Restaurar tabla y filtros",
                "action": function () {
                    n = new Noty({
                        layout: 'center',
                        text: 'Restableciendo la tabla a valores iniciales...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    window.localStorage.removeItem(LocalStorageKey);
                    this.state.clear();

                    location.reload();
                }
            },
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    this.ajax.params().filters = ObtenerFiltros();
                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            }          
        ],
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
}

function CargarDtDinamicaConsecutivo(TablaId, Ajax, Columns, Processing, ServerSide, StateSave, LocalStorageKey) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring' style='z-index:999999'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        sScrollX: true,
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        autoWidth: true,
        order: [[1, "asc"], [2, "asc"]],
        columnDefs: [{
            searchable: false,
            orderable: false,
            targets: 0
        }],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-refresh'></i> Restaurar tabla y filtros",
                "action": function () {
                    n = new Noty({
                        layout: 'center',
                        text: 'Restableciendo la tabla a valores iniciales...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    window.localStorage.removeItem(LocalStorageKey);
                    this.state.clear();

                    location.reload();
                }
            },
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    this.ajax.params().filters = ObtenerFiltros();
                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "text": '<i class="icon-line-awesome-columns"></i> Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "action": function () {
                    $.magnificPopup.open({
                        items: {
                            src: '.modal-columnas'
                        },
                        type: 'inline',
                        closeOnBgClick: false,
                        fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                    });
                }
            }
        ],
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });

    //tabla.on('order.dt search.dt', function () {
    //    tabla.column(1, { search: 'applied', order: 'applied' }).nodes().each(function (cell, i) {
    //        cell.innerHTML = i + 1;
    //    });
    //}).draw();
}

function CargarDtDinamicaPriorizacionV(TablaId, Ajax, Columns, Processing, ServerSide, StateSave, LocalStorageKey) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring' style='z-index:999999'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Último",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        sScrollX: true,
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        autoWidth: true,
        order: [[0, "asc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "text": "<i class='icon-line-awesome-refresh'></i> Restaurar tabla y filtros",
                "action": function () {
                    n = new Noty({
                        layout: 'center',
                        text: 'Restableciendo la tabla a valores iniciales...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    window.localStorage.removeItem(LocalStorageKey);
                    this.state.clear();

                    location.reload();
                }
            },
            {
                "text": "<i class='icon-line-awesome-file-excel-o'></i> Exportar a excel",
                "action": function () {
                    if (typeof UrlGenerarReporte === 'undefined' || typeof UrlObtenerReporte === 'undefined') {
                        alert('El reporte no está disponible');
                        return;
                    }

                    this.ajax.params().filters = ObtenerFiltros();
                    var params = JSON.parse(this.ajax.params());

                    n = new Noty({
                        layout: 'center',
                        text: 'Generando reporte, por favor espere. Esto puede tardar unos minutos...',
                        timeout: false,
                        progressBar: false,
                        theme: 'metroui',
                        closeWith: "",
                        modal: true
                    }).show();

                    $.post(UrlGenerarReporte, { Modelo: params }, function () {
                        var Elemento = `<button id="DescargarArchivo" data-url="${UrlObtenerReporte}"></button>`
                        $("body").append(Elemento);
                        $('#DescargarArchivo').trigger('click').remove();
                    })
                        .done(function () {
                            n.close();
                        })
                        .fail(function () {
                            alert("error");
                        });
                }
            },
            {
                "text": '<i class="icon-line-awesome-columns"></i> Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "action": function () {
                    $.magnificPopup.open({
                        items: {
                            src: '.modal-columnas'
                        },
                        type: 'inline',
                        closeOnBgClick: false,
                        fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                    });
                }
            }
        ],
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
}

function iniciaMp() {
    $('.popup-with-zoom-anim').magnificPopup({ type: 'inline', fixedContentPos: false, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in' });
}

function GetIds(tabla) {
    var Ids = [];
    $.each(tabla.rows().data(), function () {
        Ids.push(this["LLP_ID"]);
    });
    localStorage.setItem('IdsPersNotifCusosInte', Ids);
    localStorage.setItem('ValidacionNotificacion', true);
}

function CargarDtSinResponsiveInteresados(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, 'asc']],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "extend": 'excelHtml5',
                "text": '<i class="icon-line-awesome-file-excel-o"></i> Exportar a excel',
                "titleAttr": 'Excel',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",
                },
                "autoFilter": true,
                "sheetName": 'Datos exportado SNE'
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"></i> Vista de impresión',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                "className": ''
            },

            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            }

        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};

function CargarDtNotificadorYEventosInteresados(TablaId, Ajax, Columns, Processing, ServerSide, StateSave) {
    tabla = $(TablaId).DataTable({
        language: {
            search: "_INPUT_",
            searchPlaceholder: "Buscar...",
            sLengthMenu: "_MENU_",
            decimal: "",
            emptyTable: "<span class='text-error'><i class='icon-feather-alert-triangle'></i> No se encontraron resultados</span>",
            info: "Mostrando _START_ a _END_ de _TOTAL_",
            infoEmpty: "Mostrando 0 de 0",
            infoFiltered: "(Filtrado de _MAX_ registros totales)",
            infoPostFix: "",
            thousands: ",",
            loadingRecords: "Cargando...",
            processing: "<div class='lds-dual-ring'></div>",
            zeroRecords: "No se encontraron resultados",
            paginate: {
                "first": "Primero",
                "last": "Ultimo",
                "next": "Siguiente",
                "previous": "Anterior"
            },
            buttons: {
                colvisRestore: "Restaurar Columnas"
            }
        },
        stateSave: StateSave,
        processing: Processing,
        serverSide: ServerSide,
        order: [[0, "asc"]],
        ajax: Ajax,
        columns: Columns,
        responsive: true,
        dom: 'Bfrtip',
        buttons: [
            {
                "extend": 'excelHtml5',
                "text": '<i class="icon-line-awesome-file-excel-o"></i> Exportar a excel',
                "titleAttr": 'Excel',
                "className": '',
                "exportOptions": {
                    "columns": ":visible",
                },
                "autoFilter": true,
                "sheetName": 'Datos exportado SNE'
            },
            {
                "extend": 'print',
                "text": '<i class= "icon-feather-printer"></i> Vista de impresión',
                "titleAttr": 'Imprimir',
                "exportOptions": {
                    "columns": ":visible"
                },
                "className": ''
            },
            {
                "extend": 'colvis',
                "text": 'Columnas seleccionadas <i class="icon-material-outline-arrow-drop-down"></i>',
                "titleAttr": 'Columnas',
                "columnText": function (dt, idx, title) {
                    return (idx + 1) + ': ' + title;
                },
                "collectionLayout": 'two-column',
                "postfixButtons": ['colvisRestore'],
                "className": '',
                "columns": ":not(:last-child)"
            },
            {
                "text": "<i class='icon-material-outline-notifications'></i> Notificar",
                "action": function () {
                    var tabla = $("#table_personas_interesadas").DataTable();

                    GetIds(tabla);

                    var contador = localStorage.getItem('IdsPersNotifCusosInte');
                    if (contador.length > 0) {
                        $(".Crear-tab").html('<div class="text-center"><div class="lds-dual-ring"></div></div>');

                        $(".Crear-tab").load(UrlNotificaciones, { rol: "Persona Usuaria", validacion: true }, function (responseTxt, statusTxt, xhr) {
                            if (statusTxt == "error") {
                                $(".Crear-tab").append("<span style='color:red'>Ha ocurrido un problema. Por favor, contacte con un especialista</span>");
                            }
                        });

                        $.magnificPopup.open({
                            items: {
                                src: '.modal-crear'
                            },
                            type: 'inline',
                            closeOnBgClick: false,
                            fixedContentPos: true, fixedBgPos: true, overflowY: 'auto', closeBtnInside: true, preloader: false, midClick: true, removalDelay: 300, mainClass: 'my-mfp-zoom-in'
                        });
                    } else {
                        Snackbar.show({
                            text: '¡No hay personas interesadas en el curso!',
                            pos: 'top-center'
                        });
                    }
                }
            },
            {
                "text": "<i class='icon-feather-users'></i> Eventos",
                "action": function () {
                    var tabla = $("#table_personas_interesadas").DataTable();

                    GetIds(tabla);
                    var contador = localStorage.getItem('IdsPersNotifCusosInte');

                    if (contador.length > 0) {
                        window.location.href = UrlEventos;
                    } else {
                        Snackbar.show({
                            text: '¡No hay personas interesadas en el curso!',
                            pos: 'top-center'
                        });
                    }
                }
            }
        ],
        sDom: 'tF',
        orderCellsTop: true,
        "fnDrawCallback": function () {
            iniciaMp();
        },
        "initComplete": function (settings, json) {
            $(".spinner-container").remove();
            $(".table-container").show();
        }
    });
};