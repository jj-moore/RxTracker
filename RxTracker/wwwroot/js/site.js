document.addEventListener('DOMContentLoaded', () => {
  const listBody = document.getElementById('listBody');
  if (listBody) {
    const rows = listBody.querySelectorAll('tr');
    rows.forEach(row => {
      row.addEventListener('click', e => {
        const targetId = e.target.parentElement.dataset.id;
          getPrescriptionPartial(targetId);
      })
    });
  }

});

function getPrescriptionPartial(prescriptionId) {
    const url = `/Prescription/Details?id=${prescriptionId}`;
    fetch(url)
        .then(response => response.text())
        .then(text => {
            console.log(text)
            const partialDiv = document.getElementById('partialView');
            partialDiv.innerHTML = text;
        });
}