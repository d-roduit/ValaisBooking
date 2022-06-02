document.addEventListener('DOMContentLoaded', () => {
    // Manager checkIn and checkOut dates
    const checkIn = document.getElementById('SearchHotelsViewModel_CheckIn');
    const checkOut = document.getElementById('SearchHotelsViewModel_CheckOut');

    const checkInMinDate = new Date();
    let checkOutMinDate = addDays(checkInMinDate, 1);
    let checkOutMaxDate = addMonths(checkInMinDate, 1);

    // If the checkIn date field is already filled on the page load
    if (checkIn.value != "") {
        checkOutMinDate = addDays(new Date(checkIn.value), 1);
        checkOutMaxDate = addMonths(new Date(checkIn.value), 1);
    }

    checkIn.setAttribute("min", formatDate(checkInMinDate));
    checkOut.setAttribute("min", formatDate(checkOutMinDate));
    checkOut.setAttribute("max", formatDate(checkOutMaxDate));

    let oldCheckInDate = checkIn.value;

    checkIn.addEventListener("blur", () => {
        if (checkIn.value != oldCheckInDate) { // If the user has effectively changed the date
            oldCheckInDate = checkIn.value;

            currentCheckInDate = new Date(checkIn.value);

            checkOut.setAttribute("min", formatDate(addDays(currentCheckInDate, 1)));
            checkOut.setAttribute("max", formatDate(addMonths(currentCheckInDate, 1)));
        }
    });


    // Manager maxPrice slider
    const maxPriceSpan = document.getElementById('maxPriceSpan');
    const maxPriceInput = document.getElementById('SearchHotelsViewModel_MaxPrice');

    maxPriceInput.addEventListener("input", () => {
        maxPriceSpan.textContent = maxPriceInput.value;
    });

});