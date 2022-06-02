// Create a date object usable by the formatDate() function
const createDateObject = (date) => {
    tempDate = new Date(date);

    return {
        yyyy: tempDate.getFullYear().toString(),
        mm: (tempDate.getMonth() + 1).toString(), // getMonth() is zero-based         
        dd: tempDate.getDate().toString()
    };
}

// Add the given number of days to the given date
const addDays = (date, nbDays) => {
    const newDate = new Date(date);
    return newDate.setDate(newDate.getDate() + nbDays);
}

// Add the given number of months to the given date
const addMonths = (date, nbMonth) => {
    const newDate = new Date(date);
    return newDate.setMonth(newDate.getMonth() + nbMonth);
};

// Format the given date to the HTML <input type="date"> value format
const formatDate = (date) => {
    const dateObject = createDateObject(date);
    return dateObject.yyyy + '-' + (dateObject.mm[1] ? dateObject.mm : "0" + dateObject.mm[0]) + '-' + (dateObject.dd[1] ? dateObject.dd : "0" + dateObject.dd[0]);
};