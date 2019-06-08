document.addEventListener('DOMContentLoaded', () => {
    document.getElementById('drugList').addEventListener('change', updateStatistics);
    document.getElementById('doctorList').addEventListener('change', updateStatistics);
    document.getElementById('pharmacyList').addEventListener('change', updateStatistics);
    document.getElementById('includeInactive').addEventListener('click', updateStatistics);
    document.getElementById('includeBrandedGeneric').addEventListener('click', updateStatistics);
    document.getElementById('dateFrom').addEventListener('blur', updateStatistics);
    document.getElementById('dateTo').addEventListener('blur', updateStatistics);
    document.getElementById('costFrom').addEventListener('blur', updateStatistics);
    document.getElementById('costTo').addEventListener('blur', updateStatistics);
    document.getElementById('sortBy').addEventListener('change', updateStatistics);
    document.getElementById('sortDescending').addEventListener('click', updateStatistics);
});

function updateStatistics() {
    const data = getFilters();
    const url = '/Statistics/GetStatisticsJson';

    fetch(url, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: data
    })
        .then(response => response.text())
        .then(text => {
            document.getElementById('partialView').innerHTML = text;
        })
        .catch(error => {
            console.error(error);
        });
}

function getFilters() {
    let filters = {};

    const drugs = document.getElementById('drugList');
    filters.drugId = drugs.options[drugs.selectedIndex].value;
    const doctors = document.getElementById('doctorList');
    filters.doctorId = doctors.options[doctors.selectedIndex].value;
    const pharmacies = document.getElementById('pharmacyList');
    filters.pharmacyId = pharmacies.options[pharmacies.selectedIndex].value;
    filters.dateFrom = document.getElementById('dateFrom').value;
    filters.dateTo = document.getElementById('dateTo').value;
    filters.costFrom = document.getElementById('costFrom').value;
    filters.costTo = document.getElementById('costTo').value;
    filters.includeInactive = document.getElementById('includeInactive').checked ? true : false;
    filters.includeBrandedAndGeneric = document.getElementById('includeBrandedGeneric').checked ? true : false;
    const sortBy = document.getElementById('sortBy');
    filters.sortBy = sortBy.options[sortBy.selectedIndex].value;
    filters.sortDescending = document.getElementById('sortDescending').checked ? true : false;

    const data = JSON.stringify(filters);
    return data;
}