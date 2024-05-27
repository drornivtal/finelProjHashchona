
import Box from '@mui/material/Box';
import Card from '@mui/material/Card';
import CardActions from '@mui/material/CardActions';
import CardContent from '@mui/material/CardContent';
import Button from '@mui/material/Button';
import Typography from '@mui/material/Typography';
import { Avatar } from '@mui/material';
import { useState } from 'react';

import { postAndPutReqFunction } from '../utilities/ApiUtilities';


export default function RequestDisplay(props) {
    const apiSubmitHelp = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/RequestsForHelp/usersWantToAssist';
    const apiCancelHelp = 'https://proj.ruppin.ac.il/cgroup62/test2/tar1/api/RequestsForHelp/updateUserStatusToReq';

    const userDetails = JSON.parse(localStorage.getItem('user'));

    const [buttonStates, setButtonStates] = useState({}); // State to manage button's text

    async function FetchForApplyOrCancel(obj, api, method) {

        try {
            let response = await postAndPutReqFunction(obj, api, method);
        } catch (error) {
            // Handle error
            console.error('Error in fetch:', error.message);
        }
    };

    const submitHelpFunction = async (reqID) => {
        let jsonObjToPostForHelp = {
            "userID": userDetails.userId,
            "requestForHelpID": reqID
        };
        let response = await FetchForApplyOrCancel(jsonObjToPostForHelp, apiSubmitHelp, 'POST');
    };
    const cancelHelpFunction = async (reqID) => {
        let jsonObjToPostForCancel = {
            "userID": userDetails.userId,
            "requestForHelpID": reqID,
            "statusApproval": "canceled"
        };
        let response = await FetchForApplyOrCancel(jsonObjToPostForCancel, apiCancelHelp, 'PUT');
    };

    const handleButtonClick = async (reqID) => {

        let currentText = buttonStates[reqID] || 'הגש סיוע';

        // Call the appropriate function based on button state
        if (currentText === 'הגש סיוע') {
            await submitHelpFunction(reqID);
        } else {
            await cancelHelpFunction(reqID);
        }
        setButtonStates(prevState => ({
            ...prevState,
            [reqID]: currentText === 'הגש סיוע' ? 'בטל סיוע' : 'הגש סיוע'
        }));
    };

    return (

        <>
            {/* Check! */}
            {console.log(buttonStates)}

            <Card sx={{ textAlign: 'right', backgroundColor: '#f2f2f2', borderRadius: 4, maxHeight: '160px', width: '375px' }}>
                <CardContent >
                    <Typography sx={{ fontSize: 15 }}
                        color="#708DD3" >
                        {/* טל ברק - בקשה 1010 */}
                        {props.UserName}  - בקשה {props.ReqId}

                        <span style={{ marginRight: '7%', border: '1px solid #708DD3', borderRadius: 20, backgroundColor: '#708DD3', opacity: 0.8, color: 'black', padding: '1%', paddingLeft: '3%', paddingRight: '3%', fontSize: 14 }}>
                            {/* יום ד' 7/2 | 20:20 */}

                            {props.DueDateHebrewDay}  {props.DueDate} | {props.DueTime}

                        </span>
                    </Typography>

                    <Typography sx={{ fontSize: 12 }}
                        color="grey" fontWeight={600} gutterBottom>
                        {/* 23/05/24 15:38 */}
                        {props.PostDate} {props.PostTime}
                    </Typography>

                    <Box sx={{ display: 'flex', flexDirection: 'row', direction: 'rtl', marginTop: '1%' }}>
                        <Avatar sx={{ backgroundColor: '#708DD3' }}>{(props.UserName).split('')[0]}</Avatar>
                        <Typography variant="h7" component="h5" sx={{ paddingRight: 1.5, textAlign: 'right' }}>
                            {/* שלום, מחפשת מישהו שיוציא לי את הכלב
                        שלום, מחפשת מישהו שיוציא לי את הכלב שלום, מחפשת מישהו שיוציא */}
                            {props.Description}
                        </Typography>
                    </Box>
                </CardContent>
                <CardActions>
                    <Button size="small"
                        sx={{ backgroundColor: 'rgb(252, 252, 252)', border: '1px solid black', borderRadius: 20, color: 'black', marginTop: '-9%' }}
                        onClick={() => handleButtonClick(props.ReqId)}
                    > {buttonStates[props.ReqId] || 'הגש סיוע'}</Button>
                </CardActions>
            </Card >
        </>
    );
}





