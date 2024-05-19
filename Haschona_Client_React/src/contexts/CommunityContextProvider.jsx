
import { createContext, useEffect, useState } from "react";
import { getReqAllCommunities } from "../utilities/ApiUtilities";



export const CommunityContext = createContext();

export default function CommunityContextProvider(props) {

    const [communities, setCommunities] = useState([]);
    // const [communities, setCommunities] = useState([
    //     { id: 37, name: 'קרית טבעון' }
    // ]);

    const addCommunity = (obj) => {
        setCommunities([...communities, obj]);
    };

    const removeCommunity = (id) => {
        let newCommunities = communities.filter((community) => (community.id !== id));
        setCommunities(newCommunities);
    }

    const updateCommunity = (obj) => {
        removeCommunity(obj.id);
        addCommunity(obj);
    };


    // component did mount
    useEffect(() => {
        async function fetchAndSetCommunities() {
            try {
                const fetchedCommunities = await getReqAllCommunities();
                if (fetchedCommunities) {
                    setCommunities(fetchedCommunities);
                    console.log(fetchedCommunities);
                }

            } catch (error) {
                // Handle error
                console.error('Error fetching communities:', error.message);
            }
        }
        fetchAndSetCommunities();
    }, []);


    return (
        <CommunityContext.Provider value={{ communities, addCommunity, removeCommunity, updateCommunity }}>
            {props.children}
        </CommunityContext.Provider>
    )
}
