document.addEventListener('DOMContentLoaded', () => {
  const listBody = document.getElementById('listBody');
  if (listBody) {
    const rows = listBody.querySelectorAll('tr');
    rows.forEach(row => {
      row.addEventListener('click', e => {
        const targetId = e.target.parentElement.dataset.id;
        console.log(targetId);
      })
    });
  }

});
