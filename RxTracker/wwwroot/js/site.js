document.addEventListener('DOMContentLoaded', () => {
  const listBody = document.getElementById('listBody');
  if (listBody) {
    const rows = listBody.querySelectorAll('tr');
    rows.forEach(row => {
      row.addEventListener('click', e => {
        const targetId = e.target.parentElement.dataset.id;
          getPartialView(targetId);
      })
    });
  }

});

function getPartialView(targetId) {
    const controller = document.getElementById('View').value;
    const url = `/${controller}/Details?id=${targetId}`;
    fetch(url)
        .then(response => response.text())
        .then(text => {
            console.log(text)
            const partialDiv = document.getElementById('partialView');
            partialDiv.innerHTML = text;
        });
}