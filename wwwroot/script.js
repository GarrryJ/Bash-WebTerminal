var i = 0
var maxId = 0
var getId = 0
var terminalInput = document.getElementById("terminalInput")
initMaxId()

document.addEventListener("keydown", function(event) {
  terminalInput.focus()

  if (event.code == "Enter") {

    if (terminalInput.value == "clear") {

      post("clear")
      clearTable()

    } else if (terminalInput.value != "") {

      newTr(terminalInput.value)
      post(terminalInput.value)

    }
    terminalInput.value = ""
    getId = 0
    maxId++

  } else if (event.code == "ArrowUp") {


    if (getId == 0){
      getId = maxId
    } else if (getId > 1) {
      getId--
    }
    get(getId)
    moveCursorToEnd(terminalInput)

  } else if (event.code == "ArrowDown") {

    if (getId == maxId){
      getId = 0
      terminalInput.value = ""
    } else if (getId != 0) {
      getId++
      get(getId)
    }

  }
});

function moveCursorToEnd(el) {
  window.setTimeout(function () {

        if (typeof el.selectionStart == "number") {

        el.selectionStart = el.selectionEnd = el.value.length

      } else if (typeof el.createTextRange != "undefined") {

        var range = el.createTextRange()
        range.collapse(false)
        range.select()

      }
  }, 1);
}

function clearTable() {
  var Table = document.getElementById("myTable")
  Table.innerHTML = ""
  i = 0
}

function newTr(value) {
  var tr = document.createElement("TR")
  var s = "myTr" + i
  i++
  tr.setAttribute("id", s)
  document.getElementById("myTable").appendChild(tr)
  var td = document.createElement("TD")
  var text = document.createTextNode(value)
  td.appendChild(text)
  document.getElementById(s).appendChild(td)
}

async function get(id){
  var url = "api/values/" + id
  let response = await fetch(url)

  if (response.ok) {

    let result = await response.text()
    terminalInput.value = result

  }
}

async function initMaxId(){
  let response = await fetch("api/values")
  if (response.ok) {

    let result = await response.text()
    maxId = result

  }
}

async function post(value){
  let response = await fetch("api/values", {
    method: "POST",
    headers: { "Accept": "application/json", "Content-Type": "application/json" },
    body: JSON.stringify(value)
  })

  if (response.ok) {

    let result = await response.json()

    initMaxId()

    if (result != ""){

      newTr(result)

    }

  }
}
