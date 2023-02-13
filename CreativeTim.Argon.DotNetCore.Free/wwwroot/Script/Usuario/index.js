$(document).ready(function () {

});

$(document).on('change', '.ddl_StatusUsuario', function () {
  var Id = $(this).attr('id').split('_')[2];

  console.log(Id);

  var body = {
    IdUsuario: Id,
    IdNovoStatus: $(this).val(),
  };

  $.ajax({
    url: "AlterarSituacaoUsuario",
    data: body,
    method: "GET",
    async: true,
    success: function (data) {
      if (data == 200) {

      }
      else {

      }
    },
    error: function (data) {

    }
  })

})

$(document).on('click', '.situacaousuario', function () {

  var body= $(this).val();

  if (body != "") {
    modalAguarde();
    window.location.href = "/Usuario/Index?situacao=" + body;
  }

})
