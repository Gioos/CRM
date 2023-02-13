$(document).ready(function () {

  modalAguarde();
  setTimeout(() => {
    fechaModal();
  }, "1000")

});


function modalOk(mensagem = 'Operação realizada com sucesso!', bloqueia = false, rotaOk = '/Home/Index') {
  return new Promise((resolve, reject) => {
    swal({
      html: mensagem,
      type: "success",
      showCloseButton: true,
      allowOutsideClick: !bloqueia,
      allowEscapeKey: true,
      onOpen: () => {
        swal.hideLoading();
      }
    }).then((result) => {
      if (rotaOk) {
        window.location.href = rotaOk;
      }
      resolve(null);
    });
  });
}



function modalErro(mensagem = "Ocorreu um erro na solicitação!", bloqueia = false, campoErro, limpaCampo = true) {
  return new Promise((resolve, reject) => {

    swal({
      html: mensagem,
      type: "error",
      showCloseButton: true,
      allowOutsideClick: !bloqueia,
      allowEscapeKey: !bloqueia,
      onOpen: () => {
        swal.hideLoading();
      }
    }).then((result) => {
      if (campoErro) {
        var campo = $('#' + campoErro);
        if (campo.is("input") && limpaCampo)
          campo.val("");
        else if (campo.is("select") && limpaCampo) {
          $("#" + campoErro).val($("#" + campoErro + " option:first").val());
        }
        if (campo.is("select"))
          $(`[data-id=${campoErro}`).addClass("Borda-Red");


        window.setTimeout(function () {
          campo.addClass('Borda-Red');
          campo.focus();
        }, 400);
      }
      resolve(null);
    });
  });
}


function modalAguarde(titulo = 'Aguarde', bloqueia = true) {
  swal({
    text: titulo,
    showCloseButton: false,
    showCancelButton: false,
    showConfirmButton: false,
    allowOutsideClick: !bloqueia,
    allowEscapeKey: !bloqueia,
    onOpen: () => {
      swal.showLoading();
    }
  });
}

function fechaModal() {
  swal.close();
  swal.hideLoading();
  swal.resetDefaults();
}
