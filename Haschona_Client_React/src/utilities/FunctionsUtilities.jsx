
// dates functions: 

const daysOfWeekInHebrew = [
    "יום א'",
    "יום ב'",
    "יום ג'",
    "יום ד'",
    "יום ה'",
    "יום ו'",
    "יום ש'"
];

export const formatPostDate = (dateString) => {
    const options = { day: '2-digit', month: '2-digit', year: '2-digit' };
    return new Date(dateString).toLocaleDateString('en-GB', options);
};
export const formatDueDate = (dateString) => {
    const options = { day: '2-digit', month: '2-digit' };
    return new Date(dateString).toLocaleDateString('en-GB', options);
};
export const formatTime = (timeString) => {
    const [hours, minutes] = timeString.split(':');
    return `${hours}:${minutes}`;
};

export const getDayOfWeekInHebrew = (dateString) => {
    const date = new Date(dateString);
    const dayOfWeek = date.getDay();
    return daysOfWeekInHebrew[dayOfWeek];
};
