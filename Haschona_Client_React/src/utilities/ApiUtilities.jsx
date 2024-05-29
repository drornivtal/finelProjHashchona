

// Fetch function - "GET"  communites from the server
export async function getReqAllCommunities() {
    const api = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/Communities/ReadApprovedCommunities';
    try {
        const response = await fetch(api, {
            method: 'GET',
            headers: {
                'Content-Type': 'application/json',
            },
        });

        if (!response.ok) {
            throw new Error('Failed to fetch communities');
        }

        const communities = await response.json();
        return communities;
    } catch (error) {
        console.error('Error fetching communities:', error.message);
        throw error;
    }
};

// Fetch function - "POST" - send new object to DB 
export async function postAndPutReqFunction(objToPost, api, method) {

    try {
        const response = await fetch(api, {
            method: method, //method = 'POST' OR 'PUT'
            headers: new Headers({
                'Content-Type': 'application/json; charset=UTF-8'
            }),
            body: JSON.stringify(objToPost)
        });

        console.log('Response status:', response.status);

        if (!response.ok) {
            const errorDetails = await response.text(); // --- Get more details About the error ---
            console.error('Failed to POST:', errorDetails);
            throw new Error('Failed to POST. Please check your credentials.');
        }

        const finalResponse = await response.json();
        console.log('POST methood successful,  the final Response is:', finalResponse);
        return finalResponse;
    } catch (error) {
        console.error('Error POST methood:', error);
        throw new Error('Failed to POST. Please try again.');
    }
};


