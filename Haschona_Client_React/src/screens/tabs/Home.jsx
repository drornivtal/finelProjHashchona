
import { useState, useEffect } from "react";
import { useNavigate } from "react-router-dom";

import { Box, Button, Container, Grid } from "@mui/material";

import { categoriesList, iconMapping } from "../../utilities/ListsUtilities";
import LogoImage from '../../assets/Logo_TheStreets.png';
import RequestDisplay from "../../components/RequestDisplay";
import { formatDueDate, formatPostDate, formatTime, getDayOfWeekInHebrew } from "../../utilities/FunctionsUtilities";
import { postReqFunction } from "../../utilities/ApiUtilities";


export default function Home() {

    const navigate = useNavigate();
    const [selectedCategories, setSelectedCategories] = useState([]);
    const [allRequests, setAllRequests] = useState([]);

    const userDetails = JSON.parse(localStorage.getItem('user'));
    const userCommunity = JSON.parse(localStorage.getItem('userCommunityId'));

    const apiAllRequests = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/RequestsForHelp/ActiveReqByCommunity';

    // component did mount
    useEffect(() => {
        async function fetchAndSetRequest() {
            try {
                const jsonObjToPost = {
                    "CommunityID": userCommunity
                };
                const fetchedRequests = await postReqFunction(jsonObjToPost, apiAllRequests);

                if (fetchedRequests) {
                    setAllRequests(fetchedRequests);
                    console.log(fetchedRequests);
                }

            } catch (error) {
                // Handle error
                console.error('Error fetching communities:', error.message);
            }
        }
        fetchAndSetRequest();
    }, []);

    const goToNewRequestPage = () => {
        console.log('check!');
        navigate('/NewRequest');
    };

    const handleIconClick = (category) => {
        setSelectedCategories(prevSelected => {
            if (prevSelected.includes(category)) {
                return prevSelected.filter(c => c !== category);
            } else {
                return [...prevSelected, category];
            }
        });
    };

    const filteredRequests = allRequests.filter(request => request.userReqID !== userDetails.userId)
        .filter(request => selectedCategories.length === 0 || selectedCategories.includes(request.categoryId));


    return (
        // <>
        //     <div> this is Home page</div>
        //     <button color="green" onClick={goToNewRequestPage}>+</button>
        // </>
        <Container sx={{ display: 'flex', flexDirection: 'column', height: '90vh', width: '75vw', padding: '5px' }}>
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'flex-start',
                    alignItems: 'center',
                    height: '18vh',
                    width: '100%',
                    // backgroundColor: 'greenyellow',

                }}
            >
                <img
                    src={LogoImage}
                    alt="Logo"
                    style={{
                        maxWidth: '100%',
                        maxHeight: '100%',
                    }}
                />
            </Box>


            <Box
                sx={{
                    backgroundColor: 'lightgray',
                    opacity: 0.8,
                    borderRadius: '10px',
                    height: '10vh',
                    width: '100%',
                    display: 'flex',
                    alignItems: 'center',
                    overflowX: 'auto',  // Enable horizontal scrolling
                    overflowY: 'hidden',
                    whiteSpace: 'nowrap',  // Ensure the children stay in a single line
                    direction: 'rtl',
                    '&::-webkit-scrollbar': {
                        display: 'none',  //  hide scrollbar for Webkit browsers
                    },
                }}>

                {
                    categoriesList.map((category) => {
                        const IconComponent = iconMapping[category.IconName];
                        const isSelected = selectedCategories.includes(category.key);

                        return (

                            <Button
                                key={category.key}
                                onClick={() => handleIconClick(category.key)}
                                sx={{
                                    margin: '3%',
                                    // bgcolor: '#9274B2', //Light Purple
                                    bgcolor: '#708DD3', //LIght blue
                                    bgcolor: isSelected ? '#708DD3' : '#556cd6',
                                    borderRadius: 15,
                                    width: '50px',
                                    height: '50px',
                                    minWidth: '50px',
                                }} >
                                {IconComponent ? (
                                    <IconComponent id={category.key} sx={{ color: 'white', fontSize: '28px' }} />
                                ) : null}
                            </Button>
                        );
                    })
                }

            </Box>

            <Box
                sx={{
                    // backgroundColor: 'orange',
                    backgroundColor: '',
                    height: '53vh',
                    width: '100%',
                    paddingTop: '4%',
                    overflowY: 'auto',
                    '&::-webkit-scrollbar': {
                        display: 'none', // Hide scrollbar for Webkit browsers
                    },
                    msOverflowStyle: 'none', // Hide scrollbar
                }}
            >
                <Grid container rowSpacing={2}
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'flex-start',
                        alignItems: 'center',
                    }}>

                    {
                        filteredRequests.map((request) => (
                            <Grid item xs={12} key={request.reqID}>
                                <RequestDisplay
                                    ReqId={request.reqID}
                                    UserName={'טל ברק'} //~ need to fix in server ~ 
                                    DueDate={formatDueDate(request.dueDate)}
                                    DueTime={formatTime(request.dueTime)}
                                    DueDateHebrewDay={getDayOfWeekInHebrew(request.dueDate)}
                                    PostDate={formatPostDate(request.postDate)}
                                    PostTime={formatTime(request.postTime)}
                                    Description={request.description}
                                />
                            </Grid>
                        ))
                    }
                </Grid>
            </Box>
        </Container >
    );
}
