/// <summary>
/// Classe que aceita somente Numeros "/"
/// </summary>
$('.apenasnumeros').bind({
  keypress: function (event) {
    var tecla = (window.event) ? event.keyCode : event.which;
    if (tecla > 47 && tecla < 58) {
      return true;
    }
    else {
      if (tecla !== 8) {
        return false;
      }
      else {
        return true;
      }
    }
  },
  paste: function () {
    var thisAux = this;
    setTimeout(function () {
      var old = thisAux.value;
      thisAux.value = '';
      for (i = 0; i < old.length; i++) {
        if (old[i].match(/[0-9]+$/)) {
          thisAux.value = thisAux.value + old[i];
        }
      }
    }, 100);
  }
});
