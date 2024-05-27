
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

// CommunitiesMap functions: 

export async function geocodeAddress(address) {
    const url = `https://nominatim.openstreetmap.org/search?q=${encodeURIComponent(address)}&format=json&addressdetails=1`;

    try {
        const response = await fetch(url);
        if (!response.ok) {
            throw new Error('Network response was not ok');
        }
        const data = await response.json();
        if (data.length > 0) {
            const { lat, lon } = data[0];
            return {
                latitude: parseFloat(lat),
                longitude: parseFloat(lon),
            };
        } else {
            throw new Error('Address not found');
        }
    } catch (error) {
        console.error('Error geocoding address:', error);
        throw error;
    }
}

