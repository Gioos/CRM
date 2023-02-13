$(document).on('click', '#ckek_TipoPessoaJuridica', function () {
  modalAguarde();
  $.ajax({
    url: "_partial_PessoaJuridica",
    method: "GET",
    async: true,
    success: function (content) {
      fechaModal();
      $("#div_Partial_Pessoa_JuridicaFisica").html(content);
    },
    error: function () {
      modalErro("Falha de comunicação!", false, null);
    }
  });
});

$(document).on('click', '#ckek_TipoPessoaFisica', function () {
  modalAguarde();
  $.ajax({
    url: "_partial_PessoaFisica",
    method: "GET",
    async: true,
    success: function (content) {
      fechaModal();
      $("#div_Partial_Pessoa_JuridicaFisica").html(content);
    },
    error: function () {
      modalErro("Falha de comunicação!", false, null);
    }
  });
});
