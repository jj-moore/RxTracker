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

    document.getElementById('btnCreate').addEventListener('click', () => {
        getPartialView(0);
    });
});

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
                if (tradeNameElement.value) {
                    genericForElement.disabled = true;
                } else if (genericForElement.value) {
                    tradeNameElement.disabled = true;
                }

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

            document.getElementById('btnDelete').addEventListener('click', deleteRecord);
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