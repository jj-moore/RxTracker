﻿document.addEventListener('DOMContentLoaded', () => {
    const listBody = document.getElementById('listBody');
    if (listBody) {
        const rows = listBody.querySelectorAll('tr');
        rows.forEach(row => {
            row.addEventListener('click', updateDetails);
        });
    }

    const btnCreate = document.getElementById('btnCreate');
    if (btnCreate) {
        btnCreate.addEventListener('click', () => {
            getPartialView(0);
        });
    }

    const monthCost = document.getElementById('monthCost');
    if (monthCost) {
        monthCost.addEventListener('change', updateCost);
    }
});

function updateDetails(event) {
    const targetId = event.target.parentElement.dataset.id;
    const listBody = document.getElementById('listBody');
    let i = listBody.rows.length;
    while (--i >= 0) {
        if (listBody.rows[i].dataset.id == targetId) {
            listBody.rows[i].classList.add('selected');
        } else {

            listBody.rows[i].classList.remove('selected');
        }
    }
    getPartialView(targetId);
}

function getPartialView(targetId) {
    const controller = document.getElementById('View').value;
    const url = `/${controller}/Details?id=${targetId}`;
    fetch(url)
        .then(response => response.text())
        .then(text => {

            const partialDiv = document.getElementById('partialView');
            partialDiv.innerHTML = text;
        })
        .then(() => {
            if (controller == 'Drug') {
                const tradeNameElement = document.getElementById('Drug_TradeName');
                const genericForElement = document.getElementById('Drug_GenericForId');
                tradeNameElement.disabled = genericForElement.value != 0;
                genericForElement.disabled = tradeNameElement.value;

                tradeNameElement.addEventListener('blur', event => {
                    console.log(event.target);
                    const genericForElement = document.getElementById('Drug_GenericForId');
                    if (event.target.value) {
                        genericForElement.disabled = true;
                    } else {
                        genericForElement.disabled = false;
                    }
                });

                genericForElement.addEventListener('change', event => {
                    console.log(event.target);
                    const tradeNameElement = document.getElementById('Drug_TradeName');
                    if (event.target.value) {
                        tradeNameElement.disabled = false;
                    } else {
                        tradeNameElement.disabled = true;
                    }
                })
            }

            const btnSave = document.getElementById('btnSave');
            if (btnSave) {
                btnSave.addEventListener('click', saveRecord);
            }
            document.getElementById('btnDelete').addEventListener('click', deleteRecord);
        })
        .catch(error => {
            console.error(error);
        });



}

function saveRecord() {
    const controller = document.getElementById('View').value;
    switch (controller) {
        case 'Doctor':
            saveDoctor();
            break;
    }
}

function saveDoctor() {
    const formElement = document.getElementById('myForm');
    const formData = new FormData(formElement);
    var object = {};
    formData.forEach((value, key) => {
        object[key] = value
    });
    const json = JSON.stringify(object);

    const url = '/Doctor/Edit';
    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: json
    })
        .then(response => response.text())
        .then(text => {
            const doctorId = document.getElementById('recordId').value;
            if (doctorId != 0) {
                const tableRow = document.querySelector(`[data-id="${doctorId}"]`);
                tableRow.firstElementChild.innerText = object.Name;
                tableRow.lastElementChild.innerText = object.Hospital;
            } else {
                const table = document.getElementById('listBody');
                const tr = document.createElement('tr');
                tr.setAttribute('data-id', text);
                tr.addEventListener('click', e => {
                    const targetId = e.target.parentElement.dataset.id;
                    getPartialView(targetId);
                });
                let td = document.createElement('td');
                td.innerText = object.Name;
                tr.appendChild(td);
                td = document.createElement('td');
                td.innerText = object.Hospital;
                tr.appendChild(td);
                table.appendChild(tr);
                document.getElementById('recordId').value = text;
            }

        })
        .catch(error => {
            console.error(error);
        });
}

function deleteRecord() {
    const data = document.getElementById('recordId').value
    const controller = document.getElementById('View').value;
    const url = `/${controller}/Delete`;
    console.log(url);
    fetch(url, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: data
    })
        .then(response => {
            document.getElementById('partialView').innerHTML = '';
            const listBody = document.getElementById('listBody');
            const deleteTr = listBody.querySelector(`[data-id="${data}"`);
            deleteTr.parentElement.removeChild(deleteTr);
        })
        .catch(error => {
            console.error(error);
        });
}

function updateCost(event) {
    const months = event.target.value;
    console.log(months);
    const url = `/Dashboard/GetCost?months=${months}`;
    fetch(url)
        .then(response => response.json())
        .then(json => {
            console.log(json);
            document.getElementById('monthAmount').innerText = json.cost;
        })
        .catch(error => {
            console.error(error);
        });
}