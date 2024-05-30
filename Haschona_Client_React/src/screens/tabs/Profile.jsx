
import { useState } from "react";
import { Avatar, Box, Button, Container, Grid, IconButton, Rating, Typography } from "@mui/material";
import AddIcon from '@mui/icons-material/Add';

import { categoriesList, iconMapping } from "../../utilities/ListsUtilities";
import RequestDisplay from "../../components/RequestDisplay";

import LogoImage from '../../assets/Logo_TheStreets.png';
import '../../styles/ProfileStyles.css';

export default function Profile(props) {

    const [userCategories, setUserCategories] = useState([4, 3, 2]);
    // need add a function that take  a categories List from server!

    const userDetails = JSON.parse(localStorage.getItem('user'));
    const userCommunity = JSON.parse(localStorage.getItem('userCommunityId'));

    return (
        <Container
            sx={{
                display: 'flex', flexDirection: 'column',
                height: '90vh', width: '80vw', padding: '0px'
            }}>

            {/* Logo Img Box */}
            <Box
                sx={{
                    display: 'flex',
                    flexDirection: 'column',
                    justifyContent: 'flex-start',
                    alignItems: 'center',
                    height: '20vh',
                    width: '100%',
                    marginTop: -7
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

            {/* Profile deatiles Box */}
            <Box
                sx={{
                    backgroundColor: '', borderRadius: 4,
                    width: '100%', display: 'flex',
                    flexDirection: 'column', alignItems: 'end',
                    marginTop: -5
                }}>

                <Box sx={{ display: 'flex', flexDirection: 'row', direction: 'rtl', marginTop: '1%' }}>
                    <Avatar sx={{ backgroundColor: '#708DD3', width: 68, height: 68 }}
                        alt={(userDetails.firstName).split('')[0]}
                        src={userDetails.profilePicture} />
                    <Box
                        sx={{
                            display: 'flex', flexDirection: 'column',

                        }}>
                        <Typography variant="h7" component="h4" sx={{ paddingRight: 1.5, textAlign: 'right' }}>
                            {/* דרור כהן */}
                            {userDetails.firstName + " " + userDetails.lastName}
                        </Typography>

                        <Rating
                            name="half-rating-read"
                            value={userDetails.rating}
                            readOnly
                            precision={0.5}
                            size="medium"
                            sx={{
                                direction: 'ltr',
                                paddingLeft: 1,
                                transform: 'scaleX(-1)'
                            }}
                        />

                        <Typography variant="h7" component="h5" sx={{ paddingRight: 1.5, textAlign: 'right' }}>
                            {/* הי אני דרור, אוהבת ילדים ולנקות */}
                            {userDetails.description}
                        </Typography>
                    </Box>
                </Box>
            </Box>

            {/* Score Box */}
            <Box
                sx={{
                    width: '100%', display: 'flex',
                    flexDirection: 'column', alignItems: 'end',
                    marginTop: 3
                }}>
                <Button id="btnMyOffers">ההצעות שלי</Button>
                <Box
                    sx={{ display: 'flex', flexDirection: 'row', alignItems: 'start', justifyContent: 'end', width: '100%' }}>
                    <Button id="btnBuyCoupons">ממש</Button>
                    <Typography
                        variant="body"
                        component="p"
                        sx={{
                            paddingRight: 1,
                            textAlign: 'right',
                            fontWeight: '600'
                        }}>
                        הניקוד שלי : {userDetails.score}
                    </Typography>
                </Box>
            </Box>

            {/* My categories Box */}
            <Box>
                <Typography
                    variant="body2"
                    component="p"
                    sx={{
                        paddingRight: 1,
                        textAlign: 'right',
                        fontWeight: '600'
                    }}>
                    תחומי העניין שלי
                </Typography>
                <Box
                    sx={{
                        backgroundColor: '#e0e0e0',
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
                            if (userCategories.includes(category.key))

                                return (
                                    <Avatar
                                        key={category.key}
                                        sx={{
                                            width: 50,
                                            height: 50,
                                            bgcolor: '#DABB80',
                                            margin: 2
                                        }}
                                    >
                                        {IconComponent ? (
                                            <IconComponent id={category.key}
                                                sx={{ color: 'black', fontSize: '30px' }} />
                                        ) : null}
                                    </Avatar>
                                );
                        })

                    }
                    <Avatar
                        sx={{
                            width: 50,
                            height: 50,
                            bgcolor: '#DABB80',
                            color: 'black',
                            margin: 2
                        }}
                    >
                        <IconButton aria-label="add" onClick={() => console.log("Clicked!")}>
                            <AddIcon />
                        </IconButton>
                    </Avatar>
                </Box>
            </Box>

            {/* Display Request Box */}
            <Box
                sx={{
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
                {/* <Grid container rowSpacing={2}
                    sx={{
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'flex-start',
                        alignItems: 'center',
                    }}>
                    {

                        <Grid item xs={12} >
                            <RequestDisplay
                                ReqId={1008}
                                // UserName={requestObj.fullNameOfReq} //~ need to fix in server ~ 
                                DueDate={'10/06/24'}
                                DueTime={'10:30'}
                                // DueDateHebrewDay={getDayOfWeekInHebrew(requestObj.request.dueDate)}
                                PostDate={'23/05/24'}
                                PostTime={'15:38'}
                                Description={"מחפשת מישהו שיוציא לי את הכלב"}
                                // profileImg={""}
                                myProfile={true}
                                UserName=''

                            />
                        </Grid>

                    }
                </Grid> */}
            </Box>
        </Container>
    )
}
